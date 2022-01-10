using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class StackEffects : BaseMapObject
{
	// Token: 0x06000640 RID: 1600 RVA: 0x00049A80 File Offset: 0x00047C80
	private void Awake()
	{
		this.dictionary = new Dictionary<string, BaseEffectController>();
		this.list = new List<BaseEffectController>();
		this.loadEffect("fx_cast_ground_blue", true, 0.1f, "EffectsBack", 60);
		this.loadEffect("fx_cast_top_blue", true, 0.1f, "EffectsTop", 60);
		this.loadEffect("fx_cast_ground_red", true, 0.1f, "EffectsBack", 60);
		this.loadEffect("fx_cast_top_red", true, 0.1f, "EffectsTop", 60);
		this.loadEffect("fx_cast_ground_green", true, 0.1f, "EffectsBack", 60);
		this.loadEffect("fx_cast_top_green", true, 0.1f, "EffectsTop", 60);
		this.loadEffect("fx_create_skeleton", true, 0.1f, "EffectsTop", 0);
		this.loadEffect("fx_teleport_blue", true, 0.1f, "EffectsTop", 0);
		this.loadEffect("fx_teleport_red", true, 0.1f, "EffectsTop", 0);
		this.loadEffect("fx_shield_hit", true, 0.1f, "EffectsTop", 200);
		this.loadEffect("fx_bad_place", true, 0.1f, "EffectsBack", 10);
		this.loadEffect("fx_plasma_trail", true, 0.1f, "EffectsTop", 15);
		this.loadEffect("fx_enchanted_sparkle", true, 0.1f, "EffectsBack", 15);
		this.add(this.prefabFireSmoke, "fireSmoke", 0);
		this.add(this.prefabLightning, "lightning", 100);
		this.add(this.prefabSpawn, "spawn", 0);
		this.add(this.prefabSpawnBig, "spawn_big", 0);
		this.add(this.prefabExplosionSmall, "explosionSmall", 0);
		this.add(this.prefabExplosionNuke, "explosionNuke", 0);
		this.add(this.prefabExplosionWave, "explosionWave", 0);
		this.add(this.prefabFireworks1, "fireworks1", 0);
		this.add(this.prefabFireworks2, "fireworks2", 0);
		this.add(this.prefabNukeFlash, "nukeFlash", 0);
		this.add(this.prefabNapalmFlash, "napalmFlash", 0);
		this.add(this.prefabBoulderImpact, "boulderImpact", 0);
		this.add(this.prefabBoulderImpactWater, "boulderImpactWater", 0);
		this.add(this.prefabAntimatter, "antimatterEffect", 0);
		this.add(this.prefabEffectInfinityCoin, "infinityCoin", 0);
		this.add(this.prefabStatusParticle, "statusParticle", 10);
		this.add(this.prefabProjectile, "projectile", 0);
		this.add(this.prefabFireballExplosion, "fireballExplosion", 0);
		this.add(this.prefabSlashEffect, "slash", 20);
		this.add(this.prefabMiss, "miss", 20);
		this.add(this.prefabHit, "hit", 10);
		this.add(this.prefabHitCritical, "hitCritical", 10);
		this.add(this.boatExplosion, "boatExplosion", 20);
		this.add(this.fx_fishnet, "fx_fishnet", 20);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00049DAC File Offset: 0x00047FAC
	internal int countActive()
	{
		int num = 0;
		for (int i = 0; i < this.list.Count; i++)
		{
			BaseEffectController baseEffectController = this.list[i];
			num += baseEffectController.activeIndex;
		}
		return num;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00049DE8 File Offset: 0x00047FE8
	internal bool isLocked()
	{
		return this.get("spawn_big").activeIndex > 0;
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00049E00 File Offset: 0x00048000
	internal BaseEffectController get(string pID)
	{
		return this.dictionary[pID];
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00049E10 File Offset: 0x00048010
	private void loadEffect(string pName, bool pUseBasicPrefab = true, float pTimeBetweenFrames = 0.1f, string pSortingLayerName = "EffectsTop", int pLimit = 60)
	{
		string path;
		if (pUseBasicPrefab)
		{
			path = "effects/fx_basic";
		}
		else
		{
			path = "effects/" + pName;
		}
		GameObject gameObject = Object.Instantiate<GameObject>((GameObject)Resources.Load(path, typeof(GameObject)), base.transform);
		gameObject.transform.name = pName;
		gameObject.gameObject.SetActive(false);
		if (pUseBasicPrefab)
		{
			SpriteAnimation component = gameObject.GetComponent<SpriteAnimation>();
			component.timeBetweenFrames = pTimeBetweenFrames;
			component.frames = Resources.LoadAll<Sprite>("effects/" + pName + "_t");
			component.spriteRenderer.sortingLayerName = pSortingLayerName;
		}
		BaseEffectController baseEffectController = this.add(gameObject, pName, pLimit);
		baseEffectController.addNewObject(gameObject.GetComponent<BaseEffect>());
		baseEffectController.transform.SetParent(base.transform);
		baseEffectController.gameObject.SetActive(true);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00049ED8 File Offset: 0x000480D8
	private BaseEffectController add(GameObject pPrefab, string pID, int pLimit = 0)
	{
		BaseEffectController baseEffectController = Object.Instantiate<BaseEffectController>(this.prefabController);
		baseEffectController.create();
		baseEffectController.transform.parent = base.transform;
		baseEffectController.transform.name = pID;
		baseEffectController.prefab = pPrefab.transform;
		baseEffectController.objectLimit = pLimit;
		this.dictionary.Add(pID, baseEffectController);
		this.list.Add(baseEffectController);
		return baseEffectController;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00049F44 File Offset: 0x00048144
	internal void clear()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].clear();
		}
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x00049F78 File Offset: 0x00048178
	internal void spawnEffect()
	{
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00049F7C File Offset: 0x0004817C
	internal void Update()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].update(this.world.elapsed);
		}
		if (this.timeOutFireworks > 0f)
		{
			this.timeOutFireworks -= Time.deltaTime;
		}
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x00049FDA File Offset: 0x000481DA
	internal BaseEffect startSpawnEffect(WorldTile pTile, string pEffect = "spawn")
	{
		return this.get(pEffect).spawnAt(pTile, 0.25f);
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00049FEE File Offset: 0x000481EE
	internal SpawnEffect startBigSpawn(WorldTile pTile)
	{
		return this.get("spawn_big").spawnAt(pTile, 0.5f).GetComponent<SpawnEffect>();
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0004A00C File Offset: 0x0004820C
	internal void slash(Vector3 pVec, string pType, float pAngle)
	{
		if (this.world.qualityChanger.lowRes)
		{
			return;
		}
		BaseEffect baseEffect = this.get("slash").spawnAt(pVec, 0.098f);
		if (baseEffect == null)
		{
			return;
		}
		SpriteAnimation component = baseEffect.GetComponent<SpriteAnimation>();
		Sprite[] frames = Resources.LoadAll<Sprite>("effects/slashes/slash_" + pType);
		component.setFrames(frames);
		baseEffect.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, pAngle));
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0004A08C File Offset: 0x0004828C
	internal Projectile startProjectile(Vector3 pStart, Vector3 pTarget, string pAssetID, float pZ = 0f)
	{
		BaseEffect baseEffect = this.get("projectile").spawnNew();
		if (baseEffect == null)
		{
			return null;
		}
		Projectile component = baseEffect.GetComponent<Projectile>();
		component.start(pStart, pTarget, pAssetID, pZ);
		return component;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0004A0C6 File Offset: 0x000482C6
	internal void spawnAntimatterEffect(WorldTile pTile)
	{
		BaseEffect baseEffect = this.get("antimatterEffect").spawnNew();
		if (baseEffect == null)
		{
			return;
		}
		baseEffect.GetComponent<AntimatterBombEffect>().spawn(pTile);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0004A0E8 File Offset: 0x000482E8
	internal void spawnNukeFlash(WorldTile pTile, string pBomb)
	{
		BaseEffect baseEffect = this.get("nukeFlash").spawnNew();
		if (baseEffect == null)
		{
			return;
		}
		baseEffect.GetComponent<NukeFlash>().spawnFlash(pTile, pBomb);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0004A10B File Offset: 0x0004830B
	internal void spawnNapalmFlash(WorldTile pTile, string pBomb)
	{
		BaseEffect baseEffect = this.get("napalmFlash").spawnNew();
		if (baseEffect == null)
		{
			return;
		}
		baseEffect.GetComponent<NapalmFlash>().spawnFlash(pTile, pBomb);
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0004A12E File Offset: 0x0004832E
	internal void spawnStatusParticle(Vector3 pVec, Color pColor)
	{
		BaseEffect baseEffect = this.get("statusParticle").spawnNew();
		if (baseEffect == null)
		{
			return;
		}
		baseEffect.GetComponent<StatusParticle>().spawnParticle(pVec, pColor, 0.25f);
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0004A158 File Offset: 0x00048358
	internal void spawnFireworks(WorldTile pTile, float pTimeout = 0.05f)
	{
		if (this.timeOutFireworks > 0f)
		{
			return;
		}
		this.timeOutFireworks = pTimeout;
		BaseEffectController baseEffectController;
		if (Toolbox.randomBool())
		{
			baseEffectController = this.get("fireworks1");
		}
		else
		{
			baseEffectController = this.get("fireworks2");
		}
		BaseEffect baseEffect = baseEffectController.spawnAtRandomScale(pTile, 0.3f, 1f);
		baseEffect.sprRenderer.flipX = Toolbox.randomBool();
		Color color = default(Color);
		color.a = 1f;
		color.r = Toolbox.randomFloat(0f, 1f);
		color.b = Toolbox.randomFloat(0f, 1f);
		color.g = Toolbox.randomFloat(0f, 1f);
		baseEffect.sprRenderer.color = color;
		float z = Toolbox.randomFloat(-15f, 15f);
		baseEffect.transform.localEulerAngles = new Vector3(0f, 0f, z);
		Sfx.play("fireworks", true, -1f, -1f);
	}

	// Token: 0x04000821 RID: 2081
	public BaseEffectController prefabController;

	// Token: 0x04000822 RID: 2082
	public GameObject prefabFireballExplosion;

	// Token: 0x04000823 RID: 2083
	public GameObject prefabLightning;

	// Token: 0x04000824 RID: 2084
	public GameObject prefabSpawn;

	// Token: 0x04000825 RID: 2085
	public GameObject prefabSpawnBig;

	// Token: 0x04000826 RID: 2086
	public GameObject prefabExplosionSmall;

	// Token: 0x04000827 RID: 2087
	public GameObject prefabExplosionNuke;

	// Token: 0x04000828 RID: 2088
	public GameObject prefabExplosionCzarBomb;

	// Token: 0x04000829 RID: 2089
	public GameObject prefabExplosionWave;

	// Token: 0x0400082A RID: 2090
	public GameObject prefabFireworks1;

	// Token: 0x0400082B RID: 2091
	public GameObject prefabFireworks2;

	// Token: 0x0400082C RID: 2092
	public GameObject prefabNukeFlash;

	// Token: 0x0400082D RID: 2093
	public GameObject prefabBoulderImpact;

	// Token: 0x0400082E RID: 2094
	public GameObject prefabBoulderImpactWater;

	// Token: 0x0400082F RID: 2095
	public GameObject prefabAntimatter;

	// Token: 0x04000830 RID: 2096
	public GameObject prefabFireSmoke;

	// Token: 0x04000831 RID: 2097
	public GameObject prefabNapalmFlash;

	// Token: 0x04000832 RID: 2098
	public GameObject prefabEffectInfinityCoin;

	// Token: 0x04000833 RID: 2099
	public GameObject prefabStatusParticle;

	// Token: 0x04000834 RID: 2100
	public GameObject prefabSlashEffect;

	// Token: 0x04000835 RID: 2101
	public GameObject prefabProjectile;

	// Token: 0x04000836 RID: 2102
	public GameObject prefabMiss;

	// Token: 0x04000837 RID: 2103
	public GameObject prefabHit;

	// Token: 0x04000838 RID: 2104
	public GameObject prefabHitCritical;

	// Token: 0x04000839 RID: 2105
	public GameObject boatExplosion;

	// Token: 0x0400083A RID: 2106
	public GameObject fx_fishnet;

	// Token: 0x0400083B RID: 2107
	private Dictionary<string, BaseEffectController> dictionary;

	// Token: 0x0400083C RID: 2108
	internal List<BaseEffectController> list;

	// Token: 0x0400083D RID: 2109
	private float timeOutFireworks;
}
