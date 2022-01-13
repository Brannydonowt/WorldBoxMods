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
        public static GameObject statList;
        public static GameObject statListEntry;

        bool ui_initialized;

        static List<ButtonInteraction> createdButtons;

        static Dictionary<string, int> 

        void init_ui()
        {
            if (!assets_initialised)
                return;

            Debug.Log("Initialising UI");

            createdButtons = new List<ButtonInteraction>();

            brannyCanvas = GetGameObjectFromAssetBundle("BrannyCanvas");
            statParent = brannyCanvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            statEntry = GetGameObjectFromAssetBundle("StatEntry");
            statList = GetGameObjectFromAssetBundle("StatList");
            statListEntry = GetGameObjectFromAssetBundle("StatListEntry");

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
                        UpdateMostRuthless(b.myActorID, 0);
                        Debug.Log("Updating most kills");
                        break;
                    default:
                        Debug.Log("Custom Data = " + b.customData[0]);
                        break;
                }
            }
        }

        static void CreateNewStatLeaderboard(string type, List<LeaderboardEntry> leaderboard) 
        {
            
        }

        // Creates a new entry for a stat leaderboard at given position
        static void CreateNewStatLeaderboardEntry(string actorId, int position) 
        {
            
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
                button.trackActor(actor._id);
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
            statusText.text = format_status_string(actor);
        }

        static void UpdateMostRuthless(string actorId, int position)
        {
            BrannyActor actor = BrannyActorManager.GetRememberedActor(actorId);
            //Actor a = MapBox.instance.getActorByID(actorId);
            ActorStatus data = actor.getActorStatus();
            // There's a chance the actor has died

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


        // TODO - Add YearBorn-YearDeath
        static string format_status_string(BrannyActor actor) 
        {
            ActorStatus status = actor.getActorStatus();

            string result = "";
            string age = status.age.ToString();
            string born = status.bornTime.ToString();
            //MapBox.instance.mapStats.

            if (status.alive)
            {
                result = "Alive, Age: " + age;
            }
            else 
            {
                result = "Dead";
            }

            return result;
        }
	}
}
