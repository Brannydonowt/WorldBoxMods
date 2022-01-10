using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class City : BaseMapObject
{
	// Token: 0x06000506 RID: 1286 RVA: 0x000415EC File Offset: 0x0003F7EC
	private void correctBuildingDicts()
	{
		foreach (BuildingContainer buildingContainer in this.buildings_dict_type.Values)
		{
			buildingContainer.checkAddRemove();
			List<Building> simpleList = buildingContainer.getSimpleList();
			for (int i = 0; i < simpleList.Count; i++)
			{
				Building building = simpleList[i];
				if (building.data.state != BuildingState.CivKingdom)
				{
					buildingContainer.Remove(building);
				}
			}
			buildingContainer.checkAddRemove();
		}
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00041684 File Offset: 0x0003F884
	public WorldTile getTile()
	{
		return this._cityTile;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0004168C File Offset: 0x0003F88C
	internal void recalculateCityTile()
	{
		this._cityTile = null;
		this.correctBuildingDicts();
		Building building = this.getBuildingType("bonfire", true, false);
		if (building != null)
		{
			this._cityTile = building.currentTile;
			return;
		}
		List<Building> simpleList = this.buildings.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			simpleList.ShuffleOne(i);
			Building building2 = simpleList[i];
			if (!building2.stats.docks && !building2.currentTile.Type.ocean)
			{
				if (building == null)
				{
					building = building2;
				}
				else if (building2.stats.priority > building.stats.priority)
				{
					building = building2;
				}
			}
		}
		if (building != null)
		{
			this._cityTile = building.currentTile;
		}
		if (this.zones.Count != 0)
		{
			for (int j = 0; j < this.zones.Count; j++)
			{
				TileZone tileZone = this.zones[j];
				if (!tileZone.centerTile.Type.ocean)
				{
					this._cityTile = tileZone.centerTile;
					return;
				}
			}
		}
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x000417A8 File Offset: 0x0003F9A8
	internal int countInHouses()
	{
		int num = 0;
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			if (simpleList[i].insideBuilding != null)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x000417F0 File Offset: 0x0003F9F0
	internal void setKingdom(Kingdom pKingdom)
	{
		this.kingdom = pKingdom;
		if (this.kingdom == null)
		{
			this.data.kingdomID = null;
		}
		else
		{
			this.data.kingdomID = pKingdom.id;
			this.kingdom.addCity(this);
		}
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			actor.setKingdom(this.kingdom);
			if (actor.data.profession == UnitProfession.King)
			{
				actor.setProfession(UnitProfession.Unit);
			}
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00041880 File Offset: 0x0003FA80
	internal Building getStorageNear(WorldTile pTile)
	{
		Building building = null;
		float num = 0f;
		List<Building> simpleList = this.buildings.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Building building2 = simpleList[i];
			if (building2.stats.storage && !building2.data.underConstruction)
			{
				float num2 = Toolbox.DistVec2(building2.currentTile.pos, pTile.pos);
				if (building == null || num2 < num)
				{
					num = num2;
					building = building2;
				}
			}
		}
		if (building == null)
		{
			return null;
		}
		return building;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00041918 File Offset: 0x0003FB18
	public WorldTile getRoadTileToBuild(Actor pBuilder)
	{
		this.tilesToRemove.Clear();
		for (int i = 0; i < this.roadTilesToBuild.Count; i++)
		{
			WorldTile worldTile = this.roadTilesToBuild[i];
			if (worldTile.Type.road)
			{
				this.tilesToRemove.Add(worldTile);
			}
		}
		for (int j = 0; j < this.tilesToRemove.Count; j++)
		{
			WorldTile worldTile2 = this.tilesToRemove[j];
			this.roadTilesToBuild.Remove(worldTile2);
		}
		if (this.roadTilesToBuild.Count > 0)
		{
			return this.roadTilesToBuild[0];
		}
		return null;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x000419B8 File Offset: 0x0003FBB8
	internal override void create()
	{
		base.create();
		this.city_hash_id = City.LAST_CITY_HASH_ID++;
		this.createAI();
		this.cityText = base.gameObject.transform.Find("CityText").gameObject.GetComponent<TextMesh>();
		this.cityTextShadow = this.cityText.transform.Find("CityTextShadow").gameObject.GetComponent<TextMesh>();
		this.cityText.gameObject.SetActive(false);
		this.cityText.GetComponent<Renderer>().sortingOrder = base.gameObject.transform.GetComponent<SpriteRenderer>().sortingOrder;
		this.cityTextShadow.GetComponent<Renderer>().sortingOrder = base.gameObject.transform.GetComponent<SpriteRenderer>().sortingOrder;
		this.cityStatusDirty = true;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00041A90 File Offset: 0x0003FC90
	public void addPopPoint(ActorData pData)
	{
		this.data.popPoints.Add(pData);
		this.cityStatusDirty = true;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00041AAC File Offset: 0x0003FCAC
	private void createAI()
	{
		this.ai = new AiSystemCity(this);
		this.ai.nextJobDelegate = new GetNextJobID(this.getNextJob);
		this.ai.jobs_library = AssetManager.job_city;
		this.ai.task_library = AssetManager.tasks_city;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00041AFC File Offset: 0x0003FCFC
	private string getNextJob()
	{
		return "city";
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00041B04 File Offset: 0x0003FD04
	public void checkSettleTarget()
	{
		if (this.settleTarget == null)
		{
			return;
		}
		if (this.settleTarget.city == this)
		{
			this.settleTarget = null;
			return;
		}
		if (this.settleTarget.city == null)
		{
			return;
		}
		if (this.settleTarget.city.kingdom != this.kingdom)
		{
			this.settleTarget = null;
			return;
		}
		if (this.settleTarget.city.getPopulationTotal() >= 30)
		{
			this.settleTarget = null;
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00041B84 File Offset: 0x0003FD84
	internal void createNewCity()
	{
		this.data.cityID = this.world.mapStats.getNextId("city");
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00041BA6 File Offset: 0x0003FDA6
	internal void generateName()
	{
		this.data.cityName = NameGenerator.getName(this.race.name_template_city, ActorGender.Male);
		base.transform.name = this.data.cityName;
		this.hashcode = this.GetHashCode();
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00041BE6 File Offset: 0x0003FDE6
	internal void setRace(Race pRace)
	{
		this.race = pRace;
		this.data.race = pRace.id;
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00041C00 File Offset: 0x0003FE00
	public void loadLeader()
	{
		if (this.data.leaderID != null)
		{
			City.makeLeader(this.world.getActorByID(this.data.leaderID), this);
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00041C2B File Offset: 0x0003FE2B
	public void newCityEvent()
	{
		this.recalculateCityTile();
		this.generateName();
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00041C3C File Offset: 0x0003FE3C
	public void loadCity(CityData pData)
	{
		this.data = pData;
		this.data.storage.loadFromSave();
		this.race = AssetManager.raceLibrary.dict[this.data.race];
		base.transform.name = this.data.cityName;
		this.hashcode = this.GetHashCode();
		this.updateCityStatus();
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00041CA8 File Offset: 0x0003FEA8
	public void addNewUnit(Actor pActor, bool pSetKingdom = true, bool pSetJob = true)
	{
		pActor.setCity(this);
		this.units.Add(pActor);
		this.cityStatusDirty = true;
		this.unitsDirty = true;
		AchievementLibrary.achievementMegapolis.check_city(this);
		if (pSetKingdom)
		{
			pActor.setKingdom(this.kingdom);
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00041CF5 File Offset: 0x0003FEF5
	private void setAbandonedZonesDirty(bool pVal)
	{
		this.abandonedZonesDirty = true;
		this.abandonedZones_timer = 2f;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00041D0C File Offset: 0x0003FF0C
	private void updateCityStatus()
	{
		this.cityStatusDirty = false;
		this.setAbandonedZonesDirty(true);
		this.recalculateCityTile();
		this.recalculateNeighbourZones();
		this.status.maximumZones = this.race.civ_baseZones;
		this.status.maximumZones += this.buildings.Count * 3;
		if (this.leader != null)
		{
			this.status.maximumZones += this.leader.curStats.zones;
		}
		this.status.population = this.getPopulationTotal();
		this.status.populationAdults = this.getPopulationTotal() - this.countProfession(UnitProfession.Baby);
		this.status.hungry = 0;
		this.status.homesTotal = 0;
		this.status.homesFree = 0;
		this.status.homesOccupied = 0;
		this.blacksmith = null;
		Culture culture = this.getCulture();
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (actor.data.hunger <= 10)
			{
				this.status.hungry++;
			}
			if (actor.isCurrentJob("blacksmith"))
			{
				this.blacksmith = actor;
			}
		}
		List<Building> simpleList2 = this.buildings.getSimpleList();
		for (int j = 0; j < simpleList2.Count; j++)
		{
			Building building = simpleList2[j];
			if (!building.data.underConstruction && building.stats.housing > 0)
			{
				this.status.homesTotal += building.stats.housing;
				if (culture != null)
				{
					this.status.homesTotal += (int)culture.stats.housing.value;
				}
			}
		}
		if (this.status.population > this.status.homesTotal)
		{
			this.status.homesOccupied = this.status.homesTotal;
		}
		else
		{
			this.status.homesOccupied = this.status.population;
		}
		this.status.homesFree = this.status.homesTotal - this.status.homesOccupied;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00041F54 File Offset: 0x00040154
	public void recalculateNeighbourZones()
	{
		this.neighbourZones.Clear();
		for (int i = 0; i < this.zones.Count; i++)
		{
			TileZone tileZone = this.zones[i];
			for (int j = 0; j < tileZone.neighboursAll.Count; j++)
			{
				TileZone tileZone2 = tileZone.neighboursAll[j];
				if (tileZone2.city == null && !this.neighbourZones.Contains(tileZone2))
				{
					this.neighbourZones.Add(tileZone2);
				}
			}
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00041FDC File Offset: 0x000401DC
	public void removeCitizen(Actor pActor, bool pKilled = false, AttackType pAttackType = AttackType.Other)
	{
		this.units.Remove(pActor);
		this.cityStatusDirty = true;
		this.unitsDirty = true;
		if (pKilled)
		{
			this.data.deaths++;
			if (this.kingdom != null)
			{
				this.kingdom.deaths++;
			}
		}
		if (this.leader == pActor)
		{
			this.removeLeader();
		}
		if (this.kingdom.king == pActor)
		{
			this.kingdom.removeKing();
			if (pKilled)
			{
				if (pAttackType != AttackType.Age && pAttackType != AttackType.Plague && pAttackType != AttackType.Hunger)
				{
					Object x;
					if (pActor == null)
					{
						x = null;
					}
					else
					{
						BaseSimObject attackedBy = pActor.attackedBy;
						x = ((attackedBy != null) ? attackedBy.a : null);
					}
					if (!(x == null))
					{
						Kingdom pKingdom = this.kingdom;
						Actor pAttacker;
						if (pActor == null)
						{
							pAttacker = null;
						}
						else
						{
							BaseSimObject attackedBy2 = pActor.attackedBy;
							pAttacker = ((attackedBy2 != null) ? attackedBy2.a : null);
						}
						WorldLog.logKingMurder(pKingdom, pActor, pAttacker);
						if (pActor == null)
						{
							goto IL_117;
						}
						BaseSimObject attackedBy3 = pActor.attackedBy;
						if (attackedBy3 == null)
						{
							goto IL_117;
						}
						Actor a = attackedBy3.a;
						if (a == null)
						{
							goto IL_117;
						}
						a.addTrait("kingslayer");
						goto IL_117;
					}
				}
				WorldLog.logKingDead(this.kingdom, pActor);
			}
			else
			{
				WorldLog.logKingLeft(this.kingdom, pActor);
			}
		}
		IL_117:
		if (pAttackType == AttackType.Age)
		{
			City.giveItemsToCity(this, pActor);
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0004210C File Offset: 0x0004030C
	private static void giveItemsToCity(City pCity, Actor pDeadActor)
	{
		List<ActorEquipmentSlot> list = ActorEquipment.getList(pDeadActor.equipment, false);
		for (int i = 0; i < list.Count; i++)
		{
			ActorEquipmentSlot actorEquipmentSlot = list[i];
			ActorEquipmentSlot slot = pCity.data.storage.itemStorage.getSlot(actorEquipmentSlot.type);
			if (!slot.isEmpty())
			{
				ItemTools.calcItemValues(slot.data);
				float num = (float)ItemTools.s_value;
				ItemTools.calcItemValues(actorEquipmentSlot.data);
				if ((float)ItemTools.s_value > num)
				{
					slot.setItem(actorEquipmentSlot.data);
				}
			}
			else
			{
				slot.setItem(actorEquipmentSlot.data);
			}
		}
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x000421A8 File Offset: 0x000403A8
	public void removeLeader()
	{
		this.leader = null;
		this.data.leaderID = null;
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x000421BD File Offset: 0x000403BD
	public Culture getCulture()
	{
		if (string.IsNullOrEmpty(this.data.culture))
		{
			return null;
		}
		return this.world.cultures.get(this.data.culture);
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x000421EE File Offset: 0x000403EE
	internal void setCulture(Culture pCulture)
	{
		this.setCulture(pCulture.id);
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x000421FC File Offset: 0x000403FC
	internal void setCulture(string pCultureID)
	{
		this.data.culture = pCultureID;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0004220C File Offset: 0x0004040C
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.abandonedZonesDirty)
		{
			if (this.abandonedZones_timer > 0f)
			{
				this.abandonedZones_timer -= pElapsed;
			}
			else
			{
				CityCheckAbandonZones.checkAbandonZones(this);
				this.abandonedZonesDirty = false;
			}
		}
		this.buildings.checkAddRemove();
		this.units.checkAddRemove();
		if (this.data.timer_supply > 0f)
		{
			this.data.timer_supply -= pElapsed;
		}
		if (this.data.timer_trade > 0f)
		{
			this.data.timer_trade -= pElapsed;
		}
		if (this.data.timer_revolt > 0f)
		{
			this.data.timer_revolt -= pElapsed;
		}
		if (this.timer_warrior > 0f)
		{
			this.timer_warrior -= pElapsed;
		}
		if (this.data.age >= 1 && (this.zones.Count == 0 || this.getPopulationTotal() == 0))
		{
			this.world.destroyCity(this);
		}
		if (this.cityStatusDirty)
		{
			this.updateCityStatus();
		}
		if (this.world.isPaused())
		{
			return;
		}
		if (this.settlerTargetTimer > 0f)
		{
			this.settlerTargetTimer -= pElapsed;
		}
		if (this.boatBuildTimer > 0f)
		{
			this.boatBuildTimer -= pElapsed;
		}
		if (this.unitsDirty)
		{
			this.updateCitizens();
		}
		if (this.cityTickTimer > 0f)
		{
			this.cityTickTimer -= pElapsed;
		}
		else
		{
			this.cityTickTimer = this.cityTickInterval;
			this.updateTasks();
		}
		if (this.ai != null)
		{
			if (this.timer_action > 0f)
			{
				this.timer_action -= pElapsed;
			}
			else
			{
				this.ai.update();
			}
		}
		this.updateCapture(pElapsed);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x000423E8 File Offset: 0x000405E8
	private void updateCapture(float pElapsed)
	{
		if ((int)this.captureTicks == this.lastTicks)
		{
			if (this.lastTicks > 100)
			{
				this.lastTicks = 100;
			}
			if (this.lastTicks < 0)
			{
				this.lastTicks = 0;
			}
		}
		else if ((int)this.captureTicks > this.lastTicks)
		{
			this.lastTicks++;
			if (this.lastTicks > 100)
			{
				this.lastTicks = 100;
			}
		}
		else
		{
			this.lastTicks--;
			if (this.lastTicks < 0)
			{
				this.lastTicks = 0;
			}
		}
		if (this.captureTimer > 0f)
		{
			this.captureTimer -= pElapsed;
			return;
		}
		this.captureTimer = 0.1f;
		Kingdom kingdom = null;
		foreach (Kingdom kingdom2 in this.capturingUnits.Keys)
		{
			if (kingdom == null)
			{
				kingdom = kingdom2;
			}
			else if (this.capturingUnits[kingdom2] > this.capturingUnits[kingdom])
			{
				kingdom = kingdom2;
			}
		}
		if (kingdom == null)
		{
			this.captureTicks -= 0.5f;
			if (this.captureTicks <= 0f)
			{
				this.clearCapture();
			}
			return;
		}
		bool flag = false;
		if (this.capturingUnits.ContainsKey(this.kingdom) && this.capturingUnits[this.kingdom] > 0)
		{
			flag = true;
		}
		if (this.kingdom == kingdom && this.capturingBy != null)
		{
			this.captureTicks -= 1f;
			if (this.captureTicks <= 0f)
			{
				this.clearCapture();
				return;
			}
		}
		else if (kingdom.isEnemy(this.kingdom) && !flag)
		{
			if (this.capturingBy == null || this.capturingBy == kingdom)
			{
				this.captureTicks += 1f;
				this.capturingBy = kingdom;
				if (this.captureTicks >= 100f)
				{
					this.checkConquestBy(kingdom);
					return;
				}
			}
			else if (kingdom.isEnemy(this.capturingBy))
			{
				this.captureTicks -= 0.5f;
				if (this.captureTicks <= 0f)
				{
					this.clearCapture();
					return;
				}
			}
			else
			{
				this.captureTicks += 1f;
				if (this.captureTicks >= 100f)
				{
					this.checkConquestBy(this.capturingBy);
				}
			}
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0004264C File Offset: 0x0004084C
	public bool isGettingCaptured()
	{
		return this.capturingUnits.Count != 0 && (this.capturingUnits.Count != 1 || !this.capturingUnits.ContainsKey(this.kingdom));
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00042681 File Offset: 0x00040881
	public bool isGettingCapturedBy(Kingdom pKingdom)
	{
		return this.capturingUnits.ContainsKey(pKingdom) && this.capturingUnits[pKingdom] > 0;
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x000426A3 File Offset: 0x000408A3
	private void clearCapture()
	{
		this.captureTicks = 0f;
		this.capturingBy = null;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x000426B8 File Offset: 0x000408B8
	private void updateCitizens()
	{
		this.unitsDirty = false;
		if (this.professionsDict.Count == 0)
		{
			for (int i = 0; i < City.unitProfessions.Length; i++)
			{
				UnitProfession unitProfession = City.unitProfessions[i];
				this.professionsDict.Add(unitProfession, new List<Actor>());
			}
		}
		foreach (List<Actor> list in this.professionsDict.Values)
		{
			list.Clear();
		}
		if (!DebugConfig.isOn(DebugOption.SystemUseCitizensDict))
		{
			return;
		}
		List<Actor> simpleList = this.units.getSimpleList();
		for (int j = 0; j < simpleList.Count; j++)
		{
			Actor actor = simpleList[j];
			if (!actor.stats.baby)
			{
				if (actor == null || !actor.data.alive)
				{
					this.units.Remove(actor);
				}
				else
				{
					this.professionsDict[actor.data.profession].Add(actor);
				}
			}
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000427D4 File Offset: 0x000409D4
	private void updateTasks()
	{
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000427D8 File Offset: 0x000409D8
	internal bool needMoreAttackers(Actor pActor)
	{
		int num = this.jobs.countOccupied("attacker");
		int num2 = this.jobs.countOccupied("defender");
		if (num < num2)
		{
			this.jobs.freeJob("defender");
			this.jobs.takeJob("attacker");
			pActor.ai.setJob("attacker");
			return true;
		}
		return false;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0004283C File Offset: 0x00040A3C
	internal int countProfession(UnitProfession pType)
	{
		if (this.professionsDict.ContainsKey(pType))
		{
			return this.professionsDict[pType].Count;
		}
		return 0;
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00042860 File Offset: 0x00040A60
	public void destroyCity()
	{
		this.alive = false;
		this.buildings.checkAddRemove();
		this.units.checkAddRemove();
		if (this.kingdom != null)
		{
			this.kingdom.removeCity(this);
		}
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			simpleList[i].removeFromCity();
		}
		foreach (TileZone tileZone in this.zones)
		{
			tileZone.setCity(null);
		}
		foreach (Building pBuilding in this.buildings)
		{
			this.abandonBuilding(pBuilding);
		}
		this.buildings_dict_id.Clear();
		this.buildings_dict_type.Clear();
		this.zones.Clear();
		this.units.Clear();
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00042978 File Offset: 0x00040B78
	public void abandonBuilding(Building pBuilding)
	{
		pBuilding.setCity(null, true);
		this.buildings.Remove(pBuilding);
		if (pBuilding.object_destroyed)
		{
			return;
		}
		if (pBuilding.data.underConstruction || !pBuilding.stats.canBeAbandoned)
		{
			pBuilding.startDestroyBuilding(false);
			return;
		}
		pBuilding.makeAbandoned();
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000429CC File Offset: 0x00040BCC
	public void removeBuilding(Building pBuilding)
	{
		this.cityStatusDirty = true;
		this.buildings.Remove(pBuilding);
		this.setBuildingDictType(pBuilding, false);
		if (pBuilding.stats.housing > 0 && this.data.popPoints.Count > 0)
		{
			this.killRandomPopPoints(1);
			this.spawnRandomPopPoints(pBuilding.stats.housing, pBuilding.currentTile);
		}
		if (this.underConstructionBuilding == pBuilding)
		{
			this.underConstructionBuilding = null;
		}
		if (this.buildings.Count == 0)
		{
			this.world.destroyCity(this);
		}
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00042A64 File Offset: 0x00040C64
	public static ActorData getActorDataPopPoint(City pCity)
	{
		if (pCity.data.popPoints.Count == 0)
		{
			return null;
		}
		ActorData random = pCity.data.popPoints.GetRandom<ActorData>();
		pCity.data.popPoints.RemoveAtSwapBack(random);
		return random;
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00042AA8 File Offset: 0x00040CA8
	public void spawnPopPoint(ActorData pData, WorldTile pTile)
	{
		pData.status.actorID = this.world.mapStats.getNextId("unit");
		if (pData.status.age <= 8)
		{
			pData.status.statsID = pData.status.statsID.Replace("unit_", "baby_");
			pData.status.profession = UnitProfession.Baby;
		}
		ActorStats actorStats = AssetManager.unitStats.get(pData.status.statsID);
		for (int i = 0; i < actorStats.traits.Count; i++)
		{
			string text = actorStats.traits[i];
			if (!pData.status.traits.Contains(text))
			{
				pData.status.traits.Add(text);
			}
		}
		Actor actor = this.world.spawnAndLoadUnit(pData.status.statsID, pData, pTile);
		actor.setStatsDirty();
		if (actor.stats.baby)
		{
			actor.GetComponent<Baby>().timerGrow = (float)actor.stats.timeToGrow;
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00042BB4 File Offset: 0x00040DB4
	public void spawnRandomPopPoints(int pAmount, WorldTile pTile)
	{
		for (int i = 0; i < pAmount; i++)
		{
			ActorData actorDataPopPoint = City.getActorDataPopPoint(this);
			if (actorDataPopPoint == null)
			{
				return;
			}
			this.spawnPopPoint(actorDataPopPoint, pTile);
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00042BE0 File Offset: 0x00040DE0
	public void killHalfPopPoints()
	{
		this.killRandomPopPoints(this.data.popPoints.Count / 2 + 1);
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00042BFC File Offset: 0x00040DFC
	public void killRandomPopPoints(int pAmount = 1)
	{
		if (this.data.popPoints.Count == 0)
		{
			return;
		}
		int num = pAmount;
		if (num == 1)
		{
			if (this.data.popPoints.Count > 1000)
			{
				num += this.data.popPoints.Count / 100;
			}
			else if (this.data.popPoints.Count > 100)
			{
				num += this.data.popPoints.Count / 10;
			}
		}
		int num2 = 0;
		while (num2 < num && this.data.popPoints.Count != 0)
		{
			this.data.popPoints.ShuffleLast<ActorData>();
			this.data.popPoints.RemoveAt(this.data.popPoints.Count - 1);
			num2++;
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00042CCC File Offset: 0x00040ECC
	internal void spendResourcesFor(ConstructionCost pCost)
	{
		this.data.storage.change("wood", -pCost.wood);
		this.data.storage.change("gold", -pCost.gold);
		this.data.storage.change("stone", -pCost.stone);
		this.data.storage.change("metals", -pCost.metals);
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00042D4C File Offset: 0x00040F4C
	internal bool haveEnoughResourcesFor(ConstructionCost pCost)
	{
		return DebugConfig.isOn(DebugOption.CityInfiniteResources) || (this.data.storage.get("wood") >= pCost.wood && this.data.storage.get("metals") >= pCost.metals && this.data.storage.get("stone") >= pCost.stone && this.data.storage.get("gold") >= pCost.gold);
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00042DE4 File Offset: 0x00040FE4
	internal Building getBuildingToBuild()
	{
		if (this.underConstructionBuilding != null && (!this.underConstructionBuilding.data.underConstruction || !this.underConstructionBuilding.data.alive))
		{
			this.underConstructionBuilding = null;
		}
		return this.underConstructionBuilding;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00042E30 File Offset: 0x00041030
	internal void setBuildingDictType(Building pBuilding, bool pAdd)
	{
		BuildingContainer buildingContainer = this.getBuildingContainer(pBuilding.stats.type);
		if (buildingContainer == null)
		{
			buildingContainer = new BuildingContainer();
			this.buildings_dict_type.Add(pBuilding.stats.type, buildingContainer);
		}
		if (pAdd)
		{
			buildingContainer.Add(pBuilding);
		}
		else
		{
			buildingContainer.Remove(pBuilding);
		}
		this.setBuildingDictID(pBuilding, pAdd);
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00042E8C File Offset: 0x0004108C
	internal void setBuildingDictID(Building pBuilding, bool pAdd)
	{
		if (!this.buildings_dict_id.ContainsKey(pBuilding.stats.id))
		{
			this.buildings_dict_id.Add(pBuilding.stats.id, new BuildingContainer());
		}
		if (pAdd)
		{
			this.buildings_dict_id[pBuilding.stats.id].Add(pBuilding);
			return;
		}
		this.buildings_dict_id[pBuilding.stats.id].Remove(pBuilding);
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00042F08 File Offset: 0x00041108
	public Building addBuilding(Building pBuilding)
	{
		pBuilding.setCity(this, true);
		this.buildings.Add(pBuilding);
		this.setBuildingDictType(pBuilding, true);
		this.cityStatusDirty = true;
		return pBuilding;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00042F30 File Offset: 0x00041130
	public int countBuildingsType(string pBuildingTypeID)
	{
		BuildingContainer buildingContainer = this.getBuildingContainer(pBuildingTypeID);
		if (buildingContainer == null)
		{
			return 0;
		}
		buildingContainer.checkAddRemove();
		return buildingContainer.Count;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00042F58 File Offset: 0x00041158
	public int countBuildingsID(string pBuildingTypeID)
	{
		BuildingContainer buildingContainer = this.getBuildingContainer(pBuildingTypeID);
		if (buildingContainer == null)
		{
			return 0;
		}
		buildingContainer.checkAddRemove();
		return buildingContainer.Count;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00042F80 File Offset: 0x00041180
	internal bool haveBuildingType(string pBuildingTypeID, bool pCountOnlyFinished = true)
	{
		BuildingContainer buildingContainer = this.getBuildingContainer(pBuildingTypeID);
		if (buildingContainer == null)
		{
			return false;
		}
		buildingContainer.checkAddRemove();
		if (buildingContainer.Count == 0)
		{
			return false;
		}
		Building building = buildingContainer.getSimpleList()[0];
		return !pCountOnlyFinished || !building.data.underConstruction;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00042FCC File Offset: 0x000411CC
	internal BuildingContainer getBuildingContainer(string pTypeID)
	{
		BuildingContainer result = null;
		this.buildings_dict_type.TryGetValue(pTypeID, ref result);
		return result;
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00042FEC File Offset: 0x000411EC
	internal Building getBuildingType(string pBuildingTypeID, bool pCountOnlyFinished = true, bool pRandom = false)
	{
		BuildingContainer buildingContainer = this.getBuildingContainer(pBuildingTypeID);
		if (buildingContainer == null)
		{
			return null;
		}
		buildingContainer.checkAddRemove();
		if (buildingContainer.Count == 0)
		{
			return null;
		}
		Building building;
		if (pRandom)
		{
			building = buildingContainer.GetRandom();
		}
		else
		{
			building = buildingContainer.getSimpleList()[0];
		}
		if (pCountOnlyFinished && building.data.underConstruction)
		{
			return null;
		}
		return building;
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00043044 File Offset: 0x00041244
	public void addRoads(List<WorldTile> pTiles)
	{
		for (int i = 0; i < pTiles.Count; i++)
		{
			WorldTile worldTile = pTiles[i];
			if (!worldTile.Type.road && !this.roadTilesToBuild.Contains(worldTile))
			{
				this.roadTilesToBuild.Add(worldTile);
			}
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00043094 File Offset: 0x00041294
	private bool tryToMakeWarrior(Actor pActor)
	{
		if (this.status.warriorSlots <= this.status.warriorCurrent)
		{
			return false;
		}
		if (this.blacksmith == null)
		{
			return false;
		}
		if (this.data.storage.itemStorage.weapon.data == null)
		{
			this.tryProduceItem(this.blacksmith, ItemProductionOrder.Weapon);
		}
		if (this.data.storage.itemStorage.weapon.data == null && pActor.equipment.weapon.isEmpty())
		{
			return false;
		}
		pActor.setProfession(UnitProfession.Warrior);
		if (pActor.equipment.weapon.isEmpty())
		{
			City.giveItem(pActor, this.data.storage.itemStorage.weapon, this);
		}
		this.status.warriorCurrent++;
		this.timer_warrior = 30f;
		if (this.leader != null)
		{
			float num = (float)(this.leader.curStats.warfare / 2);
			this.timer_warrior -= num;
			if (this.timer_warrior < 1f)
			{
				this.timer_warrior = 1f;
			}
		}
		if (this.haveBuildingType("barracks", true))
		{
			this.timer_warrior /= 2f;
		}
		return true;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x000431E4 File Offset: 0x000413E4
	public void findNewJob(Actor pActor)
	{
		if (this.captureTicks == 0f)
		{
			if (pActor.isProfession(UnitProfession.Warrior))
			{
				if (this.status.warriorSlots < this.status.warriorCurrent)
				{
					pActor.setProfession(UnitProfession.Unit);
					this.status.warriorCurrent--;
				}
			}
			else if (this.timer_warrior <= 0f && pActor.isProfession(UnitProfession.Unit) && this.data.storage.get("gold") > 10 && this.tryToMakeWarrior(pActor))
			{
				return;
			}
		}
		CityTaskList.prepare(this, pActor);
		if (CityTaskList.checkJob("fireman"))
		{
			return;
		}
		if (CityTaskList.checkJob("builder"))
		{
			return;
		}
		if (this.getFoodItem(null) == null && CityTaskList.checkJob("gatherer"))
		{
			return;
		}
		List<string> list;
		if (pActor.isProfession(UnitProfession.Warrior))
		{
			list = CityTaskList.jobsWarrior;
		}
		else
		{
			list = CityTaskList.jobsUnit;
		}
		for (int i = 0; i < list.Count; i++)
		{
			string text;
			if (pActor.isProfession(UnitProfession.Warrior))
			{
				this.currentTaskID_warrior++;
				if (this.currentTaskID_warrior > list.Count - 1)
				{
					this.currentTaskID_warrior = 0;
				}
				text = list[this.currentTaskID_warrior];
			}
			else
			{
				this.currentTaskID_unit++;
				if (this.currentTaskID_unit > list.Count - 1)
				{
					this.currentTaskID_unit = 0;
				}
				text = list[this.currentTaskID_unit];
			}
			if ((!(text == "settler") || (!(pActor == this.leader) && !(pActor == this.kingdom.king))) && CityTaskList.checkJob(text))
			{
				return;
			}
		}
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00043380 File Offset: 0x00041580
	public void tryProduceItem(Actor pActor, ItemProductionOrder pOrder = ItemProductionOrder.Both)
	{
		Culture culture = pActor.getCulture();
		if (culture == null)
		{
			return;
		}
		int num = 1;
		switch (pOrder)
		{
		case ItemProductionOrder.RandomArmor:
		{
			if (!culture.haveTech("armor_production"))
			{
				return;
			}
			List<ActorEquipmentSlot> list = ActorEquipment.getList(this.data.storage.itemStorage, true);
			if (list.Count == 0)
			{
				return;
			}
			ActorEquipmentSlot random = list.GetRandom<ActorEquipmentSlot>();
			num += (int)culture.stats.item_production_tries_weapons.value;
			this.produceItem(pActor, pActor.data.firstName, random, num);
			return;
		}
		case ItemProductionOrder.Weapon:
			if (!culture.haveTech("weapon_production"))
			{
				return;
			}
			if (this.data.storage.itemStorage.weapon.data == null)
			{
				num += (int)culture.stats.item_production_tries_armor.value;
				this.produceItem(pActor, pActor.data.firstName, this.data.storage.itemStorage.weapon, num);
				return;
			}
			break;
		case ItemProductionOrder.Both:
			this.tryProduceItem(pActor, ItemProductionOrder.RandomArmor);
			this.tryProduceItem(pActor, ItemProductionOrder.Weapon);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00043484 File Offset: 0x00041684
	public void produceItem(Actor pActor, string pCreatorName, ActorEquipmentSlot pSlot, int pTries = 1)
	{
		Culture culture = pActor.getCulture();
		if (culture == null)
		{
			return;
		}
		ItemAssetLibrary<ItemAsset> pLib = null;
		switch (pSlot.type)
		{
		case EquipmentType.Weapon:
			pLib = AssetManager.items_material_weapon;
			break;
		case EquipmentType.Helmet:
		case EquipmentType.Armor:
		case EquipmentType.Boots:
			pLib = AssetManager.items_material_armor;
			break;
		case EquipmentType.Ring:
		case EquipmentType.Amulet:
			pLib = AssetManager.items_material_accessory;
			break;
		}
		string text = AssetManager.items.getEquipmentID(pSlot.type);
		if (text == "weapon")
		{
			text = this.race.preferred_weapons.GetRandom<string>();
		}
		ItemAsset itemAsset = AssetManager.items.get(text);
		if (!string.IsNullOrEmpty(itemAsset.tech_needed) && !culture.haveTech(itemAsset.tech_needed))
		{
			return;
		}
		ItemAsset materialForItem = this.data.storage.getMaterialForItem(itemAsset, pLib, this, true);
		if (materialForItem == null)
		{
			return;
		}
		ItemGenerator.generateItem(itemAsset, materialForItem.id, pSlot, this.world.mapStats.year, this.kingdom.name, pCreatorName, pTries);
		this.data.storage.change("gold", -materialForItem.cost_gold);
		if (materialForItem.cost_resource_id_1 != "none")
		{
			this.data.storage.change(materialForItem.cost_resource_id_1, -materialForItem.cost_resource_1);
		}
		if (materialForItem.cost_resource_id_2 != "none")
		{
			this.data.storage.change(materialForItem.cost_resource_id_2, -materialForItem.cost_resource_2);
		}
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x000435FC File Offset: 0x000417FC
	internal ResourceAsset getFoodItem(string pFavoriteFood = null)
	{
		if (!string.IsNullOrEmpty(pFavoriteFood) && this.data.storage.get(pFavoriteFood) > 0)
		{
			return AssetManager.resources.get(pFavoriteFood);
		}
		return this.data.storage.getRandomFood();
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00043636 File Offset: 0x00041836
	internal void eatFoodItem(string pItem)
	{
		if (pItem == null)
		{
			return;
		}
		this.data.storage.change(pItem, -1);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0004364E File Offset: 0x0004184E
	internal void removeZone(TileZone pZone, bool pAbandonBuildings = false)
	{
		if (this.zones.Remove(pZone))
		{
			pZone.setCity(null);
			this.world.cityPlaceFinder.setDirty();
		}
		this.updateCityCenter();
		if (pAbandonBuildings)
		{
			this.abandonBuildingsInZone(pZone);
		}
		this.cityStatusDirty = true;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0004368C File Offset: 0x0004188C
	private void abandonBuildingsInZone(TileZone pZone)
	{
		if (pZone.buildings.Count == 0)
		{
			return;
		}
		City._buildingsToAbandon.Clear();
		foreach (Building building in pZone.buildings)
		{
			if (!(building.city != this))
			{
				City._buildingsToAbandon.Add(building);
			}
		}
		for (int i = 0; i < City._buildingsToAbandon.Count; i++)
		{
			Building pBuilding = City._buildingsToAbandon[i];
			this.abandonBuilding(pBuilding);
		}
		this.buildings.checkAddRemove();
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0004373C File Offset: 0x0004193C
	internal void addZone(TileZone pZone)
	{
		if (this.zones.Contains(pZone))
		{
			return;
		}
		if (pZone.city != null)
		{
			pZone.city.removeZone(pZone, false);
		}
		this.zones.Add(pZone);
		pZone.setCity(this);
		this.updateCityCenter();
		if (this.world.cityPlaceFinder.zones.Count > 0)
		{
			this.world.cityPlaceFinder.setDirty();
		}
		this.cityStatusDirty = true;
		Toolbox.temp_list_buildings_2.Clear();
		Toolbox.temp_list_buildings_2.AddRange(pZone.abandoned);
		for (int i = 0; i < Toolbox.temp_list_buildings_2.Count; i++)
		{
			Building building = Toolbox.temp_list_buildings_2[i];
			if (building.stats.cityBuilding && (string.IsNullOrEmpty(building.stats.race) || !(building.stats.race != this.kingdom.race.id)) && building.data.state == BuildingState.CivAbandoned)
			{
				this.addBuilding(building);
				building.retake();
			}
		}
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00043854 File Offset: 0x00041A54
	public int getRelationRating()
	{
		this._opinion_king = 0;
		this._opinion_leader = 0;
		this._opinion_population = 0;
		this._opinion_zones = 0;
		this._opinion_distance = 0;
		this._opinion_capital = 0;
		this._opinion_loyalty_traits = 0;
		this._opinion_loyalty_mood = 0;
		this._opinion_cities = 0;
		this._opinion_superior_enemies = 0;
		this._opinion_close_to_capital = 0;
		this._opinion_world_law = 0;
		this._opinion_culture = 0;
		if (!this.world.worldLaws.world_law_rebellions.boolVal)
		{
			this._opinion_world_law = 100;
			this._opinion_total = 100;
			return this._opinion_total;
		}
		if (this.isCapitalCity())
		{
			this._opinion_total = 100;
			this._opinion_capital = 100;
			return this._opinion_total;
		}
		if (this.kingdom.capital != null)
		{
			if (City.nearbyBorders(this.kingdom.capital, this))
			{
				this._opinion_close_to_capital = 15;
			}
			if (this.kingdom.capital.getCulture() == this.getCulture())
			{
				this._opinion_culture = 10;
			}
			else
			{
				this._opinion_culture = -10;
			}
		}
		if (this.data.age <= 25)
		{
			this._opinion_new_city = 25 - this.data.age;
		}
		else
		{
			this._opinion_new_city = 0;
		}
		if (this.kingdom.age <= 30)
		{
			this._opinion_new_kingdom = 30 - this.kingdom.age;
		}
		else
		{
			this._opinion_new_kingdom = 0;
		}
		if (this.kingdom.civs_enemies.Count > 0)
		{
			int num = 0;
			foreach (Kingdom kingdom in this.kingdom.civs_enemies.Keys)
			{
				num += kingdom.power;
			}
			this._opinion_superior_enemies = (num - this.kingdom.power) / 2;
			if (this._opinion_superior_enemies < 0)
			{
				this._opinion_superior_enemies = 0;
			}
			else if (this._opinion_superior_enemies > 50)
			{
				this._opinion_superior_enemies = 50;
			}
		}
		if (this.kingdom.king != null)
		{
			this._opinion_king = this.kingdom.king.curStats.diplomacy;
		}
		int maxCities = this.kingdom.getMaxCities();
		if (this.kingdom.cities.Count > maxCities)
		{
			this._opinion_cities = (maxCities - this.kingdom.cities.Count) * 25;
		}
		if (this.leader != null)
		{
			this._opinion_leader = -this.leader.curStats.diplomacy;
			this._opinion_loyalty_traits = this.leader.curStats.loyalty_traits;
		}
		if (this.leader != null && this.kingdom.king != null)
		{
			int num2 = AssetManager.traits.checkTraitsMod(this.leader, this.kingdom.king);
			this._opinion_loyalty_traits += num2;
		}
		if (this.kingdom.capital != null)
		{
			int num3 = Mathf.Abs(this.status.population - this.kingdom.capital.status.population) / 3;
			if (num3 > 30)
			{
				num3 = 30;
			}
			if (this.status.population > this.kingdom.capital.status.population)
			{
				this._opinion_population = -num3;
			}
			else
			{
				this._opinion_population = num3;
			}
			int num4 = Mathf.Abs(this.zones.Count - this.kingdom.capital.zones.Count) / 20;
			if (num4 > 5)
			{
				num4 = 5;
			}
			if (this.zones.Count > this.kingdom.capital.zones.Count)
			{
				this._opinion_zones = -num4;
			}
			else
			{
				this._opinion_zones = num4;
			}
			if (this.cityCenter.x != Globals.POINT_IN_VOID.x && this.kingdom.capital.cityCenter.x != Globals.POINT_IN_VOID.x)
			{
				int num5 = (int)(Toolbox.DistVec3(this.cityCenter, this.kingdom.capital.cityCenter) / 10f);
				this._opinion_distance = -num5;
			}
		}
		else
		{
			this._opinion_capital = -20;
		}
		if (this.leader != null)
		{
			this._opinion_loyalty_mood = this.leader.curStats.loyalty_mood;
		}
		this._opinion_total = this._opinion_king + this._opinion_leader + this._opinion_population + this._opinion_zones + this._opinion_distance + this._opinion_capital + this._opinion_loyalty_traits + this._opinion_loyalty_mood + this._opinion_new_kingdom + this._opinion_cities + this._opinion_superior_enemies + this._opinion_close_to_capital + this._opinion_culture + this._opinion_new_city;
		return this._opinion_total;
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00043D30 File Offset: 0x00041F30
	public bool isCapitalCity()
	{
		return this.kingdom != null && this == this.kingdom.capital;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00043D50 File Offset: 0x00041F50
	internal void updateAge()
	{
		this.data.age++;
		this.gold_in_tax = this.getPopulationTotal() / 2;
		this.gold_out_homeless = this.getPopulationTotal() - this.status.homesTotal;
		if (this.gold_out_homeless < 0)
		{
			this.gold_out_homeless = 0;
		}
		this.gold_out_army = this.countProfession(UnitProfession.Warrior) / 2;
		this.gold_out_buildigs = this.buildings.Count / 2;
		this.gold_change = this.gold_in_tax - this.gold_out_army - this.gold_out_buildigs - this.gold_out_homeless;
		int num = this.gold_change;
		if (num < 0)
		{
			num = 0;
		}
		this.data.storage.change("gold", num);
		this.updatePopPoints();
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00043E14 File Offset: 0x00042014
	private void updatePopPoints()
	{
		if (this.data.popPoints.Count == 0)
		{
			return;
		}
		int i = 0;
		while (i < this.data.popPoints.Count)
		{
			if (this.data.popPoints[i].status.updateAge(this.race))
			{
				i++;
			}
			else
			{
				this.data.popPoints.RemoveAt(i);
			}
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00043E88 File Offset: 0x00042088
	private void updateCityCenter()
	{
		if (this.zones.Count == 0)
		{
			this.cityCenter = Globals.POINT_IN_VOID;
			return;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		TileZone tileZone = null;
		for (int i = 0; i < this.zones.Count; i++)
		{
			TileZone tileZone2 = this.zones[i];
			num += tileZone2.centerTile.posV3.x;
			num2 += tileZone2.centerTile.posV3.y;
		}
		this.cityCenter.x = num / (float)this.zones.Count;
		this.cityCenter.y = num2 / (float)this.zones.Count;
		for (int j = 0; j < this.zones.Count; j++)
		{
			TileZone tileZone3 = this.zones[j];
			float num4 = Toolbox.Dist((float)tileZone3.centerTile.x, (float)tileZone3.centerTile.y, this.cityCenter.x, this.cityCenter.y);
			if (tileZone == null || num4 < num3)
			{
				tileZone = tileZone3;
				num3 = num4;
			}
		}
		this.cityCenter.x = tileZone.centerTile.posV3.x;
		this.cityCenter.y = tileZone.centerTile.posV3.y + 2f;
		this.lastCityCenter = this.cityCenter;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00044004 File Offset: 0x00042204
	internal void removeFromCurrentKingdom()
	{
		this.kingdom.removeCity(this);
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor pUnit = simpleList[i];
			this.kingdom.removeUnit(pUnit);
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00044050 File Offset: 0x00042250
	internal void switchedKingdom()
	{
		List<Building> simpleList = this.buildings.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Building building = simpleList[i];
			if (building.data.state != BuildingState.Removed)
			{
				building.setKingdom(this.kingdom, true);
			}
		}
		this.world.zoneCalculator.setDrawnZonesDirty();
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x000440B0 File Offset: 0x000422B0
	internal void useInspire()
	{
		Kingdom tTarget = this.kingdom;
		this.makeOwnKingdom();
		MapBox.instance.kingdoms.diplomacyManager.startWar(this.kingdom, tTarget, true);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x000440E7 File Offset: 0x000422E7
	internal void clearCurrentCaptureAmounts()
	{
		this.capturingUnits.Clear();
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x000440F4 File Offset: 0x000422F4
	internal void updateConquest(Actor pActor)
	{
		if (!pActor.kingdom.isCiv())
		{
			return;
		}
		if (pActor.kingdom.race != this.kingdom.race)
		{
			return;
		}
		if (pActor.kingdom != this.kingdom && !pActor.kingdom.isEnemy(this.kingdom))
		{
			return;
		}
		this.addCapturePoints(pActor, 1);
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00044154 File Offset: 0x00042354
	public void addCapturePoints(BaseSimObject pObject, int pValue)
	{
		if (!this.capturingUnits.ContainsKey(pObject.kingdom))
		{
			this.capturingUnits[pObject.kingdom] = pValue;
			return;
		}
		Dictionary<Kingdom, int> dictionary = this.capturingUnits;
		Kingdom kingdom = pObject.kingdom;
		dictionary[kingdom] += pValue;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x000441A5 File Offset: 0x000423A5
	internal void checkConquestBy(Kingdom pKingdom)
	{
		this.clearCapture();
		this.joinAnotherKingdom(pKingdom);
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x000441B4 File Offset: 0x000423B4
	internal int getMaximumZones()
	{
		return this.status.maximumZones;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x000441C4 File Offset: 0x000423C4
	internal void shrinkZones(bool pChance = true)
	{
		if (pChance && Toolbox.randomChance(0.2f))
		{
			return;
		}
		if (this.zones.Count == 0)
		{
			return;
		}
		TileZone tileZone = null;
		float num = 0f;
		for (int i = 0; i < this.zones.Count; i++)
		{
			TileZone tileZone2 = this.zones[i];
			if (tileZone2.buildings.Count <= 0)
			{
				float num2 = Toolbox.DistVec3(tileZone2.centerTile.posV3, this.cityCenter);
				if (tileZone == null || num2 > num)
				{
					bool flag = true;
					for (int j = 0; j < tileZone2.neighbours.Count; j++)
					{
						if (tileZone2.neighbours[j].city != this)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						tileZone = tileZone2;
						num = num2;
					}
				}
			}
		}
		if (tileZone == null)
		{
			return;
		}
		this.removeZone(tileZone, false);
		this.cityStatusDirty = true;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x000442AF File Offset: 0x000424AF
	internal Kingdom makeOwnKingdom()
	{
		this.removeFromCurrentKingdom();
		Kingdom result = this.world.kingdoms.makeNewCivKingdom(this, null, null, true);
		this.switchedKingdom();
		return result;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x000442D1 File Offset: 0x000424D1
	public int getPopulationTotal()
	{
		return this.units.Count + this.data.popPoints.Count;
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x000442EF File Offset: 0x000424EF
	public int getPopulationPopPoints()
	{
		return this.data.popPoints.Count;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00044301 File Offset: 0x00042501
	public int getPopulationUnits()
	{
		return this.units.Count;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00044310 File Offset: 0x00042510
	public void joinAnotherKingdom(Kingdom pKingdom)
	{
		Kingdom pKingdom2 = this.kingdom;
		this.removeFromCurrentKingdom();
		this.setKingdom(pKingdom);
		this.switchedKingdom();
		pKingdom.timer_event = 0f;
		this.data.timer_revolt = 150f;
		pKingdom.capturedFrom(pKingdom2);
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00044359 File Offset: 0x00042559
	private void OnDrawGizmos()
	{
		if (this.attackZone != null)
		{
			Debug.DrawLine(this.getTile().posV3, this.attackZone.centerTile.posV3, Color.red);
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00044388 File Offset: 0x00042588
	public void forceSettleTarget(City pCity)
	{
		this.settlerTargetTimer = 500f;
		this.settleTarget = null;
		if (pCity != this && pCity.getTile() != null)
		{
			this.settleTarget = pCity.getTile().zone;
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000443BE File Offset: 0x000425BE
	public int getArmy()
	{
		return this.countProfession(UnitProfession.Warrior);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x000443C8 File Offset: 0x000425C8
	public int getArmyMaxLeader()
	{
		int num = this.race.civ_baseArmy;
		if (this.leader != null)
		{
			num += this.leader.curStats.army;
		}
		num = Mathf.Clamp(num, 10, 65);
		return num + this.getPopulationTotal() * num / 100;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0004441C File Offset: 0x0004261C
	public int getArmyMaxCity()
	{
		return this.status.warriorSlots;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0004442C File Offset: 0x0004262C
	public static void makeLeader(Actor pActor, City pCity)
	{
		if (pActor == null)
		{
			return;
		}
		if (pCity.kingdom.king == pActor)
		{
			return;
		}
		pCity.leader = pActor;
		pCity.leader.setProfession(UnitProfession.Leader);
		pCity.data.leaderID = pActor.data.actorID;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00044480 File Offset: 0x00042680
	public static bool nearbyBorders(City pA, City pB)
	{
		City city;
		City y;
		if (pA.zones.Count > pB.zones.Count)
		{
			city = pB;
			y = pA;
		}
		else
		{
			city = pA;
			y = pB;
		}
		for (int i = 0; i < city.zones.Count; i++)
		{
			TileZone tileZone = city.zones[i];
			for (int j = 0; j < tileZone.neighboursAll.Count; j++)
			{
				if (tileZone.neighboursAll[j].city == y)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0004450C File Offset: 0x0004270C
	public static bool giveItem(Actor pActor, ActorEquipmentSlot pSlot, City pCity)
	{
		ActorEquipmentSlot slot = pActor.equipment.getSlot(pSlot.type);
		if (slot.data != null)
		{
			ItemTools.calcItemValues(slot.data);
			float num = (float)ItemTools.s_value;
			ItemTools.calcItemValues(pSlot.data);
			if ((float)ItemTools.s_value > num)
			{
				ItemData item = slot.data;
				slot.setItem(pSlot.data);
				pSlot.setItem(item);
				pActor.setStatsDirty();
			}
			return false;
		}
		slot.setItem(pSlot.data);
		pSlot.emptySlot();
		pActor.setStatsDirty();
		return true;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00044594 File Offset: 0x00042794
	public int getLimitOfBuildings(CityBuildOrderElement pElement)
	{
		int num = pElement.limitType;
		Culture culture = this.getCulture();
		if (culture != null && pElement.buildingID == "watch_tower")
		{
			num += (int)culture.stats.bonus_watch_towers.value;
			if (this.leader != null)
			{
				num += this.leader.curStats.bonus_towers;
			}
		}
		return num;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x000445FA File Offset: 0x000427FA
	public override int GetHashCode()
	{
		return this.city_hash_id;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00044602 File Offset: 0x00042802
	public override bool Equals(object obj)
	{
		return this.Equals(obj as City);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00044610 File Offset: 0x00042810
	public bool Equals(City pObject)
	{
		return this.city_hash_id == pObject.city_hash_id;
	}

	// Token: 0x0400070B RID: 1803
	public Dictionary<UnitProfession, List<Actor>> professionsDict = new Dictionary<UnitProfession, List<Actor>>();

	// Token: 0x0400070C RID: 1804
	public ActorContainer units = new ActorContainer();

	// Token: 0x0400070D RID: 1805
	public BuildingContainer buildings = new BuildingContainer();

	// Token: 0x0400070E RID: 1806
	public Dictionary<string, BuildingContainer> buildings_dict_type = new Dictionary<string, BuildingContainer>();

	// Token: 0x0400070F RID: 1807
	public Dictionary<string, BuildingContainer> buildings_dict_id = new Dictionary<string, BuildingContainer>();

	// Token: 0x04000710 RID: 1808
	public CityTasksData tasks = new CityTasksData();

	// Token: 0x04000711 RID: 1809
	public CityJobs jobs;

	// Token: 0x04000712 RID: 1810
	public CityStatus status = new CityStatus();

	// Token: 0x04000713 RID: 1811
	internal CityData data = new CityData();

	// Token: 0x04000714 RID: 1812
	private bool cityStatusDirty;

	// Token: 0x04000715 RID: 1813
	private bool abandonedZonesDirty;

	// Token: 0x04000716 RID: 1814
	private float abandonedZones_timer;

	// Token: 0x04000717 RID: 1815
	[NonSerialized]
	internal int hashcode;

	// Token: 0x04000718 RID: 1816
	[NonSerialized]
	internal Race race;

	// Token: 0x04000719 RID: 1817
	[NonSerialized]
	internal Kingdom kingdom;

	// Token: 0x0400071A RID: 1818
	public Actor leader;

	// Token: 0x0400071B RID: 1819
	[NonSerialized]
	public UnitGroup army;

	// Token: 0x0400071C RID: 1820
	internal List<TileZone> zones = new List<TileZone>();

	// Token: 0x0400071D RID: 1821
	internal List<TileZone> neighbourZones = new List<TileZone>();

	// Token: 0x0400071E RID: 1822
	internal bool alive = true;

	// Token: 0x0400071F RID: 1823
	internal Building underConstructionBuilding;

	// Token: 0x04000720 RID: 1824
	private float cityTickTimer;

	// Token: 0x04000721 RID: 1825
	private float cityTickInterval = 6f;

	// Token: 0x04000722 RID: 1826
	internal float settlerTargetTimer = 15f;

	// Token: 0x04000723 RID: 1827
	internal float boatBuildTimer = 10f;

	// Token: 0x04000724 RID: 1828
	internal List<WorldTile> roadTilesToBuild = new List<WorldTile>();

	// Token: 0x04000725 RID: 1829
	private List<WorldTile> tilesToRemove = new List<WorldTile>();

	// Token: 0x04000726 RID: 1830
	internal City cityEnemy;

	// Token: 0x04000727 RID: 1831
	private int lastPopulationCheck;

	// Token: 0x04000728 RID: 1832
	private int samePopulationTicks;

	// Token: 0x04000729 RID: 1833
	internal TileZone settleTarget;

	// Token: 0x0400072A RID: 1834
	internal TileZone attackZone;

	// Token: 0x0400072B RID: 1835
	internal WorldTile _cityTile;

	// Token: 0x0400072C RID: 1836
	public float timer_action;

	// Token: 0x0400072D RID: 1837
	internal string _debug_nextPlannedBuilding;

	// Token: 0x0400072E RID: 1838
	internal Kingdom capturingBy;

	// Token: 0x0400072F RID: 1839
	public float captureTicks;

	// Token: 0x04000730 RID: 1840
	public int lastTicks;

	// Token: 0x04000731 RID: 1841
	internal float captureTimer;

	// Token: 0x04000732 RID: 1842
	internal Actor blacksmith;

	// Token: 0x04000733 RID: 1843
	internal Vector3 cityCenter;

	// Token: 0x04000734 RID: 1844
	[NonSerialized]
	internal Vector3 lastCityCenter;

	// Token: 0x04000735 RID: 1845
	public AiSystemCity ai;

	// Token: 0x04000736 RID: 1846
	private TextMesh cityText;

	// Token: 0x04000737 RID: 1847
	private TextMesh cityTextShadow;

	// Token: 0x04000738 RID: 1848
	internal bool unitsDirty;

	// Token: 0x04000739 RID: 1849
	private static readonly UnitProfession[] unitProfessions = (UnitProfession[])Enum.GetValues(typeof(UnitProfession));

	// Token: 0x0400073A RID: 1850
	internal float timer_warrior;

	// Token: 0x0400073B RID: 1851
	private int currentTaskID_unit;

	// Token: 0x0400073C RID: 1852
	private int currentTaskID_warrior;

	// Token: 0x0400073D RID: 1853
	private static List<Building> _buildingsToAbandon = new List<Building>();

	// Token: 0x0400073E RID: 1854
	internal int _opinion_king;

	// Token: 0x0400073F RID: 1855
	internal int _opinion_leader;

	// Token: 0x04000740 RID: 1856
	internal int _opinion_population;

	// Token: 0x04000741 RID: 1857
	internal int _opinion_zones;

	// Token: 0x04000742 RID: 1858
	internal int _opinion_distance;

	// Token: 0x04000743 RID: 1859
	internal int _opinion_capital;

	// Token: 0x04000744 RID: 1860
	internal int _opinion_loyalty_traits;

	// Token: 0x04000745 RID: 1861
	internal int _opinion_loyalty_mood;

	// Token: 0x04000746 RID: 1862
	internal int _opinion_new_city;

	// Token: 0x04000747 RID: 1863
	internal int _opinion_new_kingdom;

	// Token: 0x04000748 RID: 1864
	internal int _opinion_cities;

	// Token: 0x04000749 RID: 1865
	internal int _opinion_superior_enemies;

	// Token: 0x0400074A RID: 1866
	internal int _opinion_close_to_capital;

	// Token: 0x0400074B RID: 1867
	internal int _opinion_world_law;

	// Token: 0x0400074C RID: 1868
	internal int _opinion_culture;

	// Token: 0x0400074D RID: 1869
	internal int _opinion_total;

	// Token: 0x0400074E RID: 1870
	public int gold_in_tax;

	// Token: 0x0400074F RID: 1871
	public int gold_out_army;

	// Token: 0x04000750 RID: 1872
	public int gold_out_buildigs;

	// Token: 0x04000751 RID: 1873
	public int gold_out_homeless;

	// Token: 0x04000752 RID: 1874
	public int gold_change;

	// Token: 0x04000753 RID: 1875
	internal Dictionary<Kingdom, int> capturingUnits = new Dictionary<Kingdom, int>();

	// Token: 0x04000754 RID: 1876
	public static int LAST_CITY_HASH_ID = 0;

	// Token: 0x04000755 RID: 1877
	public int city_hash_id;
}
