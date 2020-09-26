﻿using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GraphPage : Page
    {
        public GraphViewModel ViewModel { get; set; } = new GraphViewModel();

        public GraphPage()
        {
            this.InitializeComponent();
        }
    }
}