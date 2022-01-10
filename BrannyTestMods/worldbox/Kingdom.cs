using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x0200013E RID: 318
[Serializable]
public class Kingdom
{
	// Token: 0x06000759 RID: 1881 RVA: 0x00053AE6 File Offset: 0x00051CE6
	public void createHidden()
	{
		this.kingdomColor = this.asset.default_kingdom_color;
		KingdomColor kingdomColor = this.kingdomColor;
		if (kingdomColor == null)
		{
			return;
		}
		kingdomColor.initColor();
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00053B0C File Offset: 0x00051D0C
	public void createAI()
	{
		this.ai = new AiSystemKingdom(this);
		this.ai.nextJobDelegate = new GetNextJobID(this.getNextJob);
		this.ai.jobs_library = AssetManager.job_kingdom;
		this.ai.task_library = AssetManager.tasks_kingdom;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00053B5C File Offset: 0x00051D5C
	public string getNextJob()
	{
		return "kingdom";
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00053B63 File Offset: 0x00051D63
	public bool isCiv()
	{
		return this.asset.civ;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00053B70 File Offset: 0x00051D70
	public bool isNature()
	{
		return this.asset.nature;
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00053B7D File Offset: 0x00051D7D
	public bool isNomads()
	{
		return this.asset.nomads;
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00053B8C File Offset: 0x00051D8C
	public void save()
	{
		this.saved_allies.Clear();
		this.saved_enemies.Clear();
		foreach (Kingdom kingdom in this.allies.Keys)
		{
			if (kingdom.isCiv() && kingdom != this)
			{
				this.saved_allies.Add(kingdom.id);
			}
		}
		foreach (Kingdom kingdom2 in this.enemies.Keys)
		{
			if (kingdom2.isCiv() && kingdom2 != this)
			{
				this.saved_enemies.Add(kingdom2.id);
			}
		}
		this.count_cities = this.cities.Count;
		this.count_units = this.units.Count;
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00053C90 File Offset: 0x00051E90
	internal bool isEnemy(Kingdom pKingdom)
	{
		if (pKingdom == null)
		{
			return true;
		}
		if (this.isCiv() && pKingdom.isCiv())
		{
			return pKingdom != this && !this.allies.ContainsKey(pKingdom);
		}
		return this.asset.isFoe(pKingdom.asset);
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00053CE0 File Offset: 0x00051EE0
	internal void createCiv(Race pRace, string pID = null)
	{
		this.race = pRace;
		this.raceID = pRace.id;
		this.asset = AssetManager.kingdoms.get(this.raceID);
		if (string.IsNullOrEmpty(pID))
		{
			this.id = MapBox.instance.mapStats.getNextId("kingdom");
		}
		this.createColors(-1);
		Kingdom.kingdoms++;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00053D4B File Offset: 0x00051F4B
	internal void update(float pElapsed)
	{
		this.buildings.checkAddRemove();
		this.units.checkAddRemove();
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00053D64 File Offset: 0x00051F64
	internal void updateCiv(float pElapsed)
	{
		if (this.cities.Count == 0)
		{
			this.timer_no_city += pElapsed;
			if (this.units.Count == 0)
			{
				this.timer_no_city = 1000f;
			}
		}
		else
		{
			this.timer_no_city = 0f;
		}
		if (this.timer_attack_target > 0f)
		{
			this.timer_attack_target -= pElapsed;
		}
		if (this.timer_settle_target > 0f)
		{
			this.timer_settle_target -= pElapsed;
		}
		if (this.timer_loyalty > 0f)
		{
			this.timer_loyalty -= pElapsed;
		}
		if (this.timer_new_king > 0f)
		{
			this.timer_new_king -= pElapsed;
		}
		if (this.timer_event > 0f)
		{
			this.checkRelationships();
			this.timer_event -= pElapsed;
			if (this.timer_event <= 0f && this.eventState == KingdomEventState.Wait)
			{
				this.clearTarget();
				this.eventState = KingdomEventState.Idle;
				this.timer_event = 200f + Toolbox.randomFloat(0f, 100f);
			}
		}
		if (this.ai != null)
		{
			if (this.timer_action > 0f)
			{
				this.timer_action -= pElapsed;
				return;
			}
			this.ai.update();
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00053EAC File Offset: 0x000520AC
	private void checkRelationships()
	{
		Kingdom._temp_kingdoms.Clear();
		foreach (Kingdom kingdom in this.civs_allies.Keys)
		{
			if (kingdom.cities.Count == 0 && kingdom.getPopulationTotal() == 0)
			{
				Kingdom._temp_kingdoms.Add(kingdom);
			}
		}
		foreach (Kingdom kingdom2 in this.civs_enemies.Keys)
		{
			if (kingdom2.cities.Count == 0 && kingdom2.getPopulationTotal() == 0)
			{
				Kingdom._temp_kingdoms.Add(kingdom2);
			}
		}
		foreach (Kingdom pKingdom in Kingdom._temp_kingdoms)
		{
			MapBox.instance.kingdoms.destroyKingdom(pKingdom);
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00053FD4 File Offset: 0x000521D4
	internal void addUnit(Actor pUnit)
	{
		if (this.units.Contains(pUnit))
		{
			return;
		}
		this.units.Add(pUnit);
		if (this.race == null && pUnit.race.civilization)
		{
			this.race = pUnit.race;
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00054012 File Offset: 0x00052212
	internal void removeUnit(Actor pUnit)
	{
		this.units.Remove(pUnit);
		if (pUnit == this.king)
		{
			this.removeKing();
		}
		this.checkKingdomDead();
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0005403A File Offset: 0x0005223A
	public void setKing(Actor pActor)
	{
		this.king = pActor;
		this.king.setProfession(UnitProfession.King);
		this.kingID = this.king.data.actorID;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00054065 File Offset: 0x00052265
	internal void removeKing()
	{
		if (this.king != null)
		{
			this.king.setProfession(UnitProfession.Unit);
		}
		this.king = null;
		this.kingID = null;
		this.timer_new_king = Toolbox.randomFloat(5f, 20f);
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000540A4 File Offset: 0x000522A4
	internal void removeCity(City pCity)
	{
		if (pCity.isCapitalCity())
		{
			this.capital = null;
			this.capitalID = null;
		}
		this.cities.Remove(pCity);
		this.checkKingdomDead();
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000540CF File Offset: 0x000522CF
	public void capturedFrom(Kingdom pKingdom)
	{
		MapBox.instance.kingdoms.diplomacyManager.getRelation(this, pKingdom);
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x000540E8 File Offset: 0x000522E8
	internal void addCity(City pCity)
	{
		this.cities.Add(pCity);
		if (this.name == null)
		{
			this.name = NameGenerator.getName(this.race.name_template_kingdom, ActorGender.Male);
		}
		if (pCity == this.target_city)
		{
			this.timer_attack_target = 0f;
		}
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00054139 File Offset: 0x00052339
	internal void generateMotto()
	{
		this.motto = NameGenerator.getName("kingdom_mottos", ActorGender.Male);
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0005414C File Offset: 0x0005234C
	internal void load1()
	{
		if (this.bannerSeed == -1)
		{
			this.bannerSeed = Toolbox.randomInt(0, 1000000);
		}
		this.createColors(this.colorID);
		if (this.race == null && !string.IsNullOrEmpty(this.raceID))
		{
			this.race = AssetManager.raceLibrary.get(this.raceID);
			this.asset = AssetManager.kingdoms.get(this.raceID);
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x000541C0 File Offset: 0x000523C0
	internal void load2()
	{
		Kingdom.kingdoms++;
		this.capital = MapBox.instance.getCityByID(this.capitalID);
		if (this.capital != null)
		{
			this.location = this.capital.cityCenter;
		}
		this.king = MapBox.instance.getActorByID(this.kingID);
		if (this.king != null)
		{
			this.king.setProfession(UnitProfession.King);
		}
		for (int i = 0; i < this.saved_allies.Count; i++)
		{
			string pID = this.saved_allies[i];
			Kingdom kingdomByID = MapBox.instance.kingdoms.getKingdomByID(pID);
			if (kingdomByID != null)
			{
				MapBox.instance.kingdoms.setDiplomacyState(this, kingdomByID, DiplomacyState.Ally);
			}
		}
		for (int j = 0; j < this.saved_enemies.Count; j++)
		{
			string pID2 = this.saved_enemies[j];
			Kingdom kingdomByID2 = MapBox.instance.kingdoms.getKingdomByID(pID2);
			if (kingdomByID2 != null)
			{
				MapBox.instance.kingdoms.setDiplomacyState(this, kingdomByID2, DiplomacyState.War);
			}
		}
		if (this.isCiv())
		{
			this.createAI();
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x000542E4 File Offset: 0x000524E4
	private void createColors(int pID = -1)
	{
		KingdomColors.getContainer(this.raceID).prepare(this);
		this.colorID = pID;
		if (this.colorID == -1)
		{
			this.colorID = KingdomColors.getContainer(this.raceID).curColor;
		}
		this.kingdomColor = KingdomColors.getColor(this.raceID, this.colorID);
		if (this.bannerSeed == -1)
		{
			this.bannerSeed = Toolbox.randomInt(0, 1000000);
		}
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0005435C File Offset: 0x0005255C
	internal void findCityTarget()
	{
		if (this.target_city != null && this.target_city.alive && this.target_city.kingdom.isEnemy(this))
		{
			return;
		}
		if (this.capital == null)
		{
			return;
		}
		this.target_city = null;
		float num = 0f;
		this.target_city = null;
		for (int i = 0; i < this.target_kingdom.cities.Count; i++)
		{
			City city = this.target_kingdom.cities[i];
			float num2 = Toolbox.DistVec3(this.capital.cityCenter, city.cityCenter);
			if (city.zones.Count != 0 && (this.target_city == null || num2 < num))
			{
				this.target_city = city;
				num = num2;
			}
		}
		if (this.target_city == null)
		{
			this.clearTarget();
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0005443B File Offset: 0x0005263B
	internal void checkKingdomDead()
	{
		if (!this.isCiv())
		{
			return;
		}
		if (this.getPopulationTotal() == 0 && this.cities.Count == 0)
		{
			MapBox.instance.kingdoms.destroyKingdom(this);
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0005446C File Offset: 0x0005266C
	public static float distanceBetweenKingdom(Kingdom kingdom, Kingdom pTarget)
	{
		float num = -1f;
		for (int i = 0; i < kingdom.cities.Count; i++)
		{
			City city = kingdom.cities[i];
			for (int j = 0; j < pTarget.cities.Count; j++)
			{
				City city2 = pTarget.cities[j];
				float num2 = Toolbox.DistVec3(city.cityCenter, city2.cityCenter);
				if (num == -1f || num2 < num)
				{
					num = num2;
				}
			}
		}
		return num;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x000544F0 File Offset: 0x000526F0
	public void clear()
	{
		this.buildings.Clear();
		this.units.Clear();
		this.allies.Clear();
		this.enemies.Clear();
		this.civs_allies.Clear();
		this.civs_enemies.Clear();
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00054540 File Offset: 0x00052740
	public void setDiplomacyState(Kingdom pTarget, DiplomacyState pState)
	{
		switch (pState)
		{
		case DiplomacyState.War:
			this.enemies[pTarget] = true;
			if (this.isCiv() && pTarget.isCiv())
			{
				this.civs_enemies[pTarget] = true;
			}
			break;
		case DiplomacyState.Ally:
			this.allies[pTarget] = true;
			if (this.isCiv() && pTarget.isCiv())
			{
				this.civs_allies[pTarget] = true;
			}
			break;
		case DiplomacyState.Clear:
			this.allies.Remove(pTarget);
			this.enemies.Remove(pTarget);
			if (this.isCiv() && pTarget.isCiv())
			{
				this.civs_allies.Remove(pTarget);
				this.civs_enemies.Remove(pTarget);
			}
			break;
		}
		if (pState != DiplomacyState.Ally)
		{
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00054609 File Offset: 0x00052809
	public bool hasEnemies()
	{
		return this.civs_enemies.Count > 0;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0005461C File Offset: 0x0005281C
	public void checkTarget()
	{
		if (this.target_kingdom != null && !this.target_kingdom.isEnemy(this))
		{
			this.clearTarget();
			return;
		}
		if (this.target_city == null || !this.target_city.alive || this.target_city.kingdom == this || !this.target_city.kingdom.isEnemy(this))
		{
			this.target_city = null;
		}
		if (this.target_city == null)
		{
			this.findCityTarget();
		}
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0005469D File Offset: 0x0005289D
	internal void clearTarget()
	{
		this.target_city = null;
		this.target_kingdom = null;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x000546B0 File Offset: 0x000528B0
	public void makeSurvivorsToNomads()
	{
		if (this.units.Count == 0)
		{
			return;
		}
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (actor.data.alive)
			{
				if (actor.stats.race == "boat")
				{
					actor.killHimself(true, AttackType.Other, true, true);
				}
				else
				{
					actor.cancelAllBeh(null);
					actor.removeFromCity();
					actor.removeFromGroup();
					actor.setKingdom(MapBox.instance.kingdoms.dict_hidden["nomads_" + actor.stats.race]);
				}
			}
		}
		this.units.Clear();
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00054770 File Offset: 0x00052970
	public void clearKingData()
	{
		this.king = null;
		if (this.lastKingRuledFor > 4)
		{
			this.check_for_independence = true;
		}
		this.lastKingRuledFor = 0;
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00054790 File Offset: 0x00052990
	public void updateAge()
	{
		this.age++;
		if (this.king != null)
		{
			this.lastKingRuledFor++;
		}
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x000547BC File Offset: 0x000529BC
	public void newCityBuiltEvent(City pNewCity)
	{
		for (int i = 0; i < this.cities.Count; i++)
		{
			this.cities[i].forceSettleTarget(pNewCity);
		}
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x000547F4 File Offset: 0x000529F4
	public int getPopulationTotal()
	{
		int num = this.units.Count;
		for (int i = 0; i < this.cities.Count; i++)
		{
			City city = this.cities[i];
			num += city.getPopulationPopPoints();
		}
		return num;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0005483C File Offset: 0x00052A3C
	public int countArmy()
	{
		int num = 0;
		for (int i = 0; i < this.cities.Count; i++)
		{
			City city = this.cities[i];
			num += city.getArmy();
		}
		return num;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00054878 File Offset: 0x00052A78
	public int countArmyMax()
	{
		int num = 0;
		for (int i = 0; i < this.cities.Count; i++)
		{
			City city = this.cities[i];
			num += city.getArmyMaxCity();
		}
		return num;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x000548B4 File Offset: 0x00052AB4
	public int getMaxCities()
	{
		int num = this.race.civ_baseCities;
		if (this.king != null)
		{
			num += this.king.curStats.cities;
		}
		Culture culture = this.getCulture();
		if (culture != null)
		{
			num += (int)culture.stats.bonus_max_cities.value;
		}
		return num;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00054910 File Offset: 0x00052B10
	public bool diceAgressionSuccess()
	{
		return !(this.king == null) && (this.cities.Count < this.getMaxCities() || (this.cities.Count >= this.getMaxCities() && Toolbox.randomChance(this.king.curStats.personality_aggression)));
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0005496F File Offset: 0x00052B6F
	public Culture getCulture()
	{
		if (string.IsNullOrEmpty(this.cultureID))
		{
			return null;
		}
		return MapBox.instance.cultures.get(this.cultureID);
	}

	// Token: 0x040009C6 RID: 2502
	[NonSerialized]
	public int hashcode;

	// Token: 0x040009C7 RID: 2503
	[NonSerialized]
	public KingdomAsset asset;

	// Token: 0x040009C8 RID: 2504
	public string id;

	// Token: 0x040009C9 RID: 2505
	public string name;

	// Token: 0x040009CA RID: 2506
	public string cultureID;

	// Token: 0x040009CB RID: 2507
	[DefaultValue("")]
	public string kingID = "";

	// Token: 0x040009CC RID: 2508
	[DefaultValue("")]
	public string capitalID = "";

	// Token: 0x040009CD RID: 2509
	[DefaultValue("")]
	public string raceID = "";

	// Token: 0x040009CE RID: 2510
	public int bannerSeed = -1;

	// Token: 0x040009CF RID: 2511
	public int age;

	// Token: 0x040009D0 RID: 2512
	public string motto = string.Empty;

	// Token: 0x040009D1 RID: 2513
	public float timer_loyalty = 100f;

	// Token: 0x040009D2 RID: 2514
	internal float timer_no_city;

	// Token: 0x040009D3 RID: 2515
	public float timer_settle_target = 100f;

	// Token: 0x040009D4 RID: 2516
	public float timer_event = 100f;

	// Token: 0x040009D5 RID: 2517
	public float timer_attack_target = 100f;

	// Token: 0x040009D6 RID: 2518
	public float timer_new_king = 10f;

	// Token: 0x040009D7 RID: 2519
	public int colorID = -1;

	// Token: 0x040009D8 RID: 2520
	public KingdomEventState eventState;

	// Token: 0x040009D9 RID: 2521
	public int deaths;

	// Token: 0x040009DA RID: 2522
	public int born;

	// Token: 0x040009DB RID: 2523
	public List<string> saved_allies = new List<string>();

	// Token: 0x040009DC RID: 2524
	public List<string> saved_enemies = new List<string>();

	// Token: 0x040009DD RID: 2525
	public int lastKingRuledFor;

	// Token: 0x040009DE RID: 2526
	public bool check_for_independence;

	// Token: 0x040009DF RID: 2527
	[NonSerialized]
	public float timer_action;

	// Token: 0x040009E0 RID: 2528
	[NonSerialized]
	internal KingdomColor kingdomColor;

	// Token: 0x040009E1 RID: 2529
	[NonSerialized]
	internal Race race;

	// Token: 0x040009E2 RID: 2530
	[NonSerialized]
	public Actor king;

	// Token: 0x040009E3 RID: 2531
	[NonSerialized]
	public List<City> cities = new List<City>();

	// Token: 0x040009E4 RID: 2532
	[NonSerialized]
	public ActorContainer units = new ActorContainer();

	// Token: 0x040009E5 RID: 2533
	[NonSerialized]
	public BuildingContainer buildings = new BuildingContainer();

	// Token: 0x040009E6 RID: 2534
	[NonSerialized]
	public City capital;

	// Token: 0x040009E7 RID: 2535
	public bool alive = true;

	// Token: 0x040009E8 RID: 2536
	public int count_cities;

	// Token: 0x040009E9 RID: 2537
	public int count_units;

	// Token: 0x040009EA RID: 2538
	[NonSerialized]
	public Dictionary<Kingdom, bool> allies = new Dictionary<Kingdom, bool>();

	// Token: 0x040009EB RID: 2539
	[NonSerialized]
	public Dictionary<Kingdom, bool> civs_allies = new Dictionary<Kingdom, bool>();

	// Token: 0x040009EC RID: 2540
	[NonSerialized]
	public Dictionary<Kingdom, bool> enemies = new Dictionary<Kingdom, bool>();

	// Token: 0x040009ED RID: 2541
	[NonSerialized]
	public Dictionary<Kingdom, bool> civs_enemies = new Dictionary<Kingdom, bool>();

	// Token: 0x040009EE RID: 2542
	[NonSerialized]
	public int power;

	// Token: 0x040009EF RID: 2543
	[NonSerialized]
	public static int kingdoms = 0;

	// Token: 0x040009F0 RID: 2544
	[NonSerialized]
	public Kingdom target_kingdom;

	// Token: 0x040009F1 RID: 2545
	[NonSerialized]
	public City target_city;

	// Token: 0x040009F2 RID: 2546
	[NonSerialized]
	public TileZone settlerTarget;

	// Token: 0x040009F3 RID: 2547
	[NonSerialized]
	public AiSystemKingdom ai;

	// Token: 0x040009F4 RID: 2548
	[NonSerialized]
	public Vector3 location;

	// Token: 0x040009F5 RID: 2549
	private static List<Kingdom> _temp_kingdoms = new List<Kingdom>();
}
