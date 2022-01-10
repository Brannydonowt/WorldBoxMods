using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace tools.debug
{
	// Token: 0x02000302 RID: 770
	public class DebugMap
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x0009B060 File Offset: 0x00099260
		public static void makeDebugMap(MapBox pWorld)
		{
			DebugMap.world = pWorld;
			DebugMap.createDebugButtons();
			foreach (WorldTile pTile in DebugMap.world.tilesList)
			{
				MapAction.terraformTile(pTile, TileLibrary.soil_low, TopTileLibrary.grass_low, TerraformLibrary.destroy);
			}
			int num = 10;
			int num2 = 10;
			int i = 0;
			int count = AssetManager.buildings.list.Count;
			while (i < count)
			{
				BuildingAsset buildingAsset = AssetManager.buildings.list[i];
				if (buildingAsset.id.Contains("!"))
				{
					i++;
				}
				else
				{
					i++;
					num += 20;
					if (num > 200)
					{
						num = 10;
						num2 += 10;
					}
					Building building = DebugMap.world.addBuilding(buildingAsset.id, DebugMap.world.GetTile(num, num2), null, false, false, BuildPlacingType.New);
					building.kingdom = DebugMap.world.kingdoms.dict_hidden["nature"];
					building.updateBuild(10000);
					if (building.stats.docks)
					{
						foreach (WorldTile pTile2 in building.tiles)
						{
							MapAction.terraformMain(pTile2, TileLibrary.shallow_waters, TerraformLibrary.flash);
						}
					}
				}
			}
			Config.paused = true;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0009B1E8 File Offset: 0x000993E8
		private static void debugConstructionZone()
		{
			foreach (Building building in DebugMap.world.buildings)
			{
				building.debugConstructions();
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0009B238 File Offset: 0x00099438
		private static void debugNextFrame()
		{
			foreach (Building building in DebugMap.world.buildings)
			{
				building.debugNextFrame();
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0009B288 File Offset: 0x00099488
		private static void debugRuins()
		{
			foreach (Building building in DebugMap.world.buildings)
			{
				building.debugRuins();
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0009B2D8 File Offset: 0x000994D8
		private static void createDebugButtons()
		{
			Button button = DebugMap.makeNewButton("debug_next_frame", "iconBuildings");
			button.onClick.AddListener(new UnityAction(DebugMap.debugNextFrame));
			button.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, -20f);
			Button button2 = DebugMap.makeNewButton("debug_ruins", "iconDemolish");
			button2.onClick.AddListener(new UnityAction(DebugMap.debugRuins));
			button2.GetComponent<RectTransform>().anchoredPosition = new Vector2(100f, -20f);
			Button button3 = DebugMap.makeNewButton("debug_construction", "iconBucket");
			button3.onClick.AddListener(new UnityAction(DebugMap.debugConstructionZone));
			button3.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, -20f);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0009B3A4 File Offset: 0x000995A4
		private static Button makeNewButton(string pName, string pIcon)
		{
			Button button = Object.Instantiate<Button>((Button)Resources.Load("ui/PrefabWorldBoxButton", typeof(Button)), DebugMap.world.canvas.transform);
			button.transform.name = pName;
			button.transform.parent = DebugMap.world.canvas.transform;
			Sprite sprite = (Sprite)Resources.Load("ui/Icons/" + pIcon, typeof(Sprite));
			button.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
			return button;
		}

		// Token: 0x040014C3 RID: 5315
		private static MapBox world;
	}
}
