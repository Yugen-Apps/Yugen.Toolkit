using System;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Hosting
{
    public sealed class Host
    {
        public IServiceProvider ServicesContainer { get; }

        public Frame RootFrame { get; private set; }

        public Host(IServiceProvider servicesContainer)
        {
            this.ServicesContainer = servicesContainer ?? throw new ArgumentNullException(nameof(servicesContainer));
        }

        public Frame CreateNewHostedUwpFrame()
        {
            var newFrame = new IoCFrame(this.ServicesContainer);

            this.RootFrame = newFrame;
            return this.RootFrame;
        }
    }
}
