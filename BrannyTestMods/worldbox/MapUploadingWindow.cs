using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A8 RID: 680
public class MapUploadingWindow : MonoBehaviour
{
	// Token: 0x040011EF RID: 4591
	public Button doneButton;

	// Token: 0x040011F0 RID: 4592
	public Image loadingImage;

	// Token: 0x040011F1 RID: 4593
	public Image doneImage;

	// Token: 0x040011F2 RID: 4594
	public GameObject mapIDGroup;

	// Token: 0x040011F3 RID: 4595
	public Text mapIDText;

	// Token: 0x040011F4 RID: 4596
	public Text statusMessage;

	// Token: 0x040011F5 RID: 4597
	private int windowState = -1;

	// Token: 0x040011F6 RID: 4598
	public Text percents;

	// Token: 0x040011F7 RID: 4599
	public Image bar;

	// Token: 0x040011F8 RID: 4600
	public Image mask;

	// Token: 0x040011F9 RID: 4601
	public static bool uploading;
}
