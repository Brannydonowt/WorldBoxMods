using System;
using System.Collections.Generic;

namespace tools
{
	// Token: 0x02000301 RID: 769
	public class OceanHelper
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x0009ADF4 File Offset: 0x00098FF4
		public static bool goodForNewDock(WorldTile pTile)
		{
			if (!pTile.Type.ocean)
			{
				return false;
			}
			MapRegion region = pTile.region;
			return ((region != null) ? region.island : null) != null && pTile.region.island.getTileCount() >= 2500 && !OceanHelper._ocean_pools_with_docks.Contains(pTile.region.island);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0009AE5C File Offset: 0x0009905C
		public static void saveOceanPoolsWithDocks(City pCity)
		{
			if (pCity.haveBuildingType("docks", true))
			{
				pCity.buildings_dict_type["docks"].checkAddRemove();
				List<Building> simpleList = pCity.buildings_dict_type["docks"].getSimpleList();
				for (int i = 0; i < simpleList.Count; i++)
				{
					Building building = simpleList[i];
					for (int j = 0; j < building.tiles.Count; j++)
					{
						OceanHelper.addOceanPool(building.tiles[j]);
					}
				}
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0009AEE2 File Offset: 0x000990E2
		public static void clearOceanPools()
		{
			OceanHelper._ocean_pools_with_docks.Clear();
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0009AEEE File Offset: 0x000990EE
		private static void addOceanPool(WorldTile pTile)
		{
			if (pTile.region.island == null)
			{
				return;
			}
			if (pTile.region.type != TileLayerType.Ocean)
			{
				return;
			}
			OceanHelper._ocean_pools_with_docks.Add(pTile.region.island);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0009AF24 File Offset: 0x00099124
		public static WorldTile findTileForBoat(WorldTile pTileBoat, WorldTile pTileTarget)
		{
			TileIsland island = pTileBoat.region.island;
			TileIsland island2 = pTileTarget.region.island;
			int num = 10;
			WorldTile worldTile = OceanHelper.findTileInRegion(pTileTarget.region, pTileBoat);
			if (worldTile == null)
			{
				List<MapRegion> simpleList = island2.regions.getSimpleList();
				int num2 = 0;
				while (num2 < simpleList.Count && num2 <= num)
				{
					simpleList.ShuffleOne(num2);
					worldTile = OceanHelper.findTileInRegion(simpleList[num2], pTileBoat);
					if (worldTile != null)
					{
						break;
					}
					num2++;
				}
			}
			if (worldTile == null)
			{
				return null;
			}
			for (int i = 0; i < worldTile.neighboursAll.Count; i++)
			{
				WorldTile worldTile2 = worldTile.neighboursAll[i];
				if (worldTile2.isSameIsland(pTileBoat) && worldTile2.isGoodForBoat())
				{
					worldTile = worldTile2;
					break;
				}
			}
			MapBox.instance.flashEffects.flashPixel(worldTile, -1, ColorType.White);
			return worldTile;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0009AFF8 File Offset: 0x000991F8
		private static WorldTile findTileInRegion(MapRegion pRegion, WorldTile pBoatTile)
		{
			if (pRegion.getTileCorners().Count == 0)
			{
				return null;
			}
			pRegion.getTileCorners().ShuffleOne<WorldTile>();
			List<WorldTile> tileCorners = pRegion.getTileCorners();
			for (int i = 0; i < tileCorners.Count; i++)
			{
				WorldTile worldTile = tileCorners[i];
				if (worldTile.isSameIsland(pBoatTile))
				{
					return worldTile;
				}
			}
			return null;
		}

		// Token: 0x040014C2 RID: 5314
		private static List<TileIsland> _ocean_pools_with_docks = new List<TileIsland>();
	}
}
