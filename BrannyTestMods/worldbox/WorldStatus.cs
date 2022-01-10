using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class WorldStatus : MonoBehaviour
{
	// Token: 0x06000391 RID: 913 RVA: 0x000383DC File Offset: 0x000365DC
	public void setCurrentSlot(int pSlotId)
	{
		WorldStatus.currentSlot = pSlotId;
	}

	// Token: 0x040005DD RID: 1501
	public static int currentSlot;
}
