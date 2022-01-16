//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using HarmonyLib;

using BrannyLeaderboard;

using static Config;

namespace BrannyCore
{
    [ModEntry]
    public partial class BrannyFoundation : MonoBehaviour
    {
        public const string pluginName = "Branny's Expansion Mod";
        public const string pluginVersion = "0.1";
        public const string id = "branny.wbox.expansion";

        public Harmony harmony;

        static bool initialized = false;

        public static BrannyFoundation instance;

        public void Awake()
        {
            // We want the mod to have an instance
            instance = this;

            harmony = new Harmony(id);
            Patching(harmony);

            instance.init();
        }

        void Update() 
        {
            if (!gameLoaded) return;

            if (!initialized) return;

            initialized = true;
        }

        void init()
        {
            Debug.Log("Initializing Branny Core");
            init_assets();
            init_ui();

            init_extensions();

            Debug.Log("Branny Core, initialized!");
        }

        public void CloseAllUI()
        {
            //brannyCanvas.SetActive(false);
        }

        void init_extensions() 
        {
            Leaderboard.instance.init();
        }

        private void Patching(Harmony harmony)
        {
            //stats_patch(harmony);
            BrannyActorManager.Branny_Actor_Patch(harmony);
        }
    }
}
