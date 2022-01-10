using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[Serializable]
public class UnitSpriteAnimationData
{
	// Token: 0x060001F8 RID: 504 RVA: 0x00026FAE File Offset: 0x000251AE
	public UnitSpriteAnimationData()
	{
		this.head = default(Vector3);
		this.head = default(Vector3);
		this.backpack = default(Vector3);
	}

	// Token: 0x040002B3 RID: 691
	public string name;

	// Token: 0x040002B4 RID: 692
	public Vector3 head;

	// Token: 0x040002B5 RID: 693
	public Vector3 item;

	// Token: 0x040002B6 RID: 694
	public Vector3 backpack;

	// Token: 0x040002B7 RID: 695
	public bool showHead;

	// Token: 0x040002B8 RID: 696
	public bool showItem;
}
