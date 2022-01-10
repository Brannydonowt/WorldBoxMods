using System;

// Token: 0x02000139 RID: 313
public class Egg : BaseActorComponent
{
	// Token: 0x0600074D RID: 1869 RVA: 0x000531CB File Offset: 0x000513CB
	internal override void create()
	{
		base.create();
		this.timerEgg = 100f;
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000531E0 File Offset: 0x000513E0
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.world.isPaused())
		{
			return;
		}
		this.timerEgg -= pElapsed;
		if (this.timerEgg <= 0f)
		{
			Actor actor = this.world.createNewUnit(this.actor.stats.growIntoID, this.actor.currentTile, null, 2f, null);
			actor.data.firstName = this.actor.data.firstName;
			actor.data.favorite = this.actor.data.favorite;
			actor.justBorn();
			actor.data.hunger = actor.stats.maxHunger / 2;
			this.world.gameStats.data.creaturesBorn++;
			if (Config.spectatorMode && MoveCamera.focusUnit == this.actor)
			{
				MoveCamera.focusUnit = actor;
			}
			actor.data.skin_set = this.actor.data.skin_set;
			this.actor.killHimself(true, AttackType.None, false, true);
		}
	}

	// Token: 0x040009AB RID: 2475
	private float timerEgg;
}
