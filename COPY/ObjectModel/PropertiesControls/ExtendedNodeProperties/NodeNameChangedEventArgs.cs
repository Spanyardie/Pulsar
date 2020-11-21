using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedNodeProperties
{
    public class NodeNameChangedEventArgs : EventArgs
    {
        public string NodeName { get; set; }
        public string OldNodeName { get; set; }
    }
}
