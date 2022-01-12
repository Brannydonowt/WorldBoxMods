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
    public class StatInteraction : MonoBehaviour
    {
        public Actor myActor;
        Button myButton;

        private UnityAction onClickActions;

        void Start() 
        {
            myButton = GetComponent<Button>();
            onClickActions += Interact;
            myButton.onClick.AddListener(onClickActions);
        }

        public void trackActor(Actor toTrack)
        {
            myActor = toTrack;
        }

        public Actor GetTrackedActor()
        {
            return myActor;
        }

        void Interact()
        {
            Action showMethod = delegate () { TestAction(); };
            MapBox.instance.locateAndFollow(myActor, null, null);
        }
        void TestAction() { }
    }
}
