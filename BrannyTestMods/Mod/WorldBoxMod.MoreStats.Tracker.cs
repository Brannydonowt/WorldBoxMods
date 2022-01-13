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
			Helper.Utils.HarmonyPatching(harmony, "postfix", AccessTools.Method(typeof(Actor), "consumeCityFoodItem"), AccessTools.Method(typeof(WorldBoxMod), "consumeCityFoodItem_postfix"));


			Debug.Log("PostFix stats_patch DONE");
		}

		public static void consumeCityFoodItem_postfix(Actor __instance) 
		{
			var data = Helper.Reflection.GetActorData(__instance);
		}

		public static void increaseKillCount_postfix(Actor __instance)
		{
			var data = Helper.Reflection.GetActorData(__instance);
			int kills = data.kills;

			if (tryAddToLeaderboard("Kills", data.actorID, kills)) 
			{
				// New entry stuff
			}

			if (killLeader != null)
			{
				// If the kill leader gets another kill
				if (data.actorID == killLeader.actorID)
				{
					logKillLeadKill(__instance);

					highestKills = kills;

					// Remember the actor (cache it)
					// Update the stat, with the BrannyID - not game ID
					string id = BrannyActorManager.RememberActor(__instance);
					UpdateMostRuthless(id, 0);
					return;
				}
			}

			// Compare this killer to our new kill leader
			if (CompareKillLeader(data, kills)) 
			{
				logNewKillLead(__instance);
				// Add the tyrant trait to the new kill leader
				__instance.addTrait("Tyrant");
				string id = BrannyActorManager.RememberActor(__instance);
				UpdateMostRuthless(id, 0);
			}
		}
	}
}
