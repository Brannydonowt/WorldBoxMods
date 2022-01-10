using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class TopTileLibrary : AssetLibrary<TopTileType>
{
	// Token: 0x060001F0 RID: 496 RVA: 0x00024F14 File Offset: 0x00023114
	public override void init()
	{
		base.init();
		TopTileLibrary.grass_low = this.add(new TopTileType
		{
			drawPixel = true,
			id = "grass_low",
			color = Toolbox.makeColor("#7EAF46"),
			heightMin = 108,
			grass = true,
			ground = true,
			walkMod = 1f,
			growToNearbyTiles = true,
			grassStrength = 5,
			canBeFarmField = true,
			canBuildOn = true,
			canBeSetOnFire = true,
			burnable = true,
			strength = 0,
			fireChance = 0.05f,
			remove_on_freeze = true,
			remove_on_heat = true,
			canGrowBiomeGrass = true
		});
		this.t.creepRankType = TileRank.Low;
		this.t.canBeRemovedWithSickle = true;
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"mushroom",
			"green_herb",
			"flower"
		});
		this.t.setBiome("grass");
		this.t.setDrawLayer(TileZIndexes.grass_low, null);
		this.t.addUnitsToSpawn(new string[]
		{
			"sheep",
			"chick",
			"rabbit",
			"beetle",
			"grasshopper",
			"fly"
		});
		TopTileLibrary.grass_high = this.add(new TopTileType
		{
			cost = 120,
			drawPixel = true,
			id = "grass_high",
			color = Toolbox.makeColor("#5F833C"),
			heightMin = 128,
			trees = true,
			grass = true,
			growToNearbyTiles = true,
			grassStrength = 5,
			canBeSetOnFire = true,
			burnable = true,
			additional_height = new int[]
			{
				15,
				16,
				17,
				14,
				13,
				12,
				11,
				10
			},
			ground = true,
			walkMod = 1f,
			canBuildOn = true,
			canBeFarmField = true,
			fireChance = 0.06f,
			strength = 0,
			remove_on_freeze = true,
			remove_on_heat = true,
			canGrowBiomeGrass = true
		});
		this.t.canBeRemovedWithSickle = true;
		this.t.creepRankType = TileRank.High;
		this.t.addUnitsToSpawn(new string[]
		{
			"wolf",
			"rabbit",
			"beetle",
			"grasshopper",
			"fly",
			"fox"
		});
		this.t.setBiome("grass");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"mushroom",
			"green_herb",
			"flower"
		});
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.grass_high, null);
		TopTileLibrary.savanna_low = this.clone("savanna_low", "grass_low");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"savanna_tree#5",
			"savanna_tree_big#1"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"savanna_plant"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"cat",
			"fly",
			"buffalo",
			"beetle",
			"hyena"
		});
		this.t.color = Toolbox.makeColor("#F0B121");
		this.t.setBiome("savanna");
		this.t.setDrawLayer(TileZIndexes.savanna_low, null);
		this.t.forceUnitSkinSet = "savanna";
		this.t.fireChance = 0.06f;
		TopTileLibrary.savanna_high = this.clone("savanna_high", "grass_high");
		this.t.addUnitsToSpawn(new string[]
		{
			"fly",
			"rhino"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"savanna_tree#5",
			"savanna_tree_big#1"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"savanna_plant"
		});
		this.t.color = Toolbox.makeColor("#CF931B");
		this.t.setBiome("savanna");
		this.t.setDrawLayer(TileZIndexes.savanna_high, null);
		this.t.forceUnitSkinSet = "savanna";
		this.t.fireChance = 0.06f;
		TopTileLibrary.enchanted_low = this.clone("enchanted_low", "grass_low");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"enchanted_tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"flower"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"butterfly",
			"enchanted_fairy"
		});
		this.t.color = Toolbox.makeColor("#8CDC6A");
		this.t.setBiome("enchanted");
		this.t.grassStrength = 6;
		this.t.setDrawLayer(TileZIndexes.enchanted_low, null);
		this.t.stepAction = new TileStepAction(ActionLibrary.giveBlessed);
		this.t.forceUnitSkinSet = "enchanted";
		this.t.fireChance = 0.03f;
		TopTileLibrary.enchanted_high = this.clone("enchanted_high", "grass_high");
		this.t.addUnitsToSpawn(new string[]
		{
			"butterfly",
			"fairy"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"enchanted_tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"flower"
		});
		this.t.color = Toolbox.makeColor("#76B153");
		this.t.setBiome("enchanted");
		this.t.grassStrength = 6;
		this.t.setDrawLayer(TileZIndexes.enchanted_high, null);
		this.t.stepAction = new TileStepAction(ActionLibrary.giveBlessed);
		this.t.forceUnitSkinSet = "enchanted";
		this.t.fireChance = 0.04f;
		TopTileLibrary.mushroom_low = this.clone("mushroom_low", "grass_low");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"mushroom_tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"flower",
			"mushroom"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"frog",
			"sheep"
		});
		this.t.color = Toolbox.makeColor("#677642");
		this.t.setBiome("mushroom");
		this.t.setDrawLayer(TileZIndexes.mushroom_low, null);
		this.t.forceUnitSkinSet = "mushroom";
		this.t.fireChance = 0.03f;
		TopTileLibrary.mushroom_high = this.clone("mushroom_high", "grass_high");
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"flower",
			"mushroom"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"mushroom_tree"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"frog",
			"sheep"
		});
		this.t.color = Toolbox.makeColor("#556338");
		this.t.setBiome("mushroom");
		this.t.setDrawLayer(TileZIndexes.mushroom_high, null);
		this.t.forceUnitSkinSet = "mushroom";
		this.t.fireChance = 0.03f;
		TopTileLibrary.corruption_low = this.clone("corrupted_low", "grass_low");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"corrupted_tree",
			"corrupted_tree_big"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"corrupted_plant"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"jumpy_skull"
		});
		this.t.color = Toolbox.makeColor("#6F556C");
		this.t.setBiome("corrupted");
		this.t.grassStrength = 6;
		this.t.setDrawLayer(TileZIndexes.corruption_low, null);
		this.t.unitDeathAction = new WorldAction(ActionLibrary.spawnGhost);
		this.t.stepAction = new TileStepAction(ActionLibrary.giveCursed);
		this.t.forceUnitSkinSet = "corrupted";
		this.t.fireChance = 0.02f;
		TopTileLibrary.corruption_high = this.clone("corrupted_high", "grass_high");
		this.t.addUnitsToSpawn(new string[]
		{
			"jumpy_skull"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"corrupted_tree",
			"corrupted_tree_big"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"corrupted_plant"
		});
		this.t.color = Toolbox.makeColor("#533F51");
		this.t.setBiome("corrupted");
		this.t.forceUnitSkinSet = "corrupted";
		this.t.grassStrength = 6;
		this.t.setDrawLayer(TileZIndexes.corruption_high, null);
		this.t.unitDeathAction = new WorldAction(ActionLibrary.spawnGhost);
		this.t.stepAction = new TileStepAction(ActionLibrary.giveCursed);
		this.t.fireChance = 0.02f;
		TopTileLibrary.infernal_low = this.clone("infernal_low", "grass_low");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"infernal_tree",
			"infernal_tree_big",
			"infernal_tree_small"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"flame_flower"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"fire_skull"
		});
		this.t.color = Toolbox.makeColor("#9C3626");
		this.t.setBiome("infernal");
		this.t.grassStrength = 6;
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.infernal_low, null);
		TopTileLibrary.infernal_high = this.clone("infernal_high", "grass_high");
		this.t.addUnitsToSpawn(new string[]
		{
			"fire_skull"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"infernal_tree",
			"infernal_tree_big",
			"infernal_tree_small"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"flame_flower"
		});
		this.t.color = Toolbox.makeColor("#68372D");
		this.t.setBiome("infernal");
		this.t.grassStrength = 6;
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.infernal_high, null);
		TopTileLibrary.jungle_low = this.clone("jungle_low", "grass_low");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"jungle_tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"jungle_plant",
			"jungle_flower"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"snake",
			"cat"
		});
		this.t.color = Toolbox.makeColor("#46A052");
		this.t.setBiome("jungle");
		this.t.setDrawLayer(TileZIndexes.jungle_low, null);
		this.t.forceUnitSkinSet = "jungle";
		this.t.fireChance = 0.04f;
		TopTileLibrary.jungle_high = this.clone("jungle_high", "grass_high");
		this.t.addUnitsToSpawn(new string[]
		{
			"monkey",
			"frog"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"jungle_tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"jungle_plant",
			"jungle_flower"
		});
		this.t.color = Toolbox.makeColor("#1F7020");
		this.t.setBiome("jungle");
		this.t.setDrawLayer(TileZIndexes.jungle_high, null);
		this.t.forceUnitSkinSet = "jungle";
		this.t.fireChance = 0.05f;
		TopTileLibrary.swamp_low = this.clone("swamp_low", "grass_low");
		this.t.layerType = TileLayerType.Swamp;
		this.t.swamp = true;
		this.t.liquid = true;
		this.t.canBeRemovedWithSickle = false;
		this.t.canBeRemovedWithBucket = true;
		this.t.addUnitsToSpawn(new string[]
		{
			"crocodile",
			"snake"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"swamp_tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"swamp_plant",
			"swamp_plant_big"
		});
		this.t.color = Toolbox.makeColor("#50816C");
		this.t.setBiome("swamp");
		this.t.setDrawLayer(TileZIndexes.swamp_low, null);
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.forceUnitSkinSet = "swamp";
		this.t.canBeSetOnFire = false;
		this.t.burnable = false;
		this.t.canBuildOn = false;
		TopTileLibrary.swamp_high = this.clone("swamp_high", "grass_high");
		this.t.layerType = TileLayerType.Swamp;
		this.t.swamp = true;
		this.t.liquid = true;
		this.t.canBeRemovedWithSickle = false;
		this.t.canBeRemovedWithBucket = true;
		this.t.addUnitsToSpawn(new string[]
		{
			"frog",
			"fly"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"swamp_tree"
		});
		this.t.color = Toolbox.makeColor("#6AA68B");
		this.t.setBiome("swamp");
		this.t.setDrawLayer(TileZIndexes.swamp_high, null);
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.forceUnitSkinSet = "swamp";
		this.t.canBeSetOnFire = false;
		this.t.burnable = false;
		this.t.canBuildOn = false;
		TopTileLibrary.wasteland_low = this.clone("wasteland_low", "grass_low");
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"wasteland_tree"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"wasteland_flower"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"rat",
			"acid_blob"
		});
		this.t.color = Toolbox.makeColor("#849371");
		this.t.setBiome(null);
		this.t.grass = false;
		this.t.trees = false;
		this.t.wasteland = true;
		this.t.burnable = false;
		this.t.growToNearbyTiles = false;
		this.t.canGrowBiomeGrass = true;
		this.t.grassStrength = 111;
		this.t.setDrawLayer(TileZIndexes.wasteland_low, null);
		TopTileLibrary.wasteland_high = this.clone("wasteland_high", "grass_high");
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"wasteland_flower"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"wasteland_tree"
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"rat",
			"acid_blob"
		});
		this.t.color = Toolbox.makeColor("#6C7759");
		this.t.setBiome(null);
		this.t.grass = false;
		this.t.trees = false;
		this.t.wasteland = true;
		this.t.burnable = false;
		this.t.growToNearbyTiles = false;
		this.t.canGrowBiomeGrass = true;
		this.t.grassStrength = 111;
		this.t.setDrawLayer(TileZIndexes.wasteland_high, null);
		TopTileLibrary.water_bomb = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "water_bomb",
			color = Toolbox.makeColor("#7A00DE"),
			burnable = false,
			explodable = true,
			explodableDelayed = true,
			explodeRange = 9,
			explodableByOcean = true,
			ignoreOceanEdgeRendering = true,
			ground = true,
			walkMod = 1f,
			canBeFilledWithOcean = true,
			strength = 0,
			canBuildOn = true
		});
		this.t.setDrawLayer(TileZIndexes.water_bomb, null);
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.tumor_low = this.add(new TopTileType
		{
			drawPixel = true,
			id = "tumor_low",
			creep = true,
			color = Toolbox.makeColor("#F45193"),
			heightMin = 108,
			ground = true,
			walkMod = 0.5f,
			burnable = true,
			life = true,
			fireChance = 1f,
			strength = 0,
			canBuildOn = false,
			remove_on_freeze = true
		});
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveTumorTrait);
		this.t.setDrawLayer(TileZIndexes.tumor_low, null);
		this.t.setBiome("tumor");
		TopTileLibrary.tumor_high = this.clone("tumor_high", "tumor_low");
		this.t.color = Toolbox.makeColor("#EF267A");
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveTumorTrait);
		this.t.setDrawLayer(TileZIndexes.tumor_high, null);
		this.t.setBiome("tumor");
		TopTileLibrary.biomass_low = this.clone("biomass_low", "tumor_low");
		this.t.color = Toolbox.makeColor("#45C842");
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveMadnessTrait);
		this.t.setDrawLayer(TileZIndexes.biomass_low, null);
		this.t.setBiome("biomass");
		this.t.fireChance = 0.06f;
		TopTileLibrary.biomass_high = this.clone("biomass_high", "tumor_high");
		this.t.color = Toolbox.makeColor("#41A840");
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveMadnessTrait);
		this.t.setDrawLayer(TileZIndexes.biomass_high, null);
		this.t.setBiome("biomass");
		this.t.fireChance = 0.06f;
		TopTileLibrary.pumpkin_low = this.clone("pumpkin_low", "tumor_low");
		this.t.color = Toolbox.makeColor("#8F9339");
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.stepActionChance = 0.2f;
		this.t.setDrawLayer(TileZIndexes.pumpkin_low, null);
		this.t.setBiome("pumpkin");
		this.t.fireChance = 0.06f;
		TopTileLibrary.pumpkin_high = this.clone("pumpkin_high", "tumor_high");
		this.t.color = Toolbox.makeColor("#696C02");
		this.t.stepAction = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.stepActionChance = 0.2f;
		this.t.setDrawLayer(TileZIndexes.pumpkin_high, null);
		this.t.setBiome("pumpkin");
		this.t.fireChance = 0.06f;
		TopTileLibrary.cybertile_low = this.clone("cybertile_low", "tumor_low");
		this.t.life = false;
		this.t.color = Toolbox.makeColor("#9EA6A3");
		this.t.setDrawLayer(TileZIndexes.cybertile_low, null);
		this.t.setBiome("cybertile");
		this.t.burnable = false;
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.cybertile_high = this.clone("cybertile_high", "tumor_high");
		this.t.life = false;
		this.t.color = Toolbox.makeColor("#858886");
		this.t.setDrawLayer(TileZIndexes.cybertile_high, null);
		this.t.setBiome("cybertile");
		this.t.burnable = false;
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.road = this.add(new TopTileType
		{
			cost = 116,
			drawPixel = true,
			id = "road",
			tileName = "road",
			color = Toolbox.makeColor("#C1997C"),
			road = true,
			ground = true,
			walkMod = 1.5f,
			canBeSetOnFire = true,
			canBuildOn = true,
			strength = 0
		});
		this.t.setDrawLayer(TileZIndexes.road, null);
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.fuse = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "fuse",
			color = Toolbox.makeColor("#834C4C"),
			burnable = true,
			terraformAfterFire = true,
			ground = true,
			walkMod = 1f,
			canBuildOn = true,
			strength = 0
		});
		this.t.setDrawLayer(TileZIndexes.fuse, null);
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.field = this.add(new TopTileType
		{
			cost = 115,
			drawPixel = true,
			id = "field",
			color = Toolbox.makeColor("#A8663A"),
			heightMin = 108,
			ground = true,
			walkMod = 1f,
			canBeFarmField = true,
			canBuildOn = false,
			canBeSetOnFire = true,
			burnable = true,
			strength = 0,
			fireChance = 0.4f,
			remove_on_freeze = true,
			remove_on_heat = true
		});
		this.t.setDrawLayer(TileZIndexes.field, null);
		TopTileLibrary.tnt = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "tnt",
			color = Toolbox.makeColor("#A30000"),
			burnable = true,
			explodable = true,
			explodableDelayed = true,
			explodeRange = 10,
			ground = true,
			walkMod = 1f,
			canBuildOn = true,
			strength = 0
		});
		this.t.setDrawLayer(TileZIndexes.tnt, null);
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.fireworks = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "fireworks",
			color = Toolbox.makeColor("#B43DCC"),
			burnable = true,
			terraformAfterFire = true,
			ground = true,
			walkMod = 1f,
			canBuildOn = true,
			strength = 0
		});
		this.t.setDrawLayer(TileZIndexes.fireworks, null);
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.tnt_timed = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "tnt_timed",
			color = Toolbox.makeColor("#7F0000"),
			burnable = true,
			explodable = true,
			explodableTimed = true,
			explodeTimer = 5,
			explodeRange = 8,
			ground = true,
			walkMod = 1f,
			canBuildOn = true,
			strength = 0
		});
		this.t.setDrawLayer(TileZIndexes.tnt_timed, null);
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.landmine = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "landmine",
			color = Toolbox.makeColor("#656565"),
			burnable = true,
			explodable = true,
			explodeRange = 3,
			ground = true,
			walkMod = 1f,
			strength = 0
		});
		this.t.stepAction = new TileStepAction(TileActionLibrary.landmine);
		this.t.stepActionChance = 0.9f;
		this.t.setDrawLayer(TileZIndexes.landmine, null);
		this.t.canBeRemovedWithDemolish = true;
		TopTileLibrary.snow_low = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "snow_low",
			color = Toolbox.makeColor("#BAD5D3"),
			edge_color = Toolbox.makeColor("#F5F7F6"),
			ground = true,
			walkMod = 1f,
			frozen = true,
			strength = 0,
			forceUnitSkinSet = "polar"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"snow_plant"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"pine"
		});
		this.t.canBeFarmField = true;
		this.t.canBuildOn = true;
		this.t.addUnitsToSpawn(new string[]
		{
			"penguin"
		});
		this.t.setDrawLayer(TileZIndexes.snow_low, null);
		TopTileLibrary.snow_sand = this.clone("snow_sand", "snow_low");
		this.t.canBuildOn = true;
		this.t.setDrawLayer(TileZIndexes.snow_sand, null);
		this.t.color = Toolbox.makeColor("#AFF5F1");
		this.t.frozen = true;
		this.t.ground = true;
		TopTileLibrary.ice = this.clone("ice", "snow_low");
		this.t.setDrawLayer(TileZIndexes.ice, null);
		this.t.color = Toolbox.makeColor("#A7D6F4");
		this.t.frozen = true;
		this.t.ground = true;
		this.t.damagedWhenWalked = true;
		TopTileLibrary.snow_high = this.clone("snow_high", "snow_low");
		this.t.canBuildOn = true;
		this.t.addUnitsToSpawn(new string[]
		{
			"bear",
			"wolf"
		});
		this.t.setAutoGrowPlants(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants), new string[]
		{
			"snow_plant"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees), new string[]
		{
			"pine"
		});
		this.t.color = Toolbox.makeColor("#D3E4E3");
		this.t.edge_color = Toolbox.makeColor("#F5F7F6");
		this.t.frozen = true;
		this.t.setDrawLayer(TileZIndexes.snow_high, null);
		TopTileLibrary.snow_hills = this.clone("snow_hills", "snow_low");
		this.t.color = Toolbox.makeColor("#E2EDEC");
		this.t.setDrawLayer(TileZIndexes.snow_hills, null);
		TopTileLibrary.snow_block = this.clone("snow_block", "snow_low");
		this.t.layerType = TileLayerType.Block;
		this.t.block = true;
		this.t.rocks = true;
		this.t.edge_mountains = true;
		this.t.ground = false;
		this.t.setDrawLayer(TileZIndexes.snow_block, null);
		this.loadTileSprites();
		this.addBiome(TopTileLibrary.grass_low, TopTileLibrary.grass_high, 10);
		this.addBiome(TopTileLibrary.savanna_low, TopTileLibrary.savanna_high, 5);
		this.addBiome(TopTileLibrary.jungle_low, TopTileLibrary.jungle_high, 6);
		this.addBiome(TopTileLibrary.enchanted_low, TopTileLibrary.enchanted_high, 3);
		this.addBiome(TopTileLibrary.corruption_low, TopTileLibrary.corruption_high, 1);
		this.addBiome(TopTileLibrary.infernal_low, TopTileLibrary.infernal_high, 1);
		this.addBiome(TopTileLibrary.mushroom_low, TopTileLibrary.mushroom_high, 3);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00026E94 File Offset: 0x00025094
	private void addBiome(TopTileType pLow, TopTileType pHigh, int pAmount)
	{
		BiomeContainer biomeContainer = new BiomeContainer
		{
			low = pLow,
			high = pHigh
		};
		for (int i = 0; i < pAmount; i++)
		{
			TopTileLibrary.pool_biomes.Add(biomeContainer);
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00026ECC File Offset: 0x000250CC
	public override TopTileType add(TopTileType pAsset)
	{
		pAsset.indexID = TileTypeBase.last_indexID++;
		return base.add(pAsset);
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00026EE8 File Offset: 0x000250E8
	private void loadTileSprites()
	{
		foreach (TopTileType pType in this.list)
		{
			this.loadSpritesForTile(pType);
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00026F3C File Offset: 0x0002513C
	private void loadSpritesForTile(TopTileType pType)
	{
		Sprite[] array = Resources.LoadAll<Sprite>("tiles/" + pType.id);
		if (array == null || array.Length == 0)
		{
			return;
		}
		pType.sprites = new TileSprites();
		foreach (Sprite pSprite in array)
		{
			pType.sprites.addVariation(pSprite);
		}
	}

	// Token: 0x0400028A RID: 650
	public static TopTileType snow_sand;

	// Token: 0x0400028B RID: 651
	public static TopTileType snow_low;

	// Token: 0x0400028C RID: 652
	public static TopTileType snow_high;

	// Token: 0x0400028D RID: 653
	public static TopTileType snow_hills;

	// Token: 0x0400028E RID: 654
	public static TopTileType snow_block;

	// Token: 0x0400028F RID: 655
	public static TopTileType ice;

	// Token: 0x04000290 RID: 656
	public static TopTileType landmine;

	// Token: 0x04000291 RID: 657
	public static TopTileType water_bomb;

	// Token: 0x04000292 RID: 658
	public static TopTileType tnt_timed;

	// Token: 0x04000293 RID: 659
	public static TopTileType tnt;

	// Token: 0x04000294 RID: 660
	public static TopTileType fireworks;

	// Token: 0x04000295 RID: 661
	public static TopTileType road;

	// Token: 0x04000296 RID: 662
	public static TopTileType field;

	// Token: 0x04000297 RID: 663
	public static TopTileType fuse;

	// Token: 0x04000298 RID: 664
	public static TopTileType tumor_low;

	// Token: 0x04000299 RID: 665
	public static TopTileType tumor_high;

	// Token: 0x0400029A RID: 666
	public static TopTileType biomass_low;

	// Token: 0x0400029B RID: 667
	public static TopTileType biomass_high;

	// Token: 0x0400029C RID: 668
	public static TopTileType pumpkin_low;

	// Token: 0x0400029D RID: 669
	public static TopTileType pumpkin_high;

	// Token: 0x0400029E RID: 670
	public static TopTileType cybertile_low;

	// Token: 0x0400029F RID: 671
	public static TopTileType cybertile_high;

	// Token: 0x040002A0 RID: 672
	public static TopTileType grass_low;

	// Token: 0x040002A1 RID: 673
	public static TopTileType grass_high;

	// Token: 0x040002A2 RID: 674
	public static TopTileType corruption_low;

	// Token: 0x040002A3 RID: 675
	public static TopTileType corruption_high;

	// Token: 0x040002A4 RID: 676
	public static TopTileType enchanted_low;

	// Token: 0x040002A5 RID: 677
	public static TopTileType enchanted_high;

	// Token: 0x040002A6 RID: 678
	public static TopTileType mushroom_low;

	// Token: 0x040002A7 RID: 679
	public static TopTileType mushroom_high;

	// Token: 0x040002A8 RID: 680
	public static TopTileType savanna_low;

	// Token: 0x040002A9 RID: 681
	public static TopTileType savanna_high;

	// Token: 0x040002AA RID: 682
	public static TopTileType jungle_low;

	// Token: 0x040002AB RID: 683
	public static TopTileType jungle_high;

	// Token: 0x040002AC RID: 684
	public static TopTileType infernal_low;

	// Token: 0x040002AD RID: 685
	public static TopTileType infernal_high;

	// Token: 0x040002AE RID: 686
	public static TopTileType swamp_low;

	// Token: 0x040002AF RID: 687
	public static TopTileType swamp_high;

	// Token: 0x040002B0 RID: 688
	public static TopTileType wasteland_low;

	// Token: 0x040002B1 RID: 689
	public static TopTileType wasteland_high;

	// Token: 0x040002B2 RID: 690
	public static List<BiomeContainer> pool_biomes = new List<BiomeContainer>();
}
