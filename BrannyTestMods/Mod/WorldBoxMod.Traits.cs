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
        void initTraits() 
        {
			AssetManager.traits.add(new ActorTrait
			{
				id = "Tyrant",
				icon = "iconVeteran",
				group = TraitGroup.Personality,
				type = TraitType.Positive
			});
			var killLeaderTrait = AssetManager.traits.get("Tyrant");
			killLeaderTrait.baseStats.damage = 25;
			killLeaderTrait.baseStats.speed = 10;
			killLeaderTrait.baseStats.scale = 0.2f;
			Helper.Localization.addLocalization("trait_Tyrant", "Tyrant");


			AssetManager.traits.add(new ActorTrait
			{
				id = "Tryant Killer",
				icon = "iconVeteran",
				group = TraitGroup.Personality,
				type = TraitType.Positive
			});
			var tyrantKillerTrait = AssetManager.traits.get("Tryant Killer");
			tyrantKillerTrait.baseStats.damage = 35;
			tyrantKillerTrait.baseStats.speed = 20;
			tyrantKillerTrait.baseStats.scale = 0.3f;
			Helper.Localization.addLocalization("trait_Tyrant Killer", "Tyrant Killer");
		}
	}
}
