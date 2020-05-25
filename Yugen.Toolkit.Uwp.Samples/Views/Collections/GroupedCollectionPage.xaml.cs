using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Collections;
using Yugen.Toolkit.Uwp.Samples.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupedCollectionPage : Page
    {
        public GroupedCollectionPage()
        {
            this.InitializeComponent();
        }

        public GroupedCollectionViewModel ViewModel { get; set; } = new GroupedCollectionViewModel();
    }

    public class GroupedCollectionViewModel
    {
        public GroupedCollectionViewModel()
        {
            var contacts = new[]
            {
                new Person { Name = "Staff" },
                new Person { Name = "Swan" },
                new Person { Name = "Orchid" },
                new Person { Name = "Flame" },
                new Person { Name = "Arrow" },
                new Person { Name = "Tempest" },
                new Person { Name = "Pearl" },
                new Person { Name = "Pearl" },
                new Person { Name = "Lamp Post" },
                new Person { Name = "Looking Glass" },
            };

            //var grouped = contacts
            //                .GroupBy(item => item.Name.First().ToString().ToUpper())
            //               .OrderBy(g => g.Key);

            var grouped = contacts.GroupAscending(item => item.Name);

            GroupedCollection = new ObservableGroupedCollection<string, Person>(grouped);
        }

        public ObservableGroupedCollection<string, Person> GroupedCollection { get; }
    }

    public static class GroupedCollectionExtension
    {
        public static IOrderedEnumerable<IGrouping<TKey, TSource>> GroupAscending<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
                source.GroupBy(keySelector).OrderBy(g => g.Key);
    }


    //    var query = productList.GroupBy(item => item.Title);
    //    return CollectionHelper.GroupCollectionAscending(query).ToList();

    //    public static class GroupedCollectionHelper
    //    {
    //        public static List<GroupInfoList> GroupCollectionAscending<T>(IEnumerable<IGrouping<string, T>> query) =>
    //            Group(query);

    //        public static List<GroupInfoList> GroupCollectionDescending<T>(IEnumerable<IGrouping<string, T>> query) =>
    //            Group(query, true);

    //        private static List<GroupInfoList> Group<T>(IEnumerable<IGrouping<string, T>> query, bool isDescending = false)
    //        {
    //            IList<IGrouping<string, T>> enumerable = query as IList<IGrouping<string, T>> ?? query.ToList();

    //            var result = isDescending ?
    //                         enumerable.OrderByDescending(g => g.Key).Select(g => new { GroupName = g.Key, Items = g }) :
    //                         enumerable.OrderBy(g => g.Key).Select(g => new { GroupName = g.Key, Items = g });

    //            var list = new List<GroupInfoList>();
    //            foreach (var g in result)
    //            {
    //                var info = new GroupInfoList { Key = g.GroupName };
    //                info.AddRange((IEnumerable<object>)g.Items);
    //                list.Add(info);
    //            }
    //            return list;
    //        }
    //    }

    // https://gist.github.com/Sergio0694/ba235dc4ef2c4fec9c4a8091522e475b
}