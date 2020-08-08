using System;

namespace Yugen.Toolkit.Standard.Services
{
    /// <inheritdoc/>
    public class ProgressService : IProgressService
    {
        private IProgress<int> _handler;
        private int current;

        /// <inheritdoc/>
        public void Init(Action<int> handler)
        {
            // The Progress<T> constructor captures our UI context,
            // so the lambda will be run on the UI thread.
            _handler = new Progress<int>(handler);
            current = 0;
        }

        /// <inheritdoc/>
        public void IncrementProgress(int total, int startPercentage = 0, int maxPercentage = 100)
        {
            var currentPercentage = current * (maxPercentage - startPercentage) / total;
            ++current;
            _handler.Report(startPercentage + currentPercentage);
        }

        /// <inheritdoc/>
        public void Reset()
        {
            current = 0;
        }
    }
}
