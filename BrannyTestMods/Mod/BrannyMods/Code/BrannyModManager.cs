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
using static Config;

namespace BrannyLeaderboard
{
    [ModEntry]
    public class BrannyModManager : MonoBehaviour
    {
        public const string pluginName = "Branny's Mods";
        public const string pluginVersion = "0.1";
        public const string id = "branny.wbox.expansion.leaderboard";

        public Harmony harmony;
        static bool initialized = false;

        public static BrannyFoundation foundation;

        void Awake() 
        {
            foundation = new BrannyFoundation();
            foundation.init();

            initialized = true;
        }

        // Call update on any enabled extensions;
        void Update() 
        {
            foundation.Update();
        }
    }
}
