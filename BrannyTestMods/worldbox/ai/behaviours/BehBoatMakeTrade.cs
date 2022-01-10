using System;

namespace ai.behaviours
{
	// Token: 0x02000335 RID: 821
	public class BehBoatMakeTrade : BehaviourActionActor
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x0009FA8F File Offset: 0x0009DC8F
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0009FA9E File Offset: 0x0009DC9E
		public override BehResult execute(Actor pActor)
		{
			pActor.inventory.add("gold", 5);
			return BehResult.Continue;
		}
	}
}
