using Pulsar.Helpers;
using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using System;
using Urho;
using Urho.Extensions.WinForms;

namespace Pulsar
{
    public class RenderScene : IRegisterMessage
    {
        private enum Zoom
        {
            In = 0,
            Out
        }
        private Zoom _zoom;

        private delegate void ThreadSafeFormResize(PulsarMessage message);
        private delegate void ThreadSafeAdjustZoom();
        private delegate void ThreadSafeCameraRotate(PulsarMessage message);
        private delegate void ThreadSafeCameraPan(PulsarMessage message);

        private PulsarApplication _mainApplication;
        public PulsarApplication MainApplication
        {
            get
            {
                return _mainApplication;
            }
            set
            {
                _mainApplication = value;
                RegisterForMessages();
            }
        }

        private UrhoSurface _renderSurface;
        public UrhoSurface RenderSurface 
        { 
            get
            {
                return _renderSurface;
            }
            set
            {
                _renderSurface = value;
            }
        }

        private void RegisterForMessages()
        {
            //TODO: Rewrite to allow registration of more than one message in a single registrant instance
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.MouseWheelZoomIn
            };
            _mainApplication.MessageQueue.RegisterForMessage(registrant);

            registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.MouseWheelZoomOut
            };
            _mainApplication.MessageQueue.RegisterForMessage(registrant);

            registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.MouseButtonLeftDownRotate
            };
            _mainApplication.MessageQueue.RegisterForMessage(registrant);

            registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.MouseButtonRightDownPan
            };
            _mainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        public RenderScene(UrhoSurface renderSurface)
        {
            _renderSurface = renderSurface;
            _renderSurface.Show<PulsarApplication>(new ApplicationOptions(assetsFolder: "E:\\VSProjects\\Windows\\Pulsar\\Assets"));
            MainApplication = (PulsarApplication)_renderSurface.Application;
        }

        private void RenderScene_Resize(object sender, EventArgs e)
        {
            //update the main dragger
            IntVector2 surfaceDimensions = new IntVector2(_renderSurface.Width, _renderSurface.Height);
            if (MainApplication != null)
            {
                Dragger dragger = MainApplication.GetDragger();
                if (dragger != null)
                {
                    dragger.SurfaceDimensions = surfaceDimensions;
                }
            }
        }

        public IntPtr SurfaceHandle
        {
            get
            {
                return _renderSurface.Handle;
            }
        }

        public UrhoSurface DrawingSurface
        {
            get
            {
                return _renderSurface;
            }
        }

        public string RegistrantName()
        {
            return "RenderScene";
        }

        public void CallBack(PulsarMessage message)
        {
            if (message != null)
            {
                switch (message.Type)
                {
                    case PulsarMessage.MessageType.MouseWheelZoomIn:
                        _zoom = Zoom.In;
                        AdjustZoom();
                        break;
                    case PulsarMessage.MessageType.MouseWheelZoomOut:
                        _zoom = Zoom.Out;
                        AdjustZoom();
                        break;
                    case PulsarMessage.MessageType.MouseButtonLeftDownRotate:
                        RotateCamera(message);
                        break;
                    case PulsarMessage.MessageType.MouseButtonRightDownPan:
                        PanCamera(message);
                        break;
                }
            }
        }

        private void PanCamera(PulsarMessage message)
        {
            PulsarCamera camera = null;

            if (_renderSurface.InvokeRequired)
            {
                var delegateCameraRotate = new ThreadSafeCameraPan(PanCamera);
                _renderSurface.Invoke(delegateCameraRotate, new object[] { message });
            }
            else
            {
                string keyPressed = "";
                //check if any constraint key is present
                message.Properties.TryGetValue("constrainKeyDown", out object constrainKeyPressed);
                bool keyDownFound = (constrainKeyPressed != null);
                if(keyDownFound)
                {
                    keyPressed = (string)constrainKeyPressed;
                }
                //get the mouseDelta property from the message
                message.Properties.TryGetValue("mouseDelta", out object mouseDelta);
                if (mouseDelta != null)
                {
                    IntVector2 delta = (IntVector2)mouseDelta;
                    if (delta != null)
                    {
                        camera = _mainApplication.DisplayScene.SceneCamera;
                        if (camera != null)
                        {
                            //only pan horizontally if there is no vertical constraint
                            if (!keyDownFound || keyPressed != "shift")
                            {
                                if (delta.X > 0)
                                {
                                    camera.Node.Translate(Vector3.Right * 3);
                                }
                                else if (delta.X < 0)
                                {
                                    camera.Node.Translate(Vector3.Left * 3);
                                }
                            }
                            //only pan vertically if there is no horizontal constraint
                            if (!keyDownFound || keyPressed != "ctrl")
                            {
                                if (delta.Y > 0)
                                {
                                    camera.Node.Translate(Vector3.Down * 3);
                                }
                                else if (delta.Y < 0)
                                {
                                    camera.Node.Translate(Vector3.Up * 3);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RotateCamera(PulsarMessage message)
        {
            PulsarCamera camera = null;

            if (_renderSurface.InvokeRequired)
            {
                var delegateCameraRotate = new ThreadSafeCameraRotate(RotateCamera);
                _renderSurface.Invoke(delegateCameraRotate, new object[] { message });
            }
            else
            {
                //get the mouseDelta property from the message
                message.Properties.TryGetValue("mouseDelta", out object mouseDelta);
                if(mouseDelta != null)
                {
                    IntVector2 delta = (IntVector2)mouseDelta;
                    if(delta != null)
                    {
                        camera = _mainApplication.DisplayScene.SceneCamera;
                        if(camera != null)
                        {
                            //remove any Z component computed as a result of a previous 'Rotate' call
                            //fixes Z axis rotation to zero thus preventing camera roll
                            var cameraRotation = camera.Node.Rotation.ToEulerAngles();
                            cameraRotation.Z = 0;
                            camera.Node.Rotation = new Quaternion(cameraRotation.X, cameraRotation.Y, cameraRotation.Z);
                            //do the desired rotation
                            camera.Node.Rotate(new Quaternion(delta.Y, delta.X, 0));
                        }
                    }
                }
            }
        }

        private void AdjustZoom()
        {
            PulsarCamera camera = null;


            if (_renderSurface.InvokeRequired)
            {
                var delegateZoom = new ThreadSafeAdjustZoom(AdjustZoom);
                _renderSurface.Invoke(delegateZoom, Array.Empty<object>());
            }
            else
            {
                //get the main camera
                if (_mainApplication != null)
                {
                    if (_mainApplication.DisplayScene != null)
                    {
                        camera = _mainApplication.DisplayScene.SceneCamera;
                        if (camera == null) return;
                    }
                }

                switch (_zoom)
                {
                    case Zoom.In:
                        camera.Camera.Node.Translate(Vector3.Forward * 3, TransformSpace.Local); 
                        break;
                    case Zoom.Out:
                        camera.Camera.Node.Translate(Vector3.Forward * -3, TransformSpace.Local);
                        break;
                }
            }
        }

        public object Registrant()
        {
            return this;
        }
    }
}
