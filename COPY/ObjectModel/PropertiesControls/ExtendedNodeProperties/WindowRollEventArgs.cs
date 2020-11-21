using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedNodeProperties
{
    public class WindowRollEventArgs : EventArgs
    {
        public BasicNodeProperties.WindowRoll WindowRoll { get; set; }
    }
}
