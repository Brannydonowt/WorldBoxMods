using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class AnimationDataUnit
{
	// Token: 0x04000937 RID: 2359
	internal string id;

	// Token: 0x04000938 RID: 2360
	internal Dictionary<string, Sprite> sprites;

	// Token: 0x04000939 RID: 2361
	internal Dictionary<string, AnimationFrameData> frameData;

	// Token: 0x0400093A RID: 2362
	internal ActorAnimation idle;

	// Token: 0x0400093B RID: 2363
	internal ActorAnimation walking;

	// Token: 0x0400093C RID: 2364
	internal ActorAnimation swimming;

	// Token: 0x0400093D RID: 2365
	internal Sprite[] heads;
}
