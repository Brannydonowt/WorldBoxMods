using System;

namespace ai.behaviours
{
	// Token: 0x02000354 RID: 852
	public class BehConsumeFlowers : BehaviourActionActor
	{
		// Token: 0x06001319 RID: 4889 RVA: 0x000A0C20 File Offset: 0x0009EE20
		public override void create()
		{
			base.create();
			this.null_check_tile_target = true;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000A0C30 File Offset: 0x0009EE30
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_tile_target.building == null)
			{
				return BehResult.Stop;
			}
			if (pActor.beh_tile_target.building.stats.type != "flower")
			{
				return BehResult.Stop;
			}
			pActor.punchTargetAnimation(pActor.beh_tile_target.posV3, pActor.beh_tile_target, false, false, 40f);
			pActor.restoreStatsFromEating(100, 0.1f, false);
			pActor.beh_tile_target.building.startRemove(false);
			return BehResult.Continue;
		}
	}
}
