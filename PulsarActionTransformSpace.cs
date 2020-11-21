using Pulsar.EventArguments;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Urho;

namespace Pulsar
{
    public partial class PulsarActionTransformSpace : UserControl
    {
        public event EventHandler ActionTransformSpaceChanged;

        private TransformSpace _transformSpace;
        public TransformSpace TransFormSpace 
        { 
            get
            {
                return _transformSpace;
            }
            set
            {
                _transformSpace = value;
                switch(value)
                {
                    case TransformSpace.Local:
                        radioTransformSpaceLocal.Checked = true;
                        break;
                    case TransformSpace.Parent:
                        radioTransformSpaceParent.Checked = true;
                        break;
                    case TransformSpace.World:
                        radioTransformSpaceWorld.Checked = true;
                        break;
                }
            }
        }

        public string PropertyName { get; set; }

        private static bool _isChanging = false;
        
        public PulsarActionTransformSpace()
        {
            InitializeComponent();
        }

        private void TransformSpace_Changed(object sender, EventArgs e)
        {
            if (_isChanging) return;

            if(sender != null)
            {

                _isChanging = true;

                string name = ((RadioButton)sender).Name;
                if (name.Contains("Local")) _transformSpace = TransformSpace.Local;
                if (name.Contains("Parent")) _transformSpace = TransformSpace.Parent;
                if (name.Contains("World")) _transformSpace = TransformSpace.World;

                Debug.Print("TransformSpace_Changed: '" + name + "'");

                _isChanging = false;

                ActionTransformSpaceEventArgs args = new ActionTransformSpaceEventArgs
                {
                    TransformSpace = _transformSpace,
                    PropertyName = "TransformSpace"
                };

                ActionTransformSpaceChanged?.Invoke(this, args);
            }
        }
    }
}
