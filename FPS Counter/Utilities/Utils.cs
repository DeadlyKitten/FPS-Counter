using IPA.Loader;
using IPA.Old;

namespace FPS_Counter.Utilities
{
    public class Utils
    {
        /// <summary>
        /// Check if a BSIPA plugin is enabled
        /// </summary>
        public static bool IsPluginEnabled(string pluginName)
        {
            if (IsPluginPresent(pluginName))
            {
                PluginLoader.PluginInfo pluginInfo = PluginManager.GetPluginFromId(pluginName);
                if (pluginInfo?.Metadata != null)
                {
                    return PluginManager.IsEnabled(pluginInfo.Metadata);
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a plugin exists
        /// </summary>
        public static bool IsPluginPresent(string pluginName)
        {
            // Check in BSIPA
            if (PluginManager.GetPlugin(pluginName) != null ||
                PluginManager.GetPluginFromId(pluginName) != null)
            {
                return true;
            }

#pragma warning disable CS0618 // IPA is obsolete
            // Check in old IPA
            foreach (IPlugin plugin in PluginManager.Plugins)
            {
                if (plugin.Name == pluginName)
                {
                    return true;
                }
            }
#pragma warning restore CS0618 // IPA is obsolete

            return false;
        }
    }
}
