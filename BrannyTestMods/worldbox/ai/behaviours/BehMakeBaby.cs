using System;

namespace ai.behaviours
{
	// Token: 0x02000379 RID: 889
	public class BehMakeBaby : BehaviourActionActor
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x000A29ED File Offset: 0x000A0BED
		public override void create()
		{
			base.create();
			this.null_check_actor_target = true;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000A29FC File Offset: 0x000A0BFC
		public override BehResult execute(Actor pActor)
		{
			this.makeBaby(pActor, pActor.beh_actor_target.a);
			return BehResult.Continue;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000A2A14 File Offset: 0x000A0C14
		private void makeBaby(Actor pActor1, Actor pActor2)
		{
			pActor1.startBabymakingTimeout();
			pActor2.startBabymakingTimeout();
			pActor1.data.children++;
			pActor2.data.children++;
			string id;
			if (Toolbox.randomBool())
			{
				id = pActor1.stats.id;
			}
			else
			{
				id = pActor2.stats.id;
			}
			WorldTile worldTile = null;
			foreach (WorldTile worldTile2 in pActor1.currentTile.neighbours)
			{
				if (worldTile2 != pActor1.currentTile && worldTile2 != pActor1.currentTile && !worldTile2.Type.liquid)
				{
					worldTile = worldTile2;
					break;
				}
			}
			if (worldTile == null)
			{
				worldTile = pActor1.currentTile;
			}
			Actor actor = BehaviourActionBase<Actor>.world.createNewUnit(id, worldTile, null, 6f, null);
			actor.justBorn();
			actor.data.hunger = actor.stats.maxHunger / 2;
			if (actor.stats.useSkinColors)
			{
				if (Toolbox.randomBool())
				{
					actor.data.skin_set = pActor1.data.skin_set;
				}
				else
				{
					actor.data.skin_set = pActor2.data.skin_set;
				}
				actor.data.skin = ActorTool.getBabyColor(pActor1, pActor2);
			}
			BehaviourActionBase<Actor>.world.gameStats.data.creaturesBorn++;
		}
	}
}
