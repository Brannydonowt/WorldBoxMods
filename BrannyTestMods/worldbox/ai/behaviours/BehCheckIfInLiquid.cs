using System;

namespace ai.behaviours
{
	// Token: 0x02000343 RID: 835
	public class BehCheckIfInLiquid : BehaviourActionActor
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x000A0373 File Offset: 0x0009E573
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isInLiquid())
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
