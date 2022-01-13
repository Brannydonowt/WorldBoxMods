//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using HarmonyLib;

namespace BrannyTestMods
{
    public class ButtonInteraction : MonoBehaviour
    {
        public string myActorID;
        Button myButton;
        private Actor myActor;

        public UnityAction onClickActions;

        public string[] customData;

        public List<GameObject> listeners = new List<GameObject>();

        public void Setup() 
        {
            customData = new string[0];
            myButton = GetComponent<Button>();
            onClickActions += Interact;
            myButton.onClick.AddListener(onClickActions);
            myButton.interactable = true;
        }

        public void trackActor(string toTrack)
        {
            myActorID = toTrack;
            string actorId = "";

            BrannyActor b = BrannyActorManager.GetRememberedActor(myActorID);

            if (b.alive)
            {
                actorId = b.actorID;
                GetComponent<Button>().interactable = true;
            }
            else 
            {
                GetComponent<Button>().interactable = false;
            }
            
            myActor = MapBox.instance.getActorByID(actorId);
            onClickActions += Interact;
        }

        public Actor GetTrackedActor()
        {
            return myActor;
        }

        public void AddCustomData(string[] data) 
        {
            customData = data;
        }

        public void AddListener(GameObject g) 
        {
            Debug.Log("Adding listener");
            listeners.Add(g);
        }

        void Interact()
        { 
            foreach (GameObject g in listeners) 
            {
                g.SendMessage("OnInteract");
            }

            // TODO Move this into it's own seperate component
            //trackActor(myActorID);
            //MapBox.instance.locateAndFollow(myActor, null, null);
            //WorldBoxMod.CloseAllUI();
        }
    }
}
