using System.Collections.Generic;
using Yugen.Toolkit.Standard.Mvvm.ComponentModel;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation
{
    public class NavigationParameterViewModel : ViewModelBase
    {
        //private string _parameter;

        private string _text;
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }


        private CategoryObservableObject _category = new CategoryObservableObject();
        public CategoryObservableObject Category
        {
            get { return _category; }
            set { Set(ref _category, value); }
        }

        public override void OnNavigatedTo(object parameter, IDictionary<string, object> state)
        {
            Text = parameter as string ?? string.Empty;

            Category.Name = Text;
        }

        public void Page_Loaded(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            //Text = _parameter;
        }

        public void Button_Tapped(object _1, Windows.UI.Xaml.Input.TappedRoutedEventArgs _2)
        {
            Text = "aaa";
        }
    }

    public class Category
    {
        public string Name { get; set; }
    }

    public class CategoryObservableObject : ObservableObject<Category>
    {
        public CategoryObservableObject(Category model = null) : base(model) { }

        public string Name
        {
            get { return Model.Name; }
            set { Set(Model.Name, value, () => Model.Name = value); }
        }
    }

    //public class CategoryObservableObject : ObservableObject
    //{
    //    private readonly Category _category;

    //    public CategoryObservableObject(Category category)
    //        => _category = category;

    //    public string Name
    //    {
    //        get { return _category.Name; }
    //        set => Set(() => _category.Name, value);
    //    }
    //}
}
