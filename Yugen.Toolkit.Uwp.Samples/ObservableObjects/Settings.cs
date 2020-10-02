using Yugen.Toolkit.Uwp.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ObservableObjects
{
    public class Settings : ObservableSettings
    {
        public static Settings Default { get; } = new Settings();

        [DefaultSettingValue(Value = true)]
        public bool IsDebug
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public string Url
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public int Version
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public Rendering Rendering
        {
            get { return Get<Rendering>(); }
            set { Set(value); }
        }
    }

    public class Rendering
    {
        public string Url { get; set; }

        public int Version { get; set; }
    }
}