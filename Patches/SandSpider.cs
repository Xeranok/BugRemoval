using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using LC_API;
using UnityEngine;
using BepInEx;
using JetBrains.Annotations;

namespace Scarybugs.Patches
{
    [HarmonyPatch]
    class SandSpider

    {


        [HarmonyPatch(typeof(SandSpiderAI), "Start")]
        [HarmonyPostfix]

        public static void SummonBug(SandSpiderAI __instance)
        {
            int X = 0;
            foreach (var item in __instance.footstepSFX)
            {
                __instance.footstepSFX[X] = LC_API.BundleAPI.BundleLoader.GetLoadedAsset<AudioClip>("Assets/Stored Asset Bundles/spiderStep.wav");
                X++;
            }
            Transform spiderHead = __instance.gameObject.transform.Find("MeshContainer/AnimContainer/Armature/Abdomen");
            __instance.gameObject.transform.Find("MeshContainer/MeshRenderer").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject Spider = UnityEngine.Object.Instantiate(LC_API.BundleAPI.BundleLoader.GetLoadedAsset<GameObject>("Assets/Stored Asset Bundles/spiderReplacement.prefab"), spiderHead);
            Transform spiderAngle = __instance.gameObject.transform.Find("MeshContainer/AnimContainer/Armature/Abdomen/spiderReplacement(Clone)").transform;
            Spider.transform.localPosition = new Vector3(0f, 1f, 0f);
            Vector3 angles = Spider.transform.localEulerAngles;
            spiderAngle.localEulerAngles = new Vector3(angles.x, angles.y, 90f);

        }
    }
}
