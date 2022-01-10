using System;

namespace ai.behaviours
{
	// Token: 0x0200035C RID: 860
	public class BehFightCheckEnemyIsOk : BehaviourActionActor
	{
		// Token: 0x06001330 RID: 4912 RVA: 0x000A0FDA File Offset: 0x0009F1DA
		public override void create()
		{
			base.create();
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000A0FE4 File Offset: 0x0009F1E4
		public override BehResult execute(Actor pActor)
		{
			if (pActor.attackTarget == null || !pActor.attackTarget.base_data.alive)
			{
				pActor.attackTarget = null;
				return BehResult.Stop;
			}
			if (!pActor.kingdom.isEnemy(pActor.attackTarget.kingdom))
			{
				pActor.attackTarget = null;
				return BehResult.Stop;
			}
			if (!pActor.canAttackTarget(pActor.attackTarget))
			{
				pActor.targetsToIgnore.Add(pActor.attackTarget);
				pActor.attackTarget = null;
				return BehResult.Stop;
			}
			if (!pActor.isInAttackRange(pActor.attackTarget))
			{
				if (pActor.stats.oceanCreature)
				{
					if ((!pActor.attackTarget.isInLiquid() && !pActor.stats.landCreature) || pActor.attackTarget.isInAir())
					{
						pActor.targetsToIgnore.Add(pActor.attackTarget);
						pActor.attackTarget = null;
						return BehResult.Stop;
					}
				}
				else if ((pActor.attackTarget.isInLiquid() && !pActor.stats.oceanCreature) || pActor.attackTarget.isInAir())
				{
					pActor.targetsToIgnore.Add(pActor.attackTarget);
					pActor.attackTarget = null;
					return BehResult.Stop;
				}
			}
			if (Toolbox.Dist((float)pActor.currentTile.chunk.x, (float)pActor.currentTile.chunk.y, (float)pActor.attackTarget.currentTile.chunk.x, (float)pActor.attackTarget.currentTile.chunk.y) > 2f && !pActor.currentTile.chunk.neighboursAll.Contains(pActor.attackTarget.currentTile.chunk))
			{
				pActor.attackTarget = null;
				return BehResult.Stop;
			}
			pActor.beh_actor_target = pActor.attackTarget;
			return BehResult.Continue;
		}
	}
}
