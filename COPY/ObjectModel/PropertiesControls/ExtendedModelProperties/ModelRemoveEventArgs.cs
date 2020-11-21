using System;
using Urho;

namespace ExtendedModelProperties
{
    public class ModelRemoveEventArgs : EventArgs
    {
        public string ModelNodeName { get; set; }
        public Node Node { get; set; }
    }
}
