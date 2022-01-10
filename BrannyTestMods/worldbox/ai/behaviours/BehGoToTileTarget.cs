using System;

namespace ai.behaviours
{
	// Token: 0x02000373 RID: 883
	public class BehGoToTileTarget : BehaviourActionActor
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x000A26DC File Offset: 0x000A08DC
		public override void create()
		{
			base.create();
			this.null_check_tile_target = true;
			this.walkOnWater = false;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000A26F2 File Offset: 0x000A08F2
		public override BehResult execute(Actor pActor)
		{
			if (pActor.goTo(pActor.beh_tile_target, this.walkOnWater, this.walkOnBlocks) == ExecuteEvent.False)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}

		// Token: 0x04001533 RID: 5427
		public bool walkOnWater;

		// Token: 0x04001534 RID: 5428
		public bool walkOnBlocks;
	}
}
