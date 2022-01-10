using System;

namespace ai.behaviours
{
	// Token: 0x02000375 RID: 885
	public class BehJoinCity : BehaviourActionActor
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x000A27AC File Offset: 0x000A09AC
		public override BehResult execute(Actor pActor)
		{
			City city = pActor.currentTile.zone.city;
			if (city == null)
			{
				return BehResult.Stop;
			}
			if (city == pActor.city)
			{
				return BehResult.Stop;
			}
			if (city.kingdom == pActor.kingdom || (pActor.kingdom.isNomads() && pActor.race == city.race))
			{
				City city2 = null;
				if (pActor.city != null)
				{
					city2 = pActor.city;
					pActor.city.removeCitizen(pActor, false, AttackType.Other);
					pActor.removeFromCity();
				}
				pActor.becomeCitizen(city);
				if (city2 != null)
				{
					city2.checkSettleTarget();
				}
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
