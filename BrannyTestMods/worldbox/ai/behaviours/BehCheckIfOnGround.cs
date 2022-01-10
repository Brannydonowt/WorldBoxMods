using System;

namespace ai.behaviours
{
	// Token: 0x02000344 RID: 836
	public class BehCheckIfOnGround : BehaviourActionActor
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x000A0388 File Offset: 0x0009E588
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isInLiquid())
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
