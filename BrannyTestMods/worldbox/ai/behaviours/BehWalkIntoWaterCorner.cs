using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000394 RID: 916
	public class BehWalkIntoWaterCorner : BehaviourActionActor
	{
		// Token: 0x060013C8 RID: 5064 RVA: 0x000A3EC0 File Offset: 0x000A20C0
		public override BehResult execute(Actor pActor)
		{
			if (BehWalkIntoWaterCorner.isGoodIsland(pActor.currentTile.region.island, pActor))
			{
				return BehResult.Stop;
			}
			WorldTile worldTile = null;
			float num = 0f;
			TileIsland island = pActor.currentTile.region.island;
			for (int i = 0; i < island.regionsCorners.Count; i++)
			{
				List<WorldTile> tileCorners = island.regionsCorners[i].getTileCorners();
				for (int j = 0; j < tileCorners.Count; j++)
				{
					WorldTile worldTile2 = tileCorners[j];
					if (worldTile2.Type.ocean)
					{
						float num2 = Toolbox.DistTile(pActor.currentTile, worldTile2);
						if (worldTile == null || num2 < num)
						{
							worldTile = worldTile2;
							num = num2;
						}
					}
				}
			}
			if (worldTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = worldTile;
			return BehResult.Continue;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000A3F8C File Offset: 0x000A218C
		private static bool isGoodIsland(TileIsland pIsland, Actor pActor)
		{
			return pIsland.type != TileLayerType.Block && pIsland.type != TileLayerType.Ocean && pIsland.type != TileLayerType.Swamp && (pIsland.type != TileLayerType.Lava || !pActor.stats.dieInLava) && pIsland.getTileCount() > 5;
		}
	}
}
