using System;
using System.Collections.Generic;

// Token: 0x020000F5 RID: 245
public class CityCheckAbandonZones
{
	// Token: 0x06000569 RID: 1385 RVA: 0x00044715 File Offset: 0x00042915
	public static void checkAbandonZones(City pCity)
	{
		CityCheckAbandonZones._checkingCity = pCity;
		CityCheckAbandonZones.clearAll();
		CityCheckAbandonZones.prepareCityZones(pCity);
		CityCheckAbandonZones.resetCityFinderForIslands();
		CityCheckAbandonZones.startCheckingFromBuildings(pCity);
		CityCheckAbandonZones._split_areas.Sort(new Comparison<List<TileZone>>(CityCheckAbandonZones.sorter));
		CityCheckAbandonZones.abandonLeftoverZones(pCity);
		CityCheckAbandonZones.abandonSmallAreas(pCity);
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00044758 File Offset: 0x00042958
	private static void startCheckingFromBuildings(City pCity)
	{
		List<Building> list = new List<Building>();
		pCity.buildings.checkAddRemove();
		list.AddRange(pCity.buildings.getSimpleList());
		while (list.Count > 0)
		{
			Building building = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			CityCheckAbandonZones.prepareWaveFromTile(building.currentTile);
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x000447BC File Offset: 0x000429BC
	private static void prepareWaveFromTile(WorldTile pTile)
	{
		if (!CityCheckAbandonZones._zones_hashset_to_check.Contains(pTile.zone))
		{
			return;
		}
		pTile.region.island.calc_temp_city_here = true;
		CityCheckAbandonZones.wave.Clear();
		CityCheckAbandonZones.nextWave.Clear();
		CityCheckAbandonZones._cur_area = new List<TileZone>();
		CityCheckAbandonZones._split_areas.Add(CityCheckAbandonZones._cur_area);
		CityCheckAbandonZones.addRegionsToCheckFromZone(pTile.zone, CityCheckAbandonZones.wave);
		while (CityCheckAbandonZones.wave.Count > 0)
		{
			CityCheckAbandonZones.starRegiontWave();
			if (CityCheckAbandonZones.nextWave.Count > 0)
			{
				CityCheckAbandonZones.wave.AddRange(CityCheckAbandonZones.nextWave);
				CityCheckAbandonZones.nextWave.Clear();
			}
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00044864 File Offset: 0x00042A64
	private static void abandonLeftoverZones(City pCity)
	{
		if (CityCheckAbandonZones._zones_hashset_to_check.Count == 0)
		{
			return;
		}
		foreach (TileZone pZone in CityCheckAbandonZones._zones_hashset_to_check)
		{
			pCity.removeZone(pZone, true);
		}
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000448C4 File Offset: 0x00042AC4
	private static void abandonSmallAreas(City pCity)
	{
		if (CityCheckAbandonZones._split_areas.Count < 2)
		{
			return;
		}
		CityCheckAbandonZones._split_areas.RemoveAt(0);
		foreach (List<TileZone> list in CityCheckAbandonZones._split_areas)
		{
			foreach (TileZone pZone in list)
			{
				pCity.removeZone(pZone, true);
			}
		}
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00044964 File Offset: 0x00042B64
	private static void starRegiontWave()
	{
		while (CityCheckAbandonZones.wave.Count > 0)
		{
			TileZone zone = CityCheckAbandonZones.wave[CityCheckAbandonZones.wave.Count - 1].zone;
			CityCheckAbandonZones.wave.RemoveAt(CityCheckAbandonZones.wave.Count - 1);
			CityCheckAbandonZones._zones_hashset_to_check.Remove(zone);
			CityCheckAbandonZones._cur_area.Add(zone);
			zone.zoneChecked = true;
			foreach (TileZone tileZone in zone.neighbours)
			{
				if (!tileZone.zoneChecked && !(tileZone.city != CityCheckAbandonZones._checkingCity))
				{
					tileZone.zoneChecked = true;
					CityCheckAbandonZones.addRegionsToCheckFromZone(tileZone, CityCheckAbandonZones.nextWave);
				}
			}
		}
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00044A44 File Offset: 0x00042C44
	private static void addRegionsToCheckFromZone(TileZone pZone, List<MapRegion> pRegions)
	{
		foreach (MapRegion mapRegion in pZone.chunk.regions)
		{
			if (mapRegion.type == TileLayerType.Ground && mapRegion.island.calc_temp_city_here)
			{
				pRegions.Add(mapRegion);
			}
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00044AB4 File Offset: 0x00042CB4
	private static void resetCityFinderForIslands()
	{
		foreach (TileIsland tileIsland in MapBox.instance.islandsCalculator.islands_ground)
		{
			tileIsland.calc_temp_city_here = false;
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00044B10 File Offset: 0x00042D10
	private static void prepareCityZones(City pCity)
	{
		foreach (TileZone tileZone in pCity.zones)
		{
			tileZone.zoneChecked = false;
			CityCheckAbandonZones._zones_hashset_to_check.Add(tileZone);
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00044B70 File Offset: 0x00042D70
	private static void clearAll()
	{
		CityCheckAbandonZones._zones_hashset_to_check.Clear();
		CityCheckAbandonZones._split_areas.Clear();
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00044B88 File Offset: 0x00042D88
	public static int sorter(List<TileZone> o1, List<TileZone> o2)
	{
		return o2.Count.CompareTo(o1.Count);
	}

	// Token: 0x0400075A RID: 1882
	private static List<MapRegion> nextWave = new List<MapRegion>();

	// Token: 0x0400075B RID: 1883
	private static List<MapRegion> wave = new List<MapRegion>();

	// Token: 0x0400075C RID: 1884
	private static List<List<TileZone>> _split_areas = new List<List<TileZone>>();

	// Token: 0x0400075D RID: 1885
	private static HashSetTileZone _zones_hashset_to_check = new HashSetTileZone();

	// Token: 0x0400075E RID: 1886
	private static City _checkingCity;

	// Token: 0x0400075F RID: 1887
	private static List<TileZone> _cur_area;
}
