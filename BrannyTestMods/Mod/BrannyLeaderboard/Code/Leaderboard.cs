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
using BrannyCore;

namespace BrannyLeaderboard
{
    [ModEntry]
    public partial class Leaderboard : MonoBehaviour
    {
        public const string pluginName = "Branny's World Leaderboard";
        public const string pluginVersion = "0.1";
        public const string id = "branny.wbox.expansion.leaderboard";

        public Harmony harmony;
        public static bool initialized = false;

        public static Leaderboard instance;
        public BrannyFoundation foundation;

        void Awake() 
        {
 
        }

        public void Update() 
        {
            if (!initialized) { Debug.Log("Leaderboard not yet initialized"); return; }

            update_ui();
        }

        void update_ui()
        {
            if (!ui_initialized)
            {
                Debug.Log("UI not initialized");
                init_ui();
                return;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                // This will display Branny UI
                brannyCanvas.SetActive(!brannyCanvas.activeSelf);
                statParent.gameObject.SetActive(brannyCanvas.activeSelf);
            }
        }

        public void init(BrannyFoundation bf)
        {
            Debug.Log("Initializing Branny Leaderboard");

            instance = this;
            foundation = bf;

            instance.init_traits();
            instance.init_ui();

            Debug.Log("Branny Leaderboard, initialized!");
            initialized = true;
        }

        public static void Patching(Harmony harmony)
        {
            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(Actor), "increaseKillCount"), AccessTools.Method(typeof(Leaderboard), "increaseKillCount_postfix"));
            Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(Actor), "consumeCityFoodItem"), AccessTools.Method(typeof(Leaderboard), "consumeCityFoodItem_postfix"));

            Debug.Log("PostFix stats_patch DONE");
        }
    }
}
