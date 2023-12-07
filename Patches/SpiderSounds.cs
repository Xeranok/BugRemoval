using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using LC_API;
using UnityEngine;
using BepInEx;

namespace ArachnophobiaMod.patches
{
    [HarmonyPatch]
    class SpiderSounds

    {
        [HarmonyPatch(typeof(SandSpiderAI), "Start")]
        [HarmonyPostfix]
        public static void SpiderSounds(ref AudioClip[] ___footstepSFX)
        {
            AudioClip newSFX = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("assets/stored asset bundles/spiderStep.mp3");
            ___footstepSFX[0] = newSFX;
        }
    }