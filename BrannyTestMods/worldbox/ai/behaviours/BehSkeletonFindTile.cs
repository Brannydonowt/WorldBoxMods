using System;

namespace ai.behaviours
{
	// Token: 0x0200038A RID: 906
	public class BehSkeletonFindTile : BehaviourActionActor
	{
		// Token: 0x060013A5 RID: 5029 RVA: 0x000A31C0 File Offset: 0x000A13C0
		public override BehResult execute(Actor pActor)
		{
			Toolbox.findSameUnitInChunkAround(pActor.currentTile.chunk, "necromancer");
			if (Toolbox.temp_list_units.Count != 0)
			{
				Actor random = Toolbox.temp_list_units.GetRandom<Actor>();
				pActor.beh_tile_target = random.currentTile.region.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			MapRegion mapRegion = pActor.currentTile.region;
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
