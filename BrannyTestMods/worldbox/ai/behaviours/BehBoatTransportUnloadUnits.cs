using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x0200033B RID: 827
	public class BehBoatTransportUnloadUnits : BehBoat
	{
		// Token: 0x060012DD RID: 4829 RVA: 0x0009FD8C File Offset: 0x0009DF8C
		public override BehResult execute(Actor pActor)
		{
			Boat boat = base.getBoat(pActor);
			WorldTile taxiTarget = boat.taxiTarget;
			WorldTile taxiTarget2 = boat.taxiTarget;
			WorldTile pTile = PathfinderTools.raycastTileForUnitLandingFromOcean(pActor.currentTile, boat.taxiTarget);
			boat.unloadUnits(pTile, false);
			if (boat.taxiRequest != null)
			{
				TaxiManager.finish(boat.taxiRequest);
				boat.taxiRequest = null;
				boat.cancelWork(pActor);
			}
			return BehResult.Continue;
		}
	}
}
