using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using LC_API;
using UnityEngine;


namespace Scarybugs.Patches
{
    [HarmonyPatch]
    class ArachnophobiaTerminalPatch
    {
        [HarmonyPatch(typeof(Terminal), "Awake")]
        [HarmonyPostfix]
        public static void EditTerminal(Terminal __instance)
        {
            __instance.enemyFiles[12].displayVideo = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<UnityEngine.Video.VideoClip>("assets/Gok.m4v");
        }
    }
}
