using System;

namespace ai.behaviours
{
	// Token: 0x0200036C RID: 876
	public class BehFindTileNearbyGroupLeader : BehaviourActionActor
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x000A2294 File Offset: 0x000A0494
		public override BehResult execute(Actor pActor)
		{
			if (pActor.unitGroup == null)
			{
				return BehResult.Stop;
			}
			if (pActor.unitGroup.groupLeader != null)
			{
				WorldTile random = pActor.unitGroup.groupLeader.currentTile.region.tiles.GetRandom<WorldTile>();
				pActor.beh_tile_target = random;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
