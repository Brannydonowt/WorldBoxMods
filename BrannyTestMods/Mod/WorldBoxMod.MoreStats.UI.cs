﻿//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;

namespace BrannyTestMods
{
    public partial class WorldBoxMod
    {
        public static GameObject brannyCanvas;
        public static GameObject statParent;
        public static GameObject statEntry;

        bool ui_initialized;

        void init_ui()
        {
            if (!assets_initialised)
                return;

            Debug.Log("Initialising UI");

            brannyCanvas = GetGameObjectFromAssetBundle("BrannyCanvas");
            statParent = brannyCanvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            Debug.Log(statParent.name);
            statEntry = GetGameObjectFromAssetBundle("StatEntry");

            brannyCanvas.SetActive(false);

            ui_initialized = true;
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
                statParent.SetActive(brannyCanvas.activeSelf);
            }
        }

        static void UpdateStatUI(string statName, string actorId, string[] stats) 
        {
            Actor a = MapBox.instance.getActorByID(actorId);

            GameObject entry = GetStatEntryWithName(statName);
            entry.AddComponent<StatInteraction>();
            entry.GetComponent<StatInteraction>().trackActor(a);
            entry.GetComponent<StatInteraction>().myActor = a;
            entry.name = statName;
            Text titleText = entry.transform.GetChild(0).GetComponent<Text>();
            Text detailsText = entry.transform.GetChild(1).GetComponent<Text>();

            titleText.text = statName;
            detailsText.text = format_details_string(stats);
        }

        static void UpdateMostRuthless(string actorId) 
        {
            Actor a = MapBox.instance.getActorByID(actorId);

            var data = Helper.Reflection.GetActorData(a);

            string[] killerstats = new string[3];
            killerstats[0] = data.firstName;
            killerstats[1] = a.kingdom.name;
            killerstats[2] = data.kills.ToString();

            UpdateStatUI("Most Kills", data.actorID, killerstats);
        }

        static GameObject CreateStatEntry() 
        {
            Debug.Log("Creating a new stat entry");
            GameObject entry = Instantiate(statEntry, statParent.transform);
            entry.SetActive(true);
            return entry;
        }

        static GameObject GetStatEntryWithName(string name) 
        {
            if (statParent.transform.Find(name))
            {
                return statParent.transform.Find(name).gameObject;
            }
            else
                return CreateStatEntry();
        }

        static string format_details_string(string[] details) 
        {
            return details[0] + " - " + details[1] + " - Kills: " + details[2];
        }
	}
}
