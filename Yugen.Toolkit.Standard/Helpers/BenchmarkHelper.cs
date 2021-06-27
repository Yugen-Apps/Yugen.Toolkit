using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System;

namespace Yugen.Toolkit.Standard.Helpers
{
    public class BenchmarkHelper
    {
        private string _text;
        private readonly Stopwatch _sw = new Stopwatch();
        //private readonly Logger _logger;

        public BenchmarkHelper(Type classType, string text = null, [CallerMemberName] string caller = null)
        {
            //Logger.SetDefaultConfiguration(Logger.LogLevel.Trace, Logger.LogLevel.Fatal, "KortextLog");
            //_logger = Logger.Instance();

            _text = $"{classType.Name}/{caller}/{text}";

            Start();
        }

        public void Start()
        {
            _sw.Start();

            //_logger.LogTrace(_text);
        }

        public void Stop(Type classType, string text = null, [CallerMemberName] string caller = null)
        {
            _text = $"{classType.Name}/{caller}/{text}";

            _sw.Stop();
            //_logger.LogTrace($"{_text} Elapsed: {_sw.Elapsed}");
        }
    }
}