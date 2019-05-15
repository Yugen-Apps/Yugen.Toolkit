using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Samples.Uwp.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public bool IsExpanded { get; set; } = true;
        public ObservableCollection<MenuItem> Children { get; set; } = new ObservableCollection<MenuItem>();

        public override string ToString()
        {
            return Name;
        }
    }
}
