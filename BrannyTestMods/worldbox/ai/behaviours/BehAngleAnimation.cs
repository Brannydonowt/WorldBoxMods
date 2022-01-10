using System;

namespace ai.behaviours
{
	// Token: 0x02000320 RID: 800
	public class BehAngleAnimation : BehaviourActionActor
	{
		// Token: 0x0600129E RID: 4766 RVA: 0x0009F270 File Offset: 0x0009D470
		public BehAngleAnimation(string pTarget, float pTimerAction = 0f, float pAngle = 40f)
		{
			this.angle = pAngle;
			this.target = pTarget;
			this.timer_action = pTimerAction;
			if (this.target == "tile_target")
			{
				this.null_check_tile_target = true;
				return;
			}
			if (this.target == "building_target")
			{
				this.null_check_building_target = true;
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0009F2CB File Offset: 0x0009D4CB
		public override void create()
		{
			base.create();
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0009F2D4 File Offset: 0x0009D4D4
		public override BehResult execute(Actor pActor)
		{
			if (this.target == "tile_target")
			{
				pActor.punchTargetAnimation(pActor.beh_tile_target.posV3, pActor.beh_tile_target, false, false, this.angle);
			}
			else if (this.target == "building_target")
			{
				pActor.punchTargetAnimation(pActor.beh_building_target.currentTile.posV3, pActor.beh_building_target.currentTile, true, false, this.angle);
				pActor.beh_building_target.startShake(0.3f);
			}
			pActor.timer_action = this.timer_action;
			return BehResult.Continue;
		}

		// Token: 0x04001523 RID: 5411
		private string target;

		// Token: 0x04001524 RID: 5412
		private float timer_action;

		// Token: 0x04001525 RID: 5413
		private float angle;
	}
}
