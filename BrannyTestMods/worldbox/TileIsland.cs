using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018A RID: 394
public class TileIsland
{
	// Token: 0x06000912 RID: 2322 RVA: 0x00060958 File Offset: 0x0005EB58
	public TileIsland(int pID)
	{
		this.id = pID;
		this.debug_hash_code = this.GetHashCode();
		this.tiles_roads = new TileDictionary();
		this.created = Time.time;
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x000609D2 File Offset: 0x0005EBD2
	public void addRegion(MapRegion pRegion)
	{
		this.regions.Add(pRegion);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x000609E0 File Offset: 0x0005EBE0
	public void removeRegion(MapRegion pRegion)
	{
		this.regions.Remove(pRegion);
		pRegion.island = null;
		this.dirty = true;
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x000609FC File Offset: 0x0005EBFC
	public void clearRegionsFromIsland()
	{
		if (this.removed)
		{
			return;
		}
		this.removed = true;
		List<MapRegion> simpleList = this.regions.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			simpleList[i].island = null;
		}
		this.regions.Clear();
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00060A4E File Offset: 0x0005EC4E
	public int getTileCount()
	{
		return this._tileCount;
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00060A58 File Offset: 0x0005EC58
	internal void countTiles()
	{
		this._tileCount = 0;
		List<MapRegion> simpleList = this.regions.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			MapRegion mapRegion = simpleList[i];
			this._tileCount += mapRegion.tiles.Count;
		}
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00060AA9 File Offset: 0x0005ECA9
	internal WorldTile getRandomTile()
	{
		return this.regions.GetRandom().tiles.GetRandom<WorldTile>();
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00060AC0 File Offset: 0x0005ECC0
	public bool connectedWith(TileIsland pIsland)
	{
		if (this.cached_connectedIslandsCheck.ContainsKey(pIsland))
		{
			return this.cached_connectedIslandsCheck[pIsland];
		}
		TileIsland tileIsland;
		TileIsland tileIsland2;
		if (pIsland.getTileCount() < this.getTileCount())
		{
			tileIsland = pIsland;
			tileIsland2 = this;
		}
		else
		{
			tileIsland = this;
			tileIsland2 = pIsland;
		}
		for (int i = 0; i < tileIsland.regions.Count; i++)
		{
			MapRegion mapRegion = tileIsland.regions.getSimpleList()[i];
			if (!mapRegion.centerRegion)
			{
				List<WorldTile> tileCorners = mapRegion.getTileCorners();
				for (int j = 0; j < tileCorners.Count; j++)
				{
					if (tileCorners[j].region.island == tileIsland2)
					{
						tileIsland.cached_connectedIslandsCheck[tileIsland2] = true;
						tileIsland2.cached_connectedIslandsCheck[tileIsland] = true;
						return true;
					}
				}
			}
		}
		tileIsland.cached_connectedIslandsCheck[tileIsland2] = false;
		tileIsland2.cached_connectedIslandsCheck[tileIsland] = false;
		return false;
	}

	// Token: 0x04000B8A RID: 2954
	internal bool calc_temp_city_here;

	// Token: 0x04000B8B RID: 2955
	public RegionsContainer regions = new RegionsContainer();

	// Token: 0x04000B8C RID: 2956
	public List<MapRegion> regionsCorners = new List<MapRegion>();

	// Token: 0x04000B8D RID: 2957
	public float created;

	// Token: 0x04000B8E RID: 2958
	internal bool haveCity;

	// Token: 0x04000B8F RID: 2959
	public TileLayerType type = TileLayerType.Ocean;

	// Token: 0x04000B90 RID: 2960
	public bool dirty;

	// Token: 0x04000B91 RID: 2961
	public int id;

	// Token: 0x04000B92 RID: 2962
	public int debug_hash_code;

	// Token: 0x04000B93 RID: 2963
	public TileDictionary tiles_roads;

	// Token: 0x04000B94 RID: 2964
	internal bool removed;

	// Token: 0x04000B95 RID: 2965
	public List<Actor> actors = new List<Actor>();

	// Token: 0x04000B96 RID: 2966
	public List<Docks> docks = new List<Docks>();

	// Token: 0x04000B97 RID: 2967
	private int _tileCount;

	// Token: 0x04000B98 RID: 2968
	private Dictionary<TileIsland, bool> cached_connectedIslandsCheck = new Dictionary<TileIsland, bool>();
}
