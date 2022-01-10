using System;
using System.Collections.Generic;
using System.Text;
using ai;
using ai.behaviours;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class ActorBase : BaseSimObject
{
	// Token: 0x0600023F RID: 575 RVA: 0x0002A408 File Offset: 0x00028608
	internal override void create()
	{
		base.create();
		this.ai = new AiSystemActor((Actor)this);
		this.ai.jobs_library = AssetManager.job_actor;
		this.ai.task_library = AssetManager.tasks_actor;
		this.ai.nextJobDelegate = new GetNextJobID(this.getNextJob);
		this.ai.jobSetStartAction = new JobAction(this.jobSetStartActionAction);
		this.ai.jobSetEndAction = new JobAction(this.jobSetEndActionAction);
		this.ai.clearAction = new JobAction(this.clearBeh);
		this.ai.taskSwitchedAction = new JobAction(this.taskSwitchedAction);
		this.targetsToIgnore = new HashSet<BaseMapObject>();
		this.targetsToIgnoreTimer = new WorldTimer(3f, new Action(this.clearIgnoreTargets));
		this.flying = this.stats.flying;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0002A4F7 File Offset: 0x000286F7
	internal void clearBeh()
	{
		this.clearTasks();
		this.beh_tile_target = null;
		this.beh_building_target = null;
		this.beh_actor_target = null;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0002A514 File Offset: 0x00028714
	internal void taskSwitchedAction()
	{
		this.item_sprite_dirty = true;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0002A51D File Offset: 0x0002871D
	public void jobSetStartActionAction()
	{
		if (this.data.alive)
		{
			this.loadTexture();
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0002A532 File Offset: 0x00028732
	public void jobSetEndActionAction()
	{
		if (this.data.alive)
		{
			this.loadTexture();
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0002A547 File Offset: 0x00028747
	public string getNextJob()
	{
		return ActorBase.nextJobActor(this);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0002A550 File Offset: 0x00028750
	public static string nextJobActor(ActorBase pActor)
	{
		string text;
		if (pActor.stats.unit)
		{
			if (pActor.stats.baby)
			{
				text = "baby";
			}
			else if (pActor.city != null)
			{
				text = "citizen";
			}
			else
			{
				text = pActor.stats.job;
			}
		}
		else
		{
			text = pActor.stats.job;
		}
		if (pActor.haveTrait("madness"))
		{
			text = "random_move";
		}
		if (pActor.haveStatus("burning"))
		{
			text = "unit_on_fire";
		}
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		return text;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0002A5E4 File Offset: 0x000287E4
	internal void newCreature(int pGameTime = 0)
	{
		this.world.gameStats.data.creaturesCreated++;
		AchievementLibrary.achievement10000Creatures.check();
		this.race = AssetManager.raceLibrary.get(this.stats.race);
		this.setData(new ActorStatus());
		this.data.actorID = this.world.mapStats.getNextId("unit");
		this.generatePersonality();
		this.updateStats();
		this.data.bornTime = pGameTime;
		this.data.health = this.curStats.health;
		this.data.statsID = this.stats.id;
		if (this.stats.diet_meat)
		{
			this.data.hunger = this.stats.maxHunger / 2;
			return;
		}
		if (this.stats.needFood)
		{
			this.data.hunger = this.stats.maxHunger;
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0002A6F0 File Offset: 0x000288F0
	public override void setData(BaseObjectData pData)
	{
		this.data = (ActorStatus)pData;
		base.setData(pData);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0002A708 File Offset: 0x00028908
	public static void generateCivUnit(ActorStats pStats, ActorStatus pStatus, Race pRace)
	{
		if (Toolbox.randomBool())
		{
			pStatus.gender = ActorGender.Male;
		}
		else
		{
			pStatus.gender = ActorGender.Female;
		}
		pStatus.favoriteFood = pRace.preferred_food.GetRandom<string>();
		pStatus.firstName = NameGenerator.getName(pStats.nameTemplate, pStatus.gender);
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0002A754 File Offset: 0x00028954
	private void generatePersonality()
	{
		this.data.generateTraits(this.stats, this.race);
		if (this.race.civilization)
		{
			ActorBase.generateCivUnit(this.stats, this.data, this.race);
		}
		else
		{
			this.data.firstName = NameGenerator.getName(this.stats.nameTemplate, ActorGender.Male);
		}
		if (this.stats.defaultWeapons != null && this.stats.defaultWeapons.Count > 0)
		{
			string random = this.stats.defaultWeapons.GetRandom<string>();
			ItemAsset pItem = AssetManager.items.get(random);
			string pMaterial = "base";
			if (this.stats.defaultWeaponsMaterial.Count > 0)
			{
				pMaterial = this.stats.defaultWeaponsMaterial.GetRandom<string>();
			}
			ItemGenerator.generateItem(pItem, pMaterial, this.equipment.weapon, this.world.mapStats.year - 10, null, null, 1);
		}
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
		this.setStatsDirty();
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0002A89C File Offset: 0x00028A9C
	public void resetTraits()
	{
		this.data.traits = new List<string>();
		this.s_traits_ids.Clear();
		if (this.stats.traits != null)
		{
			for (int i = 0; i < this.stats.traits.Count; i++)
			{
				string pTrait = this.stats.traits[i];
				this.addTrait(pTrait);
			}
		}
		this.setStatsDirty();
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0002A90C File Offset: 0x00028B0C
	public void removeTrait(string pTrait)
	{
		for (int i = 0; i < this.data.traits.Count; i++)
		{
			if (this.data.traits[i] == pTrait)
			{
				this.data.traits.RemoveAt(i);
				break;
			}
		}
		this.setStatsDirty();
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0002A968 File Offset: 0x00028B68
	public bool addTrait(string pTrait)
	{
		if (this.haveTrait(pTrait))
		{
			return false;
		}
		if (this.haveOppositeTrait(pTrait))
		{
			return false;
		}
		if (pTrait == "madness")
		{
			foreach (BaseActionActor baseActionActor in this.callbacks_added_madness)
			{
				baseActionActor((Actor)this);
			}
		}
		this.data.traits.Add(pTrait);
		this.setStatsDirty();
		return true;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0002A9FC File Offset: 0x00028BFC
	private void clearIgnoreTargets()
	{
		this.targetsToIgnore.Clear();
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0002AA0C File Offset: 0x00028C0C
	private string getUnitTexture()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("races/");
		stringBuilder.Append(this.stats.race);
		stringBuilder.Append('/');
		Culture culture = this.world.cultures.get(this.data.culture);
		if (this.professionAsset.use_skin_culture && culture != null)
		{
			UnitProfession profession_id = this.professionAsset.profession_id;
			if (profession_id != UnitProfession.Unit)
			{
				if (profession_id == UnitProfession.Warrior)
				{
					stringBuilder.Append(culture.skin_warrior);
				}
			}
			else if (this.data.gender == ActorGender.Female)
			{
				stringBuilder.Append(culture.skin_citizen_female);
			}
			else
			{
				stringBuilder.Append(culture.skin_citizen_male);
			}
		}
		else if (!string.IsNullOrEmpty(this.professionAsset.special_skin_path))
		{
			stringBuilder.Append(this.professionAsset.special_skin_path);
		}
		else if (this.data.gender == ActorGender.Female)
		{
			stringBuilder.Append(this.race.skin_civ_default_female);
		}
		else
		{
			stringBuilder.Append(this.race.skin_civ_default_male);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0002AB28 File Offset: 0x00028D28
	protected void loadTexture()
	{
		string b;
		if (this.stats.unit)
		{
			b = this.getUnitTexture();
		}
		else
		{
			b = this.stats.texture_path;
		}
		if (!(this.current_texture != b))
		{
			return;
		}
		this.current_texture = b;
		if (this.current_texture == string.Empty)
		{
			return;
		}
		this.actorAnimationData = ActorAnimationLoader.loadAnimationUnit("actors/" + this.current_texture, this.stats);
		this.setBodySprite(this.actorAnimationData.idle.frames[0]);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0002ABBC File Offset: 0x00028DBC
	public void addForce(float pX, float pY, float pZ)
	{
		if (!this.stats.canBeMovedByPowers)
		{
			return;
		}
		if (this.zPosition.y > 0f)
		{
			return;
		}
		this.forceVector.x = pX * 0.6f;
		this.forceVector.y = pY * 0.6f;
		this.forceVector.z = pZ * 2f;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0002AC20 File Offset: 0x00028E20
	internal override bool isInAir()
	{
		return this.flying || this.stats.hovering;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0002AC37 File Offset: 0x00028E37
	internal bool isFlying()
	{
		return this.flying;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0002AC3F File Offset: 0x00028E3F
	internal bool isHovering()
	{
		return this.stats.hovering;
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0002AC4C File Offset: 0x00028E4C
	internal bool isAffectedByLiquid()
	{
		return !this.isInAir() && this._isInLiquid;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0002AC5E File Offset: 0x00028E5E
	private void OnBecameVisible()
	{
		this._is_visible = true;
		this.updateAnimation(this.world.elapsed, false);
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0002AC79 File Offset: 0x00028E79
	private void OnBecameInvisible()
	{
		this._is_visible = false;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0002AC84 File Offset: 0x00028E84
	internal void updateAnimation(float pElapsed, bool pForce = false)
	{
		if (this.stats.specialAnimation)
		{
			return;
		}
		if (this.ai.action != null && this.ai.action.forceAnimation != null)
		{
			((UnitSpriteAnimation)this.spriteAnimation).applyCurrentSpriteGraphics(this.actorAnimationData.sprites[this.ai.action.forceAnimation]);
			return;
		}
		if (!pForce && !this._is_visible)
		{
			return;
		}
		if (!this.data.alive || this.spriteAnimation == null)
		{
			return;
		}
		if ((this.is_moving || this._isInLiquid) && this.zPosition.y == 0f)
		{
			this.spriteAnimation.isOn = true;
			if (this.isAffectedByLiquid() && this.actorAnimationData.swimming != null)
			{
				this.setAnimation(this.actorAnimationData.swimming);
				return;
			}
			this.setAnimation(this.actorAnimationData.walking);
			return;
		}
		else
		{
			if (this.timer_jump_animation > 0f)
			{
				this.spriteAnimation.isOn = true;
				return;
			}
			if (this.zPosition.y <= 0f || !this.spriteAnimation.isOn)
			{
				this.setAnimation(this.actorAnimationData.idle);
				return;
			}
			this._last_animation_id = -1;
			if (this.spriteAnimation.frames.Length > 2)
			{
				this.spriteAnimation.stopAt(2, false);
				return;
			}
			this.spriteAnimation.stopAt(0, false);
			return;
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0002ADFC File Offset: 0x00028FFC
	private void setAnimation(ActorAnimation pAnimation)
	{
		if (this._last_animation_id == pAnimation.id)
		{
			return;
		}
		this._last_animation_id = pAnimation.id;
		this.spriteAnimation.isOn = true;
		this.spriteAnimation.setFrames(pAnimation.frames);
		this.spriteAnimation.timeBetweenFrames = pAnimation.timeBetweenFrames;
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0002AE54 File Offset: 0x00029054
	public override void update(float pElapsed)
	{
		if (this.statsDirty)
		{
			this.updateStats();
		}
		this.updateSpriteAnimation(pElapsed);
		if (this.timer_jump_animation > 0f)
		{
			this.timer_jump_animation -= pElapsed;
		}
		this.checkFindCurrentTile();
		this.checkIsInWater();
		this.updateAnimation(pElapsed, false);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0002AEA5 File Offset: 0x000290A5
	private void checkFindCurrentTile()
	{
		if (this._currentTileDirty || (this.nextStepTile != null && Toolbox.DistTile(this.currentTile, this.nextStepTile) > 2f))
		{
			this.findCurrentTile(true);
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0002AED8 File Offset: 0x000290D8
	private void checkIsInWater()
	{
		bool flag = this.currentTile.Type.liquid && this.moveJumpOffset.y == 0f && this.zPosition.y <= 0f && this.data.alive;
		if (flag != this._isInLiquid)
		{
			this._isInLiquid = flag;
			this.item_sprite_dirty = true;
		}
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0002AF41 File Offset: 0x00029141
	internal override void updateSpriteAnimation(float pElapsed)
	{
		if (!this._is_visible)
		{
			return;
		}
		this.spriteAnimation.update(pElapsed);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0002AF58 File Offset: 0x00029158
	internal ItemAsset getWeaponAsset()
	{
		if (this.stats.use_items && !this.equipment.weapon.isEmpty())
		{
			return AssetManager.items.get(this.equipment.weapon.data.id);
		}
		return AssetManager.items.get(this.stats.defaultAttack);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0002AFBC File Offset: 0x000291BC
	internal void updateTargetScale()
	{
		if (this.m_transform == null)
		{
			return;
		}
		if (this.curStats.scale > 0f)
		{
			this.targetScale = this.curStats.scale;
			this.m_transform.localScale = new Vector3(this.targetScale, this.targetScale, this.targetScale);
			this.actorScale = this.targetScale;
			return;
		}
		this.targetScale = 0f;
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0002B038 File Offset: 0x00029238
	internal override void updateStats()
	{
		base.updateStats();
		if (!this.data.alive)
		{
			return;
		}
		if (this.stats.useSkinColors && this.data.skin_set == -1 && this.stats.color_sets != null)
		{
			this.setSkinSet("default");
		}
		if (this.stats.useSkinColors && this.data.skin == -1)
		{
			this.data.skin = Toolbox.randomInt(0, this.stats.color_sets[this.data.skin_set].colors.Count);
		}
		if (string.IsNullOrEmpty(this.data.mood))
		{
			this.data.mood = "normal";
		}
		MoodAsset moodAsset = AssetManager.moods.get(this.data.mood);
		this.curStats.clear();
		this.curStats.addStats(this.stats.baseStats);
		this.curStats.addStats(moodAsset.baseStats);
		this.curStats.diplomacy += this.data.diplomacy;
		this.curStats.stewardship += this.data.stewardship;
		this.curStats.intelligence += this.data.intelligence;
		this.curStats.warfare += this.data.warfare;
		if (this.activeStatusEffects != null)
		{
			for (int i = 0; i < this.activeStatusEffects.Count; i++)
			{
				ActiveStatusEffect activeStatusEffect = this.activeStatusEffects[i];
				this.curStats.addStats(activeStatusEffect.asset.baseStats);
			}
		}
		ItemAsset itemAsset = AssetManager.items.get(this.stats.defaultAttack);
		if (itemAsset != null)
		{
			this.curStats.addStats(itemAsset.baseStats);
		}
		this.s_attackType = this.getWeaponAsset().attackType;
		this.s_slashType = this.getWeaponAsset().slash;
		this.item_sprite_dirty = true;
		if (this.stats.use_items && !this.equipment.weapon.isEmpty())
		{
			this.s_weapon_texture = "w_" + this.equipment.weapon.data.id + "_" + this.equipment.weapon.data.material;
		}
		else
		{
			this.s_weapon_texture = string.Empty;
		}
		this.findHeadSprite();
		for (int j = 0; j < this.data.traits.Count; j++)
		{
			string pID = this.data.traits[j];
			ActorTrait actorTrait = AssetManager.traits.get(pID);
			if (actorTrait != null)
			{
				this.curStats.addStats(actorTrait.baseStats);
			}
		}
		if (this.stats.unit)
		{
			this.s_personality = null;
			if ((this.kingdom != null && this.kingdom.isCiv() && this.kingdom.king == this) || (this.city != null && this.city.leader == this))
			{
				string pID2 = "balanced";
				int num = this.curStats.diplomacy;
				if (this.curStats.diplomacy > this.curStats.stewardship)
				{
					pID2 = "diplomat";
					num = this.curStats.diplomacy;
				}
				else if (this.curStats.diplomacy < this.curStats.stewardship)
				{
					pID2 = "administrator";
					num = this.curStats.stewardship;
				}
				if (this.curStats.warfare > num)
				{
					pID2 = "militarist";
				}
				this.s_personality = AssetManager.personalities.get(pID2);
				this.curStats.addStats(this.s_personality.baseStats);
			}
		}
		this._trait_weightless = this.haveTrait("weightless");
		this._status_frozen = base.haveStatus("frozen");
		this.curStats.damage += (this.data.level - 1) / 2;
		this.curStats.armor += (this.data.level - 1) / 3;
		this.curStats.crit += (float)(this.data.level - 1);
		this.curStats.attackSpeed += (float)(this.data.level - 1);
		this.curStats.health += (this.data.level - 1) * 20;
		this.s_traits_ids.Clear();
		List<ActorTrait> list = this.s_special_effect_traits;
		if (list != null)
		{
			list.Clear();
		}
		for (int k = 0; k < this.data.traits.Count; k++)
		{
			string text = this.data.traits[k];
			this.s_traits_ids.Add(text);
			ActorTrait actorTrait2 = AssetManager.traits.get(text);
			if (actorTrait2 != null && actorTrait2.action_special_effect != null)
			{
				if (this.s_special_effect_traits == null)
				{
					this.s_special_effect_traits = new List<ActorTrait>();
				}
				this.s_special_effect_traits.Add(actorTrait2);
			}
		}
		List<ActorTrait> list2 = this.s_special_effect_traits;
		if (list2 != null && list2.Count == 0)
		{
			this.s_special_effect_traits = null;
		}
		this.checkMadness();
		this._trait_peaceful = this.haveTrait("peaceful");
		this._trait_fire_resistant = this.haveTrait("fire_proof");
		if (this.stats.use_items)
		{
			List<ActorEquipmentSlot> list3 = ActorEquipment.getList(this.equipment, false);
			for (int l = 0; l < list3.Count; l++)
			{
				ActorEquipmentSlot actorEquipmentSlot = list3[l];
				if (actorEquipmentSlot.data != null)
				{
					ItemTools.calcItemValues(actorEquipmentSlot.data);
					this.curStats.addStats(ItemTools.s_stats);
				}
			}
		}
		this.curStats.normalize();
		if (this.event_full_heal)
		{
			this.event_full_heal = false;
			this.data.health = this.curStats.health;
		}
		this.curStats.health += (int)((float)this.curStats.health * (this.curStats.mod_health / 100f));
		this.curStats.damage += (int)((float)this.curStats.damage * (this.curStats.mod_damage / 100f));
		this.curStats.armor += (int)((float)this.curStats.armor * (this.curStats.mod_armor / 100f));
		this.curStats.crit += (float)((int)(this.curStats.crit * (this.curStats.mod_crit / 100f)));
		this.curStats.diplomacy += (int)((float)this.curStats.diplomacy * (this.curStats.mod_diplomacy / 100f));
		this.curStats.speed += (float)((int)(this.curStats.speed * (this.curStats.mod_speed / 100f)));
		this.curStats.attackSpeed += (float)((int)(this.curStats.attackSpeed * (this.curStats.mod_attackSpeed / 100f)));
		Culture culture = this.getCulture();
		if (culture != null)
		{
			this.curStats.damage = (int)((float)this.curStats.damage + (float)this.curStats.damage * culture.stats.bonus_damage.value);
			this.curStats.armor = (int)((float)this.curStats.armor + (float)this.curStats.armor * culture.stats.bonus_armor.value);
		}
		if (this.curStats.health < 1)
		{
			this.curStats.health = 1;
		}
		if (this.data.health > this.curStats.health)
		{
			this.data.health = this.curStats.health;
		}
		if (this.data.health < 1)
		{
			this.data.health = 1;
		}
		if (this.curStats.damage < 1)
		{
			this.curStats.damage = 1;
		}
		if (this.curStats.speed < 1f)
		{
			this.curStats.speed = 1f;
		}
		if (this.curStats.armor < 0)
		{
			this.curStats.armor = 0;
		}
		if (this.curStats.diplomacy < 0)
		{
			this.curStats.diplomacy = 0;
		}
		if (this.curStats.dodge < 0f)
		{
			this.curStats.dodge = 0f;
		}
		if (this.curStats.accuracy < 10f)
		{
			this.curStats.accuracy = 10f;
		}
		if (this.curStats.crit < 0f)
		{
			this.curStats.crit = 0f;
		}
		if (this.curStats.attackSpeed < 0f)
		{
			this.curStats.attackSpeed = 1f;
		}
		if (this.curStats.attackSpeed >= 300f)
		{
			this.curStats.attackSpeed = 300f;
		}
		this.s_attackSpeed_seconds = (300f - this.curStats.attackSpeed) / (100f + this.curStats.attackSpeed);
		this.curStats.s_crit_chance = this.curStats.crit / 100f;
		this.curStats.zones = (this.curStats.stewardship + 1) * 2;
		this.curStats.cities = this.curStats.stewardship / 5 + 1;
		this.curStats.army = this.curStats.warfare + 5;
		this.curStats.bonus_towers = this.curStats.warfare / 10;
		if (this.curStats.bonus_towers > 2)
		{
			this.curStats.bonus_towers = 2;
		}
		if (this.curStats.army < 0)
		{
			this.curStats.army = 5;
		}
		this.attackTimer = 0f;
		this.updateTargetScale();
		this.curStats.normalize();
		this.currentScale.x = this.curStats.scale;
		this.currentScale.y = this.curStats.scale;
		this.currentScale.z = this.curStats.scale;
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0002BAE9 File Offset: 0x00029CE9
	public Culture getCulture()
	{
		if (string.IsNullOrEmpty(this.data.culture))
		{
			return null;
		}
		return this.world.cultures.get(this.data.culture);
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0002BB1C File Offset: 0x00029D1C
	private void findHeadSprite()
	{
		if (this.actorAnimationData == null)
		{
			return;
		}
		if (!this.stats.body_separate_part_head)
		{
			return;
		}
		this.setHeadSprite(null);
		this.tryToLoadFunHead();
		if (this.s_head_sprite != null)
		{
			return;
		}
		if (!this.stats.unit)
		{
			this.checkHeadID();
			if (this.data.head > this.actorAnimationData.heads.Length - 1)
			{
				this.data.head = 0;
			}
			this.setHeadSprite(this.actorAnimationData.heads[this.data.head]);
			return;
		}
		bool flag = false;
		string text;
		if (this.data.profession == UnitProfession.Warrior && !this.equipment.helmet.isEmpty())
		{
			text = "head_warrior";
			flag = true;
		}
		else if (this.data.profession == UnitProfession.King)
		{
			text = "head_king";
			flag = true;
		}
		else if (this.haveTrait("wise"))
		{
			if (this.data.gender == ActorGender.Male)
			{
				text = "head_old_male";
			}
			else
			{
				text = "head_old_female";
			}
			flag = true;
		}
		else if (this.data.gender == ActorGender.Male)
		{
			text = "heads_male";
		}
		else
		{
			text = "heads_female";
		}
		string pPath;
		if (flag)
		{
			pPath = "actors/races/" + this.stats.race + "/heads_special";
			this.setHeadSprite(ActorAnimationLoader.getHeadSpecial(pPath, text));
			return;
		}
		pPath = "actors/races/" + this.stats.race + "/" + text;
		this.checkHeadID();
		this.setHeadSprite(ActorAnimationLoader.getHead(pPath, this.data.head));
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0002BCA8 File Offset: 0x00029EA8
	private void setHeadSprite(Sprite pSprite)
	{
		this.s_head_sprite = pSprite;
		if (this.s_head_sprite == null)
		{
			this.id_sprite_head = -1;
			return;
		}
		this.id_sprite_head = -1;
		ActorAnimationLoader.int_ids_heads.TryGetValue(this.s_head_sprite, ref this.id_sprite_head);
		if (this.id_sprite_head == 0)
		{
			int num = ActorAnimationLoader.int_ids_heads.Count + 1;
			ActorAnimationLoader.int_ids_heads.Add(this.s_head_sprite, num);
			this.id_sprite_head = num;
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0002BD20 File Offset: 0x00029F20
	internal void setBodySprite(Sprite pSprite)
	{
		this.s_body_sprite = pSprite;
		if (this.s_body_sprite == null)
		{
			this.id_sprite_body = 0;
			return;
		}
		this.id_sprite_body = -1;
		ActorAnimationLoader.int_ids_body.TryGetValue(this.s_body_sprite, ref this.id_sprite_body);
		if (this.id_sprite_body == 0)
		{
			int num = ActorAnimationLoader.int_ids_body.Count + 1;
			ActorAnimationLoader.int_ids_body.Add(this.s_body_sprite, num);
			this.id_sprite_body = num;
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0002BD98 File Offset: 0x00029F98
	private void tryToLoadFunHead()
	{
		string text = string.Empty;
		if (DebugConfig.isOn(DebugOption.BurgerHeads))
		{
			text = "t_head_burger";
		}
		else if (DebugConfig.isOn(DebugOption.TrunkHeads))
		{
			text = "t_head_trunk";
		}
		if (!string.IsNullOrEmpty(text))
		{
			string pPath = "actors/" + text;
			this.setHeadSprite(ActorAnimationLoader.getHeadFromFullPath(pPath));
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0002BDEC File Offset: 0x00029FEC
	internal void checkHeadID()
	{
		if (this.data.head == -1)
		{
			this.loadTexture();
			if (this.stats.unit)
			{
				this.data.head = Toolbox.randomInt(0, this.stats.heads);
				return;
			}
			this.data.head = Toolbox.randomInt(0, this.actorAnimationData.heads.Length);
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0002BE58 File Offset: 0x0002A058
	protected void checkMadness()
	{
		if (this.haveTrait("madness") && (this.kingdom == null || !this.kingdom.asset.mad))
		{
			this.setKingdom(this.world.kingdoms.dict_hidden["mad"]);
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0002BEAC File Offset: 0x0002A0AC
	internal void setKingdom(Kingdom pKingdom)
	{
		if (this.kingdom == pKingdom)
		{
			return;
		}
		if (pKingdom == null && this.data.alive)
		{
			MonoBehaviour.print("kingdom is set... ");
		}
		if (this.kingdom != null)
		{
			this.kingdom.removeUnit((Actor)this);
		}
		this.kingdom = pKingdom;
		if (this.kingdom != null)
		{
			this.kingdom.addUnit((Actor)this);
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0002BF16 File Offset: 0x0002A116
	internal bool haveOppositeTrait(string pTraitMain)
	{
		return this.haveOppositeTrait(AssetManager.traits.get(pTraitMain));
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0002BF2C File Offset: 0x0002A12C
	internal bool haveOppositeTrait(ActorTrait pTraitMain)
	{
		if (pTraitMain == null)
		{
			return false;
		}
		if (pTraitMain.oppositeArr == null)
		{
			return false;
		}
		for (int i = 0; i < pTraitMain.oppositeArr.Length; i++)
		{
			string pTrait = pTraitMain.oppositeArr[i];
			if (this.haveTrait(pTrait))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0002BF70 File Offset: 0x0002A170
	public bool haveTrait(string pTrait)
	{
		if (this.s_traits_ids.Contains(pTrait))
		{
			return true;
		}
		if (this.statsDirty)
		{
			for (int i = 0; i < this.data.traits.Count; i++)
			{
				if (this.data.traits[i] == pTrait)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0002BFCC File Offset: 0x0002A1CC
	public bool inMapBorder()
	{
		return this.currentPosition.x <= (float)MapBox.width && this.currentPosition.y <= (float)MapBox.height && this.currentPosition.x >= 0f && this.currentPosition.y >= 0f;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0002C028 File Offset: 0x0002A228
	protected virtual void updateForce(float pElapsed)
	{
		if (this.forceVector.x == 0f && this.forceVector.y == 0f && this.forceVector.z == 0f)
		{
			return;
		}
		pElapsed *= 0.6f;
		this._positionDirty = true;
		this.zPosition.y = this.zPosition.y + this.forceVector.z * pElapsed * 40f;
		if (this.forceVector.x != 0f)
		{
			this.currentPosition.x = this.currentPosition.x + this.forceVector.x * pElapsed * 40f;
		}
		if (this.forceVector.y != 0f)
		{
			this.currentPosition.y = this.currentPosition.y + this.forceVector.y * pElapsed * 40f;
		}
		if (this.zPosition.y < 0f)
		{
			this.zPosition.y = 0f;
		}
		this.forceVector.z = this.forceVector.z - pElapsed * 5f;
		if (this.forceVector.z <= 0f && this.zPosition.y <= 0f)
		{
			this.forceVector.z = 0f;
			this.forceVector.x = 0f;
			this.forceVector.y = 0f;
			this._actionFall = true;
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0002C19C File Offset: 0x0002A39C
	protected virtual void actionLanded()
	{
		this.updateAnimation(0f, false);
		this._positionDirty = true;
		this._currentTileDirty = true;
		for (int i = 0; i < this.callbacks_landed.Count; i++)
		{
			this.callbacks_landed[i]((Actor)this);
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0002C1F0 File Offset: 0x0002A3F0
	internal virtual void updateFall(float pElapsed)
	{
		if (this.zPosition.y < 0f)
		{
			return;
		}
		pElapsed *= 0.6f;
		this.zPosition.y = this.zPosition.y - 25f * pElapsed;
		if (this.zPosition.y <= 0f)
		{
			this.zPosition.y = 0f;
			this._actionFall = true;
		}
		this._positionDirty = true;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0002C260 File Offset: 0x0002A460
	protected void updateShake()
	{
		if (this.shakeTimer.isActive)
		{
			if (this.shakeVertical)
			{
				this.shakeOffset.y = Random.Range(-this.shakeVolume, this.shakeVolume);
			}
			if (this.shakeHorizontal)
			{
				this.shakeOffset.x = Random.Range(-this.shakeVolume, this.shakeVolume);
			}
			this._positionDirty = true;
			return;
		}
		if (this.shakeOffset.x != 0f || this.shakeOffset.y != 0f)
		{
			this.shakeOffset.Set(0f, 0f);
			this._positionDirty = true;
		}
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0002C30C File Offset: 0x0002A50C
	protected bool isNearbyTile(WorldTile pTarget)
	{
		return this.currentTile == pTarget || pTarget.neighboursAll.Contains(this.currentTile);
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0002C32C File Offset: 0x0002A52C
	protected void updateFlipRotation(float pElapsed)
	{
		if (!this.stats.flipAnimation)
		{
			return;
		}
		if (this.flip)
		{
			this.flipAngle += pElapsed * 600f;
			if (this.flipAngle > 180f)
			{
				this.flipAngle = 180f;
			}
		}
		else
		{
			this.flipAngle -= pElapsed * 600f;
			if (this.flipAngle < 0f)
			{
				this.flipAngle = 0f;
			}
		}
		if (this.targetAngle.y != this.flipAngle)
		{
			this.targetAngle.y = this.flipAngle;
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
	protected void updateRotationBack(float pElapsed)
	{
		if (this.rotationCooldown > 0f)
		{
			this.rotationCooldown -= pElapsed;
			return;
		}
		if (this.targetAngle.z != 0f)
		{
			if (this.targetAngle.z < 0f)
			{
				this.targetAngle.z = this.targetAngle.z + 100f * pElapsed;
				if (this.targetAngle.z > 0f)
				{
					this.targetAngle.z = 0f;
				}
			}
			if (this.targetAngle.z > 0f)
			{
				this.targetAngle.z = this.targetAngle.z - 100f * pElapsed;
				if (this.targetAngle.z < 0f)
				{
					this.targetAngle.z = 0f;
				}
			}
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0002C4A4 File Offset: 0x0002A6A4
	protected void updateRotation()
	{
		if (this.curAngle.x == this.targetAngle.x && this.curAngle.y == this.targetAngle.y && this.curAngle.z == this.targetAngle.z)
		{
			return;
		}
		this.curAngle.Set(this.targetAngle.x, this.targetAngle.y, this.targetAngle.z);
		this.m_transform.localEulerAngles = this.curAngle;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0002C538 File Offset: 0x0002A738
	protected void updateDeadAnimation()
	{
		if (this.spriteRenderer == null)
		{
			return;
		}
		if (this.stats.specialDeadAnimation)
		{
			return;
		}
		if (this.world.qualityChanger.isFullLowRes())
		{
			((Actor)this).killHimself(true, AttackType.None, false, true);
			return;
		}
		if (this.stats.deathAnimationAngle && this.targetAngle.z < 90f)
		{
			this.targetAngle.z = Mathf.Lerp(this.targetAngle.z, 90f, this.world.elapsed * 4f);
			if (this.targetAngle.z > 90f)
			{
				this.targetAngle.z = 90f;
			}
			if (Mathf.Abs(this.curAngle.z) < 45f)
			{
				return;
			}
		}
		this.changeMoveJumpOffset(-0.05f);
		if (this.isFalling())
		{
			return;
		}
		this.updateDeadBlackAnimation();
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0002C628 File Offset: 0x0002A828
	internal bool isFalling()
	{
		return this.zPosition.y != 0f || this.moveJumpOffset.y != 0f;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0002C654 File Offset: 0x0002A854
	internal void updateDeadBlackAnimation()
	{
		if (Config.test)
		{
			((Actor)this).killHimself(true, AttackType.None, false, true);
			return;
		}
		Color color = this.unit_sprite_color;
		if (color.g > 0f)
		{
			color.r -= 0.5f * this.world.elapsed;
			color.g -= 0.5f * this.world.elapsed;
			color.b -= 0.5f * this.world.elapsed;
			this.setUnitSpriteColor(color);
			return;
		}
		if (color.a > 0f)
		{
			color.a -= 1f * this.world.elapsed;
			this.setUnitSpriteColor(color);
			return;
		}
		((Actor)this).killHimself(true, AttackType.None, false, true);
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0002C728 File Offset: 0x0002A928
	internal void setUnitSpriteColor(Color pColor)
	{
		this.unit_sprite_color = pColor;
		this.spriteRenderer.color = this.unit_sprite_color;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0002C744 File Offset: 0x0002A944
	protected void updateWalkJump(float pElapsed)
	{
		if (!this._is_visible && this.moveJumpOffset.y == 0f)
		{
			return;
		}
		if (this.zPosition.y > 0f)
		{
			return;
		}
		if (this.stats.disableJumpAnimation)
		{
			return;
		}
		if (!this.is_moving)
		{
			if (this.moveJumpOffset.y == 0f && (this.jumpTime == 0f || this.isAffectedByLiquid()))
			{
				return;
			}
		}
		else if ((!this.is_moving && this.jumpTime == 0f) || this.isAffectedByLiquid())
		{
			return;
		}
		this.jumpTime += this.world.elapsed * 6f;
		if (this.jumpTime >= 1f)
		{
			this.changeMoveJumpOffset(-3f * pElapsed);
		}
		else
		{
			this.changeMoveJumpOffset(3f * pElapsed);
		}
		if (this.jumpTime >= 2f)
		{
			this.jumpTime = 0f;
			this.changeMoveJumpOffset(0f);
		}
		if (this.stats.rotatingAnimation)
		{
			this.targetAngle.z = this.targetAngle.z + -this.moveJumpOffset.y * 200f * this.world.elapsed;
		}
		this._positionDirty = true;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0002C886 File Offset: 0x0002AA86
	internal void changeMoveJumpOffset(float pValue)
	{
		this.moveJumpOffset.y = this.moveJumpOffset.y + pValue;
		if (this.moveJumpOffset.y < 0f)
		{
			this.moveJumpOffset.y = 0f;
		}
		this._positionDirty = true;
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0002C8C1 File Offset: 0x0002AAC1
	internal void setCurrentTile(WorldTile pTile)
	{
		this.currentTile = pTile;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0002C8CA File Offset: 0x0002AACA
	internal void setCurrentTilePosition(WorldTile pTile)
	{
		this.setCurrentTile(pTile);
		this.currentPosition.Set(pTile.posV3.x, pTile.posV3.y);
		this._positionDirty = true;
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0002C8FB File Offset: 0x0002AAFB
	internal virtual void spawnOn(WorldTile pTile, float pZHeight = 0f)
	{
		this.setCurrentTilePosition(pTile);
		this.zPosition.y = pZHeight;
		this.hitboxZ = this.stats.defaultZ;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0002C924 File Offset: 0x0002AB24
	internal void findCurrentTile(bool pCheckNeighbours = true)
	{
		if (!this._currentTileDirty && this.currentPosition.x == this.lastX && this.currentPosition.y == this.lastY)
		{
			return;
		}
		this._currentTileDirty = false;
		this.lastX = this.currentPosition.x;
		this.lastY = this.currentPosition.y;
		int num = (int)this.currentPosition.x;
		int num2 = (int)this.currentPosition.y;
		if (num >= MapBox.width)
		{
			num = MapBox.width - 1;
		}
		if (num2 >= MapBox.height)
		{
			num2 = MapBox.height - 1;
		}
		if (num < 0)
		{
			num = 0;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		this.setCurrentTile(this.world.GetTileSimple(num, num2));
		if (Toolbox.Dist(this.currentTile.posV3.x, this.currentTile.posV3.y, this.currentPosition.x, this.currentPosition.y) < 0.5f)
		{
			return;
		}
		if (!pCheckNeighbours)
		{
			return;
		}
		if (this.currentTile.Type.ocean && this.stats.damagedByOcean && !this.stats.flying)
		{
			for (int i = 0; i < this.currentTile.neighboursAll.Count; i++)
			{
				WorldTile worldTile = this.currentTile.neighboursAll[i];
				if (!worldTile.Type.liquid)
				{
					this.currentTile = worldTile;
					return;
				}
			}
			return;
		}
		if (!this.currentTile.Type.liquid && this.stats.dieOnGround)
		{
			for (int j = 0; j < this.currentTile.neighboursAll.Count; j++)
			{
				WorldTile worldTile2 = this.currentTile.neighboursAll[j];
				if (worldTile2.Type.liquid)
				{
					this.currentTile = worldTile2;
					return;
				}
			}
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0002CB04 File Offset: 0x0002AD04
	public void moveTo(WorldTile pTile)
	{
		this._positionDirty = true;
		this.is_moving = true;
		if (this.attackTarget == null && this.currentTile != null && pTile.data.fire && !this.currentTile.data.fire && !this._trait_fire_resistant)
		{
			this.cancelAllBeh(null);
			return;
		}
		this.nextStepTile = pTile;
		if (Toolbox.DistTile(this.currentTile, pTile) > 2f)
		{
			this._currentTileDirty = true;
		}
		else
		{
			this.setCurrentTile(this.nextStepTile);
		}
		if (this.currentTile.Type.stepAction != null && Toolbox.randomChance(pTile.Type.stepActionChance))
		{
			this.currentTile.Type.stepAction(pTile, this);
		}
		Vector3 vector = new Vector3(pTile.posV3.x, pTile.posV3.y);
		this.nextStepPosition = vector;
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0002CBF3 File Offset: 0x0002ADF3
	internal void setFlip(bool pFlip)
	{
		this.flip = pFlip;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0002CBFC File Offset: 0x0002ADFC
	protected void SmoothMovement(Vector2 end, float pElapsed)
	{
		if (this.nextStepPosition.x != Globals.emptyVector.x && this.data.alive)
		{
			this.is_moving = true;
			if (Toolbox.Dist(this.currentPosition.x, this.currentPosition.y, end.x, end.y) > 1E-45f)
			{
				if (this.stats.flipAnimation && this.is_moving)
				{
					if (this.currentPosition.x < end.x)
					{
						this.setFlip(true);
					}
					else
					{
						this.setFlip(false);
					}
				}
				float num;
				if (this.stats.ignoreTileSpeedMod || this.isInAir() || this.stats.oceanCreature)
				{
					num = 1f;
				}
				else if (this.currentTile.Type.liquid && !this.currentTile.Type.liquid)
				{
					num = 1f;
				}
				else
				{
					num = this.currentTile.Type.walkMod;
				}
				if (!this.stats.ignoreTileSpeedMod && this._isInLiquid && this.stats.speedModLiquid != 1f)
				{
					num *= this.stats.speedModLiquid;
				}
				float num2 = num * this.curStats.speed * 0.1f;
				if (num2 < 0.1f)
				{
					num2 = 0.1f;
				}
				num2 *= pElapsed;
				this.currentPosition = Vector2.MoveTowards(this.currentPosition, end, num2);
				this._positionDirty = true;
				return;
			}
		}
		this._positionDirty = true;
		this.is_moving = false;
		if (this.isUsingPath())
		{
			this.updatePathMovement();
		}
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0002CDA2 File Offset: 0x0002AFA2
	internal bool isUsingPath()
	{
		return this.current_path.Count != 0 || this.split_path > SplitPathStatus.Normal;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0002CDBC File Offset: 0x0002AFBC
	public ExecuteEvent goTo(WorldTile pTile, bool pPathOnWater = false, bool pWalkOnBlocks = false)
	{
		this.setTileTarget(pTile);
		return ActorMove.goTo((Actor)this, pTile, pPathOnWater, pWalkOnBlocks);
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
	public void stopMovement()
	{
		this.split_path = SplitPathStatus.Normal;
		this.nextStepTile = null;
		this.current_path_global = null;
		if (this.current_path != null)
		{
			this.current_path.Clear();
		}
		this.setTileTarget(null);
		this.is_moving = false;
		this.nextStepPosition = Globals.emptyVector;
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0002CE24 File Offset: 0x0002B024
	public virtual void updatePathMovement()
	{
		if (this.current_path.Count == 0)
		{
			if (this.split_path != SplitPathStatus.Normal)
			{
				if (this.split_path == SplitPathStatus.Prepare)
				{
					this.split_path = SplitPathStatus.Split;
					this.timer_action = Toolbox.randomFloat(0f, 2f);
					return;
				}
				this.split_path = SplitPathStatus.Normal;
				if (this.tileTarget != null)
				{
					this.goTo(this.tileTarget, false, false);
				}
			}
			return;
		}
		WorldTile worldTile = this.current_path[0];
		this.current_path.RemoveAt(0);
		if (worldTile == null)
		{
			return;
		}
		if (this.stats.isBoat && !worldTile.isGoodForBoat())
		{
			this.callbacks_cancel_path_movement.ForEach(delegate(BaseActionActor tAction)
			{
				tAction((Actor)this);
			});
			this.cancelAllBeh(null);
			return;
		}
		if (worldTile.Type.block)
		{
			BehaviourTaskActor task = this.ai.task;
			if (task != null && !task.moveFromBlock)
			{
				this.cancelAllBeh(null);
				return;
			}
		}
		if (this.stats.dieInLava && worldTile.Type.lava)
		{
			this.cancelAllBeh(null);
			return;
		}
		if (this.stats.damagedByOcean && worldTile.Type.ocean && !this._isInLiquid)
		{
			this.cancelAllBeh(null);
			return;
		}
		if (this.stats.dieOnGround && worldTile.Type.ground)
		{
			this.cancelAllBeh(null);
			return;
		}
		this.moveTo(worldTile);
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0002CF84 File Offset: 0x0002B184
	internal void setTileTarget(WorldTile pTile)
	{
		if (this.tileTarget != null && this.tileTarget.targetedBy == this)
		{
			this.tileTarget.targetedBy = null;
		}
		this.tileTarget = pTile;
		if (this.tileTarget != null)
		{
			this.tileTarget.targetedBy = this;
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
	public static void clearStatusEffects(Actor pActor)
	{
		if (pActor.activeStatusEffects == null)
		{
			return;
		}
		int num = 0;
		for (;;)
		{
			int num2 = num;
			List<ActiveStatusEffect> activeStatusEffects = pActor.activeStatusEffects;
			int? num3 = (activeStatusEffects != null) ? new int?(activeStatusEffects.Count) : null;
			if (!(num2 < num3.GetValueOrDefault() & num3 != null))
			{
				break;
			}
			ActiveStatusEffect activeStatusEffect = pActor.activeStatusEffects[num];
			pActor.removeStatusEffect(activeStatusEffect.asset.id, activeStatusEffect, num);
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0002D044 File Offset: 0x0002B244
	private void OnDrawGizmos()
	{
		if (this.world == null)
		{
			return;
		}
		if (DebugConfig.isOn(DebugOption.ActorGizmosCurrentPosition) && this.currentTile != null)
		{
			Debug.DrawLine(this.currentPosition, this.currentTile.posV3, Color.yellow);
		}
		if (DebugConfig.isOn(DebugOption.ActorGizmosAttackTarget) && this.attackTarget != null)
		{
			Debug.DrawLine(this.currentPosition, this.attackTarget.currentPosition, ActorBase.d_colorKillTarget);
		}
		if (DebugConfig.isOn(DebugOption.ActorGizmosTileTarget) && this.tileTarget != null)
		{
			Debug.DrawLine(this.currentPosition, this.tileTarget.posV3, Color.magenta);
		}
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0002D0FE File Offset: 0x0002B2FE
	protected virtual void clearTasks()
	{
		this.insideBuilding = null;
		this._timeout_targets = 0f;
		this.attackTarget = null;
		this.timer_action = 0f;
		this.setTileTarget(null);
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0002D12C File Offset: 0x0002B32C
	public void cancelAllBeh(Actor pActor = null)
	{
		if (this.inventory.resource != string.Empty && this.city != null)
		{
			this.city.data.storage.change(this.inventory.resource, this.inventory.amount);
		}
		this.inventory.empty();
		this.ai.clearBeh();
		this.ai.setTaskBehFinished();
		this.ai.setJob(null);
		this.clearTasks();
		this.stopMovement();
	}

	// Token: 0x04000301 RID: 769
	internal UnitGroup unitGroup;

	// Token: 0x04000302 RID: 770
	internal WorldTile beh_tile_target;

	// Token: 0x04000303 RID: 771
	internal Building beh_building_target;

	// Token: 0x04000304 RID: 772
	internal BaseSimObject beh_actor_target;

	// Token: 0x04000305 RID: 773
	internal Building insideBuilding;

	// Token: 0x04000306 RID: 774
	internal Boat insideBoat;

	// Token: 0x04000307 RID: 775
	public ActorBag inventory = new ActorBag();

	// Token: 0x04000308 RID: 776
	public ActorEquipment equipment;

	// Token: 0x04000309 RID: 777
	internal Building homeBuilding;

	// Token: 0x0400030A RID: 778
	internal BaseSimObject attackedBy;

	// Token: 0x0400030B RID: 779
	[NonSerialized]
	internal Race race;

	// Token: 0x0400030C RID: 780
	internal ActorStatus data;

	// Token: 0x0400030D RID: 781
	internal ProfessionAsset professionAsset;

	// Token: 0x0400030E RID: 782
	public ActorStats stats;

	// Token: 0x0400030F RID: 783
	protected Vector3 tempPosition;

	// Token: 0x04000310 RID: 784
	public Vector3 nextStepPosition = Globals.emptyVector;

	// Token: 0x04000311 RID: 785
	protected Vector2 shakeOffset = new Vector2(0f, 0f);

	// Token: 0x04000312 RID: 786
	public static Vector2 spriteOffset = new Vector2(0.5f, 0.5f);

	// Token: 0x04000313 RID: 787
	public static Vector2 actorCastPos = new Vector3(0f, 13f);

	// Token: 0x04000314 RID: 788
	public Vector2 moveJumpOffset = new Vector3(0f, 0f);

	// Token: 0x04000315 RID: 789
	internal bool is_moving;

	// Token: 0x04000316 RID: 790
	protected WorldTile tileTarget;

	// Token: 0x04000317 RID: 791
	protected WorldTile nextStepTile;

	// Token: 0x04000318 RID: 792
	public SplitPathStatus split_path;

	// Token: 0x04000319 RID: 793
	public List<WorldTile> current_path = new List<WorldTile>();

	// Token: 0x0400031A RID: 794
	public List<MapRegion> current_path_global;

	// Token: 0x0400031B RID: 795
	protected static List<WorldTile> possible_moves = new List<WorldTile>();

	// Token: 0x0400031C RID: 796
	protected static List<TileZone> possible_zones = new List<TileZone>();

	// Token: 0x0400031D RID: 797
	public List<BaseActionActor> callbacks_on_death = new List<BaseActionActor>();

	// Token: 0x0400031E RID: 798
	public List<BaseActionActor> callbacks_landed = new List<BaseActionActor>();

	// Token: 0x0400031F RID: 799
	public List<BaseActionActor> callbacks_added_madness = new List<BaseActionActor>();

	// Token: 0x04000320 RID: 800
	public List<BaseActionActor> callbacks_cancel_path_movement = new List<BaseActionActor>();

	// Token: 0x04000321 RID: 801
	public List<BaseActionActor> callbacks_get_hit = new List<BaseActionActor>();

	// Token: 0x04000322 RID: 802
	internal BaseSimObject attackTarget;

	// Token: 0x04000323 RID: 803
	internal float timer_action;

	// Token: 0x04000324 RID: 804
	internal float timer_jump_animation;

	// Token: 0x04000325 RID: 805
	protected WorldTimer forceTimer;

	// Token: 0x04000326 RID: 806
	internal float hitboxZ;

	// Token: 0x04000327 RID: 807
	protected Vector3 forceVector;

	// Token: 0x04000328 RID: 808
	internal AnimationDataUnit actorAnimationData;

	// Token: 0x04000329 RID: 809
	internal string current_texture;

	// Token: 0x0400032A RID: 810
	protected WorldTimer targetsToIgnoreTimer;

	// Token: 0x0400032B RID: 811
	internal bool flying;

	// Token: 0x0400032C RID: 812
	internal bool _positionDirty;

	// Token: 0x0400032D RID: 813
	internal float attackTimer;

	// Token: 0x0400032E RID: 814
	internal WeaponType s_attackType;

	// Token: 0x0400032F RID: 815
	internal string s_slashType = string.Empty;

	// Token: 0x04000330 RID: 816
	internal string s_weapon_texture = string.Empty;

	// Token: 0x04000331 RID: 817
	internal int id_sprite_head;

	// Token: 0x04000332 RID: 818
	internal int id_sprite_body;

	// Token: 0x04000333 RID: 819
	internal Sprite s_head_sprite;

	// Token: 0x04000334 RID: 820
	internal Sprite s_body_sprite;

	// Token: 0x04000335 RID: 821
	internal Sprite s_item_sprite;

	// Token: 0x04000336 RID: 822
	internal Sprite s_item_sprite_weapon;

	// Token: 0x04000337 RID: 823
	internal bool item_sprite_dirty;

	// Token: 0x04000338 RID: 824
	internal float s_attackSpeed_seconds;

	// Token: 0x04000339 RID: 825
	internal PersonalityAsset s_personality;

	// Token: 0x0400033A RID: 826
	internal HashSet<string> s_traits_ids = new HashSet<string>();

	// Token: 0x0400033B RID: 827
	internal List<ActorTrait> s_special_effect_traits;

	// Token: 0x0400033C RID: 828
	internal bool _status_frozen;

	// Token: 0x0400033D RID: 829
	internal bool _trait_weightless;

	// Token: 0x0400033E RID: 830
	internal bool _trait_peaceful;

	// Token: 0x0400033F RID: 831
	internal bool _trait_fire_resistant;

	// Token: 0x04000340 RID: 832
	internal AiSystemActor ai;

	// Token: 0x04000341 RID: 833
	internal bool hasItem;

	// Token: 0x04000342 RID: 834
	internal bool _isInLiquid;

	// Token: 0x04000343 RID: 835
	public bool _is_visible;

	// Token: 0x04000344 RID: 836
	internal AnimationFrameData frameData;

	// Token: 0x04000345 RID: 837
	private int _last_animation_id = -1;

	// Token: 0x04000346 RID: 838
	internal bool _currentTileDirty = true;

	// Token: 0x04000347 RID: 839
	internal float actorScale = 1f;

	// Token: 0x04000348 RID: 840
	internal float targetScale;

	// Token: 0x04000349 RID: 841
	private string _current_item_texture;

	// Token: 0x0400034A RID: 842
	protected bool _actionFall;

	// Token: 0x0400034B RID: 843
	protected bool shakeHorizontal = true;

	// Token: 0x0400034C RID: 844
	protected bool shakeVertical = true;

	// Token: 0x0400034D RID: 845
	protected WorldTimer shakeTimer = new WorldTimer(0.3f, true);

	// Token: 0x0400034E RID: 846
	protected float shakeVolume = 0.1f;

	// Token: 0x0400034F RID: 847
	internal float rotationCooldown;

	// Token: 0x04000350 RID: 848
	internal Vector3 curAngle;

	// Token: 0x04000351 RID: 849
	internal Vector3 targetAngle;

	// Token: 0x04000352 RID: 850
	internal Color unit_sprite_color = Color.white;

	// Token: 0x04000353 RID: 851
	private float jumpTime;

	// Token: 0x04000354 RID: 852
	private float lastX = -10f;

	// Token: 0x04000355 RID: 853
	private float lastY = -10f;

	// Token: 0x04000356 RID: 854
	public float flipAngle;

	// Token: 0x04000357 RID: 855
	internal bool flip;

	// Token: 0x04000358 RID: 856
	protected Vector3 flipScale = new Vector3(1f, 1f, 1f);

	// Token: 0x04000359 RID: 857
	internal float _timeout_targets;

	// Token: 0x0400035A RID: 858
	private static Color d_colorTileTarget = new Color(0f, 1f, 1f, 0.3f);

	// Token: 0x0400035B RID: 859
	private static Color d_colorTilePath = new Color(1f, 0.5f, 0.5f, 0.9f);

	// Token: 0x0400035C RID: 860
	private static Color d_colorKillTarget = new Color(1f, 0f, 0f, 1f);

	// Token: 0x0400035D RID: 861
	private static Color d_colorHomeCity = new Color(1f, 1f, 0f, 0.5f);

	// Token: 0x0400035E RID: 862
	private static Color d_colorCurrentTile = new Color(1f, 0f, 0f, 1f);

	// Token: 0x0400035F RID: 863
	public bool isGroupLeader;
}
