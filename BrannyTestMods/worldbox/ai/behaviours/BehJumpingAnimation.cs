using System;

namespace ai.behaviours
{
	// Token: 0x02000376 RID: 886
	public class BehJumpingAnimation : BehaviourActionActor
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x000A285A File Offset: 0x000A0A5A
		public BehJumpingAnimation(float pTimerAction)
		{
			this.timer_action = pTimerAction;
			this.null_check_tile_target = true;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000A2870 File Offset: 0x000A0A70
		public override void create()
		{
			base.create();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000A2878 File Offset: 0x000A0A78
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_jump_animation = this.timer_action;
			pActor.timer_action = this.timer_action;
			return BehResult.Continue;
		}

		// Token: 0x04001535 RID: 5429
		private float timer_action;
	}
}
