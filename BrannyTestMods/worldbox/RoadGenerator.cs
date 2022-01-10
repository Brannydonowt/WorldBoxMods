using System;
using System.Collections.Generic;

// Token: 0x02000188 RID: 392
public class RoadGenerator
{
	// Token: 0x06000905 RID: 2309 RVA: 0x00060384 File Offset: 0x0005E584
	internal static void generateRoadFor(Building pBuilding)
	{
		if (RoadGenerator.world == null)
		{
			RoadGenerator.world = MapBox.instance;
		}
		City city = pBuilding.city;
		if (city == null || city.buildings.Count <= 2)
		{
			return;
		}
		if (!pBuilding.doorTile.Type.ground)
		{
			return;
		}
		RoadGenerator.calcFlow(pBuilding.doorTile, pBuilding, 12);
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x000603E8 File Offset: 0x0005E5E8
	private static void calcFlow(WorldTile pStartTile, Building pBuilding, int pMaxWave)
	{
		RoadGenerator.targetBuildings.Clear();
		RoadGenerator.checkedTiles.Clear();
		RoadGenerator.curWave.Clear();
		RoadGenerator.nextWave.Clear();
		RoadGenerator.nextWave.Add(pStartTile);
		RoadGenerator.checkedTiles.Add(pStartTile);
		int num = 0;
		while (RoadGenerator.nextWave.Count > 0 || RoadGenerator.curWave.Count > 0)
		{
			if (RoadGenerator.curWave.Count == 0)
			{
				if (num > pMaxWave)
				{
					break;
				}
				RoadGenerator.curWave.AddRange(RoadGenerator.nextWave);
				RoadGenerator.nextWave.Clear();
				num++;
			}
			WorldTile worldTile = RoadGenerator.curWave[RoadGenerator.curWave.Count - 1];
			RoadGenerator.curWave.RemoveAt(RoadGenerator.curWave.Count - 1);
			worldTile.checkedTile = true;
			worldTile.score = num;
			RoadGenerator.world.flashEffects.flashPixel(worldTile, -1, ColorType.White);
			if (worldTile.building != null && worldTile.building.city == pBuilding.city && worldTile.building != pBuilding && worldTile.building.currentTile == worldTile)
			{
				RoadGenerator.targetBuildings.Add(worldTile.building);
			}
			for (int i = 0; i < worldTile.neighboursAll.Count; i++)
			{
				WorldTile worldTile2 = worldTile.neighboursAll[i];
				if (!worldTile2.checkedTile)
				{
					worldTile2.checkedTile = true;
					RoadGenerator.checkedTiles.Add(worldTile2);
					if (!worldTile2.Type.liquid && worldTile2.Type.layerType != TileLayerType.Block)
					{
						RoadGenerator.nextWave.Add(worldTile2);
					}
				}
			}
		}
		for (int j = 0; j < RoadGenerator.targetBuildings.Count; j++)
		{
			Building building = RoadGenerator.targetBuildings[j];
			RoadGenerator.path.Clear();
			if (building.stats.buildRoadTo)
			{
				RoadGenerator.findPath(building.doorTile);
				RoadGenerator.fillPath();
			}
		}
		for (int k = 0; k < RoadGenerator.checkedTiles.Count; k++)
		{
			WorldTile worldTile3 = RoadGenerator.checkedTiles[k];
			worldTile3.checkedTile = false;
			worldTile3.score = -1;
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00060610 File Offset: 0x0005E810
	private static void fillPath()
	{
		for (int i = 0; i < RoadGenerator.path.Count; i++)
		{
			MapAction.createRoad(RoadGenerator.path[i]);
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00060644 File Offset: 0x0005E844
	private static void findPath(WorldTile pTile)
	{
		RoadGenerator.path.Add(pTile);
		if (pTile.score > 1)
		{
			WorldTile worldTile = null;
			for (int i = 0; i < pTile.neighboursAll.Count; i++)
			{
				WorldTile worldTile2 = pTile.neighboursAll[i];
				if (worldTile2.score == pTile.score - 1)
				{
					if (worldTile == null)
					{
						worldTile = worldTile2;
					}
					else if (worldTile2.Type.road)
					{
						worldTile = worldTile2;
						RoadGenerator.findPath(worldTile);
						return;
					}
				}
			}
			RoadGenerator.findPath(worldTile);
			return;
		}
	}

	// Token: 0x04000B81 RID: 2945
	internal static MapBox world;

	// Token: 0x04000B82 RID: 2946
	internal static List<WorldTile> checkedTiles = new List<WorldTile>();

	// Token: 0x04000B83 RID: 2947
	private static List<WorldTile> nextWave = new List<WorldTile>();

	// Token: 0x04000B84 RID: 2948
	private static List<WorldTile> curWave = new List<WorldTile>();

	// Token: 0x04000B85 RID: 2949
	private static List<Building> targetBuildings = new List<Building>();

	// Token: 0x04000B86 RID: 2950
	private static List<WorldTile> path = new List<WorldTile>();
}
