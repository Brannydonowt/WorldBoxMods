using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000352 RID: 850
	public class BehCityWarriorTaxiCheck : BehCity
	{
		// Token: 0x06001313 RID: 4883 RVA: 0x000A09E0 File Offset: 0x0009EBE0
		public override BehResult execute(Actor pActor)
		{
			City city = pActor.city;
			if (((city != null) ? city.attackZone : null) != null && !pActor.city.attackZone.centerTile.isSameIsland(pActor.currentTile))
			{
				TaxiManager.newRequest(pActor, pActor.city.attackZone.centerTile);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
