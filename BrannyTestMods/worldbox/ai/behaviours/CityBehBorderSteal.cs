using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000399 RID: 921
	public class CityBehBorderSteal : BehaviourActionCity
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x000A6CB8 File Offset: 0x000A4EB8
		public override BehResult execute(City pCity)
		{
			if (!DebugConfig.isOn(DebugOption.SystemZoneGrowth))
			{
				return BehResult.Stop;
			}
			if (!BehaviourActionBase<City>.world.worldLaws.world_law_border_stealing.boolVal)
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
			if (pCity.buildings.Count == 0)
			{
				return BehResult.Stop;
			}
			int num = 0;
			while (num < 3 && !this.tryStealZone(pCity))
			{
				num++;
			}
			return BehResult.Continue;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000A6D34 File Offset: 0x000A4F34
		private bool tryStealZone(City pCity)
		{
			CityBehBorderSteal._zones.Clear();
			foreach (TileZone tileZone in pCity.buildings.GetRandom().currentTile.zone.neighbours)
			{
				if (!(tileZone.city == pCity) && !(tileZone.city == null) && tileZone.buildings.Count <= 0 && tileZone.city.kingdom.isEnemy(pCity.kingdom))
				{
					this.stealZone(tileZone, pCity);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000A6DF0 File Offset: 0x000A4FF0
		private void stealZone(TileZone pZone, City pCity)
		{
			if (pZone.city != null)
			{
				pZone.city.removeZone(pZone, false);
			}
			pCity.addZone(pZone);
		}

		// Token: 0x04001562 RID: 5474
		private static List<TileZone> _zones = new List<TileZone>();
	}
}
