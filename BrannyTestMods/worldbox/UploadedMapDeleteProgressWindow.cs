using System;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class UploadedMapDeleteProgressWindow : MonoBehaviour
{
	// Token: 0x06000C6D RID: 3181 RVA: 0x00079FED File Offset: 0x000781ED
	private void OnEnable()
	{
		this.deletingOverlay.SetActive(false);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00079FFB File Offset: 0x000781FB
	public void confirmDeletion()
	{
		this.deletingOverlay.SetActive(true);
	}

	// Token: 0x04000F2F RID: 3887
	public GameObject deletingOverlay;
}
