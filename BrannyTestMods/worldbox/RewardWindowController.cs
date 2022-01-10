using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E9 RID: 489
public class RewardWindowController : MonoBehaviour
{
	// Token: 0x06000B26 RID: 2854 RVA: 0x0006D340 File Offset: 0x0006B540
	private void Update()
	{
		double num = PlayerConfig.instance.data.nextAdTimestamp;
		double num2 = Epoch.Current();
		num -= num2;
		if (num > 0.0)
		{
			this.watchVideoButton.SetActive(false);
			this.waitTimeElement.SetActive(true);
			this.textElement.text = Toolbox.formatTimer((float)num);
			return;
		}
		this.watchVideoButton.SetActive(true);
		this.waitTimeElement.SetActive(false);
	}

	// Token: 0x04000D80 RID: 3456
	public GameObject watchVideoButton;

	// Token: 0x04000D81 RID: 3457
	public GameObject waitTimeElement;

	// Token: 0x04000D82 RID: 3458
	public Text textElement;
}
