using System;
using System.Collections.Generic;

namespace Pulsar.ObjectModel.Messaging
{
    public class PulsarMessage
    {
        public enum PropertyType
        {
            PropertySet = 0,
            PropertyChanged,
            Refresh,
            Update
        }

        public enum MessageType
        {
            // Node specific
            NodeNameChanged = 0,
            NodeRotationChange,
            NodeScaleChange,
            NodeTranslationChange,

            // Properties specific
            ResetPropertiesWindow,
            ShowObjectProperties,
            RefreshPropertiesGrid,

            // TreeView specific
            RefreshTreeView,

            // Form specific
            AdjustSceneTreeFormWidth,
            AdjustPropertiesFormWidth,
            AdjustRenderFormWidth,

            ClearSelectedObjects,

            MouseWheelZoomIn,
            MouseWheelZoomOut,

            DraggingStopped,

            AddedModel,
            ModelChanged,
            AddedMaterial,
            MaterialChanged
        }

        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime TimeToLive { get; set; }
        public string Destination { get; set; }
        public int Delay { get; set; }
        public MessageType Type { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public string ID { get; private set; }
        public int Iterations { get; set; }
        public bool MarkedForDeletion { get; set; }
        public List<Dependency> Dependencies { get; set; }
        public bool HasDependencies { get { return Dependencies.Count > 0; } }

        public PulsarMessage()
        {
            GenerateID();
            Properties = new Dictionary<string, object>();
            Dependencies = new List<Dependency>();
        }

        private void GenerateID()
        {
            //generate an ID from the current tick 
            ID = DateTime.Now.Ticks.ToString();
        }
    }
}
