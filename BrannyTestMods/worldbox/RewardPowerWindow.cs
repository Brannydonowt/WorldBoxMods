using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E7 RID: 487
public class RewardPowerWindow : MonoBehaviour
{
	// Token: 0x06000B1D RID: 2845 RVA: 0x0006CF4C File Offset: 0x0006B14C
	private void OnEnable()
	{
		Transform transform = base.gameObject.transform.Find("Background/Text");
		if (transform != null)
		{
			LocalizedText component = transform.GetComponent<LocalizedText>();
			if (component != null)
			{
				component.updateText(true);
			}
		}
		if (!Config.adsInitialized)
		{
			InitStuff.initGoogleMobileAds();
		}
		if (Config.powerToUnlock != null && this.icon1 != null)
		{
			PowerButton powerButton = PowerButton.get(Config.powerToUnlock.id);
			if (powerButton != null)
			{
				this.icon1.sprite = powerButton.icon.sprite;
				this.icon2.sprite = powerButton.icon.sprite;
			}
		}
	}

	// Token: 0x04000D77 RID: 3447
	public Image icon1;

	// Token: 0x04000D78 RID: 3448
	public Image icon2;
}
