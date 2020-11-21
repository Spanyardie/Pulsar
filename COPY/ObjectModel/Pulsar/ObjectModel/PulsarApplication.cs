using Pulsar.Helpers;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using Pulsar.ObjectModel.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Instrumentation;
using System.Windows.Forms;
using Urho;
using Urho.Gui;
using static Pulsar.Helpers.Dragger;

namespace Pulsar.ObjectModel
{
    public class PulsarApplication : Urho.Application
    {
        public MessageQueue MessageQueue { get; set; }

        private PulsarScene _displayScene;
        public PulsarScene DisplayScene
        {
            get
            {
                return _displayScene;
            }
            set
            {
                _displayScene = value;
            }
        }

        private List<Form> _forms;
        public List<Form> ApplicationForms 
        { 
            get
            {
                return _forms;
            }
            set
            {
                _forms = value;
            }
        }

        public PulsarApplication(ApplicationOptions options) : base(options)
        {
            MessageQueue = new MessageQueue();
            ApplicationForms = new List<Form>();
            _input = Input;
        }
        private bool _inDesign = true;
        private Text _hudText;
        private bool _treeviewNodeClicked = false;
        private string _nodeClickedOnName;
        private Viewport _mainViewPort;
        private Dragger _dragger;
        private DragType _dragType = DragType.Translate;
        private readonly Input _input;

        public delegate void NodeClickedEventHandler(SceneObjectType sceneObjectType, object sceneObject, bool suppressTreeViewClick);
        public event NodeClickedEventHandler SceneNodeClicked;

        public DragType DragType
        {
            get
            {
                return _dragType;
            }
            set
            {
                _dragType = value;
            }
        }

        public bool InDesign
        {
            get
            {
                return _inDesign;
            }

            set
            {
                _inDesign = value;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);

            var markedForDelete = DisplayScene.SelectedSceneObjects.ObjectList.Find(node => node.MarkForDelete == true);
            if (markedForDelete != null)
            {
                if (!markedForDelete.SelectedNode.IsDeleted)
                {
                    //is this an ACTUAL deletion from the scene
                    if (markedForDelete.RemoveFromScene)
                    {
                        var node = markedForDelete.SelectedNode;
                        if (node != null)
                        {
                            var gizmo = DisplayScene.GetChild(node.Name + "_gizmo");
                            try
                            {
                                if (gizmo != null)
                                    DisplayScene.RemoveChild(gizmo);
                            }
                            catch
                            {
                                //Debug.Print("An access violation occurred here attempting to remove '" + node.Name + "_gizmo'\n" + ex.Message);
                            }

                            DisplayScene.RemoveChild(node);

                            PulsarMessage pulsarMessage = new PulsarMessage
                            {
                                Type = PulsarMessage.MessageType.RefreshTreeView,
                                Iterations = 1
                            };
                            MessageQueue.PushMessage(pulsarMessage);
                        }
                    }
                    else
                    {
                        PulsarModel pulsarModel = markedForDelete.SelectedNode?.GetComponent<PulsarModel>();
                        if (pulsarModel != null)
                        {
                            BaseEntity baseEntity = pulsarModel.GetBaseEntity();
                            if (baseEntity != null)
                            {
                                baseEntity.UnSelect();
                                DisplayScene.SelectedSceneObjects.ObjectList.Remove(markedForDelete);
                            }
                        }
                    }
                }
            }

            //TEST INPUT TO MOVE CAMERA AROUND

            Input input = Input;

            //get the camera to look at the box
            PulsarCamera camera = DisplayScene.SceneCamera;
            Node boxNode = DisplayScene.BoxNode;

            //camera.Camera.Node.LookAt(Vector3.Zero, Vector3.Up);

            if (_hudText == null)
            {
                _hudText = new Text();
                _hudText.SetColor(Color.Black);
                _hudText.SetFont(ResourceCache.GetFont("Fonts/arial.ttf", false), 24);

                UI.Root.AddChild(_hudText);
            }

            Node hitNode = null;


            if (Raycast(1000.0f, camera.Camera, out Vector3 hitPos, out Drawable hitDrawable))
            {
                if (hitDrawable != null)
                {
                    hitNode = hitDrawable.Node;
                    if (hitNode != null)
                    {
                        //exclude the wireFrame
                        if (!hitNode.Name.Contains("wirePlane"))
                        {
                            _hudText.Value = "Hit - " + hitNode.Name;
                            BoundingBox bounds = hitDrawable.WorldBoundingBox;
                            DisplayScene.SceneDebugRenderer.AddBoundingBox(bounds, Color.Red);
                        }
                    }
                }
            }

            // Movement speed as world units per second
            const float moveSpeed = 10.0f;
            // Read WASD keys and move the camera scene node to the
            // corresponding direction if they are pressed
            Node cameraNode = camera.Camera.Node;
            if (cameraNode != null)
            {
                if (input.GetKeyDown(Key.S))
                    cameraNode.Translate(Vector3.UnitY * moveSpeed * timeStep, TransformSpace.Local);
                if (input.GetKeyDown(Key.W))
                    cameraNode.Translate(new Vector3(0.0f, -1.0f, 0.0f) * moveSpeed * timeStep, TransformSpace.Local);
                if (input.GetKeyDown(Key.D))
                    cameraNode.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * moveSpeed * timeStep, TransformSpace.Local);
                if (input.GetKeyDown(Key.A))
                    cameraNode.Translate(Vector3.UnitX * moveSpeed * timeStep, TransformSpace.Local);

                camera.BaseEntity.Position = cameraNode.Position;
                camera.BaseEntity.Rotation = cameraNode.Rotation.ToEulerAngles();
            }

            ProcessMouseEvents();

            CheckForSelection(hitNode, hitDrawable, _treeviewNodeClicked);

            DrawSelectedObjects();

            if (_dragger.IsDragging)
            {
                DoGizmoTransform(null);
            }
            else if (hitNode != null)
            {
                if (hitNode.Name.Contains("GizmoNode"))
                {
                    DoGizmoTransform(hitNode);
                }
            }
        }

        private void DrawSelectedObjects()
        {
            if (DisplayScene.SelectedSceneObjects.SelectedObjectCount > 0)
            {
                foreach (SelectedObject selectedObject in DisplayScene.SelectedSceneObjects.ObjectList)
                {
                    if (selectedObject != null)
                    {
                        if (!selectedObject.Drawable.IsDeleted)
                        {
                            BoundingBox bounds = selectedObject.Drawable.WorldBoundingBox;
                            DisplayScene.SceneDebugRenderer.AddBoundingBox(bounds, Color.Red);
                            PulsarModel pulsarModel = selectedObject.SelectedNode.GetComponent<PulsarModel>();
                            if (pulsarModel != null)
                            {
                                var baseEntity = pulsarModel.GetBaseEntity();
                                var gizmoNode = DisplayScene.GetChild(selectedObject.SelectedNode.Name + "_gizmo");
                                if (baseEntity != null)
                                {
                                    if (!baseEntity.IsSelected)
                                    {
                                        baseEntity.SetAsSelected();
                                    }
                                    else
                                    {
                                        //make sure the gizmo node has the correct orientation based on type
                                        switch (DragType)
                                        {
                                            case DragType.Translate:
                                                if (gizmoNode != null)
                                                {
                                                    gizmoNode.Rotation = new Quaternion(0, 0, 0);
                                                }
                                                break;
                                            case DragType.Rotate:
                                            case DragType.Scale:
                                                if (gizmoNode != null)
                                                {
                                                    gizmoNode.Rotation = selectedObject.SelectedNode.Rotation;
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckForSelection(Node hitNode, Drawable hitDrawable, bool treeViewSelection = false)
        {
            bool multipleSelect = true;

            BaseEntity baseEntity = null;

            if (Input.GetMouseButtonPress(MouseButton.Left))
            {
                _treeviewNodeClicked = false;
                //if the node is null, we should remove all items from the list

                if (hitNode != null)
                {
                    if (!hitNode.Name.Contains("wirePlane"))
                    {
                        if (hitNode.Components.Count > 0)
                        {
                            if (!hitNode.Name.ToLower().Contains("gizmo"))
                            {
                                //baseEntity from component
                                baseEntity = hitNode.GetComponent<BaseEntity>();
                                if (baseEntity == null) return;
                            }
                            else
                            {
                                //baseEntity from gizmo (parent/parent/baseentity)
                                var parent1 = hitNode.Parent;
                                if(parent1 != null)
                                {
                                    var parent2 = parent1.Parent;
                                    if(parent2 != null)
                                    {
                                        if(parent2.Components.Count > 0)
                                        {
                                            var parent2Gizmo = parent2.GetComponent<Gizmo>();
                                            if(parent2Gizmo != null)
                                            {
                                                baseEntity = parent2Gizmo.BaseEntity;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else { return; }

                        if (baseEntity != null)
                        {
                            if (!baseEntity.IsSelected)
                            {
                                //if the user has pressed Shift button, then we need to add to the selected object list, otherwise
                                //we should clear the list first
                                if (!Input.GetKeyDown(Key.Shift))
                                {
                                    multipleSelect = false;
                                    DisplayScene.SelectedSceneObjects.ResetGizmosAndMaterials();
                                    DisplayScene.SelectedSceneObjects.Clear();
                                    SendResetPropertiesWindowMessage();
                                }

                                if (!hitNode.Name.Contains("wirePlane"))
                                {
                                    SelectedObject selectedObject = new SelectedObject
                                    {
                                        SelectedNode = hitNode,
                                        Drawable = hitDrawable
                                    };

                                    if (baseEntity != null)
                                    {
                                        baseEntity.SetAsSelected();
                                        //send a message to show the object properties
                                        PulsarMessage message = new PulsarMessage()
                                        {
                                            Type = PulsarMessage.MessageType.ShowObjectProperties,
                                            Iterations = 1
                                        };
                                        message.Properties.Add("sceneObjectType", GetSceneObjectType(hitNode));
                                        message.Properties.Add("sceneObject", hitNode);
                                        message.Properties.Add("externallySet", false);
                                        if (message != null)
                                            MessageQueue.PushMessage(message);
                                    }

                                    SceneNodeClicked?.Invoke(SceneObjectType.Node, hitNode, multipleSelect);

                                    DisplayScene.SelectedSceneObjects.AddSelectedObject(selectedObject);
                                }
                            }
                        }
                    }
                    else
                    {
                        DisplayScene.SelectedSceneObjects.ResetGizmosAndMaterials();
                        DisplayScene.SelectedSceneObjects.Clear();
                        //Using this event and passing null to inform the scenetree form that any previously selected node should be cleared (as wirePanel was hit and we've reset all gizmos and materials)
                        SceneNodeClicked?.Invoke(SceneObjectType.Node, null, false);
                        //Send message to properties window to clear its display
                        SendResetPropertiesWindowMessage();
                    }
                }
                else
                {
                    DisplayScene.SelectedSceneObjects.ResetGizmosAndMaterials();
                    DisplayScene.SelectedSceneObjects.Clear();
                    //Using this event and passing null to inform the scenetree form that any previously selected node should be cleared (as nothing was hit and we've reset all gizmos and materials)
                    SceneNodeClicked?.Invoke(SceneObjectType.Node, null, false);
                    SendResetPropertiesWindowMessage();
                }
            }
        }

        private SceneObjectType GetSceneObjectType(Node hitNode)
        {
            SceneObjectType type;

            switch (hitNode.Name)
            {
                case var camera when (camera?.Contains("camera") == true):
                    type = SceneObjectType.Camera;
                    break;
                case var light when (light?.Contains("Light") == true):
                    type = SceneObjectType.Light;
                    break;
                default:
                    type = SceneObjectType.Node;
                    break;
            }
            return type;
        }

        private void SendResetPropertiesWindowMessage()
        {
            PulsarMessage message = new PulsarMessage()
            {
                Type = PulsarMessage.MessageType.ResetPropertiesWindow,
                Iterations = 1
            };

            if (message != null)
                MessageQueue.PushMessage(message);
        }

        public void ItemSelectedFromTreeView(Node sceneNode, Node previousNode, Drawable nodeModel, bool treeviewClicked = false, string nodeClickedName = "")
        {
            _treeviewNodeClicked = treeviewClicked;

            _nodeClickedOnName = nodeClickedName.Trim();

            SelectedObject selectedObject = new SelectedObject
            {
                SelectedNode = sceneNode,
                Drawable = nodeModel,
                NodeClickedName = nodeClickedName
            };
            if (!DisplayScene.SelectedSceneObjects.ObjectList.Contains(selectedObject))
            {
                DisplayScene.SelectedSceneObjects.AddSelectedObject(selectedObject);
            }

            SelectedObject markedForDeleteNode = DisplayScene.SelectedSceneObjects.ObjectList.Find(node => node.SelectedNode == previousNode);
            if (markedForDeleteNode != null)
            {
                markedForDeleteNode.MarkForDelete = true;
            }
        }

        protected override void Setup()
        {
            base.Setup();

            ResourceCache.AddResourceDir("C:\\Users\\sebastian.quelcutti\\OneDrive - collinson365\\Documents\\Visual Studio 2019\\Temp_Projects\\WindowsFormsApp1\\Assets", 0);

            _dragger = new Dragger();
        }

        protected override void Start()
        {
            base.Start();

            Engine.PostRenderUpdate += Engine_PostRenderUpdate;

            DisplayScene = new PulsarScene("MainScene", this)
            {
                InDesign = _inDesign
            };

            //create a default plane to work on
            DisplayScene.CreateWirePlane();

            SetViewport();

            UI.Cursor = new Urho.Gui.Cursor(Context)
            {
                Visible = true
            };
        }

        private void Engine_PostRenderUpdate(PostRenderUpdateEventArgs obj)
        {
            DrawDebugLight();
        }

        private void DrawDebugLight()
        {
            //find the main scene light
            if (DisplayScene != null)
            {
                PulsarLight mainLight = DisplayScene.SceneLight;
                if (mainLight != null)
                {
                    //get the debugger renderer
                    var renderer = DisplayScene.SceneDebugRenderer;
                    if (renderer != null)
                    {
                        renderer.AddLine(mainLight.Light.Node.Position, mainLight.Light.Frustum.Vertices[4], Color.Red, true);
                        renderer.AddLine(mainLight.Light.Node.Position, mainLight.Light.Frustum.Vertices[5], Color.Red, true);
                        renderer.AddLine(mainLight.Light.Node.Position, mainLight.Light.Frustum.Vertices[6], Color.Red, true);
                        renderer.AddLine(mainLight.Light.Node.Position, mainLight.Light.Frustum.Vertices[7], Color.Red, true);
                    }
                }
            }
        }

        protected override void Stop()
        {
            base.Stop();
        }

        private void SetViewport()
        {
            _mainViewPort = new Viewport(Context, DisplayScene, DisplayScene.SceneCamera.Camera, null);
            _mainViewPort.SetClearColor(Color.White);

            Renderer.SetViewport(0, _mainViewPort);
        }

        private bool Raycast(float maxDistance, Camera camera, out Vector3 hitPos, out Drawable hitDrawable)
        {
            hitDrawable = null;
            hitPos = new Vector3();

            IntVector2 pos = UI.CursorPosition;

            // Check the cursor is visible and there is no UI element in front of the cursor
            if (!UI.Cursor.Visible || UI.GetElementAt(pos, true) != null)
                return false;

            var graphics = Graphics;
            Ray cameraRay = camera.GetScreenRay((float)pos.X / graphics.Width, (float)pos.Y / graphics.Height);

            // Pick only geometry objects, not eg. zones or lights, only get the first (closest) hit
            var result = DisplayScene.SceneOctree.RaycastSingle(cameraRay, RayQueryLevel.Triangle, maxDistance, DrawableFlags.Geometry, 0x7fffffff);

            if (result != null)
            {
                //do we return this or, if selected, the gizmo?
                hitPos = result.Value.Position;
                hitDrawable = result.Value.Drawable;

                PulsarModel pulsarModel = result.Value.Node.GetComponent<PulsarModel>();

                if (pulsarModel != null)
                {
                    BaseEntity baseEntity = pulsarModel.GetBaseEntity();
                    if (baseEntity != null)
                    {
                        if (baseEntity.IsSelected && baseEntity.HasGizmo)
                        {
                            var drawableViewMask = hitDrawable.ViewMask;
                            hitDrawable.ViewMask = 0x80000000; //exclude from raycasts
                                                               //perform a second raycast with the entity's viewmask, this allows us to exclude the basentity and raycast onto the gizmos
                            var gizmoFind = DisplayScene.SceneOctree.RaycastSingle(cameraRay, RayQueryLevel.Triangle, maxDistance, DrawableFlags.Geometry, 0x7fffffff);
                            if (gizmoFind != null && !gizmoFind.Value.Node.Name.Contains("wirePlane"))
                            {
                                hitPos = gizmoFind.Value.Position;
                                hitDrawable.ViewMask = drawableViewMask;
                                hitDrawable = gizmoFind.Value.Node.GetComponent<StaticModel>();
                            }
                            else
                            {
                                hitDrawable.ViewMask = drawableViewMask;
                            }
                        }
                    } 
                }
                return true;
            }

            return false;
        }

        private void DoGizmoTransform(Node selectedNode)
        {
            string objectName = "";
            bool leftButtonPressed = Input.GetMouseButtonDown(MouseButton.Left);

            if (selectedNode != null)
            {
                if (!_dragger.IsDragging)
                {
                    if (leftButtonPressed)
                    {
                        //get the main gizmo node (holds the three nodes which represent the axes)
                        var mainGizmo = selectedNode.Parent.Parent;
                        var axisName = selectedNode.Name.Substring(0, 1);
                        //get the entity that this gizmo is associated with
                        var entityName = mainGizmo.Name.Split(new char[] { '_' });
                        if (entityName.Length > 0)
                        {
                            objectName = entityName[0];
                        }
                        if (!String.IsNullOrEmpty(objectName))
                        {
                            var selectedObject = DisplayScene.GetChild(objectName);
                            //get the current gizmo mode
                            var gizmoComponent = mainGizmo.GetComponent<Gizmo>();
                            if (gizmoComponent != null)
                            {
                                switch (_dragType)
                                {
                                    case DragType.Translate:
                                        DoGizmoTranslate(selectedObject, mainGizmo, axisName);
                                        break;
                                    case DragType.Rotate:
                                        DoGizmoRotate(selectedObject, mainGizmo, axisName);
                                        break;
                                    case DragType.Scale:
                                        DoGizmoScale(selectedObject, mainGizmo, axisName);
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (leftButtonPressed && _dragger.IsDragging)
                    {
                        _dragger.MoveNodes(Input.MouseMove);
                    }
                    else
                    {
                        _dragger.IsDragging = false;
                        //Debug.Print("PuilsarApplication.DoGizmoTransform - Dragging has stopped, sending 'DraggingStopped' message");
                        SendDraggingStoppedMessage();
                    }
                }
            }
            else
            {
                if (leftButtonPressed)
                {
                    if (_dragger.IsDragging)
                    {
                        _dragger.MoveNodes(Input.MouseMove);
                    }
                    else
                    {
                        _dragger.IsDragging = false;
                        //Debug.Print("PuilsarApplication.DoGizmoTransform - Left button is pressed but dragging has stopped, sending 'DraggingStopped' message");
                        SendDraggingStoppedMessage();
                    }
                }
                else
                {
                    _dragger.IsDragging = false;
                    //Debug.Print("PuilsarApplication.DoGizmoTransform - Left button is NOT pressed, dragging has stopped, sending 'DraggingStopped' message");
                    SendDraggingStoppedMessage();
                }
            }
        }

        private void SendDraggingStoppedMessage()
        {
            PulsarMessage pulsarMessage = new PulsarMessage
            {
                Type = PulsarMessage.MessageType.DraggingStopped,
                Iterations = 1
            };
            MessageQueue.PushMessage(pulsarMessage);
        }

        private void DoGizmoTranslate(Node mainObject, Node mainGizmoNode, string axisName)
        {
            InitialiseDragger(mainObject, mainGizmoNode);
            _dragger.Type = DragType.Translate;
            _dragger.DraggingAxis = GetDragAxisFromAxisName(axisName);
        }

        private void DoGizmoRotate(Node mainObject, Node mainGizmoNode, string axisName)
        {
            InitialiseDragger(mainObject, mainGizmoNode);
            _dragger.Type = DragType.Rotate;
            _dragger.DraggingAxis = GetDragAxisFromAxisName(axisName);
        }

        private void DoGizmoScale(Node mainObject, Node mainGizmoNode, string axisName)
        {
            InitialiseDragger(mainObject, mainGizmoNode);
            _dragger.Type = DragType.Scale;
            _dragger.DraggingAxis = GetDragAxisFromAxisName(axisName);
        }

        public Dragger GetDragger()
        {
            return _dragger;
        }

        private void InitialiseDragger(Node mainObject, Node mainGizmoNode)
        {
            _dragger.Application = this;
            _dragger.Scene = DisplayScene;

            _dragger.IsDragging = true;

            _dragger.MainGizmoNode = mainGizmoNode;
            _dragger.MainNode = mainObject;
            _dragger.SurfaceDimensions = _mainViewPort.View.ViewSize;
        }

        private DragAxis GetDragAxisFromAxisName(string axisName)
        {
            if (axisName == "x")
            {
                return Dragger.DragAxis.X;
            }
            else if (axisName == "y")
            {
                return Dragger.DragAxis.Y;
            }
            else
            {
                return Dragger.DragAxis.Z;
            }
        }

        private void ProcessMouseEvents()
        {
            //zoom
            //start by getting the main camera
            if (DisplayScene != null)
            {
                var camera = DisplayScene.GetMainCameraNode();
                if (camera != null)
                {
                    if(_input != null)
                    {
                        var mouseDelta = _input.MouseMoveWheel;
                        if(mouseDelta > 0)
                        {
                            // zooming in
                            PulsarMessage pulsarMessage = new PulsarMessage
                            {
                                Type = PulsarMessage.MessageType.MouseWheelZoomIn,
                                Iterations = 1
                            };
                            MessageQueue.PushMessage(pulsarMessage);
                        }
                        else
                        {
                            // zooming out
                            PulsarMessage pulsarMessage = new PulsarMessage
                            {
                                Type = PulsarMessage.MessageType.MouseWheelZoomOut,
                                Iterations = 1
                            };
                            MessageQueue.PushMessage(pulsarMessage);
                        }
                    }
                }
            }
        }
    }
}

