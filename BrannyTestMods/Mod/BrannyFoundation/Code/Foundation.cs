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

        public bool initialized = false;

        public static BrannyFoundation instance;
        public Leaderboard leaderboard;

        public void Awake()
        {
            // We want the mod to have an instance
            init();
        }

        public void Update() 
        {
            if (!gameLoaded) return;

            if (!initialized) return;

            leaderboard.Update();
        }

        public void init()
        {
            instance = this;

            harmony = new Harmony(id);
            Patching(harmony);

            Debug.Log("Initializing Branny Core");
            init_assets();
            init_ui();

            initialized = true;

            Debug.Log("Branny Core, initialized!");
            Debug.Log("Branny Core : Initializaing extensions");

            init_extensions();
        }

        void init_extensions() 
        {
            leaderboard = new Leaderboard();
            leaderboard.init(this);
        }

        public void CloseAllUI()
        {
            brannyCanvas.SetActive(false);
        }

        private void Patching(Harmony harmony)
        {
            //stats_patch(harmony);
            BrannyActorManager.Branny_Actor_Patch(harmony);
        }
    }
}
