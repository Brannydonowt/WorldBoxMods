using System;

namespace ai.behaviours
{
	// Token: 0x0200035A RID: 858
	public class BehExitBuilding : BehCity
	{
		// Token: 0x0600132A RID: 4906 RVA: 0x000A0F28 File Offset: 0x0009F128
		public override void create()
		{
			base.create();
			this.null_check_building_target = true;
			this.check_building_target_non_usable = true;
			this.special_inside_object = true;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000A0F45 File Offset: 0x0009F145
		public override BehResult execute(Actor pActor)
		{
			pActor.exitHouse();
			pActor.beh_building_target.startShake(0.01f);
			return BehResult.Continue;
		}
	}
}
