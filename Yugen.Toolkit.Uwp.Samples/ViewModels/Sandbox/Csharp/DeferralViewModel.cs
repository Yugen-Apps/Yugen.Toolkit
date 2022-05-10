using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Sandbox.Csharp
{
    public class DeferralViewModel
    {
        private readonly ILogger<DeferralViewModel> _logger;
        private readonly MyEventService _myEventService = new MyEventService();

        public DeferralViewModel(ILogger<DeferralViewModel> logger)
        {
            _logger = logger;

            new MyService(_myEventService);

            StartAsyncCommand = new AsyncRelayCommand(StartAsyncCommandBehavior);
        }

        public ObservableCollection<string> List { get; set; } = new ObservableCollection<string>();

        public IAsyncRelayCommand StartAsyncCommand { get; }

        private async Task StartAsyncCommandBehavior()
        {
            try
            {
                List.Add("start");
                await _myEventService.OnReplaceAsync(500, "");
                await _myEventService.OnReplaceAsync(500, "");
                await _myEventService.OnReplaceAsync(2000, "");
                await _myEventService.OnReplaceAsync(500, "");
                List.Add("ok");
            }
            catch (Exception exception)
            {
                _logger.LogDebug($"2- {exception}");
                List.Add("fail");
            }
        }
    }

    // Entity that allows waiting for asynchronous event handlers execution.
    public class Deferral
    {
        public Deferral()
        {
            Waiter = new ManualResetEvent(false);
        }

        public ManualResetEvent Waiter { get; }

        // Notifies event source that asynchronous event handler has finished its work.
        public void Complete()
        {
            Waiter.Set();
        }
    };

    public abstract class DeferrableEventArgs : EventArgs
    {
        private readonly List<WaitHandle> _waiters;

        internal DeferrableEventArgs()
        {
            _waiters = new List<WaitHandle>();
        }

        public Deferral GetDeferral()
        {
            Deferral deferral = new Deferral();
            _waiters.Add(deferral.Waiter);
            return deferral;
        }

        internal Task WaitAllAsync()
        {
            return Task.Run(() =>
            {
                // Give code up to 1 seconds to finish its execution.
                if (!WaitHandle.WaitAll(_waiters.ToArray(), 1000))
                {
                    throw new TimeoutException("DeferrableEventArgs: Event handlers took to long to execute.");
                }
            });
        }
    }

    public class MyEventService
    {
        public event EventHandler<MyEventArgs> MyEvent;

        public async Task OnReplaceAsync(int x, string y)
        {
            if (MyEvent != null)
            {
                var args = new MyEventArgs(x, y);
                MyEvent?.Invoke(this, args);
                await args.WaitAllAsync();
            }
        }
    }

    public class MyService
    {
        private readonly MyEventService _myEventService;
        private readonly ILogger<MyService> _logger;

        public MyService(MyEventService myEventService)
        {
            _myEventService = myEventService;
            _logger = App.Current.Services.GetService<ILogger<MyService>>();

            _myEventService.MyEvent += OnMyEvent;
        }

        private async void OnMyEvent(object sender, MyEventArgs args)
        {
            Deferral deferral = args.GetDeferral();

            try
            {
                _logger.LogDebug("start");
                await Task.Delay(args.X);
                _logger.LogDebug("end");
            }
            catch (Exception exception)
            {
                _logger.LogDebug($"1- {exception}");
            }
            finally
            {
                deferral.Complete();
            }
        }
    }

    public class MyEventArgs : DeferrableEventArgs
    {
        internal MyEventArgs(int x, string y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }

        public string Y { get; private set; }
    }
}