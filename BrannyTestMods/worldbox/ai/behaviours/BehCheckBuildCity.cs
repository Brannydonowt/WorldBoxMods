using System;

namespace ai.behaviours
{
	// Token: 0x0200033E RID: 830
	public class BehCheckBuildCity : BehaviourActionActor
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x0009FF94 File Offset: 0x0009E194
		public override BehResult execute(Actor pActor)
		{
			Kingdom kingdom = pActor.kingdom;
			if (!kingdom.isNomads())
			{
				if (!kingdom.isCiv())
				{
					return BehResult.Stop;
				}
				if (!BehaviourActionBase<Actor>.world.worldLaws.world_law_kingdom_expansion.boolVal)
				{
					return BehResult.Stop;
				}
			}
			City city = pActor.currentTile.zone.city;
			TileZone zone = pActor.currentTile.zone;
			if (!zone.goodForNewCity)
			{
				return BehResult.Stop;
			}
			if (kingdom != null && kingdom.isNomads())
			{
				kingdom = null;
			}
			city = BehaviourActionBase<Actor>.world.buildNewCity(zone, null, pActor.race, false, kingdom);
			if (city == null)
			{
				return BehResult.Stop;
			}
			city.newCityEvent();
			city.race = pActor.race;
			city.setCulture(pActor.data.culture);
			City city2 = pActor.city;
			if (city2 != null)
			{
				city2.kingdom.newCityBuiltEvent(city);
				city2.removeCitizen(pActor, false, AttackType.Other);
				pActor.removeFromCity();
			}
			pActor.becomeCitizen(city);
			WorldLog.logNewCity(city);
			return BehResult.Continue;
		}
	}
}
