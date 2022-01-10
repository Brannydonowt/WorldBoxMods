using System;

namespace ai.behaviours
{
	// Token: 0x02000323 RID: 803
	public class BehAttackActorTarget : BehaviourActionActor
	{
		// Token: 0x060012A5 RID: 4773 RVA: 0x0009F445 File Offset: 0x0009D645
		public override void create()
		{
			base.create();
			this.null_check_actor_target = true;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0009F454 File Offset: 0x0009D654
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isInAttackRange(pActor.beh_actor_target))
			{
				pActor.tryToAttack(pActor.beh_actor_target);
			}
			if (pActor.beh_actor_target.a.data.alive)
			{
				return BehResult.RestartTask;
			}
			return BehResult.Continue;
		}
	}
}
