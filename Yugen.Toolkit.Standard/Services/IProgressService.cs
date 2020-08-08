using System;

namespace Yugen.Toolkit.Standard.Services
{
    /// <summary>
    /// Provides an Service that invokes callbacks for each reported progressvalue.
    /// </summary>
    public interface IProgressService
    {
        /// <summary>
        /// Initializes the Service object with the specified callback.
        /// </summary>
        /// <param name="handler"> A handler to invoke for each reported progress value. </param>
        void Init(Action<int> handler);

        /// <summary>
        /// Increment the progress value
        /// </summary>
        /// <param name="total">the total</param>
        /// <param name="startPercentage">the start percentage</param>
        /// <param name="maxPercentage">the max percentage</param>
        void IncrementProgress(int total, int startPercentage = 0, int maxPercentage = 100);

        /// <summary>
        /// Reset the current progress value
        /// </summary>
        void Reset();
    }
}