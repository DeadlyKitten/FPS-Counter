using BeatSaberMarkupLanguage.Attributes;

namespace FPS_Counter.Settings.UI
{
    internal class MainSettings : PersistentSingleton<MainSettings>
    {
        [UIValue("update-rate")]
        public float FPSUpdateRate
        {
            get => Configuration.Instance.UpdateRate;
            set => Configuration.Instance.UpdateRate = value;
        }

        [UIValue("show-ring")]
        public bool ShowFPSRing
        {
            get => Configuration.Instance.ShowRing;
            set => Configuration.Instance.ShowRing = value;
        }

        [UIValue("use-colors")]
        public bool FPSUseColors
        {
            get => Configuration.Instance.UseColors;
            set => Configuration.Instance.UseColors = value;
        }
    }
}
