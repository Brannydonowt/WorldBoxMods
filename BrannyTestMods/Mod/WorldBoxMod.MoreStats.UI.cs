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
        public static GameObject brannyCanvas;
        public static GameObject statParent;
        public static GameObject statEntry;

        bool ui_initialized;

        static List<ButtonInteraction> createdButtons;

        void init_ui()
        {
            if (!assets_initialised)
                return;

            Debug.Log("Initialising UI");

            createdButtons = new List<ButtonInteraction>();

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

                RefreshStatUI();
            }
        }

        static void RefreshStatUI() 
        {
            if (createdButtons.Count == 0)
                return;

            foreach (ButtonInteraction b in createdButtons) 
            {
                switch(b.customData[0])
                {
                    case "Most Kills":
                        UpdateMostRuthless(b.myActorID);
                        Debug.Log("Updating most kills");
                        break;
                    default:
                        Debug.Log("Custom Data = " + b.customData[0]);
                        break;
                }
            }
        }

        static void UpdateStatUI(string statName, string actorId, string[] stats) 
        {
            BrannyActor actor = BrannyActorManager.GetRememberedActor(actorId);
            //Actor a = MapBox.instance.getActorByID(actorId);
            ActorStatus data = actor.getActorStatus();

            GameObject entry = GetStatEntryWithName(statName);

            string[] cData = new string[1];
            cData[0] = statName;

            ButtonInteraction button = entry.GetComponent<ButtonInteraction>();

            button.Setup();
            if (actor.alive)
                button.trackActor(actor.actorID);
            else
                button.GetComponent<Button>().interactable = false;

            button.AddCustomData(cData);

            entry.name = statName;
            Image iconImg = entry.transform.GetChild(0).GetComponent<Image>();
            Text titleText = entry.transform.GetChild(1).GetComponent<Text>();
            Text detailsText = entry.transform.GetChild(2).GetComponent<Text>();
            Text statusText = entry.transform.GetChild(3).GetComponent<Text>();

            titleText.text = statName;
            detailsText.text = format_details_string(stats);
            statusText.text = format_status_string(data);
        }

        static void UpdateMostRuthless(string actorId) 
        {
            BrannyActor actor = BrannyActorManager.GetRememberedActor(actorId);
            //Actor a = MapBox.instance.getActorByID(actorId);
            ActorStatus data = actor.getActorStatus();
            // There's a chance the actor has died
            if (actor == null)
            {
                Debug.Log("Actor appears to have died, getting remembered state");
            }

            if (data == null) 
            {
                Debug.Log("We don't have any Branny actor data.");
                return;
            }
            
            string[] killerstats = new string[3];
            killerstats[0] = data.firstName;
            killerstats[1] = actor.kingdom;
            killerstats[2] = data.kills.ToString();

            UpdateStatUI("Most Kills", actorId, killerstats);
        }

        static GameObject CreateStatEntry() 
        {
            Debug.Log("Creating a new stat entry");
            GameObject entry = Instantiate(statEntry, statParent.transform);
            entry.AddComponent<ButtonInteraction>();
            createdButtons.Add(entry.GetComponent<ButtonInteraction>());
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


        static string format_status_string(ActorStatus status) 
        {
            string result = "";
            string age = status.age.ToString();
            string born = status.bornTime.ToString();
            if (status.alive)
            {
                result = "Alive, Age: " + age;
            }
            else 
            {
                result = "Dead, Y" + born + "-" + (born + age).ToString();
            }

            return result;
        }
	}
}
