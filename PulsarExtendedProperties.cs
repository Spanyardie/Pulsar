using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using InternalNodeProperties = Pulsar.ObjectModel.PropertiesModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Urho;
using Pulsar.ObjectModel.PropertiesModel;
using Pulsar.ObjectModel.Primitives;
using ExtendedModelProperties;

namespace Pulsar
{
    public class PulsarExtendedProperties : IRegisterMessage
    {
        //component properties list
        private readonly List<PulsarComponent> _componentList;

        private delegate void ThreadSafeShowObjectProperties(SceneObjectType objectType, object node, bool externallySet);
        private delegate void ThreadSafeResetComponentView();
        private delegate void ThreadSafeNodeTranslationChange(string nodeName, Node sceneNode, bool externallySet);
        private delegate void ThreadSafeNodeRotationChange(string nodeName, Node sceneNode, bool externallySet);
        private delegate void ThreadSafeNodeScaleChange(string nodeName, Node sceneNode, bool externallySet);

        private enum HookType
        {
            Hook = 0,
            UnHook
        }

        private const int DEFAULT_WIDTH = 332;
        private const int DEFAULT_TOP_POSITION = 12;

        public PulsarExtendedProperties(Panel propertiesPanel)
        {
            PropertiesPanel = propertiesPanel;
            _componentList = new List<PulsarComponent>();
        }

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

        public PulsarScene Scene { get; set; }
        public Panel PropertiesPanel { get; set; }

        private void RegisterForMessages()
        {
            if(_mainApplication != null)
            {
                SubscribeToShowObjectProperties();
                SubscribeToResetPropertiesWindow();
                SubscribeToTranslationChange();
                SubscribeToRotationChange();
                SubscribeToScaleChange();
            }
        }

        private void SubscribeToScaleChange()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.NodeScaleChange
            };

            _mainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToRotationChange()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.NodeRotationChange
            };

            _mainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToTranslationChange()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.NodeTranslationChange
            };

            _mainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToResetPropertiesWindow()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.ResetPropertiesWindow
            };

            _mainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        private void SubscribeToShowObjectProperties()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.ShowObjectProperties
            };

            _mainApplication.MessageQueue.RegisterForMessage(registrant);
        }

        public void AddComponentToPropertiesList(PulsarComponent nodeProperties)
        {
            if(nodeProperties != null)
            {
                _componentList.Add(nodeProperties);
                RebuildPropertiesDisplay();
            }
        }

        private void RebuildPropertiesDisplay()
        {
            //all properties controls must default to a minimum width of 332, with an X location of 11

            const int X_Position = 11;
            int currentYPosition = DEFAULT_TOP_POSITION;

            SetupToolBarComponent(X_Position, DEFAULT_WIDTH, ref currentYPosition);

            foreach (PulsarComponent control in _componentList)
            {
                if (!((ISystem)control).IsSytem)
                {
                    var controlName = control.GetType().Name;
                    switch (controlName)
                    {
                        case "NodeProperties":
                            SetupNodePropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                            break;
                        case "ModelProperties":
                            SetupModelPropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                            break;
                        case "LightProperties":
                            SetupLightPropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                            break;
                        case "CameraProperties":
                            SetupCameraPropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                            break;
                    }
                }
            }
        }

        private void SetupLightPropertiesComponent(BaseEntity baseEntity, int xPosition, int defaultWidth, ref int currentYPosition)
        {
            if(baseEntity != null)
            {
                if(!baseEntity.Node.IsDeleted)
                {
                    var existingNode = PropertiesPanel.Controls.Find("light_" + baseEntity.Node.Name, true);
                    if(existingNode.Count() != 0)
                    {
                        currentYPosition += existingNode[0].Height + 1;
                        return;
                    }

                    LightProperties light = (LightProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.LightProperties);

                    ExtendedLightProperties.LightProperties lightProperties = new ExtendedLightProperties.LightProperties
                    {
                        Name = "light_" + baseEntity.Node.Name,
                        NodeName = baseEntity.Node.Name,
                        AspectRatio = light.AspectRatio,
                        Brightness = light.Brightness,
                        Colour = light.Colour,
                        Temperature = light.Temperature,
                        UsePhysicalValues = light.UsePhysicalValues,
                        EffectiveSpecularIntensity = light.EffectiveSpecularIntensity,
                        SpecularIntensity = light.SpecularIntensity,
                        FadeDistance = light.FadeDistance,
                        FieldOfView = light.FieldOfView,
                        Length = light.Length,
                        Type = light.LightType,
                        PerVertex = light.PerVertex,
                        Radius = light.Radius,
                        Range = light.Range,
                        ShadowFadeDistance = light.ShadowFadeDistance,
                        ShadowIntensity = light.ShadowIntensity,
                        ShadowMaximumExtrusion = light.ShadowMaximumExtrusion,
                        ShadowNearFarRatio = light.ShadowNearFarRatio,
                        ShadowResolution = light.ShadowResolution,

                        Left = xPosition,
                        Top = currentYPosition,
                        Width = (defaultWidth > (PropertiesPanel.Width - 20)) ? defaultWidth : PropertiesPanel.Width - 20,
                        Tag = "lightProperties"
                    };
                    Size size = lightProperties.Size;
                    size.Height = lightProperties.MaximumHeight;
                    lightProperties.MaximumSize = size;
                    light.Container = lightProperties;

                    currentYPosition += lightProperties.Height + 1;

                    PropertiesPanel.Controls.Add(lightProperties);

                    HookLightProperties(lightProperties, HookType.Hook);

                    lightProperties.Show();
                }
            }
        }

        private void HookLightProperties(ExtendedLightProperties.LightProperties lightProperties, HookType hook)
        {
            switch(hook)
            {
                case HookType.Hook:
                    lightProperties.AspectRatioChanged += LightProperties_AspectRatioChanged;
                    lightProperties.BrightnessChanged += LightProperties_BrightnessChanged;
                    lightProperties.ColourChanged += LightProperties_ColourChanged;
                    lightProperties.EffectiveSpecularIntensityChanged += LightProperties_EffectiveSpecularIntensityChanged;
                    lightProperties.FadeDistanceChanged += LightProperties_FadeDistanceChanged;
                    lightProperties.UsePhysicalValuesChanged += LightProperties_UsePhysicalValuesChanged;
                    lightProperties.FieldOfViewChanged += LightProperties_FieldOfViewChanged;
                    lightProperties.LengthChanged += LightProperties_LengthChanged;
                    lightProperties.LightTypeChanged += LightProperties_LightTypeChanged;
                    lightProperties.PerVertexChanged += LightProperties_PerVertexChanged;
                    lightProperties.RadiusChanged += LightProperties_RadiusChanged;
                    lightProperties.RangeChanged += LightProperties_RangeChanged;
                    lightProperties.TemperatureChanged += LightProperties_TemperatureChanged;
                    lightProperties.ShadowFadeDistanceChanged += LightProperties_ShadowFadeDistanceChanged;
                    lightProperties.ShadowIntensityChanged += LightProperties_ShadowIntensityChanged;
                    lightProperties.ShadowMaximumExtrusionChanged += LightProperties_ShadowMaximumExtrusionChanged;
                    lightProperties.ShadowNearFarRatioChanged += LightProperties_ShadowNearFarRatioChanged;
                    lightProperties.ShadowResolutionChanged += LightProperties_ShadowResolutionChanged;
                    lightProperties.WindowRolled += LightProperties_WindowRolled;
                    break;
                case HookType.UnHook:
                    lightProperties.AspectRatioChanged -= LightProperties_AspectRatioChanged;
                    lightProperties.BrightnessChanged -= LightProperties_BrightnessChanged;
                    lightProperties.ColourChanged -= LightProperties_ColourChanged;
                    lightProperties.EffectiveSpecularIntensityChanged -= LightProperties_EffectiveSpecularIntensityChanged;
                    lightProperties.FadeDistanceChanged -= LightProperties_FadeDistanceChanged;
                    lightProperties.FieldOfViewChanged -= LightProperties_FieldOfViewChanged;
                    lightProperties.LightTypeChanged -= LightProperties_LightTypeChanged;
                    lightProperties.PerVertexChanged -= LightProperties_PerVertexChanged;
                    lightProperties.RadiusChanged -= LightProperties_RadiusChanged;
                    lightProperties.RangeChanged -= LightProperties_RangeChanged;
                    lightProperties.TemperatureChanged -= LightProperties_TemperatureChanged;
                    lightProperties.ShadowFadeDistanceChanged -= LightProperties_ShadowFadeDistanceChanged;
                    lightProperties.ShadowIntensityChanged -= LightProperties_ShadowIntensityChanged;
                    lightProperties.ShadowMaximumExtrusionChanged -= LightProperties_ShadowMaximumExtrusionChanged;
                    lightProperties.ShadowNearFarRatioChanged -= LightProperties_ShadowNearFarRatioChanged;
                    lightProperties.ShadowResolutionChanged -= LightProperties_ShadowResolutionChanged;
                    lightProperties.WindowRolled -= LightProperties_WindowRolled;
                    break;
            }
        }

        private void LightProperties_TemperatureChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.Temperature = eventArgs.Temperature;
                }
            }
        }

        private void LightProperties_UsePhysicalValuesChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.UsePhysicalValues = eventArgs.UsePhysicalValues;
                }
            }
        }

        private void LightProperties_WindowRolled(object sender, EventArgs e)
        {
            RearrangePropertiesView((ExtendedLightProperties.LightProperties)sender, e);
        }

        private void LightProperties_ShadowResolutionChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.ShadowResolution = eventArgs.ShadowResolution;
                }
            }
        }

        private void LightProperties_ShadowNearFarRatioChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.ShadowNearFarRatio = eventArgs.ShadowNearFarRatio;
                }
            }
        }

        private void LightProperties_ShadowMaximumExtrusionChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.ShadowMaximumExtrusion = eventArgs.ShadowMaximumExtrusion;
                }
            }
        }

        private void LightProperties_ShadowIntensityChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.ShadowIntensity = eventArgs.ShadowIntensity;
                }
            }
        }

        private void LightProperties_ShadowFadeDistanceChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.ShadowFadeDistance = eventArgs.ShadowFadeDistance;
                }
            }
        }

        private void LightProperties_RangeChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.Range = eventArgs.Range;
                }
            }
        }

        private void LightProperties_RadiusChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.Radius = eventArgs.Radius;
                }
            }
        }

        private void LightProperties_PerVertexChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.PerVertex = eventArgs.PerVertex;
                }
            }
        }

        private void LightProperties_LightTypeChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.LightType = eventArgs.LightType;
                }
            }
        }

        private void LightProperties_LengthChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.Length = eventArgs.Length;
                }
            }
        }

        private void LightProperties_FieldOfViewChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.FieldOfView = eventArgs.FieldOfView;
                }
            }
        }

        private void LightProperties_FadeDistanceChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.FadeDistance = eventArgs.FadeDistance;
                }
            }
        }

        private void LightProperties_EffectiveSpecularIntensityChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.EffectiveSpecularIntensity = eventArgs.EffectiveSpecularIntensity;
                }
            }
        }

        private void LightProperties_ColourChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.Colour = eventArgs.Colour;
                }
            }
        }

        private void LightProperties_BrightnessChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.Brightness = eventArgs.Brightness;
                }
            }
        }

        private void LightProperties_AspectRatioChanged(object sender, EventArgs e)
        {
            ExtendedLightProperties.LightPropertiesChangedEventArgs eventArgs = (ExtendedLightProperties.LightPropertiesChangedEventArgs)e;

            ExtendedLightProperties.LightProperties userControl = (ExtendedLightProperties.LightProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                LightProperties lightProperties = (LightProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (lightProperties != null)
                {
                    lightProperties.AspectRatio = eventArgs.AspectRatio;
                }
            }
        }

        private void SetupModelPropertiesComponent(BaseEntity baseEntity, int xPosition, int defaultWidth, ref int currentYPosition)
        {
            //does this control already exist in the form controls list?
            if (baseEntity != null)
            {
                if (!baseEntity.Node.IsDeleted)
                {
                    var existingNode = PropertiesPanel.Controls.Find("model_" + baseEntity.Node.Name, true);
                    if (existingNode.Count<Control>() != 0)
                    {
                        currentYPosition += existingNode[0].Height + 1;
                        return;
                    }

                    InternalNodeProperties.ModelProperties mod = (InternalNodeProperties.ModelProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.ModelProperties);

                    ExtendedModelProperties.ModelProperties modelProperties = new ExtendedModelProperties.ModelProperties
                    {
                        Name = "model_" + mod.Node.Name,
                        ModelNodeName = mod.Node.Name,
                        ModelFilePath = mod.ModelName,
                        MaterialFilePath = mod.MaterialName,
                        Enabled = baseEntity.Enabled,
                        AssetsFolder = mod.AssetsFolder,
                        Left = xPosition,
                        Top = currentYPosition,
                        Width = (defaultWidth > (PropertiesPanel.Width - 20)) ? defaultWidth : PropertiesPanel.Width - 20,
                        Tag = "modelProperties",
                        AllowDrop = true
                    };

                    Size size = modelProperties.Size;
                    size.Height = modelProperties.MaximumHeight;
                    modelProperties.MaximumSize = size;
                    mod.Container = modelProperties;

                    currentYPosition += modelProperties.Height + 1;

                    PropertiesPanel.Controls.Add(modelProperties);

                    HookModelProperties(modelProperties, HookType.Hook);
                    modelProperties.Show();
                }
            }
        }

        private void ModelProperties_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            e.Action = DragAction.Drop;
        }

        private void ModelProperties_RemoveModel(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ModelProperties_MaterialChanged(object sender, EventArgs e)
        {
            MaterialChangedEventArgs materialChangedEventArgs = (MaterialChangedEventArgs)e;

            //grab the selected object and direct our message to this entity specifically
            if (_mainApplication != null)
            {
                if (_mainApplication.DisplayScene != null)
                {
                    if (_mainApplication.DisplayScene.SelectedSceneObjects != null)
                    {
                        if (_mainApplication.DisplayScene.SelectedSceneObjects.ObjectList.Count > 0)
                        {
                            var selectedObject = _mainApplication.DisplayScene.SelectedSceneObjects.ObjectList[0];
                            if (selectedObject != null)
                            {
                                PulsarMessage message = new PulsarMessage
                                {
                                    Destination = selectedObject.SelectedNode.Name,
                                    Type = PulsarMessage.MessageType.MaterialChanged,
                                    Iterations = 1
                                };
                                string newMaterialName = "Materials/" + materialChangedEventArgs.MaterialFilePath;

                                message.Properties.Add("newMaterialName", newMaterialName);

                                _mainApplication.MessageQueue.PushMessage(message);
                            }
                        }
                    }
                }
            }
        }

        private void ModelProperties_ModelChanged(object sender, EventArgs e)
        {
            ModelChangedEventArgs modelChangedEventArgs = (ModelChangedEventArgs)e;

            //grab the selected object and direct our message to this entity specifically
            if (_mainApplication != null)
            {
                if (_mainApplication.DisplayScene != null)
                {
                    if (_mainApplication.DisplayScene.SelectedSceneObjects != null)
                    {
                        if (_mainApplication.DisplayScene.SelectedSceneObjects.ObjectList.Count > 0)
                        {
                            var selectedObject = _mainApplication.DisplayScene.SelectedSceneObjects.ObjectList[0];
                            if (selectedObject != null)
                            {
                                PulsarMessage message = new PulsarMessage
                                {
                                    Destination = selectedObject.SelectedNode.Name,
                                    Type = PulsarMessage.MessageType.ModelChanged,
                                    Iterations = 1
                                };
                                string newModelName = "Models/" + modelChangedEventArgs.ModelFilePath;

                                message.Properties.Add("newModelName", newModelName);
                                _mainApplication.MessageQueue.PushMessage(message);
                            }
                        }
                    }
                }
            }
        }

        private void SetupCameraPropertiesComponent(BaseEntity baseEntity, int xPosition, int defaultWidth, ref int currentYPosition)
        {
            if(baseEntity != null)
            {
                if(!baseEntity.Node.IsDeleted)
                {
                    var controlName = "camera_" + baseEntity.Node.Name;
                    var existingNode = PropertiesPanel.Controls.Find(controlName, true);
                    if(existingNode.Count() != 0)
                    {
                        currentYPosition += existingNode[0].Height + 1;
                        return;
                    }
                    CameraProperties camera = (CameraProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.CameraProperties);

                    ExtendedCameraProperties.CameraProperties properties = new ExtendedCameraProperties.CameraProperties
                    {
                        Name = "camera_" + baseEntity.Node.Name,
                        NodeName = baseEntity.Node.Name,
                        AspectRatio = camera.AspectRatio,
                        AutoAspectRatio = camera.AutoAspectRatio,
                        FarClip = camera.FarClip,
                        NearClip = camera.NearClip,
                        UseClipping = camera.UseClipping,
                        FlipVerical = camera.FlipVertical,
                        Skew = camera.Skew,
                        Orthographic = camera.Orthographic,
                        OrthographicSize = camera.OrthographicSize,
                        FieldOfView = camera.FieldOfView,
                        LODBias = camera.LevelOfDetailBias,
                        Zoom = camera.Zoom,

                        Left = xPosition,
                        Top = currentYPosition,
                        Width = (defaultWidth > (PropertiesPanel.Width - 20)) ? defaultWidth : PropertiesPanel.Width - 20,
                        Tag = "cameraProperties"
                    };

                    camera.Container = properties;

                    currentYPosition += properties.Height + 1;

                    PropertiesPanel.Controls.Add(properties);

                    HookCameraProperties(properties, HookType.Hook);

                    properties.Show();
                }
            }
        }

        private void HookCameraProperties(ExtendedCameraProperties.CameraProperties cameraProperties, HookType hook)
        {
            switch(hook)
            {
                case HookType.Hook:
                    cameraProperties.AspectRatioChanged += CameraProperties_AspectRatioChanged;
                    cameraProperties.AutoAspectRatioChanged += CameraProperties_AutoAspectRatioChanged;
                    cameraProperties.FarClipChanged += CameraProperties_FarClipChanged;
                    cameraProperties.NearClipChanged += CameraProperties_NearClipChanged;
                    cameraProperties.UseClippingChanged += CameraProperties_UseClippingChanged;
                    cameraProperties.FlipVerticalChanged += CameraProperties_FlipVerticalChanged;
                    cameraProperties.SkewChanged += CameraProperties_SkewChanged;
                    cameraProperties.OrthographicChanged += CameraProperties_OrthographicChanged;
                    cameraProperties.OrthographicSizeChanged += CameraProperties_OrthographicSizeChanged;
                    cameraProperties.FieldOfViewChanged += CameraProperties_FieldOfViewChanged;
                    cameraProperties.LODBiasChanged += CameraProperties_LODBiasChanged;
                    cameraProperties.ZoomChanged += CameraProperties_ZoomChanged;
                    cameraProperties.WindowRolled += CameraProperties_WindowRolled;
                    break;
                case HookType.UnHook:
                    cameraProperties.AspectRatioChanged -= CameraProperties_AspectRatioChanged;
                    cameraProperties.AutoAspectRatioChanged -= CameraProperties_AutoAspectRatioChanged;
                    cameraProperties.FarClipChanged -= CameraProperties_FarClipChanged;
                    cameraProperties.NearClipChanged -= CameraProperties_NearClipChanged;
                    cameraProperties.UseClippingChanged -= CameraProperties_UseClippingChanged;
                    cameraProperties.FlipVerticalChanged -= CameraProperties_FlipVerticalChanged;
                    cameraProperties.SkewChanged -= CameraProperties_SkewChanged;
                    cameraProperties.OrthographicChanged -= CameraProperties_OrthographicChanged;
                    cameraProperties.OrthographicSizeChanged -= CameraProperties_OrthographicSizeChanged;
                    cameraProperties.FieldOfViewChanged -= CameraProperties_FieldOfViewChanged;
                    cameraProperties.LODBiasChanged -= CameraProperties_LODBiasChanged;
                    cameraProperties.ZoomChanged -= CameraProperties_ZoomChanged;
                    cameraProperties.WindowRolled -= CameraProperties_WindowRolled;
                    break;
            }
        }

        private void CameraProperties_WindowRolled(object sender, EventArgs e)
        {
            ExtendedCameraProperties.WindowRollEventArgs eventArgs = (ExtendedCameraProperties.WindowRollEventArgs)e;
            RearrangePropertiesView((ExtendedCameraProperties.CameraProperties)sender, eventArgs);
        }

        private void CameraProperties_ZoomChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if(userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if(cameraProperties != null)
                {
                    cameraProperties.Zoom = eventArgs.Zoom;
                }
            }
        }

        private void CameraProperties_LODBiasChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.LevelOfDetailBias = eventArgs.LODBias;
                }
            }
        }

        private void CameraProperties_FieldOfViewChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.FieldOfView = eventArgs.FieldOfView;
                }
            }
        }

        private void CameraProperties_OrthographicSizeChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.OrthographicSize = eventArgs.OrthographicSize;
                }
            }
        }

        private void CameraProperties_OrthographicChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.Orthographic = eventArgs.Orthographic;
                }
            }
        }

        private void CameraProperties_SkewChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.Skew = eventArgs.Skew;
                }
            }
        }

        private void CameraProperties_FlipVerticalChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.FlipVertical = eventArgs.FlipVertical;
                }
            }
        }

        private void CameraProperties_UseClippingChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.UseClipping = eventArgs.UseClipping;
                }
            }
        }

        private void CameraProperties_NearClipChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.NearClip = eventArgs.NearClip;
                }
            }
        }

        private void CameraProperties_FarClipChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.FarClip = eventArgs.FarClip;
                }
            }
        }

        private void CameraProperties_AutoAspectRatioChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.AutoAspectRatio = eventArgs.AutoAspectRatio;
                }
            }
        }

        private void CameraProperties_AspectRatioChanged(object sender, EventArgs e)
        {
            ExtendedCameraProperties.CameraPropertiesChangedEventArgs eventArgs = (ExtendedCameraProperties.CameraPropertiesChangedEventArgs)e;
            ExtendedCameraProperties.CameraProperties userControl = (ExtendedCameraProperties.CameraProperties)sender;
            if (userControl != null)
            {
                CameraProperties cameraProperties = (CameraProperties)_componentList.Find(props => props.BaseEntity.Name == userControl.NodeName);
                if (cameraProperties != null)
                {
                    cameraProperties.AspectRatio = eventArgs.AspectRatio;
                }
            }
        }

        private PulsarToolBar.PulsarToolBar SetupToolBarComponent(int xPosition, int defaultWidth, ref int currentYPosition)
        {
            var existingNode = PropertiesPanel.Controls.Find("PulsarExtendedPropertiesToolBar", true);
            if (existingNode.Count() != 0)
            {
                currentYPosition += existingNode[0].Height + 1;
                return (PulsarToolBar.PulsarToolBar)existingNode[0];
            }
            PulsarToolBar.PulsarToolBar pulsarToolBar = new PulsarToolBar.PulsarToolBar
            {
                Name = "PulsarExtendedPropertiesToolBar",
                Enabled = true,

                Left = xPosition,
                Top = currentYPosition,
                Width = (defaultWidth > (PropertiesPanel.Width - 20)) ? defaultWidth : PropertiesPanel.Width - 20,
                Tag = "PulsarExtendedPropertiesToolBar"
            };

            Size size = pulsarToolBar.Size;
            size.Height = pulsarToolBar.MaximumHeight;
            pulsarToolBar.MaximumSize = size;

            currentYPosition += pulsarToolBar.Height + 4;

            PropertiesPanel.Controls.Add(pulsarToolBar);

            HookToolBar(pulsarToolBar, HookType.Hook);

            pulsarToolBar.Show();

            return pulsarToolBar;
        }

        private void HookToolBar(PulsarToolBar.PulsarToolBar pulsarToolBar, HookType hook)
        {
            if(pulsarToolBar != null)
            {
                pulsarToolBar.AddComponent += PulsarToolBar_AddComponent;
            }
        }

        private void PulsarToolBar_AddComponent(object sender, EventArgs e)
        {
            //here we need to allow the user to pick a component to add to the current Nodes (BaseEntity) properties

            //TESTING
            PulsarActionsSetup actionsSetup = new PulsarActionsSetup
            {
                Scene = Scene
            };
            //any one of the component properties will contain the node
            if (_componentList.Count > 0)
            {
                actionsSetup.Node = _componentList[0].BaseEntity.Node;
                actionsSetup.ShowDialog();
                if(_componentList[0].BaseEntity.Actions.ActionList.Count > 0)
                {
                    DoActions(_componentList[0].BaseEntity.Node, _componentList[0].BaseEntity.Actions.ActionList);
                    //Debug.Print("Completed DoActions, exiting method");
                }
            }
        }

        private void DoActions(Node node, List<PulsarAction> actions)
        {
            foreach (PulsarAction action in actions)
            {
                action.IsDone = false;
                //Debug.Print("Starting action " + action.PulsarActionType.ToString() + "...");
                action.StartAction();
                //Debug.Print("...returned from starting action " + action.PulsarActionType.ToString());
            }
        }

        private void SetupNodePropertiesComponent(BaseEntity baseEntity, int xPosition, int defaultWidth, ref int currentYPosition)
        {
            //does this control already exist in the form controls list?
            if (baseEntity != null)
            {
                if (!baseEntity.Node.IsDeleted)
                {
                    var existingNode = PropertiesPanel.Controls.Find("node_" + baseEntity.Node.Name, true);
                    if (existingNode.Count() != 0)
                    {
                        currentYPosition += existingNode[0].Height + 1;
                        return;
                    }

                    NodeProperties nodeProperties = (NodeProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);

                    ExtendedNodeProperties.BasicNodeProperties basicNodeProperties = new ExtendedNodeProperties.BasicNodeProperties
                    {
                        Name = "node_" + nodeProperties.Node.Name,
                        NodeName = nodeProperties.Node.Name,
                        Position = nodeProperties.Node.Position,
              
                        Rotation = nodeProperties.Node.Rotation.ToEulerAngles(),
                        Scale = nodeProperties.Node.Scale,
                        Enabled = nodeProperties.Enabled,

                        Left = xPosition,
                        Top = currentYPosition,
                        Width = (defaultWidth > (PropertiesPanel.Width - 20)) ? defaultWidth : PropertiesPanel.Width - 20,
                        Tag = "basicNodeProperties"
                    };

                    Size size = basicNodeProperties.Size;
                    size.Height = basicNodeProperties.MaximumHeight;
                    basicNodeProperties.MaximumSize = size;
                    nodeProperties.Container = basicNodeProperties;

                    currentYPosition += basicNodeProperties.Height + 1;

                    PropertiesPanel.Controls.Add(basicNodeProperties);

                    HookNodeProperties(basicNodeProperties, HookType.Hook);

                    basicNodeProperties.Show();
                }
            }
        }

        private void HookNodeProperties(ExtendedNodeProperties.BasicNodeProperties basicNodeProperties, HookType hook)
        {
            switch (hook)
            {
                case HookType.Hook:
                    basicNodeProperties.NodeNameChanged += BasicNodeProperties_NodeNameChanged;
                    basicNodeProperties.PositionChanged += BasicNodeProperties_PositionChanged;
                    basicNodeProperties.RotationChanged += BasicNodeProperties_RotationChanged;
                    basicNodeProperties.ScaleChanged += BasicNodeProperties_ScaleChanged;
                    basicNodeProperties.EnabledChanged += BasicNodeProperties_EnabledChanged;
                    basicNodeProperties.WindowRolled += BasicNodeProperties_WindowRolled;
                    break;
                case HookType.UnHook:
                    basicNodeProperties.NodeNameChanged -= BasicNodeProperties_NodeNameChanged;
                    basicNodeProperties.PositionChanged -= BasicNodeProperties_PositionChanged;
                    basicNodeProperties.RotationChanged -= BasicNodeProperties_RotationChanged;
                    basicNodeProperties.ScaleChanged -= BasicNodeProperties_ScaleChanged;
                    basicNodeProperties.EnabledChanged -= BasicNodeProperties_EnabledChanged;
                    basicNodeProperties.WindowRolled -= BasicNodeProperties_WindowRolled;
                    break;
            }
        }

        private void BasicNodeProperties_WindowRolled(object sender, EventArgs e)
        {
            RearrangePropertiesView((ExtendedNodeProperties.BasicNodeProperties)sender, e);
        }

        private void BasicNodeProperties_EnabledChanged(object sender, EventArgs e)
        {
            ExtendedNodeProperties.NodeEnabledChangedEventArgs enabledChanged = (ExtendedNodeProperties.NodeEnabledChangedEventArgs)e;

            ExtendedNodeProperties.BasicNodeProperties userControl = (ExtendedNodeProperties.BasicNodeProperties)sender;

            if(userControl != null)
            {
                //find the original NodeProperties in the local List
                NodeProperties nodeProperties = (NodeProperties)_componentList.Find(props => props.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && props.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if(nodeProperties != null)
                {
                    nodeProperties.Enabled = enabledChanged.NodeEnabled;
                }
            }
        }

        private void BasicNodeProperties_ScaleChanged(object sender, EventArgs e)
        {
            ExtendedNodeProperties.ValueChangedEventArgs valueChanged = (ExtendedNodeProperties.ValueChangedEventArgs)e;

            ExtendedNodeProperties.BasicNodeProperties userControl = (ExtendedNodeProperties.BasicNodeProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                NodeProperties nodeProperties = (NodeProperties)_componentList.Find(props => props.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && props.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.Scale = valueChanged.ValueChanged;
                    _mainApplication.DragType = Helpers.Dragger.DragType.Scale;
                }
            }
        }

        private void BasicNodeProperties_RotationChanged(object sender, EventArgs e)
        {
            ExtendedNodeProperties.ValueChangedEventArgs valueChanged = (ExtendedNodeProperties.ValueChangedEventArgs)e;
            ExtendedNodeProperties.BasicNodeProperties userControl = (ExtendedNodeProperties.BasicNodeProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                NodeProperties nodeProperties = (NodeProperties)_componentList.Find(props => props.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && props.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.Rotation = new Quaternion(valueChanged.ValueChanged.X, valueChanged.ValueChanged.Y, valueChanged.ValueChanged.Z);
                    _mainApplication.DragType = Helpers.Dragger.DragType.Rotate;
                }
            }
        }

        private void BasicNodeProperties_PositionChanged(object sender, EventArgs e)
        {
            ExtendedNodeProperties.ValueChangedEventArgs valueChanged = (ExtendedNodeProperties.ValueChangedEventArgs)e;
            ExtendedNodeProperties.BasicNodeProperties userControl = (ExtendedNodeProperties.BasicNodeProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                NodeProperties nodeProperties = (NodeProperties)_componentList.Find(props => props.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && props.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.ExternallySet = false;
                    nodeProperties.Position = valueChanged.ValueChanged;
                    PulsarMessage message = new PulsarMessage
                    {
                        Type = PulsarMessage.MessageType.DraggingStopped,
                        Iterations = 1
                    };
                    message.Properties.Add("externallySet", false);
                    _mainApplication.MessageQueue.PushMessage(message);
                    _mainApplication.DragType = Helpers.Dragger.DragType.Translate;
                }
            }
        }

        private void BasicNodeProperties_NodeNameChanged(object sender, EventArgs e)
        {
            ExtendedNodeProperties.NodeNameChangedEventArgs nodeNameChanged = (ExtendedNodeProperties.NodeNameChangedEventArgs)e;

            var oldNodeName = nodeNameChanged.OldNodeName;
            var newNodeName = nodeNameChanged.NodeName;

            ExtendedNodeProperties.BasicNodeProperties userControl = (ExtendedNodeProperties.BasicNodeProperties)sender;

            if (userControl != null)
            {
                //find the original NodeProperties in the local List
                NodeProperties nodeProperties = (NodeProperties)_componentList.Find(props => props.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && props.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.Name = newNodeName;
                }
            }
        }

        public void RemoveComponentFromPropertiesList()
        {
            RebuildPropertiesDisplay();
        }

        private void PulsarExtendedProperties_Resize(object sender, EventArgs e)
        {
            foreach(Control control in PropertiesPanel.Controls)
            {
                if(PropertiesPanel.Width > 332)
                {
                    control.Width = PropertiesPanel.Width - 20;
                }
                else
                {
                    control.Width = 332;
                }
            }
        }

        public void CallBack(PulsarMessage message)
        {
            message.Properties.TryGetValue("nodeName", out object nodeName);
            message.Properties.TryGetValue("sceneObject", out object sceneObject);
            message.Properties.TryGetValue("externallySet", out object externallySet);
            
            switch (message.Type)
            {
                case PulsarMessage.MessageType.ShowObjectProperties:
                    var delegateShowObjectProperties = new ThreadSafeShowObjectProperties(SetProperties);
                    if (sceneObject != null)
                    {
                        message.Properties.TryGetValue("sceneObjectType", out object sceneObjectType);
                        if (sceneObjectType != null && externallySet != null)
                        {
                            PropertiesPanel.Invoke(delegateShowObjectProperties, new object[] { (SceneObjectType)sceneObjectType, sceneObject, (bool)externallySet });
                        }
                    }
                    break;
                case PulsarMessage.MessageType.ResetPropertiesWindow:
                    var delegateReset = new ThreadSafeResetComponentView(ResetPropertiesWindow);
                    PropertiesPanel.Invoke(delegateReset, Array.Empty<object>());
                    break;
                case PulsarMessage.MessageType.NodeTranslationChange:
                    var delegateTranslationChange = new ThreadSafeNodeTranslationChange(TranslationChange);
                    if (sceneObject != null && externallySet != null)
                    {
                        PropertiesPanel.Invoke(delegateTranslationChange, new object[] { ((string)nodeName), (Node)sceneObject, (bool)externallySet });
                    }
                    break;
                case PulsarMessage.MessageType.NodeRotationChange:
                    var delegateRotationChange = new ThreadSafeNodeRotationChange(RotationChange);
                    if (sceneObject != null && externallySet != null)
                    {
                        PropertiesPanel.Invoke(delegateRotationChange, new object[] { ((string)nodeName), (Node)sceneObject, (bool)externallySet });
                    }
                    break;
                case PulsarMessage.MessageType.NodeScaleChange:
                    var delegateScaleChange = new ThreadSafeNodeScaleChange(ScaleChange);
                    if (sceneObject != null && externallySet != null)
                    {
                        PropertiesPanel.Invoke(delegateScaleChange, new object[] { ((string)nodeName), (Node)sceneObject, (bool)externallySet });
                    }
                    break;
            }
        }

        public void ResetPropertiesWindow()
        {
            _componentList.Clear();

            foreach(Control control in PropertiesPanel.Controls)
            {
                switch(control.GetType().Name)
                {
                    case "BasicNodeProperties":
                        HookNodeProperties((ExtendedNodeProperties.BasicNodeProperties)control, HookType.UnHook);
                        break;
                    case "ModelProperties":
                        HookModelProperties((ExtendedModelProperties.ModelProperties)control, HookType.UnHook);
                        break;
                    case "LightProperties":
                        HookLightProperties((ExtendedLightProperties.LightProperties)control, HookType.UnHook);
                        break;
                    case "CameraProperties":
                        HookCameraProperties((ExtendedCameraProperties.CameraProperties)control, HookType.UnHook);
                        break;
                }
            }
            PropertiesPanel.Controls.Clear();
        }

        private void HookModelProperties(ExtendedModelProperties.ModelProperties modelProperties, HookType hook)
        {
            switch(hook)
            {
                case HookType.Hook:
                    modelProperties.ModelChanged += ModelProperties_ModelChanged;
                    modelProperties.MaterialChanged += ModelProperties_MaterialChanged;
                    modelProperties.RemoveModel += ModelProperties_RemoveModel;
                    modelProperties.QueryContinueDrag += ModelProperties_QueryContinueDrag;
                    modelProperties.Resize += ModelProperties_Resize;
                    modelProperties.WindowRolled += ModelProperties_WindowRolled;
                    break;
                case HookType.UnHook:
                    modelProperties.ModelChanged -= ModelProperties_ModelChanged;
                    modelProperties.MaterialChanged -= ModelProperties_MaterialChanged;
                    modelProperties.RemoveModel -= ModelProperties_RemoveModel;
                    modelProperties.QueryContinueDrag -= ModelProperties_QueryContinueDrag;
                    modelProperties.Resize -= ModelProperties_Resize;
                    modelProperties.WindowRolled -= ModelProperties_WindowRolled;
                    break;
            }
        }

        private void ModelProperties_WindowRolled(object sender, EventArgs e)
        {
            RearrangePropertiesView((ExtendedModelProperties.ModelProperties)sender, e);
        }

        private void ModelProperties_Resize(object sender, EventArgs e)
        {
            ExtendedModelProperties.ModelProperties modelProperties = (ExtendedModelProperties.ModelProperties)sender;
            if(modelProperties != null)
            {
                if (PropertiesPanel.Width > 332)
                {
                    Size size = modelProperties.Size;
                    size.Width = PropertiesPanel.Width - 20;
                    modelProperties.Size = size;
                }
            }
        }

        private void ScaleChange(string nodeName, Node sceneNode, bool externallySet)
        {
            var baseEntity = sceneNode.GetComponent<BaseEntity>();
            if (baseEntity != null)
            {
                var nodeProperties = (NodeProperties)baseEntity.ComponentProperties.Find(node => node.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && node.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.ExternallySet = externallySet;
                    nodeProperties.Scale = sceneNode.Scale;
                    var basicNodeProperties = PropertiesPanel.Controls.Find("node_" + nodeName, true);
                    if (basicNodeProperties.Length > 0)
                    {
                        //should only be one
                        ExtendedNodeProperties.BasicNodeProperties properties = (ExtendedNodeProperties.BasicNodeProperties)basicNodeProperties[0];
                        if (properties != null)
                        {
                            properties.ExternallySet = externallySet;
                            properties.Scale = sceneNode.Scale;
                        }
                    }
                }
            }
        }

        private void RotationChange(string nodeName, Node sceneNode, bool externallySet)
        {
            var baseEntity = sceneNode.GetComponent<BaseEntity>();
            if (baseEntity != null)
            {
                var nodeProperties = (NodeProperties)baseEntity.ComponentProperties.Find(node => node.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && node.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.ExternallySet = externallySet;
                    nodeProperties.Rotation = sceneNode.Rotation;

                    var basicNodeProperties = PropertiesPanel.Controls.Find("node_" + nodeName, true);
                    if (basicNodeProperties.Length > 0)
                    {
                        //should only be one
                        ExtendedNodeProperties.BasicNodeProperties properties = (ExtendedNodeProperties.BasicNodeProperties)basicNodeProperties[0];
                        if (properties != null)
                        {
                            properties.ExternallySet = externallySet;
                            properties.Rotation = sceneNode.Rotation.ToEulerAngles();
                        }
                    }
                }
            }
        }

        private void TranslationChange(string nodeName, Node sceneNode, bool externallySet)
        {
            //this can only affect the basicNodeProperties - TODO: revisit this concept of 'currentPropertyNode', won't work with
            //multiple properties types
            var baseEntity = sceneNode.GetComponent<BaseEntity>();
            if (baseEntity != null)
            {
                var nodeProperties = (NodeProperties)baseEntity.ComponentProperties.Find(node => node.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && node.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.ExternallySet = externallySet;
                    nodeProperties.Position = sceneNode.Position;

                    var basicNodeProperties = PropertiesPanel.Controls.Find("node_" + nodeName, true);
                    if (basicNodeProperties.Length > 0)
                    {
                        //should only be one
                        ExtendedNodeProperties.BasicNodeProperties properties = (ExtendedNodeProperties.BasicNodeProperties)basicNodeProperties[0];
                        if (properties != null)
                        {
                            properties.ExternallySet = externallySet;
                            properties.Position = sceneNode.Position;
                        }
                    }
                }
            }
        }

        private void SetProperties(SceneObjectType sceneObjectType, object sceneObject, bool externallySet)
        {
            //get the base entity component properties list
            if (sceneObject != null)
            {
                Node node = (Node)sceneObject;
                if (node != null)
                {
                    BaseEntity baseEntity = node.GetComponent<BaseEntity>();
                    if (baseEntity != null)
                    {
                        if (baseEntity.ComponentProperties.Count > 0)
                        {
                            foreach (PulsarComponent pulsarComponent in baseEntity.ComponentProperties)
                            {
                                switch (pulsarComponent.PulsarComponentType)
                                {
                                    case PulsarComponent.ComponentType.LightProperties:
                                        CreateLightProperties(sceneObject, externallySet);
                                        break;
                                    case PulsarComponent.ComponentType.CameraProperties:
                                        CreateCameraProperties(sceneObject, externallySet);
                                        break;
                                    case PulsarComponent.ComponentType.NodeProperties:
                                        CreateNodeProperties(sceneObject, externallySet);
                                        break;
                                    case PulsarComponent.ComponentType.ModelProperties:
                                        CreateModelProperties(sceneObject, externallySet);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateModelProperties(object sceneObject, bool externallySet)
        {
            Node node = (Node)sceneObject;
            if(node != null)
            {
                var baseEntity = node.GetComponent<BaseEntity>();
                if(baseEntity != null)
                {
                    var modelProperties = baseEntity.ComponentProperties.Find(prop => prop.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && prop.PulsarComponentType == PulsarComponent.ComponentType.ModelProperties);
                    if (modelProperties != null)
                        AddComponentToPropertiesList(modelProperties);
                }
            }
        }

        private void CreateNodeProperties(object sceneObject, bool externallySet)
        {
            Node node = (Node)sceneObject;
            if (node != null)
            {
                var baseEntity = node.GetComponent<BaseEntity>();
                if (baseEntity != null)
                {
                    var nodeProperties = baseEntity.ComponentProperties.Find(prop => prop.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && prop.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                    if(nodeProperties != null)
                        AddComponentToPropertiesList(nodeProperties);
                }
            }
        }

        private void CreatePlaneProperties(object sceneObject, bool externallySet)
        {
            throw new NotImplementedException();
        }

        private void CreateCameraProperties(object sceneObject, bool externallySet)
        {
            Node node = (Node)sceneObject;
            if (node != null)
            {
                var baseEntity = node.GetComponent<BaseEntity>();
                if (baseEntity != null)
                {
                    var cameraProperties = baseEntity.ComponentProperties.Find(prop => prop.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && prop.PulsarComponentType == PulsarComponent.ComponentType.CameraProperties);
                    if (cameraProperties != null)
                        AddComponentToPropertiesList(cameraProperties);
                }
            }
        }

        private void CreateLightProperties(object sceneObject, bool externallySet)
        {
            Node node = (Node)sceneObject;
            if(node != null)
            {
                var baseEntity = node.GetComponent<BaseEntity>();
                if(baseEntity != null)
                {
                    var lightProperties = baseEntity.ComponentProperties.Find(prop => prop.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && prop.PulsarComponentType == PulsarComponent.ComponentType.LightProperties);
                    if (lightProperties != null)
                        AddComponentToPropertiesList(lightProperties);
                }
            }
        }

        public object Registrant()
        {
            return this;
        }

        public string RegistrantName()
        {
            return "PulsarExtendedProperties";
        }

        private void RearrangePropertiesView(Control control, EventArgs roll)
        {
            const int X_Position = 11;
            var currentYPosition = DEFAULT_TOP_POSITION;

            var startingX = PropertiesPanel.AutoScrollPosition.X + X_Position;
            foreach (Control ctl in PropertiesPanel.Controls)
            {
                if (ctl != null)
                {

                    PulsarToolBar.PulsarToolBar pulsarToolBar;
                    if (ctl.Name == "PulsarExtendedPropertiesToolBar")
                        pulsarToolBar = (PulsarToolBar.PulsarToolBar)ctl;
                    //because the window will vertically scroll when new sets of controls exceed the height of the form
                    //we actually find that the Y location can be negative! We have to create a new point for the location each time
                    Point point = new Point(startingX, PropertiesPanel.AutoScrollPosition.Y + currentYPosition);
                    ctl.Location = point;
                    GetHeightOfControl(control, ctl, roll, ref currentYPosition);
                }
            }
        }

        private static void GetHeightOfControl(Control eventControl, Control iteratedControl, EventArgs e, ref int currentYPosition)
        {
            int returnedHeight = 33; // default to minimum in case something goes wrong
            if (eventControl.Equals(iteratedControl))
            {
                switch (iteratedControl.GetType().Name)
                {
                    case "CameraProperties":
                        ExtendedCameraProperties.WindowRollEventArgs cameraRoll = (ExtendedCameraProperties.WindowRollEventArgs)e;
                        ExtendedCameraProperties.CameraProperties cameraProperties = (ExtendedCameraProperties.CameraProperties)eventControl;
                        if(cameraProperties != null)
                        {
                            if(cameraRoll.WindowRoll == ExtendedCameraProperties.CameraProperties.WindowRoll.RollDown)
                            {
                                returnedHeight = cameraProperties.MaximumHeight;
                            }
                            else
                            {
                                returnedHeight = cameraProperties.MinimumHeight;
                            }
                        }
                        break;
                    case "LightProperties":
                        ExtendedLightProperties.WindowRollEventArgs lightRoll = (ExtendedLightProperties.WindowRollEventArgs)e;
                        ExtendedLightProperties.LightProperties lightProperties = (ExtendedLightProperties.LightProperties)eventControl;
                        if (lightProperties != null)
                        {
                            if (lightRoll.WindowRoll == ExtendedLightProperties.LightProperties.WindowRoll.RollDown)
                            {
                                returnedHeight = lightProperties.MaximumHeight;
                            }
                            else
                            {
                                returnedHeight = lightProperties.MinimumHeight;
                            }
                        }
                        break;
                    case "ModelProperties":
                        WindowRollEventArgs modelRoll = (WindowRollEventArgs)e;
                        ExtendedModelProperties.ModelProperties modelProperties = (ExtendedModelProperties.ModelProperties)eventControl;
                        if (modelRoll.WindowRoll == ExtendedModelProperties.ModelProperties.WindowRoll.RollDown)
                        {
                            returnedHeight = modelProperties.MaximumHeight;
                        }
                        else
                        {
                            returnedHeight = modelProperties.MinimumHeight;
                        }
                        break;
                    case "BasicNodeProperties":
                        ExtendedNodeProperties.WindowRollEventArgs nodeRoll = (ExtendedNodeProperties.WindowRollEventArgs)e;
                        ExtendedNodeProperties.BasicNodeProperties nodeProperties = (ExtendedNodeProperties.BasicNodeProperties)eventControl;
                        if (nodeRoll.WindowRoll == ExtendedNodeProperties.BasicNodeProperties.WindowRoll.RollDown)
                        {
                            returnedHeight = nodeProperties.MaximumHeight;
                        }
                        else
                        {
                            returnedHeight = nodeProperties.MinimumHeight;
                        }
                        break;
                    default:
                        break;
                }
                currentYPosition += returnedHeight + 1;
            }
            else
            {
                currentYPosition += (iteratedControl.Height + 1);
            }


        }
    }
}
