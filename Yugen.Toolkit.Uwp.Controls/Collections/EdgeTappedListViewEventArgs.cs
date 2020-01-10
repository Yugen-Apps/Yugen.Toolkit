using System;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Controls.Collections
{
    public class EdgeTappedListViewEventArgs : EventArgs
    {
        /// <summary>
        /// Get the ListViewItem
        /// </summary>
        public ListViewItem ListViewItem { get; }

        public EdgeTappedListViewEventArgs(ListViewItem listViewItem)
        {
            ListViewItem = listViewItem;
        }
    }
}