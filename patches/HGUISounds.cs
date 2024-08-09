using Comfort.Common;
using EFT;
using EFT.UI;
using HarmonyLib;
using HeadshotBlackout.curves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadshotBlackout.patches
{
    [HarmonyPatch(typeof(GUISounds))]
    static class HGUISounds
    {
        private static EFT.Player GetPlayer() => Singleton<GameWorld>.Instance.MainPlayer;

        // Prevents death audio from playing
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GUISounds.PlayUISound))]
        static bool PrePlayUISound(EUISoundType soundType)
        {
            if (soundType is EUISoundType.PlayerIsDead && Plugin.Config_Enabled.Value)
            {
                Player player = GetPlayer();
                bool isBlackoutDeath = player.LastDamagedBodyPart is EBodyPart.Head || player.LastDamageType is EDamageType.Explosion;

                return !isBlackoutDeath;
            }

            return true;
        }
    }
}
