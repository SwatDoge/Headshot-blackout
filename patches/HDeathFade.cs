using Comfort.Common;
using EFT;
using HarmonyLib;
using System;
using UnityEngine;
using System.Collections;
using HeadshotBlackout.curves;


namespace HeadshotBlackout.patches
{
    [HarmonyPatch(typeof(DeathFade))]
    static class HDeathFade
    {
        private static EFT.Player GetPlayer() => Singleton<GameWorld>.Instance.MainPlayer;

        // Modify death screen curve and start audio fadeout
        [HarmonyPostfix]
        [HarmonyPatch(nameof(DeathFade.EnableEffect))]
        static void PostEnableEffect(DeathFade __instance)
        {
            if (!Plugin.Config_Enabled.Value)
            {
                return;
            }

            Player player = GetPlayer();
            Type inst = typeof(DeathFade);

            if (player.LastDamagedBodyPart is EBodyPart.Head || player.LastDamageType is EDamageType.Explosion)
            {
                __instance.bool_0 = true;
                __instance._enableCurve = Curves.deathCurve;
                __instance.animationCurve_0 = Curves.deathCurve;
            }

            __instance.StartCoroutine(DeathAudioFade(Curves.deathAudioCurve));
        }

        //Audio fade out
        static IEnumerator DeathAudioFade(AnimationCurve curve)
        {
            float currentTime = 0;
            while (currentTime <= curve.GetDuration())
            {
                AudioListener.volume = curve.Evaluate(currentTime);
                currentTime += Time.deltaTime;
                yield return null;
            }
            yield break; 
        }
    }
}
