using System;

namespace ai.behaviours
{
	// Token: 0x02000381 RID: 897
	public class BehRemoveRuins : BehaviourActionActor
	{
		// Token: 0x06001392 RID: 5010 RVA: 0x000A2F28 File Offset: 0x000A1128
		public override void create()
		{
			base.create();
			this.null_check_building_target = true;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000A2F37 File Offset: 0x000A1137
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_building_target.startRemove(true);
			return BehResult.Continue;
		}
	}
}
