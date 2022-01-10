using System;

namespace ai.behaviours
{
	// Token: 0x02000364 RID: 868
	public class BehFindRandomFrontBuildingTile : BehaviourActionActor
	{
		// Token: 0x06001346 RID: 4934 RVA: 0x000A18FC File Offset: 0x0009FAFC
		public override void create()
		{
			base.create();
			this.null_check_building_target = true;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000A190B File Offset: 0x0009FB0B
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_tile_target = pActor.beh_building_target.currentTile;
			return BehResult.Continue;
		}
	}
}
