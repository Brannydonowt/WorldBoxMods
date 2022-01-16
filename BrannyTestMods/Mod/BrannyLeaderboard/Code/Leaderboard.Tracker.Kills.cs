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
		public void CompareToKillLeaderboard(Actor __instance) 
		{
			var data = Helper.Reflection.GetActorData(__instance);
			int kills = data.kills;

			// Try to add to the "KillsLeaderboard
			if (tryAddToLeaderboard("top_killers", data.actorID, kills))
			{
				string _id = BrannyActorManager.RememberActor(__instance);
				BrannyActorManager.AddTraitToActor("bloodthirsty", _id);
			}

			if (TryAddStat("most_ruthless", data.actorID, kills)) 
			{
				string _id = BrannyActorManager.RememberActor(__instance);
				BrannyActorManager.AddTraitToActor("tyrant", _id);
				BrannyActorManager.AddTraitToActor("immortal", _id);
			}

			Race race = Helper.Reflection.GetActorRace(__instance);

			if (race == null)
				return;

			Debug.Log(race.nameLocale);

			switch (race.nameLocale) 
			{
				case "Humans":
					tryAddToLeaderboard("human_killers", data.actorID, kills);
					break;
				case "Orcs":
					tryAddToLeaderboard("orc_killers", data.actorID, kills);
					break;
				case "Elves":
					tryAddToLeaderboard("elf_killers", data.actorID, kills);
					break;
				case "Dwarves":
					tryAddToLeaderboard("dwarf_killers", data.actorID, kills);
					break;
				default:
					tryAddToLeaderboard("misc_killers", data.actorID, kills);
					break;
			}
		}
	}
}
