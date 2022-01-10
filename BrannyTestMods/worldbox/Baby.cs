using System;

// Token: 0x02000130 RID: 304
public class Baby : BaseActorComponent
{
	// Token: 0x06000702 RID: 1794 RVA: 0x000501A7 File Offset: 0x0004E3A7
	internal override void create()
	{
		base.create();
		this.timerGrow = (float)this.actor.stats.timeToGrow;
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x000501C8 File Offset: 0x0004E3C8
	public override void update(float pElapsed)
	{
		if (!this.actor.data.alive)
		{
			return;
		}
		if (this.world.isPaused())
		{
			return;
		}
		base.update(pElapsed);
		this.timerGrow -= pElapsed;
		if (this.timerGrow <= 0f)
		{
			if (this.actor.stats.unit)
			{
				"unit_" + this.actor.race.id;
			}
			Actor actor = this.world.createNewUnit(this.actor.stats.growIntoID, this.actor.currentTile, null, 0f, null);
			actor.startBabymakingTimeout();
			actor.data.hunger = actor.stats.maxHunger / 2;
			this.world.gameStats.data.creaturesBorn--;
			if (this.actor.stats.unit)
			{
				if (this.actor.city != null)
				{
					this.actor.city.addNewUnit(actor, true, true);
				}
				actor.setKingdom(this.actor.kingdom);
			}
			actor.data.diplomacy = this.actor.data.diplomacy;
			actor.data.intelligence = this.actor.data.intelligence;
			actor.data.stewardship = this.actor.data.stewardship;
			actor.data.warfare = this.actor.data.warfare;
			actor.data.culture = this.actor.data.culture;
			actor.data.experience = this.actor.data.experience;
			actor.data.level = this.actor.data.level;
			actor.data.firstName = this.actor.data.firstName;
			if (this.actor.data.skin != -1)
			{
				actor.data.skin = this.actor.data.skin;
			}
			if (this.actor.data.skin_set != -1)
			{
				actor.data.skin_set = this.actor.data.skin_set;
			}
			actor.data.age = this.actor.data.age;
			actor.data.bornTime = this.actor.data.bornTime;
			actor.data.health = this.actor.data.health;
			actor.data.gender = this.actor.data.gender;
			actor.data.kills = this.actor.data.kills;
			foreach (string text in this.actor.data.traits)
			{
				if (!(text == "peaceful"))
				{
					actor.addTrait(text);
				}
			}
			if (this.actor.data.favorite)
			{
				actor.data.favorite = true;
			}
			if (Config.spectatorMode && MoveCamera.focusUnit == this.actor)
			{
				MoveCamera.focusUnit = actor;
			}
			this.actor.killHimself(true, AttackType.GrowUp, false, false);
		}
	}

	// Token: 0x04000956 RID: 2390
	public float timerGrow;
}
