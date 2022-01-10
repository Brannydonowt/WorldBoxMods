using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A6 RID: 678
public class MapTagWindow : MonoBehaviour
{
	// Token: 0x040011E3 RID: 4579
	public Button continueButton;

	// Token: 0x040011E4 RID: 4580
	public Sprite buttonOn;

	// Token: 0x040011E5 RID: 4581
	public Sprite buttonOff;

	// Token: 0x040011E6 RID: 4582
	public CanvasGroup tagGroup;

	// Token: 0x040011E7 RID: 4583
	public Text statusMessage;

	// Token: 0x040011E8 RID: 4584
	public MapTagButton mapTagButtonPrefab;

	// Token: 0x040011E9 RID: 4585
	public Transform transformContent;
}
