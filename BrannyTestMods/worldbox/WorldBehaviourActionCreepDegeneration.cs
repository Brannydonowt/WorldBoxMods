using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public static class WorldBehaviourActionCreepDegeneration
{
	// Token: 0x060004E1 RID: 1249 RVA: 0x00040256 File Offset: 0x0003E456
	public static void checkCreep()
	{
		WorldBehaviourActionCreepDegeneration.checkBiome("tumor", "tumor");
		WorldBehaviourActionCreepDegeneration.checkBiome("biomass", "biomass");
		WorldBehaviourActionCreepDegeneration.checkBiome("superPumpkin", "pumpkin");
		WorldBehaviourActionCreepDegeneration.checkBiome("cybercore", "cybertile");
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00040294 File Offset: 0x0003E494
	private static void checkBiome(string pCreepHubID, string pBiomeID)
	{
		BuildingAsset buildingAsset = AssetManager.buildings.get(pCreepHubID);
		Kingdom kingdomByID = MapBox.instance.kingdoms.getKingdomByID(buildingAsset.kingdom);
		WorldBehaviourActionCreepDegeneration.clear();
		WorldBehaviourActionCreepDegeneration.addToNotChecked(pBiomeID + "_low");
		WorldBehaviourActionCreepDegeneration.addToNotChecked(pBiomeID + "_high");
		if (WorldBehaviourActionCreepDegeneration.not_checked_tiles.Count == 0)
		{
			return;
		}
		if (kingdomByID.buildings.Count > 0)
		{
			List<Building> simpleList = kingdomByID.buildings.getSimpleList();
			for (int i = 0; i < simpleList.Count; i++)
			{
				Building building = simpleList[i];
				if (building.data.alive)
				{
					WorldBehaviourActionCreepDegeneration.checkTile(building.currentTile);
					WorldBehaviourActionCreepDegeneration.next_wave.Add(building.currentTile);
				}
			}
		}
		WorldBehaviourActionCreepDegeneration.startWave(pBiomeID);
		if (WorldBehaviourActionCreepDegeneration.not_checked_tiles.Count > 0)
		{
			WorldBehaviourActionCreepDegeneration.destroyNonCheckedCreep();
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0004036C File Offset: 0x0003E56C
	private static void startWave(string pBiomeID)
	{
		if (WorldBehaviourActionCreepDegeneration.next_wave.Count == 0)
		{
			return;
		}
		WorldBehaviourActionCreepDegeneration.cur_wave.AddRange(WorldBehaviourActionCreepDegeneration.next_wave);
		WorldBehaviourActionCreepDegeneration.next_wave.Clear();
		while (WorldBehaviourActionCreepDegeneration.cur_wave.Count > 0)
		{
			WorldTile worldTile = WorldBehaviourActionCreepDegeneration.cur_wave[WorldBehaviourActionCreepDegeneration.cur_wave.Count - 1];
			WorldBehaviourActionCreepDegeneration.cur_wave.RemoveAt(WorldBehaviourActionCreepDegeneration.cur_wave.Count - 1);
			for (int i = 0; i < worldTile.neighboursAll.Count; i++)
			{
				WorldTile worldTile2 = worldTile.neighboursAll[i];
				if (!(worldTile2.Type.biome != pBiomeID) && !WorldBehaviourActionCreepDegeneration.checked_tiles.Contains(worldTile2))
				{
					WorldBehaviourActionCreepDegeneration.checkTile(worldTile2);
					WorldBehaviourActionCreepDegeneration.next_wave.Add(worldTile2);
				}
			}
		}
		if (WorldBehaviourActionCreepDegeneration.next_wave.Count > 0)
		{
			WorldBehaviourActionCreepDegeneration.startWave(pBiomeID);
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00040448 File Offset: 0x0003E648
	private static void destroyNonCheckedCreep()
	{
		WorldBehaviourActionCreepDegeneration._list_of_disconnected_tiles.Clear();
		foreach (WorldTile worldTile in WorldBehaviourActionCreepDegeneration.not_checked_tiles)
		{
			WorldBehaviourActionCreepDegeneration._list_of_disconnected_tiles.Add(worldTile);
		}
		int num = Mathf.Min(WorldBehaviourActionCreepDegeneration._list_of_disconnected_tiles.Count, 3);
		for (int i = 0; i < num; i++)
		{
			WorldBehaviourActionCreepDegeneration._list_of_disconnected_tiles.ShuffleOne(i);
			MapAction.decreaseTile(WorldBehaviourActionCreepDegeneration._list_of_disconnected_tiles[i], "flash");
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x000404E8 File Offset: 0x0003E6E8
	private static void checkTile(WorldTile pTile)
	{
		WorldBehaviourActionCreepDegeneration.checked_tiles.Add(pTile);
		WorldBehaviourActionCreepDegeneration.not_checked_tiles.Remove(pTile);
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00040504 File Offset: 0x0003E704
	private static void addToNotChecked(string pTileID)
	{
		TopTileType topTileType = AssetManager.topTiles.get(pTileID);
		if (topTileType.hashset.Count == 0)
		{
			return;
		}
		WorldBehaviourActionCreepDegeneration.not_checked_tiles.UnionWith(topTileType.hashset);
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0004053B File Offset: 0x0003E73B
	public static void clear()
	{
		WorldBehaviourActionCreepDegeneration.checked_tiles.Clear();
		WorldBehaviourActionCreepDegeneration.not_checked_tiles.Clear();
		WorldBehaviourActionCreepDegeneration.next_wave.Clear();
		WorldBehaviourActionCreepDegeneration.cur_wave.Clear();
	}

	// Token: 0x040006F5 RID: 1781
	private const int MAX_CREEP_TO_DESTROY_IN_ONE_STEP = 3;

	// Token: 0x040006F6 RID: 1782
	private static List<WorldTile> next_wave = new List<WorldTile>();

	// Token: 0x040006F7 RID: 1783
	private static List<WorldTile> cur_wave = new List<WorldTile>();

	// Token: 0x040006F8 RID: 1784
	private static HashSetWorldTile checked_tiles = new HashSetWorldTile();

	// Token: 0x040006F9 RID: 1785
	private static HashSetWorldTile not_checked_tiles = new HashSetWorldTile();

	// Token: 0x040006FA RID: 1786
	private static List<WorldTile> _list_of_disconnected_tiles = new List<WorldTile>();
}
