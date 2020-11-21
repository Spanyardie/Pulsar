using System;
using System.Windows.Forms;
using Urho;
using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Primitives;
using Pulsar.EventArguments;

namespace Pulsar
{
    public partial class PulsarActionTarget : UserControl
    {
        public event EventHandler ActionTargetChanged;

        private Node _target;
        public Node Target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
            }
        }

        private PulsarScene _scene;
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

        public string PropertyName { get; set; }

        public PulsarActionTarget()
        {
            InitializeComponent();
        }

        public void InitialiseComboContent()
        {
            if (_scene != null)
            {
                foreach (Node node in _scene.Children)
                {
                    BaseEntity baseEntity = node.GetComponent<BaseEntity>();
                    if (baseEntity != null)
                    {
                        if (!baseEntity.IsSystem)
                        {
                            cboTarget.Items.Add(baseEntity.Name);
                        }
                    }
                }
            }
        }

        private void Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_scene != null)
            {
                _target = _scene.GetChild(cboTarget.Text);

                ActionTargetEventArgs args = new ActionTargetEventArgs
                {
                    Target = _target,
                    PropertyName = "Target"
                };

                ActionTargetChanged?.Invoke(this, args);
            }
        }
    }
}
