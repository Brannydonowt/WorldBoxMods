using System;

namespace ai.behaviours
{
	// Token: 0x0200032C RID: 812
	public class BehBoatCollectFish : BehaviourActionActor
	{
		// Token: 0x060012BC RID: 4796 RVA: 0x0009F75E File Offset: 0x0009D95E
		public override BehResult execute(Actor pActor)
		{
			pActor.inventory.add("fish", 1);
			return BehResult.Continue;
		}
	}
}
