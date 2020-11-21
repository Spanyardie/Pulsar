using Pulsar.ExceptionsHandling;
using Pulsar.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace Pulsar.ObjectModel.Messaging
{
    public class MessageQueue
    {
        #region Private variables
        private readonly Timer _queueTimer;
        private bool processing;
        private readonly List<PulsarMessage> _pendingMessages;
        #endregion

        #region Public accessors
        public List<PulsarMessage> Queue { get; set; }
        public List<Registrant> Registrants { get; set; }
        #endregion

        #region constructor/destructor
        public MessageQueue()
        {
            Queue = new List<PulsarMessage>();
            Registrants = new List<Registrant>();
            _pendingMessages = new List<PulsarMessage>();

            _queueTimer = new Timer(50);

            _queueTimer.Elapsed += QueueTimer_Elapsed;

            _queueTimer.Start();
        }
        ~MessageQueue()
        {
            if (_queueTimer != null)
            {
                _queueTimer.Stop();
                _queueTimer.Dispose();
            }
        }
        #endregion

        #region Private methods
        private void QueueTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _queueTimer.Stop();

            try
            {
                ProcessMessages();
            }
            catch(PulsarMessageException processMessageException)
            {
                processMessageException.Source = "[MessageQueue:QueueTimer_Elapsed]";
                processMessageException.Message = "ProcessMessages failed!";
                //restart the queue even on error
                _queueTimer.Start();
                throw processMessageException;
            }
            _queueTimer.Start();
        }

        private void ProcessMessages()
        {
            if (processing) return;

            //no need to carry on if there are no registrants
            if (Registrants.Count == 0)
            {
                processing = false;
                return;
            }

            processing = true;

            //to begin with, a little housekeeping, remove all messages marked for deletion in a previous pass
            //Odd issue where null messages appear - there shouldn't be any
            //using ToList ensures a copy is made and fixes issue where the queue is altered elsewhere (for example
            //by a new message being added to the queue on a different thread) - the copy is only accessible here
            foreach (PulsarMessage message in Queue.ToList()) 
            {
                if (message == null)
                {
                    try
                    {
                        Queue.Remove(message); 
                    }
                    catch (PulsarMessageException removeMessageException)
                    {
                        Debug.Print("Not sure how I'm going to stop this error when processing the message queue to remove null messages\n" + removeMessageException.Message);
                        removeMessageException.Source = "[MessageQueue:ProcessMessages]";
                        removeMessageException.Message = "Unable to remove message from the System Queue.";
                        throw removeMessageException;
                    }
                }
            }

            try
            {
                Queue.RemoveAll(message => message.MarkedForDeletion);
            }
            catch(PulsarMessageException removeAllMarkedMessagesException)
            {
                Debug.Print("Despite all my efforts, I still am getting null messages, which just SHOULDN'T happen!!!\n" + removeAllMarkedMessagesException.Message);
                removeAllMarkedMessagesException.Source = "[MessageQueue:ProcessMessages]";
                removeAllMarkedMessagesException.Message = "Unable to remove messages marked for deletion.";
                throw removeAllMarkedMessagesException;
            }

            //add any pending messages to the queue
            if (_pendingMessages.Count > 0)
            {
                //Remove nulls, if any
                _pendingMessages.RemoveAll(nulls => nulls == null);

                try
                {
                    Queue.AddRange(_pendingMessages);
                }
                catch(PulsarMessageException addRangeMessageException)
                {
                    Debug.Print("MessageQueue.ProcessMessages - " + addRangeMessageException.Message);
                }
                _pendingMessages.Clear();
            }

            //if there are no current messages to process
            if (Queue.Count == 0)
            {
                processing = false;
                return;
            }

            //ok, we have messages and registrants so process the queue
            foreach (PulsarMessage message in Queue)
            {
                if (message != null)
                {
                    if (message.HasDependencies)
                    {
                        bool passesDependencyCheck = PerformDependencyCheck(message.Dependencies);
                        //leave message till next pass if dependency doesn't have the correct value
                        if (!passesDependencyCheck) continue;
                    }
                    //determine the registrants that are subscribed to this message type
                    var subscribed = Registrants.FindAll(item => item.Type == message.Type);
                    if (subscribed != null)
                    {
                        if (subscribed.Count > 0)
                        {
                            foreach (Registrant registrant in subscribed)
                            {
                                registrant.Subscriber.CallBack(message);
                            }
                        }
                    }

                    //decide what to do now with the message now it has been sent
                    DetermineMessageViability(message);
                }
            }
            processing = false;
        }

        private static bool PerformDependencyCheck(List<Dependency> dependencies)
        {
            bool passesDependencyCheck = true;

            if (dependencies != null)
            {
                foreach (Dependency dependency in dependencies)
                {
                    if (dependency != null)
                    {
                        if (dependency.Source != null && !string.IsNullOrEmpty(dependency.Property))
                        {
                            object propertyValue = ReflectionHelper.GetPropValue(dependency.Source, dependency.Property);
                            object dependencyValue = dependency.Value;
                            if (propertyValue != null)
                            {
                                if (!propertyValue.Equals(dependencyValue))
                                {
                                    passesDependencyCheck = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return passesDependencyCheck;
        }

        private void DetermineMessageViability(PulsarMessage message)
        {
            if (message != null)
            {
                //first, we are going to look at the iterations (-1 indicates an indefinite message, so exit if we find this)
                if (message.Iterations == -1) 
                    return;

                //next we'll look at the iterations again to see if it has a value > 0
                if (message.Iterations > 0) 
                { 
                    if (!IsViableIterative(message)) 
                        return; 
                }

                //check time to live
                if (message.TimeToLive <= DateTime.Now) 
                    message.MarkedForDeletion = true; 
            }
        }

        private static bool IsViableIterative(PulsarMessage message)
        {
            bool returnValue = true;
            if (message != null)
            {
                //if the decrement has resulted in a zero value then queue this message for deletion in the next pass
                if (--message.Iterations == 0)
                {
                    message.MarkedForDeletion = true;
                    returnValue = false;
                }
                return returnValue;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Public methods
        public void PushMessage(PulsarMessage message)
        {
            if (message != null)
            {
                //if we are currently processing messages add this message to the pending list
                if (processing)
                {
                    try
                    {
                        _pendingMessages.Add(message);
                    }
                    catch(PulsarMessageException pendingMessageAddException)
                    {
                        pendingMessageAddException.Source = "[MessageQueue:PushMessage]";
                        pendingMessageAddException.Message = "Unable to add message to Pending list.";
                        throw pendingMessageAddException;
                    }
                }
                else
                {
                    try
                    {
                        Queue.Add(message);
                    }
                    catch (PulsarMessageException systemQueueMessageAddException)
                    {
                        systemQueueMessageAddException.Source = "[MessageQueue:PushMessage]";
                        systemQueueMessageAddException.Message = "Unable to add message to System Queue.";
                        throw systemQueueMessageAddException;
                    }
                }
            }
        }

        public void PopMessage(PulsarMessage message)
        {
            try
            {
                Queue.Remove(message);
            }
            catch(PulsarMessageException removeMessageException)
            {
                removeMessageException.Source = "[MessageQueue:PopMessage]";
                removeMessageException.Message = "Unable to remove message from System Queue.";
                throw removeMessageException;
            }
        }

        public void RegisterForMessage(Registrant subscriber)
        {
            if (subscriber != null)
            {
                try
                {
                    Registrants.Add(subscriber);
                }
                catch(PulsarMessageException addRegistrantException)
                {
                    addRegistrantException.Source = "[MessageQueue:RegisterForMessage]";
                    addRegistrantException.Message = "Unable to add subscriber to Registrant list.";
                    throw addRegistrantException;
                }
            }
        }

        public void RemoveRegistration(Registrant subscriber, PulsarMessage.MessageType messageType)
        {
            if (subscriber != null)
            {
                var registrantToRemove = Registrants.Find(reg => reg.Subscriber == subscriber && reg.Type == messageType);
                if (registrantToRemove != null)
                {
                    try
                    {
                        Registrants.Remove(registrantToRemove);
                    }
                    catch(PulsarMessageException removeRegistrantException)
                    {
                        removeRegistrantException.Source = "[MessageQueue:RemoveRegistration]";
                        removeRegistrantException.Message = "Unable to remove subscriber from Registrant list";
                        throw removeRegistrantException;
                    }
                }
            }
        }

        public void StopMessagingQueue()
        {
            _queueTimer.Stop();
        }
        #endregion
    }
}
