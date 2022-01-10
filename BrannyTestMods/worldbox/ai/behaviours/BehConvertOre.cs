using System;

namespace ai.behaviours
{
	// Token: 0x02000357 RID: 855
	public class BehConvertOre : BehCity
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x000A0E05 File Offset: 0x0009F005
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000A0E14 File Offset: 0x0009F014
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.data.storage.get("ore") == 0)
			{
				return BehResult.Stop;
			}
			pActor.city.data.storage.change("ore", -1);
			pActor.inventory.add("metals", 1);
			return BehResult.Continue;
		}
	}
}
