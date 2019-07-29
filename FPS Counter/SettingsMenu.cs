using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CustomUI;
using CustomUI.Settings;
using UnityEngine;

namespace FPS_Counter
{
    class SettingsMenu
    {
        internal static bool initialized;

        internal static void CreateSettingsUI()
        {
            if (initialized) return;
            initialized = true;

            Logger.Log("Creating Settings UI", Logger.LogLevel.Notice);

            var subMenuFPS = SettingsUI.CreateSubMenu("FPS Counter");

            var updateRateOption = subMenuFPS.AddList("Update Rate", Enumerable.Range(1, 20).Select(x => x * 0.1f).ToArray());
            updateRateOption.GetValue += delegate { return Config.UpdateRate; };
            updateRateOption.SetValue += delegate (float value) { Config.UpdateRate = value; };
            updateRateOption.FormatValue += delegate (float value) { return $"{value} seconds"; };

            var displayRingOption = subMenuFPS.AddBool("Display Ring");
            displayRingOption.GetValue += delegate { return Config.ShowRing; };
            displayRingOption.SetValue += delegate (bool value) { Config.ShowRing = value; };
            displayRingOption.EnabledText = "Yes";
            displayRingOption.DisabledText = "No";

            var useColorsOption = subMenuFPS.AddBool("Use Colors");
            useColorsOption.GetValue += delegate { return Config.UseColors; };
            useColorsOption.SetValue += delegate (bool value) { Config.UseColors = value; };
            useColorsOption.EnabledText = "Yes";
            useColorsOption.DisabledText = "No";
        }
    }
}

