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
using System.Reflection;
using static Config;

namespace Helper
{
    class DebugMenu
    {
        public static void toggleDebugButton(bool value)
        {
            if (gameLoaded)
            {
                //PowerButton.get("DebugButton").gameObject.SetActive(value);

                var Buttons = Resources.FindObjectsOfTypeAll<PowerButton>();
                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (Buttons[i].gameObject.transform.name == "DebugButton")
                    {
                        Buttons[i].gameObject.SetActive(value);
                    }
                }
            }
        }
    }

    class HisHud
    {
        public static void newText(string message, Color color, Sprite icon = null)
        {
            GameObject gameObject = HistoryHud.instance.GetObject();
            gameObject.name = "HistoryItem " + (object)(HistoryHud.historyItems.Count + 1);
            gameObject.SetActive(true);

            gameObject.transform.Find("CText").GetComponent<Text>();
            gameObject.transform.SetParent(HistoryHud.contentGroup);
            RectTransform component = gameObject.GetComponent<RectTransform>();
            component.localScale = Vector3.one;
            component.localPosition = Vector3.zero;
            component.SetLeft(0.0f);

            float top = (float)HistoryHud.instance.CallMethod("recalcPositions");

            component.SetTop(top);
            component.sizeDelta = new Vector2(component.sizeDelta.x, 15f);
            gameObject.GetComponent<HistoryHudItem>().targetBottom = top;

            gameObject.GetComponent<HistoryHudItem>().textField.color = color;
            gameObject.GetComponent<HistoryHudItem>().textField.text = message;
            HistoryHud.historyItems.Add(gameObject.GetComponent<HistoryHudItem>());
            Reflection.SetField<bool>(HistoryHud.instance, "recalc", true);

            if (icon != null)
            {
                gameObject.transform.Find("Icon").GetComponent<Image>().sprite = icon;
            }

            gameObject.SetActive(true);
        }
    }

    class KingdomThings
    {
        public static void newText(string message, Color color)
        {
            HisHud.newText(message, color);
        }

        public static Color GetKingdomColor(Kingdom kingdom)
        {
            var kingdomColor = (KingdomColor)Reflection.GetField(kingdom.GetType(), kingdom, "kingdomColor");
            return kingdomColor.colorBorderOut;
        }
    }

    class Utils
    {
        public static void HarmonyPatching(Harmony harmony, string type, MethodInfo original, MethodInfo patch)
        {
            switch (type)
            {
                case "prefix":
                    harmony.Patch(original, prefix: new HarmonyMethod(patch));
                    break;
                case "postfix":
                    harmony.Patch(original, postfix: new HarmonyMethod(patch));
                    break;
            }
        }

        public static void CopyClass<T>(T copyFrom, T copyTo, bool copyChildren)
        {
            if (copyFrom == null || copyTo == null)
                throw new Exception("Must not specify null parameters");

            var properties = copyFrom.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                {
                    if (!copyChildren) continue;

                    var destinationClass = Activator.CreateInstance(p.PropertyType);
                    object copyValue = p.GetValue(copyFrom);

                    CopyClass(copyValue, destinationClass, copyChildren);

                    p.SetValue(copyTo, destinationClass);
                }
                else
                {
                    object copyValue = p.GetValue(copyFrom);
                    p.SetValue(copyTo, copyValue);
                }
            }
        }
    }

    public static partial class Reflection 
    {
        public static ActorStatus GetActorData(Actor actor) 
        {
            return (ActorStatus)GetField(actor.GetType(), actor, "data");
        }

        public static ActorStats GetActorStats(Actor actor) 
        {
            return (ActorStats)GetField(actor.GetType(), actor, "stats");
        }

        public static List<WorldTile> GetActorPath(Actor actor) 
        {
            return GetField(actor.GetType(), actor, "current_path") as List<WorldTile>;
        }

        public static List<WorldLogMessage> GetWorldLogMessages(WorldLog instance) 
        {
            return GetField(instance.GetType(), instance, "list") as List<WorldLogMessage>;
        }
    }

    // Thanks to CodyP https://github.com/itsmecodyp/WorldBox/blob/main/MapSizes/Reflection.cs
    public static partial class Reflection
    {
        // found on https://stackoverflow.com/questions/135443/how-do-i-use-reflection-to-invoke-a-private-method
        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }
        // found on: https://stackoverflow.com/questions/3303126/how-to-get-the-value-of-private-field-in-c/3303182
        public static object GetField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
        public static void SetField<T>(object originalObject, string fieldName, T newValue)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = originalObject.GetType().GetField(fieldName, bindFlags);
            field.SetValue(originalObject, newValue);
        }
    }

    public static partial class Localization 
    {
        public static void addLocalization(string key, string value)
        {
            Dictionary<string, string> dictionary = (Dictionary<string, string>)Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText");
            dictionary.Add(key, value);
        }

        public static string getLocalization(string key)
        {
            Dictionary<string, string> dictionary = (Dictionary<string, string>)Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText");
            return dictionary[key];
        }
    }
}
