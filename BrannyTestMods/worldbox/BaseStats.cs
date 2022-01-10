using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
[Serializable]
public class BaseStats
{
	// Token: 0x060000E0 RID: 224 RVA: 0x00012458 File Offset: 0x00010658
	internal void addStats(BaseStats pStats)
	{
		this.personality_rationality += pStats.personality_rationality;
		this.personality_aggression += pStats.personality_aggression;
		this.personality_diplomatic += pStats.personality_diplomatic;
		this.personality_administration += pStats.personality_administration;
		this.opinion += pStats.opinion;
		this.loyalty_traits += pStats.loyalty_traits;
		this.loyalty_mood += pStats.loyalty_mood;
		this.scale += pStats.scale;
		this.damage += pStats.damage;
		this.attackSpeed += pStats.attackSpeed;
		this.speed += pStats.speed;
		this.health += pStats.health;
		this.armor += pStats.armor;
		this.diplomacy += pStats.diplomacy;
		this.warfare += pStats.warfare;
		this.stewardship += pStats.stewardship;
		this.intelligence += pStats.intelligence;
		this.army += pStats.army;
		this.cities += pStats.cities;
		this.zones += pStats.zones;
		this.bonus_towers += pStats.bonus_towers;
		this.dodge += pStats.dodge;
		this.accuracy += pStats.accuracy;
		this.targets += pStats.targets;
		this.projectiles += pStats.projectiles;
		this.crit += pStats.crit;
		this.damageCritMod += pStats.damageCritMod;
		this.range += pStats.range;
		this.size += pStats.size;
		this.areaOfEffect += pStats.areaOfEffect;
		this.knockback += pStats.knockback;
		this.knockbackReduction += pStats.knockbackReduction;
		this.mod_health += pStats.mod_health;
		this.mod_damage += pStats.mod_damage;
		this.mod_armor += pStats.mod_armor;
		this.mod_crit += pStats.mod_crit;
		this.mod_diplomacy += pStats.mod_diplomacy;
		this.mod_speed += pStats.mod_speed;
		this.mod_supply_timer += pStats.mod_supply_timer;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0001274C File Offset: 0x0001094C
	internal void clear()
	{
		this.s_crit_chance = 0f;
		this.personality_rationality = 0f;
		this.personality_aggression = 0f;
		this.personality_diplomatic = 0f;
		this.personality_administration = 0f;
		this.opinion = 0;
		this.loyalty_mood = 0;
		this.loyalty_traits = 0;
		this.mod_supply_timer = 0f;
		this.scale = 0f;
		this.damage = 0;
		this.attackSpeed = 0f;
		this.projectiles = 0;
		this.speed = 0f;
		this.health = 0;
		this.armor = 0;
		this.diplomacy = 0;
		this.warfare = 0;
		this.stewardship = 0;
		this.intelligence = 0;
		this.army = 0;
		this.cities = 0;
		this.zones = 0;
		this.bonus_towers = 0;
		this.dodge = 0f;
		this.accuracy = 0f;
		this.targets = 0;
		this.crit = 0f;
		this.damageCritMod = 0f;
		this.range = 0f;
		this.size = 0f;
		this.areaOfEffect = 0f;
		this.knockback = 0f;
		this.knockbackReduction = 0f;
		this.mod_health = 0f;
		this.mod_damage = 0f;
		this.mod_armor = 0f;
		this.mod_crit = 0f;
		this.mod_diplomacy = 0f;
		this.mod_speed = 0f;
		this.mod_attackSpeed = 0f;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x000128DC File Offset: 0x00010ADC
	public void normalize()
	{
		this.intelligence = Mathf.Clamp(this.intelligence, 0, 999);
		this.stewardship = Mathf.Clamp(this.stewardship, 0, 999);
		this.warfare = Mathf.Clamp(this.warfare, 0, 999);
		this.diplomacy = Mathf.Clamp(this.diplomacy, 0, 999);
		this.personality_aggression = Mathf.Clamp(this.personality_aggression, 0f, 1f);
		this.personality_diplomatic = Mathf.Clamp(this.personality_diplomatic, 0f, 1f);
		this.personality_administration = Mathf.Clamp(this.personality_administration, 0f, 1f);
		this.personality_rationality = Mathf.Clamp(this.personality_rationality, 0f, 1f);
	}

	// Token: 0x040000A3 RID: 163
	public float personality_aggression;

	// Token: 0x040000A4 RID: 164
	public float personality_administration;

	// Token: 0x040000A5 RID: 165
	public float personality_diplomatic;

	// Token: 0x040000A6 RID: 166
	public float personality_rationality;

	// Token: 0x040000A7 RID: 167
	public int diplomacy;

	// Token: 0x040000A8 RID: 168
	public int warfare;

	// Token: 0x040000A9 RID: 169
	public int stewardship;

	// Token: 0x040000AA RID: 170
	public int intelligence;

	// Token: 0x040000AB RID: 171
	public int army;

	// Token: 0x040000AC RID: 172
	public int cities;

	// Token: 0x040000AD RID: 173
	public int zones;

	// Token: 0x040000AE RID: 174
	public int bonus_towers;

	// Token: 0x040000AF RID: 175
	public float s_crit_chance;

	// Token: 0x040000B0 RID: 176
	public int damage;

	// Token: 0x040000B1 RID: 177
	public float speed;

	// Token: 0x040000B2 RID: 178
	public int health;

	// Token: 0x040000B3 RID: 179
	public int armor;

	// Token: 0x040000B4 RID: 180
	public float dodge;

	// Token: 0x040000B5 RID: 181
	public float accuracy;

	// Token: 0x040000B6 RID: 182
	public int targets;

	// Token: 0x040000B7 RID: 183
	public int projectiles;

	// Token: 0x040000B8 RID: 184
	public float crit;

	// Token: 0x040000B9 RID: 185
	public float damageCritMod;

	// Token: 0x040000BA RID: 186
	public float range;

	// Token: 0x040000BB RID: 187
	public float size;

	// Token: 0x040000BC RID: 188
	public float areaOfEffect;

	// Token: 0x040000BD RID: 189
	public float attackSpeed;

	// Token: 0x040000BE RID: 190
	public float knockback;

	// Token: 0x040000BF RID: 191
	public int loyalty_traits;

	// Token: 0x040000C0 RID: 192
	public int loyalty_mood;

	// Token: 0x040000C1 RID: 193
	public int opinion;

	// Token: 0x040000C2 RID: 194
	public float knockbackReduction;

	// Token: 0x040000C3 RID: 195
	public float mod_health;

	// Token: 0x040000C4 RID: 196
	public float mod_damage;

	// Token: 0x040000C5 RID: 197
	public float mod_armor;

	// Token: 0x040000C6 RID: 198
	public float mod_crit;

	// Token: 0x040000C7 RID: 199
	public float mod_diplomacy;

	// Token: 0x040000C8 RID: 200
	public float mod_speed;

	// Token: 0x040000C9 RID: 201
	public float mod_attackSpeed;

	// Token: 0x040000CA RID: 202
	public float scale;

	// Token: 0x040000CB RID: 203
	public float mod_supply_timer;
}
