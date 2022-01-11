extern alias ncms;
using NCMS = ncms.NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Config;
using HarmonyLib;

namespace Helper
{
    partial class GodPowerTab
    {
        public static GameObject additionalPowersTab;
        public static PowersTab powersTabComponent;

        private static int createdButtons = 0;
        private static bool lined = false;
        private static float startX = 72f;
        private static float plusX = 18f;
        private static float evenY = 18f;
        private static float oddY = -18f;
        private static float lineStep = 23.4f;

        internal static string ToInstantinatePowerButton = "";
        private static Dictionary<int, string> ButtonsToRename = new Dictionary<int, string>();

        public static void init()
        {
            if (gameLoaded)
            {
                var OtherTabButton = NCMS.Utils.GameObjects.FindEvenInactive("Button_Other");
                if (OtherTabButton != null)
                {
                    NCMS.Utils.Localization.addLocalization("Button_Additional_Powers", "PowerBox");
                    NCMS.Utils.Localization.addLocalization("tab_additional", "PowerBox");


                    var newTabButton = GameObject.Instantiate(OtherTabButton);
                    newTabButton.transform.SetParent(OtherTabButton.transform.parent);
                    var buttonComponent = newTabButton.GetComponent<Button>();


                    newTabButton.transform.localPosition = new Vector3(150f, 49.62f);
                    newTabButton.transform.localScale = new Vector3(1f, 1f);
                    newTabButton.name = "Button_Additional_Powers";

                    // Assembly sas = Assembly.GetExecutingAssembly();
                    // foreach (var item in sas.GetManifestResourceNames())
                    // {
                    //     Debug.Log(item);
                    // }

                    //var spriteForTab = Mod.EmbededResources.LoadSprite(PowerBox.WorldBoxMod.resources + ".powers.tabIcon.png");
                    //newTabButton.transform.Find("Icon").GetComponent<Image>().sprite = spriteForTab;
                    //newTabButton.transform.Find("Icon").GetComponent<Image>().sprite = Helper.Utils.LoadSprite(PowerBox.WorldBoxMod.resources + ".powers.tabIcon.png", 0, 0);

                    var OtherTab = NCMS.Utils.GameObjects.FindEvenInactive("Tab_Other");
                    foreach (Transform child in OtherTab.transform)
                    {
                        child.gameObject.SetActive(false);
                    }

                    additionalPowersTab = GameObject.Instantiate(OtherTab);

                    foreach (Transform child in additionalPowersTab.transform)
                    {
                        if (child.gameObject.name == "tabBackButton" || child.gameObject.name == "-space")
                        {
                            child.gameObject.SetActive(true);
                            continue;
                        }

                        GameObject.Destroy(child.gameObject);
                    }

                    foreach (Transform child in OtherTab.transform)
                    {
                        child.gameObject.SetActive(true);
                    }


                    additionalPowersTab.transform.SetParent(OtherTab.transform.parent);
                    powersTabComponent = additionalPowersTab.GetComponent<PowersTab>();
                    powersTabComponent.powerButton = buttonComponent;


                    additionalPowersTab.name = "Tab_Additional_Powers";
                    powersTabComponent.powerButton.onClick = new Button.ButtonClickedEvent();
                    powersTabComponent.powerButton.onClick.AddListener(Button_Additional_Powers_Click);
                    Reflection.SetField<GameObject>(powersTabComponent, "parentObj", OtherTab.transform.parent.parent.gameObject);
                    powersTabComponent.tipKey = "tab_additional";

                    additionalPowersTab.SetActive(true);
                }
            }
        }

        public static void Button_Additional_Powers_Click()
        {
            var AdditionalTab = NCMS.Utils.GameObjects.FindEvenInactive("Tab_Additional_Powers");
            var AdditionalPowersTab = AdditionalTab.GetComponent<PowersTab>();

            AdditionalPowersTab.showTab(AdditionalPowersTab.powerButton);
        }

        public static GameObject createButton(string name, Sprite sprite, Transform parent, UnityAction call, string localName, string localDescription, GameObject instantinateFrom = null)
        {
            ToInstantinatePowerButton = name;

            NCMS.Utils.Localization.addLocalization(name, localName);
            NCMS.Utils.Localization.addLocalization(name + " Description", localDescription);

            GameObject newButton;
            if (instantinateFrom != null)
            {
                instantinateFrom.SetActive(false);
                newButton = GameObject.Instantiate(instantinateFrom, parent);
                instantinateFrom.SetActive(true);
            }
            else
            {
                var tempObject = NCMS.Utils.GameObjects.FindEvenInactive("WorldLaws");
                tempObject.SetActive(false);
                newButton = GameObject.Instantiate(tempObject);
                tempObject.SetActive(true);
            }

            ButtonsToRename.Add(newButton.GetInstanceID(), name);
            newButton.SetActive(true);

            newButton.transform.SetParent(parent);

            var image = newButton.transform.Find("Icon").GetComponent<Image>();
            image.sprite = sprite;

            float x = startX + (createdButtons != 0 ? (plusX *
                    (createdButtons % 2 == 0 ? createdButtons : createdButtons - 1))
                : 0);
            float y = (createdButtons % 2 == 0 ? evenY : oddY);


            newButton.transform.localPosition = new Vector3(x, y);


            var powerButtonComponent = newButton.GetComponent<PowerButton>();
            powerButtonComponent.open_window_id = string.Empty;
            powerButtonComponent.type = PowerButtonType.Active;

            var newButtonButton = newButton.GetComponent<Button>();


            if (call != null)
            {
                //powerButtonComponent.type = PowerButtonType.Library;
                newButtonButton.onClick = new Button.ButtonClickedEvent();
                newButtonButton.onClick.AddListener(call);
            }

            if (!AssetManager.powers.dict.ContainsKey(name))
            {
                powerButtonComponent.type = PowerButtonType.Library;
            }

            powersTabComponent.CallMethod("setNewWidth");

            createdButtons++;
            lined = false;
            return newButton;
        }

        public static void patch(Harmony harmony)
        {
            Debug.Log("Harmony: Patched GodPowerTab");
        }
    }
}
