using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000A RID: 10
public static class MapAction
{
	// Token: 0x06000029 RID: 41 RVA: 0x0000435F File Offset: 0x0000255F
	public static void init(MapBox pWorld)
	{
		MapAction._world = pWorld;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00004368 File Offset: 0x00002568
	public static void checkAcidTerraform(WorldTile pTile)
	{
		if (pTile.top_type != null && pTile.top_type.wasteland)
		{
			return;
		}
		if (pTile.top_type != null)
		{
			MapAction.decreaseTile(pTile, "flash");
			return;
		}
		if (pTile.Type.ground)
		{
			if (pTile.main_type.rankType == TileRank.Low)
			{
				MapAction.terraformTop(pTile, TopTileLibrary.wasteland_low);
				return;
			}
			if (pTile.main_type.rankType == TileRank.High)
			{
				MapAction.terraformTop(pTile, TopTileLibrary.wasteland_high);
			}
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000043DF File Offset: 0x000025DF
	public static void terraformMain(WorldTile pTile, TileType pType)
	{
		MapAction.terraformTile(pTile, pType, null, TerraformLibrary.flash);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000043EE File Offset: 0x000025EE
	public static void terraformTop(WorldTile pTile, TopTileType pTopType)
	{
		MapAction.terraformTile(pTile, pTile.main_type, pTopType, TerraformLibrary.flash);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00004402 File Offset: 0x00002602
	public static void terraformMain(WorldTile pTile, TileType pType, TerraformOptions pOptions)
	{
		MapAction.terraformTile(pTile, pType, null, pOptions);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000440D File Offset: 0x0000260D
	public static void terraformTop(WorldTile pTile, TopTileType pTopType, TerraformOptions pOptions)
	{
		MapAction.terraformTile(pTile, pTile.main_type, pTopType, pOptions);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00004420 File Offset: 0x00002620
	public static void terraformTile(WorldTile pTile, TileType pNewTypeMain, TopTileType pTopType, TerraformOptions pOptions)
	{
		if (pOptions == null)
		{
			pOptions = TerraformLibrary.flash;
		}
		TileLayerType layerType = pTile.Type.layerType;
		bool rocks = pTile.Type.rocks;
		TileTypeBase type = pTile.Type;
		if (pOptions.removeWater && pTile.Type.ocean)
		{
			pNewTypeMain = pTile.Type.decreaseTo;
		}
		if (pOptions.removeTopTile && pTile.top_type != null)
		{
			pNewTypeMain = pTile.Type.decreaseTo;
		}
		if (pOptions.removeRoads && pTile.Type.road)
		{
			pNewTypeMain = pTile.Type.decreaseTo;
		}
		if (pOptions.removeFrozen && pTile.Type.frozen)
		{
			pNewTypeMain = pTile.Type.decreaseTo;
		}
		if (pNewTypeMain != null)
		{
			pTile.setTileType(pNewTypeMain);
		}
		pTile.setTopTileType(pTopType);
		if (type.canBeFarmField != pTile.Type.canBeFarmField)
		{
			MapBox.instance.cityPlaceFinder.setDirty();
		}
		if (layerType != pTile.Type.layerType || rocks != pTile.Type.rocks)
		{
			MapAction._world.mapChunkManager.setDirty(pTile.chunk, true, true);
			for (int i = 0; i < pTile.chunk.neighboursAll.Count; i++)
			{
				MapChunk pChunk = pTile.chunk.neighboursAll[i];
				MapAction._world.mapChunkManager.setDirty(pChunk, false, true);
			}
		}
		if (pOptions.removeTornado)
		{
			MapAction.tryRemoveTornadoFromTile(pTile);
		}
		if ((pTile.burned_stages > 0 && !pTile.Type.canBeSetOnFire) || pOptions.removeBurned)
		{
			pTile.removeBurn();
		}
		if (pTile.Type.layerType != TileLayerType.Ground)
		{
			MapAction._world.checkCityZone(pTile);
		}
		MapAction._world.redrawTimer = -1f;
		if (pOptions.removeBorders)
		{
			MapAction._world.checkCityZone(pTile);
		}
		if (pOptions.flash)
		{
			MapAction._world.flashEffects.flashPixel(pTile, 20, ColorType.White);
		}
		if (pTile.building != null && pTile.building.stats.canBePlacedOnlyOn != null && pTile.building.stats.canBePlacedOnlyOn.Count > 0)
		{
			bool flag = false;
			for (int j = 0; j < pTile.building.stats.canBePlacedOnlyOn.Count; j++)
			{
				string v = pTile.building.stats.canBePlacedOnlyOn[j];
				if (pTile.Type.IsType(v))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				if (pTile.Type.liquid || pTile.Type.canBeFilledWithOcean)
				{
					pTile.building.startDestroyBuilding(true);
					return;
				}
				pTile.building.startDestroyBuilding(pOptions.removeBuilding);
				return;
			}
		}
		if (pOptions.destroyBuildings && pTile.building != null)
		{
			bool flag2 = false;
			if (pOptions.ignoreKingdoms != null)
			{
				flag2 = true;
				for (int k = 0; k < pOptions.ignoreKingdoms.Length; k++)
				{
					string a = pOptions.ignoreKingdoms[k];
					Kingdom kingdom = pTile.building.kingdom;
					if (!(a != ((kingdom != null) ? kingdom.name : null)))
					{
						flag2 = false;
						break;
					}
				}
			}
			else if (pOptions.destroyOnly != null)
			{
				flag2 = false;
				for (int l = 0; l < pOptions.destroyOnly.Count; l++)
				{
					if (!(pOptions.destroyOnly[l] != pTile.building.stats.race))
					{
						flag2 = true;
						break;
					}
				}
			}
			else if (pOptions.ignoreBuildings != null)
			{
				for (int m = 0; m < pOptions.ignoreBuildings.Count; m++)
				{
					string b = pOptions.ignoreBuildings[m];
					if (!(pTile.building.stats.id == b))
					{
						flag2 = true;
						break;
					}
				}
			}
			else
			{
				flag2 = true;
			}
			if (flag2)
			{
				pTile.building.startDestroyBuilding(pOptions.removeBuilding);
			}
			return;
		}
		if (pTile.building != null && !pTile.building.stats.canBePlacedOnLiquid && pTile.Type.liquid)
		{
			pTile.building.startDestroyBuilding(true);
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00004836 File Offset: 0x00002A36
	public static void setOcean(WorldTile pTile)
	{
		if (pTile.Type.fillToOcean == null)
		{
			return;
		}
		MapAction.terraformMain(pTile, AssetManager.tiles.get(pTile.Type.fillToOcean), AssetManager.terraform.get("water_fill"));
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00004870 File Offset: 0x00002A70
	public static void decreaseTile(WorldTile pTile, TerraformOptions pTerraformOption)
	{
		if (pTile.top_type != null)
		{
			MapAction.terraformTile(pTile, pTile.main_type, null, pTerraformOption);
			return;
		}
		if (pTile.Type.decreaseTo == null)
		{
			return;
		}
		MapAction.terraformMain(pTile, pTile.Type.decreaseTo, pTerraformOption);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000048A9 File Offset: 0x00002AA9
	public static void decreaseTile(WorldTile pTile, string pTerraformOption = "flash")
	{
		MapAction.decreaseTile(pTile, AssetManager.terraform.get(pTerraformOption));
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000048BC File Offset: 0x00002ABC
	public static void increaseTile(WorldTile pTile, string pTerraformOption = "flash")
	{
		if (pTile.top_type != null)
		{
			MapAction.terraformTile(pTile, pTile.main_type, null, AssetManager.terraform.get(pTerraformOption));
			return;
		}
		if (pTile.Type.increaseTo == null)
		{
			return;
		}
		MapAction.terraformMain(pTile, pTile.Type.increaseTo, AssetManager.terraform.get(pTerraformOption));
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004914 File Offset: 0x00002B14
	public static void removeLiquid(WorldTile pTile)
	{
		if (!pTile.Type.liquid)
		{
			return;
		}
		MapAction._world.setTileDirty(pTile, false);
		MapAction.decreaseTile(pTile, "flash");
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000493B File Offset: 0x00002B3B
	public static void growGreens(WorldTile pTile, TopTileType pTopType)
	{
		MapAction.terraformTop(pTile, pTopType, TerraformLibrary.flash);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00004949 File Offset: 0x00002B49
	public static void removeGreens(WorldTile pTile)
	{
		MapAction.decreaseTile(pTile, "flash");
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00004958 File Offset: 0x00002B58
	private static void applyLightningEffect(WorldTile pTile)
	{
		if (pTile.Type.lava && pTile.heat > 20)
		{
			MapAction.decreaseTile(pTile, "flash");
			if (Toolbox.randomChance(0.9f))
			{
				MapAction._world.dropManager.spawnBurstPixel(pTile, "lava", Toolbox.randomFloat(0.2f, 1.4f), Toolbox.randomFloat(0.5f, 1.6f), 0f, -1f);
			}
			AchievementLibrary.achievementLavaStrike.check();
		}
		if (pTile.Type.layerType == TileLayerType.Ocean)
		{
			MapAction.removeLiquid(pTile);
			if (Toolbox.randomChance(0.8f))
			{
				MapAction._world.dropManager.spawnBurstPixel(pTile, "rain", Toolbox.randomFloat(0.2f, 1.4f), Toolbox.randomFloat(0.5f, 1.6f), 0f, -1f);
			}
		}
		if (pTile.building != null && (pTile.building.stats.id == "volcano" || pTile.building.stats.id == "geyser" || pTile.building.stats.id == "geyserAcid"))
		{
			pTile.building.spawnBurstSpecial(10, 4f, 1.9f);
			pTile.building.data.spawnPixelActive = !pTile.building.data.spawnPixelActive;
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00004ADC File Offset: 0x00002CDC
	public static void applyTileDamage(WorldTile pTargetTile, float pRad, TerraformOptions pOptions)
	{
		MapAction._world.redrawTimer = -1f;
		BrushData brushData = Brush.get((int)pRad, "circ_");
		MapAction._world.conwayLayer.checkKillRange(pTargetTile.pos, brushData.size);
		if (pOptions.removeTornado)
		{
			MapAction.tryRemoveTornadoFromTile(pTargetTile);
		}
		MapAction._world.waveController.checkTile(pTargetTile, brushData.size);
		MapAction._world.cloudController.checkTile(pTargetTile, brushData.size);
		for (int i = 0; i < brushData.pos.Count; i++)
		{
			BrushPixelData brushPixelData = brushData.pos[i];
			int num = pTargetTile.pos.x + brushPixelData.x;
			int num2 = pTargetTile.pos.y + brushPixelData.y;
			if (num >= 0 && num < MapBox.width && num2 >= 0 && num2 < MapBox.height)
			{
				WorldTile tileSimple = MapAction._world.GetTileSimple(num, num2);
				if (tileSimple.Type.greyGoo)
				{
					Config.greyGooDamaged = true;
				}
				if (pOptions.addBurned && !tileSimple.Type.liquid)
				{
					tileSimple.setBurned(-1);
				}
				if (pOptions.lightningEffect)
				{
					MapAction.applyLightningEffect(tileSimple);
				}
				if (pOptions.addHeat != 0)
				{
					MapAction._world.heat.addTile(tileSimple, pOptions.addHeat);
				}
				if (tileSimple.building != null && pOptions.damageBuildings)
				{
					bool flag = true;
					if (pOptions.ignoreKingdoms != null)
					{
						for (int j = 0; j < pOptions.ignoreKingdoms.Length; j++)
						{
							string b = pOptions.ignoreKingdoms[j];
							if (!(tileSimple.building == null) && tileSimple.building.data.alive && !tileSimple.building.kingdom.isNature())
							{
								if (tileSimple.building.kingdom == null)
								{
									string str = "KINGDOM NOT FOUND ";
									Building building = tileSimple.building;
									Debug.LogError(str + ((building != null) ? building.ToString() : null) + " " + tileSimple.building.kingdom.id);
								}
								if (tileSimple.building.kingdom.id == b)
								{
									flag = false;
								}
							}
						}
					}
					if (flag)
					{
						tileSimple.building.getHit((float)pOptions.damage, true, AttackType.Other, null, true);
					}
				}
				if (pOptions.setFire)
				{
					tileSimple.setFire(true);
				}
				bool flag2 = false;
				if (pOptions.explode_tile)
				{
					flag2 = MapAction.explodeTile(tileSimple, brushPixelData.dist, pRad, pTargetTile, pOptions);
				}
				if (pOptions.transformToWasteland && !flag2)
				{
					MapAction.checkAcidTerraform(tileSimple);
				}
				if (tileSimple.units.Count > 0 && !string.IsNullOrEmpty(pOptions.addTrait))
				{
					for (int k = 0; k < tileSimple.units.Count; k++)
					{
						Actor actor = tileSimple.units[k];
						if (actor.data.alive)
						{
							actor.addTrait(pOptions.addTrait);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004DE4 File Offset: 0x00002FE4
	public static bool explodeTile(WorldTile pTile, float pDist, float pRadius, WorldTile pExplosionCenter, TerraformOptions pOptions)
	{
		if (pOptions.damage > 0)
		{
			for (int i = 0; i < pTile.units.Count; i++)
			{
				Actor actor = pTile.units[i];
				if (!actor.stats.very_high_flyer || pOptions.applies_to_high_flyers)
				{
					actor.getHit((float)pOptions.damage, true, AttackType.Other, null, true);
				}
			}
		}
		float num = pDist / pRadius;
		float num2 = 1f - num;
		int num3 = (int)(30f * num2);
		if (num3 <= 0)
		{
			return false;
		}
		bool liquid = pTile.Type.liquid;
		if (pOptions.spawnPixels && MapAction.pixelLimit < 30 && Random.value > 0.8f)
		{
			MapAction.pixelLimit++;
			MapAction._world.dropManager.spawnPixel(pTile, pExplosionCenter, true);
		}
		if (!pTile.Type.explodable && Random.value > num2)
		{
			return false;
		}
		MapAction._world.gameStats.data.pixelsExploded++;
		if (pOptions.explosion_pixel_effect)
		{
			MapAction._world.explosionLayer.setDirty(pTile, pDist, pRadius);
		}
		num3 -= (int)((double)num3 * 0.5 * (double)Random.value * (double)num);
		if (pTile.Type.explodable && pTile.explosionWave == 0)
		{
			MapAction._world.explosionLayer.explodeBomb(pTile, false);
		}
		if (pTile.Type.explodableDelayed)
		{
			MapAction._world.explosionLayer.activateDelayedBomb(pTile);
		}
		if (pTile.Type.strength <= pOptions.explode_strength)
		{
			MapAction.decreaseTile(pTile, TerraformLibrary.destroy);
		}
		if (pTile.building != null && pTile.Type.liquid && !pTile.building.stats.canBePlacedOnLiquid)
		{
			pTile.building.startDestroyBuilding(true);
		}
		if (!liquid)
		{
			pTile.setBurned(-1);
			if (pOptions.explode_and_set_random_fire)
			{
				if ((double)Random.value > 0.8)
				{
					pTile.setFire(true);
				}
				else
				{
					pTile.setFire(false);
				}
			}
		}
		return true;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004FF4 File Offset: 0x000031F4
	public static void damageWorld(WorldTile pTile, int pRad, TerraformOptions pOptions)
	{
		if (pOptions.shake)
		{
			MapAction._world.startShake(pOptions.shake_duration, pOptions.shake_interval, pOptions.shake_intensity, true, true);
		}
		if (pOptions.applyForce)
		{
			MapAction._world.applyForce(pTile, pRad, pOptions.force_power, true, false, pOptions.damage, pOptions.ignoreKingdoms, null, pOptions);
		}
		MapAction.applyTileDamage(pTile, (float)pRad, pOptions);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000505C File Offset: 0x0000325C
	public static void wormTile(WorldTile pTile, int pDamage)
	{
		MapAction._world.flashEffects.flashPixel(pTile, 20, ColorType.White);
		pTile.health -= pDamage;
		if (pTile.health <= 0)
		{
			pTile.health = 0;
			if (pTile.Type.increaseTo != null)
			{
				bool flag = pTile.Type.increaseTo.id.StartsWith("mountain", StringComparison.Ordinal);
				bool flag2 = pTile.Type.increaseTo.id.StartsWith("hill", StringComparison.Ordinal);
				if (!pTile.Type.id.StartsWith("road", StringComparison.Ordinal))
				{
					if (!flag && !flag2 && (pTile.Type.decreaseTo == null || Toolbox.randomBool()))
					{
						MapAction.increaseTile(pTile, "destroy");
						return;
					}
					if (pTile.Type.decreaseTo != null)
					{
						MapAction.decreaseTile(pTile, "destroy");
					}
				}
			}
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000513B File Offset: 0x0000333B
	public static void makeTileChanged(WorldTile pTile)
	{
		MapAction._world.redrawTimer = -1f;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000514C File Offset: 0x0000334C
	public static void removeLifeFromTile(WorldTile pTile)
	{
		MapAction._world.conwayLayer.remove(pTile);
		if (pTile.Type.life)
		{
			MapAction.decreaseTile(pTile, "destroy_life");
		}
		Building building = pTile.building;
		if (building != null)
		{
			City city = building.city;
			if (city != null)
			{
				city.killRandomPopPoints(1);
			}
		}
		for (int i = 0; i < pTile.units.Count; i++)
		{
			Actor actor = pTile.units[i];
			if (actor.base_data.alive && actor.isActor() && actor.a.stats.canBeKilledByLifeEraser)
			{
				actor.killHimself(false, AttackType.Other, true, true);
			}
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000051F2 File Offset: 0x000033F2
	public static void createRoad(WorldTile pTile)
	{
		MapAction.terraformTop(pTile, TopTileLibrary.road, AssetManager.terraform.get("road"));
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00005210 File Offset: 0x00003410
	public static void freezeTile(WorldTile pTile)
	{
		if (!string.IsNullOrEmpty(pTile.Type.freezeToID))
		{
			TopTileType pTopType = AssetManager.topTiles.get(pTile.Type.freezeToID);
			MapAction.terraformTop(pTile, pTopType);
		}
		if (pTile.Type.remove_on_freeze && pTile.top_type != null)
		{
			MapAction.terraformTop(pTile, null);
		}
		if (pTile.building && pTile.building.stats.spawnPixel)
		{
			pTile.building.data.spawnPixelActive = false;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00005298 File Offset: 0x00003498
	public static void unfreezeTile(WorldTile pTile)
	{
		if (pTile.Type.frozen)
		{
			MapAction.terraformTop(pTile, null);
		}
		if (pTile.Type.remove_on_heat && pTile.top_type != null)
		{
			MapAction.terraformTop(pTile, null);
		}
		Building building = pTile.building;
		if (building != null && building.stats.spawnPixel)
		{
			pTile.building.data.spawnPixelActive = true;
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00005300 File Offset: 0x00003500
	public static void createRoad(List<WorldTile> pPath, WorldTile pFrom, WorldTile pTarget, bool pFinished = false)
	{
		if (pPath.Count > 20)
		{
			return;
		}
		MapAction.temp_list_tiles2.Clear();
		if (pTarget.roadIsland != null && pTarget.roadIsland == pFrom.roadIsland)
		{
			return;
		}
		for (int i = 0; i < pPath.Count; i++)
		{
			WorldTile worldTile = pPath[i];
			if (!worldTile.Type.road)
			{
				if (pFrom != worldTile && pFrom.roadIsland != null && worldTile.roadIsland == pTarget.roadIsland)
				{
					return;
				}
				MapAction.temp_list_tiles2.Add(worldTile);
				if (pFinished)
				{
					MapAction.createRoad(worldTile);
				}
			}
		}
		MapAction._world.redrawTimer = -1f;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000053A0 File Offset: 0x000035A0
	public static void makeRoadBetween(WorldTile pTile1, WorldTile pTile2, City pCity = null, bool pFinished = false)
	{
		if (pTile1.roadIsland != null && pTile1.roadIsland == pTile2.roadIsland)
		{
			return;
		}
		MapAction.temp_list_tiles.Clear();
		MapAction._world.pathfindingParam.resetParam();
		MapAction._world.pathfindingParam.roads = true;
		MapAction._world.calcPath(pTile1, pTile2, MapAction.temp_list_tiles);
		MapAction.createRoad(MapAction.temp_list_tiles, pTile1, pTile2, pFinished);
		if (pCity != null)
		{
			pCity.addRoads(MapAction.temp_list_tiles2);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00005420 File Offset: 0x00003620
	public static void tryRemoveTornadoFromTile(WorldTile pTile)
	{
		int i = 0;
		while (i < pTile.units.Count)
		{
			Actor actor = pTile.units[i++];
			if (actor.base_data.alive && actor.isActor() && actor.a.stats.flag_tornado)
			{
				actor.GetComponent<Tornado>().killTornado();
			}
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00005484 File Offset: 0x00003684
	public static void checkSantaHit(Vector2Int pPos, int pRad)
	{
		Kingdom kingdomByID = MapAction._world.kingdoms.getKingdomByID("santa");
		if (kingdomByID.units.Count == 0)
		{
			return;
		}
		foreach (Actor actor in kingdomByID.units)
		{
			if (actor.data.alive)
			{
				Vector3 localPosition = actor.transform.localPosition;
				if (Toolbox.Dist((float)pPos.x, 0f, localPosition.x, 0f) <= (float)pRad && localPosition.y >= (float)pPos.y && localPosition.y - 20f <= (float)pPos.y)
				{
					actor.data.health = 0;
					AchievementLibrary.achievementMayday.check();
				}
			}
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00005570 File Offset: 0x00003770
	public static void checkUFOHit(Vector2Int pPos, int pRad)
	{
		Kingdom kingdomByID = MapAction._world.kingdoms.getKingdomByID("aliens");
		if (kingdomByID.units.Count == 0)
		{
			return;
		}
		foreach (Actor actor in kingdomByID.units)
		{
			if (actor.data.alive && Toolbox.Dist((float)pPos.x, 0f, actor.transform.localPosition.x, 0f) <= (float)pRad && actor.transform.position.y >= (float)pPos.y && actor.transform.position.y - 10f <= (float)pPos.y && actor.stats.flag_ufo)
			{
				actor.getHit(10000f, true, AttackType.Other, null, true);
				actor.killHimself(false, AttackType.None, false, true);
			}
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000567C File Offset: 0x0000387C
	public static void checkLightningAction(Vector2Int pPos, int pRad)
	{
		bool flag = false;
		List<Actor> simpleList = MapAction._world.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (Toolbox.DistVec2(actor.currentTile.pos, pPos) <= (float)pRad)
			{
				if (actor.stats.id == "godFinger")
				{
					actor.GetComponent<GodFinger>().lightAction();
				}
				else if (actor.stats.id == "tornado")
				{
					if (actor.GetComponent<Tornado>().split(true))
					{
						return;
					}
				}
				else if (!actor.haveTrait("immortal") && !flag && Toolbox.randomChance(0.2f))
				{
					actor.addTrait("immortal");
					actor.addTrait("energized");
					flag = true;
				}
			}
		}
	}

	// Token: 0x04000031 RID: 49
	private static MapBox _world;

	// Token: 0x04000032 RID: 50
	public static int pixelLimit;

	// Token: 0x04000033 RID: 51
	private static List<WorldTile> temp_list_tiles = new List<WorldTile>();

	// Token: 0x04000034 RID: 52
	private static List<WorldTile> temp_list_tiles2 = new List<WorldTile>();
}
