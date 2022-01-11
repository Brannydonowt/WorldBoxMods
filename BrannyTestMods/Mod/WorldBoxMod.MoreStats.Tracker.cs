//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace BrannyTestMods
{
    public partial class WorldBoxMod
    {
		public static void stats_patch(Harmony harmony) 
		{
			Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(Actor), "increaseKillCount"), AccessTools.Method(typeof(WorldBoxMod), "increaseKillCount_postfix"));
			Debug.Log("PostFix increaseKillCount DONE");
		}

		public static void increaseKillCount_postfix(Actor __instance)
		{
			var kills = Reflection.GetField(__instance.GetType(), __instance, "kills") as string;
			Debug.Log(__instance.name + " Kills:- " + kills);
			//int kills = ((ActorBase)__instance).kills;
			//Debug.Log("Added Trait: " + newTrait.id);
		}
	}
}
