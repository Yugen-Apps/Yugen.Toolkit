using Windows.UI.Xaml;

namespace Yugen.Toolkit.Uwp.Samples.Models
{
    public class ElementThemeOption
    {
        public ElementThemeOption(ElementTheme elementTheme) => ElementTheme = elementTheme;

        public ElementTheme ElementTheme { get; set; }

        public override string ToString() => ElementTheme.ToString();
    }
}