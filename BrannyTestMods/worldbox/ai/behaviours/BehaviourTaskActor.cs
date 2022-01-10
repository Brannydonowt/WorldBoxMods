using System;

namespace ai.behaviours
{
	// Token: 0x02000396 RID: 918
	public class BehaviourTaskActor : BehaviourTaskBase<BehaviourActionActor>
	{
		// Token: 0x04001556 RID: 5462
		public bool moveFromBlock;

		// Token: 0x04001557 RID: 5463
		public bool swim_to_island;

		// Token: 0x04001558 RID: 5464
		public bool ignoreFightCheck;

		// Token: 0x04001559 RID: 5465
		public bool fighting;

		// Token: 0x0400155A RID: 5466
		internal string force_item_sprite = string.Empty;
	}
}
