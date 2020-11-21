using System;

namespace ExtendedModelProperties
{
    public class MaterialChangedEventArgs : EventArgs
    {
        public string MaterialFilePath { get; set; }
    }
}
