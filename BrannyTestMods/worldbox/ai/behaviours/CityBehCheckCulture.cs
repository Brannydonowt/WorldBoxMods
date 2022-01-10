using System;
using System.Collections.Generic;
using System.Linq;

namespace ai.behaviours
{
	// Token: 0x0200039F RID: 927
	public class CityBehCheckCulture : BehaviourActionCity
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x000A8164 File Offset: 0x000A6364
		public override BehResult execute(City pCity)
		{
			this.recalcMainCulture(pCity);
			this.tryToCreateNewCultureFromBonfire(pCity);
			this.spreadMainCultureFromBonfire(pCity);
			return BehResult.Continue;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x000A817C File Offset: 0x000A637C
		private void recalcMainCulture(City pCity)
		{
			this._counters_dict.Clear();
			this._counters_list.Clear();
			CultureMainCounter cultureMainCounter = null;
			foreach (TileZone tileZone in pCity.zones)
			{
				if (tileZone.culture != null)
				{
					this._counters_dict.TryGetValue(tileZone.culture, ref cultureMainCounter);
					if (cultureMainCounter == null)
					{
						cultureMainCounter = new CultureMainCounter(tileZone.culture);
						this._counters_dict.Add(tileZone.culture, cultureMainCounter);
						this._counters_list.Add(cultureMainCounter);
					}
					this._counters_dict[tileZone.culture].amount++;
				}
			}
			foreach (Actor actor in pCity.units)
			{
				Culture culture = actor.getCulture();
				if (culture != null)
				{
					this._counters_dict.TryGetValue(culture, ref cultureMainCounter);
					if (cultureMainCounter == null)
					{
						cultureMainCounter = new CultureMainCounter(culture);
						this._counters_dict.Add(culture, cultureMainCounter);
						this._counters_list.Add(cultureMainCounter);
					}
					this._counters_dict[culture].amount++;
				}
			}
			if (!this._counters_list.Any<CultureMainCounter>())
			{
				return;
			}
			this._counters_list.Sort(new Comparison<CultureMainCounter>(this.sortByPower));
			Culture culture2 = this._counters_list[0].culture;
			pCity.setCulture(culture2);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000A8318 File Offset: 0x000A6518
		public int sortByPower(CultureMainCounter o1, CultureMainCounter o2)
		{
			return o2.amount.CompareTo(o1.amount);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000A832C File Offset: 0x000A652C
		private void tryToCreateNewCultureFromBonfire(City pCity)
		{
			if (pCity.getCulture() != null)
			{
				return;
			}
			Building buildingType = pCity.getBuildingType("bonfire", true, false);
			if (buildingType != null && buildingType.currentTile.zone.culture == null)
			{
				this.setNewCultureForCity(pCity);
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000A8374 File Offset: 0x000A6574
		private void spreadMainCultureFromBonfire(City pCity)
		{
			Culture culture = pCity.getCulture();
			Building buildingType = pCity.getBuildingType("bonfire", true, false);
			if (buildingType != null && culture != null)
			{
				TileZone zone = buildingType.currentTile.zone;
				if (zone.culture == null)
				{
					culture.addZone(zone);
				}
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000A83C0 File Offset: 0x000A65C0
		private Culture setNewCultureForCity(City pCity)
		{
			if (!string.IsNullOrEmpty(pCity.data.culture))
			{
				return null;
			}
			Culture culture = BehaviourActionBase<City>.world.cultures.newCulture(pCity.race, pCity);
			pCity.setCulture(culture);
			return culture;
		}

		// Token: 0x04001565 RID: 5477
		private Dictionary<Culture, CultureMainCounter> _counters_dict = new Dictionary<Culture, CultureMainCounter>();

		// Token: 0x04001566 RID: 5478
		private List<CultureMainCounter> _counters_list = new List<CultureMainCounter>();
	}
}
