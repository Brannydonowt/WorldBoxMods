//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

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

		public static void action_santa_bomb(WorldTile pTile = null, string pDropID = null)
		{
			MapAction.damageWorld(pTile, 10, AssetManager.terraform.get("santa_bomb"));
		}
	}
}
