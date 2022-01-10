using System;

namespace ai.behaviours
{
	// Token: 0x0200037A RID: 890
	public class BehMakeFarm : BehCity
	{
		// Token: 0x06001380 RID: 4992 RVA: 0x000A2B98 File Offset: 0x000A0D98
		public override void create()
		{
			base.create();
			this.null_check_tile_target = true;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000A2BA7 File Offset: 0x000A0DA7
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_tile_target.Type.canBeFarmField && pActor.beh_tile_target.building == null)
			{
				MapAction.terraformTop(pActor.beh_tile_target, TopTileLibrary.field);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
