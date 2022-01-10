using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000390 RID: 912
	public class BehTaxiEmbark : BehCity
	{
		// Token: 0x060013B9 RID: 5049 RVA: 0x000A3949 File Offset: 0x000A1B49
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x000A3958 File Offset: 0x000A1B58
		public override BehResult execute(Actor pActor)
		{
			TaxiRequest requestForActor = TaxiManager.getRequestForActor(pActor);
			if (requestForActor == null || requestForActor.taxi == null || requestForActor.state != TaxiRequestState.Loading)
			{
				return BehResult.Stop;
			}
			Boat component = requestForActor.taxi.actor.GetComponent<Boat>();
			bool flag = component.isNearDock();
			if (Toolbox.DistTile(component.actor.currentTile, pActor.currentTile) < 5f || flag)
			{
				pActor.beh_tile_target = null;
				pActor.embarkInto(requestForActor.taxi);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
