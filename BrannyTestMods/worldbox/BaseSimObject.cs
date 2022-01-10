using System;
using System.Collections.Generic;
using pathfinding;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class BaseSimObject : BaseAnimatedObject
{
	// Token: 0x060002AA RID: 682 RVA: 0x0002D937 File Offset: 0x0002BB37
	internal override void create()
	{
		this.hash_id = BaseSimObject.LAST_HASH_ID++;
		base.create();
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0002D952 File Offset: 0x0002BB52
	internal new void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0002D960 File Offset: 0x0002BB60
	internal virtual void addStatusEffect(string pID, float pOverrideTimer = -1f)
	{
		if (!this.base_data.alive)
		{
			return;
		}
		if (this.isActor() && !this.a.stats.canHaveStatusEffect)
		{
			return;
		}
		if (this.activeStatus_dict != null && this.activeStatus_dict.ContainsKey(pID))
		{
			return;
		}
		StatusEffect statusEffect = AssetManager.status.get(pID);
		if (this.isActor() && statusEffect.oppositeTraits.Count > 0)
		{
			for (int i = 0; i < statusEffect.oppositeTraits.Count; i++)
			{
				string pTrait = statusEffect.oppositeTraits[i];
				if (this.a.haveTrait(pTrait))
				{
					return;
				}
			}
		}
		if (statusEffect.oppositeStatus.Count > 0)
		{
			for (int j = 0; j < statusEffect.oppositeStatus.Count; j++)
			{
				string text = statusEffect.oppositeStatus[j];
				if (this.activeStatus_dict != null && this.activeStatus_dict.ContainsKey(text))
				{
					return;
				}
			}
		}
		if (!string.IsNullOrEmpty(statusEffect.texture) && (statusEffect.spriteList == null || statusEffect.spriteList.Length == 0))
		{
			statusEffect.spriteList = Resources.LoadAll<Sprite>("effects/" + statusEffect.texture);
		}
		ActiveStatusEffect component = Object.Instantiate<GameObject>((GameObject)Resources.Load("effects/fx_status", typeof(GameObject)), base.transform).GetComponent<ActiveStatusEffect>();
		component.create();
		component.setStatus(statusEffect, this);
		if (pOverrideTimer != -1f)
		{
			component.timer = pOverrideTimer;
		}
		if (statusEffect.spriteList != null)
		{
			component.timeBetweenFrames = statusEffect.animation_speed + Toolbox.randomFloat(0f, statusEffect.animation_speed_random);
			component.setFrames(statusEffect.spriteList);
		}
		else
		{
			component.isOn = false;
		}
		if (statusEffect.random_frame)
		{
			component.setRandomFrame();
		}
		if (statusEffect.random_flip)
		{
			component.spriteRenderer.flipX = Toolbox.randomBool();
		}
		component.isOn = statusEffect.animated;
		component.simObject = this;
		component.transform.parent = base.transform;
		component.updateZFlip();
		this.setStatsDirty();
		if (this.activeStatusEffects == null)
		{
			this.activeStatusEffects = new List<ActiveStatusEffect>();
			this.activeStatus_dict = new Dictionary<string, ActiveStatusEffect>();
		}
		this.activeStatusEffects.Add(component);
		this.activeStatus_dict.Add(pID, component);
		if (this.isActor() && statusEffect.cancelActorJob)
		{
			this.a.cancelAllBeh(null);
		}
		if (this.isBuilding())
		{
			component.setZFlip(-0.01f);
		}
		if (statusEffect.removeStatus.Count > 0)
		{
			for (int k = 0; k < statusEffect.removeStatus.Count; k++)
			{
				string pID2 = statusEffect.removeStatus[k];
				this.removeStatusEffect(pID2, null, -1);
			}
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0002DC0D File Offset: 0x0002BE0D
	public virtual void setStatsDirty()
	{
		this.statsDirty = true;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0002DC18 File Offset: 0x0002BE18
	internal void removeAllStatusEffects()
	{
		if (this.activeStatusEffects == null)
		{
			return;
		}
		int count = this.activeStatusEffects.Count;
		while (count-- > 0)
		{
			ActiveStatusEffect activeStatusEffect = this.activeStatusEffects[count];
			this.removeStatusEffect(activeStatusEffect.asset.id, null, -1);
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0002DC64 File Offset: 0x0002BE64
	internal virtual void removeStatusEffect(string pID, ActiveStatusEffect pEffect = null, int pIndex = -1)
	{
		if (this.activeStatus_dict == null)
		{
			return;
		}
		if (!this.activeStatus_dict.ContainsKey(pID))
		{
			return;
		}
		this.setStatsDirty();
		if (pEffect == null)
		{
			pEffect = this.activeStatus_dict[pID];
		}
		if (pIndex == -1)
		{
			pIndex = this.activeStatusEffects.IndexOf(pEffect);
		}
		this.activeStatusEffects.RemoveAt(pIndex);
		this.activeStatus_dict.Remove(pID);
		if (this.activeStatusEffects.Count == 0)
		{
			this.activeStatusEffects = null;
			this.activeStatus_dict = null;
		}
		if (pEffect.gameObject != null)
		{
			Object.Destroy(pEffect.gameObject);
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0002DD05 File Offset: 0x0002BF05
	public virtual void setData(BaseObjectData pData)
	{
		this.base_data = pData;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0002DD0E File Offset: 0x0002BF0E
	internal bool isActor()
	{
		return this.objectType == MapObjectType.Actor;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0002DD19 File Offset: 0x0002BF19
	internal bool isBuilding()
	{
		return this.objectType == MapObjectType.Building;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0002DD24 File Offset: 0x0002BF24
	public void setObjectType(MapObjectType pType)
	{
		this.objectType = pType;
		if (this.objectType == MapObjectType.Actor)
		{
			this.a = (Actor)this;
			return;
		}
		this.b = (Building)this;
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0002DD4E File Offset: 0x0002BF4E
	internal bool haveStatus(string pID)
	{
		return this.activeStatus_dict != null && this.activeStatus_dict.ContainsKey(pID);
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0002DD6B File Offset: 0x0002BF6B
	internal bool hasAnyStatusEffect()
	{
		return this.activeStatusEffects != null && this.activeStatusEffects.Count > 0;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0002DD88 File Offset: 0x0002BF88
	internal void updateStatusEffects(float pElapsed)
	{
		if (this.activeStatusEffects == null)
		{
			return;
		}
		int num = 0;
		for (;;)
		{
			int num2 = num;
			List<ActiveStatusEffect> list = this.activeStatusEffects;
			int? num3 = (list != null) ? new int?(list.Count) : null;
			if (!(num2 < num3.GetValueOrDefault() & num3 != null))
			{
				break;
			}
			ActiveStatusEffect activeStatusEffect = this.activeStatusEffects[num];
			activeStatusEffect.update(pElapsed);
			if (activeStatusEffect.finished)
			{
				this.removeStatusEffect(activeStatusEffect.asset.id, activeStatusEffect, num);
			}
			else
			{
				num++;
			}
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0002DE0B File Offset: 0x0002C00B
	internal virtual void updateStats()
	{
		this.statsDirty = false;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0002DE14 File Offset: 0x0002C014
	internal bool isInLiquid()
	{
		return this.currentTile.Type.liquid;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0002DE26 File Offset: 0x0002C026
	internal virtual bool isInAir()
	{
		return false;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0002DE29 File Offset: 0x0002C029
	internal virtual float getZ()
	{
		return 0f;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0002DE30 File Offset: 0x0002C030
	internal virtual void getHit(float pDamage, bool pFlash = true, AttackType pType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true)
	{
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0002DE34 File Offset: 0x0002C034
	internal BaseSimObject findEnemyObjectTarget()
	{
		this._tempDist = 0f;
		this._bestDist = 0f;
		this._bestObject = null;
		if (Toolbox.temp_list_objects_enemies_chunk != this.currentTile.chunk || Toolbox.temp_list_objects_enemies_kingdom != this.kingdom)
		{
			Toolbox.temp_list_objects_enemies.Clear();
			Toolbox.temp_list_objects_enemies_chunk = this.currentTile.chunk;
			Toolbox.temp_list_objects_enemies_kingdom = this.kingdom;
			Toolbox.findEnemiesOfKingdomInChunk(this.currentTile.chunk, this.kingdom);
			for (int i = 0; i < this.currentTile.chunk.neighboursAll.Count; i++)
			{
				Toolbox.findEnemiesOfKingdomInChunk(this.currentTile.chunk.neighboursAll[i], this.kingdom);
			}
		}
		if (Toolbox.temp_list_objects_enemies.Count > 0)
		{
			for (int j = 0; j < Toolbox.temp_list_objects_enemies.Count; j++)
			{
				this.checkObjectList(Toolbox.temp_list_objects_enemies[j]);
				if (this._bestObject != null)
				{
					break;
				}
			}
		}
		if (this._bestObject == null)
		{
			return null;
		}
		return this._bestObject;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0002DF50 File Offset: 0x0002C150
	private void checkObjectList(List<BaseSimObject> pList)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			BaseSimObject baseSimObject = pList[i];
			if (!(baseSimObject == null) && !(baseSimObject == this) && baseSimObject.base_data.alive && this.canAttackTarget(baseSimObject) && (!this.isActor() || this.a.s_attackType != WeaponType.Melee || baseSimObject.currentTile.isSameIsland(this.currentTile) || (CachedRaycastIslands.getResult(this.currentTile, baseSimObject.currentTile, this.a.stats) != RaycastResult.NotPossible && !baseSimObject.currentTile.Type.block && this.currentTile.region.island.connectedWith(baseSimObject.currentTile.region.island))) && (!baseSimObject.isBuilding() || !this.kingdom.isCiv() || !baseSimObject.b.stats.cityBuilding || baseSimObject.b.stats.tower || baseSimObject.kingdom.race != this.kingdom.race) && !this.targetsToIgnore.Contains(baseSimObject))
			{
				this._tempDist = Toolbox.DistTile(baseSimObject.currentTile, this.currentTile);
				if (this._bestObject == null || this._tempDist < this._bestDist)
				{
					this._bestObject = baseSimObject;
					this._bestDist = this._tempDist;
				}
			}
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0002E0E4 File Offset: 0x0002C2E4
	internal bool canAttackTarget(BaseSimObject pTarget)
	{
		if (!pTarget.m_gameObject.activeSelf)
		{
			return false;
		}
		if (!pTarget.base_data.alive)
		{
			return false;
		}
		bool flag = this.isActor();
		string text;
		WeaponType weaponType;
		if (flag)
		{
			text = this.a.race.id;
			weaponType = this.a.s_attackType;
		}
		else
		{
			text = this.b.stats.race;
			weaponType = WeaponType.Range;
		}
		if (pTarget.isActor())
		{
			Actor actor = pTarget.a;
			if (!actor.stats.canBeKilledByStuff)
			{
				return false;
			}
			if (actor.ai.action != null && actor.ai.action.special_inside_object)
			{
				return false;
			}
			if (!actor.kingdom.asset.mad && !this.kingdom.asset.mad && !this.world.worldLaws.world_law_angry_civilians.boolVal)
			{
				if (actor.race.id == text && actor.professionAsset.is_civilian)
				{
					return false;
				}
				if (actor.race.id == text && flag && this.a.professionAsset.is_civilian)
				{
					return false;
				}
			}
			if (pTarget.a.isInAir() && weaponType == WeaponType.Melee)
			{
				return false;
			}
		}
		else
		{
			Building building = pTarget.b;
			if (this.kingdom.isCiv() && building.stats.cityBuilding && building.stats.tower && flag && this.a.professionAsset.is_civilian && !this.world.worldLaws.world_law_angry_civilians.boolVal && building.kingdom.race == this.kingdom.race)
			{
				return false;
			}
			if (flag && !this.a.stats.canAttackBuildings)
			{
				return this.a.stats.canAttackBrains && pTarget.kingdom != null && pTarget.kingdom.asset.brain;
			}
		}
		if (flag)
		{
			if (this.a.stats.oceanCreature && !this.a.stats.landCreature)
			{
				if (!pTarget.isInLiquid())
				{
					return false;
				}
				if (!pTarget.currentTile.isSameIsland(this.currentTile))
				{
					return false;
				}
			}
			else if (weaponType == WeaponType.Melee && pTarget.isInLiquid() && !this.a.stats.oceanCreature)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0002E34E File Offset: 0x0002C54E
	public override int GetHashCode()
	{
		return this.hash_id;
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0002E356 File Offset: 0x0002C556
	public override bool Equals(object obj)
	{
		return this.Equals(obj as BaseSimObject);
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0002E364 File Offset: 0x0002C564
	public bool Equals(BaseSimObject pObject)
	{
		return this.hash_id == pObject.hash_id;
	}

	// Token: 0x0400038B RID: 907
	internal HashSet<BaseMapObject> targetsToIgnore = new HashSet<BaseMapObject>();

	// Token: 0x0400038C RID: 908
	public bool object_destroyed;

	// Token: 0x0400038D RID: 909
	[NonSerialized]
	public Kingdom kingdom;

	// Token: 0x0400038E RID: 910
	[NonSerialized]
	public City city;

	// Token: 0x0400038F RID: 911
	internal bool statsDirty = true;

	// Token: 0x04000390 RID: 912
	internal bool event_full_heal;

	// Token: 0x04000391 RID: 913
	internal BaseStats curStats = new BaseStats();

	// Token: 0x04000392 RID: 914
	internal Actor a;

	// Token: 0x04000393 RID: 915
	internal Building b;

	// Token: 0x04000394 RID: 916
	public MapObjectType objectType;

	// Token: 0x04000395 RID: 917
	public BaseObjectData base_data;

	// Token: 0x04000396 RID: 918
	internal List<ActiveStatusEffect> activeStatusEffects;

	// Token: 0x04000397 RID: 919
	internal Dictionary<string, ActiveStatusEffect> activeStatus_dict;

	// Token: 0x04000398 RID: 920
	internal SpriteRenderer spriteRenderer;

	// Token: 0x04000399 RID: 921
	internal float _bestDist;

	// Token: 0x0400039A RID: 922
	internal float _tempDist;

	// Token: 0x0400039B RID: 923
	internal BaseSimObject _bestObject;

	// Token: 0x0400039C RID: 924
	internal Building _bestBuilding;

	// Token: 0x0400039D RID: 925
	public static int LAST_HASH_ID;

	// Token: 0x0400039E RID: 926
	public int hash_id;
}
