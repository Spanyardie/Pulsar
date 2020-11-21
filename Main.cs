using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using System;
using System.Windows.Forms;
using static Pulsar.Helpers.Dragger;

namespace Pulsar
{
    public partial class Main : Form, IRegisterMessage
    {
        private readonly SceneTree _sceneTree;
        private readonly PulsarExtendedProperties _pulsarExtendedProperties;

        private readonly PulsarAssets _pulsarAssets;
        private readonly RenderScene _renderScene;

        private PulsarApplication _application;
        private PulsarScene _scene;

        private delegate void ThreadSafeDisplaySceneElements();

        public Main()
        {
            InitializeComponent();

            _sceneTree = new SceneTree(sceneTreeView);

            _renderScene = new RenderScene(renderSurface);

            _pulsarExtendedProperties = new PulsarExtendedProperties(propertiesPanel);

            _pulsarAssets = new PulsarAssets(modelsListView, materialsListView, animationsListView, fontsListView, texturesListView, assetsWatcher);
            if (_pulsarAssets != null)
                _pulsarAssets.LoadAssets();

            Shown += Pulsar_Shown;
            FormClosing += Main_FormClosing;
            FormClosed += Main_FormClosed;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_application != null)
                _application.MessageQueue.StopMessagingQueue();
            if (_renderScene.RenderSurface.Application.IsActive)
                _renderScene.RenderSurface.Stop();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Pulsar_Shown(object sender, EventArgs e)
        {

            PulsarApplication pulsarApplication = (PulsarApplication)_renderScene.DrawingSurface.Application;
            if (pulsarApplication != null)
            {
                _sceneTree.MainApplication = pulsarApplication;
                _scene = pulsarApplication.DisplayScene;

                _pulsarExtendedProperties.MainApplication = pulsarApplication;
                _pulsarExtendedProperties.Scene = _scene;
            }

            DisplaySceneInTree();

            Resize += Main_Resize;

            //registration for messages
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.ClearSelectedObjects
            };
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            var renderSceneWidth = (int)(0.67252 * Width);
            var sceneTreeWidth = (int)(0.12345 * Width);
            var propertiesInspectorWidth = (int)(0.19059 * Width);

            PulsarMessage message = new PulsarMessage()
            {
                Type = PulsarMessage.MessageType.AdjustRenderFormWidth,
                Iterations = 1
            };
            message.Properties.Add("width", renderSceneWidth);
            _application.MessageQueue.PushMessage(message);

            message = new PulsarMessage()
            {
                Type = PulsarMessage.MessageType.AdjustSceneTreeFormWidth,
                Iterations = 1
            };
            message.Properties.Add("width", sceneTreeWidth);
            _application.MessageQueue.PushMessage(message);

            message = new PulsarMessage()
            {
                Type = PulsarMessage.MessageType.AdjustPropertiesFormWidth,
                Iterations = 1
            };
            message.Properties.Add("width", propertiesInspectorWidth);
            _application.MessageQueue.PushMessage(message);

        }

        public void DisplaySceneInTree()
        {
            if (sceneTreeView.InvokeRequired)
            {
                //If an invoke is required because we are running on a different thread
                //we make an instance of the delegate here, once Invoke is called this method
                //will be called again, this time in the context of the called thread
                //when that occurs, InvokeRequired will then have a value of 'false'
                //and we can then update the scenetree with the latest scene information
                var d = new ThreadSafeDisplaySceneElements(DisplaySceneInTree);
                sceneTreeView.Invoke(d, Array.Empty<object>());
            }
            else
            {
                //grab the scene
                _application = (PulsarApplication)_renderScene.DrawingSurface.Application;

                if (_application != null)
                {
                    _scene = _application.DisplayScene;
                    _sceneTree.CurrentScene = _scene;
                    _sceneTree.DisplaySceneElements();
                }
            }
        }

        private void BoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Urho.Application.InvokeOnMain(CreateBox);
        }

        public void CreateBox()
        {
            _scene.CreateBox();
            DisplaySceneInTree();
        }

        private void SphereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Urho.Application.InvokeOnMain(CreateSphere);
        }

        public void CreateSphere()
        {
            _scene.CreateSphere();
            DisplaySceneInTree();
        }

        private void CylinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Urho.Application.InvokeOnMain(CreateCylinder);
        }

        public void CreateCylinder()
        {
            _scene.CreateCylinder();
            DisplaySceneInTree();
        }

        private void ConeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Urho.Application.InvokeOnMain(CreateCone);
        }

        public void CreateCone()
        {
            _scene.CreateCone();
            DisplaySceneInTree();
        }

        private void WirePlaneStripMenuItem_Click(object sender, EventArgs e)
        {
            Urho.Application.InvokeOnMain(CreateWirePlane);
        }

        public void CreateWirePlane()
        {
            _scene.CreateWirePlane();
            DisplaySceneInTree();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Find the selected object in the scene
            if(_scene != null)
            {
                if(_scene.SelectedSceneObjects.SelectedObjectCount > 0)
                {
                    foreach(SelectedObject selectedObject in _scene.SelectedSceneObjects.ObjectList)
                    {
                        if (!selectedObject.SelectedNode.IsDeleted)
                        {
                            selectedObject.MarkForDelete = true;
                            selectedObject.RemoveFromScene = true;
                            //scene tree to be updated
                            DisplaySceneInTree();
                            //Reset the properties window
                            PulsarMessage pulsarMessage = new PulsarMessage
                            {
                                Type = PulsarMessage.MessageType.ResetPropertiesWindow,
                                Iterations = 1
                            };
                            _application.MessageQueue.PushMessage(pulsarMessage);
                        }
                    }
                    //Remove selected items
                    PulsarMessage message = new PulsarMessage
                    {
                        Type = PulsarMessage.MessageType.ClearSelectedObjects,
                        Iterations = 1
                    };
                    _application.MessageQueue.PushMessage(message);
                }
            }
        }

        public string RegistrantName()
        {
            return "Main";
        }

        public void CallBack(PulsarMessage message)
        {
            if(message != null)
            {
                switch(message.Type)
                {
                    case PulsarMessage.MessageType.ClearSelectedObjects:
                        _scene.SelectedSceneObjects.Clear();
                        break;
                }
            }
        }

        public object Registrant()
        {
            return this;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _application.MessageQueue.StopMessagingQueue();
            _renderScene.RenderSurface.Stop();
            Application.Exit();
        }

        public RenderScene GetRenderSceneForm()
        {
            return _renderScene;
        }

        private void RenderSceneObjectTranslate_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            if (item != null)
            {
                ClearCheck(item);
                ResetButtonImages();
                item.Image = Properties.Resources.translateActive;
                ToolStripButton button = (ToolStripButton)sender;
                if (button != null)
                {
                    button.Checked = true;
                }
            }
            UpdateDraggerType(DragType.Translate);
        }

        private void RenderSceneObjectRotate_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            if (item != null)
            {
                ClearCheck(item);
                ResetButtonImages();
                item.Image = Properties.Resources.rotateActive;
                ToolStripButton button = (ToolStripButton)sender;
                if (button != null)
                {
                    button.Checked = true;
                }
            }
            UpdateDraggerType(DragType.Rotate);
        }

        private void RenderSceneObjectScale_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            if (item != null)
            {
                ClearCheck(item);
                ResetButtonImages();
                item.Image = Properties.Resources.scaleActive;
                ToolStripButton button = (ToolStripButton)sender;
                if (button != null)
                {
                    button.Checked = true;
                }
            }
            UpdateDraggerType(DragType.Scale);
        }

        private void UpdateDraggerType(DragType dragType)
        {
            if (_application != null)
            {
                _application.DragType = dragType;
            }
        }

        private void ClearCheck(ToolStripItem excludeItem)
        {
            foreach (ToolStripItem item in renderSceneTools.Items)
            {
                ToolStripButton button = (ToolStripButton)item;
                if (button != null && button != excludeItem)
                {
                    if (button.Checked)
                    {
                        button.Checked = false;
                    }
                }
            }
        }

        private void ResetButtonImages()
        {
            foreach (ToolStripItem item in renderSceneTools.Items)
            {
                ToolStripButton button = (ToolStripButton)item;
                if (button != null)
                {
                    switch (button.Name)
                    {
                        case "renderSceneObjectTranslate":
                            button.Image = Properties.Resources.translate;
                            break;
                        case "renderSceneObjectRotate":
                            button.Image = Properties.Resources.rotate;
                            break;
                        case "renderSceneObjectScale":
                            button.Image = Properties.Resources.scale;
                            break;
                    }
                }
            }
        }
    }
}
