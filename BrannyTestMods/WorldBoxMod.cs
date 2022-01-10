using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using HarmonyLib;

namespace BrannyTestMods
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class WorldBoxMod : BaseUnityPlugin
    {
        public const string pluginGuid = "branny.testmod";
        public const string pluginName = "Branny's Test Mod";
        public const string pluginVersion = "0.1";

        static bool initialized = false;

        void Update() 
        {
            if (!initialized) 
            {
                init();
            }
            initialized = true;

            if (Input.GetKeyDown(KeyCode.G)) 
            {
                var giantTrait = AssetManager.traits.get("giant");

                giantTrait.baseStats.scale += 1f; // default is 0.05f
                var human = AssetManager.unitStats.get("unit_human");
                human.traits.Add("giant");
            }
        }

        void init() 
        {
            Debug.Log("Branny mod, running!");
        }
    }
}
