using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedCameraProperties
{
    public class CameraPropertiesChangedEventArgs : EventArgs
    {
        public float AspectRatio { get; set; }
        public bool AutoAspectRatio { get; set; }
        public float FarClip { get; set; }
        public float NearClip { get; set; }
        public bool UseClipping { get; set; }
        public bool FlipVertical { get; set; }
        public float Skew { get; set; }
        public string Name { get; set; }
        public bool Orthographic { get; set; }
        public float OrthographicSize { get; set; }
        public float FieldOfView { get; set; }
        public float LODBias { get; set; }
        public float Zoom { get; set; }
    }
}
