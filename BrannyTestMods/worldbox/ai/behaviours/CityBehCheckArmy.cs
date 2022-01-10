using System;

namespace ai.behaviours
{
	// Token: 0x0200039C RID: 924
	public class CityBehCheckArmy : BehaviourActionCity
	{
		// Token: 0x060013EB RID: 5099 RVA: 0x000A783C File Offset: 0x000A5A3C
		public override BehResult execute(City pCity)
		{
			Culture culture = pCity.getCulture();
			if (culture == null)
			{
				return BehResult.Continue;
			}
			if (!culture.haveTech("weapon_production"))
			{
				return BehResult.Continue;
			}
			if (pCity.getArmy() == 0)
			{
				return BehResult.Continue;
			}
			if (pCity.army != null && !pCity.army.isAlive())
			{
				pCity.army = null;
			}
			if (pCity.army != null)
			{
				return BehResult.Continue;
			}
			UnitGroup army = BehaviourActionBase<City>.world.unitGroupManager.createNewGroup(pCity);
			pCity.army = army;
			return BehResult.Continue;
		}
	}
}
