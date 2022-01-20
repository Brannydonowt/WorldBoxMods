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

        public void Awake()
        {
            // We want the mod to have an instance
            init();
        }

        public void Update() 
        {
            if (!gameLoaded) return;

            if (!initialized) return;
        }

        public bool init()
        {
            instance = this;

            Debug.Log("Initializing Branny Core");
            init_assets();
            init_ui();

            Debug.Log("Branny Core, initialized!");

            initialized = true;
            return true;
        }

        void init_extensions() 
        {

        }

        public void CloseAllUI()
        {
            brannyCanvas.SetActive(false);
        }

        public static void Patching(Harmony harmony)
        {
            BrannyActorManager.Branny_Actor_Patch(harmony);
        }
    }
}
