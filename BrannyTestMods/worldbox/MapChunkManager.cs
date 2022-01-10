using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using pathfinding;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class MapChunkManager
{
	// Token: 0x060008BA RID: 2234 RVA: 0x0005DA30 File Offset: 0x0005BC30
	public MapChunkManager(MapBox pWorld)
	{
		this.world = pWorld;
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0005DA8C File Offset: 0x0005BC8C
	public void update(float pElapsed)
	{
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.updateDirty();
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0005DAB0 File Offset: 0x0005BCB0
	public void prepare()
	{
		this.list.Clear();
		int num = 8;
		this.amountX = Config.ZONE_AMOUNT_X * num;
		this.amountY = Config.ZONE_AMOUNT_Y * num;
		this.map = new MapChunk[this.amountX, this.amountY];
		this.list.Capacity = this.amountX * this.amountY;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < this.amountY; i++)
		{
			for (int j = 0; j < this.amountX; j++)
			{
				MapChunk mapChunk = new MapChunk();
				mapChunk.id = num2++;
				mapChunk.x = j;
				mapChunk.y = i;
				this.map[j, i] = mapChunk;
				if ((j + i) % 2 == 0)
				{
					mapChunk.color = this.color1;
				}
				else
				{
					mapChunk.color = this.color2;
				}
				TileZone zoneByID = this.world.zoneCalculator.getZoneByID(mapChunk.id);
				zoneByID.chunk = mapChunk;
				mapChunk.zone = zoneByID;
				this.list.Add(mapChunk);
				this.fill(mapChunk);
				num3++;
			}
		}
		this.generateNeighbours();
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0005DBE8 File Offset: 0x0005BDE8
	private void fill(MapChunk pChunk)
	{
		int num = 8;
		int num2 = pChunk.x * num;
		int num3 = pChunk.y * num;
		WorldTile tile;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				tile = this.world.GetTile(i + num2, j + num3);
				tile.chunk = pChunk;
				pChunk.addTile(tile, i, j);
			}
		}
		for (int k = 0; k < 8; k++)
		{
			tile = this.world.GetTile(k + num2, num3);
			if (tile != null)
			{
				pChunk.edges_down.Add(tile);
			}
		}
		for (int l = 0; l < 8; l++)
		{
			tile = this.world.GetTile(l + num2, num3 + 8);
			if (tile != null)
			{
				pChunk.edges_up.Add(tile);
			}
		}
		for (int m = 0; m < 8; m++)
		{
			tile = this.world.GetTile(num2, num3 + m);
			if (tile != null)
			{
				pChunk.edges_left.Add(tile);
			}
		}
		for (int n = 0; n < 8; n++)
		{
			tile = this.world.GetTile(num2 + 8, num3 + n);
			if (tile != null)
			{
				pChunk.edges_right.Add(tile);
			}
		}
		tile = this.world.GetTile(num2 + 8, num3 + 8);
		if (tile != null)
		{
			pChunk.edge_special.Add(tile);
		}
		tile = this.world.GetTile(num2, num3);
		if (tile != null)
		{
			pChunk.edge_special_bottom.Add(tile);
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0005DD54 File Offset: 0x0005BF54
	private void generateNeighbours()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			MapChunk mapChunk = this.list[i];
			mapChunk.neighbours = new List<MapChunk>();
			mapChunk.neighboursAll = new List<MapChunk>();
			MapChunk pNeighbour = this.Get(mapChunk.x - 1, mapChunk.y);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Left, false);
			pNeighbour = this.Get(mapChunk.x + 1, mapChunk.y);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Right, false);
			pNeighbour = this.Get(mapChunk.x, mapChunk.y - 1);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Down, false);
			pNeighbour = this.Get(mapChunk.x, mapChunk.y + 1);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Up, false);
			pNeighbour = this.Get(mapChunk.x - 1, mapChunk.y - 1);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Null, true);
			pNeighbour = this.Get(mapChunk.x - 1, mapChunk.y + 1);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Null, true);
			pNeighbour = this.Get(mapChunk.x + 1, mapChunk.y - 1);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Null, true);
			pNeighbour = this.Get(mapChunk.x + 1, mapChunk.y + 1);
			mapChunk.AddNeighbour(pNeighbour, TileDirection.Null, true);
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0005DE98 File Offset: 0x0005C098
	public MapChunk Get(int x, int y)
	{
		if (x < 0 || x >= this.amountX || y < 0 || y >= this.amountY)
		{
			return null;
		}
		return this.map[x, y];
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0005DEC3 File Offset: 0x0005C0C3
	public MapChunk getByID(int pID)
	{
		return this.list[pID];
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0005DED4 File Offset: 0x0005C0D4
	public void clearAll()
	{
		this.world.islandsCalculator.clear();
		this.dirtyChunks_links.Clear();
		this.dirtyChunks_regions.Clear();
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].clearFull();
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0005DF2E File Offset: 0x0005C12E
	public void setDirty(MapChunk pChunk, bool pRegions = true, bool pLinks = true)
	{
		if (pRegions && !pChunk.dirty_regions)
		{
			pChunk.dirty_regions = true;
			this.dirtyChunks_regions.Add(pChunk);
		}
		if (pLinks && !pChunk.dirty_links)
		{
			pChunk.dirty_links = true;
			this.dirtyChunks_links.Add(pChunk);
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0005DF6C File Offset: 0x0005C16C
	public void allDirty()
	{
		this.dirtyChunks_links.Clear();
		this.dirtyChunks_regions.Clear();
		for (int i = 0; i < this.list.Count; i++)
		{
			MapChunk mapChunk = this.list[i];
			mapChunk.dirty_links = true;
			mapChunk.dirty_regions = true;
		}
		this.dirtyChunks_links.AddRange(this.list);
		this.dirtyChunks_regions.AddRange(this.list);
		this.updateDirty();
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0005DFE8 File Offset: 0x0005C1E8
	public void updateDirty()
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateDirtyChunks))
		{
			return;
		}
		if (this.world.isActionHappening())
		{
			return;
		}
		if (this.dirtyChunks_links.Count == 0 && this.dirtyChunks_regions.Count == 0)
		{
			return;
		}
		Toolbox.bench("chunk_total");
		this.timer = 0.4f;
		LogText.log("ChunkManager", "updateDirty " + this.dirtyChunks_regions.Count.ToString(), "st");
		this.m_dirtyChunks = this.dirtyChunks_regions.Count;
		MapChunkManager.m_newRegions = 0;
		MapChunkManager.m_newLinks = 0;
		MapChunkManager.m_newIslands = 0;
		Toolbox.bench("clear_regions");
		for (int i = 0; i < this.dirtyChunks_regions.Count; i++)
		{
			MapChunk mapChunk = this.dirtyChunks_regions[i];
			mapChunk.clearFull();
		}
		for (int j = 0; j < this.dirtyChunks_links.Count; j++)
		{
			MapChunk mapChunk = this.dirtyChunks_links[j];
			mapChunk.clearIsland();
		}
		Toolbox.benchEnd("clear_regions");
		Toolbox.bench("calc_regions");
		if (MapChunkManager.PARALLEL)
		{
			Parallel.ForEach<MapChunk>(this.dirtyChunks_regions, delegate(MapChunk tChunk)
			{
				tChunk.calculateRegions();
			});
		}
		else
		{
			for (int k = 0; k < this.dirtyChunks_regions.Count; k++)
			{
				MapChunk mapChunk = this.dirtyChunks_regions[k];
				mapChunk.calculateRegions();
			}
		}
		Toolbox.benchEnd("calc_regions");
		Toolbox.bench("shuffle_region_tiles");
		for (int l = 0; l < this.dirtyChunks_regions.Count; l++)
		{
			MapChunk mapChunk = this.dirtyChunks_regions[l];
			MapChunkManager.m_newRegions += mapChunk.regions.Count;
			mapChunk.shuffleRegionTiles();
		}
		Toolbox.benchEnd("shuffle_region_tiles");
		Toolbox.bench("calc_links");
		if (MapChunkManager.PARALLEL)
		{
			Parallel.ForEach<MapChunk>(this.dirtyChunks_links, delegate(MapChunk tChunk)
			{
				tChunk.calculateLinks(tChunk);
			});
		}
		else
		{
			for (int m = 0; m < this.dirtyChunks_links.Count; m++)
			{
				MapChunk mapChunk = this.dirtyChunks_links[m];
				mapChunk.calculateLinks(mapChunk);
			}
		}
		Toolbox.benchEnd("calc_links");
		Toolbox.bench("create_links");
		for (int n = 0; n < this.dirtyChunks_links.Count; n++)
		{
			MapChunk mapChunk = this.dirtyChunks_links[n];
			mapChunk.checkLinksResults();
		}
		Toolbox.benchEnd("create_links");
		Toolbox.bench("calc_linked_regions");
		if (MapChunkManager.PARALLEL)
		{
			Parallel.ForEach<MapChunk>(this.dirtyChunks_links, delegate(MapChunk tChunk)
			{
				this.calculateRegionNeighbours(tChunk);
			});
		}
		else
		{
			for (int num = 0; num < this.dirtyChunks_links.Count; num++)
			{
				MapChunk mapChunk = this.dirtyChunks_links[num];
				this.calculateRegionNeighbours(mapChunk);
			}
		}
		Toolbox.benchEnd("calc_linked_regions");
		Toolbox.bench("findIslands");
		this.world.islandsCalculator.findIslands();
		Toolbox.benchEnd("findIslands");
		Toolbox.bench("center_regions");
		for (int num2 = 0; num2 < this.world.islandsCalculator.islands.Count; num2++)
		{
			List<MapRegion> simpleList = this.world.islandsCalculator.islands[num2].regions.getSimpleList();
			for (int num3 = 0; num3 < simpleList.Count; num3++)
			{
				simpleList[num3].calculateCenterRegion();
			}
		}
		Toolbox.benchEnd("center_regions");
		this.dirtyChunks_links.Clear();
		this.dirtyChunks_regions.Clear();
		this.world.cityPlaceFinder.setDirty();
		this.world.regionPathFinder.clearCache();
		CachedRaycastIslands.clear();
		Toolbox.benchEnd("chunk_total");
		LogText.log("ChunkManager", "updateDirty", "en");
		Toolbox.benchCounterSet("m_dirtyChunks", (float)this.m_dirtyChunks);
		Toolbox.benchCounterSet("m_newRegions", (float)MapChunkManager.m_newRegions);
		Toolbox.benchCounterSet("m_newLinks", (float)MapChunkManager.m_newLinks);
		Toolbox.benchCounterSet("m_newIslands", (float)MapChunkManager.m_newIslands);
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0005E42C File Offset: 0x0005C62C
	private void calculateRegionNeighbours(MapChunk pChunk)
	{
		for (int i = 0; i < pChunk.regions.Count; i++)
		{
			pChunk.regions[i].calculateNeighbours();
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0005E460 File Offset: 0x0005C660
	public int calcRegions()
	{
		int num = 0;
		for (int i = 0; i < this.list.Count; i++)
		{
			MapChunk mapChunk = this.list[i];
			num += mapChunk.regions.Count;
		}
		return num;
	}

	// Token: 0x04000B32 RID: 2866
	public static bool PARALLEL = true;

	// Token: 0x04000B33 RID: 2867
	public Color color1 = Color.gray;

	// Token: 0x04000B34 RID: 2868
	public Color color2 = Color.white;

	// Token: 0x04000B35 RID: 2869
	internal MapChunk[,] map;

	// Token: 0x04000B36 RID: 2870
	public List<MapChunk> list = new List<MapChunk>();

	// Token: 0x04000B37 RID: 2871
	private MapBox world;

	// Token: 0x04000B38 RID: 2872
	internal List<MapChunk> dirtyChunks_regions = new List<MapChunk>();

	// Token: 0x04000B39 RID: 2873
	internal List<MapChunk> dirtyChunks_links = new List<MapChunk>();

	// Token: 0x04000B3A RID: 2874
	internal int m_dirtyChunks;

	// Token: 0x04000B3B RID: 2875
	internal static int m_newRegions = 0;

	// Token: 0x04000B3C RID: 2876
	public static int m_newLinks = 0;

	// Token: 0x04000B3D RID: 2877
	internal static int m_newIslands = 0;

	// Token: 0x04000B3E RID: 2878
	internal float timer = 0.4f;

	// Token: 0x04000B3F RID: 2879
	private int amountX;

	// Token: 0x04000B40 RID: 2880
	private int amountY;
}
