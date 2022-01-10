using System;
using System.Collections.Generic;

namespace ai
{
	// Token: 0x0200031B RID: 795
	public static class ActorTool
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x0009E68C File Offset: 0x0009C88C
		public static int countRatsNearby(Actor pActor)
		{
			MapBox.instance.getObjectsInChunks(pActor.currentTile, 10, MapObjectType.Actor);
			int num = 0;
			for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
			{
				if (((Actor)MapBox.instance.temp_map_objects[i]).haveTrait("rat"))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0009E6F0 File Offset: 0x0009C8F0
		public static Building findNewBuildingTarget(Actor pActor, string pType)
		{
			ActorTool.possible_buildings.Clear();
			pActor.city.zones.ShuffleOne<TileZone>();
			if (pType == "new_building")
			{
				Building buildingToBuild = pActor.city.getBuildingToBuild();
				if (buildingToBuild != null && buildingToBuild.tiles.Count > 0)
				{
					WorldTile constructionTile = buildingToBuild.getConstructionTile();
					if (constructionTile != null && constructionTile.isSameIsland(pActor.currentTile))
					{
						ActorTool.possible_buildings.Add(buildingToBuild);
					}
				}
			}
			else if (pType == "random_house_building")
			{
				List<Building> simpleList = pActor.city.buildings.getSimpleList();
				for (int i = 0; i < simpleList.Count; i++)
				{
					simpleList.ShuffleOne(i);
					Building building = simpleList[i];
					if (building.currentTile.isSameIsland(pActor.currentTile) && !building.data.underConstruction && building.data.alive && !building.stats.isRuin && building.stats.housing != 0)
					{
						ActorTool.possible_buildings.Add(building);
						break;
					}
				}
			}
			else if (pType == "ruins")
			{
				for (int j = 0; j < pActor.city.zones.Count; j++)
				{
					TileZone tileZone = pActor.city.zones[j];
					foreach (Building building2 in tileZone.ruins)
					{
						ActorTool.possible_buildings.Add(building2);
					}
					foreach (Building building3 in tileZone.abandoned)
					{
						ActorTool.possible_buildings.Add(building3);
					}
				}
			}
			else
			{
				if (!(pType == "mine"))
				{
					return ActorTool.findNewTargetInZones(pActor, pType);
				}
				if (pActor.city.haveBuildingType("mine", true))
				{
					Building buildingType = pActor.city.getBuildingType("mine", true, false);
					if (buildingType.currentTile.isSameIsland(pActor.currentTile))
					{
						ActorTool.possible_buildings.Add(buildingType);
					}
				}
			}
			if (ActorTool.possible_buildings.Count == 0)
			{
				return null;
			}
			return ActorTool.possible_buildings.GetRandom<Building>();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0009E978 File Offset: 0x0009CB78
		public static Building findNewTargetInZones(Actor pActor, string pType)
		{
			ActorTool.possible_buildings.Clear();
			for (int i = 0; i < pActor.city.zones.Count; i++)
			{
				TileZone tileZone = pActor.city.zones[i];
				HashSet<Building> hashSet = null;
				if (pType != null)
				{
					if (!(pType == "trees"))
					{
						if (!(pType == "gold"))
						{
							if (!(pType == "ore"))
							{
								if (!(pType == "stone"))
								{
									if (pType == "fruitBush")
									{
										hashSet = tileZone.food;
									}
								}
								else
								{
									hashSet = tileZone.stone;
								}
							}
							else
							{
								hashSet = tileZone.ore;
							}
						}
						else
						{
							hashSet = tileZone.gold;
						}
					}
					else
					{
						hashSet = tileZone.trees;
					}
				}
				if (hashSet.Count != 0)
				{
					foreach (Building building in hashSet)
					{
						if (!(building.currentTile.targetedBy != null) && building.currentTile.isSameIsland(pActor.currentTile) && ((building.stats.buildingType != BuildingType.Fruits && building.stats.buildingType != BuildingType.Tree) || building.haveResources))
						{
							ActorTool.possible_buildings.Add(building);
						}
					}
					if (ActorTool.possible_buildings.Count > 0)
					{
						break;
					}
				}
			}
			if (ActorTool.possible_buildings.Count == 0)
			{
				return null;
			}
			return ActorTool.possible_buildings.GetRandom<Building>();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0009EB00 File Offset: 0x0009CD00
		public static WorldTile getTileNearby(ActorTileTarget pTarget, MapChunk pChunk)
		{
			ActorTool.temp_chunks.Clear();
			ActorTool.temp_chunks.Add(pChunk);
			ActorTool.temp_chunks.AddRange(pChunk.neighboursAll);
			ActorTool.possible_moves.Clear();
			for (int i = 0; i < ActorTool.temp_chunks.Count; i++)
			{
				MapChunk mapChunk = ActorTool.temp_chunks[i];
				if (ActorTool.possible_moves.Count <= 20)
				{
					for (int j = 0; j < mapChunk.tiles.Count; j++)
					{
						WorldTile worldTile = mapChunk.tiles[j];
						if (ActorTool.possible_moves.Count <= 20)
						{
							switch (pTarget)
							{
							case ActorTileTarget.RandomTNT:
								if (worldTile.Type.explodable)
								{
									ActorTool.possible_moves.Add(worldTile);
								}
								break;
							case ActorTileTarget.RandomBurnableTile:
								if (worldTile.Type.burnable || worldTile.Type.trees)
								{
									ActorTool.possible_moves.Add(worldTile);
								}
								break;
							case ActorTileTarget.RandomTileWithUnits:
								if (worldTile.units.Count > 0)
								{
									ActorTool.possible_moves.Add(worldTile);
								}
								break;
							case ActorTileTarget.RandomTileWithCivStructures:
								if (worldTile.building != null && worldTile.building.city != null)
								{
									ActorTool.possible_moves.Add(worldTile);
								}
								if (worldTile.Type.burnable && worldTile.zone.city != null)
								{
									ActorTool.possible_moves.Add(worldTile);
								}
								break;
							}
						}
					}
				}
			}
			if (ActorTool.possible_moves.Count == 0)
			{
				return null;
			}
			return ActorTool.possible_moves.GetRandom<WorldTile>();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0009ECA0 File Offset: 0x0009CEA0
		public static Docks getDockTradeTarget(Actor pActor)
		{
			List<Docks> docks = pActor.currentTile.region.island.docks;
			if (docks.Count == 0)
			{
				return null;
			}
			docks.Shuffle<Docks>();
			for (int i = 0; i < docks.Count; i++)
			{
				Docks docks2 = docks[i];
				if (!(pActor.homeBuilding == docks2.building) && (docks2.isDockGood() || docks2.checkOceanTiles()) && !docks2.building.isNonUsable() && !docks2.building.city.kingdom.isEnemy(pActor.kingdom))
				{
					return docks2;
				}
			}
			return null;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0009ED3C File Offset: 0x0009CF3C
		public static WorldTile getRandomTileForBoat(Actor pActor)
		{
			MapRegion mapRegion = pActor.currentTile.region;
			if (mapRegion.neighbours.Count > 0 && Toolbox.randomBool())
			{
				mapRegion = mapRegion.neighbours.GetRandom<MapRegion>();
			}
			if (mapRegion.tiles.Count > 0)
			{
				return mapRegion.tiles.GetRandom<WorldTile>();
			}
			return null;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0009ED94 File Offset: 0x0009CF94
		public static int attributeDice(Actor pActor, int pAmount = 2)
		{
			int num = 0;
			int pMaxExclusive = pActor.curStats.diplomacy + pActor.curStats.warfare + pActor.curStats.stewardship;
			for (int i = 0; i < pAmount; i++)
			{
				num += Toolbox.randomInt(0, pMaxExclusive);
			}
			return num;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0009EDE0 File Offset: 0x0009CFE0
		public static void checkHomeDocks(Actor pActor)
		{
			if (pActor.homeBuilding == null || pActor.homeBuilding.isNonUsable())
			{
				List<Docks> docks = pActor.currentTile.region.island.docks;
				for (int i = 0; i < docks.Count; i++)
				{
					Docks docks2 = docks[i];
					if (!docks2.building.isNonUsable() && !docks2.building.city.kingdom.isEnemy(pActor.kingdom) && docks2.building.city.kingdom == pActor.kingdom)
					{
						docks2.addBoatToDock(pActor);
						return;
					}
				}
			}
			if (pActor.homeBuilding == null || pActor.homeBuilding.isNonUsable())
			{
				pActor.setHomeBuilding(null);
			}
			if (pActor.homeBuilding != null)
			{
				if (!pActor.homeBuilding.currentTile.isSameIsland(pActor.currentTile))
				{
					pActor.homeBuilding.GetComponent<Docks>().removeBoatFromDock(pActor);
					pActor.setHomeBuilding(null);
					return;
				}
				if (!pActor.homeBuilding.GetComponent<Docks>().checkOceanTiles())
				{
					pActor.setHomeBuilding(null);
				}
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0009EEFC File Offset: 0x0009D0FC
		public static void copyUnitToOtherUnit(Actor p1, Actor p2)
		{
			p1.spriteRenderer.enabled = false;
			p2.currentPosition = p1.currentPosition;
			p2.transform.position = p1.transform.position;
			p2.curAngle = p1.transform.localEulerAngles;
			p2.transform.localEulerAngles = p2.curAngle;
			p2.data.firstName = p1.data.firstName;
			p2.data.age = p1.data.age;
			p2.data.kills = p1.data.kills;
			p2.data.children = p1.data.children;
			p2.data.favorite = p1.data.favorite;
			p2.takeItems(p1, p2.stats.take_items_ignore_range_weapons);
			for (int i = 0; i < p1.data.traits.Count; i++)
			{
				string text = p1.data.traits[i];
				if (!(text == "peaceful"))
				{
					p2.addTrait(text);
				}
			}
			p2.setStatsDirty();
			p2._positionDirty = true;
			if (Config.spectatorMode && MoveCamera.focusUnit == p1)
			{
				MoveCamera.focusUnit = p2;
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0009F044 File Offset: 0x0009D244
		public static bool canBeCuredFromTraits(Actor pActor)
		{
			foreach (string pID in pActor.s_traits_ids)
			{
				ActorTrait actorTrait = AssetManager.traits.get(pID);
				if (actorTrait != null && actorTrait.can_be_cured)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0009F0B0 File Offset: 0x0009D2B0
		public static int getBabyColor(Actor pActor1, Actor pActor2)
		{
			int skin = pActor1.data.skin;
			int num = (pActor2 != null) ? pActor2.data.skin : -1;
			int result;
			if (num == -1 || skin == num)
			{
				result = skin;
			}
			else if (Math.Abs(skin - num) == 1)
			{
				if (Toolbox.randomBool())
				{
					result = skin;
				}
				else
				{
					result = num;
				}
			}
			else
			{
				result = (skin + num) / 2;
			}
			return result;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0009F10C File Offset: 0x0009D30C
		public static void setSkinSet(this ActorBase pActor, string pForceUnitSet)
		{
			if (string.IsNullOrEmpty(pForceUnitSet) || !pActor.stats.useSkinColors)
			{
				return;
			}
			if (pActor.stats.color_sets.Count == 0)
			{
				return;
			}
			ActorTool._color_sets.Clear();
			int num = 0;
			for (int i = 0; i < pActor.stats.color_sets.Count; i++)
			{
				if (pActor.stats.color_sets[i].id.Contains(pForceUnitSet))
				{
					ActorTool._color_sets.Add(num);
				}
				num++;
			}
			if (ActorTool._color_sets.Count > 0)
			{
				int random = ActorTool._color_sets.GetRandom<int>();
				pActor.data.skin_set = random;
			}
		}

		// Token: 0x0400151B RID: 5403
		private static List<WorldTile> possible_moves = new List<WorldTile>();

		// Token: 0x0400151C RID: 5404
		private static List<MapChunk> temp_chunks = new List<MapChunk>();

		// Token: 0x0400151D RID: 5405
		private static List<Building> possible_buildings = new List<Building>();

		// Token: 0x0400151E RID: 5406
		private static List<int> _color_sets = new List<int>();
	}
}
