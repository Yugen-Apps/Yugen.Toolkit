using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using UwpCommunity.Uwp.ViewModels;

namespace UwpCommunity.Uwp.Controls
{
    public sealed partial class GraphUserControl : UserControl
    {
        //improve: https://stackoverflow.com/questions/7321710/wpf-binding-collection-property-in-usercontrol
        public ObservableCollection<ValueListViewModel> ValueCollection { get;set;} = new ObservableCollection<ValueListViewModel>();

        //https://raw.githubusercontent.com/ailon/UWPQuickCharts/master/UWPCharts/SerialChart.cs
        #region StatisticsCollectiontProperty

        /// <summary>
        /// Identifies <see cref="CategoryValueMemberPath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CategoryValueMemberPathProperty = DependencyProperty.Register(
            "CategoryValueMemberPath", typeof(string), typeof(GraphUserControl),
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

        /// <summary>
        /// Event is raised when ValueMemberPath changes.
        /// </summary>
        public event EventHandler<DataPathEventArgs> ValueMemberPathChanged;

        /// <summary>
        /// Identifies <see cref="ValueMemberPath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueMemberPathProperty = DependencyProperty.Register(
            "ValueMemberPath", typeof(string), typeof(GraphUserControl),
            new PropertyMetadata(null, new PropertyChangedCallback(GraphUserControl.OnValueMemberPathPropertyChanged))
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
            GraphUserControl graph = d as GraphUserControl;
            graph?.ValueMemberPathChanged?.Invoke(graph, new DataPathEventArgs(e.NewValue as string));
        }

        /// <summary>
        /// Identifies <see cref="DataSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
            "DataSource", typeof(IEnumerable), typeof(GraphUserControl),
            new PropertyMetadata(null, new PropertyChangedCallback(GraphUserControl.OnDataSourcePropertyChanged)));

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
            GraphUserControl chart = d as GraphUserControl;
            DetachOldDataSourceCollectionChangedListener(chart, e.OldValue);
            AttachDataSourceCollectionChangedListener(chart, e.NewValue);
            chart?.ProcessData();
        }

        private static void DetachOldDataSourceCollectionChangedListener(GraphUserControl chart, object dataSource)
        {
            if (dataSource is INotifyCollectionChanged changed)
                changed.CollectionChanged -= chart.OnDataSourceCollectionChanged;
        }

        private static void AttachDataSourceCollectionChangedListener(GraphUserControl chart, object dataSource)
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
                ValueCollection.Add(new ValueListViewModel{Percentage = percentage, Text = text});
            }
        }

        #endregion

        #region ScaleBarStyleProperty

        public Style ScaleBarStyle
        {
            get { return (Style) GetValue(ScaleBarStyleProperty); }
            set { SetValue(ScaleBarStyleProperty, value); }
        }

        public static readonly DependencyProperty ScaleBarStyleProperty =
            DependencyProperty.Register(nameof(ScaleBarStyle), typeof(Style), typeof(GraphUserControl),
                new PropertyMetadata(default(Style)));

        #endregion

        #region ScaleBarValueProperty

        public int ScaleBarValue
        {
            get { return (int) GetValue(ScaleBarValueProperty); }
            set { SetValue(ScaleBarValueProperty, value); }
        }

        public static readonly DependencyProperty ScaleBarValueProperty =
            DependencyProperty.Register(nameof(ScaleBarValue), typeof(int), typeof(GraphUserControl),
                new PropertyMetadata(100));

        #endregion

        #region TrailBarStyleProperty

        public Style TrailBarStyle
        {
            get { return (Style) GetValue(TrailBarStyleProperty); }
            set { SetValue(TrailBarStyleProperty, value); }
        }

        public static readonly DependencyProperty TrailBarStyleProperty =
            DependencyProperty.Register(nameof(TrailBarStyle), typeof(Style), typeof(GraphUserControl),
                new PropertyMetadata(default(Style)));

        #endregion

        #region ColumnWidthProperty

        public int ColumnWidth
        {
            get { return (int) GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }

        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.Register(nameof(ColumnWidth), typeof(int), typeof(GraphUserControl),
                new PropertyMetadata(40));

        #endregion

        #region LabelCountProperty

        public static readonly DependencyProperty LabelCountProperty =
            DependencyProperty.Register(nameof(LabelCount), typeof(int), typeof(GraphUserControl),
                new PropertyMetadata(1));

        public int LabelCount
        {
            get { return (int) GetValue(LabelCountProperty); }
            set { SetValue(LabelCountProperty, value); }
        }

        #endregion

        #region GoalProperty

        public static readonly DependencyProperty GoalProperty =
            DependencyProperty.Register(nameof(Goal), typeof(int), typeof(GraphUserControl),
                new PropertyMetadata(0));

        public int Goal
        {
            get { return (int) GetValue(GoalProperty); }
            set
            {
                SetValue(GoalProperty, value);
                //RealGoal = value * ScaleBarValue / 100;
            }
        }

        public int RealGoal => Goal * ScaleBarValue / 100;

        #endregion

        //#region RealGoalProperty

        //public static readonly DependencyProperty RealGoalProperty =
        //    DependencyProperty.Register(nameof(RealGoal), typeof(int), typeof(GraphUserControl),
        //        new PropertyMetadata(0));

        //public int RealGoal
        //{
        //    get { return (int)GetValue(RealGoalProperty); }
        //    set { SetValue(RealGoalProperty, value); }
        //}

        ////public int RealGoal => Goal * ScaleBarValue / 100;

        //#endregion

        //public UIElement HeaderContent
        //{
        //    get { return (UIElement)GetValue(HeaderContentProperty); }
        //    set { SetValue(HeaderContentProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty HeaderContentProperty =
        //    DependencyProperty.Register("HeaderContent", typeof(UIElement), typeof(GraphUserControl), new PropertyMetadata(DependencyProperty.UnsetValue));

        public GraphUserControl()
        {
            InitializeComponent();

            ScaleBarStyle = (Style) Resources["ScaleBarStyle"];
            TrailBarStyle = (Style) Resources["TrailBarStyle"];
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

    /// <summary>
    /// Represents arguments for event raised when Path related properties change.
    /// </summary>
    public class DataPathEventArgs : EventArgs

    {

        /// <summary>

        /// Instantiates class with specified new path.

        /// </summary>

        /// <param name="newPath"></param>

        public DataPathEventArgs(string newPath)
        {

            NewPath = newPath;

        }


        /// <summary>

        /// Gets or sets new path.

        /// </summary>

        public string NewPath { get; set; }

    }

    public class GraphRealGoalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int intParam = (int)parameter;
            int intValue = (int)value;
            //return 20;

            return intValue * intParam / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 20;
        }
    }

    /// <summary>
    /// Utility class to facilitate temporary binding evaluation
    /// </summary>
    public class BindingEvaluator : FrameworkElement
    {
        /// <summary>
        /// Created binding evaluator and set path to the property which's value should be evaluated.
        /// </summary>
        /// <param name="propertyPath">Path to the property</param>
        public BindingEvaluator(string propertyPath)
        {
            _propertyPath = propertyPath;
        }

        private readonly string _propertyPath;

        /// <summary>
        /// Dependency property used to evaluate values.
        /// </summary>
        public static readonly DependencyProperty EvaluatorProperty = DependencyProperty.Register(
            "Evaluator", typeof(object), typeof(BindingEvaluator), null);
        
        /// <summary>
        /// Returns evaluated value of property on provided object source.
        /// </summary>
        /// <param name="source">Object for which property value should be evaluated</param>
        /// <returns>Value of the property</returns>
        public object Eval(object source)
        {
            ClearValue(EvaluatorProperty);
            var binding = new Binding
            {
                Path = new PropertyPath(_propertyPath),
                Mode = BindingMode.OneTime,
                Source = source
            }; //new Binding(_propertyPath);
            SetBinding(EvaluatorProperty, binding);
            return GetValue(EvaluatorProperty);
        }
    }
}