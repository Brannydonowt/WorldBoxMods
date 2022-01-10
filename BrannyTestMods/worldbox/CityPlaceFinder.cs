using System;
using System.Collections.Generic;

// Token: 0x02000133 RID: 307
public class CityPlaceFinder
{
	// Token: 0x06000710 RID: 1808 RVA: 0x00050AD0 File Offset: 0x0004ECD0
	public CityPlaceFinder(MapBox pBox)
	{
		this.world = pBox;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00050B21 File Offset: 0x0004ED21
	internal void update(float pElapsed)
	{
		if (this.timer >= 0f)
		{
			this.timer -= pElapsed;
			if (this.dirty)
			{
				this.calculate();
			}
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00050B4C File Offset: 0x0004ED4C
	internal void calculate()
	{
		if (!DebugConfig.isOn(DebugOption.SystemCityPlaceFinder))
		{
			return;
		}
		Toolbox.bench("city_place_finder");
		LogText.log("City place finder", "calculate", "st");
		this.dirty = false;
		this.zones.Clear();
		for (int i = 0; i < this.world.zoneCalculator.zones.Count; i++)
		{
			TileZone tileZone = this.world.zoneCalculator.zones[i];
			tileZone.goodForNewCity = false;
			tileZone.zoneChecked = false;
			if (!(tileZone.city != null) && tileZone.tilesWithGround >= 64 && tileZone.centerTile.region.island.getTileCount() >= 500 && tileZone.countCanBeFarms() >= 5)
			{
				tileZone.goodForNewCity = true;
			}
		}
		for (int j = 0; j < this.world.citiesList.Count; j++)
		{
			City pCity = this.world.citiesList[j];
			this.checkCity(pCity);
		}
		for (int k = 0; k < this.world.zoneCalculator.zones.Count; k++)
		{
			TileZone tileZone2 = this.world.zoneCalculator.zones[k];
			if (tileZone2.goodForNewCity)
			{
				this.zones.Add(tileZone2);
			}
		}
		this.zones.Shuffle<TileZone>();
		Toolbox.benchEnd("city_place_finder");
		LogText.log("City place finder", "calculate", "en");
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00050CD8 File Offset: 0x0004EED8
	private void checkCity(City pCity)
	{
		for (int i = 0; i < this.world.islandsCalculator.islands_ground.Count; i++)
		{
			this.world.islandsCalculator.islands_ground[i].calc_temp_city_here = false;
		}
		WorldTile tile = pCity.getTile();
		if (tile != null)
		{
			tile.region.island.calc_temp_city_here = true;
		}
		List<Building> simpleList = pCity.buildings.getSimpleList();
		for (int j = 0; j < simpleList.Count; j++)
		{
			simpleList[j].currentTile.region.island.calc_temp_city_here = true;
		}
		this._curWave = 0;
		this.wave.Clear();
		this.nextWave.Clear();
		for (int k = 0; k < pCity.zones.Count; k++)
		{
			TileZone pZone = pCity.zones[k];
			this.addRegionsToCheckFromZone(pZone, this.wave);
		}
		while (this.wave.Count > 0 && this._curWave < 7)
		{
			this._curWave++;
			this.startNextWave();
			if (this.nextWave.Count > 0)
			{
				this.wave.AddRange(this.nextWave);
				this.nextWave.Clear();
			}
		}
		foreach (TileZone tileZone in this.checkedZones)
		{
			tileZone.zoneChecked = false;
		}
		this.checkedZones.Clear();
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00050E70 File Offset: 0x0004F070
	private void addRegionsToCheckFromZone(TileZone pZone, List<MapRegion> pRegions)
	{
		MapChunk chunk = pZone.chunk;
		for (int i = 0; i < chunk.regions.Count; i++)
		{
			MapRegion mapRegion = chunk.regions[i];
			if (mapRegion.type == TileLayerType.Ground && mapRegion.island.calc_temp_city_here)
			{
				pRegions.Add(mapRegion);
			}
		}
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x00050EC4 File Offset: 0x0004F0C4
	private void startNextWave()
	{
		while (this.wave.Count > 0)
		{
			TileZone zone = this.wave[this.wave.Count - 1].zone;
			this.wave.RemoveAt(this.wave.Count - 1);
			zone.zoneChecked = true;
			zone.goodForNewCity = false;
			this.checkedZones.Add(zone);
			for (int i = 0; i < zone.neighbours.Count; i++)
			{
				TileZone tileZone = zone.neighbours[i];
				if (!tileZone.zoneChecked && !tileZone.city && tileZone.tilesWithGround >= 10)
				{
					tileZone.zoneChecked = true;
					this.checkedZones.Add(tileZone);
					this.addRegionsToCheckFromZone(tileZone, this.nextWave);
				}
			}
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00050FA0 File Offset: 0x0004F1A0
	internal void cleanGoodList()
	{
		for (int i = 0; i < this.zones.Count; i++)
		{
			this.zones[i].goodForNewCity = false;
		}
		this.zones.Clear();
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00050FE0 File Offset: 0x0004F1E0
	internal void setDirty()
	{
		this.dirty = true;
		if (this.timer <= 0f)
		{
			this.timer = 1f;
		}
	}

	// Token: 0x0400095E RID: 2398
	private MapBox world;

	// Token: 0x0400095F RID: 2399
	private bool dirty;

	// Token: 0x04000960 RID: 2400
	internal List<TileZone> zones = new List<TileZone>();

	// Token: 0x04000961 RID: 2401
	private List<MapRegion> nextWave = new List<MapRegion>();

	// Token: 0x04000962 RID: 2402
	private List<MapRegion> wave = new List<MapRegion>();

	// Token: 0x04000963 RID: 2403
	private HashSetTileZone checkedZones = new HashSetTileZone();

	// Token: 0x04000964 RID: 2404
	private float timer = 2f;

	// Token: 0x04000965 RID: 2405
	private int _curWave;

	// Token: 0x04000966 RID: 2406
	private int tilesForCity;
}
