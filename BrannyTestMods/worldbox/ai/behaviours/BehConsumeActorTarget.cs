using System;

namespace ai.behaviours
{
	// Token: 0x02000353 RID: 851
	public class BehConsumeActorTarget : BehaviourActionActor
	{
		// Token: 0x06001315 RID: 4885 RVA: 0x000A0A3F File Offset: 0x0009EC3F
		public override void create()
		{
			base.create();
			this.null_check_actor_target = true;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000A0A50 File Offset: 0x0009EC50
		public override BehResult execute(Actor pActor)
		{
			if (Toolbox.DistTile(pActor.currentTile, pActor.beh_actor_target.currentTile) > 1f)
			{
				return base.forceTask(pActor, "hunting_attack", false);
			}
			this.consume(pActor, pActor.beh_actor_target.a);
			if (pActor.beh_actor_target.a.data.alive)
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000A0AB4 File Offset: 0x0009ECB4
		private void consume(Actor pMain, Actor pTarget)
		{
			pMain.timer_action = 0.3f;
			if (pMain.transform.localPosition.x > pTarget.transform.localPosition.x)
			{
				if (pTarget.targetAngle.z > -45f)
				{
					pTarget.targetAngle.z = pTarget.targetAngle.z - BehaviourActionBase<Actor>.world.elapsed * 100f;
					if (pTarget.targetAngle.z < -90f)
					{
						pTarget.targetAngle.z = -90f;
					}
					pTarget.rotationCooldown = 1f;
				}
			}
			else if (pTarget.targetAngle.z < 45f)
			{
				pTarget.targetAngle.z = pTarget.targetAngle.z + BehaviourActionBase<Actor>.world.elapsed * 100f;
				pTarget.rotationCooldown = 1f;
			}
			if (pMain.targetAngle.z == 0f)
			{
				pMain.punchTargetAnimation(pTarget.transform.localPosition, pMain.currentTile, false, false, 40f);
				pTarget.getHit((float)pMain.curStats.damage, true, AttackType.Eaten, pMain, false);
				pTarget.startShake(0.2f, 0.1f, true, true);
				pMain.restoreStatsFromEating(pMain.curStats.damage, 0.1f, false);
			}
			pTarget.cancelAllBeh(null);
			pTarget.timer_action = Toolbox.randomFloat(1f, 2f);
		}
	}
}
