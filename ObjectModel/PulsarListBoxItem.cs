using static Pulsar.ObjectModel.PulsarAction;

namespace Pulsar.ObjectModel
{
    public class PulsarListBoxItem
    {
        //DisplayMember property for the ListBox
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public ActionType Type { get; set; }

        public ActionTypes ActionType { get; set; }
    }
}
