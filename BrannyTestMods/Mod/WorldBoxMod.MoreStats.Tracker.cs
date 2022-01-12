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
			var data = Helper.Reflection.GetActorData(__instance);
			int kills = data.kills;

			if (killLeader != null)
			{
				// If the kill leader gets another kill
				if (data.actorID == killLeader.actorID)
				{
					logKillLeadKill(__instance);

					highestKills = kills;

					UpdateMostRuthless(__instance);

					return;
				}
			}

			// Compare this killer to our new kill leader
			if (CompareKillLeader(data, kills)) 
			{
				logNewKillLead(__instance);
				// Add the tyrant trait to the new kill leader
				__instance.addTrait("Tyrant");
				UpdateMostRuthless(__instance);
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
				Debug.Log(killer.firstName + " has reached " + highestKills + " total kills!");
			}

			return false;
		}
	}
}
