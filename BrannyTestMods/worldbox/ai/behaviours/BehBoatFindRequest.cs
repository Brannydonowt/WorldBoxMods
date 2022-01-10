using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000330 RID: 816
	public class BehBoatFindRequest : BehBoat
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x0009F8C8 File Offset: 0x0009DAC8
		public override BehResult execute(Actor pActor)
		{
			Boat boat = base.getBoat(pActor);
			boat.taxiRequest = TaxiManager.getNewRequestForBoat(pActor);
			if (boat.taxiRequest == null)
			{
				return BehResult.Stop;
			}
			boat.taxiRequest.assign(boat);
			return base.forceTask(pActor, "boat_transport_go_load", true);
		}
	}
}
