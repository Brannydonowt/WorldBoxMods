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
using BrannyCore;
using HarmonyLib;

namespace BrannyLeaderboard
{
    public partial class Leaderboard
    {
        public GameObject brannyCanvas;
        public GameObject statEntry;
        public GameObject statList;
        public GameObject statListEntry;

        public Transform statParent;

        bool ui_initialized;

        public void init_ui()
        {
            if (!foundation.initialized) { Debug.Log("BrannyFoundation not initialized."); return; }

            if (!foundation.assets_initialised) { Debug.Log("Assets not initialized."); return; }

            Debug.Log("Initialising UI");

            brannyCanvas = foundation.GetGameObjectFromAssetBundle("BrannyCanvas");
            Debug.Log("Branny Canvas found: " + brannyCanvas.name);
            statParent = brannyCanvas.transform.GetChild(0).GetChild(0).GetChild(0);
            statEntry = foundation.GetGameObjectFromAssetBundle("StatEntry");
            statList = foundation.GetGameObjectFromAssetBundle("StatList");
            statListEntry = foundation.GetGameObjectFromAssetBundle("StatListEntry");

            brannyCanvas.SetActive(false);

            ui_initialized = true;
        }

        void UpdateStatLeaderboard(string type, List<LeaderboardEntry> leaderboard) 
        {
            if (statParent.transform.Find(type))
            {
                GameObject list = statParent.transform.Find(type).gameObject;
                Debug.Log("Acquired List: " + list.name);
                UnfoldList myList = list.transform.GetChild(0).gameObject.GetComponent<UnfoldList>();

                int childCount = list.transform.GetChild(1).childCount;
                
                // Does the leaderboard have more entries than we have UI entries?
                if (childCount < leaderboard.Count && childCount < 10) 
                {
                    for (int i = childCount; i < leaderboard.Count; i++) 
                    {
                        GameObject entry = CreateNewStatLeaderboardEntry(leaderboard[i], type);
                        entry.transform.SetParent(list.transform.GetChild(1));
                        entry.name = entry.transform.GetSiblingIndex().ToString();
                        entry.SetActive(true);
                    }
                }

                // Get all children and update them to the values of the new leaderboard
                foreach (Transform child in list.transform.GetChild(1))
                {
                    int pos = child.GetSiblingIndex();
                    UpdateEntry(child, type, leaderboard[pos]);
                    child.GetComponent<TrackTarget>().trackTarget(leaderboard[pos].actorId);
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
            GameObject list = Instantiate(statList.gameObject, statParent.transform);
            list.name = "LIST - " + type;
            UnfoldList myList = list.transform.GetChild(0).gameObject.AddComponent<UnfoldList>();
            list.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(myList.TogglePanel); // += myList.TogglePanel;
            myList.Setup();
            list.SetActive(true);
            CustomiseStatList(list.transform, type);
                        
            foreach (LeaderboardEntry l in leaderboard) 
            {
                l.UpdatePosition(leaderboard.IndexOf(l));
                GameObject entry = CreateNewStatLeaderboardEntry(l, type);
                entry.transform.SetParent(list.transform.GetChild(1));
                entry.name = entry.transform.GetSiblingIndex().ToString();
                CustomiseStatListEntry(entry.transform, type, l);
                entry.SetActive(true);
            }

            return list;
        }

        // Creates a new entry for a stat leaderboard at given position
        GameObject CreateNewStatLeaderboardEntry(LeaderboardEntry l, string type) 
        {
            int statName = l.statValue;
;
            BrannyActor bActor = BrannyActorManager.GetRememberedActor(l.actorId);

            GameObject entry = Instantiate(statListEntry);

            TrackTarget track = entry.AddComponent<TrackTarget>();
            track.trackTarget(l.actorId);
            GetComponent<Button>().onClick.AddListener(track.OnInteract);

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
                case "human_killers":
                    title.text = "Human Most Kills";
                    details.text = "The most ruthless Humans in history";
                    alive.text = "";
                    break;
                case "elf_killers":
                    title.text = "Elves Most Kills";
                    details.text = "The most ruthless Elves in history";
                    alive.text = "";
                    break;
                case "dwarf_killers":
                    title.text = "Dwarves Most Kills";
                    details.text = "The most ruthless Dwarves in history";
                    alive.text = "";
                    break;
                case "orc_killers":
                    title.text = "Orcs Most Kills";
                    details.text = "The most ruthless Orcs in history";
                    alive.text = "";
                    break;
                case "misc_killers":
                    title.text = "Misc Most Kills";
                    details.text = "The most vicious creatures in history";
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
            BrannyActor bActor = BrannyActorManager.GetRememberedActor(l.actorId);
            if (bActor == null)
                return;

            ActorStatus data = bActor.getActorStatus();

            Text rank = listEntry.GetChild(0).gameObject.GetComponent<Text>();
            Text name = listEntry.GetChild(1).gameObject.GetComponent<Text>();
            Text statAmount = listEntry.GetChild(2).gameObject.GetComponent<Text>();
            Text status = listEntry.GetChild(3).gameObject.GetComponent<Text>();

            int rankValue = int.Parse(listEntry.name) + 1;
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
