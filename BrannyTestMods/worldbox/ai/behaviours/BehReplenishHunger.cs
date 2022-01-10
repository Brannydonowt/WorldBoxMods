using System;

namespace ai.behaviours
{
	// Token: 0x02000383 RID: 899
	public class BehReplenishHunger : BehaviourActionActor
	{
		// Token: 0x06001398 RID: 5016 RVA: 0x000A3017 File Offset: 0x000A1217
		public override BehResult execute(Actor pActor)
		{
			pActor.restoreStatsFromEating(pActor.stats.maxHunger, 0.1f, false);
			return BehResult.Continue;
		}
	}
}
