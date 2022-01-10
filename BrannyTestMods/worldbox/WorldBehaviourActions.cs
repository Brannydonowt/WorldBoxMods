using System;

// Token: 0x020000F0 RID: 240
public static class WorldBehaviourActions
{
	// Token: 0x060004FB RID: 1275 RVA: 0x00040F9C File Offset: 0x0003F19C
	public static void updateDisasters()
	{
		DisasterAsset randomAssetFromPool = AssetManager.disasters.getRandomAssetFromPool();
		if (randomAssetFromPool == null)
		{
			return;
		}
		if (!Toolbox.randomChance(randomAssetFromPool.chance))
		{
			return;
		}
		if (randomAssetFromPool == null)
		{
			return;
		}
		randomAssetFromPool.action(randomAssetFromPool);
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00040FD8 File Offset: 0x0003F1D8
	public static void updateRoadDegeneration()
	{
		MapBox.instance.zoneCalculator.zones.ShuffleOne<TileZone>();
		TileZone tileZone = MapBox.instance.zoneCalculator.zones[0];
		if (tileZone.city != null)
		{
			return;
		}
		if (Toolbox.randomBool())
		{
			return;
		}
		for (int i = 0; i < 64; i++)
		{
			tileZone.tiles.ShuffleOne(i);
			if (tileZone.tiles[i].Type.road && !Toolbox.randomBool())
			{
				MapAction.decreaseTile(tileZone.tiles[i], "flash");
			}
		}
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00041074 File Offset: 0x0003F274
	public static void updateUnitSpawn()
	{
		if (!MapBox.instance.worldLaws.world_law_animals_spawn.boolVal)
		{
			return;
		}
		if (MapBox.instance.mapChunkManager.list.Count == 0)
		{
			return;
		}
		TileIsland tileIsland = null;
		if (MapBox.instance.islandsCalculator.islands_ground.Count > 0)
		{
			tileIsland = MapBox.instance.islandsCalculator.getRandomIslandGround(true);
		}
		if (tileIsland == null)
		{
			return;
		}
		WorldTile randomTile = tileIsland.getRandomTile();
		if (!randomTile.Type.spawn_units_auto)
		{
			return;
		}
		ActorStats actorStats = AssetManager.unitStats.get(randomTile.Type.spawn_units_list.GetRandom<string>());
		if (actorStats == null)
		{
			return;
		}
		if (actorStats.currentAmount > actorStats.maxRandomAmount)
		{
			return;
		}
		MapBox.instance.getObjectsInChunks(randomTile, 0, MapObjectType.Actor);
		if (MapBox.instance.temp_map_objects.Count > 3)
		{
			return;
		}
		MapBox.instance.spawnNewUnit(actorStats.id, randomTile, string.Empty, 0f);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0004115C File Offset: 0x0003F35C
	public static void growVegetation()
	{
		if (!MapBox.instance.worldLaws.world_law_grow_trees.boolVal)
		{
			return;
		}
		for (int i = 0; i < MapBox.instance.islandsCalculator.islands.Count; i++)
		{
			TileIsland tileIsland = MapBox.instance.islandsCalculator.islands[i];
			if (tileIsland.type != TileLayerType.Block && tileIsland.type != TileLayerType.Lava && tileIsland.type != TileLayerType.Goo && tileIsland.type != TileLayerType.Ocean)
			{
				MapRegion random = tileIsland.regions.GetRandom();
				int num = tileIsland.regions.Count / 20 + 1;
				if (num > 3)
				{
					num = 3;
				}
				random.tiles.ShuffleOne<WorldTile>();
				for (int j = 0; j < num; j++)
				{
					int k = 0;
					while (k < random.tiles.Count)
					{
						WorldTile worldTile = random.tiles[k];
						if (!worldTile.data.fire && worldTile.Type.grow_vegetation_auto && !Toolbox.randomBool())
						{
							if (Toolbox.randomBool())
							{
								if (worldTile.Type.grow_type_selector_trees != null)
								{
									MapBox.instance.tryGrowVegetationRandom(worldTile, VegetationType.Trees, false, true);
									break;
								}
								break;
							}
							else
							{
								if (worldTile.Type.grow_type_selector_plants != null)
								{
									MapBox.instance.tryGrowVegetationRandom(worldTile, VegetationType.Plants, false, true);
									break;
								}
								break;
							}
						}
						else
						{
							k++;
						}
					}
				}
			}
		}
	}
}
