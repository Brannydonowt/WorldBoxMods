using System;

namespace ai.behaviours
{
	// Token: 0x02000346 RID: 838
	public class BehBuildTargetProgress : BehaviourActionActor
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x000A03C2 File Offset: 0x0009E5C2
		public override void create()
		{
			base.create();
			this.check_building_target_non_usable = true;
			this.null_check_city = true;
			this.null_check_tile_target = true;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000A03E0 File Offset: 0x0009E5E0
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.beh_building_target.data.underConstruction)
			{
				return BehResult.Stop;
			}
			Sfx.play("hammer1", true, pActor.transform.localPosition.x, pActor.transform.localPosition.y);
			pActor.beh_building_target.updateBuild(1);
			return BehResult.Continue;
		}
	}
}
