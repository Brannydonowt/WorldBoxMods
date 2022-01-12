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

        public void Setup() 
        {
            myButton = GetComponent<Button>();
            onClickActions += Interact;
            myButton.onClick.AddListener(onClickActions);
        }

        public void trackActor(string toTrack)
        {
            myActor = MapBox.instance.getActorByID(myActorID);
        }

        public Actor GetTrackedActor()
        {
            return myActor;
        }

        public void AddCustomData(string[] data) 
        {
            foreach (string s in data) 
            {
                if (!customData.Contains(s))
                    customData.Append(s);
            }
        }

        void Interact()
        {
            trackActor(myActorID);
            MapBox.instance.locateAndFollow(myActor, null, null);
            WorldBoxMod.CloseAllUI();
        }
    }
}
