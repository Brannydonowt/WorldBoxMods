using System;

namespace ai.behaviours
{
	// Token: 0x0200039D RID: 925
	public class CityBehCheckAttackZone : BehaviourActionCity
	{
		// Token: 0x060013ED RID: 5101 RVA: 0x000A78B8 File Offset: 0x000A5AB8
		public override BehResult execute(City pCity)
		{
			if (pCity.attackZone != null && (pCity.attackZone.city == null || !pCity.attackZone.city.alive || !pCity.attackZone.city.kingdom.isEnemy(pCity.kingdom)))
			{
				pCity.attackZone = null;
			}
			if (pCity.attackZone != null)
			{
				return BehResult.Continue;
			}
			if (pCity.kingdom.target_kingdom != null)
			{
				pCity.kingdom.checkTarget();
				if (pCity.kingdom.target_city != null)
				{
					if (pCity.kingdom.target_city.buildings.Count > 0)
					{
						Building random = pCity.kingdom.target_city.buildings.GetRandom();
						pCity.attackZone = random.currentTile.zone;
					}
				}
				else
				{
					pCity.kingdom.clearTarget();
				}
			}
			else
			{
				pCity.attackZone = null;
			}
			return BehResult.Continue;
		}
	}
}
