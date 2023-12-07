using BepInEx.Logging;
using HarmonyLib;
using HarmonyLib.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Scarybugs.Patches
{
    [HarmonyPatch]
    class Hoarder
    {

        [HarmonyPatch(typeof(HoarderBugAI), "Start")]
        [HarmonyPostfix]
        public static void SummonBug(HoarderBugAI __instance)
        {
            var chitter = new List<string>
            {
                "Assets/Stored Asset Bundles/hoarderBugSpottedA.wav",
                "Assets/Stored Asset Bundles/hoarderBugSpottedB.wav",
                "Assets/Stored Asset Bundles/hoarderBugSpottedC.wav"
            };

            int Z;
            string X;
            int C = 0;
            foreach (var audioClip in __instance.chitterSFX)
            {
                Z = Random.Range(0, 2);
                X = chitter[Z];
                __instance.chitterSFX[C] = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>(X);
                C++;

            }
            __instance.angryScreechSFX[0] = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/angryScreech.wav");
            __instance.angryScreechSFX[1] = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/angryScreech2.wav");
            __instance.angryVoiceSFX = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/fly.wav");
            __instance.bugFlySFX = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/haorderBugCry.wav");
            Object.Destroy(__instance.gameObject.transform.Find("HoarderBugModel").Find("Cube").gameObject.GetComponent<SkinnedMeshRenderer>());
            Object.Destroy(__instance.gameObject.transform.Find("HoarderBugModel").Find("Cube.001").gameObject.GetComponent<SkinnedMeshRenderer>());
            GameObject Hoarder = Object.Instantiate(LC_API.BundleAPI.BundleLoader.GetLoadedAsset<GameObject>("Assets/Stored Asset Bundles/hoarderReplacement.prefab"), __instance.gameObject.transform);
            Hoarder.transform.localPosition = new Vector3(0f, 2.5f, 0f);

        }
    }
}