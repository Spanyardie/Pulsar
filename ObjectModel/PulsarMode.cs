using System;
using static Pulsar.ObjectModel.PulsarAction;

namespace Pulsar.ObjectModel
{
    public class PulsarMode
    {
        private ActionType _mode;
        public ActionType Mode 
        { 
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }

        private string _id;
        public string ID 
        { 
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private void GenerateID()
        {
            //generate an ID from the current tick 
            _id = DateTime.Now.Ticks.ToString();
        }

        private PulsarAction _action;
        public PulsarAction Action 
        { 
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }

        private bool _completed;
        public bool Completed 
        { 
            get
            {
                return _completed;
            }
            set
            {
                _completed = value;
            }
        }

        public PulsarMode()
        {
            GenerateID();
            _completed = false;
        }
    }
}
