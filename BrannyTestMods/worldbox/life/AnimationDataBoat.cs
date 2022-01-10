using System;
using System.Collections.Generic;

namespace life
{
	// Token: 0x020003BA RID: 954
	public class AnimationDataBoat
	{
		// Token: 0x04001575 RID: 5493
		internal string id;

		// Token: 0x04001576 RID: 5494
		internal Dictionary<int, ActorAnimation> dict = new Dictionary<int, ActorAnimation>();

		// Token: 0x04001577 RID: 5495
		internal ActorAnimation broken;

		// Token: 0x04001578 RID: 5496
		internal ActorAnimation normal;
	}
}
