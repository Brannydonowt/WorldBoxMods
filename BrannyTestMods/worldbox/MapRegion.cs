using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000183 RID: 387
public class MapRegion
{
	// Token: 0x060008DD RID: 2269 RVA: 0x0005EDD8 File Offset: 0x0005CFD8
	public MapRegion()
	{
		this.checkDebugLists();
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0005EE42 File Offset: 0x0005D042
	private void checkDebugLists()
	{
		if (!DebugConfig.isOn(DebugOption.Connections))
		{
			return;
		}
		if (this.edges_left == null)
		{
			this.edges_left = new List<WorldTile>();
			this.edges_up = new List<WorldTile>();
			this.edges_down = new List<WorldTile>();
			this.edges_right = new List<WorldTile>();
		}
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0005EE84 File Offset: 0x0005D084
	internal int newConnection(List<WorldTile> pTiles, LinkDirection pDirection, LinkDirection pDirectionID)
	{
		if (pDirection != LinkDirection.Up)
		{
			if (pDirection == LinkDirection.Right)
			{
				for (int i = 0; i < pTiles.Count; i++)
				{
					pTiles[i].edge_region_right = this;
				}
			}
		}
		else
		{
			for (int j = 0; j < pTiles.Count; j++)
			{
				pTiles[j].edge_region_up = this;
			}
		}
		return this.newHash(pTiles, pTiles.Count, pDirection, pDirectionID);
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0005EEE8 File Offset: 0x0005D0E8
	private void checkDebugEdges(List<WorldTile> pTiles, LinkDirection pDirection)
	{
		if (!DebugConfig.isOn(DebugOption.Connections))
		{
			return;
		}
		List<WorldTile> list = null;
		this.checkDebugLists();
		switch (pDirection)
		{
		case LinkDirection.Up:
			list = this.edges_up;
			break;
		case LinkDirection.Down:
			list = this.edges_down;
			break;
		case LinkDirection.Left:
			list = this.edges_left;
			break;
		case LinkDirection.Right:
			list = this.edges_right;
			break;
		}
		list.AddRange(pTiles);
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0005EF48 File Offset: 0x0005D148
	internal int newHash(List<WorldTile> pTiles, int pLen, LinkDirection pDirection, LinkDirection pDirectionID)
	{
		WorldTile worldTile = pTiles[0];
		int x = worldTile.x;
		int y = worldTile.y;
		int num = (int)((x * 199 + y * 7) * pLen * 10000 + pLen * 1000 + (worldTile.Type.layerType + 1) * (TileLayerType)100 + x * 10 + y);
		if (pDirectionID == LinkDirection.LR)
		{
			num = -num;
		}
		return num;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0005EFA8 File Offset: 0x0005D1A8
	public void clear()
	{
		for (int i = 0; i < this.links.Count; i++)
		{
			RegionLinkHashes.remove(this.links[i], this);
		}
		this.links.Clear();
		this.regionPathID = -1;
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0005EFF0 File Offset: 0x0005D1F0
	public void calculateNeighbours()
	{
		this.neighbours.Clear();
		this._neighbours_hash.Clear();
		for (int i = 0; i < this.links.Count; i++)
		{
			foreach (MapRegion mapRegion in this.links[i].regions)
			{
				if (mapRegion != this && !this._neighbours_hash.Contains(mapRegion))
				{
					this._neighbours_hash.Add(mapRegion);
				}
			}
		}
		foreach (MapRegion mapRegion2 in this._neighbours_hash)
		{
			this.neighbours.Add(mapRegion2);
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0005F0DC File Offset: 0x0005D2DC
	public void debugLinks(DebugTool pTool)
	{
		pTool.setText("- links:", this.links.Count);
		List<RegionLink> list = Enumerable.ToList<RegionLink>(this.links);
		for (int i = 0; i < list.Count; i++)
		{
			RegionLink regionLink = list[i];
			pTool.setText("- hash " + i.ToString() + ":", list[i].id);
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x0005F158 File Offset: 0x0005D358
	public void calculateCenterRegion()
	{
		this.centerRegion = true;
		if (this.chunk.regions.Count > 1)
		{
			this.centerRegion = false;
		}
		else
		{
			for (int i = 0; i < this.chunk.neighboursAll.Count; i++)
			{
				MapChunk mapChunk = this.chunk.neighboursAll[i];
				if (mapChunk.regions.Count > 1)
				{
					this.centerRegion = false;
					break;
				}
				if (mapChunk.regions[0].island != this.island)
				{
					this.centerRegion = false;
					break;
				}
			}
		}
		if (!this.centerRegion)
		{
			this.island.regionsCorners.Add(this);
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0005F208 File Offset: 0x0005D408
	public void calculateTileCorners()
	{
		if (this.centerRegion)
		{
			return;
		}
		if (!this.cornersDirty)
		{
			return;
		}
		this.cornersDirty = false;
		for (int i = 0; i < this.tiles.Count; i++)
		{
			WorldTile worldTile = this.tiles[i];
			for (int j = 0; j < worldTile.neighboursAll.Count; j++)
			{
				WorldTile worldTile2 = worldTile.neighboursAll[j];
				if (worldTile2.Type.layerType != this.type)
				{
					this.tiles_corners.Add(worldTile2);
				}
			}
		}
		this.tiles_corners.Shuffle<WorldTile>();
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0005F29E File Offset: 0x0005D49E
	public List<WorldTile> getTileCorners()
	{
		if (this.cornersDirty)
		{
			this.calculateTileCorners();
		}
		return this.tiles_corners;
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0005F2B4 File Offset: 0x0005D4B4
	public override int GetHashCode()
	{
		return this.id;
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0005F2BC File Offset: 0x0005D4BC
	public override bool Equals(object obj)
	{
		return this.Equals(obj as MapRegion);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0005F2CA File Offset: 0x0005D4CA
	public bool Equals(MapRegion pObject)
	{
		return this.id == pObject.id;
	}

	// Token: 0x04000B5A RID: 2906
	public double lastGrassGrowth;

	// Token: 0x04000B5B RID: 2907
	public double lastBurnedUpdate;

	// Token: 0x04000B5C RID: 2908
	public int id;

	// Token: 0x04000B5D RID: 2909
	public int zone_id;

	// Token: 0x04000B5E RID: 2910
	public TileZone zone;

	// Token: 0x04000B5F RID: 2911
	public bool usedByPathLock;

	// Token: 0x04000B60 RID: 2912
	public int regionPathID = -1;

	// Token: 0x04000B61 RID: 2913
	public TileLayerType type;

	// Token: 0x04000B62 RID: 2914
	private List<WorldTile> tiles_corners = new List<WorldTile>(4);

	// Token: 0x04000B63 RID: 2915
	public List<WorldTile> tiles = new List<WorldTile>(64);

	// Token: 0x04000B64 RID: 2916
	internal List<RegionLink> links = new List<RegionLink>(4);

	// Token: 0x04000B65 RID: 2917
	public List<WorldTile> edges_left;

	// Token: 0x04000B66 RID: 2918
	public List<WorldTile> edges_up;

	// Token: 0x04000B67 RID: 2919
	public List<WorldTile> edges_down;

	// Token: 0x04000B68 RID: 2920
	public List<WorldTile> edges_right;

	// Token: 0x04000B69 RID: 2921
	public TileIsland island;

	// Token: 0x04000B6A RID: 2922
	public bool isIslandChecked;

	// Token: 0x04000B6B RID: 2923
	public bool isCheckedPath;

	// Token: 0x04000B6C RID: 2924
	public int path_wave_id = -1;

	// Token: 0x04000B6D RID: 2925
	public bool regionPath;

	// Token: 0x04000B6E RID: 2926
	public MapChunk chunk;

	// Token: 0x04000B6F RID: 2927
	public bool cornersDirty = true;

	// Token: 0x04000B70 RID: 2928
	public bool centerRegion;

	// Token: 0x04000B71 RID: 2929
	public List<MapRegion> neighbours = new List<MapRegion>(4);

	// Token: 0x04000B72 RID: 2930
	private HashSet<MapRegion> _neighbours_hash = new HashSet<MapRegion>();
}
