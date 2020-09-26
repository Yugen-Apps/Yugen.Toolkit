using Microsoft.Extensions.DependencyInjection;
using System;
using Yugen.Toolkit.Uwp.Samples.ViewModels;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples
{
    public class AppContainer
    {
        public static IServiceProvider Services { get; set; }

        public static void ConfigureServices()
        {
            Services = new ServiceCollection()
                //.AddSingleton<IProgressService, ProgressService>()
                .AddSingleton<AppShellViewModel>()
                .AddTransient<CommandViewModel>()
                .AddTransient<MediatorViewModel>()
                .AddTransient<NavigationParameterViewModel>()
                .AddTransient<ObservableObjectViewModel>()
                .AddTransient<SampleInAppControlViewModel>()
                .AddTransient<XamlUICommandViewModel>()
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
