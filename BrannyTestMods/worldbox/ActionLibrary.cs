using System;
using System.Collections.Generic;
using ai;
using UnityEngine;

// Token: 0x02000014 RID: 20
public static class ActionLibrary
{
	// Token: 0x0600007E RID: 126 RVA: 0x00006588 File Offset: 0x00004788
	public static bool pyromaniacEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.kingdom == null)
		{
			return false;
		}
		if (pTarget.a.attackTarget == null)
		{
			return false;
		}
		if (Toolbox.randomChance(0.95f))
		{
			return false;
		}
		WorldTile tileNearby;
		if (Toolbox.randomChance(0.5f) && pTarget.a.attackTarget != null)
		{
			tileNearby = ActorTool.getTileNearby(ActorTileTarget.RandomTileWithCivStructures, pTarget.a.attackTarget.currentTile.chunk);
		}
		else
		{
			tileNearby = ActorTool.getTileNearby(ActorTileTarget.RandomTileWithCivStructures, pTarget.currentTile.chunk);
		}
		if (tileNearby == null)
		{
			return false;
		}
		if (!ActionLibrary.canThrowBomb(pTarget, tileNearby))
		{
			return false;
		}
		City city = tileNearby.zone.city;
		if (((city != null) ? city.kingdom : null) != null && !pTarget.kingdom.isEnemy(tileNearby.zone.city.kingdom))
		{
			return false;
		}
		ActionLibrary.throwTorchAtTile(pTarget, tileNearby);
		return true;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00006664 File Offset: 0x00004864
	public static void throwTorchAtTile(BaseSimObject pTarget, WorldTile pTile)
	{
		Vector2Int pos = pTile.pos;
		float pDist = Vector2.Distance(pTarget.currentPosition, pos);
		Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true);
		Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.curStats.size, true);
		newPoint2.y += 0.5f;
		MapBox.instance.stackEffects.startProjectile(newPoint2, newPoint, "torch", 0f);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00006724 File Offset: 0x00004924
	public static bool bombermanEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (Toolbox.randomChance(0.98f))
		{
			return false;
		}
		WorldTile worldTile = null;
		if (pTarget.a.attackTarget != null && Toolbox.DistVec3(pTarget.a.currentPosition, pTarget.a.attackTarget.currentPosition) > 4f)
		{
			worldTile = pTarget.a.attackTarget.currentTile;
		}
		if (worldTile == null && Toolbox.randomChance(0.5f))
		{
			worldTile = ActorTool.getTileNearby(ActorTileTarget.RandomTileWithUnits, pTarget.currentTile.chunk);
			if (worldTile != null)
			{
				bool flag = false;
				for (int i = 0; i < worldTile.chunk.k_list_objects.Count; i++)
				{
					Kingdom kingdom = worldTile.chunk.k_list_objects[i];
					if (!kingdom.isNature() && pTarget.kingdom.isEnemy(kingdom))
					{
						worldTile = pTarget.currentTile;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					worldTile = null;
				}
			}
		}
		if (worldTile == null && Toolbox.randomChance(0.5f))
		{
			worldTile = ActorTool.getTileNearby(ActorTileTarget.RandomTNT, pTarget.currentTile.chunk);
		}
		if (worldTile == null)
		{
			worldTile = ActorTool.getTileNearby(ActorTileTarget.RandomBurnableTile, pTarget.currentTile.chunk);
		}
		if (worldTile == null)
		{
			return false;
		}
		if (!ActionLibrary.canThrowBomb(pTarget, worldTile))
		{
			return false;
		}
		ActionLibrary.throwBombAtTile(pTarget, worldTile);
		return true;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00006860 File Offset: 0x00004A60
	public static bool canThrowBomb(BaseSimObject pTarget, WorldTile pTile)
	{
		float num = Toolbox.DistVec2Float(pTarget.a.currentPosition, pTile.pos);
		return num > 3f && num < 26f;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000689C File Offset: 0x00004A9C
	public static void throwBombAtTile(BaseSimObject pTarget, WorldTile pTile)
	{
		Vector2Int pos = pTile.pos;
		float pDist = Vector2.Distance(pTarget.currentPosition, pos);
		Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true);
		Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.curStats.size, true);
		newPoint2.y += 0.5f;
		MapBox.instance.stackEffects.startProjectile(newPoint2, newPoint, "firebomb", 0f);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00006959 File Offset: 0x00004B59
	public static bool zombieEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.spawnParticle(Toolbox.color_infected);
		if (Toolbox.randomChance(0.25f))
		{
			pTarget.a.startShake(0.2f, 0.05f, true, false);
		}
		return true;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00006990 File Offset: 0x00004B90
	public static bool infectedEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.spawnParticle(Toolbox.color_infected);
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		if (Toolbox.randomChance(0.05f))
		{
			int val = pTarget.a.data.health / 10;
			pTarget.a.getHit((float)Math.Max(val, 100), true, AttackType.Other, null, true);
		}
		return true;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00006A00 File Offset: 0x00004C00
	public static bool mushSporesEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
		int num = 3;
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			MapBox.instance.temp_map_objects.ShuffleOne(i);
			Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
			if (!(actor == pTarget.a) && !Toolbox.randomChance(0.7f) && actor.addTrait("mushSpores"))
			{
				actor.spawnParticle(Toolbox.color_mushSpores);
				num--;
				if (num == 0)
				{
					break;
				}
			}
		}
		return true;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00006A98 File Offset: 0x00004C98
	public static bool tumorEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		if (Toolbox.randomChance(0.1f))
		{
			pTarget.a.getHit((float)pTarget.a.curStats.health * 0.1f, false, AttackType.Tumor, null, false);
		}
		return true;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00006AF0 File Offset: 0x00004CF0
	public static bool healingAuraEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (Toolbox.randomChance(0.2f))
		{
			MapBox.instance.getObjectsInChunks(pTile, 4, MapObjectType.Actor);
			for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
			{
				Actor actor = (Actor)MapBox.instance.temp_map_objects[i];
				if (!(actor == pTarget.a) && actor.data.health < actor.curStats.health)
				{
					actor.restoreHealth(10);
					actor.spawnParticle(Toolbox.color_heal);
					actor.removeTrait("plague");
				}
			}
		}
		return true;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00006B90 File Offset: 0x00004D90
	public static bool regenerationEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.a.haveTrait("infected"))
		{
			return true;
		}
		bool flag = true;
		if (pTarget.a.stats.needFood)
		{
			flag = (pTarget.a.data.hunger > 0);
		}
		if (pTarget.a.data.health != pTarget.a.curStats.health && flag && Toolbox.randomChance(0.2f))
		{
			pTarget.a.restoreHealth(1);
			pTarget.a.spawnParticle(Toolbox.color_heal);
		}
		return true;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00006C2B File Offset: 0x00004E2B
	public static bool coldAuraEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		MapBox.instance.loopWithBrush(pTarget.currentTile, Brush.get(4, "circ_"), new PowerActionWithID(PowerLibrary.drawTemperatureMinus), "coldAura");
		return true;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00006C5C File Offset: 0x00004E5C
	public static bool plagueEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		ActionLibrary.tickPlagueInfection(pTarget.a);
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		if (Toolbox.randomChance(0.07f))
		{
			pTarget.a.getHit((float)pTarget.a.curStats.health * 0.1f, false, AttackType.Plague, null, false);
		}
		return true;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00006CBE File Offset: 0x00004EBE
	public static bool ratEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (Toolbox.randomChance(0.7f) && ActorTool.countRatsNearby(pTarget.a) > 10 && Toolbox.randomChance(0.2f))
		{
			ActionLibrary.tickPlagueInfection(pTarget.a);
		}
		return true;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00006CF4 File Offset: 0x00004EF4
	private static void tickPlagueInfection(Actor pActor)
	{
		pActor.spawnParticle(Toolbox.color_plague);
		if (Toolbox.randomChance(0.05f))
		{
			for (int i = 0; i < pActor.currentTile.chunk.k_list_objects.Count; i++)
			{
				Kingdom kingdom = pActor.currentTile.chunk.k_list_objects[i];
				List<BaseSimObject> list = pActor.currentTile.chunk.k_dict_objects[kingdom];
				for (int j = 0; j < list.Count; j++)
				{
					BaseSimObject baseSimObject = list[j];
					if (baseSimObject.objectType == MapObjectType.Actor && baseSimObject.base_data.alive && !Toolbox.randomBool() && Toolbox.DistVec3(pActor.currentPosition, baseSimObject.currentPosition) <= 6f)
					{
						baseSimObject.a.addTrait("plague");
					}
				}
			}
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00006DDC File Offset: 0x00004FDC
	public static bool burningFeetEffectTileDraw(WorldTile pTile, string pPowerID)
	{
		if (pTile.Type.frozen && Toolbox.randomBool())
		{
			MapAction.unfreezeTile(pTile);
		}
		return true;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00006DFC File Offset: 0x00004FFC
	public static bool burningFeetEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		MapBox.instance.loopWithBrush(pTarget.currentTile, Brush.get(4, "circ_"), new PowerActionWithID(ActionLibrary.burningFeetEffectTileDraw), "burningFeetEffect");
		if (!pTarget.a.isInLiquid())
		{
			pTarget.currentTile.setFire(true);
		}
		for (int i = 0; i < pTarget.currentTile.neighbours.Count; i++)
		{
			WorldTile worldTile = pTarget.currentTile.neighbours[i];
			worldTile.setFire(false);
			worldTile.setBurned(-1);
		}
		return true;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00006E8C File Offset: 0x0000508C
	public static bool flowerPrintsEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!Toolbox.randomChance(0.3f))
		{
			return false;
		}
		WorldTile currentTile = pTarget.a.currentTile;
		if (!currentTile.Type.grow_vegetation_auto)
		{
			return false;
		}
		if (currentTile.Type.grow_type_selector_plants != null)
		{
			MapBox.instance.tryGrowVegetationRandom(currentTile, VegetationType.Plants, false, true);
		}
		return true;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00006EE0 File Offset: 0x000050E0
	public static bool acidBloodEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		WorldTile currentTile = pTarget.a.currentTile;
		for (int i = 0; i < 25; i++)
		{
			if (!Toolbox.randomBool())
			{
				float pForce = Toolbox.randomFloat(0.05f, 0.5f);
				float pForceZ = Toolbox.randomFloat(0.05f, 1f);
				MapBox.instance.dropManager.spawnBurstPixel(currentTile, "acid", pForce, pForceZ, 0f, -1f);
			}
		}
		return true;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00006F4F File Offset: 0x0000514F
	public static bool acidTouchEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!Toolbox.randomChance(0.3f))
		{
			return false;
		}
		MapAction.checkAcidTerraform(pTarget.a.currentTile);
		return true;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00006F70 File Offset: 0x00005170
	public static bool castSpawnSkeleton(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.currentTile;
		}
		Toolbox.findSameUnitInChunkAround(pTile.chunk, "skeleton");
		if (Toolbox.temp_list_units.Count > 6)
		{
			return false;
		}
		WorldTile worldTile;
		if (pTile == null)
		{
			worldTile = null;
		}
		else
		{
			MapRegion region = pTile.region;
			worldTile = ((region != null) ? region.tiles.GetRandom<WorldTile>() : null);
		}
		WorldTile worldTile2 = worldTile;
		if (worldTile2 == null)
		{
			return false;
		}
		ActionLibrary.spawnSkeleton(null, worldTile2);
		return true;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00006FDC File Offset: 0x000051DC
	public static bool spawnSkeleton(BaseSimObject pTarget, WorldTile pTile = null)
	{
		ActionLibrary.<>c__DisplayClass21_0 CS$<>8__locals1 = new ActionLibrary.<>c__DisplayClass21_0();
		CS$<>8__locals1.pTile = pTile;
		if (CS$<>8__locals1.pTile == null)
		{
			return false;
		}
		MapBox.instance.stackEffects.get("fx_create_skeleton").spawnAt(CS$<>8__locals1.pTile.posV3, 0.1f).setCallback(19, new BaseCallback(CS$<>8__locals1.<spawnSkeleton>g__spawnSkeletonCallback|0));
		return true;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00007040 File Offset: 0x00005240
	public static bool castFire(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.currentTile;
		}
		if (pTile == null)
		{
			return false;
		}
		MapBox.instance.dropManager.spawn(pTile, "fire", 15f, -1f);
		for (int i = 0; i < 3; i++)
		{
			MapBox.instance.dropManager.spawn(pTile.neighboursAll.GetRandom<WorldTile>(), "fire", 15f, -1f);
		}
		return true;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000070BA File Offset: 0x000052BA
	public static bool castBloodRain(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.currentTile;
		}
		if (pTile == null)
		{
			return false;
		}
		MapBox.instance.dropManager.spawn(pTile, "bloodRain", 15f, -1f);
		return true;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000070F3 File Offset: 0x000052F3
	public static bool castSpawnGrassSeeds(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			pTile = pTarget.currentTile;
		}
		if (pTile == null)
		{
			return false;
		}
		MapBox.instance.dropManager.spawn(pTile, "seedsGrass", 15f, -1f);
		return true;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00007126 File Offset: 0x00005326
	public static bool castCurses(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget;
		if (pTile == null)
		{
			return false;
		}
		MapBox.instance.dropManager.spawn(pTile, "curse", 15f, -1f);
		return true;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00007155 File Offset: 0x00005355
	public static bool castLightning(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.currentTile;
		}
		MapBox.spawnLightning(pTile, 0.15f);
		return true;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00007174 File Offset: 0x00005374
	public static bool castTornado(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.currentTile;
		}
		if (pTile == null)
		{
			return false;
		}
		MapBox.instance.createNewUnit("tornado", pTile, null, 0f, null).GetComponent<Tornado>().resizeTornado(Tornado.TORNADO_SCALE_DEFAULT / 3f);
		return true;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000071C4 File Offset: 0x000053C4
	public static bool createBlueOrbs(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.currentTile;
		}
		Vector3 pStart = new Vector3(pTile.posV3.x, pTile.posV3.y);
		for (int i = 0; i < pTile.region.tiles.Count; i++)
		{
			WorldTile worldTile = pTile.region.tiles[i];
			if (Toolbox.randomChance(0.1f))
			{
				MapBox.instance.stackEffects.startProjectile(pStart, worldTile.posV3, "blue_orb_small", 0f);
			}
		}
		return true;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000725A File Offset: 0x0000545A
	public static bool castCure(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			pTile = pTarget.currentTile;
		}
		if (pTile == null)
		{
			return false;
		}
		MapBox.instance.dropManager.spawn(pTile, "cure", 15f, -1f);
		return true;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000728D File Offset: 0x0000548D
	public static bool castShieldOnHimself(BaseSimObject pTarget, WorldTile pTile = null)
	{
		return ActionLibrary.addShieldEffectOnTarget(pTarget, null);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00007296 File Offset: 0x00005496
	public static bool addShieldEffectOnTarget(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.haveStatus("shield"))
		{
			return false;
		}
		pTarget.a.addStatusEffect("shield", 30f);
		return true;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000072C0 File Offset: 0x000054C0
	public static bool addBurningEffectOnTarget(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isBuilding() && pTarget.b.stats.burnable)
		{
			pTarget.addStatusEffect("burning", -1f);
			return true;
		}
		if (pTarget.isActor())
		{
			pTarget.addStatusEffect("burning", -1f);
			return true;
		}
		return false;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00007314 File Offset: 0x00005514
	public static bool addFrozenEffectOnTarget20(BaseSimObject pTarget, WorldTile pTile = null)
	{
		return Toolbox.randomChance(0.2f) && ActionLibrary.addFrozenEffectOnTarget(pTarget, pTile);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000732B File Offset: 0x0000552B
	public static bool addFrozenEffectOnTarget(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isBuilding())
		{
			return false;
		}
		pTarget.addStatusEffect("frozen", -1f);
		return true;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00007348 File Offset: 0x00005548
	public static bool addSlowEffectOnTarget20(BaseSimObject pTarget, WorldTile pTile = null)
	{
		return Toolbox.randomChance(0.2f) && ActionLibrary.addSlowEffectOnTarget(pTarget, pTile);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000735F File Offset: 0x0000555F
	public static bool addSlowEffectOnTarget(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isBuilding())
		{
			return false;
		}
		pTarget.addStatusEffect("slowness", -1f);
		return true;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000737C File Offset: 0x0000557C
	public static bool addPoisonedEffectOnTarget(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTarget.isActor())
		{
			return false;
		}
		if (pTarget.a.haveTrait("poison_immune"))
		{
			return false;
		}
		if (!pTarget.a.stats.have_skin)
		{
			return false;
		}
		if (pTarget.a.stats.immune_to_injuries)
		{
			return false;
		}
		if (Toolbox.randomChance(0.3f))
		{
			pTarget.a.addStatusEffect("poisoned", -1f);
		}
		return false;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000073F1 File Offset: 0x000055F1
	public static void increaseDroppedBombsCounter(WorldTile pTile = null, string pDropID = null)
	{
		MapBox.instance.gameStats.data.bombsDropped++;
		AchievementLibrary.achievementManyBombs.check();
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00007420 File Offset: 0x00005620
	public static bool mageDeath(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isActor())
		{
			Actor a = pTarget.a;
			if (((a != null) ? a.attackedBy : null) != null)
			{
				BaseSimObject attackedBy = pTarget.a.attackedBy;
				if (attackedBy != null)
				{
					Actor a2 = attackedBy.a;
					if (a2 != null)
					{
						a2.addTrait("mageslayer");
					}
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000747C File Offset: 0x0000567C
	public static bool dragonDeath(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isActor())
		{
			Actor a = pTarget.a;
			if (((a != null) ? a.attackedBy : null) != null)
			{
				BaseSimObject attackedBy = pTarget.a.attackedBy;
				if (attackedBy != null)
				{
					Actor a2 = attackedBy.a;
					if (a2 != null)
					{
						a2.addTrait("dragonslayer");
					}
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000074D5 File Offset: 0x000056D5
	public static bool giveCursed(WorldTile pTile, ActorBase pActor)
	{
		pActor.addTrait("cursed");
		pActor.removeTrait("blessed");
		return false;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000074EF File Offset: 0x000056EF
	public static bool giveBlessed(WorldTile pTile, ActorBase pActor)
	{
		pActor.removeTrait("cursed");
		pActor.addTrait("blessed");
		return false;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000750C File Offset: 0x0000570C
	public static bool spawnGhost(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTarget.isActor())
		{
			return false;
		}
		if (!pTarget.a.stats.have_soul)
		{
			return false;
		}
		Actor actor = MapBox.instance.createNewUnit("ghost", pTile, null, 0f, null);
		actor.removeTrait("blessed");
		ActorTool.copyUnitToOtherUnit(pTarget.a, actor);
		return true;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00007567 File Offset: 0x00005767
	public static bool tryToGrowBiomeGrass(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTile.Type.canGrowBiomeGrass)
		{
			return false;
		}
		if (pTile.Type.biome != string.Empty)
		{
			return false;
		}
		DropsLibrary.useSeedOn(pTile, TopTileLibrary.grass_low, TopTileLibrary.grass_high);
		return true;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x000075A2 File Offset: 0x000057A2
	public static bool tryToGrowTree(BaseSimObject pTarget, WorldTile pTile = null)
	{
		MapBox.instance.tryGrowVegetationRandom(pTile, VegetationType.Trees, false, false);
		return true;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000075B3 File Offset: 0x000057B3
	public static bool tryToCreatePlants(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.currentTile.Type.grow_type_selector_plants != null)
		{
			MapBox.instance.tryGrowVegetationRandom(pTarget.currentTile, VegetationType.Plants, false, true);
		}
		return true;
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000075DB File Offset: 0x000057DB
	public static bool startNuke(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.findCurrentTile(true);
		MapBox.instance.stackEffects.spawnNukeFlash(pTile, "atomicBomb");
		return true;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00007600 File Offset: 0x00005800
	public static bool spawnAliens(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.findCurrentTile(true);
		int num = 1;
		if (Toolbox.randomChance(0.5f))
		{
			num++;
		}
		if (Toolbox.randomChance(0.1f))
		{
			num++;
		}
		for (int i = 0; i < num; i++)
		{
			MapBox.instance.createNewUnit("alien", pTarget.a.currentTile, null, pTarget.a.zPosition.y, null);
		}
		return true;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00007678 File Offset: 0x00005878
	public static bool fireBlood(BaseSimObject pTarget, WorldTile pTile = null)
	{
		for (int i = 0; i < 5; i++)
		{
			if (Toolbox.randomBool())
			{
				MapBox.instance.dropManager.spawnBurstPixel(pTarget.a.currentTile, "fire", 0f, Toolbox.randomFloat(0.6f, 1.1f), 0.3f, 0.15f);
			}
		}
		return true;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000076D8 File Offset: 0x000058D8
	public static bool teleportRandom(BaseSimObject pTarget, WorldTile pTile = null)
	{
		TileIsland randomIslandGround = MapBox.instance.islandsCalculator.getRandomIslandGround(true);
		WorldTile worldTile;
		if (randomIslandGround == null)
		{
			worldTile = null;
		}
		else
		{
			MapRegion random = randomIslandGround.regions.GetRandom();
			worldTile = ((random != null) ? random.tiles.GetRandom<WorldTile>() : null);
		}
		WorldTile worldTile2 = worldTile;
		if (worldTile2 == null)
		{
			return false;
		}
		if (worldTile2.Type.block)
		{
			return false;
		}
		if (!worldTile2.Type.ground)
		{
			return false;
		}
		string text = pTarget.a.stats.effect_teleport;
		if (text == string.Empty)
		{
			text = "fx_teleport_blue";
		}
		if (!MapBox.instance.qualityChanger.lowRes)
		{
			MapBox.instance.stackEffects.get(text).spawnAt(pTarget.currentPosition, pTarget.a.curStats.scale);
			BaseEffect baseEffect = MapBox.instance.stackEffects.get(text).spawnAt(worldTile2.posV3, pTarget.a.curStats.scale);
			if (baseEffect != null)
			{
				baseEffect.spriteAnimation.setFrameIndex(9);
			}
		}
		pTarget.a.cancelAllBeh(null);
		pTarget.a.spawnOn(worldTile2, 0f);
		return true;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00007804 File Offset: 0x00005A04
	public static bool turnIntoMush(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor a = pTarget.a;
		if (a.gameObject == null)
		{
			return false;
		}
		if (a == null)
		{
			return false;
		}
		if (!a.haveTrait("mushSpores"))
		{
			return false;
		}
		if (!a.inMapBorder())
		{
			return false;
		}
		if (!a.stats.canTurnIntoMush)
		{
			return false;
		}
		a.removeTrait("cursed");
		a.removeTrait("infected");
		a.removeTrait("mushSpores");
		a.removeTrait("tumorInfection");
		Actor actor = MapBox.instance.createNewUnit(a.stats.mushID, a.currentTile, null, 0f, null);
		ActorTool.copyUnitToOtherUnit(a, actor);
		if (!MapBox.instance.qualityChanger.lowRes)
		{
			MapBox.instance.stackEffects.startSpawnEffect(actor.currentTile, "spawn");
		}
		MapBox.instance.destroyActor((Actor)pTarget);
		return true;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x000078F0 File Offset: 0x00005AF0
	public static bool turnIntoTumorMonster(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor a = pTarget.a;
		if (a.gameObject == null)
		{
			return false;
		}
		if (a == null)
		{
			return false;
		}
		if (!a.haveTrait("tumorInfection"))
		{
			return false;
		}
		if (!a.inMapBorder())
		{
			return false;
		}
		if (!a.stats.canTurnIntoTumorMonster)
		{
			return false;
		}
		a.removeTrait("cursed");
		a.removeTrait("infected");
		a.removeTrait("mushSpores");
		a.removeTrait("tumorInfection");
		Actor actor = MapBox.instance.createNewUnit(a.stats.tumorMonsterID, a.currentTile, null, 0f, null);
		ActorTool.copyUnitToOtherUnit(a, actor);
		if (!MapBox.instance.qualityChanger.lowRes)
		{
			MapBox.instance.stackEffects.startSpawnEffect(actor.currentTile, "spawn");
		}
		MapBox.instance.destroyActor((Actor)pTarget);
		return true;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000079DC File Offset: 0x00005BDC
	public static bool turnIntoZombie(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor a = pTarget.a;
		if (a.gameObject == null)
		{
			return false;
		}
		if (a == null)
		{
			return false;
		}
		if (!a.haveTrait("infected"))
		{
			return false;
		}
		if (!a.inMapBorder())
		{
			return false;
		}
		if (!a.stats.canTurnIntoZombie)
		{
			return false;
		}
		a.removeTrait("cursed");
		a.removeTrait("infected");
		a.removeTrait("mushSpores");
		a.removeTrait("tumorInfection");
		string pStatsID;
		if (string.IsNullOrEmpty(a.stats.zombieID))
		{
			pStatsID = "zombie";
		}
		else
		{
			pStatsID = a.stats.zombieID;
		}
		if (a.stats.id == "dragon")
		{
			a.removeTrait("fire_blood");
			a.removeTrait("fire_proof");
		}
		Actor actor = MapBox.instance.createNewUnit(pStatsID, a.currentTile, null, 0f, null);
		ActorTool.copyUnitToOtherUnit(a, actor);
		actor.removeTrait("fast");
		actor.removeTrait("agile");
		actor.removeTrait("genius");
		actor.data.firstName = "Un" + Toolbox.LowerCaseFirst(a.data.firstName);
		if (!MapBox.instance.qualityChanger.lowRes)
		{
			MapBox.instance.stackEffects.startSpawnEffect(actor.currentTile, "spawn");
		}
		MapBox.instance.destroyActor((Actor)pTarget);
		return true;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00007B58 File Offset: 0x00005D58
	public static bool turnIntoSkeleton(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor a = pTarget.a;
		if (a.gameObject == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(a.stats.skeletonID))
		{
			return false;
		}
		if (a == null)
		{
			return false;
		}
		if (!a.haveTrait("cursed"))
		{
			return false;
		}
		if (!a.inMapBorder())
		{
			return false;
		}
		string skeletonID = a.stats.skeletonID;
		a.removeTrait("cursed");
		a.removeTrait("infected");
		a.removeTrait("mushSpores");
		a.removeTrait("tumorInfection");
		Actor actor = MapBox.instance.createNewUnit(skeletonID, a.currentTile, null, 0f, null);
		ActorTool.copyUnitToOtherUnit(a, actor);
		actor.data.firstName = "Un" + Toolbox.LowerCaseFirst(a.data.firstName);
		if (!MapBox.instance.qualityChanger.lowRes)
		{
			MapBox.instance.stackEffects.startSpawnEffect(actor.currentTile, "spawn");
		}
		MapBox.instance.destroyActor((Actor)pTarget);
		return true;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00007C70 File Offset: 0x00005E70
	public static Actor getActorNearPos(Vector2 pPos)
	{
		Actor actor = null;
		float num = 0f;
		List<Actor> simpleList = MapBox.instance.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor2 = simpleList[i];
			if (actor2.data.alive && actor2.stats.canBeInspected && !actor2.isInsideSomething())
			{
				float num2 = Toolbox.DistVec2Float(actor2.currentPosition, pPos);
				if (num2 <= 3f && (actor == null || num2 < num))
				{
					actor = actor2;
					num = num2;
				}
			}
		}
		return actor;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00007D08 File Offset: 0x00005F08
	public static Actor getActorFromTile(WorldTile pTile = null)
	{
		if (pTile == null)
		{
			return null;
		}
		Actor actor = null;
		float num = 0f;
		List<Actor> simpleList = MapBox.instance.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor2 = simpleList[i];
			if (actor2.data.alive && actor2.stats.canBeInspected && !actor2.isInsideSomething())
			{
				float num2 = Toolbox.DistTile(actor2.currentTile, pTile);
				if (num2 <= 3f && (actor == null || num2 < num))
				{
					actor = actor2;
					num = num2;
				}
			}
		}
		return actor;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00007DA8 File Offset: 0x00005FA8
	public static bool inspectUnit(WorldTile pTile = null, string pPower = null)
	{
		Actor actor;
		if (pTile == null)
		{
			actor = MapBox.instance.getActorNearCursor();
		}
		else
		{
			actor = ActionLibrary.getActorFromTile(pTile);
		}
		if (actor == null)
		{
			return false;
		}
		Config.selectedUnit = actor;
		ScrollWindow.showWindow("inspect_unit");
		return true;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00007DEA File Offset: 0x00005FEA
	public static bool inspectCity(WorldTile pTile = null, string pPower = null)
	{
		if (pTile.zone.city != null)
		{
			Config.selectedCity = pTile.zone.city;
			ScrollWindow.showWindow("village");
		}
		return true;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00007E1A File Offset: 0x0000601A
	public static bool inspectKingdom(WorldTile pTile = null, string pPower = null)
	{
		if (pTile != null)
		{
			City city = pTile.zone.city;
			if (((city != null) ? city.kingdom : null) != null)
			{
				Config.selectedKingdom = pTile.zone.city.kingdom;
				ScrollWindow.showWindow("kingdom");
			}
		}
		return true;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00007E58 File Offset: 0x00006058
	public static bool inspectCulture(WorldTile pTile = null, string pPower = null)
	{
		if (pTile != null && pTile.zone.culture != null)
		{
			Config.selectedCulture = pTile.zone.culture;
			ScrollWindow.showWindow("culture");
		}
		return true;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00007E88 File Offset: 0x00006088
	public static bool action_fire(BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		if (!MapBox.instance.flashEffects.contains(pTile) && Toolbox.randomChance(0.2f))
		{
			MapBox.instance.particlesFire.spawn(pTile.posV3);
		}
		MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.All);
		for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
		{
			BaseSimObject baseSimObject = MapBox.instance.temp_map_objects[i];
			if (baseSimObject.base_data.alive && !baseSimObject.currentTile.Type.ocean)
			{
				ActionLibrary.addBurningEffectOnTarget(baseSimObject, null);
			}
		}
		pTile.setFire(true);
		return true;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00007F30 File Offset: 0x00006130
	public static void action_growTornadoes(WorldTile pTile = null, string pDropID = null)
	{
		Tornado.growTornados(pTile);
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00007F38 File Offset: 0x00006138
	public static void action_shrinkTornadoes(WorldTile pTile = null, string pDropID = null)
	{
		Tornado.shrinkTornados(pTile);
	}
}
