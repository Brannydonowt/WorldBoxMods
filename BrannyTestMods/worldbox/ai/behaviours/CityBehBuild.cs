using System;
using System.Collections.Generic;
using System.Linq;
using tools;

namespace ai.behaviours
{
	// Token: 0x0200039A RID: 922
	public class CityBehBuild : BehaviourActionCity
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x000A6E28 File Offset: 0x000A5028
		public override BehResult execute(City pCity)
		{
			if (!DebugConfig.isOn(DebugOption.SystemBuildTick))
			{
				return BehResult.Continue;
			}
			if (pCity.isGettingCaptured())
			{
				return BehResult.Continue;
			}
			CityBehBuild.buildTick(pCity);
			return BehResult.Continue;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000A6E48 File Offset: 0x000A5048
		public static void buildTick(City pCity)
		{
			Culture culture = pCity.getCulture();
			if (pCity.buildings.Count > 2 && culture != null && culture.haveTech("building_roads"))
			{
				Building random = pCity.buildings.GetRandom();
				if (random != null)
				{
					CityBehBuild.makeRoadsBuildings(pCity, random);
				}
			}
			if (pCity.tasks.fire > 0)
			{
				return;
			}
			if (pCity.underConstructionBuilding == null)
			{
				foreach (Building building in pCity.buildings)
				{
					if (building.data.underConstruction)
					{
						pCity.underConstructionBuilding = building;
						break;
					}
				}
			}
			if (pCity.underConstructionBuilding != null)
			{
				return;
			}
			pCity._debug_nextPlannedBuilding = null;
			string text = null;
			int num = 0;
			foreach (CityBuildOrderElement cityBuildOrderElement in CityBuildOrder.list)
			{
				string text2 = cityBuildOrderElement.buildingID;
				if (cityBuildOrderElement.addRace)
				{
					text2 = text2 + "_" + pCity.race.id;
				}
				if (!cityBuildOrderElement.usedByRacesCheck || cityBuildOrderElement.usedByRaces.Contains(pCity.race.id))
				{
					BuildingAsset buildingAsset = AssetManager.buildings.get(text2);
					num++;
					pCity._debug_nextPlannedBuilding = buildingAsset.type;
					if (string.IsNullOrEmpty(buildingAsset.tech) || (culture != null && culture.haveTech(buildingAsset.tech)))
					{
						int limitOfBuildings = pCity.getLimitOfBuildings(cityBuildOrderElement);
						if ((!cityBuildOrderElement.checkFullVillage || pCity.status.homesFree == 0) && (cityBuildOrderElement.limitID == 0 || pCity.countBuildingsID(text2) < cityBuildOrderElement.limitID) && (limitOfBuildings == 0 || pCity.countBuildingsType(buildingAsset.type) < limitOfBuildings) && pCity.status.population >= cityBuildOrderElement.requiredPop && pCity.buildings.Count >= cityBuildOrderElement.requiredBuildings)
						{
							if (!pCity.haveEnoughResourcesFor(buildingAsset.cost))
							{
								if (cityBuildOrderElement.waitForResources)
								{
									return;
								}
							}
							else if (!buildingAsset.docks || CityBehBuild.getDockTile(pCity) != null)
							{
								text = text2;
								break;
							}
						}
					}
				}
			}
			if (text == null)
			{
				return;
			}
			Building building2 = CityBehBuild.tryToBuild(pCity, text);
			if (building2 == null)
			{
				return;
			}
			if (DebugConfig.isOn(DebugOption.CityFastConstruction))
			{
				if (building2 != null)
				{
					building2.updateBuild(1000);
				}
				pCity.underConstructionBuilding = null;
			}
			if (culture != null && culture.haveTech("building_roads") && building2 != null)
			{
				CityBehBuild.makeRoadsBuildings(pCity, building2);
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x000A7120 File Offset: 0x000A5320
		public static Building tryToBuild(City pCity, string pBuilding)
		{
			BuildingAsset buildingAsset = AssetManager.buildings.get(pBuilding);
			if (!pCity.haveEnoughResourcesFor(buildingAsset.cost))
			{
				return null;
			}
			CityBehBuild._zones_with_buildings.Clear();
			CityBehBuild._zones_no_buildings.Clear();
			WorldTile worldTile;
			if (buildingAsset.buildingPlacement == CityBuildingPlacement.Borders)
			{
				worldTile = CityBehBuild.tryToBuildInZones(CityBehBuild.getZonesForTower(pCity), buildingAsset, pCity);
			}
			else if (buildingAsset.docks)
			{
				worldTile = CityBehBuild.getDockTile(pCity);
			}
			else
			{
				foreach (TileZone tileZone in pCity.zones)
				{
					if (tileZone.buildings.Count == 0)
					{
						CityBehBuild._zones_no_buildings.Add(tileZone);
					}
					else
					{
						foreach (TileZone tileZone2 in tileZone.neighboursAll)
						{
							if (!CityBehBuild._zones_with_buildings.Contains(tileZone2))
							{
								CityBehBuild._zones_with_buildings.Add(tileZone2);
							}
						}
						CityBehBuild._zones_with_buildings.Add(tileZone);
					}
				}
				CityBehBuild._zones_with_buildings.Shuffle<TileZone>();
				worldTile = CityBehBuild.tryToBuildInZones(CityBehBuild._zones_with_buildings, buildingAsset, pCity);
				if (worldTile == null)
				{
					CityBehBuild._zones_no_buildings.Shuffle<TileZone>();
					worldTile = CityBehBuild.tryToBuildInZones(CityBehBuild._zones_no_buildings, buildingAsset, pCity);
				}
			}
			if (worldTile == null)
			{
				return null;
			}
			Building building = BehaviourActionBase<City>.world.addBuilding(buildingAsset.id, worldTile, null, false, false, BuildPlacingType.New);
			pCity.addBuilding(building);
			pCity.underConstructionBuilding = building;
			building.data.underConstruction = true;
			building.setSpriteUnderConstruction();
			pCity.spendResourcesFor(buildingAsset.cost);
			Sfx.play("constructing", true, building.transform.localPosition.x, building.transform.localPosition.y);
			return building;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x000A72F8 File Offset: 0x000A54F8
		public static WorldTile tryToBuildInZones(List<TileZone> pList, BuildingAsset tTemp, City pCity)
		{
			WorldTile worldTile = null;
			foreach (TileZone tileZone in pList)
			{
				tileZone.tiles.ShuffleOne<WorldTile>();
				if (pCity.race.buildingPlacements == BuildingPlacements.Random || tTemp.docks)
				{
					using (List<WorldTile>.Enumerator enumerator2 = tileZone.tiles.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							WorldTile worldTile2 = enumerator2.Current;
							if (CityBehBuild.isGoodTileForBuilding(worldTile2, tTemp, pCity))
							{
								worldTile = worldTile2;
								break;
							}
						}
						continue;
					}
				}
				if (pCity.race.buildingPlacements == BuildingPlacements.Center && CityBehBuild.isGoodTileForBuilding(tileZone.centerTile, tTemp, pCity))
				{
					worldTile = tileZone.centerTile;
					if (Toolbox.randomChance(0.8f))
					{
						worldTile = worldTile.neighboursAll.GetRandom<WorldTile>();
						break;
					}
					break;
				}
			}
			return worldTile;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000A73F4 File Offset: 0x000A55F4
		public static bool isGoodTileForBuilding(WorldTile pTile, BuildingAsset pAsset, City pCity)
		{
			return pTile.canBuildOn(pAsset) && BehaviourActionBase<City>.world.canBuildFrom(pTile, pAsset, pCity, BuildPlacingType.New);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x000A7414 File Offset: 0x000A5614
		public static void debugRoards(City pCity, Building pBuilding)
		{
			CityBehBuild.makeRoadsBuildings(pCity, pBuilding);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x000A7420 File Offset: 0x000A5620
		public static void makeRoadsBuildings(City pCity, Building pBuilding)
		{
			if (pCity.roadTilesToBuild.Count > 0)
			{
				return;
			}
			if (!pBuilding.stats.buildRoadTo)
			{
				return;
			}
			WorldTile currentTile = pBuilding.currentTile;
			if (currentTile.Type.liquid)
			{
				return;
			}
			List<WorldTile> list = new List<WorldTile>();
			foreach (Building building in pCity.buildings)
			{
				if (!(building == pBuilding) && building.stats.buildRoadTo && !building.currentTile.Type.liquid && building.currentTile.isSameIsland(pBuilding.currentTile))
				{
					list.Add(building.currentTile);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			bool pFinished = false;
			if (DebugConfig.isOn(DebugOption.CityFastConstruction))
			{
				pFinished = true;
			}
			WorldTile closestTile = Toolbox.getClosestTile(list, currentTile);
			if (closestTile != null)
			{
				list.Remove(closestTile);
				MapAction.makeRoadBetween(closestTile, currentTile, pCity, pFinished);
			}
			closestTile = Toolbox.getClosestTile(list, currentTile);
			if (closestTile != null)
			{
				list.Remove(closestTile);
				MapAction.makeRoadBetween(closestTile, currentTile, pCity, pFinished);
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x000A7540 File Offset: 0x000A5740
		public static List<TileZone> getZonesForTower(City pCity)
		{
			List<TileZone> list = new List<TileZone>();
			foreach (TileZone tileZone in pCity.zones)
			{
				using (List<TileZone>.Enumerator enumerator2 = tileZone.neighboursAll.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.city != pCity && !list.Contains(tileZone))
						{
							list.Add(tileZone);
						}
					}
				}
			}
			if (!list.Any<TileZone>())
			{
				return null;
			}
			return list;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x000A75F4 File Offset: 0x000A57F4
		public static WorldTile getDockTile(City pCity)
		{
			BuildingAsset pTemplate = AssetManager.buildings.get("docks_" + pCity.race.id);
			WorldTile worldTile = null;
			OceanHelper.clearOceanPools();
			OceanHelper.saveOceanPoolsWithDocks(pCity);
			WorldTile tile = pCity.getTile();
			if (tile == null)
			{
				return null;
			}
			foreach (TileZone tileZone in pCity.zones)
			{
				MapChunk chunk = tileZone.centerTile.chunk;
				if (tileZone.tilesWithLiquid != 0 && chunk.regions.Count > 1)
				{
					bool flag = false;
					foreach (MapRegion mapRegion in chunk.regions)
					{
						if (mapRegion.type == TileLayerType.Ocean)
						{
							if (!OceanHelper.goodForNewDock(mapRegion.tiles[0]))
							{
								continue;
							}
							flag = true;
						}
						if (mapRegion.type == TileLayerType.Ground)
						{
							TileIsland island = mapRegion.island;
							TileIsland island2 = tile.region.island;
						}
					}
					if (flag && flag)
					{
						tileZone.tiles.ShuffleOne<WorldTile>();
						foreach (WorldTile worldTile2 in tileZone.tiles)
						{
							if (worldTile2.Type.ocean && BehaviourActionBase<City>.world.canBuildFrom(worldTile2, pTemplate, pCity, BuildPlacingType.New))
							{
								worldTile = worldTile2;
								break;
							}
						}
						if (worldTile != null)
						{
							break;
						}
					}
				}
			}
			return worldTile;
		}

		// Token: 0x04001563 RID: 5475
		private static List<TileZone> _zones_with_buildings = new List<TileZone>();

		// Token: 0x04001564 RID: 5476
		private static List<TileZone> _zones_no_buildings = new List<TileZone>();
	}
}
