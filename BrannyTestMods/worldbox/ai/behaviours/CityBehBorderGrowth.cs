using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000398 RID: 920
	public class CityBehBorderGrowth : BehaviourActionCity
	{
		// Token: 0x060013D4 RID: 5076 RVA: 0x000A69CE File Offset: 0x000A4BCE
		public override BehResult execute(City pCity)
		{
			if (!DebugConfig.isOn(DebugOption.SystemZoneGrowth))
			{
				return BehResult.Stop;
			}
			if (pCity.getPopulationTotal() == 0)
			{
				return BehResult.Stop;
			}
			if (pCity.zones.Count >= pCity.status.maximumZones)
			{
				return BehResult.Stop;
			}
			this.newGrowth(pCity);
			return BehResult.Continue;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000A6A08 File Offset: 0x000A4C08
		private void newGrowth(City pCity)
		{
			this._cityTile = pCity.getTile();
			if (this._cityTile == null)
			{
				return;
			}
			if (this._cityTile.zone.city != pCity)
			{
				pCity.addZone(this._cityTile.zone);
				return;
			}
			this.wave.Clear();
			this.nextWave.Clear();
			this.checked_regions.Clear();
			this._dist = 0f;
			this._bestDist = 0f;
			this._bestZone = null;
			this.wave.Add(this._cityTile.region);
			while (this.wave.Count > 0)
			{
				this.startWave();
				if (this.nextWave.Count > 0)
				{
					this.wave.AddRange(this.nextWave);
					this.nextWave.Clear();
				}
				if (this._bestZone != null)
				{
					break;
				}
			}
			this.checked_regions.Clear();
			if (this._bestZone == null)
			{
				return;
			}
			pCity.addZone(this._bestZone);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000A6B10 File Offset: 0x000A4D10
		private void startWave()
		{
			while (this.wave.Count > 0)
			{
				MapRegion mapRegion = this.wave[this.wave.Count - 1];
				TileZone zone = mapRegion.zone;
				this.wave.RemoveAt(this.wave.Count - 1);
				this.checked_regions.Add(mapRegion);
				foreach (MapRegion mapRegion2 in mapRegion.neighbours)
				{
					if (!this.checked_regions.Contains(mapRegion2))
					{
						this.checked_regions.Add(mapRegion2);
						this.checkForBestRegion(mapRegion2, this.nextWave, true);
					}
				}
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000A6BE4 File Offset: 0x000A4DE4
		private void checkForBestRegion(MapRegion pReg, List<MapRegion> pRegions, bool pCheckDistance = false)
		{
			if (pReg.type != TileLayerType.Ground)
			{
				return;
			}
			if (pReg.zone.city == this._cityTile.zone.city)
			{
				pRegions.Add(pReg);
			}
			if (pCheckDistance && pReg.zone.city == null)
			{
				this._dist = Toolbox.DistVec3(this._cityTile.posV3, pReg.zone.centerTile.posV3);
				if (this._bestZone == null || this._dist < this._bestDist)
				{
					this._bestDist = this._dist;
					this._bestZone = pReg.zone;
				}
			}
		}

		// Token: 0x0400155B RID: 5467
		private List<MapRegion> nextWave = new List<MapRegion>();

		// Token: 0x0400155C RID: 5468
		private List<MapRegion> wave = new List<MapRegion>();

		// Token: 0x0400155D RID: 5469
		private HashSetMapRegion checked_regions = new HashSetMapRegion();

		// Token: 0x0400155E RID: 5470
		private float _bestDist;

		// Token: 0x0400155F RID: 5471
		private TileZone _bestZone;

		// Token: 0x04001560 RID: 5472
		private float _dist;

		// Token: 0x04001561 RID: 5473
		private WorldTile _cityTile;
	}
}
