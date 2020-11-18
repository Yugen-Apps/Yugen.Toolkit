using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Yugen.Toolkit.Standard.Extensions;
using Yugen.Toolkit.Uwp.Helpers;
using Yugen.Toolkit.Uwp.Samples.ViewModels;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Helpers;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Snippets.Converters;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Collections;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            // Register services
            Services = ConfigureServices();

            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (!(Window.Current.Content is AppShell shell))
            {
                await InitializeServices();

                // Initial UI styling
                TitleBarHelper.ExpandViewIntoTitleBar();
                //TitleBarHelper.StyleTitleBar(...);

                // Create a AppShell to act as the navigation context and navigate to the first page
                shell = new AppShell { Language = ApplicationLanguages.Languages[0] };
                shell.MainFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: UWP Load state from previously suspended application
                }
            }

            // adds callbacks for Back requests and changes
            NavigationService.Initialize(typeof(App), shell.MainFrame, typeof(HomePage));

            // Place our app shell in the current Window
            Window.Current.Content = shell;

            if (shell.MainFrame.Content == null)
            {
                // When the navigation stack isn't restored, navigate to the first page
                // suppressing the initial entrance animation.
                NavigationService.NavigateToPage(typeof(HomePage), e.Arguments, new SuppressNavigationTransitionInfo());
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private IServiceProvider ConfigureServices()
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

            return new ServiceCollection()
                .AddSingleton<ITestService, TestService>()
                .AddSingleton<IThemeSelectorService, ThemeSelectorService>()

                .AddSingleton<AppShellViewModel>()
                .AddTransient<SettingsViewModel>()

                .AddTransient<CommandViewModel>()
                .AddTransient<MediatorViewModel>()
                .AddTransient<NavigationParameterViewModel>()
                .AddTransient<NavigationViewModel>()
                .AddTransient<ObservableObjectViewModel>()
                .AddTransient<XamlUICommandViewModel>()

                .AddTransient<EnumToBooleanConverterViewModel>()

                .AddTransient<CollectionViewModel>()
                .AddTransient<GroupedCollectionViewModel>()
                .AddTransient<GraphViewModel>()
                //.AddTransient<SampleInAppControlViewModel>()
                .AddTransient<ValidationViewModel>()
                .AddTransient<YugenDialogViewModel>()
                .AddTransient<FindControlViewModel>()
                .AddTransient<ObservableSettingsViewModel>()

                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddSerilog(dispose: true);
                })
                .BuildServiceProvider();
        }

        private async Task InitializeServices()
        {
            await Services.GetService<IThemeSelectorService>().InitializeAsync(true);
        }
    }
}