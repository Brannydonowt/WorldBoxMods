using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x0200038F RID: 911
	public class BehTaxiCheck : BehCity
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x000A3888 File Offset: 0x000A1A88
		public override BehResult execute(Actor pActor)
		{
			WorldTile tile = pActor.city.getTile();
			if (tile == null)
			{
				return BehResult.Stop;
			}
			bool flag = false;
			if (pActor.isCurrentJob("attacker"))
			{
				if (!pActor.currentTile.isSameIsland(tile) && (pActor.city.attackZone == null || !pActor.city.attackZone.centerTile.isSameIsland(pActor.currentTile)))
				{
					flag = true;
				}
			}
			else if (pActor.isCurrentJob("settler"))
			{
				City city = pActor.city;
				if (((city != null) ? city.settleTarget : null) == null && !tile.isSameIsland(pActor.currentTile))
				{
					flag = true;
				}
			}
			else if (!pActor.currentTile.isSameIsland(tile))
			{
				flag = true;
			}
			if (!flag)
			{
				return BehResult.Stop;
			}
			TaxiManager.newRequest(pActor, tile);
			return BehResult.Continue;
		}
	}
}
