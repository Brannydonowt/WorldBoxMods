using System;

namespace ai.behaviours.conditions
{
	// Token: 0x020003B9 RID: 953
	public class CondNotGroupLeader : BehaviourActorCondition
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x000AA9D6 File Offset: 0x000A8BD6
		public override bool check(Actor pActor)
		{
			return pActor.kingdom.isCiv() && (pActor.unitGroup != null && pActor.unitGroup.groupLeader != pActor);
		}
	}
}
