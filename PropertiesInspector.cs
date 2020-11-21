using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using Pulsar.ObjectModel.PropertiesModel;
using System;
using System.Diagnostics;
using Urho;
using WeifenLuo.WinFormsUI.Docking;

namespace Pulsar
{
    public partial class PropertiesInspector : DockContent, IRegisterMessage
    {
        public delegate void NodeNameChangedEventHandler(string oldName, string newName);
        public event NodeNameChangedEventHandler SceneNodeNameChanged;

        private delegate void ThreadSafeFormResize(PulsarMessage message);

        private ObjectModel.PropertiesModel.NodeProperties _currentPropertyNode;
        private PulsarApplication _mainApplication;
        private PulsarScene _scene;

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

        private delegate void ThreadSafePropertyGrid(object node, bool externallySet);
        private delegate void ThreadSafeResetPropertiesGrid();
        private delegate void ThreadSafeShowObjectProperties(SceneObjectType objectType, object node, bool externallySet);
        private delegate void ThreadSafeNodeTranslationChange(Vector3 position, bool externallySet);
        private delegate void ThreadSafeNodeRotationChange(Quaternion rotation, bool externallySet);
        private delegate void ThreadSafeNodeScaleChange(Vector3 scale, bool externallySet);

        public PulsarScene Scene
        {
            get
            {
                return _scene;
            }
            set
            {
                _scene = value;
            }
        }

        public PropertiesInspector()
        {
            InitializeComponent();
            _currentPropertyNode = new ObjectModel.PropertiesModel.NodeProperties();

            _currentPropertyNode.SceneNodeNameChanged += CurrentPropertyNode_SceneNodeNameChanged;

            propertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;

        }

        private void PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            _currentPropertyNode.ExternallySet = false;
        }

        private void RegisterForMessages()
        {
            if (MainApplication != null)
            {
                SubscribeToResetPropertiesWindow();
                SubscribeToShowObjectProperties();
                SubscribeToNodeTranslationChange();
                SubscribeToNodeRotationChange();
                SubscribeToNodeScaleChange();
                SubscribeToFormResize();
            }
        }

        private void SubscribeToFormResize()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.AdjustPropertiesFormWidth
            };
            MainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToNodeScaleChange()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.NodeScaleChange
            };
            MainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToNodeRotationChange()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.NodeRotationChange
            };
            MainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToNodeTranslationChange()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.NodeTranslationChange
            };
            MainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToShowObjectProperties()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.ShowObjectProperties
            };
            MainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToResetPropertiesWindow()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.ResetPropertiesWindow
            };
            MainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void CurrentPropertyNode_SceneNodeNameChanged(string oldName, string newName)
        {
            CameraProperties camProp = null;
            if (_currentPropertyNode.Node.TypeName == "CameraProperties")
            {
                camProp = (CameraProperties)_currentPropertyNode;
            }

            if (camProp == null)
            {
                SceneNodeNameChanged?.Invoke(oldName, newName);
            }
            else
            {
                if (!camProp.IsMainCamera)
                {
                    SceneNodeNameChanged?.Invoke(oldName, newName);
                }
                else
                {
                    _currentPropertyNode.Name = oldName;
                    propertyGrid.Refresh();
                }
            }
        }

        public void SetProperties(SceneObjectType sceneObjectType, object sceneObject, bool externallySet)
        {
            switch (sceneObjectType)
            {
                case SceneObjectType.Light:
                    CreateLightProperties(sceneObject, externallySet);
                    break;
                case SceneObjectType.Camera:
                    CreateCameraProperties(sceneObject, externallySet);
                    break;
                case SceneObjectType.Plane:
                    CreatePlaneProperties(sceneObject, externallySet);
                    break;
                case SceneObjectType.Node:
                    CreateNodeProperties(sceneObject, externallySet);
                    break;
            }
        }

        private void CreateLightProperties(object light, bool externallySet)
        {
            if (propertyGrid.InvokeRequired)
            {
                var d = new ThreadSafePropertyGrid(CreateLightProperties);
                propertyGrid.Invoke(d, new object[] { light });
            }
            else
            {
                LightProperties lightProperties = new LightProperties();
                var lightNode = ((Node)light).GetComponent<Light>();

                if (light != null)
                {
                    lightProperties.Node = lightNode;
                    lightProperties.Name = lightNode.Node.Name;
                    lightProperties.Enabled = lightNode.Enabled;
                    lightProperties.Position = lightNode.Node.Position;
                    lightProperties.Rotation = lightNode.Node.Rotation;
                    lightProperties.Scale = lightNode.Node.Scale;

                    lightProperties.PulsarApplication = _mainApplication;
                    lightProperties.Scene = _scene;

                    lightProperties.AspectRatio = lightNode.AspectRatio;
                    lightProperties.Brightness = lightNode.Brightness;

                    lightProperties.Colour = System.Drawing.Color.FromArgb((int)lightNode.Color.A, (int)lightNode.Color.R, (int)lightNode.Color.G, (int)lightNode.Color.B);

                    lightProperties.ColourFromTemperature = System.Drawing.Color.FromArgb((int)lightNode.ColorFromTemperature.A, (int)lightNode.ColorFromTemperature.R, (int)lightNode.ColorFromTemperature.G, (int)lightNode.ColorFromTemperature.B);

                    lightProperties.EffectiveColour = System.Drawing.Color.FromArgb((int)lightNode.EffectiveColor.A, (int)lightNode.EffectiveColor.R, (int)lightNode.EffectiveColor.G, (int)lightNode.EffectiveColor.B);

                    lightProperties.Temperature = lightNode.Temperature;
                    lightProperties.UsePhysicalValues = lightNode.UsePhysicalValues;
                    lightProperties.EffectiveSpecularIntensity = lightNode.EffectiveSpecularIntensity;
                    lightProperties.SpecularIntensity = lightNode.SpecularIntensity;
                    lightProperties.FadeDistance = lightNode.FadeDistance;
                    lightProperties.FieldOfView = lightNode.Fov;
                    lightProperties.Length = lightNode.Length;
                    lightProperties.LightType = lightNode.LightType;
                    lightProperties.PerVertex = lightNode.PerVertex;
                    lightProperties.Radius = lightNode.Radius;
                    lightProperties.Range = lightNode.Range;
                    lightProperties.ShadowFadeDistance = lightNode.ShadowFadeDistance;
                    lightProperties.ShadowIntensity = lightNode.ShadowIntensity;
                    lightProperties.ShadowMaximumExtrusion = lightNode.ShadowMaxExtrusion;
                    lightProperties.ShadowNearFarRatio = lightNode.ShadowNearFarRatio;
                    lightProperties.ShadowResolution = lightNode.ShadowResolution;

                    propertyGrid.SelectedObject = lightProperties;
                    _currentPropertyNode = lightProperties;
                }
            }
        }

        private void CreateCameraProperties(object camera, bool externallySet)
        {
            if (propertyGrid.InvokeRequired)
            {
                var d = new ThreadSafePropertyGrid(CreateCameraProperties);
                propertyGrid.Invoke(d, new object[] { camera });
            }
            else
            {

                if (camera != null)
                {
                    var cameraNode = _scene.SceneCamera;
                    CameraProperties cameraProperties;
                    cameraProperties = new CameraProperties(((Camera)camera).Node.Name)
                    {
                        IsMainCamera = cameraNode.IsMainCamera,

                        PulsarApplication = _mainApplication,
                        Scene = _scene,
                        Node = cameraNode.Camera,
                        Name = cameraNode.Node.Name,
                        Enabled = cameraNode.Enabled,
                        Position = cameraNode.Node.Position,
                        Rotation = cameraNode.Node.Rotation,
                        Scale = cameraNode.Node.Scale,

                        AspectRatio = cameraNode.Camera.AspectRatio,
                        AutoAspectRatio = cameraNode.Camera.AutoAspectRatio,
                        FarClip = cameraNode.Camera.FarClip,
                        NearClip = cameraNode.Camera.NearClip,
                        UseClipping = cameraNode.Camera.UseClipping,
                        FlipVertical = cameraNode.Camera.FlipVertical,
                        Skew = cameraNode.Camera.Skew,
                        FieldOfView = cameraNode.Camera.Fov,
                        LevelOfDetailBias = cameraNode.Camera.LodBias,
                        Zoom = cameraNode.Camera.Zoom,
                        ViewMask = cameraNode.Camera.ViewMask,
                        Orthographic = cameraNode.Camera.Orthographic,
                        OrthographicSize = cameraNode.Camera.OrthoSize
                    };

                    propertyGrid.SelectedObject = cameraProperties;
                    _currentPropertyNode = cameraProperties;
                }
            }
        }

        private void CreatePlaneProperties(object plane, bool externallySet)
        {
            if (propertyGrid.InvokeRequired)
            {
                var d = new ThreadSafePropertyGrid(CreatePlaneProperties);
                propertyGrid.Invoke(d, new object[] { plane });
            }
            else
            {
                if (plane != null)
                {
                    PlaneProperties planeProperties = new PlaneProperties();

                    var planeNode = (StaticModel)plane;
                    planeProperties.PulsarApplication = _mainApplication;
                    planeProperties.Scene = _scene;
                    planeProperties.Node = planeNode.Node;
                    planeProperties.Name = planeNode.Node.Name;
                    planeProperties.Enabled = planeNode.Enabled;
                    planeProperties.Position = planeNode.Node.Position;
                    planeProperties.Rotation = planeNode.Node.Rotation;
                    planeProperties.Scale = planeNode.Node.Scale;

                    planeProperties.AnimationEnabled = planeNode.AnimationEnabled;
                    planeProperties.CastShadows = planeNode.CastShadows;
                    planeProperties.DrawDistance = planeNode.DrawDistance;
                    planeProperties.LightMask = planeNode.LightMask;
                    planeProperties.LodBias = planeNode.LodBias;
                    planeProperties.MaxLights = planeNode.MaxLights;
                    planeProperties.Occludee = planeNode.Occludee;
                    planeProperties.Occluder = planeNode.Occluder;
                    planeProperties.ShadowDistance = planeNode.ShadowDistance;
                    planeProperties.ShadowMask = planeNode.ShadowMask;
                    planeProperties.SortValue = planeNode.SortValue;
                    planeProperties.ViewMask = planeNode.ViewMask;
                    planeProperties.ZoneMask = planeNode.ZoneMask;
                    planeProperties.BlockEvents = planeNode.BlockEvents;

                    propertyGrid.SelectedObject = planeProperties;
                    _currentPropertyNode = planeProperties;
                }
            }
        }

        public void CreateNodeProperties(object node, bool externallySet)
        {
            if (propertyGrid.InvokeRequired)
            {
                var delegateNodeProperties = new ThreadSafePropertyGrid(CreateNodeProperties);
                if (delegateNodeProperties != null)
                {
                    propertyGrid.Invoke(delegateNodeProperties, new object[] { node, externallySet });
                }
            }
            else
            {
                if (node != null)
                {
                    //Debug.Print("PropertiesInspector.CreateNodeProperties - Found Node '" + ((Node)node).Name + "'");

                    //if externallySet is true, we currently have an existing nodeProperties object in _currentNodeObject
                    ObjectModel.PropertiesModel.NodeProperties nodeProperties;
                    if (!externallySet)
                    {
                        nodeProperties = new ObjectModel.PropertiesModel.NodeProperties
                        {
                            InCreation = true
                        };
                    }
                    else
                    {
                        nodeProperties = _currentPropertyNode;
                    }

                    nodeProperties.ExternallySet = externallySet;

                    var sceneNode = (Node)node;

                    nodeProperties.PulsarApplication = _mainApplication;
                    nodeProperties.Scene = _scene;
                    nodeProperties.Node = sceneNode;
                    nodeProperties.Name = sceneNode.Name;
                    nodeProperties.Position = sceneNode.Position;
                    nodeProperties.Rotation = sceneNode.Rotation;
                    nodeProperties.Scale = sceneNode.Scale;
                    nodeProperties.Enabled = sceneNode.Enabled;
                    nodeProperties.InCreation = false;
                    propertyGrid.SelectedObject = nodeProperties;
                    _currentPropertyNode = nodeProperties;
                }
            }
        }

        public string RegistrantName()
        {
            return "PropertiesInspector";
        }

        public void CallBack(PulsarMessage message)
        {
            if (message != null)
            {
                message.Properties.TryGetValue("sceneObject", out object sceneObject);
                message.Properties.TryGetValue("externallySet", out object externallySet);

                switch (message.Type)
                {
                    case PulsarMessage.MessageType.ResetPropertiesWindow:
                        var delegateReset = new ThreadSafeResetPropertiesGrid(ResetPropertiesWindow);
                        propertyGrid.Invoke(delegateReset, Array.Empty<object>());
                        break;
                    case PulsarMessage.MessageType.ShowObjectProperties:
                        var delegateShowObjectProperties = new ThreadSafeShowObjectProperties(SetProperties);
                        if (sceneObject != null)
                        {
                            message.Properties.TryGetValue("sceneObjectType", out object sceneObjectType);
                            if (sceneObjectType != null && externallySet != null)
                            {
                                propertyGrid.Invoke(delegateShowObjectProperties, new object[] { (SceneObjectType)sceneObjectType, sceneObject, (bool)externallySet });
                            }
                        }
                        break;
                    case PulsarMessage.MessageType.NodeTranslationChange:
                        var delegateTranslationChange = new ThreadSafeNodeTranslationChange(TranslationChange);
                        if (sceneObject != null && externallySet != null)
                        {
                            propertyGrid.Invoke(delegateTranslationChange, new object[] { ((Node)sceneObject).Position, (bool)externallySet });
                        }
                        break;
                    case PulsarMessage.MessageType.NodeRotationChange:
                        var delegateRotationChange = new ThreadSafeNodeRotationChange(RotationChange);
                        if (sceneObject != null && externallySet != null)
                        {
                            propertyGrid.Invoke(delegateRotationChange, new object[] { ((Node)sceneObject).Rotation, (bool)externallySet });
                        }
                        break;
                    case PulsarMessage.MessageType.NodeScaleChange:
                        var delegateScaleChange = new ThreadSafeNodeScaleChange(ScaleChange);
                        if (sceneObject != null && externallySet != null)
                        {
                            propertyGrid.Invoke(delegateScaleChange, new object[] { ((Node)sceneObject).Scale, (bool)externallySet });
                        }
                        break;
                    case PulsarMessage.MessageType.AdjustPropertiesFormWidth:
                        AdjustFormWidth(message);
                        break;
                }
            }
        }

        private void AdjustFormWidth(PulsarMessage message)
        {
            if (InvokeRequired)
            {
                var delegateFormResize = new ThreadSafeFormResize(AdjustFormWidth);
                Invoke(delegateFormResize, new object[] { message });
            }
            else
            {
                if (message.Properties.Count > 0)
                {
                    message.Properties.TryGetValue("width", out object widthProperty);
                    if (widthProperty != null)
                    {
                        Width = (int)widthProperty;
                    }
                }
            }
        }

        private void ScaleChange(Vector3 scale, bool externallySet)
        {
            _currentPropertyNode.ExternallySet = externallySet;
            _currentPropertyNode.Scale = scale;
            propertyGrid.SelectedObject = _currentPropertyNode;
        }

        private void RotationChange(Quaternion rotation, bool externallySet)
        {
            _currentPropertyNode.ExternallySet = externallySet;
            _currentPropertyNode.Rotation = rotation;
            propertyGrid.SelectedObject = _currentPropertyNode;
        }

        private void TranslationChange(Vector3 position, bool externallySet)
        {
            _currentPropertyNode.ExternallySet = true;
            _currentPropertyNode.Position = position;
            propertyGrid.SelectedObject = _currentPropertyNode;
        }

        public void ResetPropertiesWindow()
        {
            propertyGrid.SelectedObject = null;
        }

        public object Registrant()
        {
            return this;
        }
    }
}
