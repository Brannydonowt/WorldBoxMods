using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000338 RID: 824
	public class BehBoatTransportDoLoading : BehBoat
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x0009FBA0 File Offset: 0x0009DDA0
		public override BehResult execute(Actor pActor)
		{
			Boat boat = base.getBoat(pActor);
			if (boat.taxiRequest == null)
			{
				boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			bool flag = true;
			if (boat.passengerWaitCounter > 4 || boat.unitsInside.Count >= 100)
			{
				flag = false;
			}
			else if (boat.taxiRequest.everyoneEmbarked())
			{
				flag = false;
			}
			if (flag)
			{
				boat.taxiRequest.setState(TaxiRequestState.Loading);
				pActor.timer_action = 12f;
				boat.passengerWaitCounter++;
				return BehResult.RepeatStep;
			}
			if (boat.unitsInside.Count == 0)
			{
				boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			boat.taxiRequest.cancelForLatePassengers();
			boat.taxiTarget = boat.taxiRequest.target;
			return BehResult.Continue;
		}
	}
}
