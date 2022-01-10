using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
[Serializable]
public class DragonAssetContainer
{
	// Token: 0x040009A4 RID: 2468
	public string name;

	// Token: 0x040009A5 RID: 2469
	public DragonState id;

	// Token: 0x040009A6 RID: 2470
	public Sprite[] frames;

	// Token: 0x040009A7 RID: 2471
	public DragonState[] states;

	// Token: 0x040009A8 RID: 2472
	public float speed = 0.1f;
}
