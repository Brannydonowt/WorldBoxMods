using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C9 RID: 713
public class MapPreviewImage : MonoBehaviour
{
	// Token: 0x04001277 RID: 4727
	public bool premiumOnly = true;

	// Token: 0x04001278 RID: 4728
	public Image premiumIcon;

	// Token: 0x04001279 RID: 4729
	public Button button;

	// Token: 0x0400127A RID: 4730
	public SlotButtonCallback slotData;

	// Token: 0x0400127B RID: 4731
	public Map map;

	// Token: 0x0400127C RID: 4732
	public Sprite defaultSprite;

	// Token: 0x0400127D RID: 4733
	private ButtonAnimation buttonAnimation;
}
