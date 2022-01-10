using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000369 RID: 873
	public class BehFindTileBeach : BehaviourActionActor
	{
		// Token: 0x06001354 RID: 4948 RVA: 0x000A1D9C File Offset: 0x0009FF9C
		public override BehResult execute(Actor pActor)
		{
			BehaviourActionActor.possible_moves.Clear();
			this.findCornersInRegion(pActor.currentTile.region);
			if (BehaviourActionActor.possible_moves.Count == 0)
			{
				for (int i = 0; i < pActor.currentTile.region.neighbours.Count; i++)
				{
					MapRegion pRegion = pActor.currentTile.region.neighbours[i];
					this.findCornersInRegion(pRegion);
				}
			}
			if (BehaviourActionActor.possible_moves.Count == 0)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = BehaviourActionActor.possible_moves.GetRandom<WorldTile>();
			return BehResult.Continue;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000A1E30 File Offset: 0x000A0030
		private void findCornersInRegion(MapRegion pRegion)
		{
			pRegion.getTileCorners().ShuffleOne<WorldTile>();
			List<WorldTile> tileCorners = pRegion.getTileCorners();
			for (int i = 0; i < tileCorners.Count; i++)
			{
				WorldTile worldTile = tileCorners[i];
				if (worldTile.Type.ocean)
				{
					BehaviourActionActor.possible_moves.Add(worldTile);
					return;
				}
			}
		}
	}
}
