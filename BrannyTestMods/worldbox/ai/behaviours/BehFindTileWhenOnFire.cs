using System;

namespace ai.behaviours
{
	// Token: 0x0200036D RID: 877
	public class BehFindTileWhenOnFire : BehaviourActionActor
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x000A22F0 File Offset: 0x000A04F0
		public override BehResult execute(Actor pActor)
		{
			WorldTile worldTile = this.findWaterIn(pActor.currentTile.chunk);
			if (worldTile == null)
			{
				foreach (MapChunk pChunk in pActor.currentTile.chunk.neighboursAll)
				{
					worldTile = this.findWaterIn(pChunk);
					if (worldTile != null)
					{
						break;
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

		// Token: 0x0600135E RID: 4958 RVA: 0x000A2378 File Offset: 0x000A0578
		private WorldTile findWaterIn(MapChunk pChunk)
		{
			for (int i = 0; i < pChunk.regions.Count; i++)
			{
				pChunk.regions.ShuffleOne(i);
				MapRegion mapRegion = pChunk.regions[i];
				if (mapRegion.type == TileLayerType.Ocean || mapRegion.type == TileLayerType.Swamp)
				{
					return mapRegion.tiles.GetRandom<WorldTile>();
				}
			}
			return null;
		}
	}
}
