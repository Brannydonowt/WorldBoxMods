using System;

namespace ai.behaviours
{
	// Token: 0x0200032D RID: 813
	public class BehBoatDamageCheck : BehActive
	{
		// Token: 0x060012BE RID: 4798 RVA: 0x0009F77A File Offset: 0x0009D97A
		public override BehResult execute(Actor pActor)
		{
			if ((float)pActor.data.health < (float)pActor.curStats.health * 0.8f)
			{
				pActor.cancelAllBeh(null);
				pActor.stopMovement();
				return base.forceTask(pActor, "boat_return_to_dock", true);
			}
			return BehResult.Continue;
		}
	}
}
