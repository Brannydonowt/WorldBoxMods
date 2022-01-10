using System;

namespace ai.behaviours
{
	// Token: 0x02000362 RID: 866
	public class BehFindMeatSource : BehaviourActionActor
	{
		// Token: 0x06001340 RID: 4928 RVA: 0x000A15F8 File Offset: 0x0009F7F8
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.stats.diet_meat && !pActor.stats.diet_meat_insect)
			{
				return BehResult.Stop;
			}
			if (pActor.beh_actor_target != null && this.isTargetOk(pActor, pActor.beh_actor_target.a))
			{
				return BehResult.Continue;
			}
			pActor.beh_actor_target = this.getClosestMeatActor(pActor, 0, false);
			if (pActor.beh_actor_target && pActor.data.hunger < 20)
			{
				pActor.beh_actor_target = this.getClosestMeatActor(pActor, 0, true);
			}
			if (pActor.beh_actor_target != null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000A1690 File Offset: 0x0009F890
		private Actor getClosestMeatActor(Actor pActor, int pMinAge = 0, bool pCheckSame = false)
		{
			BehaviourActionActor.temp_actors.Clear();
			BehaviourActionBase<Actor>.world.getObjectsInChunks(pActor.currentTile, 10, MapObjectType.Actor);
			for (int i = 0; i < BehaviourActionBase<Actor>.world.temp_map_objects.Count; i++)
			{
				Actor actor = (Actor)BehaviourActionBase<Actor>.world.temp_map_objects[i];
				if (this.isTargetOk(pActor, actor))
				{
					if (pActor.stats.diet_meat)
					{
						if (!actor.stats.source_meat)
						{
							goto IL_BB;
						}
					}
					else if (pActor.stats.diet_meat_insect && !actor.stats.source_meat_insect)
					{
						goto IL_BB;
					}
					if (pCheckSame)
					{
						if (!pActor.stats.diet_meat_same_race && actor.isRace(pActor))
						{
							goto IL_BB;
						}
					}
					else if (actor.isRace(pActor))
					{
						goto IL_BB;
					}
					if (pMinAge <= 0 || actor.data.age >= pMinAge)
					{
						BehaviourActionActor.temp_actors.Add(actor);
					}
				}
				IL_BB:;
			}
			return Toolbox.getClosestActor(BehaviourActionActor.temp_actors, pActor.currentTile);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000A1784 File Offset: 0x0009F984
		private bool isTargetOk(Actor pActor, Actor pTarget)
		{
			return !(pTarget == pActor) && pActor.canAttackTarget(pTarget) && pTarget.stats.actorSize <= pActor.stats.actorSize && pTarget.currentTile.isSameIsland(pActor.currentTile);
		}
	}
}
