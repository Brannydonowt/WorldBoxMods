﻿//extern alias ncms;
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
		public void CompareToKillLeaderboard(Actor __instance) 
		{
			var data = Helper.Reflection.GetActorData(__instance);
			int kills = data.kills;

			// Try to add to the "KillsLeaderboard
			if (tryAddToLeaderboard("top_killers", data.actorID, kills))
			{
				string _id = BrannyActorManager.RememberActor(__instance);
				BrannyActorManager.AddTraitToActor("Bloodthirsty", _id);
			}

			TryAddStat("most_ruthless", data.actorID, kills);
		}
	}
}
