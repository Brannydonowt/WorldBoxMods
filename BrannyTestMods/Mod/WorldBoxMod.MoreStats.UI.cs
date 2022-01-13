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

        static List<GameObject> createdElements = new List<GameObject>();

        static List<ButtonInteraction> createdButtons;

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

        static void UpdateStatLeaderboard(string type, List<LeaderboardEntry> leaderboard) 
        {
            if (statParent.transform.Find(type))
            {
                Debug.Log("We already have a leaderboard, we need to update the stats");

                GameObject list = statParent.transform.Find(type).gameObject;
                UnfoldList myList = list.transform.GetChild(0).gameObject.AddComponent<UnfoldList>();
                
                if (myList.open)
                    myList.UnfoldPanel();

                // Should get all children
                foreach (Transform child in list.transform.GetChild(1)) 
                {
                    Destroy(child.gameObject);
                }

                Debug.Log("Stepping through new leaderboard");
                int step = 0;
                foreach (LeaderboardEntry l in leaderboard)
                {
                    Debug.Log(step + ": " + "POS: " + l.position + " STAT: " + l.statValue + " INDEX: " + leaderboard.IndexOf(l));
                    step++;

                    l.UpdatePosition(leaderboard.IndexOf(l));
                    GameObject entry = CreateNewStatLeaderboardEntry(l, type);
                    entry.transform.SetParent(list.transform.GetChild(1));
                    entry.transform.SetSiblingIndex(leaderboard.IndexOf(l));
                    entry.SetActive(true);
                }
            }
            else 
            {
                CreateNewStatLeaderboard(type, leaderboard);
            }
        }

        static GameObject CreateNewStatLeaderboard(string type, List<LeaderboardEntry> leaderboard)
        {
            if (statParent.transform.Find(type)) 
            {
                Debug.Log("Destroying existing parent");
                Destroy(statParent.transform.Find(type).gameObject);
            }

            GameObject list = Instantiate(statList, statParent.transform);
            ButtonInteraction button = list.transform.GetChild(0).gameObject.AddComponent<ButtonInteraction>();
            button.Setup();
            UnfoldList myList = list.transform.GetChild(0).gameObject.AddComponent<UnfoldList>();
            myList.Setup();
            button.AddListener(myList.gameObject);
            list.SetActive(true);
            list.name = type;
            CustomiseStatList(list.transform, type);
            
            createdElements.Add(list);
            
            foreach (LeaderboardEntry l in leaderboard) 
            {
                l.UpdatePosition(leaderboard.IndexOf(l));
                GameObject entry = CreateNewStatLeaderboardEntry(l, type);
                entry.transform.SetParent(list.transform.GetChild(1));
                entry.transform.SetSiblingIndex(l.position);
                entry.SetActive(true);
            }

            return list;
        }

        // Creates a new entry for a stat leaderboard at given position
        static GameObject CreateNewStatLeaderboardEntry(LeaderboardEntry l, string type) 
        {
            int statName = l.statValue;
;
            BrannyActor bActor = BrannyActorManager.GetRememberedActor(l.actorId);

            GameObject entry = Instantiate(statListEntry);
            entry.AddComponent<ButtonInteraction>();
            ButtonInteraction button = entry.GetComponent<ButtonInteraction>();
            entry.SetActive(true);

            CustomiseStatListEntry(entry.transform, type, l);

            string[] cData = new string[1];
            cData[0] = type;

            button.Setup();
            button.trackActor(l.actorId);
            button.AddCustomData(cData);

            return entry;
        }

        static void CustomiseStatList(Transform listEntry, string type) 
        {
            Transform root = listEntry.GetChild(0);
            Image icon = root.GetChild(0).gameObject.GetComponent<Image>();
            Text title = root.GetChild(1).gameObject.GetComponent<Text>();
            Text details = root.GetChild(2).gameObject.GetComponent<Text>();
            Text alive = root.GetChild(3).gameObject.GetComponent<Text>();

            switch (type) 
            {
                case "Kills":
                    title.text = "Top Killers";
                    details.text = "Grants the \"BloodThirsty\" Trait";
                    alive.text = "";
                    break; 
                default:
                    title.text = "broken";
                    details.text = "This is broken, please let the dev know what you expect it to be.";
                    alive.text = "";     
                    break;
            }
        }

        static void CustomiseStatListEntry(Transform listEntry, string type, LeaderboardEntry l)
        {
            string statDisplayName = "";

            switch (type)
            {
                case "Kills":
                    statDisplayName = "Most Kills";
                    break;
                default:
                    statDisplayName = "Something has gone wrong...";
                    break;
            }

            BrannyActor bActor = BrannyActorManager.GetRememberedActor(l.actorId);
            ActorStatus data = bActor.getActorStatus();

            Text rank = listEntry.GetChild(0).gameObject.GetComponent<Text>();
            Text name = listEntry.GetChild(1).gameObject.GetComponent<Text>();
            Text statAmount = listEntry.GetChild(2).gameObject.GetComponent<Text>();
            Text status = listEntry.GetChild(3).gameObject.GetComponent<Text>();

            int rankValue = l.position + 1;
            rank.text = "#" + rankValue;
            name.text = data.firstName;
            statAmount.text = l.statValue + " Kills";
            if (bActor.alive)
                status.text = "Alive";
            else
                status.text = "Dead";
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
