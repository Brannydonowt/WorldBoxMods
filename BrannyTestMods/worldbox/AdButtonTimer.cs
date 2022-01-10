using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DF RID: 479
public class AdButtonTimer : MonoBehaviour
{
	// Token: 0x06000ACC RID: 2764 RVA: 0x0006BD04 File Offset: 0x00069F04
	private void Awake()
	{
		AdButtonTimer.instance = this;
		this.adTimer = 10.0;
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0006BD1C File Offset: 0x00069F1C
	internal static void setAdTimer()
	{
		if (PlayerConfig.instance == null)
		{
			return;
		}
		double num = PlayerConfig.instance.data.nextAdTimestamp;
		num -= Epoch.Current();
		AdButtonTimer.instance.adTimer = num;
		if (AdButtonTimer.instance.adTimer < 0.0 || PlayerConfig.instance.data.nextAdTimestamp == -1.0)
		{
			AdButtonTimer.instance.adTimer = -1.0;
		}
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0006BD96 File Offset: 0x00069F96
	private void OnEnable()
	{
		AdButtonTimer.setAdTimer();
		this.updateButton();
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0006BDA3 File Offset: 0x00069FA3
	private void Update()
	{
		if (Config.havePremium)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (this.adTimer > 0.0)
		{
			this.adTimer -= (double)Time.deltaTime;
		}
		this.updateButton();
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x0006BDE4 File Offset: 0x00069FE4
	private void updateButton()
	{
		if (this.tRecalc > 0)
		{
			this.tRecalc--;
		}
		else
		{
			this.tRecalc = 10;
			AdButtonTimer.setAdTimer();
		}
		if (this.adTimer > 0.0)
		{
			this.timer.gameObject.SetActive(true);
			this.timer.text = Toolbox.formatTimer((float)this.adTimer);
			this.icon.color = this.transparent;
			return;
		}
		this.timer.gameObject.SetActive(false);
		this.icon.color = Color.white;
	}

	// Token: 0x04000D4E RID: 3406
	internal static AdButtonTimer instance;

	// Token: 0x04000D4F RID: 3407
	public Text timer;

	// Token: 0x04000D50 RID: 3408
	public Button button;

	// Token: 0x04000D51 RID: 3409
	public Image icon;

	// Token: 0x04000D52 RID: 3410
	private double adTimer;

	// Token: 0x04000D53 RID: 3411
	private Color transparent = new Color(1f, 1f, 1f, 0.3f);

	// Token: 0x04000D54 RID: 3412
	private int tRecalc;
}
