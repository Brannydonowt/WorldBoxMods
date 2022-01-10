using System;

namespace ai.behaviours
{
	// Token: 0x02000326 RID: 806
	public class BehBeeCheckReturnHome : BehaviourActionActor
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x0009F596 File Offset: 0x0009D796
		public override BehResult execute(Actor pActor)
		{
			if (pActor.homeBuilding != null)
			{
				pActor.beh_building_target = pActor.homeBuilding;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
