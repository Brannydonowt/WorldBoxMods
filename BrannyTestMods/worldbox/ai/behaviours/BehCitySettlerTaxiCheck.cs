using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000351 RID: 849
	public class BehCitySettlerTaxiCheck : BehCity
	{
		// Token: 0x06001311 RID: 4881 RVA: 0x000A0984 File Offset: 0x0009EB84
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.settleTarget == null)
			{
				return BehResult.Stop;
			}
			if (!pActor.city.settleTarget.centerTile.isSameIsland(pActor.currentTile))
			{
				TaxiManager.newRequest(pActor, pActor.city.settleTarget.centerTile);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
