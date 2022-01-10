using System;

// Token: 0x0200013C RID: 316
public class HoverModule : BaseActorComponent
{
	// Token: 0x06000756 RID: 1878 RVA: 0x000538C8 File Offset: 0x00051AC8
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (!this.actor.data.alive)
		{
			this.actor.changeMoveJumpOffset(-pElapsed * 10f);
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = 1f + Toolbox.randomFloat(0f, 4f);
		if (this.world.isPaused())
		{
			return;
		}
		switch (this.hoverState)
		{
		case HoverState.Hover:
			if (Toolbox.randomBool())
			{
				this.hoverState = HoverState.Down;
				return;
			}
			this.hoverState = HoverState.Up;
			return;
		case HoverState.Up:
			this.hoverState = HoverState.Hover;
			if (this.actor.moveJumpOffset.y < this.actor.stats.hovering_max)
			{
				this.actor.changeMoveJumpOffset(pElapsed * 3f);
				return;
			}
			break;
		case HoverState.Down:
			this.hoverState = HoverState.Hover;
			if (this.actor.moveJumpOffset.y > this.actor.stats.hovering_min)
			{
				this.actor.changeMoveJumpOffset(-pElapsed * 3f);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x040009C0 RID: 2496
	internal HoverState hoverState;

	// Token: 0x040009C1 RID: 2497
	private float timer;
}
