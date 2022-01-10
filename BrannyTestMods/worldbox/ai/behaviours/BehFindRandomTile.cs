using System;

namespace ai.behaviours
{
	// Token: 0x02000365 RID: 869
	public class BehFindRandomTile : BehaviourActionActor
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x000A1928 File Offset: 0x0009FB28
		public override BehResult execute(Actor pActor)
		{
			MapRegion mapRegion = pActor.currentTile.region;
			if (Toolbox.randomChance(0.65f) && mapRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = mapRegion.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			if (mapRegion.neighbours.Count > 0 && Toolbox.randomBool())
			{
				mapRegion = mapRegion.neighbours.GetRandom<MapRegion>();
			}
			if (mapRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = mapRegion.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
