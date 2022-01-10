using System;

namespace ai.behaviours
{
	// Token: 0x02000349 RID: 841
	public class BehCityCreateRoad : BehCity
	{
		// Token: 0x060012FE RID: 4862 RVA: 0x000A04B7 File Offset: 0x0009E6B7
		public override void create()
		{
			base.create();
			this.null_check_tile_target = true;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000A04C6 File Offset: 0x0009E6C6
		public override BehResult execute(Actor pActor)
		{
			MapAction.createRoad(pActor.beh_tile_target);
			return BehResult.Continue;
		}
	}
}
