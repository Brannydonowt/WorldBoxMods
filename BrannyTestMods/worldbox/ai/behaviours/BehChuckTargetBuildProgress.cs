using System;

namespace ai.behaviours
{
	// Token: 0x0200033C RID: 828
	public class BehChuckTargetBuildProgress : BehaviourActionActor
	{
		// Token: 0x060012DF RID: 4831 RVA: 0x0009FDF3 File Offset: 0x0009DFF3
		public override void create()
		{
			base.create();
			this.check_building_target_non_usable = true;
			this.null_check_city = true;
			this.null_check_tile_target = true;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0009FE10 File Offset: 0x0009E010
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_building_target.data.underConstruction)
			{
				return BehResult.RestartTask;
			}
			return BehResult.Continue;
		}
	}
}
