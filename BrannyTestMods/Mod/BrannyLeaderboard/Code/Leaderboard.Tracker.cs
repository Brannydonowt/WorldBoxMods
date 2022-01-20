//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BrannyCore;
using HarmonyLib;

namespace BrannyLeaderboard
{
	public partial class Leaderboard
	{
		// Who eats the most?
		public static void consumeCityFoodItem_postfix(Actor __instance) 
		{
			//var data = Helper.Reflection.GetActorData(__instance);
		}

		// Who kills the most?
		public static void increaseKillCount_postfix(Actor __instance)
		{
			instance.CompareToKillLeaderboard(__instance);
		}
	}
}
