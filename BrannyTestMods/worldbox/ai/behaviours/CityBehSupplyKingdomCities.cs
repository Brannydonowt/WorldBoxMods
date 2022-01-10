using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020003A8 RID: 936
	public class CityBehSupplyKingdomCities : BehaviourActionCity
	{
		// Token: 0x0600140F RID: 5135 RVA: 0x000A8FE4 File Offset: 0x000A71E4
		public override BehResult execute(City pCity)
		{
			Culture culture = pCity.getCulture();
			if (culture == null)
			{
				return BehResult.Stop;
			}
			if (pCity.kingdom.cities.Count == 1)
			{
				return BehResult.Stop;
			}
			if (!culture.haveTech("trading"))
			{
				return BehResult.Stop;
			}
			if (pCity.data.timer_supply > 0f)
			{
				return BehResult.Stop;
			}
			this._resources.Clear();
			foreach (string text in pCity.data.storage.resources.Keys)
			{
				CityStorageSlot cityStorageSlot = pCity.data.storage.resources[text];
				if (cityStorageSlot.amount > cityStorageSlot.asset.supplyBoundGive)
				{
					this._resources.Add(cityStorageSlot);
				}
			}
			if (this._resources.Count == 0)
			{
				return BehResult.Stop;
			}
			this._resources.Shuffle<CityStorageSlot>();
			pCity.kingdom.cities.ShuffleOne<City>();
			foreach (City city in pCity.kingdom.cities)
			{
				if (!(city == pCity))
				{
					foreach (CityStorageSlot cityStorageSlot2 in this._resources)
					{
						if (city.data.storage.get(cityStorageSlot2.id) < cityStorageSlot2.asset.supplyBoundTake)
						{
							this.shareResource(pCity, city, cityStorageSlot2);
							this.updateSupplyTimer(pCity);
							return BehResult.Continue;
						}
					}
				}
			}
			return BehResult.Continue;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x000A91C0 File Offset: 0x000A73C0
		private void updateSupplyTimer(City pCity)
		{
			float num = 30f;
			if (pCity.leader != null)
			{
				num *= pCity.leader.curStats.mod_supply_timer;
			}
			Culture culture = pCity.getCulture();
			if (culture != null)
			{
				num -= num * culture.stats.mod_trading_bonus.value;
			}
			if (num < 10f)
			{
				num = 10f;
			}
			pCity.data.timer_supply = num;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000A9230 File Offset: 0x000A7430
		private void shareResource(City pCity, City pTargetCity, CityStorageSlot pSlot)
		{
			pCity.data.storage.change(pSlot.id, -pSlot.asset.supplyGive);
			pTargetCity.data.storage.change(pSlot.id, pSlot.asset.supplyGive);
		}

		// Token: 0x0400156E RID: 5486
		private List<CityStorageSlot> _resources = new List<CityStorageSlot>();
	}
}
