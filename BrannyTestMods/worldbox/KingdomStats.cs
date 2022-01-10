using System;
using System.Collections.Generic;

// Token: 0x02000140 RID: 320
[Serializable]
public class KingdomStats
{
	// Token: 0x06000787 RID: 1927 RVA: 0x000549DC File Offset: 0x00052BDC
	public KingdomStats()
	{
		this.bonus_watch_towers = this.newStat("bonus_watch_towers");
		this.bonus_res_wood_amount = this.newStat("bonus_res_wood_amount");
		this.bonus_res_chance_wood = this.newStat("bonus_res_chance_wood");
		this.food_from_wheat = this.newStat("food_from_wheat");
		this.bonus_res_ore_amount = this.newStat("bonus_res_ore_amount");
		this.bonus_res_chance_ores = this.newStat("bonus_res_chance_ores");
		this.item_production_tries_weapons = this.newStat("item_production_tries_weapons");
		this.item_production_tries_armor = this.newStat("item_production_tries_armor");
		this.mod_trading_bonus = this.newStat("mod_trading_bonus");
		this.bonus_damage = this.newStat("bonus_damage");
		this.bonus_armor = this.newStat("bonus_armor");
		this.bonus_max_cities = this.newStat("bonus_max_cities");
		this.bonus_max_army = this.newStat("bonus_max_army");
		this.knowledge_gain = this.newStat("knowledge_gain");
		this.housing = this.newStat("housing");
		this.culture_spread_speed = this.newStat("culture_spread_speed");
		this.culture_spread_convert_chance = this.newStat("culture_spread_convert_chance");
		this.bonus_born_level = this.newStat("bonus_born_level");
		this.bonus_age = this.newStat("bonus_age");
		this.bonus_max_unit_level = this.newStat("bonus_max_unit_level");
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00054B5C File Offset: 0x00052D5C
	private KingdomStatVal newStat(string pID)
	{
		KingdomStatVal kingdomStatVal = new KingdomStatVal(pID);
		this.list.Add(kingdomStatVal);
		this.dict.Add(pID, kingdomStatVal);
		return kingdomStatVal;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00054B8C File Offset: 0x00052D8C
	public void addStats(KingdomStats pStats)
	{
		for (int i = 0; i < pStats.list.Count; i++)
		{
			KingdomStatVal kingdomStatVal = pStats.list[i];
			if (kingdomStatVal.value != 0f)
			{
				this.dict[kingdomStatVal.id].value += kingdomStatVal.value;
			}
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00054BEC File Offset: 0x00052DEC
	public void clear()
	{
		foreach (KingdomStatVal kingdomStatVal in this.list)
		{
			kingdomStatVal.clear();
		}
	}

	// Token: 0x040009F8 RID: 2552
	public List<KingdomStatVal> list = new List<KingdomStatVal>();

	// Token: 0x040009F9 RID: 2553
	public Dictionary<string, KingdomStatVal> dict = new Dictionary<string, KingdomStatVal>();

	// Token: 0x040009FA RID: 2554
	public KingdomStatVal bonus_res_ore_amount;

	// Token: 0x040009FB RID: 2555
	public KingdomStatVal bonus_res_wood_amount;

	// Token: 0x040009FC RID: 2556
	public KingdomStatVal food_from_wheat;

	// Token: 0x040009FD RID: 2557
	public KingdomStatVal bonus_res_chance_wood;

	// Token: 0x040009FE RID: 2558
	public KingdomStatVal bonus_res_chance_ores;

	// Token: 0x040009FF RID: 2559
	public KingdomStatVal item_production_tries_weapons;

	// Token: 0x04000A00 RID: 2560
	public KingdomStatVal item_production_tries_armor;

	// Token: 0x04000A01 RID: 2561
	public KingdomStatVal mod_trading_bonus;

	// Token: 0x04000A02 RID: 2562
	public KingdomStatVal bonus_damage;

	// Token: 0x04000A03 RID: 2563
	public KingdomStatVal bonus_armor;

	// Token: 0x04000A04 RID: 2564
	public KingdomStatVal bonus_max_cities;

	// Token: 0x04000A05 RID: 2565
	public KingdomStatVal bonus_max_army;

	// Token: 0x04000A06 RID: 2566
	public KingdomStatVal knowledge_gain;

	// Token: 0x04000A07 RID: 2567
	public KingdomStatVal housing;

	// Token: 0x04000A08 RID: 2568
	public KingdomStatVal culture_spread_speed;

	// Token: 0x04000A09 RID: 2569
	public KingdomStatVal culture_spread_convert_chance;

	// Token: 0x04000A0A RID: 2570
	public KingdomStatVal bonus_born_level;

	// Token: 0x04000A0B RID: 2571
	public KingdomStatVal bonus_age;

	// Token: 0x04000A0C RID: 2572
	public KingdomStatVal bonus_max_unit_level;

	// Token: 0x04000A0D RID: 2573
	public KingdomStatVal bonus_watch_towers;
}
