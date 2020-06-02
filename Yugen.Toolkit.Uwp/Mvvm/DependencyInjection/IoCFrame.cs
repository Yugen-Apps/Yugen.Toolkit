using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Yugen.Toolkit.Uwp.Hosting.Attributes;

namespace Yugen.Toolkit.Uwp.Hosting
{
    public sealed class IoCFrame : Frame
    {
        private readonly IServiceProvider serviceProvider;

        public IoCFrame(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ??
                                     throw new ArgumentNullException(nameof(serviceProvider));

            this.Navigated += OnFrameNavigated;
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            InjectViewModelIfRequired(e);
            InjectProperties(e);
        }

        private void InjectViewModelIfRequired(NavigationEventArgs e)
        {
            var viewModelProperty = e.SourcePageType
                            .GetProperties()
                            .SingleOrDefault(prop => prop.GetCustomAttribute<ViewModelAttribute>() != null);

            if (viewModelProperty != null)
            {
                var viewModelInstance = CreateInstance(viewModelProperty.PropertyType);
                viewModelProperty.SetValue(e.Content, viewModelInstance);
            }
        }

        private void InjectProperties(NavigationEventArgs e)
        {
            var injectableProperties = e.SourcePageType
                .GetProperties()
                .Where(prop => prop.GetCustomAttribute<DependencyAttribute>() != null)
                .ToList();

            if (!injectableProperties.Any())
                return;

            foreach (var injectableProperty in injectableProperties)
            {
                var injectablePropertyValue = this.serviceProvider.GetService(injectableProperty.PropertyType);
                injectableProperty.SetValue(e.Content, injectablePropertyValue);
            }
        }

        private object CreateInstance(Type instanceType)
        {
            var ctors = instanceType.GetConstructors();
            var ctorsAndParameters = ctors
                .Select(ctor => new { Ctor = ctor, Params = ctor.GetParameters() })
                .OrderByDescending(a => a.Params.Length)
                .ToList();

            if (!ctorsAndParameters.Any())
                return Activator.CreateInstance(instanceType);

            foreach (var ctorAndParams in ctorsAndParameters)
            {
                var ctorParamsInstances = new List<object>(ctorAndParams.Params.Length);

                try
                {
                    foreach (var ctorParam in ctorAndParams.Params)
                    {
                        var initializedCtorParam = this.serviceProvider.GetService(ctorParam.ParameterType);
                        ctorParamsInstances.Add(initializedCtorParam);
                    }

                    return Activator.CreateInstance(instanceType, ctorParamsInstances.ToArray());
                }
                catch
                {
                    continue;
                }
            }

            throw new Exception($"Unable to initialize instance of type: {instanceType.FullName}. Possible cause: no appropriate constructors were found.");
        }
    }
}
