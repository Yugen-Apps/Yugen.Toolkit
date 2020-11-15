using System.Collections.ObjectModel;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Controls
{
    public class GraphViewModel : ViewModelBase
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