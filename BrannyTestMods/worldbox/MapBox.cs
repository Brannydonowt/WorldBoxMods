using System;
using System.Collections.Generic;
using ai;
using EpPathFinding.cs;
using life.taxi;
using SleekRender;
using tools;
using tools.debug;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WorldBoxConsole;

// Token: 0x020002EE RID: 750
public class MapBox : MonoBehaviour
{
	// Token: 0x0600106F RID: 4207 RVA: 0x000900A0 File Offset: 0x0008E2A0
	private void Awake()
	{
		MapBox.instance = this;
		MapAction.init(this);
		this.joys.gameObject.SetActive(false);
		this.actions = new PowerActionsModule(this);
		this.unitGroupManager = new UnitGroupManager(this);
		this.islandsCalculator = new IslandsCalculator(this);
		this.unitZones = new UnitZones(this);
		this.worldLog = new WorldLog(this);
		this.cityPlaceFinder = new CityPlaceFinder(this);
		this.qualityChanger = base.GetComponent<QualityChanger>();
		this.transformCreatures = GameObject.Find("Creatures").transform;
		this.transformUnits = this.transformCreatures.Find("Units").transform;
		this.transformBuildings = GameObject.Find("Buildings").transform;
		this.transformShadows = GameObject.Find("Shadows").transform;
		this.transformTrees = GameObject.Find("Trees").transform;
		this.tilesDirty = new List<WorldTile>();
		this.tilesList = new List<WorldTile>();
		this.citiesList = new List<City>();
		this.units = new ActorContainer();
		this.buildings = new BuildingContainer();
		this.job_manager_buildings = new JobManagerBuildings();
		this.zone_camera = new ZoneCamera();
		this.kingdoms = new KingdomManager(this);
		this.cultures = new CultureManager(this);
		this.heat = new Heat();
		this.wind_direction = new Vector2(-0.1f, 0.2f);
		this.behaviours = new List<WorldBehaviour>();
		this.behavioursDict = new Dictionary<string, WorldBehaviour>();
		this.newBehaviour(80f, 0f, "disasters", new WorldBehaviourAction(WorldBehaviourActions.updateDisasters), null, true);
		this.newBehaviour(1f, 1f, "roads", new WorldBehaviourAction(WorldBehaviourActions.updateRoadDegeneration), null, true);
		this.newBehaviour(1f, 1f, "vegetation", new WorldBehaviourAction(WorldBehaviourActions.growVegetation), null, true);
		this.newBehaviour(2.5f, 2f, "unit_spawn", new WorldBehaviourAction(WorldBehaviourActions.updateUnitSpawn), null, true);
		this.newBehaviour(5f, 2f, "creep_degeneration", new WorldBehaviourAction(WorldBehaviourActionCreepDegeneration.checkCreep), new WorldBehaviourAction(WorldBehaviourActionCreepDegeneration.clear), true);
		this.newBehaviour(0.2f, 0f, "ocean", new WorldBehaviourAction(WorldBehaviourOcean.updateOcean), new WorldBehaviourAction(WorldBehaviourOcean.clear), true);
		this.newBehaviour(0.3f, 0.2f, "grass", new WorldBehaviourAction(WorldBehaviourActionGrass.updateGrass), new WorldBehaviourAction(WorldBehaviourActionGrass.clear), true);
		this.newBehaviour(0.2f, 0f, "fire", new WorldBehaviourAction(WorldBehaviourActionFire.updateFire), new WorldBehaviourAction(WorldBehaviourActionFire.clear), true);
		this.newBehaviour(0.1f, 0.2f, "burned_tiles", new WorldBehaviourAction(WorldBehaviourActionBurnedTiles.updateBurnedTiles), null, true);
		this.newBehaviour(2f, 1f, "erosion", new WorldBehaviourAction(WorldBehaviourActionErosion.updateErosion), null, true);
		this.newBehaviour(0.1f, 1f, "biome_inferno_fires", new WorldBehaviourAction(WorldBehaviourActionInferno.updateInfernoFireAction), null, false);
		this.newBehaviour(0.1f, 0.3f, "biome_inferno_tile_low_animations", new WorldBehaviourAction(WorldBehaviourActionInferno.updateInfernalLowAnimations), null, false);
		this.newBehaviour(0.1f, 1f, "biome_enchanted_sparks", new WorldBehaviourAction(WorldBehaviourActionEnchanted.updateSparksAction), null, false);
		this.newBehaviour(0.01f, 0.01f, "creep_biomass", new WorldBehaviourAction(WorldBehaviourActionCreepBiomass.updateBiomassTiles), null, false);
		this.newBehaviour(0.01f, 0.1f, "biome_low_swamp_animations", new WorldBehaviourAction(WorldBehaviourActionSwampAnimation.updateSwampTiles), null, false);
		this.newBehaviour(0.1f, 0.1f, "unit_temperatures", new WorldBehaviourAction(WorldBehaviourUnitTemperatures.updateUnitTemperatures), new WorldBehaviourAction(WorldBehaviourUnitTemperatures.clear), false);
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x000904A0 File Offset: 0x0008E6A0
	private void Start()
	{
		PlayerConfig.instance.start();
		new ExplosionChecker();
		this.camera = Camera.main;
		this.initiated = true;
		this.mapLayers = new List<MapLayer>();
		this.mapModules = new List<BaseModule>();
		this.flashEffects = GameObject.Find("Pixel Flash Effect").GetComponent<PixelFlashEffects>();
		this.mapLayers.Add(this.worldLayer = base.GetComponent<WorldLayer>());
		this.mapLayers.Add(this.unitLayer = GameObject.Find("[layer]Units").GetComponent<UnitLayer>());
		this.mapLayers.Add(this.zoneCalculator = GameObject.Find("[layer]Zone Calculator").GetComponent<ZoneCalculator>());
		this.mapLayers.Add(this.fireLayer = GameObject.Find("[layer]Fire Layer").GetComponent<FireLayer>());
		this.mapLayers.Add(this.burnedLayer = GameObject.Find("[layer]Burned Tiles").GetComponent<BurnedTilesLayer>());
		this.mapLayers.Add(this.explosionLayer = GameObject.Find("[layer]Explosions Effect").GetComponent<ExplosionsEffects>());
		this.mapLayers.Add(this.conwayLayer = GameObject.Find("[layer]Conway Life").GetComponent<ConwayLife>());
		this.mapLayers.Add(this.debugLayer = GameObject.Find("[layer]Debug Layer").GetComponent<DebugLayer>());
		this.mapLayers.Add(GameObject.Find("[layer]Debug Layer Cursor").GetComponent<DebugLayerCursor>());
		this.mapLayers.Add(this.pathFindingVisualiser);
		this.mapLayers.Add(this.flashEffects);
		this.mapModules.Add(this.roadsCalculator = GameObject.Find("[module]Roads Calculator").GetComponent<RoadsCalculator>());
		this.mapModules.Add(this.greyGooLayer);
		this.mapModules.Add(this.lavaLayer);
		this.mapChunkManager = new MapChunkManager(this);
		if (Config.isComputer || Config.isEditor)
		{
			GameObject original = (GameObject)Resources.Load("effects/PrefabUnitSelectionEffect");
			this._unitSelectEffect = Object.Instantiate<GameObject>(original, base.gameObject.transform).GetComponent<UnitSelectionEffect>();
			this._unitSelectEffect.create();
		}
		this.addNewSystem(this.spriteSystemUnitShadows = new GameObject().AddComponent<SpriteGroupSystemUnitShadows>());
		this.addNewSystem(this.spriteSystemFavorites = new GameObject().AddComponent<SpriteGroupSystemFavorites>());
		this.addNewSystem(this.spriteSystemBanners = new GameObject().AddComponent<SpriteGroupSystemUnitBanners>());
		this.addNewSystem(this.spriteSystemBuildingShadows = new GameObject().AddComponent<SpriteGroupSystemBuildingShadows>());
		this.addNewSystem(this.spriteSystemItems = new GameObject().AddComponent<SpriteGroupSystemUnitItems>());
		MapNamesManager.instance.create();
		foreach (SpriteGroupSystem<GroupSpriteObject> spriteGroupSystem in this.list_systems)
		{
			spriteGroupSystem.create();
		}
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x000907BC File Offset: 0x0008E9BC
	private void addNewSystem(SpriteGroupSystem<GroupSpriteObject> pSystem)
	{
		this.list_systems.Add(pSystem);
		pSystem.transform.parent = base.transform;
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x000907DC File Offset: 0x0008E9DC
	internal void getObjectsInChunks(WorldTile pTile, int pRadius = 3, MapObjectType pObjectType = MapObjectType.All)
	{
		this.temp_map_objects.Clear();
		this.checkChunk(pTile.chunk, pTile, pRadius, pObjectType);
		for (int i = 0; i < pTile.chunk.neighbours.Count; i++)
		{
			MapChunk pChunk = pTile.chunk.neighbours[i];
			this.checkChunk(pChunk, pTile, pRadius, pObjectType);
		}
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0009083C File Offset: 0x0008EA3C
	private void checkChunk(MapChunk pChunk, WorldTile pTile, int pRadius, MapObjectType pObjectType = MapObjectType.All)
	{
		for (int i = 0; i < pChunk.k_list_objects.Count; i++)
		{
			Kingdom kingdom = pChunk.k_list_objects[i];
			List<BaseSimObject> list = pChunk.k_dict_objects[kingdom];
			for (int j = 0; j < list.Count; j++)
			{
				BaseSimObject baseSimObject = list[j];
				if (!(baseSimObject == null) && baseSimObject.base_data.alive)
				{
					if (baseSimObject.isActor())
					{
						if (pObjectType == MapObjectType.Building)
						{
							goto IL_7B;
						}
					}
					else if (pObjectType == MapObjectType.Actor)
					{
						goto IL_7B;
					}
					if (pRadius == 0 || Toolbox.DistTile(baseSimObject.currentTile, pTile) <= (float)pRadius)
					{
						this.temp_map_objects.Add(baseSimObject);
					}
				}
				IL_7B:;
			}
		}
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x000908E6 File Offset: 0x0008EAE6
	private void initPlayServices()
	{
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000908E8 File Offset: 0x0008EAE8
	public Building getBuildingByID(string pID)
	{
		List<Building> simpleList = this.buildings.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Building building = simpleList[i];
			if (string.Equals(pID, building.data.objectID))
			{
				return building;
			}
		}
		return null;
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x00090930 File Offset: 0x0008EB30
	public Actor getActorByID(string pID)
	{
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (string.Equals(actor.data.actorID, pID))
			{
				return actor;
			}
		}
		return null;
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00090978 File Offset: 0x0008EB78
	public City getCityByID(string pID)
	{
		for (int i = 0; i < this.citiesList.Count; i++)
		{
			City city = this.citiesList[i];
			if (string.Equals(city.data.cityID, pID))
			{
				return city;
			}
		}
		return null;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x000909C0 File Offset: 0x0008EBC0
	private WorldBehaviour newBehaviour(float pInterval, float pRandomInterval, string pId, WorldBehaviourAction pUpdateAction, WorldBehaviourAction pClearAction = null, bool pEnabledOnMinimap = true)
	{
		WorldBehaviour worldBehaviour = new WorldBehaviour(pInterval, pRandomInterval, pId, pUpdateAction, pClearAction);
		worldBehaviour.enabled_on_minimap = pEnabledOnMinimap;
		this.behaviours.Add(worldBehaviour);
		this.behavioursDict.Add(pId, worldBehaviour);
		return worldBehaviour;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x000909FC File Offset: 0x0008EBFC
	public bool isGameplayControlsLocked()
	{
		return ScrollWindow.isWindowActive() || ScrollWindow.animationActive || RewardedAds.isShowing();
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00090A14 File Offset: 0x0008EC14
	internal bool calcPath(WorldTile pFrom, WorldTile pTargetTile, List<WorldTile> pSavePath)
	{
		pSavePath.Clear();
		StaticGrid staticGrid = this.searchGridGround;
		HeuristicMode heuristic = HeuristicMode.MANHATTAN;
		float weight = 2f;
		DiagonalMovement diagonalMovement;
		if (this.pathfindingParam.ocean)
		{
			diagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;
		}
		else
		{
			if (this.pathfindingParam.limit)
			{
			}
			diagonalMovement = DiagonalMovement.Always;
		}
		int maxOpenList = -1;
		if (this.pathfindingParam.roads)
		{
			weight = 1f;
			diagonalMovement = DiagonalMovement.Never;
			heuristic = HeuristicMode.EUCLIDEAN;
		}
		staticGrid.Reset();
		GridPos iStartPos = new GridPos(pFrom.pos.x, pFrom.pos.y);
		GridPos iEndPos = new GridPos(pTargetTile.pos.x, pTargetTile.pos.y);
		if (!pFrom.isSameIsland(pTargetTile) && !this.pathfindingParam.ocean)
		{
			pSavePath.Add(pFrom);
			pSavePath.Add(pTargetTile);
			this.pathFindingVisualiser.showPath(staticGrid, pSavePath);
			return true;
		}
		this.pathfindingParam.setGrid(staticGrid, iStartPos, iEndPos);
		this.pathfindingParam.DiagonalMovement = diagonalMovement;
		this.pathfindingParam.SetHeuristic(heuristic);
		this.pathfindingParam.maxOpenList = maxOpenList;
		this.pathfindingParam.Weight = weight;
		AStarFinder.FindPath(this.pathfindingParam, pSavePath);
		this.pathFindingVisualiser.showPath(staticGrid, pSavePath);
		return pSavePath.Count != 0;
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x00090B69 File Offset: 0x0008ED69
	private void clearNewMap()
	{
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x00090B6C File Offset: 0x0008ED6C
	public void startTheGame()
	{
		LogText.log("MapBox", "startTheGame", "st");
		Config.gameLoaded = true;
		Config.currentBrushData = Brush.get(Config.currentBrush);
		if (Config.isMobile)
		{
			this.playInterstitialAd.gameObject.SetActive(true);
		}
		Config.LOAD_TIME_CREATE = Time.realtimeSinceStartup;
		if (Config.loadSaveOnStart)
		{
			this.generateNewMap("clear");
			string slotSavePath = SaveManager.getSlotSavePath(Config.loadSaveOnStartSlot);
			this.saveManager.loadWorld(slotSavePath, false);
		}
		else if (Config.loadTestMap)
		{
			this.generateNewMap("clear");
			DebugMap.makeDebugMap(this);
		}
		else
		{
			string a = "";
			try
			{
				a = DateTime.Now.ToString("MM/dd");
			}
			catch (Exception)
			{
				a = "";
			}
			if (a == "04/01")
			{
				SaveManager.loadMapFromResources("mapTemplates/map_april_fools");
			}
			else if (this.gameStats.data.gameLaunches <= 3)
			{
				SaveManager.loadMapFromResources("mapTemplates/map_dragon");
			}
			else
			{
				this.generateNewMap("islands");
				this.addBuilding("volcano", this.GetTile(MapBox.width / 2, MapBox.height / 2), null, false, false, BuildPlacingType.New);
			}
		}
		Config.LOAD_TIME_GENERATE = Time.realtimeSinceStartup;
		base.GetComponent<SpriteRenderer>().enabled = true;
		this.uiGameplay.SetActive(true);
		if (Config.showStartupWindow)
		{
			if (PlayerConfig.instance.data.tutorialFinished || Config.skipTutorial)
			{
				this.selectedButtons.gameObject.SetActive(false);
				ScrollWindow.get("welcome").forceShow();
			}
			else
			{
				this.tutorial.startTutorial();
			}
		}
		if (Config.showConsoleOnStart)
		{
			this.console.Toggle();
		}
		PremiumElementsChecker.checkElements();
		LogText.log("MapBox", "startTheGame", "en");
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00090D44 File Offset: 0x0008EF44
	private void afterLoadEvent()
	{
		Debug.Log("afterLoadEvent--------------------------");
		PremiumElementsChecker.checkElements();
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x00090D58 File Offset: 0x0008EF58
	internal void centerCamera()
	{
		Vector3 position = this.camera.transform.position;
		position.x = (float)(MapBox.width / 2);
		position.y = (float)(MapBox.height / 2);
		this.camera.transform.position = position;
		this.camera.GetComponent<MoveCamera>().resetZoom();
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x00090DB8 File Offset: 0x0008EFB8
	private void createTiles()
	{
		this.tilesList.Clear();
		this.tilesList.Capacity = MapBox.width * MapBox.height;
		this.tilesMap = new WorldTile[MapBox.width, MapBox.height];
		GeneratorTool.Setup(this.tilesMap);
		int num = 0;
		for (int i = 0; i < MapBox.height; i++)
		{
			for (int j = 0; j < MapBox.width; j++)
			{
				WorldTile worldTile = new WorldTile(j, i, num, this);
				this.searchGridGround.SetTileNode(j, i, worldTile);
				num++;
				this.tilesMap[j, i] = worldTile;
				this.tilesList.Add(worldTile);
			}
		}
		GeneratorTool.GenerateTileNeighbours(this.tilesMap);
		this.zoneCalculator.generate();
		this.mapChunkManager.prepare();
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x00090E7F File Offset: 0x0008F07F
	private void testHashSet()
	{
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00090E84 File Offset: 0x0008F084
	private void GenerateMap(string pType = "islands")
	{
		if (!CustomTextureAtlas.filesExists())
		{
			ScrollWindow.showWindow("error_happened");
			return;
		}
		LogText.log("MapBox", "GenerateMap", "st");
		this.clearWorld();
		AssetManager.tiles.SetListTo("generator");
		this.worldLaws = new WorldLaws();
		this.worldLaws.init();
		this.mapStats = new MapStats();
		this.mapStats.name = WorldNameGenerator.generateName();
		Random.InitState(Random.Range(10, 1000));
		MapGenerator.generate(pType);
		LogText.log("MapBox", "GenerateMap", "en");
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x00090F28 File Offset: 0x0008F128
	internal void spawnBeehvies(int pAmount)
	{
		for (int i = 0; i < pAmount; i++)
		{
			WorldTile random = this.tilesList.GetRandom<WorldTile>();
			if (random.Type.grass)
			{
				this.addBuilding("beehive", random, null, true, false, BuildPlacingType.New);
			}
		}
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x00090F6C File Offset: 0x0008F16C
	internal void spawnResource(int pAmount, string pType, bool pRandomSize = true)
	{
		for (int i = 0; i < pAmount; i++)
		{
			WorldTile random = this.tilesList.GetRandom<WorldTile>();
			if (random.Type.ground)
			{
				string str = "";
				if (pRandomSize)
				{
					if (Toolbox.randomBool())
					{
						str = "_s";
					}
					else if (Toolbox.randomBool())
					{
						str = "_m";
					}
				}
				this.addBuilding(pType + str, random, null, true, false, BuildPlacingType.New);
			}
		}
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x00090FDC File Offset: 0x0008F1DC
	internal void tryGrowVegetation(WorldTile pTile, bool pSfx = false, string pTemplateID = null, bool pCheckLimit = true)
	{
		BuildingAsset buildingAsset = AssetManager.buildings.get(pTemplateID);
		if (pTile.building != null && !pTile.building.isNonUsable())
		{
			return;
		}
		if (buildingAsset == null)
		{
			return;
		}
		if (pCheckLimit)
		{
			int limit_per_zone = buildingAsset.limit_per_zone;
			if (pTile.zone.isInLimitFor(pTile, buildingAsset, limit_per_zone))
			{
				return;
			}
		}
		if (!this.canBuildFrom(pTile, buildingAsset, null, BuildPlacingType.New))
		{
			return;
		}
		this.addBuilding(buildingAsset.id, pTile, null, false, pSfx, BuildPlacingType.New);
		this.gameStats.data.treesGrown++;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00091068 File Offset: 0x0008F268
	internal void tryGrowVegetationRandom(WorldTile pTile, VegetationType pType, bool pOnStart = false, bool pCheckLimit = true)
	{
		BuildingAsset buildingAsset = null;
		if (!pTile.Type.grow_vegetation_auto)
		{
			return;
		}
		if (pTile.building != null && !pTile.building.isNonUsable())
		{
			return;
		}
		if (pType == VegetationType.Plants)
		{
			if (pTile.Type.grow_type_selector_plants != null)
			{
				buildingAsset = pTile.Type.grow_type_selector_plants(pTile);
			}
		}
		else if (pType == VegetationType.Trees && pTile.Type.grow_type_selector_trees != null)
		{
			buildingAsset = pTile.Type.grow_type_selector_trees(pTile);
		}
		if (buildingAsset == null)
		{
			return;
		}
		if (pCheckLimit)
		{
			int limit_per_zone = buildingAsset.limit_per_zone;
			if (pTile.zone.isInLimitFor(pTile, buildingAsset, limit_per_zone))
			{
				return;
			}
		}
		if (buildingAsset.vegetationRandomChance < Random.value)
		{
			return;
		}
		if (!this.canBuildFrom(pTile, buildingAsset, null, BuildPlacingType.New))
		{
			return;
		}
		this.addBuilding(buildingAsset.id, pTile, null, false, false, BuildPlacingType.New);
		this.gameStats.data.treesGrown++;
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x00091150 File Offset: 0x0008F350
	public City buildNewCity(TileZone pZone, CityData pData = null, Race pRace = null, bool pLoad = false, Kingdom pKingdom = null)
	{
		if (pData != null)
		{
			Kingdom kingdom = this.kingdoms.getKingdomByID(pData.kingdomID);
			if (kingdom == null)
			{
				kingdom = this.kingdoms.makeNewCivKingdom(null, AssetManager.raceLibrary.get(pData.race), pData.kingdomID, false);
			}
			if (!kingdom.isCiv())
			{
				return null;
			}
			if (pData.race == "" || pData.race == null)
			{
				return null;
			}
		}
		City component = Object.Instantiate<Transform>(this.prefab_city).gameObject.GetComponent<City>();
		component.create();
		if (pRace != null)
		{
			component.setRace(pRace);
		}
		if (pData != null)
		{
			component.loadCity(pData);
		}
		if (pLoad)
		{
			component.setKingdom(this.kingdoms.getKingdomByID(pData.kingdomID));
		}
		else
		{
			component.setKingdom(pKingdom);
			component.createNewCity();
		}
		component.setWorld();
		component.transform.parent = GameObject.Find("Cities").transform;
		if (component.kingdom == null)
		{
			this.kingdoms.makeNewCivKingdom(component, null, null, true);
		}
		component.addZone(pZone);
		if (!pLoad)
		{
			for (int i = 0; i < pZone.neighboursAll.Count; i++)
			{
				TileZone tileZone = pZone.neighboursAll[i];
				if (!(tileZone.city != null))
				{
					component.addZone(tileZone);
				}
			}
		}
		this.citiesList.Add(component);
		if (pRace != null)
		{
			AchievementLibrary.achievement4RaceCities.check();
		}
		this.cityPlaceFinder.cleanGoodList();
		return component;
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x000912C0 File Offset: 0x0008F4C0
	public City getEmptyKingdomCity(WorldTile pFromTile, Kingdom pKingdom)
	{
		List<City> list = new List<City>();
		for (int i = 0; i < pKingdom.cities.Count; i++)
		{
			City city = pKingdom.cities[i];
			if (city.status.population <= 40 && (city.status.population <= 5 || city.status.homesFree != 0))
			{
				list.Add(city);
			}
		}
		return Toolbox.getRandom<City>(list);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00091330 File Offset: 0x0008F530
	internal bool newAttack(BaseSimObject pByWho, Vector3 pPos, WorldTile pTile, BaseSimObject pTarget)
	{
		if (pTile == null)
		{
			return false;
		}
		int num = pByWho.curStats.targets;
		Toolbox.temp_list_objects_enemies.Clear();
		Toolbox.findEnemiesOfKingdomInChunk(pTile.chunk, pByWho.kingdom);
		if (Toolbox.temp_list_objects_enemies.Count == 0)
		{
			Toolbox.temp_list_objects_enemies.Add(new List<BaseSimObject>());
		}
		Toolbox.temp_list_objects_enemies[0].Add(pTarget);
		bool flag = Toolbox.randomChance(pByWho.curStats.s_crit_chance);
		bool result = false;
		for (int i = 0; i < Toolbox.temp_list_objects_enemies.Count; i++)
		{
			List<BaseSimObject> list = Toolbox.temp_list_objects_enemies[i];
			if (num == 0)
			{
				break;
			}
			list.Shuffle<BaseSimObject>();
			for (int j = 0; j < list.Count; j++)
			{
				BaseSimObject baseSimObject = list[j];
				if (num == 0)
				{
					break;
				}
				if (!(baseSimObject == this) && !(baseSimObject == null) && baseSimObject.base_data.alive && pByWho.canAttackTarget(baseSimObject) && Toolbox.Dist(baseSimObject.currentPosition.x, baseSimObject.currentPosition.y + baseSimObject.getZ(), pPos.x, pPos.y) < pByWho.curStats.areaOfEffect + baseSimObject.curStats.size)
				{
					num--;
					Vector3 pVector = baseSimObject.currentPosition;
					pVector = Vector3.MoveTowards(pPos, baseSimObject.currentPosition, baseSimObject.curStats.size * 0.9f);
					pVector.y += baseSimObject.getZ();
					if (!this.qualityChanger.lowRes && baseSimObject.spriteRenderer != null && baseSimObject.spriteRenderer.enabled)
					{
						if (flag)
						{
							this.stackEffects.get("hitCritical").spawnAt(pVector, 0.1f);
						}
						else
						{
							this.stackEffects.get("hit").spawnAt(pVector, 0.1f);
						}
					}
					this.applyAttack(pByWho, baseSimObject, pPos, pTile, flag);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00091554 File Offset: 0x0008F754
	private void applyAttack(BaseSimObject pAttacker, BaseSimObject pTarget, Vector3 pAttackPos, WorldTile pTile, bool pCritical = false)
	{
		int num = pAttacker.curStats.damage;
		if (pCritical)
		{
			num = (int)((float)pAttacker.curStats.damage * pAttacker.curStats.damageCritMod);
		}
		pTarget.getHit((float)num, true, AttackType.Other, pAttacker, true);
		if (pAttacker.isActor())
		{
			pAttacker.a.addExperience(2);
			ItemAsset weaponAsset = pAttacker.a.getWeaponAsset();
			if (weaponAsset.attackAction != null)
			{
				weaponAsset.attackAction(pTarget, pTile);
			}
		}
		if (pTarget.isActor() && pAttacker.isActor())
		{
			if (pTarget.a.stats.canTurnIntoZombie && pAttacker.a.haveTrait("zombie") && Toolbox.randomChance(0.3f))
			{
				pTarget.a.addTrait("infected");
			}
			if (!pTarget.base_data.alive && pAttacker.a.stats.animal && pAttacker.a.stats.diet_meat && pTarget.a.stats.source_meat)
			{
				pAttacker.a.restoreStatsFromEating(70, 0f, true);
			}
		}
		float num2;
		if (pTarget.base_data.health > 0)
		{
			num2 = 0.2f * pAttacker.curStats.knockback;
		}
		else
		{
			num2 = 0.3f * pAttacker.curStats.knockback;
		}
		num2 -= num2 * (pTarget.curStats.knockbackReduction / 100f);
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		if (num2 > 0f && pTarget.isActor())
		{
			float angle = Toolbox.getAngle(pTarget.transform.position.x, pTarget.transform.position.y, pAttackPos.x, pAttackPos.y);
			pTarget.a.addForce(-Mathf.Cos(angle) * num2, -Mathf.Sin(angle) * num2, num2);
		}
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x00091733 File Offset: 0x0008F933
	public void destroyCity(City pCity)
	{
		WorldLog.logCityDestroyed(pCity);
		pCity.destroyCity();
		Object.Destroy(pCity.gameObject);
		this.citiesList.Remove(pCity);
		this.cityPlaceFinder.setDirty();
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x00091764 File Offset: 0x0008F964
	public void clearWorld()
	{
		LogText.log("MapBox", "clearWorld", "st");
		Config.worldLoading = true;
		Analytics.worldLoading();
		BaseSimObject.LAST_HASH_ID = 0;
		Config.setControllableCreature(null);
		ExplosionChecker.instance.clear();
		this.debugLayer.clear();
		this.selectedButtons.unselectAll();
		this.firstClick = true;
		this.worldLog.clear();
		this.islandsCalculator.clear();
		RegionLinkHashes.clear();
		this.unitZones.clear();
		MapNamesManager.instance.world = this;
		MapNamesManager.instance.clear();
		this.mapChunkManager.clearAll();
		this.qualityChanger.reset();
		this.tilemap.clear();
		Config.paused = false;
		this.selectedButtons.checkToggleIcons();
		this.heat.clear();
		using (List<WorldTile>.Enumerator enumerator = this.tilesList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				WorldTile worldTile = enumerator.Current;
				worldTile.clear();
			}
			goto IL_12E;
		}
		IL_FD:
		City city = this.citiesList[0];
		this.citiesList.RemoveAt(0);
		Object.Destroy(city.gameObject);
		this.citiesList.Remove(city);
		IL_12E:
		if (this.citiesList.Count <= 0)
		{
			foreach (TileType tileType in AssetManager.tiles.list)
			{
				tileType.hashsetClear();
			}
			foreach (TopTileType topTileType in AssetManager.topTiles.list)
			{
				topTileType.hashsetClear();
			}
			foreach (WorldBehaviour worldBehaviour in this.behaviours)
			{
				worldBehaviour.clear();
			}
			foreach (object obj in this.transformShadows)
			{
				Object.Destroy(((Transform)obj).gameObject);
			}
			AutoSaveManager.reset();
			AssetManager.unitStats.clear();
			AssetManager.raceLibrary.clear();
			this.dropManager.clear();
			this.citiesList.Clear();
			this.kingdoms.clear();
			this.cultures.clear();
			this.waveController.clear();
			this.cloudController.clear();
			this.particlesFire.clear();
			this.particlesSmoke.clear();
			this.stackEffects.clear();
			this.clearSimObjects();
			foreach (MapLayer mapLayer in this.mapLayers)
			{
				mapLayer.clear();
			}
			foreach (BaseModule baseModule in this.mapModules)
			{
				baseModule.clear();
			}
			foreach (WorldTile worldTile2 in this.tilesDirty)
			{
				worldTile2.dirty = false;
			}
			this.tilesDirty.Clear();
			HistoryHud.Clear();
			LogText.log("MapBox", "clearWorld", "en");
			return;
		}
		goto IL_FD;
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x00091B3C File Offset: 0x0008FD3C
	internal void clearSimObjects()
	{
		this.units.checkAddRemove();
		this.buildings.checkAddRemove();
		foreach (Actor pActor in this.units)
		{
			this.destroyActor(pActor);
		}
		foreach (Building pBuilding in this.buildings)
		{
			this.removeBuildingFully(pBuilding);
		}
		this.units.Clear();
		this.buildings.Clear();
		this.job_manager_buildings.clear();
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x00091BFC File Offset: 0x0008FDFC
	public WorldTile GetTile(int x, int y)
	{
		if (x < 0 || x >= MapBox.width || y < 0 || y >= MapBox.height)
		{
			return null;
		}
		return this.tilesMap[x, y];
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x00091C25 File Offset: 0x0008FE25
	public WorldTile GetTileSimple(int x, int y)
	{
		return this.tilesMap[x, y];
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x00091C34 File Offset: 0x0008FE34
	public void setMapSize(int pWidth, int pHeight)
	{
		Config.ZONE_AMOUNT_X = pWidth;
		Config.ZONE_AMOUNT_Y = pHeight;
		MapBox.width = Config.ZONE_AMOUNT_X * 64;
		MapBox.height = Config.ZONE_AMOUNT_Y * 64;
		if (this.lastUsedWidth != MapBox.width || this.lastUsedHeight != MapBox.height)
		{
			this.recreateSizes();
		}
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x00091C87 File Offset: 0x0008FE87
	private void afterTransitionGeneration()
	{
		this.generateNewMap(this.islandTypeToGenerate);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x00091C95 File Offset: 0x0008FE95
	public void clickGenerateNewMap(string pType = "islands")
	{
		this.islandTypeToGenerate = pType;
		this.transitionScreen.startTransition(new LoadingScreen.TransitionAction(this.afterTransitionGeneration));
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x00091CB8 File Offset: 0x0008FEB8
	public void generateNewMap(string pType = "islands")
	{
		if (!this.initiated)
		{
			return;
		}
		LogText.log("MapBox", "generateNewMap", "st");
		Config.worldLoading = true;
		Analytics.worldLoading();
		if (pType != "custom")
		{
			Config.customMapSize = Config.customMapSizeDefault;
		}
		else
		{
			AchievementLibrary.achievementCustomWorld.check();
		}
		Config.ZONE_AMOUNT_Y = (Config.ZONE_AMOUNT_X = MapSizePresset.getSize(Config.customMapSize));
		this.setMapSize(Config.ZONE_AMOUNT_X, Config.ZONE_AMOUNT_Y);
		if (!this.first_gen)
		{
			Analytics.LogEvent("clicked", "generate", pType);
		}
		this.first_gen = false;
		if (pType == "custom")
		{
			ScrollWindow.moveAllToLeftAndRemove(true);
		}
		this.GenerateMap(pType);
		this.finishMakingWorld();
		LogText.log("MapBox", "generateNewMap", "en");
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x00091D8D File Offset: 0x0008FF8D
	public void finishMakingWorld()
	{
		this.mapChunkManager.allDirty();
		this.centerCamera();
		Config.worldLoading = false;
		Analytics.worldLoaded();
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x00091DAC File Offset: 0x0008FFAC
	private void recreateSizes()
	{
		this.lastUsedWidth = MapBox.width;
		this.lastUsedHeight = MapBox.height;
		this.searchGridGround = new StaticGrid(MapBox.width, MapBox.height, null);
		this.createTiles();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (MapLayer mapLayer in this.mapLayers)
		{
			mapLayer.createTextureNew();
		}
		Debug.Log(string.Concat(new string[]
		{
			"texture creation ",
			MapBox.width.ToString(),
			":",
			MapBox.height.ToString(),
			"... ",
			(Time.realtimeSinceStartup - realtimeSinceStartup).ToString()
		}));
		this.mapBorder.generateTexture();
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x00091E94 File Offset: 0x00090094
	public Actor getActorNearCursor()
	{
		return ActionLibrary.getActorNearPos(MapBox.instance.getMousePos());
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x00091EA8 File Offset: 0x000900A8
	public Vector2 getMousePos()
	{
		Vector2 v = Input.mousePosition;
		return this.camera.ScreenToWorldPoint(v);
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x00091ED8 File Offset: 0x000900D8
	public WorldTile getMouseTilePos()
	{
		Vector2Int vector2Int;
		if (!PixelDetector.GetSpritePixelColorUnderMousePointer(this, out vector2Int))
		{
			return null;
		}
		return this.GetTile(vector2Int.x, vector2Int.y);
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x00091F05 File Offset: 0x00090105
	private void showKingdomWindow()
	{
		ScrollWindow.showWindow("kingdom");
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x00091F14 File Offset: 0x00090114
	internal void Clicked(Vector2Int pPos, GodPower pPower = null, BrushData pBrushData = null)
	{
		if (pPower == null)
		{
			pPower = this.selectedButtons.selectedButton.godPower;
		}
		if (pPower.requiresPremium && !Config.havePremium)
		{
			ScrollWindow.showWindow("steam");
			return;
		}
		WorldTile tile = this.GetTile(pPos.x, pPos.y);
		LogText.log("Clicked", string.Concat(new string[]
		{
			pPower.id,
			" ",
			tile.pos.x.ToString(),
			":",
			tile.pos.y.ToString()
		}), "");
		if (pPower.id == "relations")
		{
			if (tile.zone.city != null)
			{
				if (Config.selectedKingdom != tile.zone.city.kingdom)
				{
					Config.selectedKingdom = tile.zone.city.kingdom;
					return;
				}
				this.showKingdomWindow();
			}
			return;
		}
		if (this.firstPressedType == null)
		{
			this.firstPressedType = tile.main_type;
			this.firstPressedTopType = tile.top_type;
		}
		string pID = Config.currentBrush;
		if (pPower != null && !string.IsNullOrEmpty(pPower.forceBrush))
		{
			pID = pPower.forceBrush;
		}
		if (pBrushData == null)
		{
			Brush.get(pID);
		}
		if (pPower.click_power_action != null || pPower.click_power_brush_action != null)
		{
			if (pPower.click_power_brush_action != null)
			{
				pPower.click_power_brush_action(tile, pPower);
			}
			else if (pPower.click_power_action != null)
			{
				pPower.click_power_action(tile, pPower);
			}
			DiscordTracker.PlusOne();
			return;
		}
		if (pPower.click_action != null || pPower.click_brush_action != null)
		{
			if (pPower.click_brush_action != null)
			{
				pPower.click_brush_action(tile, pPower.id);
			}
			else if (pPower.click_action != null)
			{
				pPower.click_action(tile, pPower.id);
			}
			DiscordTracker.PlusOne();
			return;
		}
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x000920FC File Offset: 0x000902FC
	private void checkEmptyClick()
	{
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		Vector2Int vector2Int;
		if (!PixelDetector.GetSpritePixelColorUnderMousePointer(this, out vector2Int) || vector2Int.x == -1)
		{
			this.lastClick.Set(-1, -1);
			return;
		}
		if (this.GetTile(vector2Int.x, vector2Int.y) == null)
		{
			return;
		}
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (Toolbox.Dist(actor.currentTile.posV3.x, actor.currentTile.posV3.y, (float)vector2Int.x, (float)vector2Int.y) <= 10f)
			{
				if (actor.stats.id.Contains("dragon"))
				{
					actor.GetComponent<Dragon>().clickToWakeup();
				}
				if (actor.stats.id.Contains("UFO"))
				{
					actor.GetComponent<UFO>().click();
				}
			}
		}
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x000921F8 File Offset: 0x000903F8
	internal void Clicked()
	{
		this.alreadyUsedPower = true;
		Vector2Int pPos;
		if (!PixelDetector.GetSpritePixelColorUnderMousePointer(this, out pPos) || pPos.x == -1)
		{
			this.lastClick.Set(-1, -1);
			return;
		}
		GodPower godPower = this.selectedButtons.selectedButton.godPower;
		string pID = Config.currentBrush;
		if (godPower != null && !string.IsNullOrEmpty(godPower.forceBrush))
		{
			pID = godPower.forceBrush;
		}
		BrushData brushData = Brush.get(pID);
		if (brushData.continuous && godPower.drawLines)
		{
			if (this.lastClick.x != -1 && (pPos.x != this.lastClick.x || pPos.y != this.lastClick.y))
			{
				int num = (int)(Toolbox.Dist((float)pPos.x, (float)pPos.y, (float)this.lastClick.x, (float)this.lastClick.y) / (float)(brushData.size + 1)) + 1;
				for (int i = 0; i < num; i++)
				{
					Vector3 a = new Vector3((float)pPos.x, (float)pPos.y);
					Vector3 b = new Vector3((float)this.lastClick.x, (float)this.lastClick.y);
					Vector3 vector = Vector3.Lerp(a, b, (float)i / (float)num);
					Vector2Int pPos2 = new Vector2Int((int)vector.x, (int)vector.y);
					if (this.GetTile(pPos2.x, pPos2.y) != null)
					{
						this.Clicked(pPos2, godPower, null);
					}
				}
			}
			this.Clicked(pPos, godPower, null);
		}
		else
		{
			this.Clicked(pPos, godPower, null);
		}
		this.firstClick = false;
		this.lastClick.Set(pPos.x, pPos.y);
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x000923B8 File Offset: 0x000905B8
	internal Building loadBuilding(BuildingData pBuildingData)
	{
		WorldTile tileSimple = this.GetTileSimple(pBuildingData.mainX, pBuildingData.mainY);
		Building building = this.addBuilding(pBuildingData.templateID, tileSimple, pBuildingData, true, false, BuildPlacingType.Load);
		if (building == null)
		{
			return null;
		}
		if (!pBuildingData.cityID.Equals(""))
		{
			City cityByID = this.getCityByID(pBuildingData.cityID);
			if (cityByID != null)
			{
				cityByID.addBuilding(building);
			}
		}
		return building;
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x00092428 File Offset: 0x00090628
	internal Building addBuilding(string pID, WorldTile pTile, BuildingData pData = null, bool pCheckForBuild = false, bool pSfx = false, BuildPlacingType pType = BuildPlacingType.New)
	{
		if (pCheckForBuild && !this.canBuildFrom(pTile, AssetManager.buildings.get(pID), null, pType))
		{
			return null;
		}
		Building building = Object.Instantiate<Building>(PrefabLibrary.instance.building);
		building.create();
		building.setBuilding(pTile, AssetManager.buildings.get(pID), pData);
		if (pData != null)
		{
			building.finishScaleTween();
			building.setAnimData(pData.frameID);
			building.applyAnimDataToAnimation();
			building.spriteAnimation.forceUpdateFrame();
		}
		building.transform.parent = this.transformBuildings;
		if (building.stats.buildingType == BuildingType.Tree)
		{
			building.transform.parent = building.transform.parent.Find("Trees");
		}
		building.resetShadow();
		this.buildings.Add(building);
		if (pSfx && building.stats.sfx != "none")
		{
			Sfx.play(building.stats.sfx, true, -1f, -1f);
		}
		if (Config.timeScale > 10f)
		{
			building.finishScaleTween();
		}
		return building;
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0009253C File Offset: 0x0009073C
	internal bool canBuildFrom(WorldTile pTile, BuildingAsset pTemplate, City pCity, BuildPlacingType pType = BuildPlacingType.New)
	{
		int num = pTile.x - pTemplate.fundament.left;
		int num2 = pTile.y - pTemplate.fundament.bottom;
		int num3 = pTemplate.fundament.right + pTemplate.fundament.left + 1;
		int num4 = pTemplate.fundament.top + pTemplate.fundament.bottom + 1;
		bool flag = false;
		bool flag2 = false;
		MapBox.temp_list_tiles.Clear();
		WorldTile worldTile = (pCity != null) ? pCity.getTile() : null;
		if (pCity != null && worldTile == null)
		{
			return false;
		}
		for (int i = 0; i < num3; i++)
		{
			for (int j = 0; j < num4; j++)
			{
				WorldTile tile = this.GetTile(num + i, num2 + j);
				if (tile == null)
				{
					return false;
				}
				MapBox.temp_list_tiles.Add(tile);
				if (pTemplate.docks)
				{
					if (tile.Type.ocean && OceanHelper.goodForNewDock(tile))
					{
						flag2 = true;
					}
					if (tile.Type.ground)
					{
						flag = true;
					}
				}
				if (pCity != null && pCity.getTile() != null)
				{
					if (!pTemplate.docks && !tile.isSameIsland(pCity.getTile()))
					{
						return false;
					}
					if (tile.zone.city != pCity)
					{
						return false;
					}
					if (pTemplate.onlyBuildTiles && !tile.Type.canBuildOn)
					{
						return false;
					}
				}
				if (pTemplate.canBePlacedOnlyOn != null && pTemplate.canBePlacedOnlyOn.Count > 0 && !pTemplate.canBePlacedOnlyOn.Contains(tile.Type.id))
				{
					return false;
				}
				if (pTemplate.fauna && !tile.canGrow())
				{
					return false;
				}
				BuildingType buildingType = pTemplate.buildingType;
				if (tile.Type.liquid && !pTemplate.canBePlacedOnLiquid)
				{
					return false;
				}
				if (pTemplate.destroyOnLiquid && tile.Type.ocean)
				{
					return false;
				}
				if (pTemplate.buildingType == BuildingType.Tree && tile.building != null && tile.building.stats.buildingType != BuildingType.Plant && !tile.building.isNonUsable())
				{
					return false;
				}
				if (!tile.canBuildOn(pTemplate))
				{
					return false;
				}
				if (pTemplate.checkForCloseBuilding && pType == BuildPlacingType.New)
				{
					if (i == 0)
					{
						if (this.isBuildingNearby(tile.tile_left))
						{
							return false;
						}
					}
					else if (i == num3 - 1 && this.isBuildingNearby(tile.tile_right))
					{
						return false;
					}
					if (j == 0)
					{
						if (this.isBuildingNearby(tile.tile_down))
						{
							return false;
						}
						if (tile.tile_down != null && this.isBuildingNearby(tile.tile_down.tile_down))
						{
							return false;
						}
					}
					else if (j == num4 - 1)
					{
						if (this.isBuildingNearby(tile.tile_up))
						{
							return false;
						}
						if (tile.tile_up != null && this.isBuildingNearby(tile.tile_up.tile_up))
						{
							return false;
						}
					}
				}
			}
		}
		if (pTemplate.docks && pType == BuildPlacingType.New)
		{
			if (flag2 && !flag)
			{
				for (int k = 0; k < MapBox.temp_list_tiles.Count; k++)
				{
					WorldTile worldTile2 = MapBox.temp_list_tiles[k];
					for (int l = 0; l < worldTile2.neighbours.Count; l++)
					{
						WorldTile worldTile3 = worldTile2.neighbours[l];
						if (worldTile3.Type.ground && worldTile3.region.island == ((worldTile != null) ? worldTile.region.island : null))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x000928AA File Offset: 0x00090AAA
	internal bool isBuildingNearby(WorldTile pTile)
	{
		return pTile == null || (pTile.building != null && !pTile.building.isNonUsable() && pTile.building.stats.cityBuilding);
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x000928E4 File Offset: 0x00090AE4
	public void loopWithBrushPower(WorldTile pCenterTile, BrushData pBrush, PowerAction pAction, GodPower pPower)
	{
		for (int i = 0; i < pBrush.pos.Count; i++)
		{
			BrushPixelData brushPixelData = pBrush.pos[i];
			int num = pCenterTile.x + brushPixelData.x;
			int num2 = pCenterTile.y + brushPixelData.y;
			if (num >= 0 && num < MapBox.width && num2 >= 0 && num2 < MapBox.height)
			{
				WorldTile tileSimple = MapBox.instance.GetTileSimple(num, num2);
				pAction(tileSimple, pPower);
			}
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x00092968 File Offset: 0x00090B68
	public void loopWithBrush(WorldTile pCenterTile, BrushData pBrush, PowerActionWithID pAction, string pPowerID)
	{
		for (int i = 0; i < pBrush.pos.Count; i++)
		{
			BrushPixelData brushPixelData = pBrush.pos[i];
			int num = pCenterTile.x + brushPixelData.x;
			int num2 = pCenterTile.y + brushPixelData.y;
			if (num >= 0 && num < MapBox.width && num2 >= 0 && num2 < MapBox.height)
			{
				WorldTile tileSimple = MapBox.instance.GetTileSimple(num, num2);
				pAction(tileSimple, pPowerID);
			}
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x000929EC File Offset: 0x00090BEC
	public void checkCityZone(WorldTile pTile)
	{
		if (pTile.zone.city == null)
		{
			return;
		}
		bool flag = false;
		using (HashSet<Building>.Enumerator enumerator = pTile.zone.buildings.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!(enumerator.Current.city != pTile.zone.city))
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			pTile.zone.city.removeZone(pTile.zone, false);
		}
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x00092A88 File Offset: 0x00090C88
	public void spawnFlash(WorldTile pTile, int pRadius)
	{
		ExplosionFlash explosionFlash = (ExplosionFlash)this.stackEffects.get("explosionWave").spawnNew();
		if (explosionFlash == null)
		{
			return;
		}
		explosionFlash.startFlash(pTile, pRadius);
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x00092AB0 File Offset: 0x00090CB0
	public static void spawnLightning(WorldTile pTile, float pScale = 0.25f)
	{
		BaseEffectController baseEffectController = MapBox.instance.stackEffects.get("lightning");
		BaseEffect baseEffect = (baseEffectController != null) ? baseEffectController.spawnAt(pTile, pScale) : null;
		if (baseEffect == null)
		{
			return;
		}
		int pRad = (int)(pScale * 25f);
		MapAction.damageWorld(pTile, pRad, AssetManager.terraform.get("lightning"));
		Sfx.play("lightning", true, -1f, -1f);
		baseEffect.sprRenderer.flipX = Toolbox.randomBool();
		MapAction.checkLightningAction(pTile.pos, pRad);
		MapAction.checkSantaHit(pTile.pos, pRad);
		MapAction.checkUFOHit(pTile.pos, pRad);
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x00092B54 File Offset: 0x00090D54
	public void spawnMeteorite(WorldTile pTile)
	{
		Meteorite component = Object.Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/PrefabMeteorite", typeof(GameObject))).gameObject.GetComponent<Meteorite>();
		component.spawnOn(pTile);
		component.transform.parent = GameObject.Find("Meteorites").transform;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x00092BAC File Offset: 0x00090DAC
	public Actor spawnAndLoadUnit(string pStatsID, ActorData pSaveData, WorldTile pTile)
	{
		int health = pSaveData.status.health;
		Actor actor = this.createNewUnit(pStatsID, pTile, null, 0f, pSaveData);
		if (actor == null)
		{
			return null;
		}
		actor.setData(pSaveData.status);
		if (pSaveData.inventory != null)
		{
			actor.inventory = pSaveData.inventory;
		}
		if (actor.stats.use_items)
		{
			actor.equipment.load(pSaveData.items);
		}
		if (actor.stats.unit)
		{
			actor.reloadInventory();
		}
		actor.loadFromSave();
		actor.cancelAllBeh(null);
		actor.updateStats();
		if (health > actor.curStats.health)
		{
			health = actor.curStats.health;
		}
		actor.data.health = health;
		City cityByID = this.getCityByID(pSaveData.cityID);
		if (cityByID != null)
		{
			if (actor.stats.isBoat)
			{
				actor.setKingdom(cityByID.kingdom);
				actor.GetComponent<Boat>().updateTexture();
				actor.setCity(cityByID);
			}
			else
			{
				cityByID.addNewUnit(actor, true, true);
			}
		}
		if (actor.stats.id == "livingPlants" || actor.stats.id == "livingHouse")
		{
			string[] array = pSaveData.status.special_graphics.Split(new char[]
			{
				'#'
			});
			string pID = array[0];
			int num = int.Parse(array[1]);
			BuildingAsset buildingAsset = AssetManager.buildings.get(pID);
			actor.spriteRenderer.sprite = buildingAsset.sprites.animationData[num].main[0];
		}
		return actor;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x00092D40 File Offset: 0x00090F40
	public Actor spawnNewUnit(string pStatsID, WorldTile pTile, string pJob = "", float pSpawnHeight = 6f)
	{
		Actor actor = this.createNewUnit(pStatsID, pTile, pJob, pSpawnHeight, null);
		if (actor.stats.unit)
		{
			actor.data.age = 18;
			for (int i = 0; i < 6; i++)
			{
				actor.data.updateAttributes(actor.stats, actor.race, true);
			}
			actor.setKingdom(this.kingdoms.dict_hidden["nomads_" + actor.stats.race]);
			actor.setStatsDirty();
		}
		actor.setSkinSet(pTile.Type.forceUnitSkinSet);
		return actor;
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x00092DDC File Offset: 0x00090FDC
	public Actor createNewUnit(string pStatsID, WorldTile pTile, string pJob = null, float pZHeight = 0f, ActorData pData = null)
	{
		ActorStats actorStats = AssetManager.unitStats.get(pStatsID);
		if (actorStats == null)
		{
			return null;
		}
		Actor component;
		try
		{
			component = Object.Instantiate<GameObject>((GameObject)Resources.Load("actors/" + actorStats.prefab, typeof(GameObject))).gameObject.GetComponent<Actor>();
		}
		catch (Exception)
		{
			Debug.Log("Tried to create actor: " + actorStats.id);
			Debug.Log("Failed to load prefab for actor: " + actorStats.prefab);
			return null;
		}
		component.transform.name = actorStats.id;
		component.new_creature = true;
		if (pData != null)
		{
			component.new_creature = false;
		}
		component.setWorld();
		component.loadStats(actorStats);
		if (pData != null)
		{
			component.data = pData.status;
			component.professionAsset = AssetManager.professions.get(pData.status.profession);
		}
		if (component.new_creature)
		{
			component.newCreature((int)(this.gameStats.data.gameTime + (double)this.units.Count));
		}
		component.transform.position = pTile.posV3;
		component.spawnOn(pTile, pZHeight);
		component.create();
		LogText.log("createNewUnit", component.stats.id + ", " + component.data.actorID, "");
		if (component.stats.kingdom != "")
		{
			component.setKingdom(this.kingdoms.dict_hidden[component.stats.kingdom]);
		}
		if (component.stats.hideOnMinimap)
		{
			component.transform.parent = this.transformUnits;
		}
		else
		{
			component.transform.parent = this.transformCreatures;
		}
		this.units.Add(component);
		return component;
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x00092FC4 File Offset: 0x000911C4
	public void destroyActor(Actor pActor)
	{
		pActor.data.alive = false;
		if (pActor.kingdom != null)
		{
			pActor.kingdom.removeUnit(pActor);
		}
		if (pActor.city != null)
		{
			pActor.city.removeCitizen(pActor, false, AttackType.Other);
		}
		this.units.Remove(pActor);
		this._list_to_clean_effects.Clear();
		for (int i = 0; i < pActor.transform.childCount; i++)
		{
			Transform child = pActor.transform.GetChild(i);
			if (child.GetComponent<BaseEffect>() != null)
			{
				this._list_to_clean_effects.Add(child.GetComponent<BaseEffect>());
			}
		}
		for (int j = 0; j < this._list_to_clean_effects.Count; j++)
		{
			BaseEffect baseEffect = this._list_to_clean_effects[j];
			baseEffect.makeParentController();
			baseEffect.kill();
		}
		pActor.object_destroyed = true;
		Object.Destroy(pActor.gameObject);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x000930A5 File Offset: 0x000912A5
	public void removeBuildingFully(Building pBuilding)
	{
		pBuilding.data.alive = false;
		this.buildings.Remove(pBuilding);
		this.job_manager_buildings.removeObject(pBuilding);
		pBuilding.object_destroyed = true;
		Object.Destroy(pBuilding.gameObject);
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x000930E0 File Offset: 0x000912E0
	public void applyForce(WorldTile pTile, int pRad = 10, float pSpeedForce = 1.5f, bool forceOut = true, bool useOnNature = false, int pDamage = 0, string[] pIgnoreKingdoms = null, Actor pByActor = null, TerraformOptions pOptions = null)
	{
		this._force_temp_actor_list.Clear();
		Toolbox.fillListWithUnitsFromChunk(pTile.chunk, this._force_temp_actor_list);
		for (int i = 0; i < pTile.chunk.neighboursAll.Count; i++)
		{
			Toolbox.fillListWithUnitsFromChunk(pTile.chunk.neighboursAll[i], this._force_temp_actor_list);
		}
		float num = 1f;
		for (int j = 0; j < this._force_temp_actor_list.Count; j++)
		{
			Actor actor = this._force_temp_actor_list[j];
			if (pOptions == null || !actor.stats.very_high_flyer || pOptions.applies_to_high_flyers)
			{
				float num2 = Toolbox.DistVec2(actor.currentTile.pos, pTile.pos);
				if ((useOnNature || !actor.race.nature) && num2 <= (float)pRad)
				{
					if (pIgnoreKingdoms != null)
					{
						bool flag = false;
						foreach (string b in pIgnoreKingdoms)
						{
							Kingdom kingdom = actor.kingdom;
							if (((kingdom != null) ? kingdom.id : null) == b)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							goto IL_1CE;
						}
					}
					if (actor.stats.canBeHurtByPowers)
					{
						actor.getHit((float)pDamage, true, AttackType.Other, pByActor, true);
					}
					float num3 = pSpeedForce - pSpeedForce * (actor.curStats.knockbackReduction / 100f);
					if (num3 < 0f)
					{
						num3 = 0f;
					}
					if (num3 > 0f)
					{
						float angle = Toolbox.getAngle((float)actor.currentTile.x, (float)actor.currentTile.y, (float)pTile.x, (float)pTile.y);
						if (forceOut)
						{
							actor.addForce(-Mathf.Cos(angle) * num3 * num, -Mathf.Sin(angle) * num3 * num, num);
						}
						else
						{
							actor.addForce(Mathf.Cos(angle) * num3 * num, Mathf.Sin(angle) * num3 * num, num);
						}
					}
				}
			}
			IL_1CE:;
		}
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x000932D4 File Offset: 0x000914D4
	internal void stopAttacksFor(bool pMonsters)
	{
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (actor.attackTarget != null && (actor.kingdom.asset.mobs || actor.attackTarget.kingdom.asset.mobs) == pMonsters)
			{
				actor.cancelAllBeh(null);
			}
		}
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00093348 File Offset: 0x00091548
	public void allDirty()
	{
		for (int i = 0; i < this.tilesList.Count; i++)
		{
			WorldTile worldTile = this.tilesList[i];
			this.tilesDirty.Add(worldTile);
			worldTile.dirty = true;
		}
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0009338B File Offset: 0x0009158B
	private void OnApplicationFocus(bool focus)
	{
		this.hasFocus = focus;
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x00093394 File Offset: 0x00091594
	private void updateShake(float pElapsed)
	{
		if (this.shakeTimer == 0f)
		{
			return;
		}
		if (this.shakeTimer > 0f)
		{
			this.shakeTimer -= pElapsed;
		}
		if (this.shakeTimer <= 0f)
		{
			this.shakeTimer = 0f;
			this.shakeCamera.transform.position = new Vector3(0f, 0f);
			return;
		}
		if (this.shakeIntervalTimer > 0f)
		{
			this.shakeIntervalTimer -= pElapsed;
			return;
		}
		this.shakeIntervalTimer = this.shakeInterval;
		Vector3 position = default(Vector3);
		if (this.shakeX)
		{
			position.x = Random.Range(-this.shakeIntencity, this.shakeIntencity);
		}
		if (this.shakeY)
		{
			position.y = Random.Range(-this.shakeIntencity, this.shakeIntencity);
		}
		this.shakeCamera.transform.position = position;
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x00093485 File Offset: 0x00091685
	public void startShake(float pDuration = 0.3f, float pInterval = 0.01f, float pIntencity = 2f, bool pShakeX = true, bool pShakeY = true)
	{
		this.shakeTimer = pDuration;
		this.shakeInterval = pInterval;
		this.shakeIntencity = pIntencity;
		this.shakeX = pShakeX;
		this.shakeY = pShakeY;
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x000934AC File Offset: 0x000916AC
	private void updateMapLayers(float pElapsed)
	{
		if (Config.worldLoading)
		{
			return;
		}
		this.heat.update(pElapsed);
		this.mapChunkManager.update(pElapsed);
		for (int i = 0; i < this.mapLayers.Count; i++)
		{
			this.mapLayers[i].update(pElapsed);
		}
		for (int j = 0; j < this.mapModules.Count; j++)
		{
			this.mapModules[j].update(pElapsed);
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0009352C File Offset: 0x0009172C
	public void switchAutoTester()
	{
		if (this.auto_tester == null)
		{
			GameObject gameObject = new GameObject();
			this.auto_tester = gameObject.AddComponent<AutoTesterBot>();
			this.auto_tester.create();
		}
		this.auto_tester.active = !this.auto_tester.active;
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0009357D File Offset: 0x0009177D
	public float getCurElapsed()
	{
		return Time.fixedDeltaTime * Config.timeScale;
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0009358C File Offset: 0x0009178C
	private void Update()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (Config.worldLoading)
		{
			return;
		}
		if (Config.isComputer && Application.targetFrameRate != 60)
		{
			Application.targetFrameRate = 60;
		}
		Config.updateCrashMetadata();
		PlayerConfig.instance.update();
		Tooltip.instance.checkClear();
		this.deltaTime = Time.fixedDeltaTime;
		this.gameStats.updateStats(this.deltaTime);
		this._isPaused = (Config.paused || ScrollWindow.isWindowActive() || RewardedAds.isShowing());
		if (this._unitSelectEffect != null)
		{
			this._unitSelectEffect.update(this.elapsed);
		}
		if (this.timerSpawnPixels > 0f)
		{
			this.timerSpawnPixels -= this.deltaTime;
		}
		this.elapsed = this.getCurElapsed();
		if (this.auto_tester != null)
		{
			this.auto_tester.update(this.elapsed);
		}
		AutoSaveManager.update();
		AssetManager.raceLibrary.update();
		this.islandsCalculator.update();
		this.unitZones.update();
		this.cityPlaceFinder.update(this.elapsed);
		ExplosionChecker.instance.update(this.deltaTime);
		MapMarks.drawMarks();
		this.kingdoms.update(this.elapsed);
		if (!this.isPaused())
		{
			this.unitGroupManager.update(this.elapsed);
			this.updateUnitsHunger(this.elapsed);
			this.mapStats.updateAge(this.elapsed);
			TaxiManager.update(this.elapsed);
		}
		Toolbox.temp_list_objects_enemies.Clear();
		Toolbox.temp_list_objects_enemies_chunk = null;
		Toolbox.temp_list_objects_enemies_kingdom = null;
		UnitSpriteConstructor.reset();
		int num = 0;
		while ((float)num < Config.actorFastMode)
		{
			this.updateMapLayers(this.elapsed);
			this.updateCities(this.elapsed);
			this.updateActors(this.elapsed);
			this.updateBuildings(this.elapsed);
			this.updateShake(this.elapsed);
			this.dropManager.update(this.elapsed);
			this.cultures.update(this.elapsed);
			if (this.updateWorldBehaviours)
			{
				for (int i = 0; i < this.behaviours.Count; i++)
				{
					this.behaviours[i].update(this.elapsed);
				}
			}
			num++;
		}
		MapNamesManager.instance.update();
		this.updateSpriteSystems();
		this.qualityChanger.update();
		this.updateControls();
		this.zone_camera.update();
		this.cloudController.update(this.elapsed);
		this.waveController.update(this.elapsed);
		if (this.redrawTimer > 0f)
		{
			this.redrawTimer -= Time.deltaTime;
		}
		else
		{
			this.redrawTimer = 0.001f;
			if (this.tilesDirty.Count > 0)
			{
				this.RedrawMap();
			}
		}
		if (!Screen.fullScreen)
		{
			if (Screen.width < 720)
			{
				Screen.SetResolution(720, Screen.height, false);
			}
			else if (Screen.height < 480)
			{
				Screen.SetResolution(Screen.width, 480, false);
			}
		}
		if (VersionCallbacks.timer > 0f)
		{
			VersionCallbacks.updateVC(this.elapsed);
		}
		if (Config.EVERYTHING_FIREWORKS)
		{
			this.spawnForcedFireworks();
		}
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000938D0 File Offset: 0x00091AD0
	public void updateSpriteSystems()
	{
		this.updateSpriteConstructor();
		this.updateUnitShadows();
		this.updateBuildingShadows();
		this.updateFavoriteUnitIcons();
		this.updateUnitBanners();
		this.updateUnitItems();
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x000938F6 File Offset: 0x00091AF6
	public void updateSpriteConstructor()
	{
		UnitSpriteConstructor.checkDirty();
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x000938FD File Offset: 0x00091AFD
	public void updateUnitShadows()
	{
		this.spriteSystemUnitShadows.update(this.elapsed);
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x00093910 File Offset: 0x00091B10
	public void updateFavoriteUnitIcons()
	{
		this.spriteSystemFavorites.update(this.elapsed);
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00093923 File Offset: 0x00091B23
	public void updateUnitBanners()
	{
		this.spriteSystemBanners.update(this.elapsed);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x00093936 File Offset: 0x00091B36
	public void updateUnitItems()
	{
		this.spriteSystemItems.update(this.elapsed);
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00093949 File Offset: 0x00091B49
	public void updateBuildingShadows()
	{
		this.spriteSystemBuildingShadows.update(this.elapsed);
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0009395C File Offset: 0x00091B5C
	internal void updateUnitsHunger(float pElapsed)
	{
		if (!this.worldLaws.world_law_hunger.boolVal)
		{
			return;
		}
		if (this.timer_hunger > 0f)
		{
			this.timer_hunger -= pElapsed;
			return;
		}
		this.timer_hunger = 8f;
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (actor.created && actor.stats.needFood)
			{
				actor.updateHunger();
			}
		}
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x000939E4 File Offset: 0x00091BE4
	internal void updateObjectAge()
	{
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (actor.created)
			{
				actor.updateAge();
			}
		}
		for (int j = 0; j < this.citiesList.Count; j++)
		{
			City city = this.citiesList[j];
			if (city.created)
			{
				city.updateAge();
			}
		}
		this.kingdoms.updateAge();
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x00093A63 File Offset: 0x00091C63
	internal void updateCultures()
	{
		this.cultures.updateProgress();
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x00093A70 File Offset: 0x00091C70
	private void updateCities(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateCities))
		{
			return;
		}
		for (int i = 0; i < this.citiesList.Count; i++)
		{
			City city = this.citiesList[i];
			if (city.created)
			{
				city.update(pElapsed);
			}
		}
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00093ABC File Offset: 0x00091CBC
	private void updateBuildings(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateBuildings))
		{
			return;
		}
		Toolbox.bench("bench_buildings");
		this.buildings.checkAddRemove();
		this.job_manager_buildings.updateBase(pElapsed);
		this.buildings.checkAddRemove();
		Toolbox.benchEnd("bench_buildings");
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00093B0C File Offset: 0x00091D0C
	private void updateActors(float pElapsed)
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateUnits))
		{
			return;
		}
		for (int i = 0; i < this.list_systems.Count; i++)
		{
			this.list_systems[i].clearList();
		}
		this.units.checkAddRemove();
		Toolbox.bench("actor_total");
		Toolbox.bench("actor0");
		List<Actor> simpleList = this.units.getSimpleList();
		int count = this.units.Count;
		for (int j = 0; j < count; j++)
		{
			Actor actor = simpleList[j];
			if (actor.created)
			{
				actor.updateTimers(pElapsed);
			}
		}
		Toolbox.benchEnd("actor0");
		Toolbox.bench("actor2");
		for (int k = 0; k < count; k++)
		{
			Actor actor = simpleList[k];
			if (actor.created)
			{
				actor.update(pElapsed);
			}
		}
		Toolbox.benchEnd("actor2");
		Toolbox.benchEnd("actor_total");
		this.units.checkAddRemove();
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x00093C0C File Offset: 0x00091E0C
	public void updateCurrentPosition()
	{
		bool flag = false;
		if (Input.touchSupported && Input.touchCount != 0)
		{
			if (this.eventDataCurrentPosition == null)
			{
				this.eventDataCurrentPosition = new PointerEventData(EventSystem.current);
			}
			this.eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			flag = true;
		}
		if (Input.mousePresent && !flag)
		{
			if (this.eventDataCurrentPosition == null)
			{
				this.eventDataCurrentPosition = new PointerEventData(EventSystem.current);
			}
			this.eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			flag = true;
		}
		if (!flag)
		{
			this.eventDataCurrentPosition = null;
		}
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x00093CCC File Offset: 0x00091ECC
	public bool IsPointerOverUIObject()
	{
		this.updateCurrentPosition();
		if (this.eventDataCurrentPosition == null)
		{
			return false;
		}
		this.results.Clear();
		EventSystem.current.RaycastAll(this.eventDataCurrentPosition, this.results);
		return this.results.Count > 0;
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x00093D18 File Offset: 0x00091F18
	public bool isOverUI()
	{
		return this.IsPointerOverUIObject();
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x00093D28 File Offset: 0x00091F28
	public bool isPointerOverUIScroll()
	{
		this.updateCurrentPosition();
		if (this.eventDataCurrentPosition == null)
		{
			return false;
		}
		this.results.Clear();
		EventSystem.current.RaycastAll(this.eventDataCurrentPosition, this.results);
		for (int i = 0; i < this.results.Count; i++)
		{
			RaycastResult raycastResult = this.results[i];
			if (!raycastResult.isValid)
			{
				return false;
			}
			this.guiCheckGameObject = raycastResult.gameObject;
			if (this.guiCheckGameObject == null)
			{
				return false;
			}
			if (this.guiCheckGameObject.GetComponent<ScrollRectExtended>() != null)
			{
				return true;
			}
			if (this.guiCheckGameObject.name == "Scroll View")
			{
				Transform transform = this.guiCheckGameObject.transform.Find("Viewport/Content");
				if (transform != null)
				{
					Vector2 sizeDelta = this.guiCheckGameObject.GetComponent<RectTransform>().sizeDelta;
					if (transform.GetComponent<RectTransform>().sizeDelta.y > sizeDelta.y)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x00093E2E File Offset: 0x0009202E
	public bool isActionHappening()
	{
		return this.earthquakeManager.quakeActive || (Input.touchSupported && Input.touchCount > 1) || (Input.mousePresent && (Input.GetMouseButton(0) || Input.GetMouseButton(2)));
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00093E6A File Offset: 0x0009206A
	public static bool controlsLocked()
	{
		return MapBox.instance.tutorial.gameObject.activeSelf || Config.controllingUnit || MapBox.instance.stackEffects.isLocked() || Config.lockGameControls;
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x00093EAC File Offset: 0x000920AC
	private void updateControls()
	{
		if (MapBox.controlsLocked())
		{
			return;
		}
		if (DebugConfig.isOn(DebugOption.MakeUnitsFollowCursor) && Input.GetMouseButtonDown(0) && this.getMouseTilePos() != null && !this.isOverUI())
		{
			Toolbox.bench("test_follow");
			List<Actor> simpleList = this.units.getSimpleList();
			for (int i = 0; i < simpleList.Count; i++)
			{
				Actor actor = simpleList[i];
				if (actor.currentTile.region.island == this.getMouseTilePos().region.island)
				{
					actor.stopMovement();
					actor.goTo(this.getMouseTilePos(), false, false);
				}
			}
			Toolbox.benchEnd("test_follow");
		}
		if (!ScrollWindow.isWindowActive() && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.F9))
		{
			this.canvas.gameObject.SetActive(!this.canvas.gameObject.activeSelf);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Config.paused = !Config.paused;
			this.selectedButtons.pauseButton.GetComponent<PauseButton>().updateSprite();
		}
		if (!this.canvas.gameObject.activeSelf)
		{
			return;
		}
		if ((Input.GetKeyDown(KeyCode.Tilde) || Input.GetKeyDown(KeyCode.BackQuote)) && EventSystem.current.currentSelectedGameObject == null)
		{
			this.console.Toggle();
		}
		Input.GetKeyDown(KeyCode.P);
		if (Input.GetKeyDown(KeyCode.Return) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftCommand)))
		{
			PlayerConfig.toggleFullScreen();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (this.console.isActive())
			{
				this.console.Hide();
			}
			else if (ScrollWindow.isWindowActive() && !ScrollWindow.animationActive)
			{
				ScrollWindow scrollWindow = ScrollWindow.currentWindows[ScrollWindow.currentWindows.Count - 1];
				if (scrollWindow.historyActionEnabled)
				{
					if (WindowHistory.list.Count >= 2)
					{
						WindowHistory.clickBack();
					}
					else
					{
						scrollWindow.clickHide("right");
					}
				}
			}
			else if (this.selectedButtons.selectedButton != null)
			{
				this.selectedButtons.unselectAll();
			}
			else if (PowersTab.isTabSelected())
			{
				this.selectedButtons.unselectTabs();
			}
			else
			{
				ScrollWindow.showWindow("quit_game");
			}
		}
		if (!ScrollWindow.isWindowActive())
		{
			if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
			{
				PowersTab.showTabFromButton(PowerTabController.instance.t_drawing);
			}
			else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
			{
				PowersTab.showTabFromButton(PowerTabController.instance.t_kingdoms);
			}
			else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
			{
				PowersTab.showTabFromButton(PowerTabController.instance.t_creatures);
			}
			else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				PowersTab.showTabFromButton(PowerTabController.instance.t_nature);
			}
			else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
			{
				PowersTab.showTabFromButton(PowerTabController.instance.t_bombs);
			}
			else if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
			{
				PowersTab.showTabFromButton(PowerTabController.instance.t_other);
			}
		}
		this.actions.magnetAction(true, null);
		if (Input.GetMouseButtonUp(0))
		{
			this.timerSpawnPixels = 0f;
		}
		if (Input.touchSupported)
		{
			if (Input.GetMouseButtonDown(0) && Input.touchCount > 0 && this.isOverUI())
			{
				this.alreadyUsedPower = true;
				return;
			}
			if (Input.touchCount == 0)
			{
				this.touchTimer = 0f;
				this.alreadyUsedZoom = false;
				this.alreadyUsedPower = false;
				this.originTouch = Vector2.zero;
			}
			else if (Input.touchCount == 1)
			{
				if (this.originTouch == Vector2.zero)
				{
					this.originTouch = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
				}
				else
				{
					this.currentTouch = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
				}
				this.touchTimer += Time.deltaTime;
				if (this.touchTimer < 0.05f)
				{
					return;
				}
			}
			else if (Input.touchCount > 1)
			{
				this.originTouch = Vector2.zero;
				return;
			}
		}
		if (this.controlsLockTimer > 0f)
		{
			this.controlsLockTimer -= Time.deltaTime;
			return;
		}
		if (this.isGameplayControlsLocked())
		{
			return;
		}
		if (this.isOverUI())
		{
			return;
		}
		Input.GetMouseButtonUp(1);
		bool flag = this.canInspectUnitWithCurrentPower();
		if (!this.alreadyUsedZoom && (this.canInspectWithMainTouch() || this.canInspectWithRightClick()) && this.inspectTimerClick < 0.2f && !this.isActionHappening() && !MoveCamera.cameraDragActivated && Toolbox.DistVec3(this.originTouch, this.currentTouch) < 20f)
		{
			WorldTile mouseTilePos = this.getMouseTilePos();
			if (mouseTilePos != null)
			{
				if (this.qualityChanger.lowRes)
				{
					if (this.showCultureZones())
					{
						ActionLibrary.inspectCulture(mouseTilePos, null);
						return;
					}
					if (this.showKingdomZones())
					{
						ActionLibrary.inspectKingdom(mouseTilePos, null);
						return;
					}
					ActionLibrary.inspectCity(mouseTilePos, null);
					return;
				}
				else
				{
					ActionLibrary.inspectUnit(null, null);
				}
			}
			return;
		}
		if (!this.alreadyUsedZoom && Config.spectatorMode && !MoveCamera.cameraDragActivated && Input.GetMouseButtonUp(0) && Toolbox.DistVec3(this.originTouch, this.currentTouch) < 20f)
		{
			Actor actorFromTile = ActionLibrary.getActorFromTile(this.getMouseTilePos());
			if (actorFromTile != null)
			{
				this.locateAndFollow(actorFromTile, null, null);
				return;
			}
		}
		if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
		{
			this.inspectTimerClick += Time.deltaTime;
		}
		else
		{
			this.inspectTimerClick = 0f;
		}
		if (this.selectedButtons.selectedButton == null)
		{
			this.checkEmptyClick();
			return;
		}
		if (this.selectedButtons.selectedButton.godPower == null)
		{
			return;
		}
		if (this.alreadyUsedZoom)
		{
			return;
		}
		GodPower godPower = this.selectedButtons.selectedButton.godPower;
		this.highlightCursor(godPower);
		if (godPower.holdAction || (DebugConfig.isOn(DebugOption.FastSpawn) && !godPower.ignoreFastSpawn && godPower.type == PowerActionType.SpawnActor))
		{
			if (Input.GetMouseButton(0))
			{
				this.Clicked();
				return;
			}
			this.lastClick.Set(-1, -1);
			this.firstPressedType = null;
			this.firstPressedTopType = null;
			this.firstClick = true;
			return;
		}
		else
		{
			if (Input.GetMouseButtonUp(0))
			{
				this.Clicked();
				return;
			}
			this.firstPressedType = null;
			this.firstPressedTopType = null;
			this.firstClick = true;
			return;
		}
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x00094576 File Offset: 0x00092776
	private bool canInspectUnitWithCurrentPower()
	{
		return !this.selectedButtons.isPowerSelected() || this.selectedButtons.selectedButton.godPower.allow_unit_selection;
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x000945A1 File Offset: 0x000927A1
	private bool canInspectWithRightClick()
	{
		return Input.mousePresent && this.canInspectUnitWithCurrentPower() && Input.GetMouseButtonUp(1);
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x000945BC File Offset: 0x000927BC
	private bool canInspectWithMainTouch()
	{
		return this.canInspectUnitWithCurrentPower() && Input.GetMouseButtonUp(0);
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x000945D0 File Offset: 0x000927D0
	private void highlightCursor(GodPower pPower)
	{
		if (!Config.isComputer && !Config.isEditor)
		{
			return;
		}
		WorldTile mouseTilePos = this.getMouseTilePos();
		if (mouseTilePos == null)
		{
			return;
		}
		this.flashEffects.flashPixel(mouseTilePos, 10, ColorType.White);
		if (pPower != null)
		{
			if (!string.IsNullOrEmpty(pPower.forceBrush))
			{
				this.highlightFrom(mouseTilePos, Brush.get(pPower.forceBrush));
				return;
			}
			if (pPower.highlight || pPower.showToolSizes)
			{
				this.highlightFrom(mouseTilePos, Config.currentBrushData);
			}
		}
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00094648 File Offset: 0x00092848
	private void highlightFrom(WorldTile pTile, BrushData pBrushData)
	{
		for (int i = 0; i < pBrushData.pos.Count; i++)
		{
			BrushPixelData brushPixelData = pBrushData.pos[i];
			WorldTile tile = this.GetTile(brushPixelData.x + pTile.x, brushPixelData.y + pTile.y);
			if (tile != null)
			{
				this.flashEffects.flashPixel(tile, 10, ColorType.White);
			}
		}
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x000946AC File Offset: 0x000928AC
	private void allTilesDirty()
	{
		for (int i = 0; i < this.tilesList.Count; i++)
		{
			WorldTile pTile = this.tilesList[i];
			this.setTileDirty(pTile, false);
		}
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x000946E4 File Offset: 0x000928E4
	public void redrawRenderedTile(WorldTile pTile)
	{
		pTile.lastDrawnType = null;
		this.setTileDirty(pTile, false);
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x000946F8 File Offset: 0x000928F8
	public void setTileDirty(WorldTile pTile, bool pCorners = false)
	{
		if (pTile.dirty)
		{
			return;
		}
		pTile.dirty = true;
		this.tilesDirty.Add(pTile);
		if (!pCorners)
		{
			if (pTile.tile_up != null)
			{
				pTile.tile_up.dirty = true;
				this.tilesDirty.Add(pTile.tile_up);
			}
			if (pTile.tile_down != null)
			{
				pTile.tile_down.dirty = true;
				this.tilesDirty.Add(pTile.tile_down);
			}
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0009476E File Offset: 0x0009296E
	internal void zoomUpdated(float pZoom, float pMaxZoom)
	{
		this.qualityChanger.zoomUpdated(pZoom, pMaxZoom);
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x00094780 File Offset: 0x00092980
	internal void RedrawMap()
	{
		if (!DebugConfig.isOn(DebugOption.SystemRedrawMap))
		{
			return;
		}
		this.dirtyTilesLast = this.tilesDirty.Count;
		this.tilemap.updateTileMap();
		for (int i = 0; i < this.tilesDirty.Count; i++)
		{
			WorldTile pTile = this.tilesDirty[i];
			this.updateDirtyTile(pTile);
		}
		this.tilesDirty.Clear();
		this.worldLayer.updatePixels();
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x000947F4 File Offset: 0x000929F4
	internal void checkBehaviours(WorldTile pTile)
	{
		if (pTile.Type.explodableTimed)
		{
			this.explosionLayer.addTimedTnt(pTile);
		}
		if (pTile.Type.canBeFilledWithOcean)
		{
			WorldBehaviourOcean.tiles.Add(pTile);
		}
		else
		{
			WorldBehaviourOcean.tiles.Remove(pTile);
		}
		if (pTile.data.fire)
		{
			WorldBehaviourActionFire.tiles.Add(pTile);
			return;
		}
		WorldBehaviourActionFire.tiles.Remove(pTile);
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x00094868 File Offset: 0x00092A68
	private void updateDirtyTile(WorldTile pTile)
	{
		if (pTile.building != null)
		{
			Color grey = Color.grey;
			if (!(pTile.building.getColor(pTile) == Toolbox.clear))
			{
				this.worldLayer.pixels[pTile.data.tile_id] = pTile.building.getColor(pTile);
				return;
			}
		}
		if (pTile.Type.edge_hills && pTile != null)
		{
			WorldTile tile_down = pTile.tile_down;
			bool? flag = (tile_down != null) ? new bool?(tile_down.Type.rocks) : null;
			bool flag2 = false;
			if (flag.GetValueOrDefault() == flag2 & flag != null)
			{
				this.worldLayer.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_hills;
				return;
			}
		}
		if (pTile.Type.edge_mountains)
		{
			WorldTile tile_down2 = pTile.tile_down;
			if (tile_down2 != null && tile_down2.Type.edge_hills)
			{
				this.worldLayer.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_mountain;
				return;
			}
		}
		if (pTile.Type.rocks)
		{
			WorldTile tile_down3 = pTile.tile_down;
			if (tile_down3 != null && !tile_down3.Type.rocks)
			{
				this.worldLayer.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_mountain;
				return;
			}
		}
		WorldTile tile_up = pTile.tile_up;
		if (tile_up != null && tile_up.Type.ground)
		{
			if (pTile.Type.layerType == TileLayerType.Ocean)
			{
				this.worldLayer.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_ocean;
				return;
			}
			if (pTile.Type.canBeFilledWithOcean)
			{
				this.worldLayer.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_no_ocean;
				return;
			}
		}
		this.worldLayer.pixels[pTile.data.tile_id] = pTile.getColor();
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x00094A80 File Offset: 0x00092C80
	public void locateSelectedUnit()
	{
		Actor selectedUnit = Config.selectedUnit;
		ScrollWindow.hideAllEvent(true);
		this.camera.GetComponent<MoveCamera>().focusOnAndFollow(selectedUnit, null, null);
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x00094AAC File Offset: 0x00092CAC
	public void locateSelectedVillage()
	{
		City selectedCity = Config.selectedCity;
		ScrollWindow.hideAllEvent(true);
		this.camera.GetComponent<MoveCamera>().focusOn(selectedCity.cityCenter);
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x00094ADB File Offset: 0x00092CDB
	public void locatePosition(Vector3 pVector)
	{
		if (this.isGameplayControlsLocked())
		{
			ScrollWindow.hideAllEvent(true);
		}
		this.camera.GetComponent<MoveCamera>().focusOn(pVector);
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x00094AFC File Offset: 0x00092CFC
	public void locatePosition(Vector3 pVector, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		if (this.isGameplayControlsLocked())
		{
			ScrollWindow.hideAllEvent(true);
		}
		this.camera.GetComponent<MoveCamera>().focusOn(pVector, pFocusReachedCallback, pFocusCancelCallback);
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x00094B1F File Offset: 0x00092D1F
	public void locateAndFollow(Actor pActor, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		if (this.isGameplayControlsLocked())
		{
			ScrollWindow.hideAllEvent(true);
		}
		this.camera.GetComponent<MoveCamera>().focusOnAndFollow(pActor, pFocusReachedCallback, pFocusCancelCallback);
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x00094B42 File Offset: 0x00092D42
	internal void specialPowerAction(GodPower pPower)
	{
		if (pPower == null)
		{
			return;
		}
		if (this.isSelectedPower("relations"))
		{
			Config.selectedKingdom = this.kingdoms.getRandom();
		}
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x00094B65 File Offset: 0x00092D65
	internal bool isSelectedPower(string pPower)
	{
		return !(this.selectedButtons.selectedButton == null) && this.selectedButtons.selectedButton.godPower.id == pPower;
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x00094B9C File Offset: 0x00092D9C
	internal string getSelectedPower()
	{
		if (this.selectedButtons.selectedButton == null)
		{
			return string.Empty;
		}
		return this.selectedButtons.selectedButton.godPower.id;
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x00094BCC File Offset: 0x00092DCC
	internal bool isSelectedPowerAny()
	{
		return !(this.selectedButtons.selectedButton == null);
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x00094BE4 File Offset: 0x00092DE4
	internal bool isPowerForceMapMode(MapMode pMode = MapMode.None)
	{
		return !(this.selectedButtons.selectedButton == null) && this.selectedButtons.selectedButton.godPower.force_map_text == pMode;
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00094C13 File Offset: 0x00092E13
	internal MapMode getForcedMapMode(MapMode pMode = MapMode.None)
	{
		if (this.selectedButtons.selectedButton == null)
		{
			return MapMode.None;
		}
		return this.selectedButtons.selectedButton.godPower.force_map_text;
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x00094C3F File Offset: 0x00092E3F
	internal bool showCityZones()
	{
		return PlayerConfig.optionBoolEnabled("map_city_zones") || this.isPowerForceMapMode(MapMode.Villages);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x00094C56 File Offset: 0x00092E56
	internal bool showKingdomZones()
	{
		return PlayerConfig.optionBoolEnabled("map_kingdom_zones") || this.isPowerForceMapMode(MapMode.Kingdoms);
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x00094C6D File Offset: 0x00092E6D
	internal bool showCultureZones()
	{
		return PlayerConfig.optionBoolEnabled("map_culture_zones") || this.isPowerForceMapMode(MapMode.Cultures);
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x00094C84 File Offset: 0x00092E84
	internal bool showMapNames()
	{
		return PlayerConfig.optionBoolEnabled("map_names") || this.isPowerForceMapMode(MapMode.Villages);
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x00094C9B File Offset: 0x00092E9B
	internal bool isPaused()
	{
		return this._isPaused;
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x00094CA4 File Offset: 0x00092EA4
	internal void spawnCongratulationFireworks()
	{
		City random = Toolbox.getRandom<City>(this.citiesList);
		if (random == null)
		{
			return;
		}
		Building random2 = random.buildings.GetRandom();
		if (random2 == null)
		{
			return;
		}
		if (random2.data.underConstruction)
		{
			return;
		}
		this.stackEffects.spawnFireworks(random2.currentTile, 0.2f);
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x00094D04 File Offset: 0x00092F04
	internal void spawnForcedFireworks()
	{
		WorldTile random = Toolbox.getRandom<WorldTile>(this.tilesList);
		PlayerConfig.dict["sound"].boolVal = true;
		this.stackEffects.spawnFireworks(random, 0f);
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x00094D44 File Offset: 0x00092F44
	internal void spawnPeaceFireworks(Kingdom kingdom)
	{
		City capital = kingdom.capital;
		if (capital == null)
		{
			return;
		}
		Building random = capital.buildings.GetRandom();
		if (random == null)
		{
			return;
		}
		if (random.data.underConstruction)
		{
			return;
		}
		this.stackEffects.spawnFireworks(random.currentTile, 0.2f);
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x00094D9C File Offset: 0x00092F9C
	public MapObjectShadow createShadowBuilding(BaseAnimatedObject pObject)
	{
		MapObjectShadow mapObjectShadow = Object.Instantiate<MapObjectShadow>(PrefabLibrary.instance.shadow, pObject.transform);
		mapObjectShadow.create();
		return mapObjectShadow;
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x00094DB9 File Offset: 0x00092FB9
	public MapObjectShadow createShadow(BaseAnimatedObject pObject, string pType = null)
	{
		MapObjectShadow mapObjectShadow = Object.Instantiate<MapObjectShadow>(PrefabLibrary.instance.shadow, this.transformShadows);
		mapObjectShadow.create();
		mapObjectShadow.setShadow(pType, false);
		return mapObjectShadow;
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x00094DDE File Offset: 0x00092FDE
	private void OnDrawGizmos()
	{
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x00094DE0 File Offset: 0x00092FE0
	public int getCivWorldPopulation()
	{
		int num = 0;
		int num2 = this.countPopPoints();
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			if (simpleList[i].stats.unit)
			{
				num++;
			}
		}
		return num + num2;
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x00094E30 File Offset: 0x00093030
	public int countPopPoints()
	{
		int num = 0;
		for (int i = 0; i < this.citiesList.Count; i++)
		{
			City city = this.citiesList[i];
			num += city.getPopulationPopPoints();
		}
		return num;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x00094E6C File Offset: 0x0009306C
	internal static void aye()
	{
		CornerAye.instance.startAye();
	}

	// Token: 0x0400138B RID: 5003
	public Transform mapMarksArrows;

	// Token: 0x0400138C RID: 5004
	public GameObject joys;

	// Token: 0x0400138D RID: 5005
	public SleekRenderSettings sleekRenderSettings;

	// Token: 0x0400138E RID: 5006
	public PlayInterstitialAd playInterstitialAd;

	// Token: 0x0400138F RID: 5007
	public SaveManager saveManager;

	// Token: 0x04001390 RID: 5008
	public GameStats gameStats;

	// Token: 0x04001391 RID: 5009
	[HideInInspector]
	public MapStats mapStats;

	// Token: 0x04001392 RID: 5010
	[HideInInspector]
	public WorldLaws worldLaws;

	// Token: 0x04001393 RID: 5011
	public Canvas canvas;

	// Token: 0x04001394 RID: 5012
	public static MapBox instance;

	// Token: 0x04001395 RID: 5013
	public Transform prefab_city;

	// Token: 0x04001396 RID: 5014
	public PowerButtonSelector selectedButtons;

	// Token: 0x04001397 RID: 5015
	public static int width;

	// Token: 0x04001398 RID: 5016
	public static int height;

	// Token: 0x04001399 RID: 5017
	public string seed;

	// Token: 0x0400139A RID: 5018
	public bool useRandomSeed;

	// Token: 0x0400139B RID: 5019
	public WorldTile[,] tilesMap;

	// Token: 0x0400139C RID: 5020
	public List<WorldTile> tilesList;

	// Token: 0x0400139D RID: 5021
	public List<City> citiesList = new List<City>();

	// Token: 0x0400139E RID: 5022
	public ActorContainer units;

	// Token: 0x0400139F RID: 5023
	public BuildingContainer buildings;

	// Token: 0x040013A0 RID: 5024
	public JobManagerBuildings job_manager_buildings;

	// Token: 0x040013A1 RID: 5025
	public KingdomManager kingdoms;

	// Token: 0x040013A2 RID: 5026
	public CultureManager cultures;

	// Token: 0x040013A3 RID: 5027
	public WorldTilemap tilemap;

	// Token: 0x040013A4 RID: 5028
	internal float redrawTimer;

	// Token: 0x040013A5 RID: 5029
	private List<WorldBehaviour> behaviours;

	// Token: 0x040013A6 RID: 5030
	internal Dictionary<string, WorldBehaviour> behavioursDict;

	// Token: 0x040013A7 RID: 5031
	private bool initiated;

	// Token: 0x040013A8 RID: 5032
	private DebugLayer debugLayer;

	// Token: 0x040013A9 RID: 5033
	public RegionPathFinder regionPathFinder = new RegionPathFinder();

	// Token: 0x040013AA RID: 5034
	public LoadingScreen transitionScreen;

	// Token: 0x040013AB RID: 5035
	public StackEffects stackEffects;

	// Token: 0x040013AC RID: 5036
	public ItemsController itemsController;

	// Token: 0x040013AD RID: 5037
	public WaveController waveController;

	// Token: 0x040013AE RID: 5038
	public CloudController cloudController;

	// Token: 0x040013AF RID: 5039
	public PrintLibrary printLibrary;

	// Token: 0x040013B0 RID: 5040
	public DropManager dropManager;

	// Token: 0x040013B1 RID: 5041
	public PathFindingVisualiser pathFindingVisualiser;

	// Token: 0x040013B2 RID: 5042
	internal WorldLayer worldLayer;

	// Token: 0x040013B3 RID: 5043
	internal UnitLayer unitLayer;

	// Token: 0x040013B4 RID: 5044
	public LavaLayer lavaLayer;

	// Token: 0x040013B5 RID: 5045
	public GreyGooLayer greyGooLayer;

	// Token: 0x040013B6 RID: 5046
	internal PixelFlashEffects flashEffects;

	// Token: 0x040013B7 RID: 5047
	internal FireLayer fireLayer;

	// Token: 0x040013B8 RID: 5048
	internal IslandsCalculator islandsCalculator;

	// Token: 0x040013B9 RID: 5049
	internal ZoneCalculator zoneCalculator;

	// Token: 0x040013BA RID: 5050
	internal RoadsCalculator roadsCalculator;

	// Token: 0x040013BB RID: 5051
	internal BurnedTilesLayer burnedLayer;

	// Token: 0x040013BC RID: 5052
	internal ExplosionsEffects explosionLayer;

	// Token: 0x040013BD RID: 5053
	internal ConwayLife conwayLayer;

	// Token: 0x040013BE RID: 5054
	internal MapChunkManager mapChunkManager;

	// Token: 0x040013BF RID: 5055
	internal List<MapLayer> mapLayers;

	// Token: 0x040013C0 RID: 5056
	internal List<BaseModule> mapModules;

	// Token: 0x040013C1 RID: 5057
	public Earthquake earthquakeManager;

	// Token: 0x040013C2 RID: 5058
	public Vector2 wind_direction;

	// Token: 0x040013C3 RID: 5059
	internal StaticGrid searchGridGround;

	// Token: 0x040013C4 RID: 5060
	internal List<WorldTile> tilesDirty;

	// Token: 0x040013C5 RID: 5061
	public GlowParticles particlesFire;

	// Token: 0x040013C6 RID: 5062
	public GlowParticles particlesSmoke;

	// Token: 0x040013C7 RID: 5063
	internal bool updateWorldBehaviours = true;

	// Token: 0x040013C8 RID: 5064
	public GameObject uiGameplay;

	// Token: 0x040013C9 RID: 5065
	public Console console;

	// Token: 0x040013CA RID: 5066
	public Text debugText;

	// Token: 0x040013CB RID: 5067
	public bool hasFocus;

	// Token: 0x040013CC RID: 5068
	internal Heat heat;

	// Token: 0x040013CD RID: 5069
	public HeatRayEffect heatRayFx;

	// Token: 0x040013CE RID: 5070
	public EffectDivineLight fxDivineLight;

	// Token: 0x040013CF RID: 5071
	public MapBorder mapBorder;

	// Token: 0x040013D0 RID: 5072
	internal QualityChanger qualityChanger;

	// Token: 0x040013D1 RID: 5073
	private Transform transformCreatures;

	// Token: 0x040013D2 RID: 5074
	internal Transform transformUnits;

	// Token: 0x040013D3 RID: 5075
	internal Transform transformBuildings;

	// Token: 0x040013D4 RID: 5076
	internal Transform transformShadows;

	// Token: 0x040013D5 RID: 5077
	private Transform transformTrees;

	// Token: 0x040013D6 RID: 5078
	internal Camera camera;

	// Token: 0x040013D7 RID: 5079
	internal ZoneCamera zone_camera;

	// Token: 0x040013D8 RID: 5080
	internal CityPlaceFinder cityPlaceFinder;

	// Token: 0x040013D9 RID: 5081
	internal UnitZones unitZones;

	// Token: 0x040013DA RID: 5082
	public Tutorial tutorial;

	// Token: 0x040013DB RID: 5083
	public WorldLog worldLog;

	// Token: 0x040013DC RID: 5084
	public PowerActionsModule actions;

	// Token: 0x040013DD RID: 5085
	internal float timer_hunger;

	// Token: 0x040013DE RID: 5086
	public UnitGroupManager unitGroupManager;

	// Token: 0x040013DF RID: 5087
	internal AutoTesterBot auto_tester;

	// Token: 0x040013E0 RID: 5088
	internal UnitSelectionEffect _unitSelectEffect;

	// Token: 0x040013E1 RID: 5089
	private List<SpriteGroupSystem<GroupSpriteObject>> list_systems = new List<SpriteGroupSystem<GroupSpriteObject>>();

	// Token: 0x040013E2 RID: 5090
	internal SpriteGroupSystemUnitShadows spriteSystemUnitShadows;

	// Token: 0x040013E3 RID: 5091
	internal SpriteGroupSystemBuildingShadows spriteSystemBuildingShadows;

	// Token: 0x040013E4 RID: 5092
	internal SpriteGroupSystemFavorites spriteSystemFavorites;

	// Token: 0x040013E5 RID: 5093
	internal SpriteGroupSystemUnitItems spriteSystemItems;

	// Token: 0x040013E6 RID: 5094
	internal SpriteGroupSystemUnitBanners spriteSystemBanners;

	// Token: 0x040013E7 RID: 5095
	internal List<BaseSimObject> temp_map_objects = new List<BaseSimObject>();

	// Token: 0x040013E8 RID: 5096
	internal float controlsLockTimer;

	// Token: 0x040013E9 RID: 5097
	public AStarParam pathfindingParam = new AStarParam();

	// Token: 0x040013EA RID: 5098
	private static List<WorldTile> temp_list_tiles = new List<WorldTile>();

	// Token: 0x040013EB RID: 5099
	private int lastUsedWidth;

	// Token: 0x040013EC RID: 5100
	private int lastUsedHeight;

	// Token: 0x040013ED RID: 5101
	private bool first_gen = true;

	// Token: 0x040013EE RID: 5102
	private string islandTypeToGenerate;

	// Token: 0x040013EF RID: 5103
	internal bool firstClick = true;

	// Token: 0x040013F0 RID: 5104
	private WorldTile tile_firstClick;

	// Token: 0x040013F1 RID: 5105
	private WorldTile tile_secondClick;

	// Token: 0x040013F2 RID: 5106
	internal float timerSpawnPixels;

	// Token: 0x040013F3 RID: 5107
	private List<BaseEffect> _list_to_clean_effects = new List<BaseEffect>();

	// Token: 0x040013F4 RID: 5108
	private List<Actor> _force_temp_actor_list = new List<Actor>();

	// Token: 0x040013F5 RID: 5109
	private float shakeTimer;

	// Token: 0x040013F6 RID: 5110
	private float shakeIntervalTimer;

	// Token: 0x040013F7 RID: 5111
	private float shakeIntencity = 1f;

	// Token: 0x040013F8 RID: 5112
	private float shakeInterval = 0.1f;

	// Token: 0x040013F9 RID: 5113
	private bool shakeX = true;

	// Token: 0x040013FA RID: 5114
	private bool shakeY = true;

	// Token: 0x040013FB RID: 5115
	public GameObject shakeCamera;

	// Token: 0x040013FC RID: 5116
	internal float elapsed;

	// Token: 0x040013FD RID: 5117
	internal float deltaTime;

	// Token: 0x040013FE RID: 5118
	private PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

	// Token: 0x040013FF RID: 5119
	public List<RaycastResult> results = new List<RaycastResult>();

	// Token: 0x04001400 RID: 5120
	private GameObject guiCheckGameObject;

	// Token: 0x04001401 RID: 5121
	internal float inspectTimerClick;

	// Token: 0x04001402 RID: 5122
	internal float touchTimer;

	// Token: 0x04001403 RID: 5123
	internal bool alreadyUsedZoom;

	// Token: 0x04001404 RID: 5124
	internal bool alreadyUsedPower;

	// Token: 0x04001405 RID: 5125
	private Vector2Int lastClick;

	// Token: 0x04001406 RID: 5126
	private Vector2 originTouch;

	// Token: 0x04001407 RID: 5127
	private Vector2 currentTouch;

	// Token: 0x04001408 RID: 5128
	public TileType firstPressedType;

	// Token: 0x04001409 RID: 5129
	public TopTileType firstPressedTopType;

	// Token: 0x0400140A RID: 5130
	internal int dirtyTilesLast;

	// Token: 0x0400140B RID: 5131
	internal bool _isPaused;
}
