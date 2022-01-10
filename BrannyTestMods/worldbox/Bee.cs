using System;

// Token: 0x020000B6 RID: 182
public class Bee : BaseActorComponent
{
	// Token: 0x060003A6 RID: 934 RVA: 0x00038AE2 File Offset: 0x00036CE2
	internal override void create()
	{
		base.create();
		this.hoverModule = base.GetComponent<HoverModule>();
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00038AF8 File Offset: 0x00036CF8
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (!this.actor.data.alive)
		{
			return;
		}
		if (!this.actor.is_moving && this.actor.ai.task != null && this.actor.ai.task.id == "pollinate")
		{
			this.hoverModule.hoverState = HoverState.Down;
			this.actor.changeMoveJumpOffset(-pElapsed * 3f);
			return;
		}
		this.hoverModule.hoverState = HoverState.Up;
		if (this.actor.moveJumpOffset.y < this.actor.stats.hovering_max)
		{
			this.actor.changeMoveJumpOffset(pElapsed * 3f);
		}
	}

	// Token: 0x04000602 RID: 1538
	public int pollen;

	// Token: 0x04000603 RID: 1539
	private HoverModule hoverModule;
}
