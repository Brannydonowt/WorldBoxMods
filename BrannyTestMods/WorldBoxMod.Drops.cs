using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using HarmonyLib;

namespace BrannyTestMods
{
    public partial class WorldBoxMod
	{
		//	this.add(new DropAsset
		//{
		//	id = "water_bomb",
		//	texture = "drops/drop_waterbomb",
		//	default_scale = 0.2f,
		//	action_landed = new DropsAction(DropsLibrary.action_water_bomb)
		//});


		void initDrops()
		{
			var instantiateFrom = NCMS.Utils.GameObjects.FindEvenInactive("crab");

			var dM = AssetManager.drops;
			dM.add(new DropAsset
			{
				id = "branny_bomb",
				texture = "drops/drop_waterbomb",
				default_scale = 0.4f,
				action_landed = new DropsAction(action_santa_bomb)
			});

			Helper.GodPowerTab.createButton(
				"spawnGregCreature",
				Resources.Load<Sprite>("ui/icons/icongreg"),
				Helper.GodPowerTab.additionalPowersTab.transform,
				null,
				"Greg",
				"Greg?  Greg...",
				instantiateFrom);
		}

		public static void action_santa_bomb(WorldTile pTile = null, string pDropID = null)
		{
			MapAction.damageWorld(pTile, 10, AssetManager.terraform.get("santa_bomb"));
		}
	}
}
