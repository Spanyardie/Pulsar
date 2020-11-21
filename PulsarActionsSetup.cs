using Pulsar.EventArguments;
using Pulsar.Helpers;
using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Forms;
using Urho;
using Urho.Actions;
using static Pulsar.ObjectModel.PulsarAction;

namespace Pulsar
{
    public partial class PulsarActionsSetup : Form
    {
        #region Enumerations and Structs
        private enum EditMode
        {
            NotEditing = 0,
            Editing
        }
        private struct AttributeInfo
        {
            public string PropertyName;
            public ActionType ActionType;
            public ActionDataTypes DataType;
        }
        #endregion

        #region Read only properties
        private readonly PulsarActions _nodeActions;
        private readonly List<PulsarMode> _modeTracking = new List<PulsarMode>();
        #endregion

        #region Controls variables
        //Controls
        private PulsarActionProperty _basicPropertyControl;
        private PulsarActionVector3 _vector3PropertyControl;
        private PulsarActionVector4 _vector4PropertyControl;
        private PulsarActionBezierConfig _bezierConfigControl;
        private PulsarActionTransformSpace _transformSpaceControl;
        private PulsarActionTarget _actionTargetControl;
        #endregion

        #region Private variables
        private PulsarMode _currentMode;
        private Vector2 _lastMousePosition;
        private TreeNode _dragOverNode;
        private CheckBox _retainAction;
        private PulsarAction _pulsarAction;
        private bool _isDirty = false;
        private bool _canDrop = false;
        #endregion

        #region Public accessors
        public Node Node { get; set; }
        public PulsarScene Scene { get; set; }
        #endregion

        #region Constructor
        public PulsarActionsSetup()
        {
            InitializeComponent();

            _nodeActions = new PulsarActions(Node);

            actionsList.MouseDown += ActionsList_MouseDown;
            treeViewActions.DragEnter += TreeViewActions_DragEnter;
            treeViewActions.DragOver += TreeViewActions_DragOver;

            _lastMousePosition = new Vector2();

            SetupActionList(ActionType.Single);
        }
        #endregion

        #region TreeView event handlers
        private void TreeViewActions_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //if we haven't actually moved the mouse since the last call then bail out now
            if (e.X == _lastMousePosition.X && e.Y == _lastMousePosition.Y) return;

            //update the stored mouse position
            _lastMousePosition.X = e.X;
            _lastMousePosition.Y = e.Y;

            //if there are no items in the treeView we can default to being able to drop
            if (treeViewActions.Nodes.Count == 0)
            {
                _canDrop = true;
                return;
            }

            //get the node under the mouse
            System.Drawing.Point mousePoint = new System.Drawing.Point(e.X, e.Y);
            System.Drawing.Point targetPoint = treeViewActions.PointToClient(mousePoint);

            var treeViewNode = treeViewActions.GetNodeAt(targetPoint);
            if (treeViewNode != null)
            {
                ProcessTreeViewNodeDragOver(treeViewNode);
                if (!_canDrop)
                {
                    e.Effect = System.Windows.Forms.DragDropEffects.None;
                }
                else
                {
                    e.Effect = System.Windows.Forms.DragDropEffects.Copy;
                }
            }
            else
            {
                //if we are not adding to an existing node then it should be safe to just add
                _dragOverNode = null;
                _canDrop = true;
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
        }

        private void TreeViewActions_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            DragDropAction dropAsset = new DragDropAction();
            Type type = dropAsset.GetType();

            DragDropAction data = (DragDropAction)e.Data.GetData(type);

            if (data != null)
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;
        }

        private void TreeViewActions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //based on action type, use reflection to find the required fields to display
            ResetControlsEnabled();

            PulsarListBoxItem item = (PulsarListBoxItem)actionsList.SelectedItem;

            TreeNode selectedNode = e.Node;

            PulsarAction parentAction = null;

            //does this node have a parent (i.e is it an action that takes one or more actions
            if (selectedNode.Parent != null)
            {
                var parentNode = selectedNode.Parent;
                //only one more level of nesting to reach root if this node has a parent also
                if (parentNode.Parent != null)
                {
                    var rootAction = _nodeActions.ActionList.Find(grandParent => grandParent.ID == parentNode.Parent.Tag.ToString());
                    if (rootAction != null)
                    {
                        parentAction = rootAction.GetActionSet().Find(rootAct => rootAct.ID == parentNode.Tag.ToString());
                    }
                }
                else
                {
                    parentAction = _nodeActions.ActionList.Find(parent => parent.ID == selectedNode.Parent.Tag.ToString());
                }
                if (parentAction != null)
                {
                    _pulsarAction = parentAction.GetActionSet().Find(act => act.ID == selectedNode.Tag.ToString());
                }
            }
            else
            {
                //_pulsarAction is the currently selected action
                _pulsarAction = _nodeActions.ActionList.Find(act => act.ID == selectedNode.Tag.ToString());
            }

            ActionTypes actionType = ActionTypes.LastItem;

            if (_pulsarAction != null)
            {
                actionType = _pulsarAction.PulsarActionType;
                lblActionProperties.Text = actionType.ToString() + " properties:";
            }

            //specify the type of the class we want to retrieve the properties of
            Type type = typeof(PulsarAction);

            //array to hold the names of the properties of the class which have the specific action type attribute
            AttributeInfo[] propertiesWithActionType = Array.Empty<AttributeInfo>();

            //get a list of the properties of this class
            PropertyInfo[] elements = type.GetProperties();

            if (elements.Length != 0)
            {
                for (int index = 0; index < elements.Length; index++)
                {
                    //get a list of all attributes associated with this property
                    ActionAttribute[] attributeList = (ActionAttribute[])Attribute.GetCustomAttributes(elements[index], typeof(ActionAttribute));

                    if (attributeList.Length > 0)
                    {
                        //are any of the attributes a match for the action type?
                        var attribute = attributeList.ToList().Find(attr => attr.Type == actionType);
                        if (attribute != null)
                        {
                            //add this element to the array
                            Array.Resize(ref propertiesWithActionType, propertiesWithActionType.Length + 1);
                            var dataType = GetDataType(elements[index].PropertyType);
                            AttributeInfo info = new AttributeInfo
                            {
                                PropertyName = elements[index].Name,
                                ActionType = GetActionType(attribute.Type),
                                DataType = dataType
                            };
                            propertiesWithActionType[propertiesWithActionType.Length - 1] = info;
                        }
                    }
                }

                //grab the retain checkbox
                _retainAction = chkRetainAction;

                //if we have found any properties then make those enabled
                if (propertiesWithActionType.Length > 0)
                {
                    SetControlsEnabled(propertiesWithActionType);
                }
                actionPropertiesPanel.Controls.Add(_retainAction);

                //push data from the _pulsarAction to the displayed controls
                AddExistingDataToControls(_pulsarAction, _pulsarAction.PulsarActionType);

                _retainAction.Visible = true;
                _retainAction.Checked = _pulsarAction.RetainAction;
            }
        }

        private void TreeViewActions_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            DragDropAction dropAsset = new DragDropAction();
            Type type = dropAsset.GetType();

            DragDropAction data = (DragDropAction)e.Data.GetData(type);

            if (data != null)
            {
                //if the DragOver event processing shows we can safely add this action then proceed
                if (_canDrop)
                {
                    Add_Click(null, null);
                }
            }
        }

        private void TreeViewActions_QueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e)
        {
            e.Action = System.Windows.Forms.DragAction.Continue;
        }
        #endregion

        #region Control creation methods
        private void CreateNodeControl(AttributeInfo property)
        {
            PulsarActionTarget actionTarget = new PulsarActionTarget
            {
                Name = "cboActionTarget",
                Scene = Scene,
                PropertyName = property.PropertyName
            };
            actionTarget.InitialiseComboContent();
            _actionTargetControl = actionTarget;

            HookActionTargetControlEvents();

            actionPropertiesPanel.Controls.Add(actionTarget);
        }

        private void CreateTransformSpaceControl(AttributeInfo property)
        {
            PulsarActionTransformSpace transformSpace = new PulsarActionTransformSpace
            {
                Name = "group" + property.PropertyName
            };
            actionPropertiesPanel.Controls.Add(transformSpace);

            _transformSpaceControl = transformSpace;

            HookTransformSpaceControlEvents();
        }

        private void CreateVector4Control(AttributeInfo property)
        {
            PulsarActionVector4 pulsarActionVector4 = new PulsarActionVector4
            {
                Name = "group" + property.PropertyName,
                Heading = SplitStringByUpperCase(property.PropertyName),
                PropertyName = property.PropertyName
            };
            actionPropertiesPanel.Controls.Add(pulsarActionVector4);
            _vector4PropertyControl = pulsarActionVector4;

            HookVector4ControlEvents();
        }

        private void CreateVector3Control(AttributeInfo property)
        {
            PulsarActionVector3 pulsarActionVector3 = new PulsarActionVector3
            {
                Name = "group" + property.PropertyName,
                Heading = SplitStringByUpperCase(property.PropertyName),
                PropertyName = property.PropertyName
            };
            actionPropertiesPanel.Controls.Add(pulsarActionVector3);
            _vector3PropertyControl = pulsarActionVector3;

            HookVector3ControlEvents();
        }

        private void CreateBezierConfigControl(AttributeInfo property)
        {
            PulsarActionBezierConfig pulsarActionBezierConfig = new PulsarActionBezierConfig
            {
                Name = "group" + property.PropertyName,
                PropertyName = property.PropertyName
            };
            actionPropertiesPanel.Controls.Add(pulsarActionBezierConfig);
            _bezierConfigControl = pulsarActionBezierConfig;

            HookBezierControlEvents();
        }

        private void CreateBasicDataTypeControl(AttributeInfo property)
        {
            PulsarActionProperty actionProperty = new PulsarActionProperty
            {
                Type = property.DataType,
                Label = SplitStringByUpperCase(property.PropertyName),
                Name = "txt" + property.PropertyName,
                PropertyName = property.PropertyName
            };
            actionPropertiesPanel.Controls.Add(actionProperty);
            _basicPropertyControl = actionProperty;

            HookBasicControlEvents();
        }
        #endregion

        #region Control hook methods
        private void HookBasicControlEvents()
        {
            if(_basicPropertyControl != null)
                _basicPropertyControl.ActionPropertyChanged += BasicPropertyControl_ActionPropertyChanged;
        }

        private void HookVector3ControlEvents()
        {
            if(_vector3PropertyControl != null)
                _vector3PropertyControl.ActionVector3Changed += Vector3PropertyControl_ActionVector3Changed;
        }

        private void HookVector4ControlEvents()
        {
            if(_vector4PropertyControl != null)
                _vector4PropertyControl.ActionVector4PropertyChanged += Vector4PropertyControl_ActionVector4PropertyChanged;
        }

        private void HookBezierControlEvents()
        {
            if(_bezierConfigControl != null)
                _bezierConfigControl.ActionBezierChanged += BezierConfigControl_ActionBezierChanged;
        }

        private void HookTransformSpaceControlEvents()
        {
            if(_transformSpaceControl != null)
                _transformSpaceControl.ActionTransformSpaceChanged += TransformSpaceControl_ActionTransformSpaceChanged;
        }

        private void HookActionTargetControlEvents()
        {
            if(_actionTargetControl != null)
                _actionTargetControl.ActionTargetChanged += ActionTargetControl_ActionTargetChanged;
        }
        #endregion

        #region Control UnHook methods
        private void UnHookControlEvents()
        {
            UnHookBasicControlEvents();
            UnHookVector3ControlEvents();
            UnHookVector4ControlEvents();
            UnHookBezierControlEvents();
            UnHookTransformSpaceControlEvents();
            UnHookActionTargetControlEvents();
        }

        private void UnHookBasicControlEvents()
        {
            if(_basicPropertyControl != null)
                _basicPropertyControl.ActionPropertyChanged -= BasicPropertyControl_ActionPropertyChanged;
        }

        private void UnHookVector3ControlEvents()
        {
            if (_vector3PropertyControl != null)
                _vector3PropertyControl.ActionVector3Changed -= Vector3PropertyControl_ActionVector3Changed;
        }

        private void UnHookVector4ControlEvents()
        {
            if (_vector4PropertyControl != null)
                _vector4PropertyControl.ActionVector4PropertyChanged -= Vector4PropertyControl_ActionVector4PropertyChanged;
        }

        private void UnHookBezierControlEvents()
        {
            if (_bezierConfigControl != null)
                _bezierConfigControl.ActionBezierChanged -= BezierConfigControl_ActionBezierChanged;
        }

        private void UnHookTransformSpaceControlEvents()
        {
            if (_transformSpaceControl != null)
                _transformSpaceControl.ActionTransformSpaceChanged -= TransformSpaceControl_ActionTransformSpaceChanged;
        }

        private void UnHookActionTargetControlEvents()
        {
            if (_actionTargetControl != null)
                _actionTargetControl.ActionTargetChanged -= ActionTargetControl_ActionTargetChanged;
        }
        #endregion

        #region Control content changed event handlers
        private void BasicPropertyControl_ActionPropertyChanged(object sender, EventArgs e)
        {
            //use reflection with the arg PropertyName to update the relevant property in the current _pulsarAction
            ActionPropertyEventArgs args = (ActionPropertyEventArgs)e;
            if(args != null)
            {
                ReflectionHelper.SetPropValue(_pulsarAction, args.PropertyName, args.Data);
                _isDirty = true;
                btnComplete.Visible = true;
            }
        }

        private void Vector3PropertyControl_ActionVector3Changed(object sender, EventArgs e)
        {
            ActionVector3EventArgs args = (ActionVector3EventArgs)e;
            if(args != null)
            {
                ReflectionHelper.SetPropValue(_pulsarAction, args.PropertyName, args.Vector);
                _isDirty = true;
                btnComplete.Visible = true;
            }
        }

        private void Vector4PropertyControl_ActionVector4PropertyChanged(object sender, EventArgs e)
        {
            ActionVector4EventArgs args = (ActionVector4EventArgs)e;
            if(args != null)
            {
                ReflectionHelper.SetPropValue(_pulsarAction, args.PropertyName, args.Vector);
                _isDirty = true;
                btnComplete.Visible = true;
            }
        }

        private void BezierConfigControl_ActionBezierChanged(object sender, EventArgs e)
        {
            ActionBezierConfigEventArgs args = (ActionBezierConfigEventArgs)e;
            if(args != null)
            {
                BezierConfig bezierConfig = new BezierConfig
                {
                    ControlPoint1 = args.ControlPoint1,
                    ControlPoint2 = args.ControlPoint2,
                    EndPosition = args.EndPoint
                };
                ReflectionHelper.SetPropValue(_pulsarAction, args.PropertyName, bezierConfig);
                _isDirty = true;
                btnComplete.Visible = true;
            }
        }

        private void TransformSpaceControl_ActionTransformSpaceChanged(object sender, EventArgs e)
        {
            ActionTransformSpaceEventArgs args = (ActionTransformSpaceEventArgs)e;
            if(args != null)
            {
                ReflectionHelper.SetPropValue(_pulsarAction, args.PropertyName, args.TransformSpace);
                _isDirty = true;
                btnComplete.Visible = true;
            }
        }

        private void ActionTargetControl_ActionTargetChanged(object sender, EventArgs e)
        {
            ActionTargetEventArgs args = (ActionTargetEventArgs)e;
            if(args != null)
            {
                ReflectionHelper.SetPropValue(_pulsarAction, args.PropertyName, args.Target);
                _isDirty = true;
                btnComplete.Visible = true;
            }
        }

        private void RetainAction_CheckChanged(object sender, EventArgs e)
        {
            if (_pulsarAction != null)
            {
                _pulsarAction.RetainAction = chkRetainAction.Checked;
                _isDirty = true;
                btnComplete.Visible = true;
            }
        }
        #endregion

        #region Controls enabled methods
        private void SetControlsEnabled(AttributeInfo[] propertiesWithActionType)
        {
            UnHookControlEvents();

            actionPropertiesPanel.Controls.Clear();

            var propertyList = propertiesWithActionType.ToList();
            if (propertyList != null)
            {
                foreach (AttributeInfo property in propertyList)
                {
                    switch (property.DataType)
                    {
                        case ActionDataTypes.Float:
                        case ActionDataTypes.Int:
                        case ActionDataTypes.String:
                        case ActionDataTypes.UInt:
                            CreateBasicDataTypeControl(property);
                            break;
                        case ActionDataTypes.BezierConfig:
                            CreateBezierConfigControl(property);
                            break;
                        case ActionDataTypes.Vector3:
                            CreateVector3Control(property);
                            break;
                        case ActionDataTypes.PulsarVector4RGBA:
                            CreateVector4Control(property);
                            break;
                        case ActionDataTypes.TransformSpace:
                            CreateTransformSpaceControl(property);
                            break;
                        case ActionDataTypes.Node:
                            CreateNodeControl(property);
                            break;
                    }
                }
            }
        }

        private void ResetControlsEnabled()
        {
            actionPropertiesPanel.Controls.Clear();
        }
        #endregion

        #region Entity action methods
        private void GetEntityActions()
        {
            if (Node != null)
            {
                BaseEntity baseEntity = Node.GetComponent<BaseEntity>();
                _nodeActions.BaseEntity = baseEntity;
                if (baseEntity.Actions.ActionList.Count > 0)
                {
                    foreach (PulsarAction action in baseEntity.Actions.ActionList)
                    {
                        _nodeActions.AddAction(action);
                    }
                }
            }
        }

        private void ShowEntityActions()
        {
            if(_nodeActions.ActionList.Count > 0)
            {
                treeViewActions.Nodes.Clear();
                List<PulsarAction> actions = _nodeActions.ActionList;
                foreach (PulsarAction action in actions)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = action.PulsarActionType.ToString(),
                        Tag = action.ID
                    };
                    if (action.GetActionSet().Count > 0)
                    {
                        foreach(PulsarAction timeAction in action.GetActionSet())
                        {
                            TreeNode childNode = new TreeNode
                            {
                                Text = timeAction.PulsarActionType.ToString(),
                                Tag = timeAction.ID
                            };
                            //I think this is the extent of the nesting, if this becomes unwieldy then needs refactoring
                            if(timeAction.GetActionSet().Count > 0)
                            {
                                foreach(PulsarAction nestedAction in timeAction.GetActionSet())
                                {
                                    TreeNode nestedNode = new TreeNode
                                    {
                                        Text = nestedAction.PulsarActionType.ToString(),
                                        Tag = nestedAction.ID
                                    };
                                    childNode.Nodes.Add(nestedNode);
                                }
                            }
                            newNode.Nodes.Add(childNode);
                        }
                    }
                    treeViewActions.Nodes.Add(newNode);
                }
            }
            else
            {
                //ensure treeview nodes are empty as we have no nodes
                treeViewActions.Nodes.Clear();
            }
        }

        private void SetEntityActions()
        {
            if (Node != null)
            {
                BaseEntity baseEntity = Node.GetComponent<BaseEntity>();
                if (baseEntity != null)
                {
                    baseEntity.Actions.ActionList.Clear();
                    foreach (PulsarAction action in _nodeActions.ActionList)
                    {
                        action.Initialise();
                        baseEntity.Actions.ActionList.Add(action);
                    }
                    //after a recreation of the actionList we can deem the actions saved and no longer dirty
                    _isDirty = false;
                }
            }
        }
        #endregion

        #region Control data assign methods
        private void AddExistingDataToControls(PulsarAction pulsarAction, ActionTypes action)
        {
            switch(action)
            {
                case ActionTypes.Amplitude:
                    AddDataAmplitude(pulsarAction);
                    break;
                case ActionTypes.BezierBy:
                    AddDataBezier(pulsarAction);
                    break;
                case ActionTypes.BezierTo:
                    AddDataBezier(pulsarAction);
                    break;
                case ActionTypes.Blink:
                    AddDataBlink(pulsarAction);
                    break;
                case ActionTypes.DelayTime:
                    AddDataDelayTime(pulsarAction);
                    break;
                case ActionTypes.EaseElastic:
                    AddDataEaseElastic(pulsarAction);
                    break;
                case ActionTypes.EaseElasticIn:
                    AddDataEaseElasticIn(pulsarAction);
                    break;
                case ActionTypes.EaseElasticInOut:
                    AddDataEaseElasticInOut(pulsarAction);
                    break;
                case ActionTypes.EaseElasticOut:
                    AddDataEaseElasticOut(pulsarAction);
                    break;
                case ActionTypes.EaseIn:
                    AddDataEaseIn(pulsarAction);
                    break;
                case ActionTypes.EaseInOut:
                    AddDataEaseInOut(pulsarAction);
                    break;
                case ActionTypes.EaseOut:
                    AddDataEaseOut(pulsarAction);
                    break;
                case ActionTypes.FadeIn:
                    AddDataFadeIn(pulsarAction);
                    break;
                case ActionTypes.FadeOut:
                    AddDataFadeOut(pulsarAction);
                    break;
                case ActionTypes.FadeTo:
                    AddDataFadeTo(pulsarAction);
                    break;
                //case ActionTypes.Hide:
                //    AddDataHide(pulsarAction);
                //    break;
                case ActionTypes.JumpBy:
                    AddDataJumpBy(pulsarAction);
                    break;
                case ActionTypes.JumpTo:
                    AddDataJumpTo(pulsarAction);
                    break;
                case ActionTypes.MoveBy:
                    AddDataMoveBy(pulsarAction);
                    break;
                case ActionTypes.MoveTo:
                    AddDataMoveTo(pulsarAction);
                    break;
                case ActionTypes.Place:
                    AddDataPlace(pulsarAction);
                    break;
                case ActionTypes.Repeat:
                    AddDataRepeat(pulsarAction);
                    break;
                //case ActionTypes.RepeatForever:
                //    AddDataRepeatForever(pulsarAction);
                //    break;
                case ActionTypes.RotateAroundBy:
                    AddDataRotateAroundBy(pulsarAction);
                    break;
                case ActionTypes.RotateBy:
                    AddDataRotateBy(pulsarAction);
                    break;
                case ActionTypes.RotateTo:
                    AddDataRotateTo(pulsarAction);
                    break;
                case ActionTypes.ScaleBy:
                    AddDataScaleBy(pulsarAction);
                    break;
                case ActionTypes.ScaleTo:
                    AddDataScaleTo(pulsarAction);
                    break;
                //case ActionTypes.Show:
                //    AddDataShow(pulsarAction);
                //    break;
                case ActionTypes.Spawn:
                    AddDataSpawn(pulsarAction);
                    break;
                case ActionTypes.TargettedAction:
                    AddDataTargettedAction(pulsarAction);
                    break;
                case ActionTypes.TintBy:
                    AddDataTintBy(pulsarAction);
                    break;
                case ActionTypes.TintTo:
                    AddDataTintTo(pulsarAction);
                    break;
                //case ActionTypes.ToggleVisibility:
                //    AddDataToggleVisibility(pulsarAction);
                //    break;
                //case ActionTypes.Tween:
                //    AddDataTween(pulsarAction);
                //    break;

            }
        }

        private void AddDataTintTo(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];
            PulsarActionVector4 deltaRGBA = (PulsarActionVector4)actionPropertiesPanel.Controls.Find("groupDeltaRGBA", true)[0];
            PulsarActionProperty materialIndex = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtMaterialIndex", true)[0];
            PulsarActionProperty shaderParameterName = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtShaderParameterName", true)[0];

            duration.Value = pulsarAction.Duration.ToString();
            deltaRGBA.Vector4 = pulsarAction.DeltaRGBA;
            materialIndex.Value = pulsarAction.MaterialIndex.ToString();
            shaderParameterName.Value = pulsarAction.ShaderParameterName;
        }

        private void AddDataTintBy(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];
            PulsarActionVector4 deltaRGBA = (PulsarActionVector4)actionPropertiesPanel.Controls.Find("groupDeltaRGBA", true)[0];
            PulsarActionProperty materialIndex = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtMaterialIndex", true)[0];
            PulsarActionProperty shaderParameterName = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtShaderParameterName", true)[0];

            duration.Value = pulsarAction.Duration.ToString();
            deltaRGBA.Vector4 = pulsarAction.DeltaRGBA;
            materialIndex.Value = pulsarAction.MaterialIndex.ToString();
            shaderParameterName.Value = pulsarAction.ShaderParameterName;
        }

        private void AddDataTargettedAction(PulsarAction pulsarAction)
        {

        }

        private void AddDataSpawn(PulsarAction pulsarAction)
        {

        }

        private void AddDataScaleTo(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            PulsarActionVector3 vector3 = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupScale", true)[0];

            duration.Value = pulsarAction.Duration.ToString();

            vector3.Vector3 = pulsarAction.Scale;
        }

        private void AddDataScaleBy(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            PulsarActionVector3 vector3 = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupScale", true)[0];

            duration.Value = pulsarAction.Duration.ToString();

            vector3.Vector3 = pulsarAction.Scale;
        }

        private void AddDataRepeat(PulsarAction pulsarAction)
        {
            PulsarActionProperty numberOfTimes = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtNumberOfTimes", true)[0];

            numberOfTimes.Value = pulsarAction.NumberOfTimes.ToString();
        }

        private void AddDataPlace(PulsarAction pulsarAction)
        {
            PulsarActionVector3 vector3 = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupPosition", true)[0];

            vector3.Vector3 = pulsarAction.Position;
        }

        private void AddDataFadeTo(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            PulsarActionProperty opacity = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtOpacity", true)[0];

            duration.Value = pulsarAction.Duration.ToString();
            opacity.Value = pulsarAction.Opacity.ToString();
        }

        private void AddDataFadeOut(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            duration.Value = pulsarAction.Duration.ToString();
        }

        private void AddDataFadeIn(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            duration.Value = pulsarAction.Duration.ToString();
        }

        private void AddDataEaseOut(PulsarAction pulsarAction)
        {
            PulsarActionProperty rate = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtRate", true)[0];

            rate.Value = pulsarAction.Rate.ToString();
        }

        private void AddDataEaseInOut(PulsarAction pulsarAction)
        {
            PulsarActionProperty rate = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtRate", true)[0];

            rate.Value = pulsarAction.Rate.ToString();
        }

        private void AddDataEaseElasticOut(PulsarAction pulsarAction)
        {
            PulsarActionProperty period = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtPeriod", true)[0];

            period.Value = pulsarAction.Period.ToString();
        }

        private void AddDataEaseElasticInOut(PulsarAction pulsarAction)
        {
            PulsarActionProperty period = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtPeriod", true)[0];

            period.Value = pulsarAction.Period.ToString();
        }

        private void AddDataEaseElasticIn(PulsarAction pulsarAction)
        {
            PulsarActionProperty period = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtPeriod", true)[0];

            period.Value = pulsarAction.Period.ToString();
        }

        private void AddDataEaseElastic(PulsarAction pulsarAction)
        {
            PulsarActionProperty period = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtPeriod", true)[0];

            period.Value = pulsarAction.Period.ToString();
        }

        private void AddDataDelayTime(PulsarAction pulsarAction)
        {
            PulsarActionProperty delayTime = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDelayTime", true)[0];

            delayTime.Value = pulsarAction.DelayTime.ToString();
        }

        private void AddDataBlink(PulsarAction pulsarAction)
        {
            PulsarActionProperty numberOfTimes = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtNumberOfTimes", true)[0];

            numberOfTimes.Value = pulsarAction.NumberOfTimes.ToString();
        }

        private void AddDataEaseIn(PulsarAction pulsarAction)
        {
            PulsarActionProperty rate = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtRate", true)[0];

            rate.Value = pulsarAction.Rate.ToString();
        }

        private void AddDataRotateBy(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];
            if (duration != null)
            {
                duration.Value = pulsarAction.Duration.ToString();
            }

            PulsarActionVector3 delta = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupDeltaXYZ", true)[0];
            if (delta != null)
            {
                delta.Vector3 = pulsarAction.DeltaXYZ;
            }
        }

        private void AddDataRotateTo(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];
            if (duration != null)
            {
                duration.Value = pulsarAction.Duration.ToString();
            }

            PulsarActionVector3 delta = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupDeltaXYZ", true)[0];
            if (delta != null)
            {
                delta.Vector3 = pulsarAction.DeltaXYZ;
            }
        }

        private void AddDataJumpTo(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];
            PulsarActionVector3 vector3 = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupPosition", true)[0];
            PulsarActionProperty height = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtHeight", true)[0];
            PulsarActionProperty jumps = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtJumps", true)[0];

            duration.Value = pulsarAction.Duration.ToString();
            vector3.Vector3 = pulsarAction.Position;
            height.Value = pulsarAction.Height.ToString();
            jumps.Value = pulsarAction.Jumps.ToString();
        }

        private void AddDataJumpBy(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];
            PulsarActionVector3 vector3 = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupPosition", true)[0];
            PulsarActionProperty height = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtHeight", true)[0];
            PulsarActionProperty jumps = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtJumps", true)[0];

            duration.Value = pulsarAction.Duration.ToString();
            vector3.Vector3 = pulsarAction.Position;
            height.Value = pulsarAction.Height.ToString();
            jumps.Value = pulsarAction.Jumps.ToString();
        }

        private void AddDataMoveBy(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            PulsarActionVector3 vector3 = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupPosition", true)[0];

            duration.Value = pulsarAction.Duration.ToString();

            vector3.Vector3 = pulsarAction.Position;
        }

        private void AddDataRotateAroundBy(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];
            if (duration != null)
            {
                duration.Value = pulsarAction.Duration.ToString();
            }

            PulsarActionVector3 point = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupPoint", true)[0];
            if(point != null)
            {
                point.Vector3 = pulsarAction.Point;
            }

            PulsarActionVector3 delta = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupDeltaXYZ", true)[0];
            if(delta != null)
            {
                delta.Vector3 = pulsarAction.DeltaXYZ;
            }

            PulsarActionTransformSpace pulsarActionTransformSpace = (PulsarActionTransformSpace)actionPropertiesPanel.Controls.Find("groupTransformSpace", true)[0];
            if(pulsarActionTransformSpace != null)
            {
                pulsarActionTransformSpace.TransFormSpace = pulsarAction.TransformSpace;
            }
        }

        private void AddDataAmplitude(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            PulsarActionProperty amplitutde = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtAmplitude", true)[0];

            if (duration != null)
            {
                duration.Value = pulsarAction.Duration.ToString();
            }
            if(amplitutde != null)
            {
                amplitutde.Value = pulsarAction.Amplitude.ToString();
            }
        }

        private void AddDataMoveTo(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            PulsarActionVector3 vector3  = (PulsarActionVector3)actionPropertiesPanel.Controls.Find("groupPosition", true)[0];

            duration.Value = pulsarAction.Duration.ToString();

            vector3.Vector3 = pulsarAction.Position;
        }

        private void AddDataBezier(PulsarAction pulsarAction)
        {
            PulsarActionProperty duration = (PulsarActionProperty)actionPropertiesPanel.Controls.Find("txtDuration", true)[0];

            PulsarActionBezierConfig bezierConfig = (PulsarActionBezierConfig)actionPropertiesPanel.Controls.Find("groupBezierConfig", true)[0];

            duration.Value = pulsarAction.Duration.ToString();

            bezierConfig.ControlPoint1 = pulsarAction.BezierConfig.ControlPoint1;

            bezierConfig.ControlPoint2 = pulsarAction.BezierConfig.ControlPoint2;

            bezierConfig.EndPoint = pulsarAction.BezierConfig.EndPosition;
        }

        //private void AddDataShow(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataRepeatForever(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataHide(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseSineOut(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseSineInOut(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataSineIn(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseBounceOut(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseBounceInOut(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseBounceIn(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseBackOut(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseBackInOut(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataEaseBackIn(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataTween(PulsarAction pulsarAction)
        //{

        //}

        //private void AddDataToggleVisibility(PulsarAction pulsarAction)
        //{

        //}
        #endregion

        #region Radio button control change event handlers
        private void Single_CheckChanged(object sender, EventArgs e)
        {
            if(_currentMode != null)
            {
                _currentMode.Mode = ActionType.Single;
            }
            else
            {
                _currentMode = new PulsarMode
                {
                    Mode = ActionType.Single
                };
            }
            SetupActionList(ActionType.Single);
        }

        private void SinglePlus_CheckChanged(object sender, EventArgs e)
        {
            //current mode will definitely have been created by now
            _currentMode.Mode = ActionType.SingleRequiresAdditionalAction;
            _modeTracking.Add(_currentMode);
            SetupActionList(ActionType.SingleRequiresAdditionalAction);
        }

        private void SinglePlusPlus_CheckChanged(object sender, EventArgs e)
        {
            _currentMode.Mode = ActionType.SingleRequiresAdditionalActions;
            _modeTracking.Add(_currentMode);
            SetupActionList(ActionType.SingleRequiresAdditionalActions);
        }

        private void Sequence_CheckChanged(object sender, EventArgs e)
        {
            _currentMode.Mode = ActionType.Sequence;
            _modeTracking.Add(_currentMode);
            SetupActionList(ActionType.Sequence);
        }

        private void Parallel_CheckChanged(object sender, EventArgs e)
        {
            _currentMode.Mode = ActionType.Parallel;
            _modeTracking.Add(_currentMode);
            SetupActionList(ActionType.Parallel);
        }
        #endregion

        #region Action related methods
        private void ActionsList_MouseDown(object sender, MouseEventArgs e)
        {
            //what have we clicked on, if anything
            PulsarListBoxItem listBoxItem = (PulsarListBoxItem)actionsList.SelectedItem;
            if (listBoxItem != null)
            {
                DragDropAction dropAsset = new DragDropAction
                {
                    Type = listBoxItem.Type,
                    ActionType = listBoxItem.ActionType,
                    Text = listBoxItem.Text
                };
                System.Windows.Forms.DataObject data = new System.Windows.Forms.DataObject(dropAsset);

                actionsList.DoDragDrop(data, System.Windows.Forms.DragDropEffects.Copy);
            }
        }

        private void SetupActionList(ActionType type)
        {
            actionsList.Items.Clear();

            int actionTypeslength = (int)ActionTypes.LastItem;
            for (var index = 0; index < actionTypeslength; index++)
            {
                var acts = (ActionTypes)index;
                if (GetActionType((ActionTypes)index) == type)
                {
                    PulsarListBoxItem item = new PulsarListBoxItem
                    {
                        Type = type,
                        ActionType = acts,
                        Text = acts.ToString()
                    };
                    actionsList.Items.Add(item);
                }
            }
            actionsList.SelectedIndex = 0;
        }

        private PulsarAction CreateAction(ActionTypes selectedIndex, Node node, bool retainAction)
        {
            PulsarAction createdAction = new PulsarAction(node, retainAction)
            {
                PulsarActionType = selectedIndex,
                Type = GetActionType(selectedIndex)
            };

            return createdAction;
        }

        private static ActionType GetActionType(ActionTypes type)
        {
            ActionType actionType = ActionType.Unset;

            switch (type)
            {
                case ActionTypes.BezierBy:
                case ActionTypes.BezierTo:
                case ActionTypes.Blink:
                case ActionTypes.DelayTime:
                case ActionTypes.FadeIn:
                case ActionTypes.FadeOut:
                case ActionTypes.FadeTo:
                case ActionTypes.Hide:
                case ActionTypes.JumpBy:
                case ActionTypes.JumpTo:
                case ActionTypes.MoveBy:
                case ActionTypes.MoveTo:
                case ActionTypes.Place:
                case ActionTypes.RotateAroundBy:
                case ActionTypes.RotateBy:
                case ActionTypes.RotateTo:
                case ActionTypes.ScaleBy:
                case ActionTypes.ScaleTo:
                case ActionTypes.Show:
                case ActionTypes.TintBy:
                case ActionTypes.TintTo:
                case ActionTypes.ToggleVisibility:
                    //case ActionTypes.Tween:
                    actionType = ActionType.Single;
                    break;

                case ActionTypes.EaseBackIn:
                case ActionTypes.EaseBackInOut:
                case ActionTypes.EaseBackOut:
                case ActionTypes.EaseBounceIn:
                case ActionTypes.EaseBounceInOut:
                case ActionTypes.EaseBounceOut:
                case ActionTypes.EaseElastic:
                case ActionTypes.EaseElasticIn:
                case ActionTypes.EaseElasticInOut:
                case ActionTypes.EaseElasticOut:
                case ActionTypes.EaseExponentialIn:
                case ActionTypes.EaseExponentialInOut:
                case ActionTypes.EaseExponentialOut:
                case ActionTypes.EaseIn:
                case ActionTypes.EaseInOut:
                case ActionTypes.EaseOut:
                case ActionTypes.EaseSineIn:
                case ActionTypes.EaseSineInOut:
                case ActionTypes.EaseSineOut:
                case ActionTypes.Repeat:
                case ActionTypes.TargettedAction:
                    actionType = ActionType.SingleRequiresAdditionalAction;
                    break;

                case ActionTypes.Parallel:
                    actionType = ActionType.Parallel;
                    break;

                case ActionTypes.RepeatForever:
                case ActionTypes.Spawn:
                    actionType = ActionType.SingleRequiresAdditionalActions;
                    break;

                case ActionTypes.Sequence:
                    actionType = ActionType.Sequence;
                    break;
            }

            return actionType;
        }
        #endregion

        #region Miscellaneous methods
        private ActionDataTypes GetDataType(Type propertyType)
        {
            ActionDataTypes returnDataType = ActionDataTypes.Null;

            if (propertyType != null)
            {
                switch (propertyType.Name)
                {
                    case "Single":
                        returnDataType = ActionDataTypes.Float;
                        break;
                    case "String":
                        returnDataType = ActionDataTypes.String;
                        break;
                    case "Int32":
                        returnDataType = ActionDataTypes.Int;
                        break;
                    case "UInt32":
                        returnDataType = ActionDataTypes.UInt;
                        break;
                    case "Vector3":
                        returnDataType = ActionDataTypes.Vector3;
                        break;
                    case "PulsarVector4RGBA":
                        returnDataType = ActionDataTypes.PulsarVector4RGBA;
                        break;
                    case "BezierConfig":
                        returnDataType = ActionDataTypes.BezierConfig;
                        break;
                    case "TransformSpace":
                        returnDataType = ActionDataTypes.TransformSpace;
                        break;
                    case "Node":
                        returnDataType = ActionDataTypes.Node;
                        break;
                    case "Color":
                        returnDataType = ActionDataTypes.Color;
                        break;
                    case "Action":
                        returnDataType = ActionDataTypes.Action;
                        break;
                }
            }

            return returnDataType;
        }

        private void ProcessTreeViewNodeDragOver(TreeNode treeViewNode)
        {
            //default to not being able to drop
            _canDrop = false;
            _dragOverNode = null;

            //update the canDrop flag depending on what we find in the action specified by the node
            //any existing treeViewNodes will have their Tag property set to the ID of the action

            PulsarAction action = null;
            //if the treeViewNode has a parent then get the action from the actionSet 
            if (treeViewNode.Parent != null)
            {
                var parentAction = _nodeActions.ActionList.Find(node => node.ID == treeViewNode.Parent.Tag.ToString());
                if (parentAction != null)
                    action = parentAction.GetActionSet().Find(act => act.ID == treeViewNode.Tag.ToString());
            }
            else
            {
                action = _nodeActions.ActionList.Find(node => node.ID == treeViewNode.Tag.ToString());
            }
            if (action != null)
            {
                switch (action.Type)
                {
                    case ActionType.Single:
                        //cannot drop another action on a single type
                        _canDrop = false;
                        break;
                    case ActionType.SingleRequiresAdditionalAction:
                        //cannot drop if the single action has already been provided
                        //the user will have to remove the single action if they want to replace with new
                        if (action.GetActionSet().Count == 0)
                        {
                            _canDrop = true;
                            _dragOverNode = treeViewNode;
                        }
                        break;
                    case ActionType.SingleRequiresAdditionalActions:
                        if (action.GetActionSet().Count < 2)
                        {
                            _canDrop = true;
                            _dragOverNode = treeViewNode;
                        }
                        break;
                    case ActionType.Sequence:
                    case ActionType.Parallel:
                        //both sequence and parallel can have any number of additions
                        _canDrop = true;
                        _dragOverNode = treeViewNode;
                        break;
                    default:
                        _canDrop = false;
                        break;
                }
            }
        }

        private static string SplitStringByUpperCase(string source)
        {
            string[] splitString = Regex.Split(source, @"(?<!^)(?=[A-Z])");

            string returnString = "";

            for (int index = 0; index < splitString.Length; index++)
            {
                returnString += splitString[index] + " ";
            }

            return returnString.Trim();
        }

        private void Complete_Click(object sender, EventArgs e)
        {
            if(_isDirty)
            {
                //replace the baseEntity action list with the _pulsarActions list
                SetEntityActions();
                _isDirty = false;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (Node != null)
            {
                BaseEntity baseEntity = Node.GetComponent<BaseEntity>();

                if (baseEntity != null)
                {
                    switch (_currentMode.Mode)
                    {
                        case ActionType.Single:
                            PulsarAction action = CreateAction(((PulsarListBoxItem)actionsList.SelectedItem).ActionType, Node, false);
                            //are we adding a single to a SingleRequiresAdditionalAction node
                            if (_dragOverNode != null)
                            {
                                PulsarAction singleAdditionalAction = null;
                                //get the action from the node dropped onto
                                //if this listBoxItem has a parent we need to get that and examine its actionSet
                                if (_dragOverNode.Parent != null)
                                {
                                    var parentAction = _nodeActions.ActionList.Find(par => par.ID == _dragOverNode.Parent.Tag.ToString());
                                    if (parentAction != null)
                                    {
                                        singleAdditionalAction = parentAction.GetActionSet().Find(sing => sing.ID == _dragOverNode.Tag.ToString());
                                    }
                                }
                                else
                                {
                                    singleAdditionalAction = _nodeActions.ActionList.Find(act => act.ID == _dragOverNode.Tag.ToString());
                                }

                                if (singleAdditionalAction != null)
                                {
                                    singleAdditionalAction.AddActionToSet(action);
                                }
                            }
                            else
                            {
                                _nodeActions.ActionList.Add(action);
                            }
                            ShowEntityActions();
                            break;

                        case ActionType.SingleRequiresAdditionalAction:
                            PulsarAction singleAction = CreateAction(((PulsarListBoxItem)actionsList.SelectedItem).ActionType, Node, false);
                            //are we adding to a sequence or a parallel?
                            if (_dragOverNode != null)
                            {
                                PulsarAction seqParAction = _nodeActions.ActionList.Find(act => act.ID == _dragOverNode.Tag.ToString());
                                if (seqParAction != null)
                                {
                                    seqParAction.AddActionToSet(singleAction);
                                }
                            }
                            else
                            {
                                _nodeActions.ActionList.Add(singleAction);
                            }

                            ShowEntityActions();
                            break;

                        case ActionType.Sequence:
                            PulsarAction sequenceAction = CreateAction(((PulsarListBoxItem)actionsList.SelectedItem).ActionType, Node, false);
                            _nodeActions.ActionList.Add(sequenceAction);
                            ShowEntityActions();
                            break;

                        case ActionType.Parallel:
                            PulsarAction parallelAction = CreateAction(((PulsarListBoxItem)actionsList.SelectedItem).ActionType, Node, false);
                            _nodeActions.ActionList.Add(parallelAction);
                            ShowEntityActions();
                            break;
                    }
                }
            }
        }
        #endregion

        #region Form event handlers
        private void PulsarActionsSetup_Shown(object sender, EventArgs e)
        {
            if (Node != null)
            {
                lblEntity.Text = Node.Name;
                lblEntityActions.Text = Node.Name + " actions:";

                GetEntityActions();
            }

            ShowEntityActions();

            _currentMode = new PulsarMode
            {
                Mode = ActionType.Single
            };

            lblMode.Text = "Current mode: " + _currentMode.Mode.ToString();
        }

        private void PulsarActionsSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(_isDirty)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Changes have been made to this entity's actions. Would you like to commit these changes?", "Un-saved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    //save actions to the entity
                    SetEntityActions();
                }
            }
        }
        #endregion

        #region Entity Action Context menu functions
        private void ContextEntityAction_MouseClick(object sender, MouseEventArgs e)
        {
            //if it's a right click show the context menu
            if (e.Button == MouseButtons.Right)
            {
                contextEntityAction.Show(new System.Drawing.Point(e.X, e.Y));
            }
        }

        private void ContextEntityActionCancel_Click(object sender, EventArgs e)
        {
            contextEntityAction.Hide();
        }

        private void ContextEntityActionEdit_Click(object sender, EventArgs e)
        {

        }

        private void ContextEntityActionDelete_Click(object sender, EventArgs e)
        {
            //is there a current selection?
            if(treeViewActions.SelectedNode != null)
            {
                TreeNode selectedNode = treeViewActions.SelectedNode;

                DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure you want to remove this action?", "Delete Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    //delete the item from the actionList - the treeNode Tag contains the action ID
                    if (selectedNode.Parent != null)
                    {
                        var parentAction = _nodeActions.ActionList.Find(par => par.ID == selectedNode.Parent.Tag.ToString());
                        parentAction.RemoveFromActionSet(selectedNode.Tag.ToString());
                    }
                    else
                    {
                        _nodeActions.RemoveActionByID(treeViewActions.SelectedNode.Tag.ToString());
                    }
                    //also need to update the actions of the entity so they match
                    SetEntityActions();
                    //update the action display
                    ShowEntityActions();
                    //finally remove any leftover properties
                    ResetControlsEnabled();
                }
            }
            else
            {
                if (treeViewActions.Nodes.Count > 0)
                {
                    //there is no current selection, inform the user
                    System.Windows.Forms.MessageBox.Show("Please select an action to delete!", "Selection required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ContextEntityAction_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(treeViewActions.Nodes.Count == 0)
            {
                //there are no nodes so we don't want to display the menu
                e.Cancel = true;
            }
        }
        #endregion
    }
}
