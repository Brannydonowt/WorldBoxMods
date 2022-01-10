using System;

namespace ai.behaviours
{
	// Token: 0x02000331 RID: 817
	public class BehBoatFindTargetForTrade : BehBoat
	{
		// Token: 0x060012C6 RID: 4806 RVA: 0x0009F914 File Offset: 0x0009DB14
		public override BehResult execute(Actor pActor)
		{
			Docks dockTradeTarget = ActorTool.getDockTradeTarget(pActor);
			if (dockTradeTarget != null)
			{
				pActor.beh_building_target = dockTradeTarget.building;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
