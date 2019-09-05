using System.Collections.ObjectModel;
using UwpCommunity.Uwp.ViewModels;
using UwpCommunity.Uwp.Samples.Models;

namespace UwpCommunity.Uwp.Samples.ViewModels.Controls
{
    public class GraphViewModel : BaseViewModel
    {
        public ObservableCollection<Graph> StatisticsCollection { get; set; } = new ObservableCollection<Graph>()
        {
            new Graph{ Value = 10, Title = "aaa"},
            new Graph{ Value = 20, Title = "sss"},
            new Graph{ Value = 30, Title = "ddd"},
            new Graph{ Value = 40, Title = "fff"}
        };
    }
}
