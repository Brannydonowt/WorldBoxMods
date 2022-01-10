using System;

namespace ai.behaviours
{
	// Token: 0x02000322 RID: 802
	public class BehAnimalFindTile : BehaviourActionActor
	{
		// Token: 0x060012A3 RID: 4771 RVA: 0x0009F38C File Offset: 0x0009D58C
		public override BehResult execute(Actor pActor)
		{
			if (Toolbox.randomChance(0.8f))
			{
				Toolbox.findSameUnitInChunkAround(pActor.currentTile.chunk, "druid");
				if (Toolbox.temp_list_units.Count != 0)
				{
					Actor random = Toolbox.temp_list_units.GetRandom<Actor>();
					pActor.beh_tile_target = random.currentTile.region.tiles.GetRandom<WorldTile>();
					return BehResult.Continue;
				}
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
