using System;

namespace ai.behaviours.conditions
{
	// Token: 0x020003B6 RID: 950
	public class CondHaveUnitGroup : BehaviourActorCondition
	{
		// Token: 0x0600143C RID: 5180 RVA: 0x000AA921 File Offset: 0x000A8B21
		public override bool check(Actor pActor)
		{
			if (pActor.unitGroup == null || !pActor.unitGroup.isAlive())
			{
				pActor.unitGroup = null;
				return false;
			}
			return true;
		}
	}
}
