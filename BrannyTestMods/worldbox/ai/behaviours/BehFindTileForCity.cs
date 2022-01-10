using System;

namespace ai.behaviours
{
	// Token: 0x0200036A RID: 874
	public class BehFindTileForCity : BehaviourActionActor
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x000A1E8C File Offset: 0x000A008C
		public override BehResult execute(Actor pActor)
		{
			if (BehaviourActionBase<Actor>.world.cityPlaceFinder.zones.Count == 0)
			{
				return BehResult.Stop;
			}
			TileZone tileZone = null;
			float num = 100000f;
			BehaviourActionBase<Actor>.world.cityPlaceFinder.zones.ShuffleOne<TileZone>();
			TileZone tileZone2 = null;
			for (int i = 0; i < BehaviourActionBase<Actor>.world.cityPlaceFinder.zones.Count; i++)
			{
				TileZone tileZone3 = BehaviourActionBase<Actor>.world.cityPlaceFinder.zones[i];
				if (tileZone3.goodForNewCity && tileZone3.tiles[0].isSameIsland(pActor.currentTile))
				{
					tileZone2 = tileZone3;
					Kingdom kingdom = pActor.kingdom;
					if (((kingdom != null) ? kingdom.capital : null) == null)
					{
						pActor.beh_tile_target = Toolbox.getRandom<WorldTile>(tileZone2.tiles);
						return BehResult.Continue;
					}
					float num2 = Toolbox.DistVec3(pActor.kingdom.capital.cityCenter, tileZone3.centerTile.posV3);
					if (num > num2)
					{
						num = num2;
						tileZone = tileZone3;
					}
				}
			}
			if (tileZone != null)
			{
				pActor.beh_tile_target = Toolbox.getRandom<WorldTile>(tileZone.tiles);
				return BehResult.Continue;
			}
			if (tileZone2 != null)
			{
				pActor.beh_tile_target = Toolbox.getRandom<WorldTile>(tileZone2.tiles);
				return BehResult.Continue;
			}
			TileIsland randomIslandGround = BehaviourActionBase<Actor>.world.islandsCalculator.getRandomIslandGround(true);
			if (randomIslandGround == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = randomIslandGround.getRandomTile();
			return BehResult.Continue;
		}
	}
}
