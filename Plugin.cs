using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using EFT.UI;

namespace HeadshotBlackout
{
    [BepInPlugin("headshotblackout", "HeadshotBlackout", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        public static ConfigEntry<bool> Config_Enabled { get; private set; }

        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("Headshot blackout loaded!");

            new Harmony("harmony").PatchAll();
            Config_Enabled = Config.Bind("", "Blackout on/off", true, "Turn mod on or off");
        }
    }
}
