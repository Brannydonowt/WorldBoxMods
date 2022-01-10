using System;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x02000083 RID: 131
[ObfuscateLiterals]
public class BuildingLibrary : AssetLibrary<BuildingAsset>
{
	// Token: 0x060002CF RID: 719 RVA: 0x0002E5FC File Offset: 0x0002C7FC
	public override void init()
	{
		base.init();
		this.add(new BuildingAsset
		{
			id = "tree",
			fundament = new BuildingFundament(1, 1, 1, 0),
			ruins = "tree_dead",
			buildingType = BuildingType.Tree,
			resource_id = "wood",
			type = "trees",
			resources_given = 3,
			destroyOnLiquid = true,
			randomFlip = true,
			ignoredByCities = true,
			ignoreDemolish = true,
			burnable = true,
			affectedByAcid = true,
			maxTreesInZone = 5,
			affectedByLava = true,
			sfx = "spawn_tree",
			fauna = true,
			canBeDamagedByTornado = true,
			race = "nature",
			kingdom = "nature",
			checkForCloseBuilding = false,
			canBePlacedOnlyOn = List.Of<string>(new string[]
			{
				"grass_high",
				"grass_low"
			})
		});
		this.t.limit_per_zone = 3;
		this.t.canBeLivingPlant = true;
		this.t.baseStats.health = 10;
		this.clone("tree_dead", "tree");
		this.t.isRuin = true;
		this.t.burnable = false;
		this.t.buildingType = BuildingType.None;
		this.t.ruins = null;
		this.t.ignoreBuildings = true;
		this.t.baseStats.health = 1000;
		this.clone("corrupted_tree", "tree");
		this.t.limit_per_zone = 6;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"corrupted_high",
			"corrupted_low"
		});
		this.clone("corrupted_tree_big", "tree");
		this.t.fundament = new BuildingFundament(2, 2, 1, 0);
		this.clone("enchanted_tree", "tree");
		this.t.limit_per_zone = 4;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"enchanted_high",
			"enchanted_low"
		});
		this.clone("swamp_tree", "tree");
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.limit_per_zone = 2;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"swamp_high",
			"swamp_low"
		});
		this.t.canBePlacedOnLiquid = true;
		this.clone("savanna_tree", "tree");
		this.t.limit_per_zone = 7;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"savanna_high",
			"savanna_low"
		});
		this.clone("savanna_tree_big", "savanna_tree");
		this.t.fundament = new BuildingFundament(2, 2, 1, 0);
		this.clone("mushroom_tree", "tree");
		this.t.limit_per_zone = 2;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"mushroom_high",
			"mushroom_low"
		});
		this.clone("jungle_tree", "tree");
		this.t.limit_per_zone = 8;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"jungle_high",
			"jungle_low"
		});
		this.clone("infernal_tree", "tree");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"infernal_high",
			"infernal_low"
		});
		this.t.burnable = false;
		this.clone("infernal_tree_small", "infernal_tree");
		this.t.fundament = new BuildingFundament(0, 0, 1, 0);
		this.clone("infernal_tree_big", "infernal_tree");
		this.t.fundament = new BuildingFundament(2, 2, 1, 0);
		this.clone("cacti", "tree");
		this.t.maxTreesInZone = 1;
		this.t.vegetationRandomChance = 0.2f;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"sand"
		});
		this.clone("palm", "tree");
		this.t.maxTreesInZone = 1;
		this.t.vegetationRandomChance = 0.1f;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"sand"
		});
		this.clone("pine", "tree");
		this.t.maxTreesInZone = 3;
		this.t.vegetationRandomChance = 0.5f;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"snow_low",
			"snow_high",
			"grass_high",
			"grass_low",
			"soil_low",
			"soil_high"
		});
		this.clone("wasteland_tree", "tree");
		this.t.maxTreesInZone = 3;
		this.t.vegetationRandomChance = 0.5f;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"wasteland_high",
			"wasteland_low"
		});
		this.t.affectedByAcid = false;
		this.add(new BuildingAsset
		{
			id = "!vegetation",
			fundament = new BuildingFundament(0, 0, 0, 0),
			ruins = null,
			destroyOnLiquid = true,
			randomFlip = true,
			ignoredByCities = true,
			ignoreDemolish = true,
			burnable = true,
			affectedByAcid = true,
			affectedByLava = true,
			race = "nature",
			kingdom = "nature",
			buildingType = BuildingType.Plant
		});
		this.t.limit_per_zone = 3;
		this.t.priority = -1;
		this.t.canBePlacedOnBlocks = false;
		this.t.baseStats.health = 10;
		this.t.shadow = false;
		this.clone("snow_plant", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"snow_low",
			"snow_high"
		});
		this.clone("green_herb", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"grass_low",
			"grass_high"
		});
		this.clone("corrupted_plant", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"corrupted_low",
			"corrupted_high"
		});
		this.clone("jungle_plant", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"jungle_low",
			"jungle_high"
		});
		this.t.limit_per_zone = 6;
		this.clone("savanna_plant", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"savanna_low",
			"savanna_high"
		});
		this.clone("mushroom", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"grass_low",
			"grass_high",
			"mushroom_high",
			"mushroom_low"
		});
		this.clone("flower", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"grass_low",
			"grass_high",
			"mushroom_high",
			"mushroom_low",
			"enchanted_low",
			"enchanted_high"
		});
		this.t.type = "flower";
		this.clone("flame_flower", "flower");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"infernal_high",
			"infernal_low"
		});
		this.t.burnable = false;
		this.clone("jungle_flower", "flower");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"jungle_low",
			"jungle_high"
		});
		this.clone("wasteland_flower", "flower");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"wasteland_low",
			"wasteland_high"
		});
		this.t.affectedByAcid = false;
		this.clone("swamp_plant", "!vegetation");
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"swamp_low",
			"swamp_high"
		});
		this.t.fundament = new BuildingFundament(0, 0, 0, 0);
		this.t.canBePlacedOnLiquid = true;
		this.t.limit_per_zone = 4;
		this.clone("swamp_plant_big", "swamp_plant");
		this.t.limit_per_zone = 4;
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.add(new BuildingAsset
		{
			id = "!resource",
			fundament = new BuildingFundament(1, 1, 1, 0),
			ruins = null,
			destroyOnLiquid = true,
			randomFlip = true,
			ignoredByCities = false,
			ignoreDemolish = true,
			burnable = false,
			affectedByAcid = true,
			affectedByLava = true,
			race = "nature",
			kingdom = "nature"
		});
		this.t.canBePlacedOnBlocks = false;
		this.t.baseStats.health = 10;
		this.clone("!rock_temp", "!resource");
		this.t.destroyedSound = "buildingDestroyed";
		this.t.ignoreBuildings = true;
		this.clone("stone", "!rock_temp");
		this.t.buildingType = BuildingType.Stone;
		this.t.resource_id = "stone";
		this.t.destroyOnLiquid = false;
		this.t.resources_given = 3;
		this.t.sfx = "spawn_stone";
		this.clone("stone_m", "stone");
		this.t.resources_given = 2;
		this.clone("stone_s", "stone");
		this.t.resources_given = 1;
		this.clone("ore_deposit", "!rock_temp");
		this.t.buildingType = BuildingType.Ore;
		this.t.resource_id = "ore";
		this.t.sfx = "spawn_ore_deposit";
		this.t.resources_given = 3;
		this.clone("ore_deposit_m", "ore_deposit");
		this.t.resources_given = 2;
		this.clone("ore_deposit_s", "ore_deposit");
		this.t.resources_given = 1;
		this.clone("gold", "!rock_temp");
		this.t.buildingType = BuildingType.Gold;
		this.t.resource_id = "gold";
		this.t.sfx = "spawn_gold";
		this.t.resources_given = 3;
		this.clone("gold_m", "gold");
		this.t.resources_given = 2;
		this.clone("gold_s", "gold");
		this.t.resources_given = 1;
		this.clone("fruit_bush", "!resource");
		this.t.canBeLivingPlant = true;
		this.t.buildingType = BuildingType.Fruits;
		this.t.resource_id = "berries";
		this.t.resources_given = 3;
		this.t.type = "fruits";
		this.t.burnable = true;
		this.t.sfx = "spawn_bush";
		this.t.fauna = true;
		this.t.canBeDamagedByTornado = true;
		this.t.ignoredByCities = true;
		this.t.vegetationRandomChance = 0.02f;
		this.t.limit_per_zone = 2;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"jungle_low",
			"jungle_high",
			"grass_low",
			"grass_high",
			"mushroom_low",
			"mushroom_high",
			"enchanted_low",
			"enchanted_high",
			"savanna_low",
			"savanna_high"
		});
		this.add(new BuildingAsset
		{
			id = "0wheat",
			fundament = new BuildingFundament(0, 0, 0, 0),
			type = "crops",
			resource_id = "wheat",
			resources_given = 3,
			buildingType = BuildingType.Wheat,
			destroyOnLiquid = true,
			randomFlip = true,
			ignoredByCities = true,
			ignoreDemolish = true,
			burnable = true,
			affectedByAcid = true,
			affectedByLava = true,
			sfx = "spawn_tree",
			fauna = true,
			canBeDamagedByTornado = true,
			race = "nature",
			kingdom = "nature",
			canBeUpgraded = true,
			upgradeTo = "1wheat",
			orderInLayer = -2,
			shadow = false,
			canBePlacedOnlyOn = List.Of<string>(new string[]
			{
				"field"
			}),
			ruins = null
		});
		this.t.baseStats.health = 10;
		this.t.canBeLivingPlant = false;
		this.clone("1wheat", "0wheat");
		this.t.orderInLayer = 0;
		this.t.upgradeTo = "2wheat";
		this.clone("2wheat", "1wheat");
		this.t.upgradeTo = "3wheat";
		this.clone("3wheat", "2wheat");
		this.t.upgradeTo = "4wheat";
		this.clone("4wheat", "3wheat");
		this.t.upgradeTo = "";
		this.t.canBeUpgraded = false;
		this.t.canBeLivingPlant = true;
		this.t.canBeHarvested = true;
		this.add(new BuildingAsset
		{
			id = "!building",
			fundament = new BuildingFundament(3, 3, 2, 0),
			destroyedSound = "buildingDestroyed",
			burnable = true,
			destroyOnLiquid = true,
			buildRoadTo = true,
			affectedByAcid = true,
			affectedByLava = true,
			canBeDamagedByTornado = true,
			onlyBuildTiles = true,
			checkForCloseBuilding = true
		});
		this.t.baseStats.health = 1500;
		this.clone("!city_building", "!building");
		this.t.cityBuilding = true;
		this.t.canBeAbandoned = true;
		this.clone("!city_colored_building", "!city_building");
		this.t.hasKingdomColor = true;
		this.clone("bonfire", "!city_building");
		this.t.canBeAbandoned = false;
		this.t.priority = 1;
		this.t.type = "bonfire";
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.cost = new ConstructionCost(0, 0, 0, 0);
		this.t.smoke = true;
		this.t.smokeInterval = 2.5f;
		this.t.smokeOffset = new Vector2Int(2, 3);
		this.t.canBeLivingHouse = false;
		this.clone("well", "!city_building");
		this.t.priority = 21;
		this.t.type = "well";
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.cost = new ConstructionCost(0, 20, 1, 5);
		this.t.burnable = false;
		this.t.tech = "building_well";
		this.clone("watch_tower_human", "!city_colored_building");
		this.t.baseStats.health = 3000;
		this.t.baseStats.targets = 1;
		this.t.baseStats.areaOfEffect = 1f;
		this.t.baseStats.damage = 50;
		this.t.baseStats.knockback = 1.4f;
		this.t.priority = 22;
		this.t.type = "watch_tower";
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.cost = new ConstructionCost(0, 20, 1, 5);
		this.t.burnable = false;
		this.t.tech = "building_watch_tower";
		this.t.race = "human";
		this.t.tower = true;
		this.t.tower_projectile = "arrow";
		this.t.tower_projectile_offset = 4f;
		this.t.tower_projectile_amount = 6;
		this.t.buildingPlacement = CityBuildingPlacement.Borders;
		this.clone("watch_tower_elf", "watch_tower_human");
		this.t.race = "elf";
		this.clone("watch_tower_orc", "watch_tower_human");
		this.t.race = "orc";
		this.clone("watch_tower_dwarf", "watch_tower_human");
		this.t.race = "dwarf";
		this.clone("statue", "!city_building");
		this.t.priority = 27;
		this.t.type = "statue";
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.cost = new ConstructionCost(0, 5, 0, 25);
		this.t.burnable = false;
		this.t.tech = "building_statues";
		this.clone("docks_human", "!city_colored_building");
		this.t.race = "human";
		this.t.priority = 20;
		this.t.type = "docks";
		this.t.fundament = new BuildingFundament(2, 2, 4, 0);
		this.t.cost = new ConstructionCost(10, 6, 0, 0);
		this.t.burnable = false;
		this.t.docks = true;
		this.t.canBePlacedOnLiquid = true;
		this.t.destroyOnLiquid = false;
		this.t.buildRoadTo = false;
		this.t.onlyBuildTiles = false;
		this.t.auto_remove_ruin = true;
		this.t.tech = "building_docks";
		this.clone("docks_elf", "docks_human");
		this.t.race = "elf";
		this.clone("docks_dwarf", "docks_human");
		this.t.race = "dwarf";
		this.clone("docks_orc", "docks_human");
		this.t.race = "orc";
		this.clone("mine", "!city_building");
		this.t.priority = 50;
		this.t.type = "mine";
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.cost = new ConstructionCost(0, 0, 0, 15);
		this.t.burnable = false;
		this.t.tech = "building_mine";
		this.clone("barracks_human", "!city_colored_building");
		this.t.priority = 22;
		this.t.burnable = false;
		this.t.type = "barracks";
		this.t.race = "human";
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.cost = new ConstructionCost(0, 5, 2, 15);
		this.t.tech = "building_barracks";
		this.clone("barracks_orc", "barracks_human");
		this.t.race = "orc";
		this.clone("barracks_dwarf", "barracks_human");
		this.t.race = "dwarf";
		this.clone("barracks_elf", "barracks_human");
		this.t.race = "elf";
		this.clone("temple_human", "!city_colored_building");
		this.t.priority = 26;
		this.t.type = "temple";
		this.t.fundament = new BuildingFundament(2, 2, 3, 0);
		this.t.cost = new ConstructionCost(0, 10, 2, 30);
		this.t.burnable = false;
		this.t.race = "human";
		this.t.tech = "building_temple";
		this.clone("temple_orc", "temple_human");
		this.t.race = "orc";
		this.clone("temple_dwarf", "temple_human");
		this.t.race = "dwarf";
		this.clone("temple_elf", "temple_human");
		this.t.race = "elf";
		this.clone("windmill_human", "!city_colored_building");
		this.t.priority = 23;
		this.t.burnable = false;
		this.t.type = "windmill";
		this.t.race = "human";
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.cost = new ConstructionCost(0, 5, 2, 15);
		this.t.tech = "building_windmill";
		this.clone("windmill_orc", "windmill_human");
		this.t.race = "orc";
		this.clone("windmill_dwarf", "windmill_human");
		this.t.race = "dwarf";
		this.clone("windmill_elf", "windmill_human");
		this.t.race = "elf";
		this.clone("tent_human", "!city_colored_building");
		this.t.race = "human";
		this.t.type = "house";
		this.t.cost = new ConstructionCost(0, 0, 0, 0);
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.canBeUpgraded = true;
		this.t.housing = 3;
		this.t.burnable = true;
		this.t.upgradeTo = "house_human";
		this.t.storage = true;
		this.clone("house_human", "!city_colored_building");
		this.t.type = "house";
		this.t.cost = new ConstructionCost(5, 0, 0, 0);
		this.t.housing = 3;
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.canBeUpgraded = true;
		this.t.burnable = true;
		this.t.upgradeTo = "1house_human";
		this.t.storage = true;
		this.t.race = "human";
		this.t.tech = "house_tier_0";
		this.clone("1house_human", "house_human");
		this.t.cost = new ConstructionCost(4, 0, 0, 0);
		this.t.housing = 4;
		this.t.upgradeLevel = 1;
		this.t.upgradeTo = "2house_human";
		this.t.baseStats.health = 150;
		this.t.race = "human";
		this.t.tech = "house_tier_1";
		this.clone("2house_human", "1house_human");
		this.t.cost = new ConstructionCost(0, 5, 0, 0);
		this.t.upgradeLevel = 2;
		this.t.burnable = false;
		this.t.upgradeTo = "3house_human";
		this.t.baseStats.health = 200;
		this.t.race = "human";
		this.t.tech = "house_tier_2";
		this.clone("3house_human", "2house_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.cost = new ConstructionCost(0, 10, 0, 0);
		this.t.housing = 5;
		this.t.upgradeLevel = 3;
		this.t.upgradeTo = "4house_human";
		this.t.baseStats.health = 250;
		this.t.race = "human";
		this.t.tech = "house_tier_3";
		this.clone("4house_human", "3house_human");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.cost = new ConstructionCost(0, 15, 0, 0);
		this.t.housing = 6;
		this.t.upgradeLevel = 4;
		this.t.upgradeTo = "5house_human";
		this.t.baseStats.health = 350;
		this.t.race = "human";
		this.t.tech = "house_tier_4";
		this.clone("5house_human", "4house_human");
		this.t.cost = new ConstructionCost(0, 20, 2, 10);
		this.t.housing = 7;
		this.t.upgradeLevel = 5;
		this.t.canBeUpgraded = false;
		this.t.baseStats.health = 400;
		this.t.race = "human";
		this.t.tech = "house_tier_5";
		this.clone("hall_human", "house_human");
		this.t.priority = 100;
		this.t.type = "hall";
		this.t.cost = new ConstructionCost(10, 0, 0, 10);
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.canBeUpgraded = true;
		this.t.baseStats.health = 200;
		this.t.burnable = true;
		this.t.housing = 5;
		this.t.upgradeTo = "1hall_human";
		this.t.ignoreOtherBuildingsForUpgrade = true;
		this.t.race = "human";
		this.t.tech = "house_tier_1";
		this.clone("1hall_human", "hall_human");
		this.t.cost = new ConstructionCost(0, 10, 1, 20);
		this.t.housing = 8;
		this.t.upgradeLevel = 1;
		this.t.burnable = false;
		this.t.upgradeTo = "2hall_human";
		this.t.baseStats.health = 400;
		this.t.race = "human";
		this.t.tech = "house_tier_3";
		this.clone("2hall_human", "1hall_human");
		this.t.cost = new ConstructionCost(0, 15, 1, 100);
		this.t.housing = 12;
		this.t.upgradeLevel = 2;
		this.t.canBeUpgraded = false;
		this.t.baseStats.health = 600;
		this.t.race = "human";
		this.t.tech = "house_tier_5";
		this.clone("tent_orc", "tent_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgradeTo = "house_orc";
		this.t.race = "orc";
		this.clone("house_orc", "house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "1house_orc";
		this.t.race = "orc";
		this.clone("1house_orc", "1house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "2house_orc";
		this.t.race = "orc";
		this.clone("2house_orc", "2house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "3house_orc";
		this.t.race = "orc";
		this.clone("3house_orc", "3house_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgradeTo = "4house_orc";
		this.t.race = "orc";
		this.clone("4house_orc", "4house_human");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.upgradeTo = "5house_orc";
		this.t.race = "orc";
		this.clone("5house_orc", "5house_human");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.race = "orc";
		this.clone("tent_elf", "tent_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgradeTo = "house_elf";
		this.t.race = "elf";
		this.clone("house_elf", "house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "1house_elf";
		this.t.race = "elf";
		this.clone("1house_elf", "1house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "2house_elf";
		this.t.race = "elf";
		this.clone("2house_elf", "2house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "3house_elf";
		this.t.race = "elf";
		this.clone("3house_elf", "3house_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgradeTo = "4house_elf";
		this.t.race = "elf";
		this.clone("4house_elf", "4house_human");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.upgradeTo = "5house_elf";
		this.t.race = "elf";
		this.clone("5house_elf", "5house_human");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.race = "elf";
		this.clone("tent_dwarf", "tent_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgradeTo = "house_dwarf";
		this.t.race = "dwarf";
		this.clone("house_dwarf", "house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "1house_dwarf";
		this.t.race = "dwarf";
		this.clone("1house_dwarf", "1house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "2house_dwarf";
		this.t.race = "dwarf";
		this.clone("2house_dwarf", "2house_human");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgradeTo = "3house_dwarf";
		this.t.race = "dwarf";
		this.clone("3house_dwarf", "3house_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgradeTo = "4house_dwarf";
		this.t.race = "dwarf";
		this.t.housing = 6;
		this.clone("4house_dwarf", "4house_human");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.upgradeTo = "5house_dwarf";
		this.t.race = "dwarf";
		this.t.housing = 8;
		this.clone("5house_dwarf", "5house_human");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.race = "dwarf";
		this.t.housing = 10;
		this.clone("hall_orc", "hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgradeTo = "1hall_orc";
		this.t.race = "orc";
		this.clone("1hall_orc", "1hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgradeTo = "2hall_orc";
		this.t.race = "orc";
		this.clone("2hall_orc", "2hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.race = "orc";
		this.clone("hall_elf", "hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgradeTo = "1hall_elf";
		this.t.race = "elf";
		this.clone("1hall_elf", "1hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgradeTo = "2hall_elf";
		this.t.race = "elf";
		this.clone("2hall_elf", "2hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.race = "elf";
		this.clone("hall_dwarf", "hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgradeTo = "1hall_dwarf";
		this.t.race = "dwarf";
		this.clone("1hall_dwarf", "1hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgradeTo = "2hall_dwarf";
		this.t.race = "dwarf";
		this.clone("2hall_dwarf", "2hall_human");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.race = "dwarf";
		this.clone("tumor", "!building");
		this.t.transformTilesToTopTiles = "tumor_low";
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.race = "tumor";
		this.t.kingdom = "tumor";
		this.t.canBePlacedOnBlocks = false;
		this.t.canBePlacedOnLiquid = false;
		this.t.ignoreBuildings = true;
		this.t.checkForCloseBuilding = false;
		this.t.canBeLivingHouse = false;
		this.t.spawnUnits = true;
		this.t.spawnUnits_asset = "tumor_monster_animal";
		this.setGrowBiomeAround("tumor", 5, 2, 0.1f, CreepWorkerMovementType.Direction);
		this.t.grow_creep_direction_random_position = true;
		this.t.grow_creep_flash = true;
		this.t.grow_creep_redraw_tile = true;
		this.clone("biomass", "tumor");
		this.t.race = "biomass";
		this.t.kingdom = "biomass";
		this.t.spawnUnits_asset = "bioblob";
		this.t.transformTilesToTopTiles = "biomass_low";
		this.setGrowBiomeAround("biomass", 10, 4, 0.7f, CreepWorkerMovementType.RandomNeighbourAll);
		this.clone("superPumpkin", "tumor");
		this.t.race = "super_pumpkins";
		this.t.kingdom = "super_pumpkins";
		this.t.spawnUnits_asset = "lil_pumpkin";
		this.t.transformTilesToTopTiles = "pumpkin_low";
		this.setGrowBiomeAround("pumpkin", 10, 3, 0.2f, CreepWorkerMovementType.Direction);
		this.t.grow_creep_direction_random_position = true;
		this.t.grow_creep_random_new_direction = true;
		this.t.grow_creep_steps_before_new_direction = 20;
		this.t.grow_creep_flash = true;
		this.t.grow_creep_redraw_tile = true;
		this.clone("cybercore", "tumor");
		this.t.race = "assimilators";
		this.t.kingdom = "assimilators";
		this.t.spawnUnits_asset = "assimilator";
		this.t.transformTilesToTopTiles = "cybertile_low";
		this.setGrowBiomeAround("cybertile", 20, 6, 2f, CreepWorkerMovementType.Direction);
		this.t.grow_creep_steps_before_new_direction = 7;
		this.t.grow_creep_direction_random_position = false;
		this.t.grow_creep_random_new_direction = true;
		this.t.damagedByRain = true;
		this.t.burnable = false;
		this.clone("goldenBrain", "!building");
		this.t.baseStats.health = 1000;
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.race = "goldenBrain";
		this.t.kingdom = "goldenBrain";
		this.t.canBePlacedOnLiquid = false;
		this.t.ignoreBuildings = true;
		this.t.checkForCloseBuilding = false;
		this.t.canBeLivingHouse = true;
		this.t.burnable = false;
		this.clone("corruptedBrain", "!building");
		this.t.baseStats.health = 1000;
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.race = "corruptedBrain";
		this.t.kingdom = "corruptedBrain";
		this.t.canBePlacedOnLiquid = false;
		this.t.ignoreBuildings = true;
		this.t.checkForCloseBuilding = false;
		this.t.canBeLivingHouse = true;
		this.t.burnable = false;
		this.t.tower = true;
		this.t.tower_projectile = "madness";
		this.t.tower_projectile_offset = 6f;
		this.clone("beehive", "!building");
		this.t.baseStats.health = 100;
		this.t.fundament = new BuildingFundament(1, 0, 1, 0);
		this.t.race = "nature";
		this.t.kingdom = "nature";
		this.t.canBePlacedOnLiquid = false;
		this.t.ignoreBuildings = true;
		this.t.checkForCloseBuilding = false;
		this.t.canBeLivingHouse = true;
		this.t.burnable = true;
		this.t.spawnUnits = true;
		this.t.beehive = true;
		this.t.canBePlacedOnlyOn = List.Of<string>(new string[]
		{
			"grass_high",
			"grass_low",
			"soil_low",
			"soil_high",
			"enchanted_high",
			"enchanted_low",
			"savanna_low",
			"savanna_high",
			"jungle_high",
			"jungle_high",
			"mushroom_high",
			"mushroom_high"
		});
		this.t.spawnUnits_asset = "bee";
		this.clone("flameTower", "!building");
		this.t.baseStats.health = 1000;
		this.t.fundament = new BuildingFundament(2, 2, 3, 0);
		this.t.race = "demon";
		this.t.kingdom = "demons";
		this.t.canBePlacedOnLiquid = false;
		this.t.ignoreBuildings = true;
		this.t.checkForCloseBuilding = false;
		this.t.canBeLivingHouse = true;
		this.t.burnable = false;
		this.t.spawnUnits = true;
		this.t.spawnUnits_asset = "demon";
		this.t.tower = true;
		this.t.tower_projectile = "fireball";
		this.t.tower_projectile_offset = 10f;
		this.clone("iceTower", "!building");
		this.t.baseStats.health = 1000;
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.race = "walker";
		this.t.kingdom = "walkers";
		this.t.canBePlacedOnLiquid = false;
		this.t.ignoreBuildings = true;
		this.t.checkForCloseBuilding = false;
		this.t.canBeLivingHouse = true;
		this.t.burnable = false;
		this.t.iceTower = true;
		this.t.spawnUnits = true;
		this.t.spawnUnits_asset = "walker";
		this.clone("volcano", "!building");
		this.t.race = "nature";
		this.t.kingdom = "nature";
		this.t.burnable = false;
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.canBePlacedOnBlocks = true;
		this.t.destroyOnLiquid = false;
		this.t.ignoredByCities = false;
		this.t.affectedByLava = false;
		this.t.canBePlacedOnLiquid = true;
		this.t.smoke = true;
		this.t.smokeInterval = 1.5f;
		this.t.smokeOffset = new Vector2Int(2, 2);
		this.t.spawnPixel = true;
		this.t.spawnDropID = "lava";
		this.t.spawnPixelStartZ = 4f;
		this.t.spawnPixelInterval = 0.1f;
		this.t.canBeDamagedByTornado = false;
		this.t.ignoreBuildings = true;
		this.t.checkForCloseBuilding = false;
		this.t.canBeLivingHouse = false;
		this.clone("geyser", "volcano");
		this.t.smoke = true;
		this.t.smokeInterval = 2.5f;
		this.t.spawnDropID = "rain";
		this.t.spawnPixelStartZ = 3f;
		this.clone("geyserAcid", "geyser");
		this.t.smoke = true;
		this.t.smokeInterval = 3.5f;
		this.t.spawnDropID = "acid";
		this.t.affectedByAcid = false;
		this.t.spawnPixelStartZ = 2f;
		foreach (BuildingAsset pTemplate in this.list)
		{
			this.loadSprites(pTemplate);
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00031314 File Offset: 0x0002F514
	private void setGrowBiomeAround(string pID, int pMaxSteps, int pWorkers, float pStepInterval, CreepWorkerMovementType pMovementType)
	{
		this.t.grow_creep = true;
		this.t.grow_creep_type = pID;
		this.t.grow_creep_type_low = pID + "_low";
		this.t.grow_creep_type_high = pID + "_high";
		this.t.grow_creep_steps_max = pMaxSteps;
		this.t.grow_creep_workers = pWorkers;
		this.t.grow_creep_step_inteval = pStepInterval;
		this.t.grow_creep_movement_type = pMovementType;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00031398 File Offset: 0x0002F598
	private void loadSprites(BuildingAsset pTemplate)
	{
		Sprite[] array = Resources.LoadAll<Sprite>("buildings/" + pTemplate.id);
		pTemplate.sprites = new BuildingSprites();
		foreach (Sprite sprite in array)
		{
			string[] array2 = sprite.name.Split(new char[]
			{
				'_'
			});
			string text = array2[0];
			int num = int.Parse(array2[1]);
			if (array2.Length == 3)
			{
				int.Parse(array2[2]);
			}
			while (pTemplate.sprites.animationData.Count < num + 1)
			{
				pTemplate.sprites.animationData.Add(null);
			}
			if (pTemplate.sprites.animationData[num] == null)
			{
				pTemplate.sprites.animationData[num] = new BuildingAnimationDataNew();
			}
			BuildingAnimationDataNew buildingAnimationDataNew = pTemplate.sprites.animationData[num];
			if (text.Equals("main"))
			{
				buildingAnimationDataNew.list_main.Add(sprite);
				if (buildingAnimationDataNew.list_main.Count > 1)
				{
					buildingAnimationDataNew.animated = true;
				}
			}
			else if (text.Equals("ruin"))
			{
				buildingAnimationDataNew.list_ruins.Add(sprite);
			}
			else if (text.Equals("shadow"))
			{
				buildingAnimationDataNew.list_shadows.Add(sprite);
			}
			else if (text.Equals("construction"))
			{
				pTemplate.sprites.construction = sprite;
			}
			else if (text.Equals("constructionShadow"))
			{
				pTemplate.sprites.construction_shadow = sprite;
			}
			else if (text.Equals("special"))
			{
				buildingAnimationDataNew.list_special.Add(sprite);
			}
			else if (text.Equals("mini"))
			{
				pTemplate.sprites.mapIcon = new BuildingMapIcon(sprite);
			}
		}
		foreach (BuildingAnimationDataNew buildingAnimationDataNew2 in pTemplate.sprites.animationData)
		{
			buildingAnimationDataNew2.main = buildingAnimationDataNew2.list_main.ToArray();
			buildingAnimationDataNew2.ruins = buildingAnimationDataNew2.list_ruins.ToArray();
			buildingAnimationDataNew2.shadows = buildingAnimationDataNew2.list_shadows.ToArray();
			buildingAnimationDataNew2.special = buildingAnimationDataNew2.list_special.ToArray();
		}
	}
}
