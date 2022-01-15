using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrannyTestMods
{
    public class TrackActor : MonoBehaviour
    {
        public string myActorID;
        private Actor myActor;

        private bool hasActor;

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
            hasActor = true;
        }

        bool isActorAlive() 
        {
            BrannyActor b = BrannyActorManager.GetRememberedActor(myActorID);
            return b.alive;
        }

        public void watchActor()
        {
            if (!hasActor || !isActorAlive()) { return; }

            MapBox.instance.locateAndFollow(myActor, null, null);
            WorldBoxMod.instance.CloseAllUI();
        }

        public void OnInteract() 
        {
            watchActor();
        }
    }
}