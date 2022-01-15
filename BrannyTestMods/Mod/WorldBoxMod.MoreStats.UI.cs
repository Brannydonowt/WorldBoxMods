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
        //public GameObject statParent;
        public GameObject statEntry;
        public GameObject statList;
        public GameObject statListEntry;

        public Transform statParent;

        bool ui_initialized;

        List<GameObject> createdElements = new List<GameObject>();

        void init_ui()
        {
            if (!assets_initialised)
                return;

            Debug.Log("Initialising UI");

            brannyCanvas = GetGameObjectFromAssetBundle("BrannyCanvas");
            statParent = brannyCanvas.transform.GetChild(0).GetChild(0).GetChild(0);
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
                statParent.gameObject.SetActive(brannyCanvas.activeSelf);
            }
        }

        void UpdateStatLeaderboard(string type, List<LeaderboardEntry> leaderboard) 
        {
            if (statParent.transform.Find(type))
            {
                GameObject list = statParent.transform.Find(type).gameObject;
                Helper.UnityHelpers.LogGameObjectHierachy(list.transform);
                UnfoldList myList = list.transform.GetChild(0).gameObject.GetComponent<UnfoldList>();

                // Get all children and update them to the values of the new leaderboard
                foreach (Transform child in list.transform.GetChild(1)) 
                {
                    if (child.name != "StatListEntry")
                    {
                        int pos = int.Parse(child.name);
                        UpdateEntry(child, type, leaderboard[pos]);
                        child.GetComponent<TrackActor>().trackActor(leaderboard[pos].actorId);
                    }
                }

                if (myList.open)
                {
                    myList.UnfoldPanel();
                }
            }
            else 
            {
                CreateNewStatLeaderboard(type, leaderboard);
            }
        }

        GameObject CreateNewStatLeaderboard(string type, List<LeaderboardEntry> leaderboard)
        {
            GameObject list = Instantiate(statList, statParent.transform);
            ButtonInteraction button = list.transform.GetChild(0).gameObject.AddComponent<ButtonInteraction>();
            button.Setup();
            UnfoldList myList = list.transform.GetChild(0).gameObject.AddComponent<UnfoldList>();
            myList.Setup();
            button.AddListener(myList.gameObject);
            list.SetActive(true);
            CustomiseStatList(list.transform, type);
            
            createdElements.Add(list);
            
            foreach (LeaderboardEntry l in leaderboard) 
            {
                l.UpdatePosition(leaderboard.IndexOf(l));
                GameObject entry = CreateNewStatLeaderboardEntry(l, type);
                entry.transform.SetParent(list.transform.GetChild(1));
                entry.transform.SetSiblingIndex(l.position);
                entry.SetActive(true);
                Debug.Log("ENTRY NAME: " + entry.gameObject.name);
            }

            Helper.UnityHelpers.LogGameObjectHierachy(list.transform);

            return list;
        }

        // Creates a new entry for a stat leaderboard at given position
        GameObject CreateNewStatLeaderboardEntry(LeaderboardEntry l, string type) 
        {
            int statName = l.statValue;
;
            BrannyActor bActor = BrannyActorManager.GetRememberedActor(l.actorId);

            GameObject entry = Instantiate(statListEntry);
            TrackActor track = entry.AddComponent<TrackActor>();
            track.trackActor(l.actorId);
            ButtonInteraction button = entry.AddComponent<ButtonInteraction>();
            button.Setup();
            button.AddListener(entry);
            entry.SetActive(true);

            CustomiseStatListEntry(entry.transform, type, l);

            string[] cData = new string[1];
            cData[0] = type;

            button.AddCustomData(cData);

            return entry;
        }

        void UpdateEntry(Transform listEntry, string type, LeaderboardEntry l) 
        {
            CustomiseStatListEntry(listEntry, type, l);
        }

        // This needs to work somehow with the modular approach
        // For now, hardcoded for each type of leaderboard
        void CustomiseStatList(Transform listEntry, string type) 
        {
            listEntry.gameObject.name = type;

            Transform root = listEntry.GetChild(0);
            Image icon = root.GetChild(0).gameObject.GetComponent<Image>();
            Text title = root.GetChild(1).gameObject.GetComponent<Text>();
            Text details = root.GetChild(2).gameObject.GetComponent<Text>();
            Text alive = root.GetChild(3).gameObject.GetComponent<Text>();

            switch (type) 
            {
                case "top_killers":
                    title.text = "Top Killers";
                    details.text = "Grants the \"BloodThirsty\" Trait";
                    alive.text = "";
                    break;
                case "most_ruthless":
                    title.text = "Most Ruthless";
                    details.text = "Grants the \"Tyrant\" Trait";
                    alive.text = "";
                    break;
                default:
                    title.text = "broken";
                    details.text = "This is broken, please let the dev know what you expect it to be.";
                    alive.text = "";     
                    break;
            }
        }

        void CustomiseStatListEntry(Transform listEntry, string type, LeaderboardEntry l)
        {
            string statDisplayName = "";

            listEntry.gameObject.name = l.position.ToString();

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
            {
                status.text = "Alive";
                status.color = new Color(66, 238, 73);
            }
            else
            {
                status.text = "Dead";
                status.color = new Color(238, 77, 67);
            }
        }

        string format_details_string(string[] details) 
        {
            return details[0] + " - " + details[1] + " - Kills: " + details[2];
        }


        // TODO - Add YearBorn-YearDeath
        string format_status_string(BrannyActor actor) 
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
