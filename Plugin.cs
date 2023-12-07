
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Scarybugs
{
    [BepInPlugin("com.xeranok.BugsBGone", "BugsBGone", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public Harmony harmonymain;
        private void Awake()
        {
            harmonymain = new Harmony("com.xeranok.BugsBGone");
            harmonymain.PatchAll();
            Logger.LogInfo($"consider the bugs gone.");
        }
    }
}
