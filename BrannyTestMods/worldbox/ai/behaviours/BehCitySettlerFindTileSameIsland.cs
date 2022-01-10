using System;

namespace ai.behaviours
{
	// Token: 0x02000350 RID: 848
	public class BehCitySettlerFindTileSameIsland : BehCity
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x000A0910 File Offset: 0x0009EB10
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.settleTarget == null)
			{
				return BehResult.Stop;
			}
			if (!BehaviourActionBase<Actor>.world.worldLaws.world_law_kingdom_expansion.boolVal)
			{
				return BehResult.Stop;
			}
			if (pActor.currentTile.isSameIsland(pActor.city.settleTarget.centerTile))
			{
				pActor.beh_tile_target = pActor.city.settleTarget.centerTile;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
