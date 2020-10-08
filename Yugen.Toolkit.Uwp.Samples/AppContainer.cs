using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using Windows.Storage;
using Yugen.Toolkit.Uwp.Samples.ViewModels;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Snippets.Converters;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Mvvm;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples
{
    public class AppContainer
    {
        public static IServiceProvider Services { get; set; }

        public static void ConfigureServices()
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Logs\\Yugen.Toolkit.Log.");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .WriteTo.File(logFilePath, restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            // .MinimumLevel.Debug()
            // .WriteTo.Debug()
            //  .WriteTo.Logger(l => l.Filter
            //      .ByIncludingOnly(e => e.Level == LogEventLevel.Information)
            //          .WriteTo.File($"{logFilePath}.Info", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information))
            //  .WriteTo.Logger(l => l.Filter
            //      .ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
            //          .WriteTo.File($"{logFilePath}.Warning", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information))
            //  .CreateLogger();

            Log.Debug("Serilog started Debug!");
            Log.Information("Serilog started Information!");
            Log.Warning("Serilog started Warning!");

            Services = new ServiceCollection()
                .AddSingleton<ITestService, TestService>()
                .AddSingleton<IThemeSelectorService, ThemeSelectorService>()
                .AddSingleton<AppShellViewModel>()
                .AddTransient<CommandViewModel>()
                .AddTransient<EnumToBooleanConverterViewModel>()
                .AddTransient<MediatorViewModel>()
                .AddTransient<NavigationParameterViewModel>()
                .AddTransient<ObservableObjectViewModel>()
                .AddTransient<ObservableSettingsViewModel>()
                .AddTransient<SampleInAppControlViewModel>()
                .AddTransient<SettingsViewModel>()
                .AddTransient<XamlUICommandViewModel>()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddSerilog(dispose: true);
                })
                .BuildServiceProvider();
        }

        //public static void ConfigureServices(Action<IServiceCollection> setup)
        //{
        //    var collection = new ServiceCollection();
        //    setup(collection);
        //    Services = collection.BuildServiceProvider();
        //}
    }
}
