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
        void init_traits() 
        {
			AssetManager.traits.add(new ActorTrait
			{
				id = "bloodthirsty",
				icon = "iconVeteran",
				group = TraitGroup.Personality,
				type = TraitType.Positive
			});
			var killLeaderTrait = AssetManager.traits.get("bloodthirsty");
			killLeaderTrait.baseStats.damage = 25;
			killLeaderTrait.baseStats.speed = 10;
			killLeaderTrait.baseStats.scale = 0.2f;
			Helper.Localization.addLocalization("trait_bloodthirsty", "Bloodthirsty");

			AssetManager.traits.add(new ActorTrait
			{
				id = "tyrant",
				icon = "iconVeteran",
				group = TraitGroup.Personality,
				type = TraitType.Positive
			});
			var tyrantKillerTrait = AssetManager.traits.get("tyrant");
			tyrantKillerTrait.baseStats.damage = 45;
			tyrantKillerTrait.baseStats.speed = 20;
			tyrantKillerTrait.baseStats.scale = 0.4f;
			Helper.Localization.addLocalization("trait_tyrant", "Tyrant");
		}
	}
}
