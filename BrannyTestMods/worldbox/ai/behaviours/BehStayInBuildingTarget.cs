using System;

namespace ai.behaviours
{
	// Token: 0x0200038D RID: 909
	public class BehStayInBuildingTarget : BehCity
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x000A3520 File Offset: 0x000A1720
		public BehStayInBuildingTarget(float pMin = 0f, float pMax = 1f)
		{
			this.min = pMin;
			this.max = pMax;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000A3536 File Offset: 0x000A1736
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000A3553 File Offset: 0x000A1753
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_action = Toolbox.randomFloat(this.min, this.max);
			pActor.stayInBuilding(pActor.beh_building_target);
			pActor.beh_tile_target = null;
			pActor.beh_building_target.startShake(0.5f);
			return BehResult.Continue;
		}

		// Token: 0x04001544 RID: 5444
		private float min;

		// Token: 0x04001545 RID: 5445
		private float max;
	}
}
