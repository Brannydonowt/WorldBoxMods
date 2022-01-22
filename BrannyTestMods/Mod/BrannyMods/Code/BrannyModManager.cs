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
using BrannyLeaderboard;
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
        static bool b_initialized = false;
        static bool e_initialized = false;

        public BrannyFoundation foundation;
        public Leaderboard leaderboard;

        void Awake()
        {
            setup_mod();

            harmony = new Harmony(id);
            Patching(harmony);

            init_core();
        }

        void setup_mod() 
        {
            foundation = new BrannyFoundation();
            leaderboard = new Leaderboard();
        }

        void Patching(Harmony harmony) 
        {
            foundation.Patching(harmony);
            leaderboard.Patching(harmony);
        }

        void init_core()
        {
            if (!foundation.init())
                return;

            b_initialized = true;

            init_extensions();
        }

        void init_extensions() 
        {
            leaderboard.init(foundation);

            e_initialized = true;
        }

        // Call update on any enabled extensions;
        void Update()
        {
            if (!b_initialized)
                init_core();

            foundation.Update();
            leaderboard.Update();
        }
    }
}
