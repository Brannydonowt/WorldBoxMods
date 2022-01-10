using System;

namespace ai.behaviours
{
	// Token: 0x0200037B RID: 891
	public class BehMakeItem : BehCity
	{
		// Token: 0x06001383 RID: 4995 RVA: 0x000A2BE9 File Offset: 0x000A0DE9
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000A2BF8 File Offset: 0x000A0DF8
		public override BehResult execute(Actor pActor)
		{
			pActor.city.tryProduceItem(pActor, ItemProductionOrder.Both);
			return BehResult.Continue;
		}
	}
}
