//extern alias ncms;
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
        public GameObject brannyCanvas;
        public GameObject statParent;
        public GameObject statPanel;

        bool ui_initialized;

        void init_ui()
        {
            if (!assets_initialised)
                return;

            Debug.Log("Initialising UI");

            brannyCanvas = GetGameObjectFromAssetBundle("BrannyCanvas");
            Debug.Log("Got Canvas");
            statParent = brannyCanvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            Debug.Log("Got Parent");
            statPanel = GetGameObjectFromAssetBundle("StatEntry");
            Debug.Log("Got Entry");

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
                Debug.Log("Toggling Branny UI");
                brannyCanvas.SetActive(!brannyCanvas.activeSelf);
            }
        }

        void UpdateStatUI(string statName, string[] details) 
        {
            GameObject statEntry = GetStatEntryWithName(statName);
            statEntry.name = statName;
            Text titleText = statEntry.transform.GetChild(0).GetComponent<Text>();
            Text detailsText = statEntry.transform.GetChild(0).GetComponent<Text>();

            titleText.text = statName;
            detailsText.text = format_details_string(details);
        }

        GameObject CreateStatEntry() 
        {
            return Instantiate(statPanel, statParent.transform);
        }

        GameObject GetStatEntryWithName(string name) 
        {
            if (statParent.transform.Find(name))
                return statParent.transform.Find(name).gameObject;
            else
                return CreateStatEntry();
        }

        string format_details_string(string[] details) 
        {
            string result = "";

            foreach (string s in details)
                result = result + " - " + s;

            return result;
        }
	}
}
