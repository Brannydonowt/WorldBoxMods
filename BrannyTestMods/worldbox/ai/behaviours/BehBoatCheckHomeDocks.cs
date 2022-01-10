using System;

namespace ai.behaviours
{
	// Token: 0x0200032B RID: 811
	public class BehBoatCheckHomeDocks : BehBoat
	{
		// Token: 0x060012BA RID: 4794 RVA: 0x0009F73C File Offset: 0x0009D93C
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			if (pActor.homeBuilding == null)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
