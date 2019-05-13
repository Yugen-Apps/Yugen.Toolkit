using System;
using Common.Standard.Collections;

namespace Common.Standard.Services
{
    /// <summary>
    /// in viewmodel constructor
    /// register to the mediator for the UserWroteSomething message, Callback delegate, when message is seen
    /// MediatorService.Instance.Register(o => { CommandBarViewModel.Title = (string) o; },
    /// </summary>
    public sealed class MediatorService
    {
        private readonly MultiDictionary<string, Action<object>> _internalList = new MultiDictionary<string, Action<object>>();

        public static MediatorService Instance { get; } = new MediatorService();

        /// <summary>
        /// Registers a Colleague to a specific message
        /// </summary>
        /// <param name="callback">The callback to use
        /// when the message it seen</param>
        /// <param name="propertyEnum">The message to
        /// register to</param>
        public void Register(Action<object> callback, string propertyEnum)
        {
            _internalList.AddValue(propertyEnum, callback);
        }

        /// <summary>
        /// Notify all colleagues that are registed to the
        /// specific message
        /// </summary>
        /// <param name="propertyEnum">The message for the notify by</param>
        /// <param name="args">The arguments for the message</param>
        public void NotifyColleagues(string propertyEnum, object args)
        {
            if (!_internalList.ContainsKey(propertyEnum)) return;
            
            //forward the message to all listeners
            foreach (var callback in _internalList[propertyEnum]) callback(args);
        }
    }
}