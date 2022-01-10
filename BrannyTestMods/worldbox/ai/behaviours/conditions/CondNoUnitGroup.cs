using System;

namespace ai.behaviours.conditions
{
	// Token: 0x020003B8 RID: 952
	public class CondNoUnitGroup : BehaviourActorCondition
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x000AA9AD File Offset: 0x000A8BAD
		public override bool check(Actor pActor)
		{
			if (pActor.unitGroup == null || !pActor.unitGroup.isAlive())
			{
				pActor.unitGroup = null;
				return true;
			}
			return false;
		}
	}
}
