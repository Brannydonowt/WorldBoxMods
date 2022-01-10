using System;

namespace ai.behaviours.conditions
{
	// Token: 0x020003B5 RID: 949
	public class CondHaveAttackTarget : BehaviourActorCondition
	{
		// Token: 0x0600143A RID: 5178 RVA: 0x000AA8C0 File Offset: 0x000A8AC0
		public override bool check(Actor pActor)
		{
			return pActor.kingdom.isCiv() && pActor.unitGroup != null && !(pActor.unitGroup.groupLeader != pActor) && !(pActor.city == null) && pActor.city.attackZone != null;
		}
	}
}
