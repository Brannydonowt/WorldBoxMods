using System;

namespace ai.behaviours
{
	// Token: 0x0200032E RID: 814
	public class BehBoatDangerCheck : BehActive
	{
		// Token: 0x060012C0 RID: 4800 RVA: 0x0009F7C0 File Offset: 0x0009D9C0
		public override BehResult execute(Actor pActor)
		{
			if (!(pActor.attackedBy != null))
			{
				return BehResult.Stop;
			}
			if ((float)pActor.data.health < (float)pActor.curStats.health * 0.25f)
			{
				pActor.cancelAllBeh(null);
				pActor.stopMovement();
				return base.forceTask(pActor, "boat_return_to_dock", true);
			}
			return BehResult.Continue;
		}
	}
}
