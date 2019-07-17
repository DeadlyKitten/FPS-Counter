using IPA;
using UnityEngine.SceneManagement;
using Logger = FPS_Counter.Logger;
using CountersPlus.Custom;

namespace FPS_Counter
{
    public class Plugin : IBeatSaberPlugin
    {
        public void Init() { }

        public void OnApplicationStart() { }

        public void OnApplicationQuit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) { }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) { }

        public void OnSceneUnloaded(Scene scene) { }

    }
}
