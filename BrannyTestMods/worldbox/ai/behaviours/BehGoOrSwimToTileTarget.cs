using System;

namespace ai.behaviours
{
	// Token: 0x02000370 RID: 880
	public class BehGoOrSwimToTileTarget : BehGoToTileTarget
	{
		// Token: 0x06001365 RID: 4965 RVA: 0x000A25AF File Offset: 0x000A07AF
		public override void create()
		{
			base.create();
			this.walkOnWater = true;
		}
	}
}
