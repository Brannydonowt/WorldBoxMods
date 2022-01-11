extern alias ncms;
using NCMS = ncms.NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;
using BepInEx;
using static Config;

namespace BrannyTestMods
{
    [BepInPlugin(id, "BrannyTests", "0.1")]
    //[ModEntry]
    public partial class WorldBoxMod : BaseUnityPlugin
    {
        public const string pluginName = "Branny's Test Mod";
        public const string pluginVersion = "0.1";
        public const string id = "branny.testmod";

        public Harmony harmony;

        static bool initialized = false;

        public void Awake()
        {
            harmony = new Harmony(id);
            Helper.GodPowerTab.patch(harmony);
            Patching(harmony);
        }

        void Update() 
        {
            if (!gameLoaded) return;

            if (!initialized) 
            {
                init();
            }
            initialized = true;

            if (Input.GetKeyDown(KeyCode.G)) 
            {
                var human = AssetManager.unitStats.get("unit_human");
                human.traits.Add("Hedgehog");
            }
        }

        void init() 
        {
            Debug.Log("Branny mod, running!");
            initTraits();
            initDrops();
        }

        private void Patching(Harmony harmony)
        {

        }
    }
}
