using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Evaluators;

namespace Yugen.Toolkit.Uwp.Controls.Graphs
{
    public sealed partial class BarGraph : UserControl
    {
        //https://raw.githubusercontent.com/ailon/UWPQuickCharts/master/UWPCharts/SerialChart.cs
        #region DependencyProperties

        /// <summary>
        /// Gets or sets path to the property holding category values in data source.
        /// This is a dependency property.
        /// </summary>
        public string CategoryValueMemberPath
        {
            get { return (string)GetValue(CategoryValueMemberPathProperty); }
            set { SetValue(CategoryValueMemberPathProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="CategoryValueMemberPath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CategoryValueMemberPathProperty = DependencyProperty.Register(
            nameof(CategoryValueMemberPath), 
            typeof(string), 
            typeof(BarGraph),
            new PropertyMetadata(null)
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

        /// <summary>
        /// Identifies <see cref="ValueMemberPath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueMemberPathProperty = DependencyProperty.Register(
            nameof(ValueMemberPath), 
            typeof(string), 
            typeof(BarGraph),
            new PropertyMetadata(null, new PropertyChangedCallback(OnValueMemberPathPropertyChanged))
        );

        private static void OnValueMemberPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BarGraph graph = d as BarGraph;
            graph?.ValueMemberPathChanged?.Invoke(graph, new DataPathEventArgs(e.NewValue as string));
        }
        
        /// <summary>
        /// Event is raised when ValueMemberPath changes.
        /// </summary>
        public event EventHandler<DataPathEventArgs> ValueMemberPathChanged;


        /// <summary>
        /// Gets or sets data source for the chart.
        /// This is a dependency property.
        /// </summary>
        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="DataSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
            nameof(DataSource), 
            typeof(IEnumerable), 
            typeof(BarGraph),
            new PropertyMetadata(null, new PropertyChangedCallback(OnDataSourcePropertyChanged)));
        
        private static void OnDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BarGraph chart = d as BarGraph;
            DetachOldDataSourceCollectionChangedListener(chart, e.OldValue);
            AttachDataSourceCollectionChangedListener(chart, e.NewValue);
            chart?.ProcessData();
        }

        private static void DetachOldDataSourceCollectionChangedListener(BarGraph chart, object dataSource)
        {
            if (dataSource is INotifyCollectionChanged changed)
                changed.CollectionChanged -= chart.OnDataSourceCollectionChanged;
        }

        private static void AttachDataSourceCollectionChangedListener(BarGraph chart, object dataSource)
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
            ElementCollection.Clear();
            if (DataSource == null || string.IsNullOrEmpty(CategoryValueMemberPath) ||
                string.IsNullOrEmpty(ValueMemberPath)) return;

            BindingEvaluator categoryEval = new BindingEvaluator(CategoryValueMemberPath);
            BindingEvaluator valueEval = new BindingEvaluator(ValueMemberPath);
            foreach (object dataItem in DataSource)
            {
                var text = categoryEval.Eval(dataItem).ToString();
                var percentage = (int)valueEval.Eval(dataItem);
                ElementCollection.Add(new ElementObservableObject { Value = percentage, Label = text });
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the style of the scale bar.
        /// </summary>
        public Style ScaleBarStyle
        {
            get { return (Style)GetValue(ScaleBarStyleProperty); }
            set { SetValue(ScaleBarStyleProperty, value); }
        }
        
        /// <summary>
        /// Identifies <see cref="ScaleBarStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleBarStyleProperty = DependencyProperty.Register(
            nameof(ScaleBarStyle),
            typeof(Style),
            typeof(BarGraph),
            new PropertyMetadata(default(Style)));

        /// <summary>
        /// Gets or sets a value indicating the value of the scale bar
        /// </summary>
        public int ScaleBarValue
        {
            get { return (int)GetValue(ScaleBarValueProperty); }
            set
            {
                SetValue(ScaleBarValueProperty, value);
                RealGoal = (Goal * ScaleBarValue / 100) * -1;
            }
        }
        
        /// <summary>
        /// Identifies <see cref="ScaleBarValue"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleBarValueProperty = DependencyProperty.Register(
            nameof(ScaleBarValue),
            typeof(int),
            typeof(BarGraph),
            new PropertyMetadata(100));

        /// <summary>
        /// Gets or sets a value indicating the style of trail bar
        /// </summary>
        public Style TrailBarStyle
        {
            get { return (Style)GetValue(TrailBarStyleProperty); }
            set { SetValue(TrailBarStyleProperty, value); }
        }
        
        /// <summary>
        /// Identifies <see cref="TrailBarStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TrailBarStyleProperty = DependencyProperty.Register(
            nameof(TrailBarStyle),
            typeof(Style),
            typeof(BarGraph),
            new PropertyMetadata(default(Style)));

        /// <summary>
        /// Gets or sets a value indicating the width of a bar
        /// </summary>
        public int ColumnWidth
        {
            get { return (int)GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }
        
        /// <summary>
        /// Identifies <see cref="ColumnWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ColumnWidthProperty = DependencyProperty.Register(
            nameof(ColumnWidth),
            typeof(int),
            typeof(BarGraph),
            new PropertyMetadata(40));

        /// <summary>
        /// Gets or sets a value indicating the number of labels.
        /// </summary>
        public int LabelCount
        {
            get { return (int)GetValue(LabelCountProperty); }
            set { SetValue(LabelCountProperty, value); }
        }
        
        /// <summary>
        /// Identifies <see cref="LabelCount"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelCountProperty = DependencyProperty.Register(
            nameof(LabelCount),
            typeof(int),
            typeof(BarGraph),
            new PropertyMetadata(1));

        /// <summary>
        /// Gets or sets a value indicating the goal value
        /// </summary>
        public int Goal
        {
            get { return (int)GetValue(GoalProperty); }
            set { SetValue(GoalProperty, value); }
        }
        
        /// <summary>
        /// Identifies <see cref="Goal"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GoalProperty = DependencyProperty.Register(
            nameof(Goal),
            typeof(int),
            typeof(BarGraph),
            new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets a value indicating the bottom content
        /// </summary>
        public string BottomContent
        {
            get { return (string)GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }
        
        /// <summary>
        /// Identifies <see cref="BottomContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BottomContentProperty = DependencyProperty.Register(
            nameof(BottomContent),
            typeof(string),
            typeof(BarGraph),
            new PropertyMetadata(default(string)));

        #endregion

        private int RealGoal
        {
            get { return (int)GetValue(GoalProperty); }
            set { SetValue(GoalProperty, value); }
        }

        // TODO: improve https://stackoverflow.com/questions/7321710/wpf-binding-collection-property-in-usercontrol
        /// <summary>
        /// Gets or sets a value indicating the list of elements
        /// </summary>
        public ObservableCollection<ElementObservableObject> ElementCollection { get; set; } = new ObservableCollection<ElementObservableObject>();
             
        /// <summary>
        /// Constructor
        /// </summary>
        public BarGraph()
        {
            InitializeComponent();

            ScaleBarStyle = (Style)Resources["ScaleBarStyle"];
            TrailBarStyle = (Style)Resources["TrailBarStyle"];
        }

        /// <summary>
        /// Update the Graph
        /// </summary>
        public void Update()
        {
            UpdateScaleTrailBar();
            UpdateColumnWidth();
            UpdateLabelCount();
        }

        /// <summary>
        /// Update Scale Trail Bar
        /// </summary>
        private void UpdateScaleTrailBar()
        {
            foreach (var item in ElementCollection)
            {
                item.Value = item.Value * ScaleBarValue / 100;
            }
        }

        /// <summary>
        /// Update Bars width
        /// </summary>
        private void UpdateColumnWidth()
        {
            if (ElementCollection.Count < 1) return;

            var columnWidth = (int) Width / ElementCollection.Count;
            ColumnWidth = columnWidth < 30 ? 30 : columnWidth;
        }

        /// <summary>
        /// Update the number of labels
        /// </summary>
        private void UpdateLabelCount()
        {
            for (var index = 1; index < ElementCollection.Count; index++)
            {
                if (index > 1)
                {
                    ElementCollection[index - 1].Label = index % LabelCount == 0
                        ? ElementCollection[index - 1].Label
                        : "";
                }
            }
        }
    }
}