using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UwpCommunity.Uwp.Evaluators;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpCommunity.Uwp.Controls.Graphs
{
    public sealed partial class BarGraphUserControl : UserControl
    {
        //https://raw.githubusercontent.com/ailon/UWPQuickCharts/master/UWPCharts/SerialChart.cs
        #region CategoryValueMemberPathProperty

        /// <summary>
        /// Identifies <see cref="CategoryValueMemberPath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CategoryValueMemberPathProperty = DependencyProperty.Register(
            nameof(CategoryValueMemberPath), 
            typeof(string), 
            typeof(BarGraphUserControl),
            new PropertyMetadata(null)
        );

        /// <summary>
        /// Gets or sets path to the property holding category values in data source.
        /// This is a dependency property.
        /// </summary>
        public string CategoryValueMemberPath
        {
            get { return (string)GetValue(CategoryValueMemberPathProperty); }
            set { SetValue(CategoryValueMemberPathProperty, value); }
        }

        #endregion

        #region ValueMemberPathProperty

        /// <summary>
        /// Identifies <see cref="ValueMemberPath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueMemberPathProperty = DependencyProperty.Register(
            nameof(ValueMemberPath), 
            typeof(string), 
            typeof(BarGraphUserControl),
            new PropertyMetadata(null, new PropertyChangedCallback(BarGraphUserControl.OnValueMemberPathPropertyChanged))
        );

        /// <summary>
        /// Gets or sets path to the member in the datasource holding values for this graph.
        /// This is a dependency property.
        /// </summary>
        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }

        private static void OnValueMemberPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BarGraphUserControl graph = d as BarGraphUserControl;
            graph?.ValueMemberPathChanged?.Invoke(graph, new DataPathEventArgs(e.NewValue as string));
        }
        
        /// <summary>
        /// Event is raised when ValueMemberPath changes.
        /// </summary>
        public event EventHandler<DataPathEventArgs> ValueMemberPathChanged;

        #endregion

        #region DataSourceProperty

        /// <summary>
        /// Identifies <see cref="DataSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
            nameof(DataSource), 
            typeof(IEnumerable), 
            typeof(BarGraphUserControl),
            new PropertyMetadata(null, new PropertyChangedCallback(BarGraphUserControl.OnDataSourcePropertyChanged)));

        /// <summary>
        /// Gets or sets data source for the chart.
        /// This is a dependency property.
        /// </summary>
        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        
        private static void OnDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BarGraphUserControl chart = d as BarGraphUserControl;
            DetachOldDataSourceCollectionChangedListener(chart, e.OldValue);
            AttachDataSourceCollectionChangedListener(chart, e.NewValue);
            chart?.ProcessData();
        }

        private static void DetachOldDataSourceCollectionChangedListener(BarGraphUserControl chart, object dataSource)
        {
            if (dataSource is INotifyCollectionChanged changed)
                changed.CollectionChanged -= chart.OnDataSourceCollectionChanged;
        }

        private static void AttachDataSourceCollectionChangedListener(BarGraphUserControl chart, object dataSource)
        {
            if (dataSource is INotifyCollectionChanged changed)
                changed.CollectionChanged += new NotifyCollectionChangedEventHandler(chart.OnDataSourceCollectionChanged);
        }

        private void OnDataSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // TODO: LOW implement intelligent mechanism to hanlde multiple changes in one batch
            ProcessData();
        }

        private void ProcessData()
        {
            ValueCollection.Clear();
            if (this.DataSource == null || string.IsNullOrEmpty(CategoryValueMemberPath) ||
                string.IsNullOrEmpty(ValueMemberPath)) return;

            BindingEvaluator categoryEval = new BindingEvaluator(CategoryValueMemberPath);
            BindingEvaluator valueEval = new BindingEvaluator(ValueMemberPath);
            foreach (object dataItem in this.DataSource)
            {
                var text = categoryEval.Eval(dataItem).ToString();
                var percentage = (int)valueEval.Eval(dataItem);
                ValueCollection.Add(new ValueListViewModel { Percentage = percentage, Text = text });
            }
        }

        #endregion

        #region ScaleBarStyleProperty

        public static readonly DependencyProperty ScaleBarStyleProperty = DependencyProperty.Register(
            nameof(ScaleBarStyle),
            typeof(Style),
            typeof(BarGraphUserControl),
            new PropertyMetadata(default(Style)));

        public Style ScaleBarStyle
        {
            get { return (Style)GetValue(ScaleBarStyleProperty); }
            set { SetValue(ScaleBarStyleProperty, value); }
        }

        #endregion

        #region ScaleBarValueProperty

        public static readonly DependencyProperty ScaleBarValueProperty = DependencyProperty.Register(
            nameof(ScaleBarValue),
            typeof(int),
            typeof(BarGraphUserControl),
            new PropertyMetadata(100));

        public int ScaleBarValue
        {
            get { return (int)GetValue(ScaleBarValueProperty); }
            set
            {
                SetValue(ScaleBarValueProperty, value);
                RealGoal = (Goal * ScaleBarValue / 100) * -1;
            }
        }

        #endregion

        #region TrailBarStyleProperty

        public static readonly DependencyProperty TrailBarStyleProperty = DependencyProperty.Register(
            nameof(TrailBarStyle),
            typeof(Style),
            typeof(BarGraphUserControl),
            new PropertyMetadata(default(Style)));

        public Style TrailBarStyle
        {
            get { return (Style)GetValue(TrailBarStyleProperty); }
            set { SetValue(TrailBarStyleProperty, value); }
        }

        #endregion

        #region ColumnWidthProperty

        public static readonly DependencyProperty ColumnWidthProperty = DependencyProperty.Register(
            nameof(ColumnWidth),
            typeof(int),
            typeof(BarGraphUserControl),
            new PropertyMetadata(40));

        public int ColumnWidth
        {
            get { return (int)GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }

        #endregion

        #region LabelCountProperty

        public static readonly DependencyProperty LabelCountProperty = DependencyProperty.Register(
            nameof(LabelCount),
            typeof(int),
            typeof(BarGraphUserControl),
            new PropertyMetadata(1));

        public int LabelCount
        {
            get { return (int)GetValue(LabelCountProperty); }
            set { SetValue(LabelCountProperty, value); }
        }

        #endregion

        #region GoalProperty

        public static readonly DependencyProperty GoalProperty = DependencyProperty.Register(
            nameof(Goal),
            typeof(int),
            typeof(BarGraphUserControl),
            new PropertyMetadata(0));

        public int Goal
        {
            get { return (int)GetValue(GoalProperty); }
            set { SetValue(GoalProperty, value); }
        }

        private int RealGoal
        {
            get { return (int)GetValue(GoalProperty); }
            set { SetValue(GoalProperty, value); }
        }

        #endregion

        #region BottomContentProperty

        public static readonly DependencyProperty BottomContentProperty = DependencyProperty.Register(
            nameof(BottomContent),
            typeof(string),
            typeof(BarGraphUserControl),
            new PropertyMetadata(default(string)));

        public string BottomContent
        {
            get { return (string)GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }

        #endregion

        // TODO: improve https://stackoverflow.com/questions/7321710/wpf-binding-collection-property-in-usercontrol
        public ObservableCollection<ValueListViewModel> ValueCollection { get; set; } = new ObservableCollection<ValueListViewModel>();
                
        public BarGraphUserControl()
        {
            InitializeComponent();

            ScaleBarStyle = (Style)Resources["ScaleBarStyle"];
            TrailBarStyle = (Style)Resources["TrailBarStyle"];
        }

        public void Update()
        {
            UpdateScaleTrailBar();
            UpdateColumnWidth();
            UpdateLabelCount();
        }

        private void UpdateScaleTrailBar()
        {
            foreach (var item in ValueCollection)
            {
                item.Percentage = item.Percentage * ScaleBarValue / 100;
            }
        }

        private void UpdateColumnWidth()
        {
            if (ValueCollection.Count < 1) return;

            var columnWidth = (int) Width / ValueCollection.Count;
            ColumnWidth = columnWidth < 30 ? 30 : columnWidth;
        }

        private void UpdateLabelCount()
        {
            for (var index = 1; index < ValueCollection.Count; index++)
            {
                if (index > 1)
                {
                    ValueCollection[index - 1].Text = index % LabelCount == 0
                        ? ValueCollection[index - 1].Text
                        : "";
                }
            }
        }
    }
}