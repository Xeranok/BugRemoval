using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

namespace FlowerMorgan.Patches
{
    [HarmonyPatch(typeof(FlowermanAI))]
    internal class FlowerMorganPatch
    {
        private bool ___wasInEvadeMode;
        private class FlowermanAudioInfo
        {
            public bool IsPlaying { get; set; }

            public AudioSource AudioSource { get; set; }
        }

        private static Dictionary<FlowermanAI, FlowermanAudioInfo> flowermanAudioInfoMap = new Dictionary<FlowermanAI, FlowermanAudioInfo>();

        [HarmonyPatch("ChooseClosestNodeToPlayer")]
        [HarmonyPrefix]
        private static async void TrackPatch(FlowermanAI __instance)
        {
            if (!flowermanAudioInfoMap.TryGetValue(__instance, out var flowermanAudioInfo))
            {
                flowermanAudioInfo = new FlowermanAudioInfo
                {
                    IsPlaying = false,
                    AudioSource = ((Component)__instance).gameObject.AddComponent<AudioSource>()
                };
                flowermanAudioInfo.AudioSource.spatialBlend = 1f;
                flowermanAudioInfo.AudioSource.dopplerLevel = 1f;
                flowermanAudioInfo.AudioSource.rolloffMode = (AudioRolloffMode)0;
                flowermanAudioInfo.AudioSource.minDistance = 5f;
                flowermanAudioInfo.AudioSource.maxDistance = 50f;
                flowermanAudioInfo.AudioSource.volume = 0.8f;
                flowermanAudioInfoMap[__instance] = flowermanAudioInfo;
            }
            if (___wasInEvadeMode && flowermanAudioInfo.IsPlaying)
            {
                flowermanAudioInfo.AudioSource.Stop();
                flowermanAudioInfo.IsPlaying = false;
            }
            if (!___wasInEvadeMode && !flowermanAudioInfo.IsPlaying && ((EnemyAI)__instance).mostOptimalDistance <= 50f)
            {
                flowermanAudioInfo.IsPlaying = true;
                string path = "file://" + Paths.PluginPath + "\\sounds\\smell.mp3";
                UnityWebRequest audioClip = UnityWebRequestMultimedia.GetAudioClip(path, (AudioType)13);
                audioClip.SendWebRequest();
                while (!audioClip.isDone)
                {
                }
                AudioClip clip = DownloadHandlerAudioClip.GetContent(audioClip);
                flowermanAudioInfo.AudioSource.clip = clip;
                flowermanAudioInfo.AudioSource.PlayOneShot(clip);
                System.Random random = new System.Random();
                double randomDelay = random.NextDouble() * 5.0 + 5.0;
                await Task.Run(async delegate
                {
                    await Task.Delay(TimeSpan.FromSeconds((double)clip.length + randomDelay));
                    flowermanAudioInfo.AudioSource.Stop();
                    flowermanAudioInfo.IsPlaying = false;
                });
            }
        }
        internal static AudioClip[] newsfx;
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void FlowerMorganAudioPatch(ref AudioClip[] ___crackNeckSFX)
        {
            AudioClip newSFX = LoadAudioClip(Paths.PluginPath + "sounds/rehehe.mp3");
            ___crackNeckSFX[0] = newSFX;
        }

        private static AudioClip LoadAudioClip(string filepath)
        {
            UnityWebRequest audioClip = UnityWebRequestMultimedia.GetAudioClip(filepath, (AudioType)13);
            audioClip.SendWebRequest();
            while (!audioClip.isDone)
            {
            }
            AudioClip content = DownloadHandlerAudioClip.GetContent(audioClip);
            return content;
        }
    }