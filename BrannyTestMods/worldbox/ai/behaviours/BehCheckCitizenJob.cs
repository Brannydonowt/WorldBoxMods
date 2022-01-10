using System;

namespace ai.behaviours
{
	// Token: 0x0200033F RID: 831
	public class BehCheckCitizenJob : BehaviourActionActor
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x000A008A File Offset: 0x0009E28A
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city != null)
			{
				pActor.ai.setJob("citizen");
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
