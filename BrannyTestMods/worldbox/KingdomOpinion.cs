using System;
using System.Collections.Generic;

// Token: 0x0200016F RID: 367
public class KingdomOpinion
{
	// Token: 0x06000844 RID: 2116 RVA: 0x0005A0AC File Offset: 0x000582AC
	public KingdomOpinion(Kingdom k1, Kingdom k2)
	{
		this.main = k1;
		this.target = k2;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0005A0C4 File Offset: 0x000582C4
	internal void calculate(Kingdom pMain, Kingdom pTarget, DiplomacyRelation pRelation)
	{
		this.main = pMain;
		this.target = pTarget;
		bool flag = pMain.civs_allies.ContainsKey(pTarget);
		this._opinion_king = 0;
		this._opinion_race = 0;
		this._opinion_peace_time = 0;
		this._opinion_power = 0;
		this._opinion_total = 0;
		this._opinion_same_wars = 0;
		this._opinion_is_supreme = 0;
		this._opinion_zones = 0;
		this._in_war = 0;
		this._opinion_traits = 0;
		this._opinion_close_borders = 0;
		this._opinion_far_lands = 0;
		this._opinion_culture = 0;
		if (!flag)
		{
			this._in_war = -100;
			this._opinion_total = this._in_war;
			return;
		}
		if (DiplomacyManager.kingdom_supreme == pTarget && MapBox.instance.kingdoms.list_civs.Count >= 3)
		{
			this._opinion_is_supreme = -100;
		}
		int num = pMain.cities.Count * 5 + pMain.getPopulationTotal();
		int num2 = pTarget.cities.Count * 5 + pTarget.getPopulationTotal() - num;
		if (num2 > 0)
		{
			this._opinion_power = num2 / 10;
		}
		else
		{
			this._opinion_power = 0;
		}
		if (pTarget.king != null)
		{
			this._opinion_king += pTarget.king.curStats.diplomacy;
		}
		if (pTarget.cultureID != null && pMain.king != null && pTarget.king != null)
		{
			int num3 = AssetManager.traits.checkTraitsMod(pTarget.king, pMain.king);
			this._opinion_traits += num3;
		}
		if (pTarget.getCulture() != null)
		{
			if (pTarget.getCulture() == pMain.getCulture())
			{
				this._opinion_culture = 10;
			}
			else
			{
				this._opinion_culture = -10;
			}
		}
		this._opinion_kings_mood = 0;
		if (pMain.king != null)
		{
			this._opinion_kings_mood = pMain.king.curStats.opinion;
		}
		int num4 = 0;
		int num5 = 0;
		foreach (City city in pMain.cities)
		{
			num4 += city.zones.Count;
		}
		foreach (City city2 in pTarget.cities)
		{
			num5 += city2.zones.Count;
		}
		int num6 = num4 - num5;
		this._opinion_zones = num6 / 5;
		if (this._opinion_zones > 0)
		{
			this._opinion_zones = 0;
		}
		if (this._opinion_zones < -20)
		{
			this._opinion_zones = -20;
		}
		foreach (Kingdom kingdom in pMain.civs_enemies.Keys)
		{
			using (Dictionary<Kingdom, bool>.KeyCollection.Enumerator enumerator3 = pTarget.civs_enemies.Keys.GetEnumerator())
			{
				if (enumerator3.MoveNext())
				{
					Kingdom kingdom2 = enumerator3.Current;
					this._opinion_same_wars = 50;
				}
			}
		}
		if (this.areKingdomsClose(pMain, pTarget))
		{
			this._opinion_close_borders = -50;
		}
		else
		{
			this._opinion_far_lands = 25;
		}
		if (pMain.race == pTarget.race)
		{
			this._opinion_race = 5;
		}
		else if (pMain.race.hateRaces.Contains(pTarget.race.id))
		{
			this._opinion_race -= 20;
		}
		if (flag)
		{
			this._opinion_peace_time = MapBox.instance.mapStats.year - pRelation.peaceSince;
			if (this._opinion_peace_time > 20)
			{
				this._opinion_peace_time = 20;
			}
			this._opinion_peace_time = 0;
		}
		this._opinion_total = this._opinion_close_borders + this._opinion_far_lands + this._opinion_king + this._opinion_peace_time + this._opinion_power + this._opinion_race + this._opinion_same_wars + this._opinion_zones + this._opinion_is_supreme + this._opinion_kings_mood + this._opinion_culture + this._opinion_traits;
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0005A4E4 File Offset: 0x000586E4
	private bool areKingdomsClose(Kingdom pMain, Kingdom pTarget)
	{
		foreach (City pA in pMain.cities)
		{
			foreach (City pB in pTarget.cities)
			{
				if (City.nearbyBorders(pA, pB))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04000AAF RID: 2735
	public Kingdom main;

	// Token: 0x04000AB0 RID: 2736
	public Kingdom target;

	// Token: 0x04000AB1 RID: 2737
	internal int _opinion_king;

	// Token: 0x04000AB2 RID: 2738
	internal int _opinion_race;

	// Token: 0x04000AB3 RID: 2739
	internal int _opinion_close_borders;

	// Token: 0x04000AB4 RID: 2740
	internal int _opinion_far_lands;

	// Token: 0x04000AB5 RID: 2741
	internal int _opinion_kings_mood;

	// Token: 0x04000AB6 RID: 2742
	internal int _in_war;

	// Token: 0x04000AB7 RID: 2743
	internal int _opinion_peace_time;

	// Token: 0x04000AB8 RID: 2744
	internal int _opinion_power;

	// Token: 0x04000AB9 RID: 2745
	internal int _opinion_traits;

	// Token: 0x04000ABA RID: 2746
	internal int _opinion_zones;

	// Token: 0x04000ABB RID: 2747
	internal int _opinion_same_wars;

	// Token: 0x04000ABC RID: 2748
	internal int _opinion_is_supreme;

	// Token: 0x04000ABD RID: 2749
	internal int _opinion_culture;

	// Token: 0x04000ABE RID: 2750
	internal int _opinion_total;
}
