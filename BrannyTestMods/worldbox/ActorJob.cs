using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x02000151 RID: 337
public class ActorJob : JobAsset<BehaviourActorCondition>
{
	// Token: 0x04000A38 RID: 2616
	public bool cityJob;

	// Token: 0x04000A39 RID: 2617
	public bool random;

	// Token: 0x04000A3A RID: 2618
	public List<BehActive> active = new List<BehActive>();

	// Token: 0x04000A3B RID: 2619
	public bool cancel_when_embarking = true;
}
