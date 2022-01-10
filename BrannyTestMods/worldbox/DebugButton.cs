using System;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class DebugButton : MonoBehaviour
{
	// Token: 0x06000837 RID: 2103 RVA: 0x00059CB0 File Offset: 0x00057EB0
	private void OnEnable()
	{
		this.lastPremium = !Config.havePremium;
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00059CC0 File Offset: 0x00057EC0
	private void Update()
	{
		if (this.lastPremium != Config.havePremium)
		{
			this.updatePosition();
			this.lastPremium = Config.havePremium;
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00059CE0 File Offset: 0x00057EE0
	private void updatePosition()
	{
		if (this.origPosition == Vector3.zero)
		{
			this.origPosition = base.GetComponent<RectTransform>().localPosition;
			this.premPosition = this.premiumButton.GetComponent<RectTransform>().localPosition;
		}
		if (Config.havePremium)
		{
			base.GetComponent<RectTransform>().localPosition = this.premPosition;
			return;
		}
		base.GetComponent<RectTransform>().localPosition = this.origPosition;
	}

	// Token: 0x04000AA6 RID: 2726
	public GameObject premiumButton;

	// Token: 0x04000AA7 RID: 2727
	private bool lastPremium;

	// Token: 0x04000AA8 RID: 2728
	private Vector3 origPosition = Vector3.zero;

	// Token: 0x04000AA9 RID: 2729
	private Vector3 premPosition = Vector3.zero;
}
