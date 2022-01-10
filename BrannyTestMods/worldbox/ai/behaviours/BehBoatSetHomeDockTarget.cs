using System;

namespace ai.behaviours
{
	// Token: 0x02000336 RID: 822
	public class BehBoatSetHomeDockTarget : BehBoat
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x0009FABA File Offset: 0x0009DCBA
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			if (pActor.homeBuilding == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target = pActor.homeBuilding;
			return BehResult.Continue;
		}
	}
}
