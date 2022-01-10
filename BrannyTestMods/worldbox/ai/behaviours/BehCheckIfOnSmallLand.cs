using System;

namespace ai.behaviours
{
	// Token: 0x02000345 RID: 837
	public class BehCheckIfOnSmallLand : BehaviourActionActor
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x000A039D File Offset: 0x0009E59D
		public override BehResult execute(Actor pActor)
		{
			if (pActor.currentTile.region.island.getTileCount() > 5)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
