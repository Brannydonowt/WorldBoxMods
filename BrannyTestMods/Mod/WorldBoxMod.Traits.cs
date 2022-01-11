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
				id = "Hedgehog",
				icon = "iconVeteran",
				group = TraitGroup.Genetic,
				type = TraitType.Positive
			});
			var hedgehogTrait = AssetManager.traits.get("Hedgehog");
			hedgehogTrait.baseStats.damage = 10;
			hedgehogTrait.baseStats.speed = 250;
		}
	}
}
