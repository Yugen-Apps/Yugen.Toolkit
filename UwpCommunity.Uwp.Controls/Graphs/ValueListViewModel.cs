using UwpCommunity.Uwp.ViewModels;

namespace UwpCommunity.Uwp.Controls.Graphs
{
    public class ValueListViewModel : BaseViewModel
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        private int _percentage;
        public int Percentage
        {
            get { return _percentage; }
            set { Set(ref _percentage, value); }
        }
    }
}