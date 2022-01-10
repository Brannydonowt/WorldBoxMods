using System;

namespace ai.behaviours
{
	// Token: 0x02000366 RID: 870
	public class BehFindSameRaceActor : BehaviourActionActor
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x000A19BC File Offset: 0x0009FBBC
		public override BehResult execute(Actor pActor)
		{
			Actor beh_actor_target = null;
			pActor.currentTile.region.island.actors.ShuffleOne<Actor>();
			foreach (Actor actor in pActor.currentTile.region.island.actors)
			{
				if (!(actor == pActor) && actor.data.alive && actor.currentTile.isSameIsland(pActor.currentTile) && actor.isRace(pActor) && actor.data.bornTime <= pActor.data.bornTime)
				{
					beh_actor_target = actor;
				}
			}
			pActor.beh_actor_target = beh_actor_target;
			if (pActor.beh_actor_target == null)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
