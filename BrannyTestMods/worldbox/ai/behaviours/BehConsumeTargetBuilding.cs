using System;

namespace ai.behaviours
{
	// Token: 0x02000356 RID: 854
	public class BehConsumeTargetBuilding : BehaviourActionActor
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x000A0D2E File Offset: 0x0009EF2E
		public override void create()
		{
			base.create();
			this.null_check_building_target = true;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000A0D40 File Offset: 0x0009EF40
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_building_target.stats.type == "fruits")
			{
				if (pActor.beh_building_target.haveResources)
				{
					pActor.beh_building_target.extractResources(pActor, 1);
					pActor.restoreStatsFromEating(100, 0.1f, false);
				}
			}
			else if (pActor.beh_building_target.stats.type == "crops" && pActor.beh_building_target.data.alive)
			{
				pActor.beh_building_target.startDestroyBuilding(true);
				pActor.restoreStatsFromEating(100, 0.1f, false);
			}
			WorldTile currentTile = pActor.beh_building_target.currentTile;
			pActor.punchTargetAnimation(currentTile.posV3, currentTile, false, false, 40f);
			return BehResult.Continue;
		}
	}
}
