using System;

namespace ai.behaviours
{
	// Token: 0x02000355 RID: 853
	public class BehConsumeGrass : BehaviourActionActor
	{
		// Token: 0x0600131C RID: 4892 RVA: 0x000A0CBB File Offset: 0x0009EEBB
		public override void create()
		{
			base.create();
			this.null_check_tile_target = true;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000A0CCC File Offset: 0x0009EECC
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.beh_tile_target.Type.grass)
			{
				return BehResult.Stop;
			}
			pActor.punchTargetAnimation(pActor.beh_tile_target.posV3, pActor.beh_tile_target, false, false, 40f);
			pActor.restoreStatsFromEating(100, 0.1f, false);
			pActor.beh_tile_target.eatGreens(5);
			return BehResult.Continue;
		}
	}
}
