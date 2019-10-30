using System.Collections.Generic;
using System.Linq;
using Yugen.Toolkit.Standard.Collections;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class CollectionHelper
    {
        public static List<GroupInfoList> GroupCollectionAscending<T>(IEnumerable<IGrouping<string, T>> query) => 
            Group(query);

        public static List<GroupInfoList> GroupCollectionDescending<T>(IEnumerable<IGrouping<string, T>> query) => 
            Group(query, true);

        private static List<GroupInfoList> Group<T>(IEnumerable<IGrouping<string, T>> query, bool isDescending =  false)
        {
            var enumerable = query as IList<IGrouping<string, T>> ?? query.ToList();

            var result = isDescending ?
                         enumerable.OrderByDescending(g => g.Key).Select(g => new { GroupName = g.Key, Items = g }) :
                         enumerable.OrderBy(g => g.Key).Select(g => new { GroupName = g.Key, Items = g });

            var list = new List<GroupInfoList>();
            foreach (var g in result)
            {
                GroupInfoList info = new GroupInfoList { Key = g.GroupName };
                info.AddRange((IEnumerable<object>)g.Items);
                list.Add(info);
            }
            return list;
        }
    }
}