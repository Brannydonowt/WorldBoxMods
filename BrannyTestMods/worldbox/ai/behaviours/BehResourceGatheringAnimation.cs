using System;

namespace ai.behaviours
{
	// Token: 0x02000384 RID: 900
	public class BehResourceGatheringAnimation : BehaviourActionActor
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x000A3039 File Offset: 0x000A1239
		public BehResourceGatheringAnimation(float pTimerAction)
		{
			this.timer_action = pTimerAction;
			this.null_check_building_target = true;
			this.check_building_target_non_usable = true;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000A3056 File Offset: 0x000A1256
		public override void create()
		{
			base.create();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000A3060 File Offset: 0x000A1260
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_building_target.stats.type == "fruits" && !pActor.beh_building_target.haveResources)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target.getConstructionTile();
			pActor.punchTargetAnimation(pActor.beh_building_target.currentTile.posV3, pActor.beh_building_target.currentTile, true, false, 40f);
			pActor.beh_building_target.resourceGathering(BehaviourActionBase<Actor>.world.elapsed);
			pActor.timer_action = this.timer_action;
			return BehResult.Continue;
		}

		// Token: 0x04001539 RID: 5433
		private float timer_action;
	}
}
