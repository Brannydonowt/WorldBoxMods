using System;
using tools;

namespace ai.behaviours
{
	// Token: 0x0200033A RID: 826
	public class BehBoatTransportFindTileUnload : BehBoat
	{
		// Token: 0x060012DB RID: 4827 RVA: 0x0009FD3C File Offset: 0x0009DF3C
		public override BehResult execute(Actor pActor)
		{
			Boat boat = base.getBoat(pActor);
			if (boat.taxiTarget == null)
			{
				return BehResult.Stop;
			}
			WorldTile worldTile = OceanHelper.findTileForBoat(pActor.currentTile, boat.taxiTarget);
			if (worldTile == null)
			{
				boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			pActor.beh_tile_target = worldTile;
			return BehResult.Continue;
		}
	}
}
