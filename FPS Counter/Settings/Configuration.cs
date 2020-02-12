using BS_Utils.Utilities;

namespace FPS_Counter.Settings
{
    public class Configuration
    {
        private static Config config;
        private static readonly string ConfigSection = "Settings";

        public static float UpdateRate { get; internal set; }
        public static bool ShowRing { get; internal set; }
        public static bool UseColors { get; internal set; }

        internal static void Init()
        {
            config = new Config(Plugin.PluginName);
        }

        internal static void Load()
        {
            UpdateRate = config.GetFloat(ConfigSection, nameof(UpdateRate), 0.5f, false);
            ShowRing = config.GetBool(ConfigSection, nameof(ShowRing), true, false);
            UseColors = config.GetBool(ConfigSection, nameof(UseColors), true, false);
        }

        internal static void Save()
        {
            config.SetFloat(ConfigSection, nameof(UpdateRate), UpdateRate);
            config.SetBool(ConfigSection, nameof(ShowRing), ShowRing);
            config.SetBool(ConfigSection, nameof(UseColors), UseColors);
        }
    }
}
