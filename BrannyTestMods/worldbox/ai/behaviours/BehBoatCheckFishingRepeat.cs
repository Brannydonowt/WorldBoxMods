using System;

namespace ai.behaviours
{
	// Token: 0x0200032A RID: 810
	public class BehBoatCheckFishingRepeat : BehaviourActionActor
	{
		// Token: 0x060012B8 RID: 4792 RVA: 0x0009F70F File Offset: 0x0009D90F
		public override BehResult execute(Actor pActor)
		{
			if (pActor.inventory.getResource("fish") <= 10)
			{
				return BehResult.RestartTask;
			}
			return base.forceTask(pActor, "boat_return_to_dock", true);
		}
	}
}
