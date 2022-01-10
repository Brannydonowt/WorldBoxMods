using System;

namespace ai.behaviours
{
	// Token: 0x02000380 RID: 896
	public class BehRandomWait : BehaviourActionActor
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x000A2EF8 File Offset: 0x000A10F8
		public BehRandomWait(float pMin = 0f, float pMax = 1f)
		{
			this.min = pMin;
			this.max = pMax;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000A2F0E File Offset: 0x000A110E
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_action = Toolbox.randomFloat(this.min, this.max);
			return BehResult.Continue;
		}

		// Token: 0x04001537 RID: 5431
		private float min;

		// Token: 0x04001538 RID: 5432
		private float max;
	}
}
