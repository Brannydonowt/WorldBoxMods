using System;

namespace ai.behaviours
{
	// Token: 0x02000348 RID: 840
	public class BehCityCheckAttack : BehCity
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x000A0458 File Offset: 0x0009E658
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.attackZone == null)
			{
				return BehResult.Stop;
			}
			if (pActor.city.attackZone.centerTile.isSameIsland(pActor.currentTile))
			{
				pActor.beh_tile_target = pActor.city.attackZone.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
