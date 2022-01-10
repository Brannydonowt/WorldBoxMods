using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;

// Token: 0x02000028 RID: 40
[ObfuscateLiterals]
public class DisasterLibrary : AssetLibrary<DisasterAsset>
{
	// Token: 0x060000FE RID: 254 RVA: 0x00013EB8 File Offset: 0x000120B8
	public override void init()
	{
		base.init();
		this.add(new DisasterAsset
		{
			id = "rain_cloud",
			rate = 40,
			chance = 0.5f,
			world_log = string.Empty,
			type = DisasterType.Nature
		});
		this.t.action = new DisasterAction(this.spawnRainCloud);
		this.add(new DisasterAsset
		{
			id = "tornado",
			rate = 5,
			chance = 0.5f,
			world_log = "worldlog_disaster_tornado",
			world_log_icon = "iconTornado",
			min_world_population = 100,
			type = DisasterType.Nature
		});
		this.t.action = new DisasterAction(this.spawnTornado);
		this.add(new DisasterAsset
		{
			id = "small_meteorite",
			rate = 5,
			chance = 0.5f,
			world_log = "worldlog_disaster_meteorite",
			world_log_icon = "iconMeteorite",
			min_world_population = 400,
			premium_only = true,
			type = DisasterType.Nature
		});
		this.t.action = new DisasterAction(this.spawnMeteorite);
		this.add(new DisasterAsset
		{
			id = "small_earthquake",
			rate = 3,
			chance = 0.4f,
			world_log = "worldlog_disaster_earthquake",
			world_log_icon = "iconEarthquake",
			min_world_population = 400,
			type = DisasterType.Nature
		});
		this.t.action = new DisasterAction(this.spawnSmallEarthquake);
		this.add(new DisasterAsset
		{
			id = "hellspawn",
			rate = 2,
			chance = 0.9f,
			world_log = "worldlog_disaster_hellspawn",
			world_log_icon = "iconDemon",
			min_world_population = 300,
			premium_only = true
		});
		this.t.action = new DisasterAction(this.spawnHellSpawn);
		this.add(new DisasterAsset
		{
			id = "wild_mage",
			rate = 1,
			chance = 0.8f,
			world_log = "worldlog_disaster_evil_mage",
			world_log_icon = "iconEvilMage",
			min_world_population = 400,
			premium_only = true
		});
		this.t.action = new DisasterAction(this.spawnEvilMage);
		this.add(new DisasterAsset
		{
			id = "underground_necromancer",
			rate = 2,
			chance = 0.9f,
			world_log = "worldlog_disaster_underground_necromancer",
			world_log_icon = "iconNecromancer",
			min_world_population = 200,
			premium_only = true
		});
		this.t.action = new DisasterAction(this.spawnNecromancer);
		this.add(new DisasterAsset
		{
			id = "mad_thoughts",
			rate = 2,
			chance = 0.7f,
			world_log = "worldlog_disaster_mad_thoughts",
			world_log_icon = "iconMadness",
			min_world_population = 150
		});
		this.t.action = new DisasterAction(this.spawnMadThought);
		this.add(new DisasterAsset
		{
			id = "greg_abominations",
			rate = 1,
			chance = 0.5f,
			world_log = "worldlog_disaster_greg_abominations",
			world_log_icon = "iconGreg",
			min_world_population = 2000
		});
		this.t.action = new DisasterAction(this.spawnGreg);
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00014254 File Offset: 0x00012454
	public DisasterAsset getRandomAssetFromPool()
	{
		if (this._random_pool == null || this._random_pool.Count == 0)
		{
			this._random_pool = new List<DisasterAsset>();
			for (int i = 0; i < this.list.Count; i++)
			{
				DisasterAsset disasterAsset = this.list[i];
				for (int j = 0; j < disasterAsset.rate; j++)
				{
					this._random_pool.Add(disasterAsset);
				}
			}
		}
		DisasterAsset random = this._random_pool.GetRandom<DisasterAsset>();
		if (random.type == DisasterType.Nature)
		{
			if (!MapBox.instance.worldLaws.world_law_disasters_nature.boolVal)
			{
				return null;
			}
		}
		else if (random.type == DisasterType.Other && !MapBox.instance.worldLaws.world_law_disasters_other.boolVal)
		{
			return null;
		}
		if (random.min_world_population <= MapBox.instance.getCivWorldPopulation())
		{
			return random;
		}
		return null;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00014324 File Offset: 0x00012524
	public void spawnMadThought(DisasterAsset pAsset)
	{
		City random = MapBox.instance.citiesList.GetRandom<City>();
		if (random == null)
		{
			return;
		}
		if (random.getPopulationUnits() < 50)
		{
			return;
		}
		if (random.getTile() == null)
		{
			return;
		}
		List<Actor> list = new List<Actor>();
		List<Actor> simpleList = random.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			if (Toolbox.randomChance(0.2f))
			{
				simpleList.ShuffleOne(i);
				list.Add(simpleList[i]);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			list[j].addTrait("madness");
		}
		WorldLog.logDisaster(pAsset, random.getTile(), null, random, null);
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000143DC File Offset: 0x000125DC
	public void spawnGreg(DisasterAsset pAsset)
	{
		if (!DebugConfig.isOn(DebugOption.Greg))
		{
			return;
		}
		if (AssetManager.raceLibrary.get("greg").units.Count >= 1)
		{
			return;
		}
		List<City> citiesList = MapBox.instance.citiesList;
		City city = (citiesList != null) ? citiesList.GetRandom<City>() : null;
		Building building = (city != null) ? city.getBuildingType("mine", true, false) : null;
		if (building == null)
		{
			return;
		}
		WorldTile currentTile = building.currentTile;
		Actor actor = MapBox.instance.createNewUnit("greg", currentTile, "", 0f, null);
		WorldLog.logDisaster(pAsset, currentTile, actor.data.firstName, city, actor);
		int num = Toolbox.randomInt(5, 25);
		for (int i = 0; i < num; i++)
		{
			MapBox.instance.createNewUnit("greg", currentTile.region.tiles.GetRandom<WorldTile>(), "", 0f, null);
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x000144C4 File Offset: 0x000126C4
	public void spawnNecromancer(DisasterAsset pAsset)
	{
		if (AssetManager.raceLibrary.get("necromancer").units.Count >= 1)
		{
			return;
		}
		City random = MapBox.instance.citiesList.GetRandom<City>();
		Building building = (random != null) ? random.getBuildingType("mine", true, false) : null;
		if (building == null)
		{
			return;
		}
		WorldTile currentTile = building.currentTile;
		Actor actor = MapBox.instance.createNewUnit("necromancer", currentTile, "", 0f, null);
		WorldLog.logDisaster(pAsset, currentTile, actor.data.firstName, random, actor);
		int num = Toolbox.randomInt(5, 25);
		for (int i = 0; i < num; i++)
		{
			MapBox.instance.createNewUnit("skeleton", currentTile.region.tiles.GetRandom<WorldTile>(), "", 0f, null);
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0001459C File Offset: 0x0001279C
	public void spawnEvilMage(DisasterAsset pAsset)
	{
		if (AssetManager.raceLibrary.get("evilMage").units.Count >= 1)
		{
			return;
		}
		TileIsland randomIslandGround = MapBox.instance.islandsCalculator.getRandomIslandGround(true);
		if (randomIslandGround == null)
		{
			return;
		}
		WorldTile randomTile = randomIslandGround.getRandomTile();
		Actor actor = MapBox.instance.createNewUnit("evilMage", randomTile, "", 0f, null);
		WorldLog.logDisaster(pAsset, randomTile, actor.data.firstName, null, actor);
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00014614 File Offset: 0x00012814
	public void spawnSmallEarthquake(DisasterAsset pAsset)
	{
		WorldTile random = MapBox.instance.tilesList.GetRandom<WorldTile>();
		MapBox.instance.earthquakeManager.startQuake(random, EarthquakeType.SmallDisaster);
		WorldLog.logDisaster(pAsset, random, null, null, null);
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0001464C File Offset: 0x0001284C
	public void spawnHellSpawn(DisasterAsset pAsset)
	{
		if (AssetManager.raceLibrary.get("demon").units.Count > 20)
		{
			return;
		}
		TileIsland randomIslandGround = MapBox.instance.islandsCalculator.getRandomIslandGround(true);
		if (randomIslandGround == null)
		{
			return;
		}
		WorldTile randomTile = randomIslandGround.getRandomTile();
		GodPower godPower = AssetManager.powers.get("demons");
		if (godPower.showSpawnEffect != string.Empty)
		{
			MapBox.instance.stackEffects.startSpawnEffect(randomTile, godPower.showSpawnEffect);
		}
		MapBox.instance.createNewUnit(godPower.actorStatsId, randomTile, "", godPower.actorSpawnHeight, null);
		int num = Toolbox.randomInt(2, 5);
		for (int i = 0; i < num; i++)
		{
			WorldTile random = randomTile.neighbours.GetRandom<WorldTile>();
			MapBox.instance.createNewUnit(godPower.actorStatsId, random, "", godPower.actorSpawnHeight, null);
		}
		WorldLog.logDisaster(pAsset, randomTile, null, null, null);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0001473C File Offset: 0x0001293C
	public void spawnRainCloud(DisasterAsset pAsset)
	{
		GodPower godPower = AssetManager.powers.get("cloudRain");
		WorldTile random = MapBox.instance.tilesList.GetRandom<WorldTile>();
		MapBox.instance.cloudController.spawnCloud(random.posV3, godPower.id);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00014784 File Offset: 0x00012984
	public void spawnTornado(DisasterAsset pAsset)
	{
		GodPower godPower = AssetManager.powers.get("tornado");
		if (godPower.spawnSound != "")
		{
			Sfx.play(godPower.spawnSound, true, -1f, -1f);
		}
		WorldTile random = MapBox.instance.tilesList.GetRandom<WorldTile>();
		MapBox.instance.createNewUnit(godPower.actorStatsId, random, "", godPower.actorSpawnHeight, null);
		WorldLog.logDisaster(pAsset, random, null, null, null);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00014804 File Offset: 0x00012A04
	public void spawnMeteorite(DisasterAsset pAsset)
	{
		WorldTile random = MapBox.instance.tilesList.GetRandom<WorldTile>();
		MapBox.instance.spawnMeteorite(random);
		WorldLog.logDisaster(pAsset, random, null, null, null);
	}

	// Token: 0x040000EE RID: 238
	private List<DisasterAsset> _random_pool;
}
