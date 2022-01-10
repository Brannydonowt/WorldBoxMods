using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class PremiumElementsChecker : BaseWorldObject
{
	// Token: 0x06001018 RID: 4120 RVA: 0x0008DE85 File Offset: 0x0008C085
	private void Awake()
	{
		PremiumElementsChecker.instance = this;
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0008DE8D File Offset: 0x0008C08D
	internal override void create()
	{
		base.create();
		this.firstTimeRewardButtonTimer = 0f;
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0008DEA0 File Offset: 0x0008C0A0
	internal static bool goodForInterstitialAd()
	{
		return false;
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0008DEA3 File Offset: 0x0008C0A3
	public static void setInterstitialAdTimer(int howLong = 100)
	{
		if (howLong > 100)
		{
			howLong = 100;
		}
		PremiumElementsChecker.instance.insterAdTimer = (float)howLong;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0008DEBA File Offset: 0x0008C0BA
	private void Update()
	{
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0008DEBC File Offset: 0x0008C0BC
	public static void checkElements()
	{
		if (Config.havePremium)
		{
			PremiumElementsChecker.instance.premiumButtonCorner.SetActive(false);
			PremiumElementsChecker.instance.adsButton.SetActive(false);
		}
		else
		{
			PremiumElementsChecker.instance.premiumButtonCorner.SetActive(true);
		}
		foreach (PowerButton powerButton in PowerButton.powerButtons)
		{
			powerButton.checkLockIcon();
		}
	}

	// Token: 0x04001341 RID: 4929
	public GameObject premiumButtonCorner;

	// Token: 0x04001342 RID: 4930
	public GameObject adsButton;

	// Token: 0x04001343 RID: 4931
	private static PremiumElementsChecker instance;

	// Token: 0x04001344 RID: 4932
	private float firstTimeRewardButtonTimer = 1f;

	// Token: 0x04001345 RID: 4933
	internal float insterAdTimer;
}
