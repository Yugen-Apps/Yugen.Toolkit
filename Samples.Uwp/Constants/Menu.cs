using Samples.Uwp.Models;
using System.Collections.Generic;

namespace Samples.Uwp.Constants
{
    public static class Menu
    {
        public static List<MenuItem> MenuCollection = new List<MenuItem>()
        {
            new MenuItem()
            {
                Name = "Home",
                Tag = "HomePage"
            },
            new MenuItem()
            {
                Name = "Controls",
                Children =
                    {
                        new MenuItem()
                        {
                            Name = "Validation",
                            Tag = "ValidationPage"
                        }
                    }
            }
        };
    }
}
