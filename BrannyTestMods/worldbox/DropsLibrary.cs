using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class DropsLibrary : AssetLibrary<DropAsset>
{
	// Token: 0x0600010B RID: 267 RVA: 0x000148C8 File Offset: 0x00012AC8
	public override void init()
	{
		base.init();
		this.add(new DropAsset
		{
			id = "tnt",
			sound_drop = "fallingSand",
			animated = true,
			texture = "drops/drop_tnt",
			animation_speed = 0.03f,
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_tnt)
		});
		this.add(new DropAsset
		{
			id = "tnt_timed",
			texture = "drops/drop_tnttimed",
			default_scale = 0.2f,
			sound_drop = "fallingSand",
			action_landed = new DropsAction(DropsLibrary.action_tnt_timed)
		});
		this.add(new DropAsset
		{
			id = "water_bomb",
			texture = "drops/drop_waterbomb",
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_water_bomb)
		});
		this.add(new DropAsset
		{
			id = "landmine",
			texture = "drops/drop_landmine",
			default_scale = 0.2f,
			sound_drop = "fallingSand",
			action_landed = new DropsAction(DropsLibrary.action_landmine)
		});
		this.add(new DropAsset
		{
			id = "fireworks",
			texture = "drops/drop_fireworks",
			random_frame = true,
			default_scale = 0.1f,
			sound_drop = "fallingSand",
			action_landed = new DropsAction(DropsLibrary.action_fireworks)
		});
		this.add(new DropAsset
		{
			id = "inspiration",
			texture = "drops/drop_fire",
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_inspiration)
		});
		this.add(new DropAsset
		{
			id = "friendship",
			texture = "drops/drop_friendship",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_friendship)
		});
		this.add(new DropAsset
		{
			id = "spite",
			texture = "drops/drop_spite",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_spite)
		});
		this.add(new DropAsset
		{
			id = "madness",
			texture = "drops/drop_madness",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_madness)
		});
		this.add(new DropAsset
		{
			id = "blessing",
			texture = "drops/drop_blessing",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_blessing)
		});
		DropAsset t = this.t;
		t.action_landed = (DropsAction)Delegate.Combine(t.action_landed, new DropsAction(ActionLibrary.action_shrinkTornadoes));
		this.add(new DropAsset
		{
			id = "shield",
			texture = "drops/drop_shield",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_shield)
		});
		this.add(new DropAsset
		{
			id = "coffee",
			texture = "drops/drop_coffee",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_coffee)
		});
		this.add(new DropAsset
		{
			id = "powerup",
			texture = "drops/drop_mushroom_powerup",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_powerup)
		});
		this.add(new DropAsset
		{
			id = "curse",
			texture = "drops/drop_curse",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_curse)
		});
		DropAsset t2 = this.t;
		t2.action_landed = (DropsAction)Delegate.Combine(t2.action_landed, new DropsAction(ActionLibrary.action_growTornadoes));
		this.add(new DropAsset
		{
			id = "zombieInfection",
			texture = "drops/drop_zombieinfection",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_zombieInfection)
		});
		this.add(new DropAsset
		{
			id = "mushSpores",
			texture = "drops/drop_mushSpores",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_mushSpore)
		});
		this.add(new DropAsset
		{
			id = "plague",
			texture = "drops/drop_plague",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_plague)
		});
		this.add(new DropAsset
		{
			id = "livingPlants",
			texture = "drops/drop_blessing",
			animated = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_livingPlants)
		});
		this.add(new DropAsset
		{
			id = "livingHouse",
			texture = "drops/drop_blessing",
			animated = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_livingHouse)
		});
		this.add(new DropAsset
		{
			id = "bomb",
			sound_launch = "bomb fall",
			sound_drop = "explosion small",
			texture = "drops/drop_bomb",
			default_scale = 0.2f,
			fallingHeight = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_bomb)
		});
		DropAsset t3 = this.t;
		t3.action_launch = (DropsAction)Delegate.Combine(t3.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "grenade",
			texture = "drops/drop_grenade",
			animated = true,
			default_scale = 0.2f,
			animation_speed = 0.03f,
			sound_drop = "explosion small",
			fallingHeight = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_grenade),
			random_flip = true
		});
		this.add(new DropAsset
		{
			id = "napalmBomb",
			texture = "drops/drop_napalmbomb",
			default_scale = 0.2f,
			sound_drop = "explosion small",
			fallingHeight = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_napalmBomb),
			random_flip = true
		});
		DropAsset t4 = this.t;
		t4.action_launch = (DropsAction)Delegate.Combine(t4.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "atomicBomb",
			texture = "drops/drop_atomicbomb",
			default_scale = 0.2f,
			sound_launch = "bomb fall",
			sound_drop = "explosion medium",
			fallingHeight = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_atomicBomb),
			random_flip = true
		});
		DropAsset t5 = this.t;
		t5.action_launch = (DropsAction)Delegate.Combine(t5.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "antimatterBomb",
			texture = "drops/drop_antimatterbomb",
			default_scale = 0.2f,
			fallingHeight = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_antimatterBomb)
		});
		DropAsset t6 = this.t;
		t6.action_launch = (DropsAction)Delegate.Combine(t6.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "czarBomba",
			texture = "drops/drop_czarbomba",
			default_scale = 0.2f,
			sound_launch = "bomb fall",
			sound_drop = "explosion big",
			fallingHeight = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_czarBomba)
		});
		DropAsset t7 = this.t;
		t7.action_launch = (DropsAction)Delegate.Combine(t7.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "rain",
			texture = "drops/drop_rain",
			random_frame = true,
			default_scale = 0.2f,
			fallingHeight = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_rain)
		});
		this.add(new DropAsset
		{
			id = "bouncy_sand",
			default_scale = 0.2f,
			random_frame = true
		});
		this.add(new DropAsset
		{
			id = "bouncy_soil",
			default_scale = 0.2f,
			random_frame = true
		});
		this.add(new DropAsset
		{
			id = "bloodRain",
			texture = "drops/drop_blood",
			random_frame = true,
			default_scale = 0.1f,
			fallingHeight = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_bloodRain)
		});
		this.add(new DropAsset
		{
			id = "cure",
			texture = "drops/drop_cure",
			random_frame = true,
			default_scale = 0.1f,
			fallingHeight = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_cure)
		});
		this.add(new DropAsset
		{
			id = "fire",
			texture = "drops/drop_fire",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.2f,
			sound_launch = "fallingFire",
			fallingHeight = new Vector2(30f, 45f),
			fallingRandomXMove = true,
			particleInterval = 0.3f,
			action_landed = new DropsAction(DropsLibrary.action_fire),
			animation_speed_random = 0.08f,
			random_frame = true,
			random_flip = true
		});
		this.add(new DropAsset
		{
			id = "snow",
			texture = "drops/drop_snow",
			random_frame = true,
			default_scale = 0.2f,
			sound_drop = "snow",
			fallingSpeed = 0.3f,
			fallingHeight = new Vector2(30f, 45f),
			fallingRandomXMove = true,
			particleInterval = 0.15f,
			action_landed = new DropsAction(DropsLibrary.action_snow)
		});
		this.add(new DropAsset
		{
			id = "acid",
			texture = "drops/drop_acid",
			random_frame = true,
			default_scale = 0.2f,
			sound_drop = "acid",
			fallingHeight = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_acid)
		});
		this.add(new DropAsset
		{
			id = "lava",
			texture = "drops/drop_lava",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.2f,
			sound_drop = "lava",
			fallingHeight = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_lava)
		});
		this.add(new DropAsset
		{
			id = "santa_bomb",
			texture = "drops/drop_santabomb",
			random_frame = true,
			default_scale = 0.2f,
			sound_launch = "bomb fall",
			sound_drop = "explosion small",
			action_landed = new DropsAction(DropsLibrary.action_santa_bomb)
		});
		this.add(new DropAsset
		{
			id = "_spawn_building",
			texture = "drops/drop_stone",
			random_frame = true,
			default_scale = 0.2f,
			fallingHeight = new Vector2(10f, 15f),
			fallingSpeed = 5f
		});
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.clone("seedsGrass", "_spawn_building");
		this.t.texture = "drops/drop_seed_grass";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_grass);
		this.clone("seedsEnchanted", "_spawn_building");
		this.t.texture = "drops/drop_seed_enchanted";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_enchanted);
		this.clone("seedsSavanna", "_spawn_building");
		this.t.texture = "drops/drop_seed_savanna";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_savanna);
		this.clone("seedsCorrupted", "_spawn_building");
		this.t.texture = "drops/drop_seed_corrupted";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_corrupted);
		this.clone("seedsMushroom", "_spawn_building");
		this.t.texture = "drops/drop_seed_mushroom";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_mushroom);
		this.clone("seedsJungle", "_spawn_building");
		this.t.texture = "drops/drop_seed_jungle";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_jungle);
		this.clone("seedsSwamp", "_spawn_building");
		this.t.texture = "drops/drop_seed_swamp";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_swamp);
		this.clone("seedsInfernal", "_spawn_building");
		this.t.texture = "drops/drop_seed_infernal";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_seeds_infernal);
		this.clone("fruitBush", "_spawn_building");
		this.t.texture = "drops/drop_seed";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_fruitBush);
		this.clone("fertilizerPlants", "_spawn_building");
		this.t.texture = "drops/drop_fertilizer";
		this.t.fallingSpeed = 3f;
		DropAsset t8 = this.t;
		t8.action_landed = (DropsAction)Delegate.Combine(t8.action_landed, new DropsAction(DropsLibrary.action_fertilizerPlants));
		DropAsset t9 = this.t;
		t9.action_landed = (DropsAction)Delegate.Combine(t9.action_landed, new DropsAction(DropsLibrary.flash));
		this.clone("fertilizerTrees", "_spawn_building");
		this.t.texture = "drops/drop_fertilizer";
		this.t.fallingSpeed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_fertilizerTrees);
		DropAsset t10 = this.t;
		t10.action_landed = (DropsAction)Delegate.Combine(t10.action_landed, new DropsAction(DropsLibrary.flash));
		this.clone("stone", "_spawn_building");
		this.t.texture = "drops/drop_stone";
		this.t.default_scale = 0.2f;
		this.t.constructionTemplate = "stone,stone_m,stone_s";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "stone";
		this.clone("ore_deposit", "_spawn_building");
		this.t.texture = "drops/drop_metal";
		this.t.default_scale = 0.2f;
		this.t.constructionTemplate = "ore_deposit,ore_deposit_m,ore_deposit_s";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "ore_deposit";
		this.clone("gold", "_spawn_building");
		this.t.texture = "drops/drop_gold";
		this.t.default_scale = 0.2f;
		this.t.constructionTemplate = "gold,gold_m,gold_s";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "gold";
		this.clone("tumor", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "spawnTumor";
		this.clone("biomass", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "spawnBiomass";
		this.clone("cybercore", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "spawnCybercore";
		this.clone("superPumpkin", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "spawnSuperPumpkin";
		this.clone("geyser", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "geyser";
		this.clone("geyserAcid", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "geyserAcid";
		this.clone("volcano", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.constructionTemplateSound = "spawnVolcano";
		this.clone("goldenBrain", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.clone("corruptedBrain", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.clone("iceTower", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.clone("beehive", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.clone("flameTower", "_spawn_building");
		this.t.constructionTemplate = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00015E28 File Offset: 0x00014028
	public static void useDropSeedOn(WorldTile pTile, TopTileType pTypeLow, TopTileType pHigh)
	{
		DropsLibrary.useSeedOn(pTile, pTypeLow, pHigh);
		for (int i = 0; i < pTile.neighbours.Count; i++)
		{
			DropsLibrary.useSeedOn(pTile.neighbours[i], pTypeLow, pHigh);
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00015E66 File Offset: 0x00014066
	public static void useSeedOn(WorldTile pTile, TopTileType pTypeLow, TopTileType pHigh)
	{
		if (!pTile.Type.canGrowBiomeGrass)
		{
			return;
		}
		if (pTile.main_type.rankType == TileRank.Low)
		{
			MapAction.growGreens(pTile, pTypeLow);
			return;
		}
		if (pTile.main_type.rankType == TileRank.High)
		{
			MapAction.growGreens(pTile, pHigh);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00015EA1 File Offset: 0x000140A1
	public static void action_seeds_grass(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.grass_low, TopTileLibrary.grass_high);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00015EB3 File Offset: 0x000140B3
	public static void action_seeds_enchanted(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.enchanted_low, TopTileLibrary.enchanted_high);
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00015EC5 File Offset: 0x000140C5
	public static void action_seeds_corrupted(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.corruption_low, TopTileLibrary.corruption_high);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00015ED7 File Offset: 0x000140D7
	public static void action_seeds_savanna(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.savanna_low, TopTileLibrary.savanna_high);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00015EE9 File Offset: 0x000140E9
	public static void action_seeds_swamp(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.swamp_low, TopTileLibrary.swamp_high);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00015EFB File Offset: 0x000140FB
	public static void action_seeds_jungle(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.jungle_low, TopTileLibrary.jungle_high);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00015F0D File Offset: 0x0001410D
	public static void action_seeds_infernal(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.infernal_low, TopTileLibrary.infernal_high);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00015F1F File Offset: 0x0001411F
	public static void action_seeds_mushroom(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useDropSeedOn(pTile, TopTileLibrary.mushroom_low, TopTileLibrary.mushroom_high);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00015F34 File Offset: 0x00014134
	public static void action_rain(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useRainOn(pTile);
		for (int i = 0; i < pTile.neighbours.Count; i++)
		{
			DropsLibrary.useRainOn(pTile.neighbours[i]);
		}
		for (int j = 0; j < pTile.zone.tiles.Count; j++)
		{
			WorldTile worldTile = pTile.zone.tiles[j];
			if (worldTile.data.fire)
			{
				worldTile.stopFire(false);
			}
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00015FB0 File Offset: 0x000141B0
	private static void useRainOn(WorldTile pTile)
	{
		pTile.stopFire(false);
		for (int i = 0; i < pTile.units.Count; i++)
		{
			Actor actor = pTile.units[i];
			if (actor.data.alive)
			{
				actor.removeStatusEffect("burning", null, -1);
				if (actor.stats.damagedByRain)
				{
					actor.getHit(13f, true, AttackType.Other, null, true);
				}
			}
		}
		if (pTile.building != null)
		{
			pTile.building.stopFire();
			if (pTile.building.stats.buildingType == BuildingType.Wheat)
			{
				pTile.building.GetComponent<Wheat>().growWheat();
			}
			if (pTile.building.stats.damagedByRain)
			{
				pTile.building.getHit(13f, true, AttackType.Other, null, true);
			}
		}
		pTile.removeBurn();
		if (pTile.Type.canBeFilledWithOcean)
		{
			MapAction.setOcean(pTile);
		}
		if (pTile.Type.lava)
		{
			MapBox.instance.lavaLayer.putOut(pTile);
		}
		if (pTile.Type.explodableByOcean)
		{
			MapBox.instance.explosionLayer.explodeBomb(pTile, false);
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000160D4 File Offset: 0x000142D4
	public static void action_acid(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.checkAcidTerraform(pTile);
		if (Toolbox.randomChance(0.2f))
		{
			MapBox.instance.particlesSmoke.spawn(pTile.posV3);
		}
		if (pTile.building != null && pTile.building.stats.affectedByAcid && pTile.building.data.alive)
		{
			pTile.building.getHit(20f, true, AttackType.Other, null, true);
		}
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (!Toolbox.randomChance(0.6f) && !actor.haveTrait("acid_proof") && !actor.haveTrait("acid_blood"))
			{
				actor.getHit(20f, true, AttackType.Other, null, true);
			}
		}
		MapBox.instance.conwayLayer.checkKillRange(pTile.pos, 2);
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000161D6 File Offset: 0x000143D6
	public static void action_fire(WorldTile pTile = null, string pDropID = null)
	{
		ActionLibrary.action_fire(null, pTile);
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000161E0 File Offset: 0x000143E0
	public static void action_fireworks(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.terraformTop(pTile, TopTileLibrary.fireworks, TerraformLibrary.remove);
	}

	// Token: 0x0600011B RID: 283 RVA: 0x000161F4 File Offset: 0x000143F4
	public static void action_tnt(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.lava || pTile.data.fire)
		{
			MapAction.terraformTop(pTile, TopTileLibrary.tnt, TerraformLibrary.remove);
			MapBox.instance.explosionLayer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.tnt, TerraformLibrary.remove);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00016250 File Offset: 0x00014450
	public static void action_tnt_timed(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.lava || pTile.data.fire)
		{
			MapAction.terraformTop(pTile, TopTileLibrary.tnt_timed, TerraformLibrary.remove);
			MapBox.instance.explosionLayer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.tnt_timed, TerraformLibrary.remove);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x000162A9 File Offset: 0x000144A9
	public static void action_czarBomba(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.stackEffects.spawnNukeFlash(pTile, "czarBomba");
		MapBox.instance.startShake(0.3f, 0.01f, 2f, true, true);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000162DB File Offset: 0x000144DB
	public static void action_atomicBomb(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.startShake(0.3f, 0.01f, 2f, true, true);
		MapBox.instance.stackEffects.spawnNukeFlash(pTile, "atomicBomb");
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0001630D File Offset: 0x0001450D
	public static void action_antimatterBomb(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.startShake(0.3f, 0.01f, 2f, true, true);
		MapBox.instance.stackEffects.spawnAntimatterEffect(pTile);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0001633C File Offset: 0x0001453C
	public static void action_napalmBomb(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.startShake(0.3f, 0.01f, 2f, true, true);
		MapBox.instance.stackEffects.spawnNapalmFlash(pTile, "napalmBomb");
		MapBox.instance.stackEffects.get("explosionSmall").spawnAtRandomScale(pTile, 0.15f, 0.3f);
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0001639E File Offset: 0x0001459E
	public static void action_grenade(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.damageWorld(pTile, 5, AssetManager.terraform.get("grenade"));
		MapBox.instance.stackEffects.get("explosionSmall").spawnAtRandomScale(pTile, 0.1f, 0.15f);
	}

	// Token: 0x06000122 RID: 290 RVA: 0x000163DC File Offset: 0x000145DC
	public static void action_bomb(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.stackEffects.get("explosionSmall").spawnAtRandomScale(pTile, 0.45f, 0.6f);
		if (ExplosionChecker.instance.checkNearby(pTile, 10))
		{
			return;
		}
		MapAction.damageWorld(pTile, 10, AssetManager.terraform.get("bomb"));
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00016435 File Offset: 0x00014635
	public static void action_santa_bomb(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.damageWorld(pTile, 10, AssetManager.terraform.get("santa_bomb"));
		MapBox.instance.stackEffects.get("explosionSmall").spawnAtRandomScale(pTile, 0.45f, 0.6f);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00016474 File Offset: 0x00014674
	public static void action_water_bomb(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.liquid || pTile.Type.lava || pTile.data.fire)
		{
			MapAction.terraformTop(pTile, TopTileLibrary.water_bomb, TerraformLibrary.remove);
			MapBox.instance.explosionLayer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.water_bomb, TerraformLibrary.remove);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x000164DA File Offset: 0x000146DA
	public static void action_lava(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.lavaLayer.addLava(pTile, "lava3");
	}

	// Token: 0x06000126 RID: 294 RVA: 0x000164F4 File Offset: 0x000146F4
	public static void action_snow(WorldTile pTile = null, string pDropID = null)
	{
		if (!pTile.Type.frozen)
		{
			MapAction.freezeTile(pTile);
			MapAction.freezeTile(pTile);
		}
		for (int i = 0; i < 10; i++)
		{
			WorldTile random = pTile.chunk.tiles.GetRandom<WorldTile>();
			if (!random.Type.frozen)
			{
				if (Toolbox.DistTile(pTile, random) < 11f)
				{
					return;
				}
				MapAction.freezeTile(random);
				MapAction.freezeTile(random);
			}
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00016560 File Offset: 0x00014760
	public static void action_cure(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 4, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (actor.base_data.alive)
			{
				actor.removeTrait("plague");
				actor.removeTrait("cursed");
				actor.removeTrait("tumorInfection");
				actor.removeTrait("mushSpores");
				actor.removeTrait("infected");
				actor.startShake(0.3f, 0.1f, true, true);
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00016610 File Offset: 0x00014810
	public static void action_bloodRain(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (actor.base_data.alive)
			{
				actor.removeStatusEffect("burning", null, -1);
				actor.restoreHealth((int)((float)actor.curStats.health * 0.2f));
				actor.startShake(0.3f, 0.1f, true, true);
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000166AC File Offset: 0x000148AC
	public static void action_plague(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			actor.addTrait("plague");
			if (actor.haveTrait("plague"))
			{
				actor.startShake(0.3f, 0.1f, true, true);
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0001672C File Offset: 0x0001492C
	public static void action_zombieInfection(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (actor.stats.canTurnIntoZombie && !actor.haveTrait("zombie"))
			{
				actor.addTrait("infected");
				actor.startShake(0.3f, 0.1f, true, true);
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x000167BC File Offset: 0x000149BC
	public static void action_mushSpore(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (actor.stats.canTurnIntoMush && !actor.haveTrait("mushSpores"))
			{
				actor.addTrait("mushSpores");
				actor.startShake(0.3f, 0.1f, true, true);
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0001684C File Offset: 0x00014A4C
	public static void action_curse(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (actor.addTrait("cursed"))
			{
				actor.setStatsDirty();
				actor.removeTrait("blessed");
				actor.startShake(0.3f, 0.1f, true, true);
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000168D4 File Offset: 0x00014AD4
	public static void action_shield(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			BaseSimObject baseSimObject = MapBox.instance.temp_map_objects[i];
			baseSimObject.addStatusEffect("shield", -1f);
			if (baseSimObject.isActor())
			{
				baseSimObject.a.startColorEffect("white");
			}
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00016944 File Offset: 0x00014B44
	public static void action_powerup(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			BaseSimObject baseSimObject = MapBox.instance.temp_map_objects[i];
			baseSimObject.addStatusEffect("powerup", -1f);
			baseSimObject.a.startColorEffect("white");
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000169A8 File Offset: 0x00014BA8
	public static void action_coffee(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			BaseSimObject baseSimObject = MapBox.instance.temp_map_objects[i];
			baseSimObject.addStatusEffect("caffeinated", -1f);
			if (baseSimObject.isActor())
			{
				baseSimObject.a.startColorEffect("white");
			}
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00016A18 File Offset: 0x00014C18
	public static void action_blessing(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (actor.addTrait("blessed"))
			{
				actor.setStatsDirty();
				actor.event_full_heal = true;
			}
			actor.removeTrait("cursed");
			actor.startShake(0.3f, 0.1f, true, true);
			actor.startColorEffect("white");
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00016AA4 File Offset: 0x00014CA4
	public static void action_madness(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			((Actor)MapBox.instance.temp_map_objects[i]).addTrait("madness");
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00016AF8 File Offset: 0x00014CF8
	public static void action_inspiration(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.zone.city == null)
		{
			return;
		}
		if (pTile.zone.city.kingdom.cities.Count == 1)
		{
			return;
		}
		if (pTile.zone.city.kingdom.capital == pTile.zone.city)
		{
			return;
		}
		pTile.zone.city.useInspire();
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00016B6F File Offset: 0x00014D6F
	public static void action_spite(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.zone.city == null)
		{
			return;
		}
		MapBox.instance.kingdoms.diplomacyManager.eventSpite(pTile.zone.city.kingdom);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00016BA9 File Offset: 0x00014DA9
	public static void action_friendship(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.zone.city == null)
		{
			return;
		}
		MapBox.instance.kingdoms.diplomacyManager.eventFriendship(pTile.zone.city.kingdom);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00016BE4 File Offset: 0x00014DE4
	public static void action_spawn_building(WorldTile pTile = null, string pDropID = null)
	{
		DropAsset dropAsset = AssetManager.drops.get(pDropID);
		string[] list = dropAsset.constructionTemplate.Split(new char[]
		{
			','
		});
		if (MapBox.instance.addBuilding(list.GetRandom<string>(), pTile, null, true, false, BuildPlacingType.New) == null)
		{
			MapBox.instance.stackEffects.get("fx_bad_place").spawnAt(pTile, 0.25f);
			return;
		}
		if (!string.IsNullOrEmpty(dropAsset.constructionTemplateSound))
		{
			Sfx.play(dropAsset.constructionTemplateSound, true, -1f, -1f);
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00016C75 File Offset: 0x00014E75
	public static void flash(WorldTile pTile, string pDropID)
	{
		MapBox.instance.flashEffects.flashPixel(pTile, 20, ColorType.White);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00016C8A File Offset: 0x00014E8A
	public static void action_fertilizerPlants(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.tryGrowVegetationRandom(pTile, VegetationType.Plants, false, false);
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00016C9A File Offset: 0x00014E9A
	public static void action_fertilizerTrees(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.tryGrowVegetationRandom(pTile, VegetationType.Trees, false, false);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00016CAA File Offset: 0x00014EAA
	public static void action_fruitBush(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.tryGrowVegetation(pTile, true, "fruit_bush", true);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00016CBE File Offset: 0x00014EBE
	public static void action_landmine(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.lava)
		{
			MapBox.instance.explosionLayer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.landmine, TerraformLibrary.remove);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00016CF0 File Offset: 0x00014EF0
	public static void action_livingHouse(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary._tempList.Clear();
		DropsLibrary._tempList.AddRange(pTile.zone.buildings);
		DropsLibrary._tempList.AddRange(pTile.zone.ruins);
		DropsLibrary._tempList.AddRange(pTile.zone.abandoned);
		for (int i = 0; i < DropsLibrary._tempList.Count; i++)
		{
			Building building = DropsLibrary._tempList[i];
			if (!building.isRuin() && !building.data.underConstruction && building.stats.canBeLivingHouse)
			{
				Actor actor = MapBox.instance.createNewUnit("livingHouse", building.currentTile, null, 0f, null);
				actor.data.special_graphics = building.stats.id + "#" + building.animData_index.ToString();
				actor.spriteRenderer.sprite = building.s_main_sprite;
				building.destroyBuilding();
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00016DFA File Offset: 0x00014FFA
	public static void action_livingPlants(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.checkListForLivingPlants(pTile.zone.food, pTile);
		DropsLibrary.checkListForLivingPlants(pTile.zone.trees, pTile);
		DropsLibrary.checkListForLivingPlants(pTile.zone.wheat, pTile);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00016E30 File Offset: 0x00015030
	public static void checkListForLivingPlants(HashSet<Building> pList, WorldTile pTile)
	{
		DropsLibrary._tempList.Clear();
		DropsLibrary._tempList.AddRange(pList);
		for (int i = 0; i < DropsLibrary._tempList.Count; i++)
		{
			Building building = DropsLibrary._tempList[i];
			if (building.data.alive && building.stats.canBeLivingPlant && !building.chopped && !building.isRuin() && !building.data.underConstruction)
			{
				Actor actor = MapBox.instance.createNewUnit("livingPlants", building.currentTile, null, 0f, null);
				actor.data.special_graphics = building.stats.id + "#" + building.animData_index.ToString();
				actor.spriteRenderer.sprite = building.s_main_sprite;
				building.destroyBuilding();
				actor.startColorEffect("white");
			}
		}
	}

	// Token: 0x04000104 RID: 260
	private static List<Building> _tempList = new List<Building>();
}
