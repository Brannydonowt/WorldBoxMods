using System;

namespace ai.behaviours
{
	// Token: 0x02000377 RID: 887
	public class BehLookAtTarget : BehaviourActionActor
	{
		// Token: 0x06001378 RID: 4984 RVA: 0x000A2893 File Offset: 0x000A0A93
		public BehLookAtTarget(string pType)
		{
			this.type = pType;
			this.null_check_building_target = true;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000A28AC File Offset: 0x000A0AAC
		public override BehResult execute(Actor pActor)
		{
			if (this.type == "building_target" && pActor.stats.flipAnimation)
			{
				if (pActor.transform.localPosition.x < (float)pActor.beh_building_target.currentTile.x)
				{
					pActor.setFlip(true);
				}
				else
				{
					pActor.setFlip(false);
				}
			}
			pActor.timer_action = 0.3f;
			return BehResult.Continue;
		}

		// Token: 0x04001536 RID: 5430
		private string type;
	}
}
