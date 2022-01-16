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

    public class Utils
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

    class UnityHelpers : MonoBehaviour 
    {
        public static void LogGameObjectHierachy(Transform target, int depth = 0) 
        {
            if (target.childCount > 0)
            {
                // For every child
                foreach (Transform t in target) 
                {
                    string prepend = String.Concat(Enumerable.Repeat("--", depth));
                    Debug.Log(prepend + t.name);

                    LogGameObjectHierachy(t, depth + 1);
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

        public static Race GetActorRace(Actor actor) 
        {
            return (Race)GetField(actor.GetType(), actor, "race");
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

        public static void addSpecialLocalization(string key, string value) 
        {
            LocalizedText lText = new LocalizedText();
            lText.key = key;
            lText.specialTags = true;
            LocalizedTextManager.addTextField(lText);
            addLocalization(key, value);
            LocalizedTextManager.updateTexts();
            Debug.Log("Added: " + lText.key + "to the localization list");
        }
    }
}
