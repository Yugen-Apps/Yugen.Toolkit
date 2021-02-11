namespace Yugen.Toolkit.Uwp.Samples.Models
{
    public class RadioOption<T>
    {
        public RadioOption(T element) => Element = element;

        public T Element { get; set; }

        public override string ToString() => Element.ToString();
    }
}