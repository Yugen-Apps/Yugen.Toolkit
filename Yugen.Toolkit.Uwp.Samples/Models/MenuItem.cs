using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Yugen.Toolkit.Uwp.Samples.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public bool IsExpanded { get; set; } = true;
        public ObservableCollection<MenuItem> Children { get; set; } = new ObservableCollection<MenuItem>();

        public MenuItem() { }

        public MenuItem(string name, string tag)
        {
            Name = name;
            Tag = tag;
        }

        public override string ToString() => Name;
    }
}
