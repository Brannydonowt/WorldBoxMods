using System;

namespace ai.behaviours
{
	// Token: 0x0200035E RID: 862
	public class BehFindConstructionTile : BehaviourActionActor
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x000A12A7 File Offset: 0x0009F4A7
		public override void create()
		{
			base.create();
			this.null_check_building_target = true;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000A12B6 File Offset: 0x0009F4B6
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_tile_target = pActor.beh_building_target.getConstructionTile();
			return BehResult.Continue;
		}
	}
}
