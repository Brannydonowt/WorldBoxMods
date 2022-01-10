using System;

namespace ai.behaviours
{
	// Token: 0x02000393 RID: 915
	public class BehUnloadResources : BehCity
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x000A3E4F File Offset: 0x000A204F
		public override void create()
		{
			this.special_inside_object = true;
			base.create();
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000A3E60 File Offset: 0x000A2060
		public override BehResult execute(Actor pActor)
		{
			if (string.IsNullOrEmpty(pActor.inventory.resource))
			{
				return BehResult.Continue;
			}
			pActor.city.data.storage.change(pActor.inventory.resource, pActor.inventory.amount);
			pActor.inventory.empty();
			return BehResult.Continue;
		}
	}
}
