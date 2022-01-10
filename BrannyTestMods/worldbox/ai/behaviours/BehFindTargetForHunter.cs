using System;

namespace ai.behaviours
{
	// Token: 0x02000367 RID: 871
	public class BehFindTargetForHunter : BehCity
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x000A1AA0 File Offset: 0x0009FCA0
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_actor_target != null && this.isTargetOk(pActor, pActor.beh_actor_target.a))
			{
				return BehResult.Continue;
			}
			pActor.beh_actor_target = this.getClosestMeatActor(pActor, 3, false);
			if (pActor.beh_actor_target != null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000A1AF4 File Offset: 0x0009FCF4
		private Actor getClosestMeatActor(Actor pActor, int pMinAge = 0, bool pCheckSame = false)
		{
			BehaviourActionActor.temp_actors.Clear();
			BehaviourActionBase<Actor>.world.getObjectsInChunks(pActor.currentTile, 10, MapObjectType.Actor);
			for (int i = 0; i < BehaviourActionBase<Actor>.world.temp_map_objects.Count; i++)
			{
				Actor actor = (Actor)BehaviourActionBase<Actor>.world.temp_map_objects[i];
				if (this.isTargetOk(pActor, actor) && actor.stats.source_meat && (pMinAge <= 0 || actor.data.age >= pMinAge))
				{
					BehaviourActionActor.temp_actors.Add(actor);
				}
			}
			return Toolbox.getClosestActor(BehaviourActionActor.temp_actors, pActor.currentTile);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000A1B92 File Offset: 0x0009FD92
		private bool isTargetOk(Actor pActor, Actor pTarget)
		{
			return !(pTarget == pActor) && pActor.canAttackTarget(pTarget) && pTarget.currentTile.isSameIsland(pActor.currentTile);
		}
	}
}
