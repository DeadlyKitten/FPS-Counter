using System;
using System.Reflection;
using BeatSaberMarkupLanguage.Settings;
using CountersPlus.Custom;
using FPS_Counter.Settings;
using FPS_Counter.Settings.UI;
using FPS_Counter.Utilities;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace FPS_Counter
{
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		private static PluginMetadata _metadata;
		private static string? _name;
		private static Version? _version;

		internal static bool IsCountersPlusPresent { get; set; }
		public static string PluginName => _name ??= _metadata?.Name ?? Assembly.GetExecutingAssembly().GetName().Name;


		[Init]
		public void Init(IPALogger logger, PluginMetadata metaData, Config config)
		{
			_metadata = metaData;
			Logger.log = logger;

			Configuration.Instance = config.Generated<Configuration>();
		}

		[OnStart]
		public void OnStart()
		{
			Logger.log.Info("Checking for Counters+");
			if (Utils.IsPluginEnabled("Counters+"))
			{
				IsCountersPlusPresent = true;
				AddCustomCounter();
			}
			else
			{
				Logger.log.Warn("Counters+ not installed");
			}

			BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh += BSEventsOnlateMenuSceneLoadedFresh;
			BS_Utils.Utilities.BSEvents.gameSceneLoaded += BSEventsOngameSceneLoaded;
		}

		[OnExit]
		public void OnExit()
		{
			BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh -= BSEventsOnlateMenuSceneLoadedFresh;
			BS_Utils.Utilities.BSEvents.gameSceneLoaded -= BSEventsOngameSceneLoaded;
		}

		private void BSEventsOngameSceneLoaded()
		{
			new GameObject("FPS Counter").AddComponent<Behaviours.FPSCounter>();
		}

		private void BSEventsOnlateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
		{
			BSMLSettings.instance.AddSettingsMenu(PluginName, "FPS_Counter.Settings.UI.Views.mainsettings.bsml", MainSettings.instance);
		}

		private void AddCustomCounter()
		{
			Logger.log.Info("Creating Custom Counter");

			CustomCounter counter = new CustomCounter
			{
				SectionName = "fpsCounter",
				Name = PluginName,
				BSIPAMod = _metadata,
				Counter = PluginName,
				Icon_ResourceName = "FPS_Counter.Resources.icon.png"
			};

			CustomCounterCreator.Create(counter);
		}
	}
}