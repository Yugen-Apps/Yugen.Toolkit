//namespace Common.Data.Extensions
//{
//    public static class DbContextExtension
//    {
//        public static bool AllMigrationsApplied(this DbContext context)
//        {
//            var applied = context.GetService<IHistoryRepository>()
//                .GetAppliedMigrations()
//                .Select(m => m.MigrationId);

//            var total = context.GetService<IMigrationsAssembly>()
//                .Migrations
//                .Select(m => m.Key);

//            return !total.Except(applied).Any();
//        }

//        public static void EnsureSeeded(this DbContext context)
//        {
//            if (!context.Attributes.Any())
//            {
//                context.AddRange(articleList);
//                context.SaveChanges();
//            }

//            if (!context.Status.Any())
//            {
//                var stati = JsonConvert.DeserializeObject<List<Status>>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "status.json"));
//                context.AddRange(stati);
//                context.SaveChanges();
//            }
//        }
//    }
//}