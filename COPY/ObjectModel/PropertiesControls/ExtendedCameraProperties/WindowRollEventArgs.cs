using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedCameraProperties
{
    public class WindowRollEventArgs : EventArgs
    {
            public CameraProperties.WindowRoll WindowRoll { get; set; }
    }
}
