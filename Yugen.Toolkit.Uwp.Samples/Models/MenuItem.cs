using System.Collections.ObjectModel;

namespace Yugen.Toolkit.Uwp.Samples.Models
{
    public class MenuItem
    {
        public string Glyph { get; set; } = "\uE80F";
        public string Name { get; set; }
        public string Tag { get; set; }
        public bool IsExpanded { get; set; } = true;
        public bool IsLeaf { get; set; } = true;
        public ObservableCollection<MenuItem> Children { get; set; }

        public MenuItem() { }

        public MenuItem(string name, string tag)
        {
            Name = name;
            Tag = tag;
        }

        public override string ToString() => Name;
    }
}
