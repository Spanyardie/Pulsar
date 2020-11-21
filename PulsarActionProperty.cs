using System;
using System.Windows.Forms;
using System.ComponentModel;
using static Pulsar.ObjectModel.PulsarAction;
using Pulsar.EventArguments;

namespace Pulsar
{
    public partial class PulsarActionProperty : UserControl
    {
        public event EventHandler ActionPropertyChanged;

        public ActionDataTypes Type { get; set; }

        public string PropertyName { get; set; }

        private string _label;
        public string Label 
        { 
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                lblHeading.Text = value;
            }
        }

        public string Value 
        { 
            get
            {
                return txtPropertyValue.Text;
            }
            set
            {
                txtPropertyValue.Text = value;
            }
        }

        public PulsarActionProperty()
        {
            InitializeComponent();
            Validating += PulsarActionProperty_Validating;
        }

        public PulsarActionProperty(string label, string value)
        {
            InitializeComponent();

            Label = label;
            Value = value;

            lblHeading.Text = Label;
            txtPropertyValue.Text = Value;
            Validating += PulsarActionProperty_Validating;
        }

        private void PulsarActionProperty_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateProperty())
            {
                txtPropertyValue.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtPropertyValue.BackColor = System.Drawing.Color.White;
            }
        }

        private bool ValidateProperty()
        {
            bool returnValue = false;

            switch(Type)
            {
                case ActionDataTypes.Float:
                    returnValue = float.TryParse(txtPropertyValue.Text.Trim(), out _);
                    break;
                case ActionDataTypes.Int:
                    returnValue = int.TryParse(txtPropertyValue.Text.Trim(), out _);
                    break;
                case ActionDataTypes.String:
                    returnValue = (!string.IsNullOrWhiteSpace(txtPropertyValue.Text));
                    break;
                case ActionDataTypes.UInt:
                    returnValue = uint.TryParse(txtPropertyValue.Text.Trim(), out _);
                    break;
            }

            return returnValue;
        }

        private void TextPropertyValue_Changed(object sender, EventArgs e)
        {
            ActionPropertyEventArgs eventArgs = new ActionPropertyEventArgs();

            if (ValidateProperty())
            {
                eventArgs.DataType = Type;
                eventArgs.PropertyName = PropertyName;
                switch (Type)
                {
                    case ActionDataTypes.Float:
                        float.TryParse(txtPropertyValue.Text, out float floatValue);
                        Value = floatValue.ToString();
                        eventArgs.Data = floatValue;
                        break;
                    case ActionDataTypes.Int:
                        int.TryParse(txtPropertyValue.Text, out int intValue);
                        Value = intValue.ToString();
                        eventArgs.Data = intValue;
                        break;
                    case ActionDataTypes.String:
                        Value = txtPropertyValue.Text;
                        eventArgs.Data = txtPropertyValue.Text;
                        break;
                    case ActionDataTypes.UInt:
                        uint.TryParse(txtPropertyValue.Text, out uint uintValue);
                        Value = uintValue.ToString();
                        eventArgs.Data = uintValue;
                        break;
                }

                //raise event
                ActionPropertyChanged?.Invoke(this, eventArgs);
            }
        }
    }
}
