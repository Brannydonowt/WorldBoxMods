using System;

namespace ai.behaviours
{
	// Token: 0x020003B0 RID: 944
	public class KingdomCheckDeadKingdom : BehaviourActionKingdom
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x000A9ECB File Offset: 0x000A80CB
		public override BehResult execute(Kingdom pKingdom)
		{
			if (pKingdom.timer_no_city > 200f)
			{
				MapBox.instance.kingdoms.destroyKingdom(pKingdom);
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
