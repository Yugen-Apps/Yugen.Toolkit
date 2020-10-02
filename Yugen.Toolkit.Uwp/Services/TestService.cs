using Microsoft.Extensions.Logging;

namespace Yugen.Toolkit.Uwp.Services
{
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> _logger;

        public TestService()
        {
        }

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
        }

        public void Test()
        {
            System.Diagnostics.Debug.WriteLine("A");
            _logger?.LogDebug("aaa");
        }
    }

    public interface ITestService
    {
        void Test();
    }
}
