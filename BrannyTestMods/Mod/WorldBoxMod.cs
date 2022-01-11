//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;
//using BepInEx;
using static Config;

namespace BrannyTestMods
{
    //[BepInPlugin(id, "BrannyTests", "0.1")]
    [ModEntry]
    public partial class WorldBoxMod : MonoBehaviour
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

            Helper.Localization.addLocalization("kill_lead_new", "There $wbcode$ is $discord_count$ a new $number$ kill $total_prem_powers$ leader!");
        }

        private void Patching(Harmony harmony)
        {
            // Non working example
            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(ActorTraitLibrary), "add"), AccessTools.Method(typeof(WorldBoxMod), "addTraits_postfix"));
            Debug.Log("PostFix TraitLibrary DONE");

            stats_patch(harmony);
        }

        public static void addTraits_postfix(ActorTraitLibrary __instance) 
        {
            ActorTrait newTrait = __instance.list[__instance.list.Count() - 1];
            Debug.Log("Added Trait: " + newTrait.id);
        }
    }
}
