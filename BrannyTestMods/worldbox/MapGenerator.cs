using System;

// Token: 0x02000181 RID: 385
public static class MapGenerator
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x0005E4C4 File Offset: 0x0005C6C4
	public static void generate(string pType)
	{
		MapGenerator._world = MapBox.instance;
		MapGenerator._width = MapBox.width;
		MapGenerator._height = MapBox.height;
		MapGenerator._tilesMap = MapGenerator._world.tilesMap;
		MapGenerator._random_shapes_amount = 0;
		MapGenerator._water_level = 0;
		MapGenerator._random_shapes = false;
		if (pType == "archipelago" || pType == "islands")
		{
			MapGenerator._random_shapes = true;
			MapGenerator._random_shapes_amount = 20;
		}
		else if (pType == "earth")
		{
			GeneratorTool.applyTemplate("earth", 1f);
		}
		if (pType == "archipelago")
		{
			MapGenerator._perlinScale = 20f;
		}
		else if (pType == "islands")
		{
			MapGenerator._perlinScale = 5f;
		}
		if (pType == "custom")
		{
			MapGenerator._random_shapes_amount = Config.customRandomShapes;
			MapGenerator._perlinScale = (float)Config.customPerlinScale;
			MapGenerator._water_level = Config.customWaterLevel;
			MapGenerator._random_shapes = true;
		}
		if (MapGenerator._random_shapes)
		{
			MapGenerator.generatePerlinNoiseMap(true);
		}
		GeneratorTool.UpdateTileTypes(true);
		MapGenerator._world.mapChunkManager.allDirty();
		MapGenerator._world.mapChunkManager.updateDirty();
		MapGenerator.addRandomBiome();
		MapGenerator.tryToAddRandomBiomeIslands();
		MapGenerator.freezeMountainTops();
		AssetManager.tiles.SetListTo("gameplay");
		MapGenerator.spawnVegetation();
		MapGenerator.spawnResources();
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0005E610 File Offset: 0x0005C810
	private static void generatePerlinNoiseMap(bool pAddEdge = true)
	{
		if (MapGenerator._random_shapes)
		{
			MapGenerator.addRandomShapes(MapGenerator._random_shapes_amount);
		}
		MapGenerator._perlinX = (float)Toolbox.randomInt(0, 15000);
		MapGenerator._perlinY = (float)Toolbox.randomInt(0, 15000);
		float perlinScale = MapGenerator._perlinScale;
		GeneratorTool.ApplyPerlinNoice(MapGenerator._tilesMap, MapGenerator._width, MapGenerator._height, MapGenerator._perlinX, MapGenerator._perlinY, 1f, 1f * perlinScale, false, GeneratorTarget.Height);
		if (pAddEdge)
		{
			MapEdges.AddEdge(MapGenerator._world.tilesMap, "height");
		}
		int num = Toolbox.randomInt(0, 10000);
		int num2 = Toolbox.randomInt(0, 10000);
		GeneratorTool.ApplyPerlinNoice(MapGenerator._tilesMap, MapGenerator._width, MapGenerator._height, (float)num, (float)num2, 0.2f, 4f * perlinScale, true, GeneratorTarget.Height);
		num = Toolbox.randomInt(0, 10000);
		num2 = Toolbox.randomInt(0, 10000);
		GeneratorTool.ApplyPerlinNoice(MapGenerator._tilesMap, MapGenerator._width, MapGenerator._height, (float)num, (float)num2, 0.1f, 10f * perlinScale, true, GeneratorTarget.Height);
		MapGenerator.addRandomShapes(MapGenerator._random_shapes_amount / 2);
		GeneratorTool.ApplyWaterLevel(MapGenerator._tilesMap, MapGenerator._width, MapGenerator._height, MapGenerator._water_level);
		GeneratorTool.ApplyRingEffect();
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0005E740 File Offset: 0x0005C940
	private static void addRandomShapes(int pAmount)
	{
		for (int i = 0; i < pAmount; i++)
		{
			GeneratorTool.ApplyRandomShape("height", 2f, 0.7f, true);
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0005E770 File Offset: 0x0005C970
	private static void addRandomBiome()
	{
		for (int i = 0; i < MapGenerator._world.islandsCalculator.islands.Count; i++)
		{
			TileIsland tileIsland = MapGenerator._world.islandsCalculator.islands[i];
			if (tileIsland.type == TileLayerType.Ground)
			{
				BiomeContainer random = TopTileLibrary.pool_biomes.GetRandom<BiomeContainer>();
				foreach (MapRegion mapRegion in tileIsland.regions)
				{
					for (int j = 0; j < mapRegion.tiles.Count; j++)
					{
						DropsLibrary.useSeedOn(mapRegion.tiles[j], random.low, random.high);
					}
				}
			}
		}
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0005E840 File Offset: 0x0005CA40
	private static void tryToAddRandomBiomeIslands()
	{
		if (MapGenerator._world.gameStats.data.gameTime > 200.0)
		{
			foreach (TileIsland tileIsland in MapGenerator._world.islandsCalculator.islands)
			{
				if (!Toolbox.randomChance(0.2f) && tileIsland.type == TileLayerType.Ground)
				{
					GeneratorTool.modifyIsland(tileIsland);
				}
			}
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0005E8D0 File Offset: 0x0005CAD0
	private static void freezeMountainTops()
	{
		for (int i = 0; i < MapGenerator._world.tilesList.Count; i++)
		{
			WorldTile worldTile = MapGenerator._world.tilesList[i];
			if (worldTile.Type.IsType("mountains") && worldTile.Height > 220)
			{
				MapAction.freezeTile(worldTile);
			}
		}
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0005E930 File Offset: 0x0005CB30
	private static void spawnVegetation()
	{
		int num = MapGenerator._world.tilesList.Count / 2;
		for (int i = 0; i < num; i++)
		{
			WorldTile random = MapGenerator._world.tilesList.GetRandom<WorldTile>();
			if (random.Type.ground && random.zone.trees.Count < 3 && random.Type.grow_vegetation_auto)
			{
				if (Toolbox.randomBool())
				{
					MapGenerator._world.tryGrowVegetationRandom(random, VegetationType.Plants, true, true);
				}
				else
				{
					MapGenerator._world.tryGrowVegetationRandom(random, VegetationType.Trees, true, true);
				}
			}
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0005E9C0 File Offset: 0x0005CBC0
	private static void spawnResources()
	{
		int num = MapGenerator._world.tilesList.Count / 1000;
		int num2 = num / 2;
		int num3 = num2 / 2;
		int num4 = num3;
		MapGenerator._world.spawnResource(num, "stone", true);
		MapGenerator._world.spawnResource(num3, "gold", true);
		MapGenerator._world.spawnResource(num2, "ore_deposit", true);
		MapGenerator._world.spawnResource(num4, "fruit_bush", false);
		MapGenerator._world.spawnBeehvies(num4 / 2);
	}

	// Token: 0x04000B41 RID: 2881
	private static float _perlinScale = 1f;

	// Token: 0x04000B42 RID: 2882
	private static float _perlinX = 1f;

	// Token: 0x04000B43 RID: 2883
	private static float _perlinY = 1f;

	// Token: 0x04000B44 RID: 2884
	private static int _random_shapes_amount = 0;

	// Token: 0x04000B45 RID: 2885
	private static int _water_level = 0;

	// Token: 0x04000B46 RID: 2886
	private static int _width = 0;

	// Token: 0x04000B47 RID: 2887
	private static int _height = 0;

	// Token: 0x04000B48 RID: 2888
	private static bool _random_shapes;

	// Token: 0x04000B49 RID: 2889
	private static WorldTile[,] _tilesMap;

	// Token: 0x04000B4A RID: 2890
	private static MapBox _world;
}
