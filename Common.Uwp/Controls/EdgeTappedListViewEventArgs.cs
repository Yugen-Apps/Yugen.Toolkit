using System;
using Windows.UI.Xaml.Controls;

namespace Common.Uwp.Controls
{
    public class EdgeTappedListViewEventArgs : EventArgs
    {
        public ListViewItem ListViewItem { get; }

        public EdgeTappedListViewEventArgs(ListViewItem listViewItem)
        {
            ListViewItem = listViewItem;
        }
    }
}