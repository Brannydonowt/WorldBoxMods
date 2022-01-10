using System;

namespace ai.behaviours
{
	// Token: 0x0200034E RID: 846
	public class BehCityGetRandomZoneTile : BehCity
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x000A07C0 File Offset: 0x0009E9C0
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.zones.Count == 0)
			{
				return BehResult.Stop;
			}
			if (pActor.city.zones.Count == 0)
			{
				return BehResult.Stop;
			}
			WorldTile random;
			if (pActor.currentTile.zone.city != pActor.city || Toolbox.randomChance(0.2f))
			{
				random = pActor.city.zones.GetRandom<TileZone>().tiles.GetRandom<WorldTile>();
				if (!random.isSameIsland(pActor.currentTile))
				{
					return BehResult.Stop;
				}
			}
			else
			{
				random = pActor.currentTile.region.tiles.GetRandom<WorldTile>();
			}
			pActor.beh_tile_target = random;
			return BehResult.Continue;
		}
	}
}
