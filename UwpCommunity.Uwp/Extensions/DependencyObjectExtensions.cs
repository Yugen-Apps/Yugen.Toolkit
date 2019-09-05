// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyObjectExtensions.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using UwpCommunity.Uwp.Listeners;

namespace UwpCommunity.Uwp.Extensions
{
    /// <summary>
    /// A collection of <see cref="DependencyObject"/> extensions.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Listens to changes of a property on a DependencyObject.
        /// </summary>
        /// <param name="obj">
        /// The DependencyObject.
        /// </param>
        /// <param name="propertyName">
        /// The property name to listen to.
        /// </param>
        /// <param name="eventHandler">
        /// The event handler which will fire when the property changes.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public static IDisposable ListenToProperty(
            this DependencyObject obj,
            string propertyName,
            DependencyPropertyChangedEventHandler eventHandler)
        {
            return new DependencyPropertyListener(obj, propertyName, eventHandler);
        }

        /// <summary>
        /// Finds a child DependencyObject of a certain type from the given DependencyObject.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object to find.
        /// </typeparam>
        /// <param name="d">
        /// The DependencyObject to start looking from.
        /// </param>
        /// <returns>
        /// Returns the requested object, if it exists.
        /// </returns>
        public static T FindChildElementOfType<T>(this DependencyObject d) where T : DependencyObject
        {
            if (d == null)
            {
                return null;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                var child = VisualTreeHelper.GetChild(d, i);

                var result = child as T ?? FindChildElementOfType<T>(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the descendants of a <see cref="DependencyObject"/> based on a given type.
        /// </summary>
        /// <param name="obj">
        /// The object to get descendants from.
        /// </param>
        /// <typeparam name="T">
        /// The type of descendants to find.
        /// </typeparam>
        /// <returns>
        /// Returns a collection of DependencyObjects of the given type.
        /// </returns>
        public static IEnumerable<T> GetDescendantsOfType<T>(this DependencyObject obj) where T : DependencyObject
        {
            return obj.GetDescendants().OfType<T>();
        }

        /// <summary>
        /// Gets the decendants of a <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The object to get descendants from.
        /// </param>
        /// <returns>
        /// Returns a collection of DependencyObjects.
        /// </returns>
        public static IEnumerable<DependencyObject> GetDescendants(this DependencyObject obj)
        {
            if (obj == null)
            {
                yield break;
            }

            var queue = new Queue<DependencyObject>();


            if (obj is Popup popup)
            {
                if (popup.Child != null)
                {
                    queue.Enqueue(popup.Child);
                    yield return popup.Child;
                }
            }
            else
            {
                var count = VisualTreeHelper.GetChildrenCount(obj);

                for (var i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    queue.Enqueue(child);
                    yield return child;
                }
            }

            while (queue.Count > 0)
            {
                var parent = queue.Dequeue();

                popup = parent as Popup;

                if (popup != null)
                {
                    if (popup.Child == null)
                    {
                        continue;
                    }

                    queue.Enqueue(popup.Child);
                    yield return popup.Child;
                }
                else
                {
                    var count = VisualTreeHelper.GetChildrenCount(parent);

                    for (var i = 0; i < count; i++)
                    {
                        var child = VisualTreeHelper.GetChild(parent, i);
                        yield return child;
                        queue.Enqueue(child);
                    }
                }
            }
        }
    }
}