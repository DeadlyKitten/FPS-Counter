using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace FPS_Counter.Settings
{
    internal class Configuration
    {
        public static Configuration Instance { get; set; }

        public virtual float UpdateRate { get; set; } = 0.5f;
        public virtual bool ShowRing { get; set; } = true;
        public virtual bool UseColors { get; set; } = true;
    }
}
