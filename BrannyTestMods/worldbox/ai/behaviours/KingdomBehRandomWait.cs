using System;

namespace ai.behaviours
{
	// Token: 0x020003AE RID: 942
	public class KingdomBehRandomWait : BehaviourActionKingdom
	{
		// Token: 0x06001424 RID: 5156 RVA: 0x000A9D4D File Offset: 0x000A7F4D
		public KingdomBehRandomWait(float pMin = 0f, float pMax = 1f)
		{
			this.min = pMin;
			this.max = pMax;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000A9D63 File Offset: 0x000A7F63
		public override BehResult execute(Kingdom pKingdom)
		{
			pKingdom.timer_action = Toolbox.randomFloat(this.min, this.max);
			return BehResult.Continue;
		}

		// Token: 0x04001571 RID: 5489
		private float min;

		// Token: 0x04001572 RID: 5490
		private float max;
	}
}
