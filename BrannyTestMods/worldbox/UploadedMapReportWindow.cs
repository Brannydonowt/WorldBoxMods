using System;
using UnityEngine;

// Token: 0x02000235 RID: 565
public class UploadedMapReportWindow : MonoBehaviour
{
	// Token: 0x06000C79 RID: 3193 RVA: 0x0007A08D File Offset: 0x0007828D
	private void OnEnable()
	{
		this.reportOverlay.SetActive(false);
		this.reportButtons.SetActive(true);
		this.reportConfirmation.SetActive(false);
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x0007A0B3 File Offset: 0x000782B3
	public void reportNSFW()
	{
		this.reportReason = "nsfw";
		this.confirmReport();
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0007A0C6 File Offset: 0x000782C6
	public void reportCrash()
	{
		this.reportReason = "crash";
		this.confirmReport();
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0007A0D9 File Offset: 0x000782D9
	public void reportBroken()
	{
		this.reportReason = "broken";
		this.confirmReport();
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x0007A0EC File Offset: 0x000782EC
	public void confirmReport()
	{
		this.reportButtons.SetActive(false);
		this.reportOverlay.SetActive(true);
	}

	// Token: 0x04000F4B RID: 3915
	public GameObject reportOverlay;

	// Token: 0x04000F4C RID: 3916
	public GameObject reportButtons;

	// Token: 0x04000F4D RID: 3917
	public GameObject reportConfirmation;

	// Token: 0x04000F4E RID: 3918
	private string reportReason = "";
}
