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
		public static ActorStatus killLeader;
		public static int highestKills;

		public static void stats_patch(Harmony harmony) 
		{
			Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(Actor), "increaseKillCount"), AccessTools.Method(typeof(WorldBoxMod), "increaseKillCount_postfix"));
			Debug.Log("PostFix increaseKillCount DONE");
		}

		public static void increaseKillCount_postfix(Actor __instance)
		{
			var data = (ActorStatus)Reflection.GetField(__instance.GetType(), __instance, "data");
			int kills = data.kills;

			if (CompareKillLeader(data, kills)) 
			{
				Debug.Log("We have a new kill leader!");
				Debug.Log(data.firstName + " : " + kills + " kills");
			}
		}

		public static bool CompareKillLeader(ActorStatus killer, int numKills) 
		{
			if (numKills > highestKills)
			{
				killLeader = killer;
				highestKills = numKills;
				return true;
			}
			else
			if (numKills == highestKills) 
			{
				Debug.Log("We have a contender for the most ruthless!");
				Debug.Log(killer.firstName + " has reached " + highestKills + " total kills!");
			}

			return false;
		}
	}
}
