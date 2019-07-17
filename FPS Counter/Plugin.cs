using System.Linq;
using IPA;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = FPS_Counter.Logger;
using CountersPlus.Custom;

namespace FPS_Counter
{
    public class Plugin : IBeatSaberPlugin
    {
        public static bool CountersPlusInstalled { get; private set; } = false;
        private FPSCounter _counter;

        public void Init(IPA.Logging.Logger logger)
        {
            Logger.logger = logger;
            Config.Init();
        }

        public void OnApplicationStart()
        {
            Logger.Log("Checking for Counters+");
            if (IPA.Loader.PluginManager.AllPlugins.Any(x => x.Metadata.Id == "Counters+"))
            {
                Logger.Log("Counters+ is installed");
                CountersPlusInstalled = true;
                AddCustomCounter();
            }
            else
                Logger.Log("Counters+ not installed");
        }

        public void OnApplicationQuit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GameCore")
            {
                _counter = new GameObject("FPS Counter").AddComponent<FPSCounter>();
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuCore")
                SettingsMenu.CreateSettingsUI();
        }

        public void OnSceneUnloaded(Scene scene)
        {
            if (scene.name == "MenuCore")
                SettingsMenu.initialized = false;
        }

        void AddCustomCounter()
        {
            Logger.Log("Creating Custom Counter");
            CustomCounter counter = new CustomCounter
            {
                SectionName = "fpsCounter",
                Name = "FPS Counter",
                BSIPAMod = this,
                Counter = "FPS Counter",
            };
            CustomCounterCreator.Create(counter);
        }
    }
}