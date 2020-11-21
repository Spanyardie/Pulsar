using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace ExtendedNodeProperties
{
    public class ValueChangedEventArgs : EventArgs
    {
        public Vector3 ValueChanged { get; set; }
    }
}
