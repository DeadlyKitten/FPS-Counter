using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS_Counter
{
    static class Config
    {
        static BS_Utils.Utilities.Config config;

        static readonly string configName = "FPS Counter";
        static readonly string sectionCore = "Settings";

        internal static void Init()
        {
            config = new BS_Utils.Utilities.Config(configName);
        }

        static readonly string updateRate = "UpdateRate";
        internal static float UpdateRate
        {
            get
            {
                return config.GetFloat(sectionCore, updateRate, 0.5f, true);
            }
            set
            {
                config.SetFloat(sectionCore, updateRate, value);
            }
        }

        static readonly string showRing = "ShowRing";
        internal static bool ShowRing
        {
            get
            {
                return config.GetBool(sectionCore, showRing, true, true);
            }
            set
            {
                config.SetBool(sectionCore, showRing, value);
            }
        }

        static readonly string useColors = "ShowRing";
        internal static bool UseColors
        {
            get
            {
                return config.GetBool(sectionCore, useColors, true, true);
            }
            set
            {
                config.SetBool(sectionCore, useColors, value);
            }
        }
    }
}
