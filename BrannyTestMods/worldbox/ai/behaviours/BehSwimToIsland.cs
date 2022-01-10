using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200038E RID: 910
	public class BehSwimToIsland : BehaviourActionActor
	{
		// Token: 0x060013B1 RID: 5041 RVA: 0x000A3590 File Offset: 0x000A1790
		public override BehResult execute(Actor pActor)
		{
			if (BehSwimToIsland.isGoodIsland(pActor.currentTile.region.island, pActor))
			{
				return BehResult.Stop;
			}
			BehSwimToIsland.best_region = null;
			BehSwimToIsland.dist = 0f;
			BehSwimToIsland.best_dist = 0f;
			TileIsland island = pActor.currentTile.region.island;
			BehSwimToIsland.best_region = BehSwimToIsland.findIslandNearby(pActor);
			if (BehSwimToIsland.best_region != null)
			{
				pActor.beh_tile_target = BehSwimToIsland.best_region.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			for (int i = 0; i < BehaviourActionBase<Actor>.world.islandsCalculator.islands.Count; i++)
			{
				TileIsland tileIsland = BehaviourActionBase<Actor>.world.islandsCalculator.islands[i];
				if (BehSwimToIsland.checkIsland(island, tileIsland, pActor))
				{
					MapRegion random = tileIsland.regionsCorners.GetRandom<MapRegion>();
					if (random.getTileCorners().Count != 0)
					{
						BehSwimToIsland.dist = Toolbox.DistTile(pActor.currentTile, random.getTileCorners().GetRandom<WorldTile>());
						if (BehSwimToIsland.best_region == null || BehSwimToIsland.dist < BehSwimToIsland.best_dist)
						{
							BehSwimToIsland.best_region = random;
							BehSwimToIsland.best_dist = BehSwimToIsland.dist;
						}
					}
				}
			}
			if (Toolbox.randomChance(0.8f) && BehSwimToIsland.best_region != null)
			{
				pActor.beh_tile_target = BehSwimToIsland.best_region.tiles.GetRandom<WorldTile>();
			}
			else
			{
				MapRegion mapRegion;
				if (Toolbox.randomChance(0.5f))
				{
					mapRegion = pActor.currentTile.region;
				}
				else
				{
					mapRegion = Toolbox.getRandom<MapRegion>(pActor.currentTile.region.neighbours);
				}
				if (mapRegion != null)
				{
					pActor.beh_tile_target = Toolbox.getRandom<WorldTile>(mapRegion.tiles);
				}
			}
			return BehResult.Continue;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000A3718 File Offset: 0x000A1918
		private static MapRegion findIslandNearby(Actor pActor)
		{
			List<MapChunk> allChunksFromTile = Toolbox.getAllChunksFromTile(pActor.currentTile);
			TileIsland island = pActor.currentTile.region.island;
			for (int i = 0; i < allChunksFromTile.Count; i++)
			{
				MapChunk mapChunk = allChunksFromTile[i];
				for (int j = 0; j < mapChunk.regions.Count; j++)
				{
					MapRegion mapRegion = mapChunk.regions[j];
					if (BehSwimToIsland.checkIsland(island, mapRegion.island, pActor) && mapRegion.getTileCorners().Count != 0)
					{
						BehSwimToIsland.dist = Toolbox.DistTile(pActor.currentTile, mapRegion.getTileCorners().GetRandom<WorldTile>());
						if (BehSwimToIsland.best_region == null || BehSwimToIsland.dist < BehSwimToIsland.best_dist)
						{
							BehSwimToIsland.best_region = mapRegion;
							BehSwimToIsland.best_dist = BehSwimToIsland.dist;
						}
					}
				}
			}
			return BehSwimToIsland.best_region;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000A37F0 File Offset: 0x000A19F0
		private static bool checkIsland(TileIsland pCurrentIsland, TileIsland pIsland, Actor pActor)
		{
			if (pCurrentIsland == pIsland)
			{
				return false;
			}
			if (!BehSwimToIsland.isGoodIsland(pIsland, pActor))
			{
				return false;
			}
			bool flag;
			if (pCurrentIsland.getTileCount() > pIsland.getTileCount())
			{
				flag = pIsland.connectedWith(pCurrentIsland);
			}
			else
			{
				flag = pCurrentIsland.connectedWith(pIsland);
			}
			return flag && pIsland.regionsCorners.Count != 0;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000A3843 File Offset: 0x000A1A43
		private static bool isGoodIsland(TileIsland pIsland, Actor pActor)
		{
			return pIsland.type != TileLayerType.Block && pIsland.type != TileLayerType.Ocean && pIsland.type != TileLayerType.Lava && pIsland.type != TileLayerType.Swamp && pIsland.getTileCount() > 5;
		}

		// Token: 0x04001546 RID: 5446
		private static MapRegion best_region;

		// Token: 0x04001547 RID: 5447
		private static float dist;

		// Token: 0x04001548 RID: 5448
		private static float best_dist;
	}
}
