using Pulsar.ObjectModel.Messaging;
using Pulsar.ObjectModel.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Urho;
using Urho.Actions;

namespace Pulsar.ObjectModel
{
    public class PulsarActions : Component
    {
        private List<PulsarAction> _actions;

        public new Node Node { get; set; }
        public BaseEntity BaseEntity { get; set; }

        public PulsarActions()
        {
            _actions = new List<PulsarAction>();
        }

        public PulsarActions(Node node)
        {
            Node = node;
            _actions = new List<PulsarAction>();
        }

        public List<PulsarAction> ActionList 
        { 
            get
            {
                return _actions;
            }
            set 
            {
                _actions = value;
            } 
        }

        public void Update(float timeStep)
        {
            //Actions are not part of the scene and, so, we cannot set ReceiveSceneUpdates in the constructor
            //In this case the BaseEntity is set to ReceiveSceneUpdates and then calls this method
            //if there are any actions so that updates can be performed

            //make sure that if there are any actions being performed we update the gizmo pos/rot
            if (_actions.Count() > 0)
            {
                //if any actions are running then update
                if (ActionsAreRunning())
                {
                    if (BaseEntity != null)
                    {
                        //get the gizmo
                        Gizmo gizmo = BaseEntity.GetGizmo();
                        if (gizmo != null)
                        {
                            gizmo.Node.Position = Node.Position;
                            gizmo.Node.Rotation = Node.Rotation;

                            //now we need to create messages so the properties of this actions node
                            //get updated whilst the actions run
                            PulsarMessage message = new PulsarMessage
                            {
                                Type = PulsarMessage.MessageType.NodeTranslationChange,
                                Iterations = 1
                            };
                            message.Properties.Add("nodeName", Node.Name);
                            message.Properties.Add("changeType", PulsarMessage.MessageType.NodeTranslationChange);
                            message.Properties.Add("sceneObject", Node);
                            message.Properties.Add("externallySet", true);
                            BaseEntity.PulsarScene.GetApplication().MessageQueue.PushMessage(message);

                            message = new PulsarMessage
                            {
                                Type = PulsarMessage.MessageType.NodeRotationChange,
                                Iterations = 1
                            };
                            message.Properties.Add("nodeName", Node.Name);
                            message.Properties.Add("changeType", PulsarMessage.MessageType.NodeRotationChange);
                            message.Properties.Add("sceneObject", Node);
                            message.Properties.Add("externallySet", true);
                            BaseEntity.PulsarScene.GetApplication().MessageQueue.PushMessage(message);
                        }
                    }
                }

                RemoveCompletedActions();
            }
        }

        private void RemoveCompletedActions()
        {
            if(_actions.Count() > 0)
            {
                var completedActions = _actions.FindAll(action => action.IsDone && !action.RetainAction);
                if(completedActions != null && completedActions.Count > 0)
                {
                    foreach(PulsarAction pulsarAction in completedActions)
                    {
                        _actions.Remove(pulsarAction);
                    }
                }
            }
        }

        private bool ActionsAreRunning()
        {
            bool areRunning = false;

            foreach(PulsarAction pulsarAction in _actions)
            {
                if (!pulsarAction.IsDone)
                {
                    areRunning = true;
                    break;
                }
            }
            return areRunning;
        }

        public PulsarAction AddAction(PulsarAction action)
        {
            List<PulsarAction> singleAction = new List<PulsarAction>
            {
                action
            };
            return AddActions(singleAction, PulsarAction.ActionType.Single);
        }

        public PulsarAction AddSequence(List<PulsarAction> actionsList)
        {
            return AddActions(actionsList, PulsarAction.ActionType.Sequence);
        }

        public PulsarAction AddParallel(List<PulsarAction> actionsList)
        {
            return AddActions(actionsList, PulsarAction.ActionType.Parallel);
        }

        private PulsarAction AddActions(List<PulsarAction> actionsList, PulsarAction.ActionType actionType)
        {
            PulsarAction pulsarAction = null;
            switch (actionType)
            {
                case PulsarAction.ActionType.Single:
                    pulsarAction = actionsList?.ElementAt(0);
                    break;
                case PulsarAction.ActionType.Parallel:
                    pulsarAction = AddActionParallel(actionsList);
                    break;
                case PulsarAction.ActionType.Sequence:
                    pulsarAction = AddActionSequence(actionsList);
                    break;
            }
            
            _actions.Add(pulsarAction);

            return pulsarAction;
        }

        private PulsarAction AddActionSequence(List<PulsarAction> pulsarActions)
        {
            var pulsarAction = new PulsarAction(Node, false)
            {
                Type = PulsarAction.ActionType.Sequence
            };
            pulsarAction.AddActionsToSet(pulsarActions);

            return pulsarAction;
        }

        private PulsarAction AddActionParallel(List<PulsarAction> pulsarActions)
        {
            var pulsarAction = new PulsarAction(Node, false)
            {
                Type = PulsarAction.ActionType.Parallel
            };
            pulsarAction.AddActionsToSet(pulsarActions);

            return pulsarAction;
        }

        public int RemoveActionByID(string actionID)
        {
            int returnValue = 0;

            returnValue = _actions.RemoveAll(act => act.ID == actionID);

            return returnValue;
        }
    }
}
