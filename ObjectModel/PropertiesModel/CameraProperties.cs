using System.Linq;
using Urho;

namespace Pulsar.ObjectModel.PropertiesModel
{
    public class CameraProperties : PulsarComponent
    {
        private float _aspectRatio;
        private bool _autoAspectRatio;
        private float _farClip;
        private float _nearClip;
        private bool _useClipping;
        private bool _flipVertical;
        private float _skew;
        private float _fieldOfView;
        private float _levelOfDetailBias;
        private float _zoom;
        private uint _viewMask;
        private bool _orthographic;
        private float _orthographicSize;
        private Camera _node;

        private bool _isMainCamera = false;

        private bool _externallySet;

        private ExtendedCameraProperties.CameraProperties _container;
        public ExtendedCameraProperties.CameraProperties Container 
        { 
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        public new Camera Node 
        { 
            get
            {
                return _node;
            }
            set
            {
                _node = value;
            }
        }

        public bool ExternallySet
        {
            get
            {
                return _externallySet;
            }
            set
            {
                _externallySet = value;
            }
        }

        public bool IsMainCamera
        {
            get
            {
                return _isMainCamera;
            }
            set
            {
                _isMainCamera = value;
            }
        }

        public float AspectRatio
        {
            get
            {
                return _aspectRatio;
            }
            set
            {
                _aspectRatio = value;
                Application.InvokeOnMain(Update);
            }
        }

        public bool AutoAspectRatio
        {
            get
            {
                return _autoAspectRatio;
            }
            set
            {
                _autoAspectRatio = value;
                Application.InvokeOnMain(Update);
            }
        }

        public float FarClip
        {
            get
            {
                return _farClip;
            }
            set
            {
                _farClip = value;
                Application.InvokeOnMain(Update);
            }
        }

        public float NearClip
        {
            get
            {
                return _nearClip;
            }
            set
            {
                _nearClip = value;
                Application.InvokeOnMain(Update);
            }
        }

        public bool UseClipping
        {
            get
            {
                return _useClipping;
            }
            set
            {
                _useClipping = value;
                Application.InvokeOnMain(Update);
            }
        }

        public bool FlipVertical
        {
            get
            {
                return _flipVertical;
            }
            set
            {
                _flipVertical = value;
                Application.InvokeOnMain(Update);
            }
        }

        public float Skew
        {
            get
            {
                return _skew;
            }
            set
            {
                _skew = value;
                Application.InvokeOnMain(Update);
            }
        }

        public float FieldOfView
        {
            get
            {
                return _fieldOfView;
            }
            set
            {
                _fieldOfView = value;
                Application.InvokeOnMain(Update);
            }
        }

        public float LevelOfDetailBias
        {
            get
            {
                return _levelOfDetailBias;
            }
            set
            {
                _levelOfDetailBias = value;
                Application.InvokeOnMain(Update);
            }
        }

        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                Application.InvokeOnMain(Update);
            }
        }

        public uint ViewMask
        {
            get
            {
                return _viewMask;
            }
            set
            {
                _viewMask = value;
                Application.InvokeOnMain(Update);
            }
        }

        public bool Orthographic
        {
            get
            {
                return _orthographic;
            }
            set
            {
                _orthographic = value;
                Application.InvokeOnMain(Update);
            }
        }

        public float OrthographicSize
        {
            get
            {
                return _orthographicSize;
            }
            set
            {
                _orthographicSize = value;
                Application.InvokeOnMain(Update);
            }
        }

        public void Update()
        {
            if (Node != null)
            {
                var cameraNode = Node.GetComponent<Camera>();
                cameraNode.AspectRatio = _aspectRatio;
                cameraNode.AutoAspectRatio = _autoAspectRatio;
                cameraNode.FarClip = _farClip;
                cameraNode.FlipVertical = _flipVertical;
                cameraNode.Fov = _fieldOfView;
                cameraNode.LodBias = _levelOfDetailBias;
                cameraNode.NearClip = _nearClip;
                cameraNode.Orthographic = _orthographic;
                cameraNode.OrthoSize = _orthographicSize;
                cameraNode.Skew = _skew;
                cameraNode.UseClipping = _useClipping;
                cameraNode.Zoom = _zoom;

                //if (Node.Scene != null)
                //{
                //    var sceneNode = Node.Scene.Children.ToList().Find(node => node.Name == cameraNode.Node.Name);
                //    if (sceneNode != null)
                //    {
                //        Camera sceneCamera = sceneNode.GetComponent<Camera>();
                //        if (sceneCamera != null)
                //        {
                //            sceneCamera.AspectRatio = _aspectRatio;
                //            sceneCamera.AutoAspectRatio = _autoAspectRatio;
                //            sceneCamera.FarClip = _farClip;
                //            sceneCamera.NearClip = _nearClip;
                //            sceneCamera.UseClipping = _useClipping;
                //            sceneCamera.FlipVertical = _flipVertical;
                //            sceneCamera.Skew = _skew;
                //            sceneCamera.Fov = _fieldOfView;
                //            sceneCamera.LodBias = _levelOfDetailBias;
                //            sceneCamera.Zoom = _zoom;
                //            sceneCamera.Orthographic = _orthographic;
                //            sceneCamera.OrthoSize = _orthographicSize;
                //        }
                //    }
                //}
            }
        }
    }
}
