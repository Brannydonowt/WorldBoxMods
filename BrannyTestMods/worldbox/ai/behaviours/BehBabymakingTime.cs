using System;

namespace ai.behaviours
{
	// Token: 0x02000324 RID: 804
	public class BehBabymakingTime : BehaviourActionActor
	{
		// Token: 0x060012A8 RID: 4776 RVA: 0x0009F493 File Offset: 0x0009D693
		public override void create()
		{
			base.create();
			this.null_check_actor_target = true;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0009F4A4 File Offset: 0x0009D6A4
		public override BehResult execute(Actor pActor)
		{
			if (Toolbox.DistTile(pActor.currentTile, pActor.beh_actor_target.currentTile) > 1f)
			{
				return BehResult.Stop;
			}
			pActor.beh_actor_target.a.startShake(0.3f, 0.1f, true, true);
			pActor.startShake(0.3f, 0.1f, true, true);
			pActor.beh_actor_target.a.timer_action = 2f;
			return BehResult.Continue;
		}
	}
}
