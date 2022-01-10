using System;

namespace ai.behaviours
{
	// Token: 0x02000382 RID: 898
	public class BehRepairInDock : BehCity
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x000A2F4E File Offset: 0x000A114E
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000A2F6C File Offset: 0x000A116C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.curStats.health <= pActor.data.health)
			{
				return BehResult.Continue;
			}
			int num = pActor.curStats.health - pActor.data.health;
			num = ((num > 100) ? 100 : num);
			pActor.restoreHealth(num);
			float num2 = (float)(num / 25);
			pActor.timer_action = (float)Math.Ceiling((double)num2);
			pActor.stayInBuilding(pActor.beh_building_target);
			pActor.beh_tile_target = null;
			pActor.beh_building_target.startShake(0.5f);
			if (pActor.curStats.health > pActor.data.health)
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}
	}
}
