using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using LC_API;
using UnityEngine;
using HarmonyLib.Tools;
using BepInEx.Logging;
using System.Linq;

namespace Scarybugs.Patches
{
    [HarmonyPatch]
    internal class Centipede
    {
        [HarmonyPatch(typeof(CentipedeAI), "Start")]
        [HarmonyPostfix]

        public static void SummonBug(CentipedeAI __instance)
        {
            __instance.hitCentipede = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/bugDie.wav");
            __instance.fallShriek = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/beesAngry.wav");
            UnityEngine.Object.Destroy(__instance.gameObject.transform.Find("CentipedeModel").Find("LOD1").gameObject.GetComponent<SkinnedMeshRenderer>());
            UnityEngine.Object.Destroy(__instance.gameObject.transform.Find("CentipedeModel").Find("LOD2").gameObject.GetComponent<SkinnedMeshRenderer>());
            GameObject Bug = UnityEngine.Object.Instantiate(LC_API.BundleAPI.BundleLoader.GetLoadedAsset<GameObject>("Assets/Stored Asset Bundles/centipedeReplacement.prefab"), __instance.gameObject.transform);

            if (UnityEngine.Object.FindObjectsOfType<CentipedeAI>().Any(centipede => centipede.clingingToPlayer != null)) // THANKS TO callmeverity ON DISCORD FOR THIS CODE
            {
                Bug.transform.localPosition = new Vector3(0f, 0f, 0f); // Centipede is on anyones head move local to 0, 0, 0
            }
            else
            {
                Bug.transform.localPosition = new Vector3(0f, 0.8f, 0f);
            }
        }
    }
}
