using System;

namespace ai.behaviours.conditions
{
	// Token: 0x020003B4 RID: 948
	public class CondGroupLeader : BehaviourActorCondition
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x000AA889 File Offset: 0x000A8A89
		public override bool check(Actor pActor)
		{
			return pActor.kingdom.isCiv() && (pActor.unitGroup != null && pActor.unitGroup.groupLeader == pActor);
		}
	}
}
