using System;

namespace ai.behaviours
{
	// Token: 0x02000371 RID: 881
	public class BehGoToActorTarget : BehaviourActionActor
	{
		// Token: 0x06001367 RID: 4967 RVA: 0x000A25C6 File Offset: 0x000A07C6
		public BehGoToActorTarget(string pType = "sameTile", bool pPathOnWater = false)
		{
			this.pathOnWater = pPathOnWater;
			this.type = pType;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000A25DC File Offset: 0x000A07DC
		public override void create()
		{
			base.create();
			this.null_check_actor_target = true;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000A25EC File Offset: 0x000A07EC
		public override BehResult execute(Actor pActor)
		{
			WorldTile pTile = pActor.beh_actor_target.currentTile;
			string text = this.type;
			if (text != null)
			{
				if (!(text == "sameTile"))
				{
					if (text == "sameRegion")
					{
						pTile = pActor.beh_actor_target.currentTile.region.tiles.GetRandom<WorldTile>();
					}
				}
				else
				{
					pTile = pActor.beh_actor_target.currentTile;
				}
			}
			if (pActor.goTo(pTile, this.pathOnWater, false) == ExecuteEvent.True)
			{
				return BehResult.Continue;
			}
			if (!pActor.targetsToIgnore.Contains(pActor.beh_actor_target))
			{
				pActor.targetsToIgnore.Add(pActor.beh_actor_target);
			}
			return BehResult.Stop;
		}

		// Token: 0x04001530 RID: 5424
		private string type;

		// Token: 0x04001531 RID: 5425
		private bool pathOnWater;
	}
}
