using System.Linq;
using IPA;
using UnityEngine.SceneManagement;
using Logger = FPS_Counter.Logger;
using CountersPlus.Custom;

namespace FPS_Counter
{
    public class Plugin : IBeatSaberPlugin
    {
        public static bool CountersPlusInstalled { get; private set; } = false;

        public void Init()
        {
            if (IPA.Loader.PluginManager.AllPlugins.Any(x => x.Metadata.Id == "Counters+"))
            {
                CountersPlusInstalled = true;
                AddCustomCounter();
            }

        }

        public void OnApplicationStart() { }

        public void OnApplicationQuit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) { }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) { }

        public void OnSceneUnloaded(Scene scene) { }

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
