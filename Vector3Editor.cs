using PulsarPropertyVector3;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms.Design;
using Urho;

namespace Pulsar.Helpers
{
    public class Vector3Editor : UITypeEditor
    {
        public string LabelText { get; set; }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)

            {

                var propertyAttributes = context.PropertyDescriptor.Attributes.OfType<LabelTextAttribute>();

                if (propertyAttributes.Count() > 0)
                {
                    var argumentsAttribute = propertyAttributes.First();
                    LabelText = argumentsAttribute.LabelText;
                }

                PropertyVector3 vector3 = new PropertyVector3(LabelText)
                {
                    Vector3 = (Vector3)value
                };

                wfes.DropDownControl(vector3);

                value = vector3.Vector3;

            }

            return value;
        }

        public class LabelTextAttribute : Attribute
        {
            public string LabelText { get; private set; }

            public LabelTextAttribute(string labelText)
            {
                LabelText = labelText;
            }
        }
    }
}
