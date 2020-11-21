using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using InternalNodeProperties = Pulsar.ObjectModel.PropertiesModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Urho;
using Pulsar.ObjectModel.PropertiesModel;
using System.Diagnostics;
using Pulsar.ObjectModel.Primitives;
using ExtendedModelProperties;
using PulsarToolBar;

namespace Pulsar
{
    public partial class PulsarExtendedProperties : DockContent, IRegisterMessage
    {
        //component properties list
        private readonly List<PulsarComponent> _componentList;

        private delegate void ThreadSafeShowObjectProperties(SceneObjectType objectType, object node, bool externallySet);
        private delegate void ThreadSafeResetComponentView();
        private delegate void ThreadSafeNodeTranslationChange(string nodeName, Node sceneNode, bool externallySet);
        private delegate void ThreadSafeNodeRotationChange(string nodeName, Node sceneNode, bool externallySet);
        private delegate void ThreadSafeNodeScaleChange(string nodeName, Node sceneNode, bool externallySet);
        private delegate void ThreadSafeFormResize(PulsarMessage message);

        private enum HookType
        {
            Hook = 0,
            UnHook
        }

        private const int DEFAULT_WIDTH = 332;
        private const int DEFAULT_TOP_POSITION = 12;

        public PulsarExtendedProperties()
        {
            InitializeComponent();

            _componentList = new List<PulsarComponent>();

            Resize += PulsarExtendedProperties_Resize;
        }

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

            var toolBar = SetupToolBarComponent(X_Position, DEFAULT_WIDTH, ref currentYPosition);

            foreach (PulsarComponent control in _componentList)
            {
                var controlName = control.GetType().Name;
                Debug.Print("PulsarExtendedProperties.RebuildPropertiesDisplay - Checking for control '" + controlName + "'");
                switch(controlName)
                {
                    case "NodeProperties":
                        Debug.Print("PulsarExtendedProperties.RebuildPropertiesDisplay - Calling SetupNodePropertiesComponent...");
                        SetupNodePropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                        break;
                    case "ModelProperties":
                        Debug.Print("PulsarExtendedProperties.RebuildPropertiesDisplay - Calling SetupModelPropertiesComponent...");
                        SetupModelPropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                        break;
                    case "LightProperties":
                        Debug.Print("PulsarExtendedProperties.RebuildPropertiesDisplay - Calling SetupLightPropertiesComponent...");
                        SetupLightPropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                        break;
                    case "CameraProperties":
                        Debug.Print("PulsarExtendedProperties.RebuildPropertiesDisplay - Calling SetupCameraPropertiesComponent...");
                        SetupCameraPropertiesComponent(control.BaseEntity, X_Position, DEFAULT_WIDTH, ref currentYPosition);
                        break;
                }
            }
            ScrollToControl(toolBar);
        }
        private void SetupLightPropertiesComponent(BaseEntity baseEntity, int xPosition, int defaultWidth, ref int currentYPosition)
        {
            if(baseEntity != null)
            {
                if(!baseEntity.Node.IsDeleted)
                {
                    var existingNode = Controls.Find("light_" + baseEntity.Node.Name, true);
                    if(existingNode.Count<Control>() != 0)
                    {
                        currentYPosition += existingNode[0].Height + 1;
                        return;
                    }

                    InternalNodeProperties.LightProperties light = (InternalNodeProperties.LightProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.LightProperties);

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
                        Width = (defaultWidth > (Width - 20)) ? defaultWidth : Width - 20,
                        Tag = "lightProperties"
                    };
                    Size size = lightProperties.Size;
                    size.Height = lightProperties.MaximumHeight;
                    lightProperties.MaximumSize = size;
                    light.Container = lightProperties;

                    currentYPosition += lightProperties.Height + 1;

                    Controls.Add(lightProperties);

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
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for AspecRatioChanged");
                    lightProperties.BrightnessChanged += LightProperties_BrightnessChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for BrightnessChanged");
                    lightProperties.ColourChanged += LightProperties_ColourChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for ColourChanged");
                    lightProperties.EffectiveSpecularIntensityChanged += LightProperties_EffectiveSpecularIntensityChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for EffectiveSpecularIntensityChanged");
                    lightProperties.FadeDistanceChanged += LightProperties_FadeDistanceChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for FadeDistanceChanged");
                    lightProperties.UsePhysicalValuesChanged += LightProperties_UsePhysicalValuesChanged;
                    lightProperties.FieldOfViewChanged += LightProperties_FieldOfViewChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for FieldOfViewChanged");
                    lightProperties.LengthChanged += LightProperties_LengthChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for LengthChanged");
                    lightProperties.LightTypeChanged += LightProperties_LightTypeChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for LightTypeChanged");
                    lightProperties.PerVertexChanged += LightProperties_PerVertexChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for PerVertexChanged");
                    lightProperties.RadiusChanged += LightProperties_RadiusChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for RadiusChanged");
                    lightProperties.RangeChanged += LightProperties_RangeChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for RangeChanged");
                    lightProperties.TemperatureChanged += LightProperties_TemperatureChanged;
                    lightProperties.ShadowFadeDistanceChanged += LightProperties_ShadowFadeDistanceChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for ShadowFadeDistanceChanged");
                    lightProperties.ShadowIntensityChanged += LightProperties_ShadowIntensityChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for ShadowIntensityChanged");
                    lightProperties.ShadowMaximumExtrusionChanged += LightProperties_ShadowMaximumExtrusionChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for ShadowMaximumExtrusionChanged");
                    lightProperties.ShadowNearFarRatioChanged += LightProperties_ShadowNearFarRatioChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for ShadowNearFarRatioChanged");
                    lightProperties.ShadowResolutionChanged += LightProperties_ShadowResolutionChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for ShadowResolutionChanged");
                    lightProperties.WindowRolled += LightProperties_WindowRolled;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Added event handler for WindowRolled");
                    break;
                case HookType.UnHook:
                    lightProperties.AspectRatioChanged -= LightProperties_AspectRatioChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for AspecRatioChanged");
                    lightProperties.BrightnessChanged -= LightProperties_BrightnessChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for BrightnessChanged");
                    lightProperties.ColourChanged -= LightProperties_ColourChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for ColourChanged");
                    lightProperties.EffectiveSpecularIntensityChanged -= LightProperties_EffectiveSpecularIntensityChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for EffectiveSpecularIntensityChanged");
                    lightProperties.FadeDistanceChanged -= LightProperties_FadeDistanceChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for FadeDistanceChanged");
                    lightProperties.FieldOfViewChanged -= LightProperties_FieldOfViewChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for FieldOfViewChanged");
                    lightProperties.LightTypeChanged -= LightProperties_LightTypeChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for LightTypeChanged");
                    lightProperties.PerVertexChanged -= LightProperties_PerVertexChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for PerVertexChanged");
                    lightProperties.RadiusChanged -= LightProperties_RadiusChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for RadiusChanged");
                    lightProperties.RangeChanged -= LightProperties_RangeChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for RangeChanged");
                    lightProperties.TemperatureChanged -= LightProperties_TemperatureChanged;
                    lightProperties.ShadowFadeDistanceChanged -= LightProperties_ShadowFadeDistanceChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for ShadowFadeDistanceChanged");
                    lightProperties.ShadowIntensityChanged -= LightProperties_ShadowIntensityChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for ShadowIntensityChanged");
                    lightProperties.ShadowMaximumExtrusionChanged -= LightProperties_ShadowMaximumExtrusionChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for ShadowMaximumExtrusionChanged");
                    lightProperties.ShadowNearFarRatioChanged -= LightProperties_ShadowNearFarRatioChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for ShadowNearFarRatioChanged");
                    lightProperties.ShadowResolutionChanged -= LightProperties_ShadowResolutionChanged;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for ShadowResolutionChanged");
                    lightProperties.WindowRolled -= LightProperties_WindowRolled;
                    Debug.Print("PulsarExtendedProperties.HookLightProperties - Removed event handler for WindowRolled");
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
            ExtendedLightProperties.WindowRollEventArgs eventArgs = (ExtendedLightProperties.WindowRollEventArgs)e;
            Debug.Print("PulsarExtendedProperties.LightProperties_WindowRolled - Received " + eventArgs.WindowRoll.ToString() + " event, calling RearrangePropertiesView...");
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
                Debug.Print("PulsarExtendedProperties.SetupModelPropertiesComponent - Found nodeProperties '" + baseEntity.Name + "'");
                if (!baseEntity.Node.IsDeleted)
                {
                    var existingNode = Controls.Find("model_" + baseEntity.Node.Name, true);
                    if (existingNode.Count<Control>() != 0)
                    {
                        Debug.Print("PulsarExtendedProperties.SetupModelPropertiesComponent - Found existing user control, exiting!");
                        currentYPosition += existingNode[0].Height + 1;
                        return;
                    }

                    InternalNodeProperties.ModelProperties mod = (InternalNodeProperties.ModelProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.ModelProperties);

                    Debug.Print("PulsarExtendedProperties.SetupModelPropertiesComponent - Setting up user control");
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
                        Width = (defaultWidth > (Width - 20)) ? defaultWidth : Width - 20,
                        Tag = "modelProperties",
                        AllowDrop = true
                    };

                    Size size = modelProperties.Size;
                    size.Height = modelProperties.MaximumHeight;
                    modelProperties.MaximumSize = size;
                    mod.Container = modelProperties;

                    currentYPosition += modelProperties.Height + 1;

                    Debug.Print("PulsarExtendedProperties.SetupModelPropertiesComponent - Adding model properties control to form list");
                    Controls.Add(modelProperties);


                    Debug.Print("PulsarExtendedProperties.SetupModelPropertiesComponent - Setting up event handlers");
                    HookModelProperties(modelProperties, HookType.Hook);
                    Debug.Print("PulsarExtendedProperties.SetupModelPropertiesComponent - Showing control");
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
                    Debug.Print("PulsarExtendedProperties.SetupCameraPropertiesComponent - Checking for existing control called '" + controlName + "'");
                    var existingNode = Controls.Find(controlName, true);
                    if(existingNode.Count<Control>() != 0)
                    {
                        currentYPosition += existingNode[0].Height + 1;
                        Debug.Print("PulsarExtendedProperties.SetupCameraPropertiesComponent - Found existing control, exiting and updating currentYPosition to '" + currentYPosition.ToString() + "'");
                        return;
                    }
                    Debug.Print("PulsarExtendedProperties.SetupCameraPropertiesComponent - Creating camera properties");
                    InternalNodeProperties.CameraProperties camera = (InternalNodeProperties.CameraProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.CameraProperties);

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
                        Width = (defaultWidth > (Width - 20)) ? defaultWidth : Width - 20,
                        Tag = "cameraProperties"
                    };

                    camera.Container = properties;

                    currentYPosition += properties.Height + 1;

                    Controls.Add(properties);

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
            var existingNode = Controls.Find("PulsarExtendedPropertiesToolBar", true);
            if (existingNode.Count<Control>() != 0)
            {
                Debug.Print("PulsarExtendedProperties.SetupToolBarComponent - Found existing user control, exiting!");
                currentYPosition += existingNode[0].Height + 1;
                return (PulsarToolBar.PulsarToolBar)existingNode[0];
            }
            PulsarToolBar.PulsarToolBar pulsarToolBar = new PulsarToolBar.PulsarToolBar
            {
                Name = "PulsarExtendedPropertiesToolBar",
                Enabled = true,

                Left = xPosition,
                Top = currentYPosition,
                Width = (defaultWidth > (Width - 20)) ? defaultWidth : Width - 20,
                Tag = "PulsarExtendedPropertiesToolBar"
            };

            Size size = pulsarToolBar.Size;
            size.Height = pulsarToolBar.MaximumHeight;
            pulsarToolBar.MaximumSize = size;
            //nodeProperties.Container = basicNodeProperties;

            currentYPosition += pulsarToolBar.Height + 4;

            Debug.Print("PulsarExtendedProperties.SetupToolBarComponent - Adding ToolBar control to form list");
            Controls.Add(pulsarToolBar);

            //Debug.Print("PulsarExtendedProperties.SetupNodePropertiesComponent - Setting up event handlers");

            HookToolBar(pulsarToolBar, HookType.Hook);

            Debug.Print("PulsarExtendedProperties.SetupToolBarComponent - Showing control");
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
        }

        private void SetupNodePropertiesComponent(BaseEntity baseEntity, int xPosition, int defaultWidth, ref int currentYPosition)
        {
            //does this control already exist in the form controls list?
            if (baseEntity != null)
            {
                Debug.Print("PulsarExtendedProperties.SetupNodePropertiesComponent - Found baseEntity '" + baseEntity.Name + "'");
                if (!baseEntity.Node.IsDeleted)
                {
                    var existingNode = Controls.Find("node_" + baseEntity.Node.Name, true);
                    if (existingNode.Count<Control>() != 0)
                    {
                        Debug.Print("PulsarExtendedProperties.SetupNodePropertiesComponent - Found existing user control, exiting!");
                        currentYPosition += existingNode[0].Height + 1;
                        return;
                    }

                    NodeProperties nodeProperties = (NodeProperties)baseEntity.ComponentProperties.Find(comp => comp.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && comp.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);

                    Debug.Print("PulsarExtendedProperties.SetupNodePropertiesComponent - Setting up user control");
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
                        Width = (defaultWidth > (Width - 20)) ? defaultWidth : Width - 20,
                        Tag = "basicNodeProperties"
                    };

                    Size size = basicNodeProperties.Size;
                    size.Height = basicNodeProperties.MaximumHeight;
                    basicNodeProperties.MaximumSize = size;
                    nodeProperties.Container = basicNodeProperties;

                    currentYPosition += basicNodeProperties.Height + 1;

                    Debug.Print("PulsarExtendedProperties.SetupNodePropertiesComponent - Adding basicNodeProperties control to form list");
                    Controls.Add(basicNodeProperties);

                    //Debug.Print("PulsarExtendedProperties.SetupNodePropertiesComponent - Setting up event handlers");

                    HookNodeProperties(basicNodeProperties, HookType.Hook);

                    Debug.Print("PulsarExtendedProperties.SetupNodePropertiesComponent - Showing control");
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
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Added event handler for NodeNameChanged");
                    basicNodeProperties.PositionChanged += BasicNodeProperties_PositionChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Added event handler for PositionChanged");
                    basicNodeProperties.RotationChanged += BasicNodeProperties_RotationChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Added event handler for RotationChanged");
                    basicNodeProperties.ScaleChanged += BasicNodeProperties_ScaleChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Added event handler for ScaleChanged");
                    basicNodeProperties.EnabledChanged += BasicNodeProperties_EnabledChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Added event handler for EnabledChanged");
                    basicNodeProperties.WindowRolled += BasicNodeProperties_WindowRolled;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Added event handler for WindowRolled");
                    break;
                case HookType.UnHook:
                    basicNodeProperties.NodeNameChanged -= BasicNodeProperties_NodeNameChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Removing event handler for NodeNameChanged");
                    basicNodeProperties.PositionChanged -= BasicNodeProperties_PositionChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Removing event handler for PositionChanged");
                    basicNodeProperties.RotationChanged -= BasicNodeProperties_RotationChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Removing event handler for RotationChanged");
                    basicNodeProperties.ScaleChanged -= BasicNodeProperties_ScaleChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Removing event handler for ScaleChanged");
                    basicNodeProperties.EnabledChanged -= BasicNodeProperties_EnabledChanged;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Removing event handler for EnabledChanged");
                    basicNodeProperties.WindowRolled -= BasicNodeProperties_WindowRolled;
                    Debug.Print("PulsarExtendedProperties.HookNodeProperties - Removing event handler for WindowRolled");
                    break;
            }
        }

        private void BasicNodeProperties_WindowRolled(object sender, EventArgs e)
        {
            ExtendedNodeProperties.WindowRollEventArgs eventArgs = (ExtendedNodeProperties.WindowRollEventArgs)e;
            Debug.Print("PulsarExtendedProperties.BasicNodeProperties_WindowRolled - Received " + eventArgs.WindowRoll.ToString() + " event, calling RearrangePropertiesView...");
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
            foreach(Control control in Controls)
            {
                if(Width > 332)
                {
                    control.Width = Width - 20;
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
                case PulsarMessage.MessageType.AdjustRenderFormWidth:
                    AdjustFormWidth(message);
                    break;
                case PulsarMessage.MessageType.ShowObjectProperties:
                    //just for the moment - for testing
                    var delegateShowObjectProperties = new ThreadSafeShowObjectProperties(SetProperties);
                    if (sceneObject != null)
                    {
                        message.Properties.TryGetValue("sceneObjectType", out object sceneObjectType);
                        if (sceneObjectType != null && externallySet != null)
                        {
                            Invoke(delegateShowObjectProperties, new object[] { (SceneObjectType)sceneObjectType, sceneObject, (bool)externallySet });
                        }
                    }
                    break;
                case PulsarMessage.MessageType.ResetPropertiesWindow:
                    var delegateReset = new ThreadSafeResetComponentView(ResetPropertiesWindow);
                    Invoke(delegateReset, Array.Empty<object>());
                    break;
                case PulsarMessage.MessageType.NodeTranslationChange:
                    //Debug.Print("PulsarExtendedProperties.CallBack - Received a 'NodeTranslationChange' message");
                    var delegateTranslationChange = new ThreadSafeNodeTranslationChange(TranslationChange);
                    if (sceneObject != null && externallySet != null)
                    {
                        //Debug.Print("PulsarExtendedProperties.CallBack - Invoking delegateTranslationChange");
                        Invoke(delegateTranslationChange, new object[] { ((string)nodeName), (Node)sceneObject, (bool)externallySet });
                    }
                    break;
                case PulsarMessage.MessageType.NodeRotationChange:
                    var delegateRotationChange = new ThreadSafeNodeRotationChange(RotationChange);
                    if (sceneObject != null && externallySet != null)
                    {
                        Invoke(delegateRotationChange, new object[] { ((string)nodeName), (Node)sceneObject, (bool)externallySet });
                    }
                    break;
                case PulsarMessage.MessageType.NodeScaleChange:
                    var delegateScaleChange = new ThreadSafeNodeScaleChange(ScaleChange);
                    if (sceneObject != null && externallySet != null)
                    {
                        Invoke(delegateScaleChange, new object[] { ((string)nodeName), (Node)sceneObject, (bool)externallySet });
                    }
                    break;
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
                        Size newSize = new Size((int)widthProperty, Size.Height);
                        Size = newSize;
                    }
                }
            }
            ResumeLayout();
        }

        public void ResetPropertiesWindow()
        {
            _componentList.Clear();

            foreach(Control control in Controls)
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
            Controls.Clear();
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
            ExtendedModelProperties.WindowRollEventArgs eventArgs = (ExtendedModelProperties.WindowRollEventArgs)e;
            Debug.Print("PulsarExtendedProperties.ModelProperties_WindowRolled - Received " + eventArgs.WindowRoll.ToString() + " event, calling RearrangePropertiesView...");
            RearrangePropertiesView((ExtendedModelProperties.ModelProperties)sender, e);
        }

        private void ModelProperties_Resize(object sender, EventArgs e)
        {
            ExtendedModelProperties.ModelProperties modelProperties = (ExtendedModelProperties.ModelProperties)sender;
            if(modelProperties != null)
            {
                if (Width > 332)
                {
                    Size size = modelProperties.Size;
                    size.Width = Width - 20;
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
                    var basicNodeProperties = Controls.Find("node_" + nodeName, true);
                    if (basicNodeProperties.Length > 0)
                    {
                        //should only be one
                        ExtendedNodeProperties.BasicNodeProperties properties = (ExtendedNodeProperties.BasicNodeProperties)basicNodeProperties[0];
                        if (properties != null)
                        {
                            //Debug.Print("PulsarExtendedProperties.TranslationChange - Found the basic properties control");
                            properties.ExternallySet = externallySet;
                            //Debug.Print("PulsarExtendedProperties.TranslationChange - Setting properties control position to '" + position.ToString() + "'");
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

                    var basicNodeProperties = Controls.Find("node_" + nodeName, true);
                    if (basicNodeProperties.Length > 0)
                    {
                        //should only be one
                        ExtendedNodeProperties.BasicNodeProperties properties = (ExtendedNodeProperties.BasicNodeProperties)basicNodeProperties[0];
                        if (properties != null)
                        {
                            //Debug.Print("PulsarExtendedProperties.TranslationChange - Found the basic properties control");
                            properties.ExternallySet = externallySet;
                            //Debug.Print("PulsarExtendedProperties.TranslationChange - Setting properties control position to '" + position.ToString() + "'");
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
            //Debug.Print("PulsarExtendedProperties.TranslationChange - Setting _currentPropertyNode ExternallySet to " +externallySet.ToString());
            var baseEntity = sceneNode.GetComponent<BaseEntity>();
            if (baseEntity != null)
            {
                var nodeProperties = (NodeProperties)baseEntity.ComponentProperties.Find(node => node.PulsarComponentClass == PulsarComponent.ComponentClass.Properties && node.PulsarComponentType == PulsarComponent.ComponentType.NodeProperties);
                if (nodeProperties != null)
                {
                    nodeProperties.ExternallySet = externallySet;
                    //Debug.Print("PulsarExtendedProperties.TranslationChange - _currentPropertyNode Position set to '" + position.ToString() + "'");
                    nodeProperties.Position = sceneNode.Position;

                    var basicNodeProperties = Controls.Find("node_" + nodeName, true);
                    if (basicNodeProperties.Length > 0)
                    {
                        //should only be one
                        ExtendedNodeProperties.BasicNodeProperties properties = (ExtendedNodeProperties.BasicNodeProperties)basicNodeProperties[0];
                        if (properties != null)
                        {
                            //Debug.Print("PulsarExtendedProperties.TranslationChange - Found the basic properties control");
                            properties.ExternallySet = externallySet;
                            //Debug.Print("PulsarExtendedProperties.TranslationChange - Setting properties control position to '" + position.ToString() + "'");
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
            Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Starting Rearrange...");
            Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - X_Position defined as 11");
            const int X_Position = 11;
            Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Initial Y position is " + DEFAULT_TOP_POSITION.ToString());
            var currentYPosition = DEFAULT_TOP_POSITION;

            var startingX = AutoScrollPosition.X + X_Position;
            Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Starting X position set to AutoScrollPosition.X (" + AutoScrollPosition.X.ToString() + ") + X_Position (11), giving an overall value of - " + startingX.ToString());
            var startingY = AutoScrollPosition.Y + currentYPosition;
            Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Starting Y position set to AutoScrollPosition.Y (" + AutoScrollPosition.Y.ToString() + ") + currentYPosition (" + currentYPosition.ToString() + "), giving an overall value of - " + startingY.ToString());

            PulsarToolBar.PulsarToolBar pulsarToolBar = null;
            foreach (Control ctl in Controls)
            {
                if(ctl != null)
                {
                    if (ctl.Name == "PulsarExtendedPropertiesToolBar")
                        pulsarToolBar = (PulsarToolBar.PulsarToolBar)ctl;
                    //because the window will vertically scroll when new sets of controls exceed the height of the form
                    //we actually find that the Y location can be negative! We have to create a new point for the location each time
                    Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Processing control '" + ctl.Name + "'...");
                    Point point = new Point(startingX, AutoScrollPosition.Y + currentYPosition);
                    Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Created new Point( startingX(" + startingX.ToString() + "), AutoScrollPosition.Y(" + AutoScrollPosition.Y.ToString() + ") + currentYPosition(" + currentYPosition.ToString() + ")");
                    ctl.Location = point;
                    Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Set control.Location to point " + point.ToString());
                    Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - Height of '" + ctl.Name + "' is " + ctl.Height.ToString());
                    GetHeightOfControl(control, ctl, roll, ref currentYPosition);
                    Debug.Print("PulsarExtendedProperties.RearrangePropertiesView - currentYPosition updated to " + currentYPosition.ToString());
                }
            }
            if (pulsarToolBar != null)
                ScrollToControl(pulsarToolBar);
        }

        private void GetHeightOfControl(Control eventControl, Control iteratedControl, EventArgs e, ref int currentYPosition)
        {
            int returnedHeight = 33; // default to minimum in case something goes wrong
            Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Set default returned height to 33");
            if (eventControl.Equals(iteratedControl))
            {
                Debug.Print("PulsarExtendedProperties.GetHeightOfControl - the control that raised the roll event, '" + eventControl.Name + "', matches the iterative control...");
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
                        Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Found iterated control LightProperties...");
                        ExtendedLightProperties.WindowRollEventArgs lightRoll = (ExtendedLightProperties.WindowRollEventArgs)e;
                        ExtendedLightProperties.LightProperties lightProperties = (ExtendedLightProperties.LightProperties)eventControl;
                        if (lightProperties != null)
                        {
                            if (lightRoll.WindowRoll == ExtendedLightProperties.LightProperties.WindowRoll.RollDown)
                            {
                                Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Light properties Roll Down, returning lightProperties.MaximumHeight of " + lightProperties.MaximumHeight.ToString());
                                returnedHeight = lightProperties.MaximumHeight;
                            }
                            else
                            {
                                returnedHeight = lightProperties.MinimumHeight;
                                Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Light properties Roll Up, returning lightProperties.MinimumHeight of " + lightProperties.MinimumHeight.ToString());
                            }
                        }
                        break;
                    case "ModelProperties":
                        ExtendedModelProperties.WindowRollEventArgs modelRoll = (ExtendedModelProperties.WindowRollEventArgs)e;
                        ExtendedModelProperties.ModelProperties modelProperties = (ExtendedModelProperties.ModelProperties)eventControl;
                        if (modelRoll.WindowRoll == ExtendedModelProperties.ModelProperties.WindowRoll.RollDown)
                        {
                            returnedHeight = modelProperties.MaximumHeight;
                            Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Model properties Roll Down, returning modelProperties.MaximumHeight of " + modelProperties.MaximumHeight.ToString());
                        }
                        else
                        {
                            returnedHeight = modelProperties.MinimumHeight;
                            Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Model properties Roll Up, returning modelProperties.MinimumHeight of " + modelProperties.MinimumHeight.ToString());
                        }
                        break;
                    case "BasicNodeProperties":
                        ExtendedNodeProperties.WindowRollEventArgs nodeRoll = (ExtendedNodeProperties.WindowRollEventArgs)e;
                        ExtendedNodeProperties.BasicNodeProperties nodeProperties = (ExtendedNodeProperties.BasicNodeProperties)eventControl;
                        if (nodeRoll.WindowRoll == ExtendedNodeProperties.BasicNodeProperties.WindowRoll.RollDown)
                        {
                            returnedHeight = nodeProperties.MaximumHeight;
                            Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Node properties Roll Down, returning nodeProperties.MaximumHeight of " + nodeProperties.MaximumHeight.ToString());
                        }
                        else
                        {
                            returnedHeight = nodeProperties.MinimumHeight;
                            Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Node properties Roll Up, returning nodeProperties.MinimumHeight of " + nodeProperties.MinimumHeight.ToString());
                        }
                        break;
                    default:
                        Debug.Print("PulsarExtendedProperties.GetHeightOfControl - default switch case - ERROR!!!!");
                        break;
                }
                currentYPosition += returnedHeight + 1;
            }
            else
            {
                currentYPosition += (iteratedControl.Height + 1);
                Debug.Print("PulsarExtendedProperties.GetHeightOfControl - Control that raised the roll event does NOT match the iterative control!!!");
                Debug.Print("PulsarExtendedProperties.GetHeightOfControl - currentYPosition is now " + currentYPosition.ToString() + " after the iterated control height of " + iteratedControl.Height.ToString() + " was added");
            }


        }
    }
}
