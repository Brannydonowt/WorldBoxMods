using System;
using tools;

namespace ai.behaviours
{
	// Token: 0x02000339 RID: 825
	public class BehBoatTransportFindTilePickUp : BehBoat
	{
		// Token: 0x060012D9 RID: 4825 RVA: 0x0009FC58 File Offset: 0x0009DE58
		public override BehResult execute(Actor pActor)
		{
			Boat boat = base.getBoat(pActor);
			if (boat.taxiRequest == null || !boat.taxiRequest.isStillLegit())
			{
				boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			boat.pickup_near_dock = false;
			ActorTool.checkHomeDocks(pActor);
			if (pActor.homeBuilding != null)
			{
				WorldTile oceanTileInSameOcean = pActor.homeBuilding.GetComponent<Docks>().getOceanTileInSameOcean(pActor.currentTile);
				if (oceanTileInSameOcean != null && oceanTileInSameOcean.isSameIsland(boat.taxiRequest.requestTile))
				{
					boat.pickup_near_dock = true;
					if (boat.isNearDock())
					{
						boat.passengerWaitCounter = 0;
						pActor.beh_tile_target = pActor.currentTile;
						return BehResult.Continue;
					}
					pActor.beh_tile_target = oceanTileInSameOcean;
					return BehResult.Continue;
				}
			}
			WorldTile worldTile = OceanHelper.findTileForBoat(pActor.currentTile, boat.taxiRequest.requestTile);
			if (worldTile == null)
			{
				boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			boat.passengerWaitCounter = 0;
			pActor.beh_tile_target = worldTile;
			return BehResult.Continue;
		}
	}
}
