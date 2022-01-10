using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class MapChunk
{
	// Token: 0x060008A9 RID: 2217 RVA: 0x0005D160 File Offset: 0x0005B360
	internal void addTile(WorldTile pTile, int pX, int pY)
	{
		this.tiles.Add(pTile);
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0005D170 File Offset: 0x0005B370
	internal void AddNeighbour(MapChunk pNeighbour, TileDirection pDirection, bool pDiagonal = false)
	{
		if (pNeighbour == null)
		{
			this.world_edge = true;
			return;
		}
		if (!pDiagonal)
		{
			this.neighbours.Add(pNeighbour);
		}
		this.neighboursAll.Add(pNeighbour);
		switch (pDirection)
		{
		case TileDirection.Left:
			this.chunk_left = pNeighbour;
			return;
		case TileDirection.Right:
			this.chunk_right = pNeighbour;
			return;
		case TileDirection.Up:
			this.chunk_up = pNeighbour;
			return;
		case TileDirection.Down:
			this.chunk_down = pNeighbour;
			return;
		default:
			return;
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0005D1DC File Offset: 0x0005B3DC
	public void calculateRegions()
	{
		this.dirty_regions = false;
		for (int i = 0; i < this.tiles.Count; i++)
		{
			WorldTile worldTile = this.tiles[i];
			worldTile.checkedTile = false;
			worldTile.region = null;
			worldTile.edge_region_right = null;
			worldTile.edge_region_up = null;
		}
		bool pFirstCheck = true;
		for (int j = 0; j < this.tiles.Count; j++)
		{
			WorldTile worldTile2 = this.tiles[j];
			if (worldTile2.region == null)
			{
				MapRegion mapRegion = new MapRegion();
				mapRegion.type = worldTile2.Type.layerType;
				mapRegion.chunk = this;
				bool flag = this.newRegion(worldTile2, worldTile2.Type.layerType, mapRegion, pFirstCheck);
				WorldTile worldTile3 = mapRegion.tiles[0];
				mapRegion.id = worldTile3.zone.id * 1000 + this.regions.Count;
				mapRegion.zone_id = worldTile3.zone.id;
				mapRegion.zone = worldTile3.zone;
				this.regions.Add(mapRegion);
				if (flag)
				{
					break;
				}
				pFirstCheck = false;
			}
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0005D300 File Offset: 0x0005B500
	internal void shuffleRegionTiles()
	{
		for (int i = 0; i < this.regions.Count; i++)
		{
			MapRegion mapRegion = this.regions[i];
			if (this.regions.Count > 1)
			{
				mapRegion.centerRegion = false;
			}
			else
			{
				mapRegion.centerRegion = true;
			}
			mapRegion.tiles.Shuffle<WorldTile>();
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0005D35C File Offset: 0x0005B55C
	private bool newRegion(WorldTile pTile, TileLayerType pType, MapRegion pTargetRegion, bool pFirstCheck)
	{
		this._nextWave.Clear();
		this._nextWave.Add(pTile);
		if (pFirstCheck)
		{
			bool flag = false;
			for (int i = 0; i < this.tiles.Count; i++)
			{
				if (this.tiles[i].Type.layerType != pType)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < this.tiles.Count; j++)
				{
					WorldTile worldTile = this.tiles[j];
					worldTile.region = pTargetRegion;
					pTargetRegion.tiles.Add(worldTile);
				}
				return true;
			}
		}
		int k = 0;
		while (k < this._nextWave.Count)
		{
			WorldTile worldTile2 = this._nextWave[k];
			worldTile2.checkedTile = true;
			worldTile2.region = pTargetRegion;
			pTargetRegion.tiles.Add(worldTile2);
			k++;
			for (int l = 0; l < worldTile2.neighboursAll.Count; l++)
			{
				WorldTile worldTile3 = worldTile2.neighboursAll[l];
				if (!worldTile3.checkedTile && worldTile3.Type.layerType == worldTile2.region.type && worldTile3.chunk == this)
				{
					worldTile3.checkedTile = true;
					worldTile3.region = pTargetRegion;
					this._nextWave.Add(worldTile3);
				}
			}
		}
		return pTargetRegion.tiles.Count == 64;
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0005D4C3 File Offset: 0x0005B6C3
	private bool canContinue(WorldTile pTile, TileLayerType pType, MapRegion pTargetRegion)
	{
		return pTile != null && !pTile.checkedTile && pTile.Type.layerType == pType && pTile.chunk == this;
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0005D4EC File Offset: 0x0005B6EC
	public void clearObjects()
	{
		this.k_dict_objects.Clear();
		this.k_list_objects.Clear();
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0005D504 File Offset: 0x0005B704
	public void addObject(BaseSimObject pUnit)
	{
		if (pUnit.kingdom == null)
		{
			return;
		}
		MapChunk._objectList = null;
		this.k_dict_objects.TryGetValue(pUnit.kingdom, ref MapChunk._objectList);
		if (MapChunk._objectList == null)
		{
			MapChunk._objectList = new List<BaseSimObject>();
			this.k_list_objects.Add(pUnit.kingdom);
			this.k_dict_objects[pUnit.kingdom] = MapChunk._objectList;
		}
		MapChunk._objectList.Add(pUnit);
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0005D57A File Offset: 0x0005B77A
	public void clearFull()
	{
		this.clearIsland();
		this.regions.Clear();
		this.clearObjects();
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0005D594 File Offset: 0x0005B794
	public void clearIsland()
	{
		for (int i = 0; i < this.regions.Count; i++)
		{
			MapRegion mapRegion = this.regions[i];
			mapRegion.clear();
			if (mapRegion.island != null)
			{
				mapRegion.island.clearRegionsFromIsland();
			}
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0005D5E0 File Offset: 0x0005B7E0
	internal void calculateLinks(MapChunk pChunk)
	{
		pChunk.dirty_links = false;
		this.calculateLink(pChunk.edges_right, LinkDirection.Right, LinkDirection.LR);
		this.calculateLink(pChunk.edges_left, LinkDirection.Left, LinkDirection.LR);
		this.calculateLink(pChunk.edges_up, LinkDirection.Up, LinkDirection.UD);
		this.calculateLink(pChunk.edges_down, LinkDirection.Down, LinkDirection.UD);
		if (pChunk.edge_special.Count > 0)
		{
			this.calculateLink(pChunk.edge_special, LinkDirection.Up, LinkDirection.UD);
		}
		if (pChunk.edge_special_bottom.Count > 0)
		{
			this.calculateLink(pChunk.edge_special_bottom, LinkDirection.Down, LinkDirection.UD);
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0005D664 File Offset: 0x0005B864
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void checkTileEdges(WorldTile pChunkEdgeTile, bool pEdge, int pX, int pY)
	{
		if (pEdge)
		{
			return;
		}
		WorldTile tile = MapBox.instance.GetTile(pChunkEdgeTile.x + pX, pChunkEdgeTile.y + pY);
		if (tile != null)
		{
			this._tilesToCheck.Add(tile);
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0005D6A0 File Offset: 0x0005B8A0
	private void findRegionToLink(List<WorldTile> pChunkEdgeTiles, LinkDirection pDirection, LinkDirection pDirID, bool pEdgeFrst, bool pEdgeLast)
	{
		for (int i = 0; i < pChunkEdgeTiles.Count; i++)
		{
			WorldTile worldTile = pChunkEdgeTiles[i];
			this._tilesToCheck.Clear();
			switch (pDirection)
			{
			case LinkDirection.Up:
				this._tilesToCheck.Add(worldTile.tile_down);
				this.checkTileEdges(worldTile, pEdgeFrst, -1, -1);
				this.checkTileEdges(worldTile, pEdgeLast, 1, -1);
				break;
			case LinkDirection.Down:
				this._tilesToCheck.Add(worldTile);
				this.checkTileEdges(worldTile, pEdgeFrst, -1, 1);
				this.checkTileEdges(worldTile, pEdgeLast, 1, 1);
				break;
			case LinkDirection.Left:
				this._tilesToCheck.Add(worldTile);
				this.checkTileEdges(worldTile, pEdgeFrst, -1, -1);
				this.checkTileEdges(worldTile, pEdgeLast, -1, 1);
				break;
			case LinkDirection.Right:
				this._tilesToCheck.Add(worldTile.tile_left);
				this.checkTileEdges(worldTile, pEdgeFrst, 1, -1);
				this.checkTileEdges(worldTile, pEdgeLast, 1, 1);
				break;
			}
			if (this._tilesToCheck.Count != 0)
			{
				for (int j = 0; j < this._tilesToCheck.Count; j++)
				{
					WorldTile worldTile2 = this._tilesToCheck[j];
					if (worldTile2.Type.layerType == worldTile.Type.layerType)
					{
						int hash = worldTile2.region.newConnection(this._temp_tiles, pDirection, pDirID);
						this._newHashes.Add(new TempLinkStruct
						{
							region = worldTile2.region,
							hash = hash
						});
					}
				}
			}
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0005D814 File Offset: 0x0005BA14
	public void checkLinksResults()
	{
		for (int i = 0; i < this._newHashes.Count; i++)
		{
			TempLinkStruct tempLinkStruct = this._newHashes[i];
			RegionLinkHashes.addHash(tempLinkStruct.hash, tempLinkStruct.region);
		}
		MapChunkManager.m_newLinks += this._newHashes.Count;
		this._newHashes.Clear();
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0005D878 File Offset: 0x0005BA78
	private void calculateLink(List<WorldTile> pChunkEdgeTiles, LinkDirection pDirection, LinkDirection pDirID)
	{
		this._temp_tiles.Clear();
		for (int i = 0; i < pChunkEdgeTiles.Count; i++)
		{
			WorldTile worldTile = pChunkEdgeTiles[i];
			WorldTile worldTile2;
			if (i + 1 < pChunkEdgeTiles.Count)
			{
				worldTile2 = pChunkEdgeTiles[i + 1];
			}
			else
			{
				worldTile2 = null;
			}
			if (worldTile2 == null || worldTile.Type.layerType != worldTile2.Type.layerType)
			{
				this._temp_tiles.Add(worldTile);
				bool pEdgeFrst;
				bool pEdgeLast;
				if (pChunkEdgeTiles.Count > 1)
				{
					pEdgeFrst = (this._temp_tiles[0] == pChunkEdgeTiles[0]);
					pEdgeLast = (this._temp_tiles[this._temp_tiles.Count - 1] == pChunkEdgeTiles[pChunkEdgeTiles.Count - 1]);
				}
				else
				{
					pEdgeFrst = false;
					pEdgeLast = false;
				}
				this.findRegionToLink(this._temp_tiles, pDirection, pDirID, pEdgeFrst, pEdgeLast);
				this._temp_tiles.Clear();
			}
			else
			{
				this._temp_tiles.Add(worldTile);
			}
		}
	}

	// Token: 0x04000B0D RID: 2829
	public static bool debug;

	// Token: 0x04000B0E RID: 2830
	private List<WorldTile> _nextWave = new List<WorldTile>(64);

	// Token: 0x04000B0F RID: 2831
	public List<Kingdom> k_list_objects = new List<Kingdom>();

	// Token: 0x04000B10 RID: 2832
	public Dictionary<Kingdom, List<BaseSimObject>> k_dict_objects = new Dictionary<Kingdom, List<BaseSimObject>>();

	// Token: 0x04000B11 RID: 2833
	public List<MapChunk> neighbours;

	// Token: 0x04000B12 RID: 2834
	public List<MapChunk> neighboursAll;

	// Token: 0x04000B13 RID: 2835
	public List<WorldTile> tiles = new List<WorldTile>(64);

	// Token: 0x04000B14 RID: 2836
	public List<MapRegion> regions = new List<MapRegion>(1);

	// Token: 0x04000B15 RID: 2837
	public List<WorldTile> edges_left = new List<WorldTile>(8);

	// Token: 0x04000B16 RID: 2838
	public List<WorldTile> edges_up = new List<WorldTile>(8);

	// Token: 0x04000B17 RID: 2839
	public List<WorldTile> edges_down = new List<WorldTile>(8);

	// Token: 0x04000B18 RID: 2840
	public List<WorldTile> edges_right = new List<WorldTile>(8);

	// Token: 0x04000B19 RID: 2841
	public List<WorldTile> edge_special_bottom = new List<WorldTile>();

	// Token: 0x04000B1A RID: 2842
	public List<WorldTile> edge_special = new List<WorldTile>();

	// Token: 0x04000B1B RID: 2843
	public bool world_edge;

	// Token: 0x04000B1C RID: 2844
	public TileZone zone;

	// Token: 0x04000B1D RID: 2845
	public int x;

	// Token: 0x04000B1E RID: 2846
	public int y;

	// Token: 0x04000B1F RID: 2847
	public int id;

	// Token: 0x04000B20 RID: 2848
	public Color color;

	// Token: 0x04000B21 RID: 2849
	public bool dirty_regions;

	// Token: 0x04000B22 RID: 2850
	public bool dirty_links;

	// Token: 0x04000B23 RID: 2851
	internal MapChunk chunk_up;

	// Token: 0x04000B24 RID: 2852
	internal MapChunk chunk_down;

	// Token: 0x04000B25 RID: 2853
	internal MapChunk chunk_left;

	// Token: 0x04000B26 RID: 2854
	internal MapChunk chunk_right;

	// Token: 0x04000B27 RID: 2855
	private static List<BaseSimObject> _objectList;

	// Token: 0x04000B28 RID: 2856
	private List<WorldTile> _temp_tiles = new List<WorldTile>();

	// Token: 0x04000B29 RID: 2857
	private List<WorldTile> _tilesToCheck = new List<WorldTile>();

	// Token: 0x04000B2A RID: 2858
	private List<TempLinkStruct> _newHashes = new List<TempLinkStruct>();
}
