using BeatSaberMarkupLanguage.Attributes;

namespace FPS_Counter.Settings.UI
{
    internal class MainSettings : PersistentSingleton<MainSettings>
    {
        [UIValue("update-rate")]
        public float FPSUpdateRate
        {
            get => Configuration.UpdateRate;
            set => Configuration.UpdateRate = value;
        }

        [UIValue("show-ring")]
        public bool ShowFPSRing
        {
            get => Configuration.ShowRing;
            set => Configuration.ShowRing = value;
        }

        [UIValue("use-colors")]
        public bool FPSUseColors
        {
            get => Configuration.UseColors;
            set => Configuration.UseColors = value;
        }
    }
}
