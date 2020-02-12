using BeatSaberMarkupLanguage.Settings;
using CountersPlus.Custom;
using FPS_Counter.Settings;
using FPS_Counter.Settings.UI;
using FPS_Counter.Utilities;
using IPA;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace FPS_Counter
{
    public class Plugin : IBeatSaberPlugin
    {
        public static string PluginName => "FPS Counter";

        public void Init(IPALogger logger)
        {
            Logger.log = logger;
            Configuration.Init();
        }

        public void OnApplicationStart() => Load();
        public void OnApplicationQuit() => Unload();

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (Utils.IsPluginEnabled("Counters+"))
            {
                if (nextScene.name == "GameCore")
                {
                    new GameObject("FPS Counter").AddComponent<Behaviours.FPSCounter>();
                }
                else if (nextScene.name == "MenuViewControllers" && prevScene.name == "EmptyTransition")
                {
                    BSMLSettings.instance.AddSettingsMenu(PluginName, "FPS_Counter.Settings.UI.Views.mainsettings.bsml", MainSettings.instance);
                }
            }
        }

        public void OnUpdate() { }
        public void OnFixedUpdate() { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) { }
        public void OnSceneUnloaded(Scene scene) { }

        private void Load()
        {
            Configuration.Load();

            Logger.log.Info("Checking for Counters+");
            if (Utils.IsPluginEnabled("Counters+"))
            {
                AddCustomCounter();
            }
            else
            {
                Logger.log.Error("Counters+ not installed");
            }
        }

        private void Unload()
        {
            Configuration.Save();
        }

        private void AddCustomCounter()
        {
            Logger.log.Info("Creating Custom Counter");

            CustomCounter counter = new CustomCounter
            {
                SectionName = "fpsCounter",
                Name = "FPS Counter",
                BSIPAMod = this,
                Counter = "FPS Counter",
                Icon_ResourceName = "FPS_Counter.Resources.icon.png"
            };

            CustomCounterCreator.Create(counter);
        }
    }
}
