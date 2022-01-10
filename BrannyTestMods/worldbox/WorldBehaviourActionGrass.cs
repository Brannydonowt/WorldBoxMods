using System;
using System.Collections.Generic;

// Token: 0x020000EC RID: 236
public static class WorldBehaviourActionGrass
{
	// Token: 0x060004F0 RID: 1264 RVA: 0x000408AC File Offset: 0x0003EAAC
	public static void clear()
	{
		WorldBehaviourActionGrass._possibleTiles.Clear();
		WorldBehaviourActionGrass._tempIslands.Clear();
		WorldBehaviourActionGrass._regions_to_add.Clear();
		WorldBehaviourActionGrass.last_used_regions.Clear();
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x000408D8 File Offset: 0x0003EAD8
	public static void updateGrass()
	{
		if (!MapBox.instance.worldLaws.world_law_grow_grass.boolVal)
		{
			return;
		}
		WorldBehaviourActionGrass._tempIslands.Clear();
		WorldBehaviourActionGrass._limit = 0;
		for (int i = 0; i < MapBox.instance.islandsCalculator.islands.Count; i++)
		{
			TileIsland tileIsland = MapBox.instance.islandsCalculator.islands[i];
			if (tileIsland.type == TileLayerType.Ground)
			{
				WorldBehaviourActionGrass._tempIslands.Add(tileIsland);
			}
		}
		WorldBehaviourActionGrass._regions_to_add.Clear();
		bool flag = false;
		foreach (MapRegion pRegion in WorldBehaviourActionGrass.last_used_regions)
		{
			WorldBehaviourActionGrass._limit = 0;
			if (WorldBehaviourActionGrass.tryToGrowGrassOnRegion(pRegion, true))
			{
				flag = true;
			}
		}
		if (!flag)
		{
			WorldBehaviourActionGrass.ticks_to_clear--;
		}
		else
		{
			WorldBehaviourActionGrass.ticks_to_clear = 5;
		}
		if (WorldBehaviourActionGrass.ticks_to_clear <= 0 || WorldBehaviourActionGrass.last_used_regions.Count > 100)
		{
			WorldBehaviourActionGrass.ticks_to_clear = 5;
			WorldBehaviourActionGrass.last_used_regions.Clear();
			WorldBehaviourActionGrass._regions_to_add.Clear();
		}
		for (int j = 0; j < WorldBehaviourActionGrass._tempIslands.Count; j++)
		{
			TileIsland tileIsland2 = WorldBehaviourActionGrass._tempIslands[j];
			WorldBehaviourActionGrass._limit = 0;
			int num = tileIsland2.regions.Count / 10 + 1;
			if (num > 5)
			{
				num = 5;
			}
			for (int k = 0; k < num; k++)
			{
				WorldBehaviourActionGrass._region = tileIsland2.regions.GetRandom();
				WorldBehaviourActionGrass.tryToGrowGrassOnRegion(WorldBehaviourActionGrass._region, false);
			}
		}
		for (int l = 0; l < WorldBehaviourActionGrass._regions_to_add.Count; l++)
		{
			MapRegion mapRegion = WorldBehaviourActionGrass._regions_to_add[l];
			WorldBehaviourActionGrass.last_used_regions.Add(mapRegion);
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00040AA0 File Offset: 0x0003ECA0
	private static bool tryToGrowGrassOnRegion(MapRegion pRegion, bool pFromCache = false)
	{
		if (MapBox.instance.gameStats.data.gameTime - pRegion.lastGrassGrowth < 0.5)
		{
			return false;
		}
		pRegion.lastGrassGrowth = MapBox.instance.gameStats.data.gameTime;
		WorldBehaviourActionGrass._region.tiles.ShuffleOne<WorldTile>();
		bool result = false;
		for (int i = 0; i < pRegion.tiles.Count; i++)
		{
			WorldTile worldTile = pRegion.tiles[i];
			if (worldTile.Type.growToNearbyTiles && worldTile.burned_stages <= 0 && !Toolbox.randomBool())
			{
				WorldBehaviourActionGrass._possibleTiles.Clear();
				for (int j = 0; j < worldTile.neighbours.Count; j++)
				{
					WorldTile worldTile2 = worldTile.neighbours[j];
					if (worldTile2.burned_stages <= 0 && worldTile2.Type.canGrowBiomeGrass && !(worldTile2.Type.biome == worldTile.Type.biome))
					{
						bool flag = false;
						if (worldTile2.Type.soil)
						{
							flag = true;
						}
						else if (MapBox.instance.worldLaws.world_law_biome_overgrowth.boolVal)
						{
							if (worldTile2.Type.grassStrength == worldTile.Type.grassStrength)
							{
								if (Toolbox.randomChance(0.05f))
								{
									flag = true;
								}
							}
							else
							{
								int num = Toolbox.randomInt(0, worldTile.Type.grassStrength);
								int num2 = Toolbox.randomInt(0, worldTile2.Type.grassStrength);
								if (num > num2)
								{
									flag = true;
								}
							}
						}
						if (flag)
						{
							WorldBehaviourActionGrass._possibleTiles.Add(worldTile2);
						}
					}
				}
				if (WorldBehaviourActionGrass._possibleTiles.Count != 0)
				{
					WorldTile random = WorldBehaviourActionGrass._possibleTiles.GetRandom<WorldTile>();
					if (random.grassTicksBeforeGrowth > 0)
					{
						random.grassTicksBeforeGrowth--;
					}
					else
					{
						result = true;
						for (int k = 0; k < random.neighbours.Count; k++)
						{
							WorldTile worldTile3 = random.neighbours[k];
							WorldBehaviourActionGrass._regions_to_add.Add(worldTile3.region);
						}
						WorldBehaviourActionGrass._regions_to_add.Add(random.region);
						if (random.main_type.rankType == TileRank.High)
						{
							MapAction.growGreens(random, AssetManager.topTiles.get(worldTile.top_type.biome_high));
						}
						else
						{
							MapAction.growGreens(random, AssetManager.topTiles.get(worldTile.top_type.biome_low));
						}
						if (WorldBehaviourActionGrass._limit++ > 3 || pFromCache)
						{
							break;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x040006FE RID: 1790
	public static int ticks_to_clear = 10;

	// Token: 0x040006FF RID: 1791
	private static int _limit = 0;

	// Token: 0x04000700 RID: 1792
	private static List<WorldTile> _possibleTiles = new List<WorldTile>();

	// Token: 0x04000701 RID: 1793
	private static List<TileIsland> _tempIslands = new List<TileIsland>();

	// Token: 0x04000702 RID: 1794
	private static List<MapRegion> _regions_to_add = new List<MapRegion>();

	// Token: 0x04000703 RID: 1795
	public static HashSetMapRegion last_used_regions = new HashSetMapRegion();

	// Token: 0x04000704 RID: 1796
	private static MapRegion _region;
}
