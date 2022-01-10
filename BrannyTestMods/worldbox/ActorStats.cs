using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class ActorStats : Asset
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x0004EED8 File Offset: 0x0004D0D8
	public ActorStats()
	{
		this.baseStats.damage = 10;
		this.baseStats.health = 100;
		this.baseStats.armor = 0;
		this.baseStats.speed = 40f;
		this.baseStats.dodge = 0f;
		this.baseStats.accuracy = 90f;
		this.baseStats.attackSpeed = 1f;
		this.baseStats.diplomacy = 0;
		this.baseStats.knockback = 1f;
		this.baseStats.knockbackReduction = 0f;
		this.baseStats.targets = 1;
		this.baseStats.areaOfEffect = 1f;
		this.baseStats.size = 0.5f;
		this.baseStats.range = 1f;
		this.baseStats.damageCritMod = 2f;
		this.baseStats.scale = 0.1f;
		this.baseStats.mod_supply_timer = 1f;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0004F208 File Offset: 0x0004D408
	public void clearFlags()
	{
		this.flags.Clear();
		this.flags_dict = null;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0004F21C File Offset: 0x0004D41C
	public void clearTraits()
	{
		if (this.traits != null)
		{
			this.traits.Clear();
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0004F234 File Offset: 0x0004D434
	public bool haveFlag(string pFlagID)
	{
		if (this.flags_dict == null)
		{
			this.flags_dict = new Dictionary<string, bool>();
			foreach (string text in this.flags)
			{
				this.flags_dict.Add(text, true);
			}
		}
		return this.flags_dict.ContainsKey(pFlagID);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0004F2AC File Offset: 0x0004D4AC
	public void setBaseStats(int pHealth, int pDamage, int pSpeed, int pArmor, int pDodge, int pAccuracy, int pDiplomacy = 0)
	{
		this.baseStats.health = pHealth;
		this.baseStats.damage = pDamage;
		this.baseStats.speed = (float)pSpeed;
		this.baseStats.armor = pArmor;
		this.baseStats.dodge = (float)pDodge;
		this.baseStats.accuracy = (float)pAccuracy;
		this.baseStats.diplomacy = pDiplomacy;
	}

	// Token: 0x0400089F RID: 2207
	public UnitTextureAtlasID texture_atlas;

	// Token: 0x040008A0 RID: 2208
	public bool ignoredByInfinityCoin;

	// Token: 0x040008A1 RID: 2209
	public bool newBeh;

	// Token: 0x040008A2 RID: 2210
	public string race = "animal";

	// Token: 0x040008A3 RID: 2211
	public string nameLocale = "";

	// Token: 0x040008A4 RID: 2212
	public string kingdom = "";

	// Token: 0x040008A5 RID: 2213
	public ActorSize actorSize = ActorSize.S13_Human;

	// Token: 0x040008A6 RID: 2214
	public string animation_walk = string.Empty;

	// Token: 0x040008A7 RID: 2215
	public float animation_walk_speed = 0.1f;

	// Token: 0x040008A8 RID: 2216
	public string animation_swim = string.Empty;

	// Token: 0x040008A9 RID: 2217
	public float animation_swim_speed = 0.1f;

	// Token: 0x040008AA RID: 2218
	public float animation_idle_speed = 0.1f;

	// Token: 0x040008AB RID: 2219
	public string animation_idle = "walk_0";

	// Token: 0x040008AC RID: 2220
	public string texture_heads = string.Empty;

	// Token: 0x040008AD RID: 2221
	public BaseStats baseStats = new BaseStats();

	// Token: 0x040008AE RID: 2222
	public string tech = string.Empty;

	// Token: 0x040008AF RID: 2223
	public string defaultAttack = "base";

	// Token: 0x040008B0 RID: 2224
	public bool immune_to_tumor;

	// Token: 0x040008B1 RID: 2225
	public bool immune_to_slowness;

	// Token: 0x040008B2 RID: 2226
	public int aggression;

	// Token: 0x040008B3 RID: 2227
	public bool shadow = true;

	// Token: 0x040008B4 RID: 2228
	public string shadowTexture = "unitShadow_5";

	// Token: 0x040008B5 RID: 2229
	internal Sprite shadow_sprite;

	// Token: 0x040008B6 RID: 2230
	public bool hit_fx_alternative_offset = true;

	// Token: 0x040008B7 RID: 2231
	public bool canLevelUp = true;

	// Token: 0x040008B8 RID: 2232
	public string nameTemplate = "default_name";

	// Token: 0x040008B9 RID: 2233
	public bool playRandomSound;

	// Token: 0x040008BA RID: 2234
	public string playRandomSound_id = "-";

	// Token: 0x040008BB RID: 2235
	public int maxAge;

	// Token: 0x040008BC RID: 2236
	public int timeToGrow;

	// Token: 0x040008BD RID: 2237
	public bool use_items;

	// Token: 0x040008BE RID: 2238
	public bool take_items;

	// Token: 0x040008BF RID: 2239
	public bool take_items_ignore_range_weapons;

	// Token: 0x040008C0 RID: 2240
	[NonSerialized]
	public bool useSkinColors;

	// Token: 0x040008C1 RID: 2241
	[NonSerialized]
	public List<ColorSet> color_sets;

	// Token: 0x040008C2 RID: 2242
	public bool canBeKilledByStuff;

	// Token: 0x040008C3 RID: 2243
	public bool canBeKilledByLifeEraser;

	// Token: 0x040008C4 RID: 2244
	public bool ignoreTileSpeedMod;

	// Token: 0x040008C5 RID: 2245
	public bool skipFightLogic;

	// Token: 0x040008C6 RID: 2246
	public bool canAttackBuildings;

	// Token: 0x040008C7 RID: 2247
	public bool canAttackBrains;

	// Token: 0x040008C8 RID: 2248
	public bool ignoreJobs;

	// Token: 0x040008C9 RID: 2249
	public bool countAsUnit = true;

	// Token: 0x040008CA RID: 2250
	public bool egg;

	// Token: 0x040008CB RID: 2251
	public bool flag_tornado;

	// Token: 0x040008CC RID: 2252
	public bool flag_ufo;

	// Token: 0x040008CD RID: 2253
	public bool flag_finger;

	// Token: 0x040008CE RID: 2254
	public bool animal;

	// Token: 0x040008CF RID: 2255
	public bool unit;

	// Token: 0x040008D0 RID: 2256
	public bool baby;

	// Token: 0x040008D1 RID: 2257
	public string texture_path = string.Empty;

	// Token: 0x040008D2 RID: 2258
	public bool body_separate_part_head;

	// Token: 0x040008D3 RID: 2259
	public bool body_separate_part_hands;

	// Token: 0x040008D4 RID: 2260
	public float hovering_min = 0.5f;

	// Token: 0x040008D5 RID: 2261
	public float hovering_max = 1.2f;

	// Token: 0x040008D6 RID: 2262
	public bool hovering;

	// Token: 0x040008D7 RID: 2263
	public bool flying;

	// Token: 0x040008D8 RID: 2264
	public bool very_high_flyer;

	// Token: 0x040008D9 RID: 2265
	public bool disableJumpAnimation;

	// Token: 0x040008DA RID: 2266
	public bool rotatingAnimation;

	// Token: 0x040008DB RID: 2267
	public bool dieOnBlocks = true;

	// Token: 0x040008DC RID: 2268
	public bool moveFromBlock = true;

	// Token: 0x040008DD RID: 2269
	public bool dieOnGround;

	// Token: 0x040008DE RID: 2270
	public bool damagedByOcean;

	// Token: 0x040008DF RID: 2271
	public bool damagedByRain;

	// Token: 0x040008E0 RID: 2272
	public bool oceanCreature;

	// Token: 0x040008E1 RID: 2273
	public bool landCreature;

	// Token: 0x040008E2 RID: 2274
	public bool swampCreature;

	// Token: 0x040008E3 RID: 2275
	public bool isBoat;

	// Token: 0x040008E4 RID: 2276
	public bool drawBoatMark;

	// Token: 0x040008E5 RID: 2277
	public bool swimToIsland;

	// Token: 0x040008E6 RID: 2278
	public float speedModLiquid = 1f;

	// Token: 0x040008E7 RID: 2279
	public bool procreate;

	// Token: 0x040008E8 RID: 2280
	public int procreate_age = 3;

	// Token: 0x040008E9 RID: 2281
	public int animal_baby_making_around_limit = 6;

	// Token: 0x040008EA RID: 2282
	public bool layEggs;

	// Token: 0x040008EB RID: 2283
	public string eggStatsID = "";

	// Token: 0x040008EC RID: 2284
	public string prefab = "";

	// Token: 0x040008ED RID: 2285
	public bool dieInLava = true;

	// Token: 0x040008EE RID: 2286
	public bool canBeMovedByPowers;

	// Token: 0x040008EF RID: 2287
	public bool canBeHurtByPowers;

	// Token: 0x040008F0 RID: 2288
	public bool canTurnIntoZombie;

	// Token: 0x040008F1 RID: 2289
	public bool canTurnIntoMush;

	// Token: 0x040008F2 RID: 2290
	public bool canTurnIntoTumorMonster;

	// Token: 0x040008F3 RID: 2291
	public bool have_soul;

	// Token: 0x040008F4 RID: 2292
	public bool canReceiveTraits = true;

	// Token: 0x040008F5 RID: 2293
	public string zombieID = "";

	// Token: 0x040008F6 RID: 2294
	public string skeletonID = "";

	// Token: 0x040008F7 RID: 2295
	public string mushID = "";

	// Token: 0x040008F8 RID: 2296
	public string tumorMonsterID = "";

	// Token: 0x040008F9 RID: 2297
	public bool showIconInspectWindow;

	// Token: 0x040008FA RID: 2298
	public string showIconInspectWindow_id = "";

	// Token: 0x040008FB RID: 2299
	public bool hideFavoriteIcon;

	// Token: 0x040008FC RID: 2300
	public bool canBeInspected;

	// Token: 0x040008FD RID: 2301
	public float inspectAvatarScale = 2.5f;

	// Token: 0x040008FE RID: 2302
	public int maxHunger = 100;

	// Token: 0x040008FF RID: 2303
	public bool needFood;

	// Token: 0x04000900 RID: 2304
	public bool diet_berries;

	// Token: 0x04000901 RID: 2305
	public bool diet_crops;

	// Token: 0x04000902 RID: 2306
	public bool diet_flowers;

	// Token: 0x04000903 RID: 2307
	public bool diet_grass;

	// Token: 0x04000904 RID: 2308
	public bool diet_meat;

	// Token: 0x04000905 RID: 2309
	public bool diet_meat_insect;

	// Token: 0x04000906 RID: 2310
	public bool diet_meat_same_race;

	// Token: 0x04000907 RID: 2311
	public bool source_meat;

	// Token: 0x04000908 RID: 2312
	public bool source_meat_insect;

	// Token: 0x04000909 RID: 2313
	public float defaultZ;

	// Token: 0x0400090A RID: 2314
	public bool updateZ;

	// Token: 0x0400090B RID: 2315
	public bool hideOnMinimap;

	// Token: 0x0400090C RID: 2316
	public bool inspect_stats = true;

	// Token: 0x0400090D RID: 2317
	public bool inspect_children = true;

	// Token: 0x0400090E RID: 2318
	public bool inspect_kills = true;

	// Token: 0x0400090F RID: 2319
	public bool inspect_experience = true;

	// Token: 0x04000910 RID: 2320
	public bool inspect_home;

	// Token: 0x04000911 RID: 2321
	public bool immune_to_injuries;

	// Token: 0x04000912 RID: 2322
	public bool sprite_group_renderer;

	// Token: 0x04000913 RID: 2323
	public string job = "";

	// Token: 0x04000914 RID: 2324
	public string effect_cast_top = "";

	// Token: 0x04000915 RID: 2325
	public string effect_cast_ground = "";

	// Token: 0x04000916 RID: 2326
	public string effect_teleport = "";

	// Token: 0x04000917 RID: 2327
	public List<string> attack_spells;

	// Token: 0x04000918 RID: 2328
	public int heads = 7;

	// Token: 0x04000919 RID: 2329
	public bool effectDamage;

	// Token: 0x0400091A RID: 2330
	public bool specialAnimation;

	// Token: 0x0400091B RID: 2331
	public bool flipAnimation;

	// Token: 0x0400091C RID: 2332
	public bool specialDeadAnimation;

	// Token: 0x0400091D RID: 2333
	public bool deathAnimationAngle;

	// Token: 0x0400091E RID: 2334
	public bool canHaveStatusEffect;

	// Token: 0x0400091F RID: 2335
	public bool haveSpriteRenderer = true;

	// Token: 0x04000920 RID: 2336
	public bool dieByLightning;

	// Token: 0x04000921 RID: 2337
	public bool have_skin = true;

	// Token: 0x04000922 RID: 2338
	public string growIntoID = "";

	// Token: 0x04000923 RID: 2339
	public string icon = "iconQuestionMark";

	// Token: 0x04000924 RID: 2340
	public bool skipSave;

	// Token: 0x04000925 RID: 2341
	public Color32 color = Color.clear;

	// Token: 0x04000926 RID: 2342
	public ConstructionCost cost;

	// Token: 0x04000927 RID: 2343
	public List<string> traits;

	// Token: 0x04000928 RID: 2344
	public List<string> defaultWeapons;

	// Token: 0x04000929 RID: 2345
	public List<string> defaultWeaponsMaterial;

	// Token: 0x0400092A RID: 2346
	public List<string> flags = new List<string>();

	// Token: 0x0400092B RID: 2347
	private Dictionary<string, bool> flags_dict;

	// Token: 0x0400092C RID: 2348
	public bool disablePunchAttackAnimation;

	// Token: 0x0400092D RID: 2349
	public int maxRandomAmount = 3;

	// Token: 0x0400092E RID: 2350
	public int currentAmount;

	// Token: 0x0400092F RID: 2351
	public WorldAction action_death;
}
