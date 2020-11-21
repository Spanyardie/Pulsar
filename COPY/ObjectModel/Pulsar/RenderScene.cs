using Pulsar.Helpers;
using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Urho;
using Urho.Extensions.WinForms;
using WeifenLuo.WinFormsUI.Docking;
using static Pulsar.Helpers.Dragger;

namespace Pulsar
{
    public partial class RenderScene : DockContent, IRegisterMessage
    {
        private enum Zoom
        {
            In = 0,
            Out
        }
        private Zoom _zoom;

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
                RegisterForMessages();
            }
        }

        private void RegisterForMessages()
        {
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.AdjustRenderFormWidth
            };
            _mainApplication.MessageQueue.RegisterForMessage(registrant);

            registrant = new Registrant
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

        }

        public RenderScene()
        {
            InitializeComponent();

            Resize += RenderScene_Resize;

        }

        private void RenderScene_Load(object sender, EventArgs e)
        {
            renderSurface.Show<PulsarApplication>(new ApplicationOptions(assetsFolder: "E:\\VSProjects\\Windows\\Pulsar\\Assets"));
            MainApplication = (PulsarApplication)renderSurface.Application;
        }

        private void RenderScene_Resize(object sender, EventArgs e)
        {
            renderSurface.Top = renderSceneTools.Size.Height + 1;
            renderSurface.Width = Width;
            renderSurface.Height = Height - (renderSceneTools.Size.Height + renderSurface.Size.Height) - 1;

            //update the main dragger
            IntVector2 surfaceDimensions = new IntVector2(Width, Height);
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
                return renderSurface.Handle;
            }
        }

        public UrhoSurface DrawingSurface
        {
            get
            {
                return renderSurface;
            }
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
            if (MainApplication != null)
            {
                MainApplication.DragType = dragType;
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
                    case PulsarMessage.MessageType.AdjustRenderFormWidth:
                        AdjustFormWidth(message);
                        break;
                    case PulsarMessage.MessageType.MouseWheelZoomIn:
                        _zoom = Zoom.In;
                        AdjustZoom();
                        break;
                    case PulsarMessage.MessageType.MouseWheelZoomOut:
                        _zoom = Zoom.Out;
                        AdjustZoom();
                        break;
                }
            }
        }

        private void AdjustZoom()
        {
            PulsarCamera mainCamera = null;
            Component cameraComponent = null;

            if (InvokeRequired)
            {
                Urho.Application.InvokeOnMain(AdjustZoom);
            }
            else
            {
                if (!Focused) return;

                //get the main camera
                if (_mainApplication != null)
                {
                    if (_mainApplication.DisplayScene != null)
                    {
                        cameraComponent = _mainApplication.DisplayScene.Components.ToList().Find(camera => camera.Node.Name == "mainCamera");
                        if (cameraComponent != null)
                        {
                            mainCamera = (PulsarCamera)cameraComponent;
                            if (mainCamera == null) return;
                        }
                    }
                }

                switch (_zoom)
                {
                    case Zoom.In:
                        mainCamera.Camera.Fov -= 2.0f;
                        break;
                    case Zoom.Out:
                        mainCamera.Camera.Fov += 2.0f;
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
                        Size newSize = new Size((int)widthProperty, Size.Height);
                        Size = newSize;
                    }
                }
            }
            ResumeLayout();
        }

        public object Registrant()
        {
            return this;
        }
    }
}
