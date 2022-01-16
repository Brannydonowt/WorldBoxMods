using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrannyTestMods
{
    public class TrackTarget : MonoBehaviour
    {
        public string myTargetId;
        private Actor myActor;
        private Vector3 targetPos = new Vector3();

        private bool hasTarget;

        string trackingType;

        public void trackTarget(string toTrack)
        {
            myTargetId = toTrack;

            trackingType = FindTrackingType(toTrack);

            switch (trackingType)
            {
                case "actor":
                    TrackActor(toTrack);
            break;
                case "kingdom":
                    Kingdom k = MapBox.instance.kingdoms.getKingdomByID(toTrack);
                    TrackCapital(k.capital);
                    break;
                default:
                    Debug.Log("Trying to track an unknown target type");
                    break;
            }
        }

        void TrackActor(string actorId) 
        {
            BrannyActor b = BrannyActorManager.GetRememberedActor(myTargetId);
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
            hasTarget = true;
        }

        void TrackCapital(City toTrack) 
        {
            Vector3 cityPos = new Vector3();
            cityPos = (Vector3)Helper.Reflection.GetField(cityPos.GetType(), toTrack, "cityCenter");
            targetPos = cityPos;
            hasTarget = true;
        }

        string FindTrackingType(string id) 
        {
            string result = "";

            BrannyActor b = BrannyActorManager.GetRememberedActor(myTargetId);

            if (b != null)
            {
                result = "actor";
                return result;
            }

            Kingdom k = MapBox.instance.kingdoms.getKingdomByID(id);

            if (k != null)
            {
                result = "kingdom";
                return result;
            }

            return result;
        }

        bool isActorAlive() 
        {
            BrannyActor b = BrannyActorManager.GetRememberedActor(myTargetId);
            return b.alive;
        }

        public void watchActor()
        {
            if (!hasTarget || !isActorAlive()) { return; }

            MapBox.instance.locateAndFollow(myActor, null, null);
            WorldBoxMod.instance.CloseAllUI();
        }

        public void moveToTarget() 
        {
            if (!hasTarget) { return; }

            MapBox.instance.locatePosition(targetPos);
            WorldBoxMod.instance.CloseAllUI();
        }

        public void OnInteract() 
        {
            switch (trackingType)
            {
                case "actor":
                    watchActor();
                    break;
                case "kingdom":
                    moveToTarget();
                    break;
                default:
                    break;
            }
            
        }
    }
}