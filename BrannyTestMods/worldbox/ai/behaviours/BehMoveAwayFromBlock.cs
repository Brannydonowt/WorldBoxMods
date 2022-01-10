using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200037C RID: 892
	public class BehMoveAwayFromBlock : BehaviourActionActor
	{
		// Token: 0x06001386 RID: 4998 RVA: 0x000A2C10 File Offset: 0x000A0E10
		public override BehResult execute(Actor pActor)
		{
			if (pActor.currentTile.region.island.type != TileLayerType.Block)
			{
				return BehResult.Stop;
			}
			WorldTile worldTile = null;
			float num = 0f;
			TileIsland island = pActor.currentTile.region.island;
			for (int i = 0; i < island.regionsCorners.Count; i++)
			{
				MapRegion mapRegion = island.regionsCorners[i];
				if (mapRegion.getTileCorners().Count != 0)
				{
					List<WorldTile> tileCorners = mapRegion.getTileCorners();
					for (int j = 0; j < tileCorners.Count; j++)
					{
						WorldTile worldTile2 = tileCorners[j];
						if (BehMoveAwayFromBlock.isGoodTileRegion(worldTile2.region, pActor) && worldTile2.region.island != island && worldTile2.region.island.getTileCount() >= 5)
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
			}
			if (worldTile != null)
			{
				pActor.beh_tile_target = worldTile;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000A2D16 File Offset: 0x000A0F16
		private static bool isGoodTileRegion(MapRegion pRegion, Actor pActor)
		{
			return (pRegion.type != TileLayerType.Ocean || !pActor.stats.damagedByOcean) && (pRegion.type != TileLayerType.Lava || !pActor.stats.dieInLava);
		}
	}
}
