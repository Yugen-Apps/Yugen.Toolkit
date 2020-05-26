using System;
using System.Collections.Generic;
using System.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Extensions
{
    public static class GroupedCollectionExtension
    {
        public static IOrderedEnumerable<IGrouping<string, TSource>> GroupByFirstLetterAscending<TSource>(
            this IEnumerable<TSource> source, Func<TSource, string> keySelector) =>
                source.GroupBy(item => keySelector(item).Substring(0, 1).ToUpper()).OrderBy(g => g.Key);

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