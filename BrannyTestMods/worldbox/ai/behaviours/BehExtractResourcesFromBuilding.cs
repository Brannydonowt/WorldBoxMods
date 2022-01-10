using System;

namespace ai.behaviours
{
	// Token: 0x0200035B RID: 859
	public class BehExtractResourcesFromBuilding : BehaviourActionActor
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x000A0F66 File Offset: 0x0009F166
		public override void create()
		{
			base.create();
			this.null_check_building_target = true;
			this.check_building_target_non_usable = true;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000A0F7C File Offset: 0x0009F17C
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_building_target.extractResources(pActor, pActor.beh_building_target.stats.resources_given);
			pActor.inventory.add(pActor.beh_building_target.stats.resource_id, pActor.beh_building_target.stats.resources_given);
			return BehResult.Continue;
		}
	}
}
