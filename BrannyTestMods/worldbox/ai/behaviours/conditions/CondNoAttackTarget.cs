using System;

namespace ai.behaviours.conditions
{
	// Token: 0x020003B7 RID: 951
	public class CondNoAttackTarget : BehaviourActorCondition
	{
		// Token: 0x0600143E RID: 5182 RVA: 0x000AA94C File Offset: 0x000A8B4C
		public override bool check(Actor pActor)
		{
			return pActor.kingdom.isCiv() && pActor.unitGroup != null && !(pActor.unitGroup.groupLeader != pActor) && !(pActor.city == null) && pActor.city.attackZone == null;
		}
	}
}
