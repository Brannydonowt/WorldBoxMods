using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class Actor : ActorBase
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000680 RID: 1664 RVA: 0x0004B3D8 File Offset: 0x000495D8
	public string coloredName
	{
		get
		{
			if (this.data == null)
			{
				return "";
			}
			Kingdom kingdom = this.kingdom;
			bool flag;
			if (kingdom == null)
			{
				flag = false;
			}
			else
			{
				KingdomColor kingdomColor = kingdom.kingdomColor;
				Color? color = (kingdomColor != null) ? new Color?(kingdomColor.colorBorderOut) : null;
				flag = (color != null);
			}
			if (flag)
			{
				return string.Concat(new string[]
				{
					"<color=",
					Toolbox.colorToHex(this.kingdom.kingdomColor.colorBorderOut, true),
					">",
					this.data.firstName,
					"</color>"
				});
			}
			return this.data.firstName;
		}
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0004B488 File Offset: 0x00049688
	private void updateChildren(List<BaseActorComponent> pList, float pElapsed)
	{
		if (pList == null)
		{
			return;
		}
		for (int i = 0; i < pList.Count; i++)
		{
			pList[i].update(pElapsed);
		}
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0004B4B7 File Offset: 0x000496B7
	public void loadStats(ActorStats pStats)
	{
		this.stats = pStats;
		this.setStatsDirty();
		if (this.stats.use_items)
		{
			this.equipment = new ActorEquipment();
		}
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0004B4DE File Offset: 0x000496DE
	public void setHomeBuilding(Building pBuilding)
	{
		this.homeBuilding = pBuilding;
		if (this.homeBuilding == null)
		{
			this.data.homeBuildingID = string.Empty;
			return;
		}
		this.data.homeBuildingID = pBuilding.data.objectID;
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0004B51C File Offset: 0x0004971C
	internal override void create()
	{
		if (this.created)
		{
			return;
		}
		base.create();
		base.setObjectType(MapObjectType.Actor);
		this.stats.currentAmount++;
		if (this.new_creature)
		{
			if (this.stats.unit)
			{
				if (this.stats.baby)
				{
					this.setProfession(UnitProfession.Baby);
				}
				else
				{
					this.setProfession(UnitProfession.Unit);
				}
			}
			else
			{
				this.setProfession(UnitProfession.Null);
			}
		}
		this.race = AssetManager.raceLibrary.get(this.stats.race);
		this.race.units.Add(this);
		base.loadTexture();
		if (this.race.id != "nature")
		{
			this.timerReproduction = new WorldTimer(10f, true);
			this.growTimer = new WorldTimer(0.05f, new Action(this.updateGrowthScale));
		}
		if (this.stats.hovering)
		{
			this.moveJumpOffset.y = this.stats.hovering_min;
		}
		this.addChildren();
		if (this.stats.kingdom.Contains("ants"))
		{
			AchievementLibrary.achievementAntWorld.check();
		}
		this.updateStats();
		base.updateTargetScale();
		this.startBabymakingTimeout();
		this.callbacks_landed.Add(new BaseActionActor(base.cancelAllBeh));
		this.callbacks_landed.Add(new BaseActionActor(this.checkDeathOutsideMap));
		this.callbacks_added_madness.Add(new BaseActionActor(this.applyMadness));
		this.callbacks_on_death.Add(new BaseActionActor(ActorBase.clearStatusEffects));
		this._positionDirty = true;
		this.updatePos();
		this.m_transform.name = this.stats.id + "_" + this.data.actorID;
		this.forceAnimation();
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0004B6FE File Offset: 0x000498FE
	private void checkDeathOutsideMap(Actor pActor)
	{
		if (!base.inMapBorder())
		{
			this.killHimself(false, AttackType.Other, true, true);
		}
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0004B714 File Offset: 0x00049914
	internal void loadFromSave()
	{
		base.checkMadness();
		this.setStatsDirty();
		int i = 0;
		while (i < this.data.traits.Count)
		{
			if (AssetManager.traits.get(this.data.traits[i]) == null)
			{
				this.data.traits.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
		if (this.stats.unit && this.data.profession == UnitProfession.Null)
		{
			this.data.profession = UnitProfession.Unit;
		}
		this.setProfession(this.data.profession);
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0004B7B0 File Offset: 0x000499B0
	internal void setProfession(UnitProfession pType)
	{
		this.data.profession = pType;
		this.professionAsset = AssetManager.professions.get(pType);
		this.setStatsDirty();
		if (this.city != null)
		{
			this.city.unitsDirty = true;
		}
		base.cancelAllBeh(null);
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0004B801 File Offset: 0x00049A01
	internal bool isProfession(UnitProfession pType)
	{
		return this.data.profession == pType;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0004B814 File Offset: 0x00049A14
	public void updateHunger()
	{
		this.data.hunger--;
		if (this.data.hunger <= 10 && this.city != null)
		{
			ResourceAsset foodItem = this.city.getFoodItem(this.data.favoriteFood);
			if (foodItem != null)
			{
				this.consumeCityFoodItem(foodItem);
				if (base.haveTrait("gluttonous"))
				{
					foodItem = this.city.getFoodItem(this.data.favoriteFood);
					if (foodItem != null)
					{
						this.consumeCityFoodItem(foodItem);
					}
				}
			}
		}
		if (this.data.hunger <= 0)
		{
			this.data.hunger = 0;
			if (this.data.hunger == 0)
			{
				this.getHit(1f, true, AttackType.Hunger, null, true);
			}
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0004B8D8 File Offset: 0x00049AD8
	public void consumeCityFoodItem(ResourceAsset pAsset)
	{
		this.city.eatFoodItem(pAsset.id);
		this.restoreStatsFromEating(pAsset.restoreHunger, pAsset.restoreHealth, false);
		if (pAsset.id == this.data.favoriteFood)
		{
			this.restoreStatsFromEating(pAsset.restoreHunger / 2, pAsset.restoreHealth / 2f, false);
		}
		if (string.IsNullOrEmpty(this.data.favoriteFood))
		{
			return;
		}
		if (pAsset.id == this.data.favoriteFood && this.data.mood != "happy")
		{
			this.changeMood("happy");
			return;
		}
		if (this.data.mood == "happy")
		{
			this.changeMood("normal");
			return;
		}
		if (this.data.mood == "normal")
		{
			if (Toolbox.randomChance(0.2f))
			{
				this.changeMood("sad");
				return;
			}
		}
		else if (this.data.mood == "sad" && Toolbox.randomChance(0.05f))
		{
			this.changeMood("angry");
		}
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0004BA0A File Offset: 0x00049C0A
	private void changeMood(string pMood)
	{
		if (pMood == this.data.mood)
		{
			return;
		}
		this.setStatsDirty();
		this.data.mood = pMood;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0004BA34 File Offset: 0x00049C34
	public void restoreStatsFromEating(int pVal = 100, float pRestoreHealthMod = 0.1f, bool pSetMaxHunger = false)
	{
		this.data.hunger += pVal;
		if (pSetMaxHunger && this.data.hunger > this.stats.maxHunger)
		{
			this.data.hunger = this.stats.maxHunger;
		}
		if (this.data.health < this.curStats.health)
		{
			this.restoreHealth((int)((float)this.curStats.health * pRestoreHealthMod + 1f));
		}
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0004BAB8 File Offset: 0x00049CB8
	public void restoreHealth(int pVal)
	{
		if (this.data.health == this.curStats.health)
		{
			return;
		}
		this.data.health += pVal;
		if (this.data.health > this.curStats.health)
		{
			this.data.health = this.curStats.health;
		}
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0004BB1F File Offset: 0x00049D1F
	internal void reloadInventory()
	{
		this.setStatsDirty();
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0004BB27 File Offset: 0x00049D27
	internal bool isRace(string pID)
	{
		return this.stats.race == pID;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0004BB3A File Offset: 0x00049D3A
	internal bool isRace(Actor pActor)
	{
		return this.stats.race == pActor.stats.race;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0004BB58 File Offset: 0x00049D58
	private void addChildren()
	{
		this.children_pre_behaviour = new List<BaseActorComponent>();
		this.children_behaviour = new List<BaseActorComponent>();
		this.children_special = new List<BaseActorComponent>();
		if (base.GetComponent<GodFinger>() != null)
		{
			this.addChild(base.GetComponent<GodFinger>(), this.children_special);
		}
		if (base.GetComponent<Dragon>() != null)
		{
			this.addChild(base.GetComponent<Dragon>(), this.children_special);
		}
		if (base.GetComponent<UFO>() != null)
		{
			this.addChild(base.GetComponent<UFO>(), this.children_special);
		}
		if (base.GetComponent<Tornado>() != null)
		{
			this.addChild(base.GetComponent<Tornado>(), this.children_special);
		}
		if (this.stats.flags.Contains("ant"))
		{
			base.gameObject.AddComponent<Ant>();
			this.addChild(base.GetComponent<Ant>(), this.children_behaviour);
		}
		if (this.stats.flags.Contains("egg"))
		{
			base.gameObject.AddComponent<Egg>();
			this.addChild(base.GetComponent<Egg>(), this.children_pre_behaviour);
		}
		if (this.stats.baby)
		{
			base.gameObject.AddComponent<Baby>();
			this.addChild(base.gameObject.AddComponent<Baby>(), this.children_pre_behaviour);
		}
		if (this.stats.hovering)
		{
			this.addChild(this._hoverModule = base.gameObject.AddComponent<HoverModule>(), this.children_special);
		}
		if (this.stats.id == "bee")
		{
			this.addChild(base.gameObject.AddComponent<Bee>(), this.children_pre_behaviour);
		}
		if (this.stats.isBoat)
		{
			this.addChild(base.gameObject.AddComponent<Boat>(), this.children_pre_behaviour);
		}
		if (this.children_pre_behaviour.Count == 0)
		{
			this.children_pre_behaviour = null;
		}
		if (this.children_behaviour.Count == 0)
		{
			this.children_behaviour = null;
		}
		if (this.children_special.Count == 0)
		{
			this.children_special = null;
		}
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0004BD5B File Offset: 0x00049F5B
	private void addChild(BaseActorComponent pObject, List<BaseActorComponent> pList)
	{
		if (pList != null)
		{
			pList.Add(pObject);
		}
		pObject.create();
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0004BD70 File Offset: 0x00049F70
	private void updateSpecialTraitEffects()
	{
		if (this.s_special_effect_traits == null)
		{
			return;
		}
		Actor._tempTraitList.Clear();
		Actor._tempTraitList.AddRange(this.s_special_effect_traits);
		for (int i = 0; i < Actor._tempTraitList.Count; i++)
		{
			Actor._tempTraitList[i].action_special_effect(this, this.currentTile);
		}
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0004BDD4 File Offset: 0x00049FD4
	public void spawnParticle(Color pColor)
	{
		if (Toolbox.randomBool())
		{
			return;
		}
		if (this.world.qualityChanger.lowRes)
		{
			return;
		}
		Vector3 position = this.m_transform.position;
		position.y += this.spriteRenderer.sprite.bounds.size.y * this.m_transform.localScale.y / 2f;
		position.x += Toolbox.randomFloat(-0.2f, 0.2f);
		position.y += Toolbox.randomFloat(-0.2f, 0.2f);
		this.world.stackEffects.spawnStatusParticle(position, pColor);
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0004BE8D File Offset: 0x0004A08D
	public void makeWait(float pValue = 10f)
	{
		base.stopMovement();
		this.timer_action = pValue;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0004BE9C File Offset: 0x0004A09C
	private void playRandomSound()
	{
		if (this.stats.playRandomSound)
		{
			Sfx.play(this.stats.playRandomSound_id, true, this.currentPosition.x, this.currentPosition.y);
		}
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0004BED2 File Offset: 0x0004A0D2
	public void startShake(float pTimer = 0.3f, float pVol = 0.1f, bool pHorizontal = true, bool pVertical = true)
	{
		this.shakeHorizontal = pHorizontal;
		this.shakeVertical = pVertical;
		this.shakeTimer.startTimer(pTimer);
		this.shakeVolume = pVol;
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0004BEF8 File Offset: 0x0004A0F8
	private void updateSpecialTimers(float pElapsed)
	{
		if (this.s_special_effect_traits != null)
		{
			if (this.specialTimer > 0f)
			{
				this.specialTimer -= pElapsed;
			}
			else
			{
				this.specialTimer = 1f;
				this.updateSpecialTraitEffects();
			}
		}
		if (this._timerSound > 0f)
		{
			this._timerSound -= pElapsed;
			return;
		}
		this._timerSound = 2f;
		this.playRandomSound();
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0004BF68 File Offset: 0x0004A168
	private bool canFight()
	{
		BehaviourTaskActor task = this.ai.task;
		return (task == null || !task.ignoreFightCheck) && !this.stats.skipFightLogic && !this._trait_peaceful;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0004BFA0 File Offset: 0x0004A1A0
	protected virtual void updateBehaviour(float pElapsed)
	{
		if (this.forceVector.x != 0f || this.forceVector.y != 0f || this.forceVector.z != 0f)
		{
			return;
		}
		if (this.zPosition.y != 0f && this.stats.updateZ)
		{
			return;
		}
		if (this.currentTile == null)
		{
			return;
		}
		if (this.behaviourActorTargetCheck())
		{
			return;
		}
		if (this.checkEnemyTargets())
		{
			base.stopMovement();
			return;
		}
		if (this.is_moving)
		{
			return;
		}
		if (base.isUsingPath())
		{
			this.updatePathMovement();
			return;
		}
		this.updateChildren(this.children_behaviour, pElapsed);
		this.ai.update();
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0004C058 File Offset: 0x0004A258
	private bool behaviourActorTargetCheck()
	{
		if (this.attackTarget != null)
		{
			if (!this.attackTarget.base_data.alive || (this.kingdom != null && this.kingdom.isCiv() && !this.attackTarget.kingdom.isEnemy(this.kingdom)))
			{
				this.attackTarget = null;
			}
			else
			{
				if (this.kingdom != null && this.kingdom.isCiv())
				{
					this.kingdom.isEnemy(this.attackTarget.kingdom);
				}
				int num = 1;
				if (num == 0)
				{
					this.attackTarget = null;
				}
				if (num != 0 && base.canAttackTarget(this.attackTarget))
				{
					if (this.attackTimer > 0f || (this.attackTimer > 0f && this.isInAttackRange(this.attackTarget)))
					{
						base.stopMovement();
						return true;
					}
					if (this.tryToAttack(this.attackTarget))
					{
						base.stopMovement();
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0004C150 File Offset: 0x0004A350
	private bool checkEnemyTargets()
	{
		if (!this.canFight())
		{
			return false;
		}
		if (this.attackTarget != null)
		{
			if (this.ai.task == null || !this.ai.task.fighting)
			{
				this.ai.setTask("fight", true, true);
			}
			return false;
		}
		if (this._timeout_targets > 0f)
		{
			return false;
		}
		this._timeout_targets = 0.4f + Toolbox.randomFloat(0f, 0.1f);
		if (this.attackTarget == null || !this.attackTarget.base_data.alive || this.targetsToIgnore.Contains(this.attackTarget) || !base.canAttackTarget(this.attackTarget))
		{
			this.attackTarget = null;
		}
		if (base.isAffectedByLiquid() && !this.stats.oceanCreature)
		{
			return false;
		}
		if (this.kingdom == null)
		{
			this.checkKingdom();
		}
		BaseSimObject baseSimObject = base.findEnemyObjectTarget();
		if (baseSimObject != null)
		{
			if (this.attackTarget == null)
			{
				this.attackTarget = baseSimObject;
			}
			else
			{
				float num = Toolbox.DistTile(this.attackTarget.currentTile, this.currentTile);
				if (Toolbox.DistTile(baseSimObject.currentTile, this.currentTile) < num)
				{
					this.attackTarget = baseSimObject;
					this._timeout_targets = 1f;
				}
			}
			this.ai.setTask("fight", false, true);
			return true;
		}
		return false;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0004C2B8 File Offset: 0x0004A4B8
	public void updatePos()
	{
		if (!this._positionDirty)
		{
			return;
		}
		this._positionDirty = false;
		float num = this.currentPosition.x + this.moveJumpOffset.x + this.shakeOffset.x;
		float num2 = this.currentPosition.y + this.moveJumpOffset.y + this.shakeOffset.y;
		float num3 = num2;
		num2 += this.zPosition.y;
		this.curShadowPosition.x = this.currentPosition.x + this.shakeOffset.x;
		this.curShadowPosition.y = this.currentPosition.y + this.shakeOffset.y;
		if (base.isAffectedByLiquid())
		{
			num3 += 0.3f;
		}
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		this.curTransformPosition.x = num;
		this.curTransformPosition.y = num2;
		this.curTransformPosition.z = num3;
		if (this.lastTransformPosition.x != num || this.lastTransformPosition.y != num2)
		{
			this.m_transform.localPosition = this.curTransformPosition;
			this.lastTransformPosition.Set(num, num2, num3);
		}
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0004C3F0 File Offset: 0x0004A5F0
	internal void updateAge()
	{
		if (!this.data.updateAge(this.race) && !base.haveTrait("immortal"))
		{
			this.killHimself(false, AttackType.Age, true, true);
			return;
		}
		if (this.city != null)
		{
			if (this.city.kingdom.king == this)
			{
				this.addExperience(20);
			}
			if (this.city.leader == this)
			{
				this.addExperience(10);
			}
		}
		if (this.data.age > 40 && Toolbox.randomChance(0.3f))
		{
			base.addTrait("wise");
		}
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0004C498 File Offset: 0x0004A698
	private void checkCulture()
	{
		if (this.city == null)
		{
			return;
		}
		if (this.world.cultures.get(this.data.culture) != null)
		{
			return;
		}
		this.data.culture = string.Empty;
		if (this.currentTile.zone.city != this.city)
		{
			return;
		}
		if (this.currentTile.zone.culture == null)
		{
			return;
		}
		this.setCulture(this.currentTile.zone.culture);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0004C52B File Offset: 0x0004A72B
	private void setCulture(Culture pCulture)
	{
		this.data.culture = pCulture.id;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0004C540 File Offset: 0x0004A740
	private void checkKingdom()
	{
		if (this.kingdom != null)
		{
			return;
		}
		base.checkMadness();
		if (this.stats.unit)
		{
			base.setKingdom(this.world.kingdoms.dict_hidden["nomads_" + this.stats.race]);
			return;
		}
		if (this.stats.kingdom != "")
		{
			base.setKingdom(this.world.kingdoms.dict_hidden[this.stats.kingdom]);
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0004C5D7 File Offset: 0x0004A7D7
	public bool isInsideSomething()
	{
		return this.ai.action != null && this.ai.action.special_inside_object;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0004C5FC File Offset: 0x0004A7FC
	public bool isItemRendered()
	{
		return this.stats.use_items && !this._isInLiquid && (!this.equipment.weapon.isEmpty() || (this.ai.task != null && this.ai.task.force_item_sprite != string.Empty) || this.inventory.resource != string.Empty);
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0004C67C File Offset: 0x0004A87C
	private string getRenderedToolOrItem()
	{
		if (this.inventory.resource != string.Empty)
		{
			return this.inventory.resource;
		}
		if (this.ai.task != null && this.ai.task.force_item_sprite != string.Empty)
		{
			return this.ai.task.force_item_sprite;
		}
		return string.Empty;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0004C6EC File Offset: 0x0004A8EC
	public string getTextureToRenderInHand()
	{
		string renderedToolOrItem = this.getRenderedToolOrItem();
		if (!string.IsNullOrEmpty(renderedToolOrItem))
		{
			return renderedToolOrItem;
		}
		return this.s_weapon_texture;
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0004C710 File Offset: 0x0004A910
	public void updateTimers(float pElapsed)
	{
		if (this.zPosition.y != 0f && this.stats.updateZ)
		{
			this.updateFall(pElapsed);
		}
		if (this.attackedBy != null && !this.attackedBy.base_data.alive)
		{
			this.attackedBy = null;
		}
		if (this._is_visible && this._activeSelf && !this.world.qualityChanger.lowRes)
		{
			if (this.stats.shadow)
			{
				this.world.spriteSystemUnitShadows.add(this);
			}
			if (this.data.alive)
			{
				if (this.data.favorite && !this.stats.hideFavoriteIcon)
				{
					this.world.spriteSystemFavorites.add(this);
				}
				if (this.isGroupLeader)
				{
					this.world.spriteSystemBanners.add(this);
				}
			}
		}
		if (this.insideBoat != null)
		{
			return;
		}
		this.shakeTimer.update(pElapsed);
		base.updateFlipRotation(pElapsed);
		this.updateForce(pElapsed);
		if (this.world._isPaused)
		{
			return;
		}
		base.updateShake();
		if (!this.data.alive)
		{
			return;
		}
		this.checkCulture();
		base.updateRotationBack(pElapsed);
		if (this._status_frozen)
		{
			return;
		}
		base.updateWalkJump(this.world.deltaTime);
		if (this.attackTimer >= 0f)
		{
			this.attackTimer -= pElapsed;
		}
		if (this._timeout_targets >= 0f)
		{
			this._timeout_targets -= this.world.deltaTime;
		}
		if (this.timer_action >= 0f)
		{
			this.timer_action -= pElapsed;
		}
		if (this.canFight())
		{
			this.targetsToIgnoreTimer.update(pElapsed);
		}
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0004C8E4 File Offset: 0x0004AAE4
	internal bool checkSpriteConstructor()
	{
		if (!this.stats.sprite_group_renderer)
		{
			return true;
		}
		if (this.s_body_sprite == null)
		{
			return false;
		}
		if (!this.data.alive)
		{
			return false;
		}
		Sprite spriteUnit = UnitSpriteConstructor.getSpriteUnit(this.frameData, this, this.s_item_sprite, this.kingdom.kingdomColor, this.race, this.data.skin_set, this.data.skin, this.stats.texture_atlas);
		if (spriteUnit == null)
		{
			return false;
		}
		this.spriteRenderer.sprite = spriteUnit;
		return true;
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0004C97C File Offset: 0x0004AB7C
	public void forceAnimation()
	{
		base.updateAnimation(this.world.elapsed, true);
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0004C990 File Offset: 0x0004AB90
	private void checkSpriteRenderer()
	{
		if (!this.stats.haveSpriteRenderer)
		{
			return;
		}
		if (!this.stats.hideOnMinimap)
		{
			return;
		}
		if (!this.world.qualityChanger.lowRes)
		{
			this.spriteRenderer.enabled = true;
			if (this.item_sprite_dirty)
			{
				this.item_sprite_dirty = false;
				this.s_item_sprite = null;
				if (this.isItemRendered())
				{
					this.s_item_sprite = ActorAnimationLoader.getItem(this.getTextureToRenderInHand());
				}
			}
			if (this.s_item_sprite != null && this._is_visible)
			{
				this.world.spriteSystemItems.add(this);
			}
			return;
		}
		if (!this.spriteRenderer.enabled)
		{
			return;
		}
		this.spriteRenderer.enabled = false;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0004CA48 File Offset: 0x0004AC48
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._actionFall)
		{
			this._actionFall = false;
			this.actionLanded();
		}
		this.checkExit();
		if (this.insideBoat != null)
		{
			base.setCurrentTilePosition(this.insideBoat.actor.currentTile);
			return;
		}
		this.checkKingdom();
		this.updateColorEffect(pElapsed);
		this.updatePos();
		this.updateChildren(this.children_special, pElapsed);
		base.updateRotation();
		this.updateChildren(this.children_pre_behaviour, pElapsed);
		this.checkSpriteRenderer();
		if (this._is_visible && this._activeSelf && !this.world.qualityChanger.lowRes)
		{
			this.checkSpriteConstructor();
		}
		if (!this.data.alive)
		{
			base.updateDeadAnimation();
			return;
		}
		if (!this.checkCurTileAction())
		{
			return;
		}
		if (this.world.isPaused())
		{
			return;
		}
		base.updateStatusEffects(pElapsed);
		if (this._status_frozen)
		{
			return;
		}
		this.checkUpdateTimers(pElapsed);
		if (this.timer_action >= 0f)
		{
			return;
		}
		if (!this.data.alive)
		{
			return;
		}
		this.updateBehaviour(pElapsed);
		if (this.nextStepPosition.x != Globals.emptyVector.x)
		{
			base.SmoothMovement(this.nextStepPosition, pElapsed);
		}
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0004CB8C File Offset: 0x0004AD8C
	private void checkUpdateTimers(float pElapsed)
	{
		if (!this.race.nature)
		{
			if (!this.stats.specialAnimation && this.actorScale != this.targetScale)
			{
				this.growTimer.update(pElapsed);
			}
			this.timerReproduction.update(pElapsed);
			this.updateSpecialTimers(pElapsed);
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0004CBE0 File Offset: 0x0004ADE0
	private void checkExit()
	{
		if (this._activeSelf)
		{
			return;
		}
		if (this.ai.action != null && this.ai.action.special_inside_object)
		{
			if ((this.insideBuilding == null || this.insideBuilding.isNonUsable()) && this.insideBoat == null)
			{
				this.exitHouse();
				base.cancelAllBeh(null);
				return;
			}
		}
		else
		{
			this.exitHouse();
			base.cancelAllBeh(null);
		}
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0004CC5C File Offset: 0x0004AE5C
	private bool checkCurTileAction()
	{
		if (this.zPosition.y != 0f)
		{
			return true;
		}
		TileTypeBase type = this.currentTile.Type;
		if (this.stats.canBeKilledByStuff)
		{
			AchievementLibrary.achievementPiranhaLand.check_actor(this);
			if (this.world.isPaused())
			{
				return false;
			}
			if (type.liquid && !base.isFlying())
			{
				if (!type.lava)
				{
					this.removeStatusEffect("burning", null, -1);
				}
				if (this.stats.damagedByOcean && this.currentTile.Type.ocean && this.colorEffect <= 0f && !this.shakeTimer.isActive)
				{
					this.getHit(5f, true, AttackType.Other, null, true);
				}
				if (this.currentTile.Type.liquid && this.stats.swimToIsland && !this.is_moving)
				{
					bool flag = false;
					if (this.currentTile.Type.ocean && !this.stats.oceanCreature)
					{
						flag = true;
					}
					else if (this.currentTile.Type.swamp && !this.stats.swampCreature)
					{
						flag = true;
					}
					if (flag && (this.ai.task == null || !this.ai.task.swim_to_island))
					{
						this.ai.setTask("swim_to_island", true, true);
					}
				}
			}
			else if (type.block)
			{
				if (this.stats.moveFromBlock && !this.is_moving && (this.ai.task == null || !this.ai.task.moveFromBlock))
				{
					this.ai.setTask("move_from_block", true, true);
				}
				if (this.stats.dieOnBlocks && this.colorEffect <= 0f && !this.shakeTimer.isActive)
				{
					this.getHit(1f, true, AttackType.Other, null, true);
				}
			}
			else if (type.ground && !base.isFlying())
			{
				if (type.damagedWhenWalked && !this._trait_weightless)
				{
					if (this.currentTile.health > 0)
					{
						this.currentTile.health--;
					}
					else
					{
						MapAction.decreaseTile(this.currentTile, "flash");
					}
				}
				if (this.currentTile.data.fire && !this._trait_fire_resistant)
				{
					ActionLibrary.addBurningEffectOnTarget(this, null);
					if (!this.stats.specialAnimation)
					{
						this.spriteAnimation.stopAt(2, false);
					}
					if (!this.data.alive)
					{
						return false;
					}
				}
			}
			if (type.damageUnits && !base.isFlying() && this.colorEffect <= 0f && (!type.lava || this.stats.dieInLava))
			{
				this.getHit((float)type.damage, true, AttackType.Other, null, true);
				if (!this.stats.specialAnimation)
				{
					this.spriteAnimation.stopAt(2, false);
				}
				if (!this.data.alive)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0004CF88 File Offset: 0x0004B188
	public void killHimself(bool pDestroy = false, AttackType pType = AttackType.Other, bool pCountDeath = true, bool pLaunchCallbacks = true)
	{
		if (!this.data.alive && !pDestroy)
		{
			return;
		}
		if (Config.controllableUnit == this)
		{
			Config.setControllableCreature(null);
			this.world.selectedButtons.unselectAll();
		}
		if (this.data.alive)
		{
			if (this.stats.id == "tornado")
			{
				base.GetComponent<Tornado>().killTornado();
			}
			this.data.alive = false;
			if (this.insideBoat != null)
			{
				this.insideBoat.removeFromTaxi(this);
			}
			if (this.stats.id == "crabzilla")
			{
				pDestroy = true;
			}
			if (pLaunchCallbacks)
			{
				if (this.currentTile.Type.unitDeathAction != null)
				{
					this.currentTile.Type.unitDeathAction(this, this.currentTile);
				}
				if (this.stats.action_death != null)
				{
					this.stats.action_death(this, this.currentTile);
				}
				if (base.gameObject == null)
				{
					return;
				}
				int i = 0;
				while (i < this.data.traits.Count)
				{
					string text = this.data.traits[i];
					ActorTrait actorTrait = AssetManager.traits.get(text);
					if (actorTrait != null)
					{
						if (actorTrait.action_death != null)
						{
							actorTrait.action_death(this, this.currentTile);
						}
						if (this.data.traits.Contains(text))
						{
							i++;
						}
					}
				}
				if (base.gameObject == null)
				{
					pDestroy = true;
				}
				for (int j = 0; j < this.callbacks_on_death.Count; j++)
				{
					this.callbacks_on_death[j](this);
				}
			}
			if (base.haveTrait("energized") && pType != AttackType.None)
			{
				MapBox.spawnLightning(this.currentTile, 0.25f);
			}
			if (pCountDeath)
			{
				this.world.gameStats.data.creaturesDied++;
				this.world.mapStats.deaths++;
				switch (pType)
				{
				case AttackType.Plague:
					this.world.mapStats.deaths_plague++;
					goto IL_2D1;
				case AttackType.Other:
					this.world.mapStats.deaths_other++;
					goto IL_2D1;
				case AttackType.Hunger:
					this.world.mapStats.deaths_hunger++;
					goto IL_2D1;
				case AttackType.Eaten:
					this.world.mapStats.deaths_eaten++;
					goto IL_2D1;
				case AttackType.Age:
					this.world.mapStats.deaths_age++;
					goto IL_2D1;
				}
				this.world.mapStats.deaths_other++;
			}
			IL_2D1:
			this.stats.currentAmount--;
			if (this.data.favorite && pType != AttackType.GrowUp)
			{
				BaseSimObject attackedBy = this.attackedBy;
				if (((attackedBy != null) ? attackedBy.a : null) == null)
				{
					WorldLog.logFavDead(this);
				}
				else
				{
					BaseSimObject attackedBy2 = this.attackedBy;
					WorldLog.logFavMurder(this, (attackedBy2 != null) ? attackedBy2.a : null);
				}
			}
			this.race.units.Remove(this);
			if (this.spriteRenderer != null && !this.stats.specialAnimation)
			{
				this.spriteRenderer.sortingOrder = -1;
			}
			this.clearTasks();
			base.stopMovement();
			if (!this.stats.specialAnimation && this.spriteAnimation != null)
			{
				this.spriteAnimation.stopAnimations();
			}
		}
		if (this.city != null)
		{
			this.city.removeCitizen(this, pCountDeath, pType);
			this.setCity(null);
		}
		if (pDestroy)
		{
			this.race.units.Remove(this);
			if (this.kingdom != null)
			{
				base.setKingdom(null);
			}
			this.world.destroyActor(this);
		}
		this.attackTarget = null;
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0004D388 File Offset: 0x0004B588
	internal bool isInAttackRange(BaseSimObject pObject)
	{
		return Toolbox.DistVec3(this.currentPosition, pObject.currentPosition) < this.curStats.range + pObject.curStats.size;
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0004D3C0 File Offset: 0x0004B5C0
	internal bool tryToAttack(BaseSimObject pTarget)
	{
		if (base.isInLiquid() && !this.stats.oceanCreature)
		{
			return false;
		}
		if (this.attackTimer > 0f)
		{
			return false;
		}
		WorldTile currentTile = pTarget.currentTile;
		if (!this.isInAttackRange(pTarget))
		{
			return false;
		}
		if (this.s_attackType == WeaponType.Melee && pTarget.zPosition.y > 0f)
		{
			return false;
		}
		this.timer_action = this.s_attackSpeed_seconds;
		this.attackTimer = this.s_attackSpeed_seconds;
		this.punchTargetAnimation(pTarget.currentPosition, pTarget.currentTile, true, this.s_attackType == WeaponType.Range, 40f);
		Sfx.play("punch", true, this.currentPosition.x, this.currentPosition.y);
		Vector3 vector = new Vector3(pTarget.currentPosition.x, pTarget.currentPosition.y);
		if (pTarget.objectType == MapObjectType.Actor && pTarget.a.is_moving && pTarget.a.isFlying())
		{
			vector = Vector3.MoveTowards(vector, pTarget.a.nextStepPosition, pTarget.curStats.size * 3f);
		}
		float num = Vector2.Distance(this.currentPosition, pTarget.currentPosition) + pTarget.getZ();
		Vector3 newPoint = Toolbox.getNewPoint(this.currentPosition.x, this.currentPosition.y, vector.x, vector.y, num - pTarget.curStats.size, true);
		Vector3 vector2 = this.currentPosition;
		vector2.y += 0.5f;
		float pAngle = Toolbox.getAngle(vector2.x, vector2.y, vector.x, vector.y) * 57.29578f;
		if (this.s_attackType == WeaponType.Melee)
		{
			vector2 = Toolbox.getNewPoint(vector2.x, vector2.y, vector.x, vector.y + pTarget.getZ(), this.curStats.size * 2f, true);
			this.world.newAttack(this, newPoint, currentTile, pTarget);
		}
		else if (this.s_attackType == WeaponType.Range)
		{
			vector2 = Toolbox.getNewPoint(vector2.x, vector2.y, vector.x, vector.y + pTarget.getZ(), this.curStats.size, true);
			if (!this.tryToCastSpell(pTarget))
			{
				string projectile = base.getWeaponAsset().projectile;
				for (int i = 0; i < this.curStats.projectiles; i++)
				{
					newPoint.x = vector.x;
					newPoint.y = vector.y + 0.1f;
					newPoint.x += Toolbox.randomFloat(-(pTarget.curStats.size + 1f), pTarget.curStats.size + 1f);
					newPoint.y += Toolbox.randomFloat(-pTarget.curStats.size, pTarget.curStats.size);
					Vector3 newPoint2 = Toolbox.getNewPoint(this.currentPosition.x, this.currentPosition.y, vector.x, vector.y, this.curStats.size, true);
					newPoint2.y += 0.5f;
					float pZ = 0f;
					if (pTarget.isInAir())
					{
						pZ = pTarget.getZ();
					}
					Projectile projectile2 = this.world.stackEffects.startProjectile(newPoint2, newPoint, projectile, pZ);
					pAngle = projectile2.getAngle();
					if (projectile2 != null)
					{
						projectile2.byWho = this;
						projectile2.setStats(projectile2.byWho.curStats);
						projectile2.targetObject = pTarget;
					}
				}
			}
		}
		this.world.stackEffects.slash(vector2, this.s_slashType, pAngle);
		return true;
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0004D788 File Offset: 0x0004B988
	private bool tryToCastSpell(BaseSimObject pTarget)
	{
		List<string> attack_spells = this.stats.attack_spells;
		if (attack_spells != null && attack_spells.Count == 0)
		{
			return false;
		}
		Spell spell = AssetManager.spells.get(this.stats.attack_spells.GetRandom<string>());
		if (!Toolbox.randomChance(spell.chance))
		{
			return false;
		}
		if (spell.castTarget == CastTarget.Himself)
		{
			pTarget = this;
		}
		if (spell.castEntity == CastEntity.BuildingsOnly)
		{
			if (pTarget.isActor())
			{
				return false;
			}
		}
		else if (spell.castEntity == CastEntity.UnitsOnly && pTarget.isBuilding())
		{
			return false;
		}
		float num = (float)this.data.health / (float)this.curStats.health;
		if (spell.healthPercent != 0f && spell.healthPercent <= num)
		{
			return false;
		}
		if (spell.min_distance != 0f && Toolbox.DistTile(this.currentTile, pTarget.currentTile) < spell.min_distance)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < spell.action.Count; i++)
		{
			if (spell.action[i](pTarget, pTarget.currentTile))
			{
				flag = true;
			}
		}
		if (flag)
		{
			this.doCastAnimation();
		}
		return flag;
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0004D8A8 File Offset: 0x0004BAA8
	public void doCastAnimation()
	{
		if (this.world.qualityChanger.lowRes)
		{
			return;
		}
		BaseEffect baseEffect = this.world.stackEffects.get(this.stats.effect_cast_ground).spawnAt(this.currentPosition, this.curStats.scale);
		if (baseEffect != null)
		{
			baseEffect.transform.SetParent(base.transform, true);
			baseEffect.transform.localPosition = Vector3.zero;
		}
		baseEffect = this.world.stackEffects.get(this.stats.effect_cast_top).spawnAt(this.currentPosition + ActorBase.actorCastPos, this.curStats.scale);
		if (baseEffect != null)
		{
			baseEffect.transform.SetParent(base.transform, true);
			baseEffect.transform.localPosition = ActorBase.actorCastPos;
		}
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0004D99D File Offset: 0x0004BB9D
	internal override float getZ()
	{
		return this.zPosition.y + this.hitboxZ;
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0004D9B4 File Offset: 0x0004BBB4
	internal void punchTargetAnimation(Vector3 pDirection, WorldTile pTile, bool pFlip = true, bool pReverse = false, float pAngle = 40f)
	{
		if (this.stats.flipAnimation)
		{
			if (pFlip)
			{
				if (this.currentPosition.x < pDirection.x)
				{
					base.setFlip(true);
				}
				else
				{
					base.setFlip(false);
				}
			}
			if (!this.stats.disablePunchAttackAnimation)
			{
				if (pReverse)
				{
					this.targetAngle.z = -pAngle;
					return;
				}
				this.targetAngle.z = pAngle;
			}
		}
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0004DA24 File Offset: 0x0004BC24
	private void calculateAttackOffset(WorldTile pTile)
	{
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0004DA34 File Offset: 0x0004BC34
	public void startColorEffect(string pType = "red")
	{
		if (!this.stats.effectDamage)
		{
			return;
		}
		this.colorEffect = 0.3f;
		if (pType == "red")
		{
			this.colorMaterial = LibraryMaterials.instance.matDamaged;
		}
		else if (pType == "white")
		{
			this.colorMaterial = LibraryMaterials.instance.matHighLighted;
		}
		this.updateColorEffect(0f);
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
	private void updateColorEffect(float pElapsed)
	{
		if (this.colorEffect == 0f)
		{
			return;
		}
		if (this.spriteRenderer == null)
		{
			return;
		}
		this.colorEffect -= pElapsed;
		if (this.colorEffect < 0f)
		{
			this.colorEffect = 0f;
		}
		if (this.colorEffect > 0f)
		{
			this.setSpriteSharedMaterial(this.colorMaterial);
			return;
		}
		this.setSpriteSharedMaterial(LibraryMaterials.instance.matWorldObjects);
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0004DB1E File Offset: 0x0004BD1E
	private void setSpriteSharedMaterial(Material pMat)
	{
		this.spriteRenderer.sharedMaterial = pMat;
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0004DB2C File Offset: 0x0004BD2C
	internal override void getHit(float pDamage, bool pFlash = true, AttackType pType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true)
	{
		this.attackedBy = null;
		if (pSkipIfShake && this.shakeTimer.isActive)
		{
			return;
		}
		if (this.data.health <= 0)
		{
			return;
		}
		if (pType == AttackType.Other)
		{
			float num = 1f - (float)this.curStats.armor / 100f;
			pDamage *= num;
		}
		if (pDamage < 1f)
		{
			pDamage = 1f;
		}
		this.data.health -= (int)pDamage;
		this.timer_action = 0.002f;
		if (pType == AttackType.Other && !this.stats.immune_to_injuries && !base.haveStatus("shield"))
		{
			if (Toolbox.randomChance(0.02f))
			{
				base.addTrait("crippled");
			}
			if (Toolbox.randomChance(0.02f))
			{
				base.addTrait("eyepatch");
			}
			this.addExperience(1);
		}
		if (pFlash)
		{
			this.startColorEffect("red");
		}
		if (this.data.health <= 0)
		{
			if (pAttacker != this)
			{
				this.attackedBy = pAttacker;
			}
			this.killHimself(false, pType, true, true);
			if (pAttacker != null && pAttacker != this && pAttacker.isActor())
			{
				pAttacker.a.increaseKillCount();
				if (pAttacker.a.stats.use_items && !base.haveTrait("infected") && pAttacker.a.stats.take_items)
				{
					pAttacker.a.takeItems(this, pAttacker.a.stats.take_items_ignore_range_weapons);
				}
				if (pAttacker.city != null)
				{
					bool flag = false;
					if (this.stats.animal)
					{
						flag = true;
						pAttacker.city.data.storage.change("meat", 1);
					}
					else if (this.stats.unit && pAttacker.a.haveTrait("savage"))
					{
						flag = true;
					}
					if (flag)
					{
						if (Toolbox.randomChance(0.5f))
						{
							pAttacker.city.data.storage.change("bones", 1);
							return;
						}
						if (Toolbox.randomChance(0.5f))
						{
							pAttacker.city.data.storage.change("leather", 1);
							return;
						}
						if (Toolbox.randomChance(0.5f))
						{
							pAttacker.city.data.storage.change("meat", 1);
							return;
						}
					}
				}
			}
		}
		else
		{
			this.shakeTimer.startTimer(-1f);
			if (pAttacker != this)
			{
				this.attackedBy = pAttacker;
			}
			if (this.attackTarget == null && this.attackedBy != null && !this.targetsToIgnore.Contains(this.attackedBy) && base.canAttackTarget(this.attackedBy))
			{
				this.attackTarget = this.attackedBy;
			}
			if (this.activeStatusEffects != null)
			{
				for (int i = 0; i < this.activeStatusEffects.Count; i++)
				{
					ActiveStatusEffect activeStatusEffect = this.activeStatusEffects[i];
					if (activeStatusEffect.asset.actionOnHit != null)
					{
						activeStatusEffect.asset.actionOnHit(this, this.currentTile);
					}
				}
			}
			for (int j = 0; j < this.callbacks_get_hit.Count; j++)
			{
				this.callbacks_get_hit[j](this);
			}
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0004DE94 File Offset: 0x0004C094
	public void takeItems(Actor pActor, bool pIgnoreRangeWeapons = false)
	{
		if (!this.stats.use_items)
		{
			return;
		}
		if (!pActor.stats.use_items)
		{
			return;
		}
		List<ActorEquipmentSlot> list = ActorEquipment.getList(pActor.equipment, false);
		for (int i = 0; i < list.Count; i++)
		{
			ActorEquipmentSlot actorEquipmentSlot = list[i];
			if (actorEquipmentSlot.data != null && (actorEquipmentSlot.type != EquipmentType.Weapon || !pIgnoreRangeWeapons || AssetManager.items.get(actorEquipmentSlot.data.id).attackType != WeaponType.Range))
			{
				ActorEquipmentSlot slot = this.equipment.getSlot(actorEquipmentSlot.type);
				if (slot.data == null)
				{
					slot.setItem(actorEquipmentSlot.data);
					actorEquipmentSlot.emptySlot();
					this.setStatsDirty();
					this.item_sprite_dirty = true;
				}
				else
				{
					ItemTools.calcItemValues(slot.data);
					int s_value = ItemTools.s_value;
					ItemTools.calcItemValues(actorEquipmentSlot.data);
					if (ItemTools.s_value > s_value)
					{
						slot.setItem(actorEquipmentSlot.data);
						actorEquipmentSlot.emptySlot();
						this.setStatsDirty();
						this.item_sprite_dirty = true;
					}
				}
			}
		}
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0004DFAC File Offset: 0x0004C1AC
	internal void increaseKillCount()
	{
		this.data.kills++;
		if (this.data.kills > 10)
		{
			base.addTrait("veteran");
		}
		this.addExperience(10);
		if (base.haveTrait("madness"))
		{
			this.restoreHealth(this.curStats.health / 15 + 1);
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0004E012 File Offset: 0x0004C212
	public int getExpToLevelup()
	{
		return 100 + (this.data.level - 1) * 20;
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0004E028 File Offset: 0x0004C228
	internal void addExperience(int pValue)
	{
		if (!this.stats.canLevelUp)
		{
			return;
		}
		int num = 10;
		Culture culture = base.getCulture();
		if (culture != null)
		{
			num += culture.getMaxLevelBonus();
		}
		if (this.data.level >= num)
		{
			return;
		}
		int expToLevelup = this.getExpToLevelup();
		this.data.experience += pValue;
		if (this.data.experience >= expToLevelup)
		{
			this.data.experience = 0;
			this.data.level++;
			if (this.data.level == num)
			{
				this.data.experience = expToLevelup;
			}
			this.setStatsDirty();
			this.event_full_heal = true;
		}
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0004E0D6 File Offset: 0x0004C2D6
	public void startBabymakingTimeout()
	{
		if (this.timerReproduction == null)
		{
			return;
		}
		this.timerReproduction.startTimer(40f + Toolbox.randomFloat(0f, 40f));
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0004E101 File Offset: 0x0004C301
	public void stayInBuilding(Building pBuilding)
	{
		this.insideBuilding = pBuilding;
		this.setActorVisible(false);
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0004E111 File Offset: 0x0004C311
	internal void exitHouse()
	{
		this.timer_action = 0f;
		this.insideBuilding = null;
		this.setActorVisible(true);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0004E12C File Offset: 0x0004C32C
	internal void embarkInto(Boat pBoat)
	{
		ActorJob job = this.ai.job;
		if (job != null && job.cancel_when_embarking)
		{
			base.cancelAllBeh(null);
		}
		else
		{
			this.clearTasks();
			base.stopMovement();
			this.ai.restartJob();
		}
		this.data.transportID = pBoat.actor.data.actorID;
		this.setActorVisible(false);
		this.m_transform.localPosition = Globals.POINT_IN_VOID;
		this.lastTransformPosition.Set(Globals.POINT_IN_VOID.x, Globals.POINT_IN_VOID.y, Globals.POINT_IN_VOID.z);
		this.insideBoat = pBoat;
		this.insideBoat.addUnitInto(this);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0004E1E0 File Offset: 0x0004C3E0
	private void setActorVisible(bool pVal)
	{
		this._activeSelf = pVal;
		this.m_gameObject.SetActive(pVal);
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0004E1F5 File Offset: 0x0004C3F5
	internal void disembarkTo(Boat pBoat, WorldTile pTile)
	{
		this.spawnOn(pTile, 0f);
		this.data.transportID = null;
		this.setActorVisible(true);
		this.insideBoat = null;
		this._currentTileDirty = true;
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0004E224 File Offset: 0x0004C424
	internal bool isCurrentJob(string pJob)
	{
		return this.ai.job != null && this.ai.job.id == pJob;
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0004E24C File Offset: 0x0004C44C
	private void checkZoneForGround(TileZone pZone)
	{
		for (int i = 0; i < pZone.tiles.Count; i++)
		{
			WorldTile worldTile = pZone.tiles[i];
			if (!worldTile.Type.liquid)
			{
				ActorBase.possible_moves.Add(worldTile);
			}
		}
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0004E294 File Offset: 0x0004C494
	internal void becomeCitizen(City pCity)
	{
		if (this.city == pCity)
		{
			return;
		}
		pCity.addNewUnit(this, true, true);
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0004E2B0 File Offset: 0x0004C4B0
	internal override void spawnOn(WorldTile pTile, float pZHeight = 0f)
	{
		base.spawnOn(pTile, pZHeight);
		if (this.stats.id.Contains("santa"))
		{
			float num = Toolbox.randomFloat(30f, 50f);
			this.currentPosition.y = this.currentPosition.y - num;
			this.zPosition.y = num;
			return;
		}
		if (this.stats.id.Contains("UFO"))
		{
			this.zPosition.y = 8f;
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0004E330 File Offset: 0x0004C530
	internal void applyRandomForce()
	{
		float num = Toolbox.randomFloat(0.05f, 0.4f);
		WorldTile random = this.currentTile.neighboursAll.GetRandom<WorldTile>();
		float angle = Toolbox.getAngle(this.currentTile.posV3.x, this.currentTile.posV3.y, random.posV3.x, random.posV3.y);
		base.addForce(-Mathf.Cos(angle) * num, -Mathf.Sin(angle) * num, num);
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
	public void applyMadness(Actor pActor)
	{
		if (pActor.city != null)
		{
			pActor.city.removeCitizen(this, false, AttackType.Other);
			pActor.removeFromCity();
		}
		pActor.removeTrait("peaceful");
		pActor.startShake(0.3f, 0.2f, true, true);
		pActor.startColorEffect("white");
		if (pActor.kingdom != null)
		{
			pActor.kingdom.removeUnit(pActor);
		}
		pActor.setKingdom(this.world.kingdoms.dict_hidden["mad"]);
		pActor.cancelAllBeh(null);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0004E448 File Offset: 0x0004C648
	private void updateGrowthScale()
	{
		if (this.targetScale == 0f)
		{
			return;
		}
		this.actorScale += 0.001f;
		if (this.actorScale > this.targetScale)
		{
			this.actorScale = this.targetScale;
		}
		Vector3 vector = new Vector3(this.actorScale, this.actorScale, this.actorScale);
		if (this.flipScale.x < 0f)
		{
			vector.x = -vector.x;
		}
		this.m_transform.localScale = vector;
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
	internal void justBorn()
	{
		this.actorScale = 0.02f;
		this.updateGrowthScale();
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0004E4E7 File Offset: 0x0004C6E7
	public void removeFromCity()
	{
		this.setCity(null);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0004E4F0 File Offset: 0x0004C6F0
	internal void setCity(City pCity)
	{
		this.city = pCity;
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0004E4F9 File Offset: 0x0004C6F9
	internal void removeFromGroup()
	{
		if (this.unitGroup != null)
		{
			this.unitGroup.removeUnit(this);
		}
		this.unitGroup = null;
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0004E518 File Offset: 0x0004C718
	public bool isInCityIsland()
	{
		if (this.city == null)
		{
			return false;
		}
		WorldTile tile = this.city.getTile();
		return tile != null && this.currentTile.isSameIsland(tile);
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0004E557 File Offset: 0x0004C757
	public void setGroupLeader(bool pVal)
	{
		this.isGroupLeader = pVal;
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0004E560 File Offset: 0x0004C760
	public AnimationFrameData getAnimationFrameData()
	{
		return this.frameData;
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0004E568 File Offset: 0x0004C768
	public bool isInfectedWithAnything()
	{
		return base.haveTrait("infected") || base.haveTrait("plague") || (base.haveTrait("mushSpores") && this.stats.canTurnIntoMush) || (base.haveTrait("tumorInfection") && this.stats.canTurnIntoTumorMonster);
	}

	// Token: 0x04000865 RID: 2149
	public bool new_creature = true;

	// Token: 0x04000866 RID: 2150
	internal float colorEffect;

	// Token: 0x04000867 RID: 2151
	internal WorldTimer timerReproduction;

	// Token: 0x04000868 RID: 2152
	private WorldTimer growTimer;

	// Token: 0x04000869 RID: 2153
	private float specialTimer;

	// Token: 0x0400086A RID: 2154
	private float _timerSound = 2f;

	// Token: 0x0400086B RID: 2155
	private List<BaseActorComponent> children_special;

	// Token: 0x0400086C RID: 2156
	private List<BaseActorComponent> children_pre_behaviour;

	// Token: 0x0400086D RID: 2157
	private List<BaseActorComponent> children_behaviour;

	// Token: 0x0400086E RID: 2158
	private HoverModule _hoverModule;

	// Token: 0x0400086F RID: 2159
	private static List<ActorTrait> _tempTraitList = new List<ActorTrait>();

	// Token: 0x04000870 RID: 2160
	internal Vector2 curShadowPosition = Globals.POINT_IN_VOID;

	// Token: 0x04000871 RID: 2161
	internal Vector3 curTransformPosition;

	// Token: 0x04000872 RID: 2162
	internal Vector3 lastTransformPosition = Globals.POINT_IN_VOID;

	// Token: 0x04000873 RID: 2163
	internal Material colorMaterial;

	// Token: 0x04000874 RID: 2164
	private static List<string> temp_tasks = new List<string>();

	// Token: 0x04000875 RID: 2165
	internal int diet_index;

	// Token: 0x04000876 RID: 2166
	private bool _activeSelf;
}
