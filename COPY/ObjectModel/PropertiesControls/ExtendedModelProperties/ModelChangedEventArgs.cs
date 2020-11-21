using System;

namespace ExtendedModelProperties
{
    public class ModelChangedEventArgs : EventArgs
    {
        public string ModelFilePath { get; set; }
    }
}
