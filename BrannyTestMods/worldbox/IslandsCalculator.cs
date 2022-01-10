using System;
using System.Collections.Generic;

// Token: 0x02000178 RID: 376
public class IslandsCalculator
{
	// Token: 0x06000879 RID: 2169 RVA: 0x0005BD14 File Offset: 0x00059F14
	public IslandsCalculator(MapBox pWorld)
	{
		this.world = pWorld;
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0005BD65 File Offset: 0x00059F65
	public void clear()
	{
		IslandsCalculator.island_id = 0;
		this.islands.Clear();
		this._temp_regions.Clear();
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_next_wave.Clear();
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0005BD99 File Offset: 0x00059F99
	public void update()
	{
		if (this.timer_updateActors > 0f)
		{
			this.timer_updateActors -= this.world.deltaTime;
			return;
		}
		this.recalcActors();
		this.timer_updateActors = 2f;
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0005BDD4 File Offset: 0x00059FD4
	public WorldTile getRandomGround()
	{
		WorldTile worldTile = null;
		if (this.islands.Count > 0)
		{
			TileIsland randomIslandGround = this.getRandomIslandGround(true);
			if (randomIslandGround != null && randomIslandGround.regions.Count > 0)
			{
				worldTile = randomIslandGround.getRandomTile();
			}
		}
		if (worldTile == null)
		{
			worldTile = this.world.tilesList.GetRandom<WorldTile>();
		}
		return worldTile;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0005BE28 File Offset: 0x0005A028
	internal TileIsland getRandomIslandGround(bool pMinRegions = true)
	{
		if (this.islands_ground.Count == 0)
		{
			return null;
		}
		if (!pMinRegions)
		{
			return this.islands_ground.GetRandom<TileIsland>();
		}
		for (int i = 0; i < this.islands_ground.Count; i++)
		{
			this.islands_ground.ShuffleOne(i);
			if (this.islands_ground[i].regions.Count >= 4)
			{
				return this.islands_ground[i];
			}
		}
		return null;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0005BE9C File Offset: 0x0005A09C
	public int countLandIslands()
	{
		int num = 0;
		for (int i = 0; i < this.islands.Count; i++)
		{
			TileIsland tileIsland = this.islands[i];
			if (tileIsland.type == TileLayerType.Ground && tileIsland.regions.Count >= 4)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0005BEEC File Offset: 0x0005A0EC
	internal void recalcActors()
	{
		for (int i = 0; i < this.islands.Count; i++)
		{
			this.islands[i].actors.Clear();
		}
		List<Actor> simpleList = this.world.units.getSimpleList();
		for (int j = 0; j < simpleList.Count; j++)
		{
			Actor actor = simpleList[j];
			if (actor.data.alive && actor.currentTile.region != null && actor.currentTile.region.island != null)
			{
				actor.currentTile.region.island.actors.Add(actor);
			}
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0005BF98 File Offset: 0x0005A198
	private void clearEmptyIslands()
	{
		int i = this.islands.Count;
		while (i > 0)
		{
			if (this.islands[--i].regions.Count == 0)
			{
				this.islands.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0005BFE0 File Offset: 0x0005A1E0
	public void findIslands()
	{
		LogText.log("findIslands", "calculate", "st");
		if (!DebugConfig.isOn(DebugOption.SystemCalculateIslands))
		{
			return;
		}
		this._temp_regions.Clear();
		this.islands_ground.Clear();
		this.clearEmptyIslands();
		for (int i = 0; i < this.world.mapChunkManager.list.Count; i++)
		{
			MapChunk mapChunk = this.world.mapChunkManager.list[i];
			for (int j = 0; j < mapChunk.regions.Count; j++)
			{
				MapRegion mapRegion = mapChunk.regions[j];
				if (mapRegion.island == null)
				{
					mapRegion.isIslandChecked = false;
					this._temp_regions.Add(mapRegion);
				}
			}
		}
		for (int k = 0; k < this._temp_regions.Count; k++)
		{
			MapRegion mapRegion2 = this._temp_regions[k];
			if (mapRegion2.island == null)
			{
				this.newIslandFrom(mapRegion2);
			}
		}
		for (int l = 0; l < this.islands.Count; l++)
		{
			TileIsland tileIsland = this.islands[l];
			tileIsland.regions.getSimpleList().Shuffle<MapRegion>();
			tileIsland.countTiles();
			if (tileIsland.type == TileLayerType.Ground)
			{
				this.islands_ground.Add(tileIsland);
			}
		}
		LogText.log("findIslands", "calculate", "en");
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0005C144 File Offset: 0x0005A344
	private void newIslandFrom(MapRegion pRegion)
	{
		IslandsCalculator.island_id++;
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_next_wave.Clear();
		TileIsland tileIsland = new TileIsland(IslandsCalculator.island_id);
		tileIsland.type = pRegion.type;
		this.islands.Add(tileIsland);
		this._temp_regions_next_wave.Add(pRegion);
		IslandsCalculator._nextWave.Clear();
		IslandsCalculator._nextWave.Add(pRegion);
		this.startFill(tileIsland);
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0005C1C0 File Offset: 0x0005A3C0
	private void startFill(TileIsland pIsland)
	{
		int i = 0;
		while (i < IslandsCalculator._nextWave.Count)
		{
			MapRegion mapRegion = IslandsCalculator._nextWave[i];
			if (!mapRegion.isIslandChecked)
			{
				mapRegion.island = pIsland;
				pIsland.addRegion(mapRegion);
			}
			mapRegion.isIslandChecked = true;
			i++;
			for (int j = 0; j < mapRegion.neighbours.Count; j++)
			{
				MapRegion mapRegion2 = mapRegion.neighbours[j];
				if (!mapRegion2.isIslandChecked)
				{
					mapRegion2.isIslandChecked = true;
					mapRegion2.island = pIsland;
					pIsland.addRegion(mapRegion2);
					IslandsCalculator._nextWave.Add(mapRegion2);
				}
			}
		}
		pIsland.regions.checkAddRemove();
	}

	// Token: 0x04000AF0 RID: 2800
	public MapBox world;

	// Token: 0x04000AF1 RID: 2801
	private float timer_updateActors;

	// Token: 0x04000AF2 RID: 2802
	public List<TileIsland> islands = new List<TileIsland>();

	// Token: 0x04000AF3 RID: 2803
	public List<TileIsland> islands_ground = new List<TileIsland>();

	// Token: 0x04000AF4 RID: 2804
	private List<MapRegion> _temp_regions = new List<MapRegion>();

	// Token: 0x04000AF5 RID: 2805
	private List<MapRegion> _temp_regions_cur_wave = new List<MapRegion>();

	// Token: 0x04000AF6 RID: 2806
	private List<MapRegion> _temp_regions_next_wave = new List<MapRegion>();

	// Token: 0x04000AF7 RID: 2807
	private static int island_id = 0;

	// Token: 0x04000AF8 RID: 2808
	private static List<MapRegion> _nextWave = new List<MapRegion>();
}
