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
        Button myButton;

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

        public void AddCustomData(string[] data) 
        {
            customData = data;
        }

        public void AddListener(GameObject g) 
        {
            listeners.Add(g);
        }

        void Interact()
        {
            Debug.Log(gameObject.name + " button clicked!");

            foreach (GameObject g in listeners) 
            {
                Debug.Log("Sending message to: " + g.name);
                g.SendMessage("OnInteract");
            }
        }
    }
}
