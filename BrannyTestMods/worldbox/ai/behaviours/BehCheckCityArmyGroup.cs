using System;

namespace ai.behaviours
{
	// Token: 0x02000340 RID: 832
	public class BehCheckCityArmyGroup : BehaviourActionActor
	{
		// Token: 0x060012EA RID: 4842 RVA: 0x000A00B5 File Offset: 0x0009E2B5
		public override void create()
		{
			base.create();
			this.null_check_city = true;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000A00C4 File Offset: 0x0009E2C4
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.army == null)
			{
				return BehResult.Stop;
			}
			UnitGroup unitGroup = pActor.unitGroup;
			if (unitGroup != null && unitGroup.isAlive())
			{
				return BehResult.Stop;
			}
			if (!pActor.kingdom.isCiv())
			{
				pActor.unitGroup = null;
			}
			if (pActor.city.army != null)
			{
				pActor.city.army.addUnit(pActor);
			}
			return BehResult.Continue;
		}
	}
}
