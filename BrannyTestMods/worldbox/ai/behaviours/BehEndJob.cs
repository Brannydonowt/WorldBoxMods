using System;

namespace ai.behaviours
{
	// Token: 0x02000359 RID: 857
	public class BehEndJob : BehaviourActionActor
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x000A0F11 File Offset: 0x0009F111
		public override BehResult execute(Actor pActor)
		{
			pActor.ai.setJob(null);
			return BehResult.Continue;
		}
	}
}
