using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using LC_API;
using UnityEngine;
using BepInEx;

namespace Scarybugs.Patches
{
    [HarmonyPatch]
    class Bees

    {
        [HarmonyPatch(typeof(RedLocustBees), "Start")]
        [HarmonyPostfix]
        public static void Buzz(RedLocustBees __instance)
        {
            __instance.beesAngry.clip = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/beesAngry.wav");
            __instance.beesDefensive.clip = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/beesDefensive.wav");
            __instance.beesIdle.clip = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/beesIdle.wav");
            __instance.beeZapAudio.clip = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/beeZapAudio.wav");
        }
    }

}