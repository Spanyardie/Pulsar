using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Urho;
using WeifenLuo.WinFormsUI.Docking;

namespace Pulsar
{
    public partial class SceneTree : DockContent, IRegisterMessage
    {
        private delegate void ThreadSafeDisplaySceneTreeViewSelect(object sender, TreeNodeMouseClickEventArgs e);
        private delegate void ThreadSafeDisplaySceneElements();
        private delegate void ThreadSafeFormResize(PulsarMessage message);

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
                //remove any existing registration - is there a way to test this?!!!
                _mainApplication.SceneNodeClicked -= MainApplication_SceneNodeClicked;
                //new registration
                _mainApplication.SceneNodeClicked += MainApplication_SceneNodeClicked;

                //add any registrations for messages here
                Registrant registrant = new Registrant
                {
                    Subscriber = this,
                    Type = PulsarMessage.MessageType.NodeNameChanged
                };
                _mainApplication.MessageQueue.RegisterForMessage(registrant);

                registrant = new Registrant
                {
                    Subscriber = this,
                    Type = PulsarMessage.MessageType.AdjustSceneTreeFormWidth
                };
                _mainApplication.MessageQueue.RegisterForMessage(registrant);

                registrant = new Registrant
                {
                    Subscriber = this,
                    Type = PulsarMessage.MessageType.RefreshTreeView
                };
                _mainApplication.MessageQueue.RegisterForMessage(registrant);
            }
        }

        private Camera _mainSceneCamera;
        private Light _mainSceneLight;
        //private StaticModel _scenePlane;
        private PulsarScene _currentScene;
        private string _newNodeName = "";
        private string _oldNodeName = "";

        private Node _previouslySelectedNode = null;
        private readonly Node _nodeNameChanged = null;
        private bool _suppressTreeViewClick = false;

        public PulsarScene CurrentScene
        {
            get
            {
                return _currentScene;
            }
            set
            {
                _currentScene = value;
            }
        }

        public SceneTree()
        {
            InitializeComponent();
            sceneTreeView.NodeMouseClick += SceneTreeView_NodeMouseClick;

        }

        private void MainApplication_SceneNodeClicked(SceneObjectType sceneObjectType, object sceneObject, bool suppressTreeViewClick)
        {
            //if we get a null sceneObject it indicates a reset of the previously selected node and reset of the font on the previously selected item
            if (sceneObject == null)
            {
                if (_previouslySelectedNode != null && !_previouslySelectedNode.IsDeleted)
                {
                    TreeNode[] previousNode = sceneTreeView.Nodes.Find(_previouslySelectedNode.Name, true); //there should only be one!
                    if (previousNode != null)
                    {
                        foreach (TreeNode node in previousNode)
                        {
                            node.NodeFont = new Font("Arial", 8.0f, FontStyle.Regular);
                        }
                    }
                    _previouslySelectedNode = null;
                }
                return;
            }

            Node sceneNode = (Node)sceneObject;

            TreeNodeCollection childNodes = sceneTreeView.Nodes[0].Nodes;

            var treeNodes = childNodes.Find(sceneNode.Name, false);

            _suppressTreeViewClick = suppressTreeViewClick;

            if (!_suppressTreeViewClick)
                SceneTreeView_NodeMouseClick(sceneObject, new TreeNodeMouseClickEventArgs(treeNodes[0], MouseButtons.Left, 1, 0, 0));
        }

        public void DisplaySceneElements()
        {
            if (sceneTreeView.InvokeRequired)
            {
                Urho.Application.InvokeOnMain(DisplaySceneElements);
            }
            else
            {
                if (_currentScene != null)
                {
                    sceneTreeView.Nodes.Clear();

                    //Add a scene node
                    TreeNode sceneRootNode = new TreeNode("Scene - " + _currentScene.SceneName + "   ", 2, 2)
                    {
                        NodeFont = new Font("Arial", 8.0f, FontStyle.Regular)
                    };
                    sceneTreeView.Nodes.Add(sceneRootNode);

                    //Lights and cameras are children of the scene, not in the component list of the scene
                    if (_currentScene.Children.Count > 0)
                    {
                        foreach (Node node in _currentScene.Children)
                        {
                            //don't list any gizmos
                            if (!(node.Name.Contains("gizmo") || node.Name.Contains("wirePlane")))
                            {
                                TreeNode treeNode = new TreeNode
                                {
                                    Text = node.Name + "   ",
                                    Name = node.Name,
                                    NodeFont = new Font("Arial", 8.0f, FontStyle.Regular)
                                };
                                switch (node.Name)
                                {
                                    case var light when (light?.Contains("MainDirectionalLight") == true):
                                        treeNode.Text = (_newNodeName.Length == 0 && _newNodeName.Contains("MainDirectionalLight")) ? _newNodeName + "   " : node.Name + "   ";
                                        treeNode.ImageIndex = 4;
                                        treeNode.SelectedImageIndex = 4;
                                        if (node.Components.Count > 0)
                                        {
                                            _mainSceneLight = node.GetComponent<Light>();

                                        }
                                        break;
                                    case var camera when (camera?.ToLower().Contains("camera") == true):
                                        treeNode.Text = (_newNodeName.Length == 0 && _newNodeName.Contains("camera")) ? _newNodeName + "   " : node.Name + "   ";
                                        treeNode.ImageIndex = 8;
                                        treeNode.SelectedImageIndex = 8;
                                        if (node.Components.Count > 0)
                                        {
                                            _mainSceneCamera = node.GetComponent<Camera>();

                                        }
                                        break;
                                }
                                sceneRootNode.Nodes.Add(treeNode);
                            }
                        }
                    }

                    //expand the root node on first view
                    sceneRootNode.Expand();
                }
            }
        }

        public string OldNodeName
        {
            get
            {
                return _oldNodeName;
            }
            set
            {
                _oldNodeName = value;
            }
        }

        public string NewNodeName
        {
            get
            {
                return _newNodeName;
            }
            set
            {
                _newNodeName = value;
            }
        }

        string IRegisterMessage.RegistrantName()
        {
            return "SceneTree";
        }

        object IRegisterMessage.Registrant()
        {
            return this;
        }

        public void SetNewGizmoName()
        {
            if (_nodeNameChanged != null)
            {
                _nodeNameChanged.Name = _newNodeName + "_gizmo";
                DisplaySceneElements();
            }
        }

        private void FormSceneTree_Load(object sender, EventArgs e)
        {
            Resize += FormSceneTree_Resize;
            FormSceneTree_Resize(null, null);

            sceneTreeView.NodeMouseClick += SceneTreeView_NodeMouseClick;
        }

        private void SceneTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (_suppressTreeViewClick) return;

            if (sceneTreeView.InvokeRequired)
            {
                var d = new ThreadSafeDisplaySceneTreeViewSelect(SceneTreeView_NodeMouseClick);
                sceneTreeView.Invoke(d, new object[] { sender, e });
            }
            else
            {
                //inform the PropertiesInspector that there has been a change
                InformMainToChangeProperties(e.Node.Name);

                //Unlike the scene window, only single selections are possible, this means that
                //if the selection has changed we need to mark the current selected object (if any)
                //for deletion before adding the new selection.
                Node selectedNode = _currentScene.Children.ToList().Find(node => node.Name == e.Node.Name);

                if (selectedNode != null && selectedNode != _previouslySelectedNode)
                {
                    MarkExistingSelectionsForDelete();
                    //now add the new selection
                    AddNewSelectionToSceneList(selectedNode);

                    UpdateTreeViewWithSelection(e.Node);

                    _previouslySelectedNode = selectedNode;
                }
            }
        }

        public void UpdateTreeViewWithSelection(TreeNode selectedTreeNode)
        {
            //first, let's get the node of the previous selection
            if (_previouslySelectedNode != null && !_previouslySelectedNode.IsDeleted)
            {
                TreeNode[] previousNode = sceneTreeView.Nodes.Find(_previouslySelectedNode.Name, true); //there should only be one!
                if (previousNode.Length != 0)
                {
                    //reset the font
                    previousNode[0].NodeFont = new Font("Arial", 8.0f, FontStyle.Regular);
                }
            }

            //now change the font of the existing selection
            if (selectedTreeNode != null)
            {
                selectedTreeNode.NodeFont = new Font("Arial", 8.0f, FontStyle.Italic);
                //and select it
                sceneTreeView.SelectedNode = selectedTreeNode;
            }
        }

        public void AddNewSelectionToSceneList(Node selectedNode)
        {
            if (selectedNode != null)
            {
                Drawable nodeDrawable = selectedNode.GetComponent<StaticModel>();
                if (nodeDrawable != null)
                {
                    SelectedObject newSelectedObject = new SelectedObject()
                    {
                        SelectedNode = selectedNode,
                        Drawable = nodeDrawable,
                        MarkForDelete = false,
                        NodeClickedName = selectedNode.Name.Trim()
                    };
                    _currentScene.SelectedSceneObjects.AddSelectedObject(newSelectedObject);
                }
            }
        }

        public void MarkExistingSelectionsForDelete()
        {
            var selectedObjectList = _currentScene.SelectedSceneObjects.ObjectList;

            if (selectedObjectList != null)
            {
                if (selectedObjectList.Count > 0)
                {
                    foreach (SelectedObject selectedObject in selectedObjectList)
                    {
                        selectedObject.MarkForDelete = true;
                    }
                }
            }
        }

        public void InformMainToChangeProperties(string treeNodeName)
        {
            //send a reset properties view message
            if (_mainApplication != null)
            {
                PulsarMessage pulsarMessage = new PulsarMessage
                {
                    Type = PulsarMessage.MessageType.ResetPropertiesWindow,
                    Iterations = 1
                };
                _mainApplication.MessageQueue.PushMessage(pulsarMessage);
            }

            switch (treeNodeName)
            {
                case var light when (light?.Contains("MainDirectionalLight") == true):
                    SendShowObjectPropertiesMessage(SceneObjectType.Light, _currentScene.GetChild(treeNodeName));
                    break;
                case var camera when (camera?.ToLower().Contains("camera") == true):
                    SendShowObjectPropertiesMessage(SceneObjectType.Camera, _currentScene.GetChild(treeNodeName));
                    break;
                default:
                    SendShowObjectPropertiesMessage(SceneObjectType.Node, _currentScene.GetChild(treeNodeName));
                    break;
            }
        }

        private void SendShowObjectPropertiesMessage(SceneObjectType objectType, object sceneObject)
        {
            if (_mainApplication != null)
            {
                PulsarMessage message = new PulsarMessage
                {
                    Type = PulsarMessage.MessageType.ShowObjectProperties,
                    Iterations = 1
                };

                message.Properties.Add("sceneObjectType", objectType);
                message.Properties.Add("sceneObject", sceneObject);
                message.Properties.Add("externallySet", false);

                if (message != null)
                    _mainApplication.MessageQueue.PushMessage(message);
            }
        }

        private void FormSceneTree_Resize(object sender, EventArgs e)
        {
            sceneTreeView.Top = sceneTreeTools.Size.Height + 1;
            sceneTreeView.Width = Width;
            sceneTreeView.Height = Height - (sceneTreeTools.Height + sceneTreeStatus.Height) - 1;
        }

        public void UpdateTreeViewWithSceneNodes()
        {
            DisplaySceneElements();
        }

        void IRegisterMessage.CallBack(PulsarMessage message)
        {
            if (message != null)
            {
                switch (message.Type)
                {
                    case PulsarMessage.MessageType.NodeNameChanged:
                    case PulsarMessage.MessageType.RefreshTreeView:
                        var d = new ThreadSafeDisplaySceneElements(DisplaySceneElements);
                        sceneTreeView.Invoke(d, Array.Empty<object>());
                        break;
                    case PulsarMessage.MessageType.AdjustSceneTreeFormWidth:
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
                        var newSize = new Size((int)widthProperty, Size.Height);
                        Size = newSize;
                    }
                }
            }
        }
    }
}
