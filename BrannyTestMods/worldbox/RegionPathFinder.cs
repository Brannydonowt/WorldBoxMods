using System;
using System.Collections.Generic;

// Token: 0x0200019A RID: 410
public class RegionPathFinder
{
	// Token: 0x06000966 RID: 2406 RVA: 0x00063A6C File Offset: 0x00061C6C
	public PathFinderResult getGlobalPath(WorldTile pFrom, WorldTile pTarget, bool pBoat = false)
	{
		if (this.world == null)
		{
			this.world = MapBox.instance;
		}
		this.last_globalPath = null;
		if (pFrom == pTarget)
		{
			return PathFinderResult.SamePlace;
		}
		if (pFrom.region == pTarget.region && pFrom.region.tiles.Count == 64)
		{
			return PathFinderResult.SamePlace;
		}
		if (pFrom.region == pTarget.region)
		{
			return PathFinderResult.PathFound;
		}
		if (pFrom.region.island != pTarget.region.island)
		{
			return PathFinderResult.DifferentIslands;
		}
		this.tileStart = pFrom;
		this.tileTarget = pTarget;
		string text = pFrom.region.id.ToString() + "_" + pTarget.region.id.ToString();
		if (DebugConfig.isOn(DebugOption.UseCacheForRegionPath))
		{
			this.cachedPaths.TryGetValue(text, ref this.last_globalPath);
			if (this.last_globalPath != null)
			{
				this.last_globalPath = this.cachedPaths[text];
				return PathFinderResult.PathFound;
			}
		}
		this.last_globalPath = new List<MapRegion>();
		this.startWave(this.tileTarget.region);
		if (DebugConfig.isOn(DebugOption.UseCacheForRegionPath))
		{
			this.addToCache(text, this.last_globalPath);
		}
		return PathFinderResult.PathFound;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00063B91 File Offset: 0x00061D91
	public void addToCache(string pID, List<MapRegion> pPath)
	{
		if (this.cachedPaths.Count > 1000)
		{
			this.cachedPaths.Clear();
		}
		this.cachedPaths.Add(pID, pPath);
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00063BBD File Offset: 0x00061DBD
	public void clearCache()
	{
		this.cachedPaths.Clear();
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00063BCA File Offset: 0x00061DCA
	private void startWave(MapRegion pRegion)
	{
		this.simplePath = true;
		this.clearRegions();
		this.currentWave = 0;
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_next_wave.Clear();
		this.addToNext(pRegion);
		this.newWave();
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00063C04 File Offset: 0x00061E04
	private void addToNext(MapRegion pRegion)
	{
		if (pRegion.tiles.Count != 64)
		{
			this.simplePath = false;
		}
		pRegion.isCheckedPath = true;
		this._temp_regions_next_wave.Add(pRegion);
		this._temp_regions_checked.Add(pRegion);
		pRegion.path_wave_id = this.currentWave;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00063C54 File Offset: 0x00061E54
	private void newWave()
	{
		this.currentWave++;
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_cur_wave.AddRange(this._temp_regions_next_wave);
		this._temp_regions_next_wave.Clear();
		for (int i = 0; i < this._temp_regions_cur_wave.Count; i++)
		{
			MapRegion mapRegion = this._temp_regions_cur_wave[i];
			for (int j = 0; j < mapRegion.neighbours.Count; j++)
			{
				MapRegion mapRegion2 = mapRegion.neighbours[j];
				if (!mapRegion2.isCheckedPath)
				{
					this.addToNext(mapRegion2);
					if (mapRegion2 == this.tileStart.region)
					{
						this.finalPath(this.tileStart.region);
						return;
					}
				}
			}
		}
		if (this._temp_regions_next_wave.Count > 0)
		{
			this.newWave();
		}
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x00063D20 File Offset: 0x00061F20
	private void finalPath(MapRegion pRegion)
	{
		this.last_globalPath.Add(pRegion);
		pRegion.regionPath = true;
		if (pRegion == this.tileTarget.region)
		{
			return;
		}
		pRegion.neighbours.ShuffleOne<MapRegion>();
		for (int i = 0; i < pRegion.neighbours.Count; i++)
		{
			MapRegion mapRegion = pRegion.neighbours[i];
			if (mapRegion.path_wave_id != -1 && mapRegion.path_wave_id < pRegion.path_wave_id)
			{
				this.finalPath(mapRegion);
				return;
			}
		}
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00063D9C File Offset: 0x00061F9C
	private void clearRegions()
	{
		for (int i = 0; i < this._temp_regions_checked.Count; i++)
		{
			MapRegion mapRegion = this._temp_regions_checked[i];
			mapRegion.isCheckedPath = false;
			mapRegion.path_wave_id = -1;
			mapRegion.regionPath = false;
		}
		this._temp_regions_checked.Clear();
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00063DEC File Offset: 0x00061FEC
	public string debug()
	{
		return this.cachedPaths.Count.ToString() ?? "";
	}

	// Token: 0x04000C03 RID: 3075
	private Dictionary<string, List<MapRegion>> cachedPaths = new Dictionary<string, List<MapRegion>>();

	// Token: 0x04000C04 RID: 3076
	private List<MapRegion> _temp_regions_cur_wave = new List<MapRegion>();

	// Token: 0x04000C05 RID: 3077
	private List<MapRegion> _temp_regions_next_wave = new List<MapRegion>();

	// Token: 0x04000C06 RID: 3078
	internal List<MapRegion> _temp_regions_checked = new List<MapRegion>();

	// Token: 0x04000C07 RID: 3079
	internal List<MapRegion> last_globalPath;

	// Token: 0x04000C08 RID: 3080
	internal MapBox world;

	// Token: 0x04000C09 RID: 3081
	private int currentWave;

	// Token: 0x04000C0A RID: 3082
	public WorldTile tileStart;

	// Token: 0x04000C0B RID: 3083
	public WorldTile tileTarget;

	// Token: 0x04000C0C RID: 3084
	public bool simplePath;
}
