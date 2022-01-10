using System;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public class PlayInterstitialAd : MonoBehaviour
{
	// Token: 0x06000B04 RID: 2820 RVA: 0x0006C760 File Offset: 0x0006A960
	private void Awake()
	{
		PlayInterstitialAd.instance = this;
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x0006C768 File Offset: 0x0006A968
	private void Start()
	{
		this.initAds();
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x0006C770 File Offset: 0x0006A970
	private void Update()
	{
		if (!Config.isMobile)
		{
			return;
		}
		if (!this.initiated)
		{
			this.initAds();
			return;
		}
		if (this.timeout >= 0f)
		{
			this.timeout -= Time.deltaTime;
			return;
		}
		if (!PlayInterstitialAd.adProvider.HasAd())
		{
			this.scheduleAd(60f);
			PlayInterstitialAd.adProvider.RequestAd();
			return;
		}
		this.timeout += 10f;
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
	public void initAds()
	{
		if (!Config.isMobile)
		{
			return;
		}
		if (Config.havePremium)
		{
			return;
		}
		if (!Config.adsInitialized)
		{
			return;
		}
		if (this.initiated)
		{
			return;
		}
		Debug.Log("Init iads");
		this.realInit();
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0006C81C File Offset: 0x0006AA1C
	public void realInit()
	{
		this.initiated = true;
		PlayInterstitialAd.adProvider = new GoogleInterstitialAd();
		PlayInterstitialAd.adProvider.adFinishedCallback = new Action(this.adFinished);
		PlayInterstitialAd.adProvider.adFailedCallback = new Action(this.adFailed);
		PlayInterstitialAd.adProvider.adStartedCallback = new Action(this.adStarted);
		this.scheduleAd(10f);
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0006C887 File Offset: 0x0006AA87
	internal bool isReady()
	{
		return PlayInterstitialAd.instance.initiated && PlayInterstitialAd.adProvider != null && PlayInterstitialAd.adProvider.IsReady();
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x0006C8AC File Offset: 0x0006AAAC
	internal void showAd()
	{
		MonoBehaviour.print("- Show interstitial ad " + this.isReady().ToString());
		Analytics.LogEvent("interstitial_ad_show", true, true);
		PlayInterstitialAd.adProvider.ShowAd();
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0006C8EC File Offset: 0x0006AAEC
	public static void forceShowAd()
	{
		try
		{
			Analytics.LogEvent("interstitial_ad_force_show", true, true);
			if (!PlayInterstitialAd.instance.initiated)
			{
				PlayInterstitialAd.instance.realInit();
				PlayInterstitialAd.adProvider.RequestAd();
			}
			if (PlayInterstitialAd.adProvider.IsReady())
			{
				PlayInterstitialAd.adProvider.ShowAd();
			}
			else
			{
				PlayInterstitialAd.adProvider.RequestAd();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x0006C95C File Offset: 0x0006AB5C
	private void scheduleAd(float pTimer = 60f)
	{
		if (this.timeout > 0f)
		{
			return;
		}
		PlayInterstitialAd.adProvider.KillAd();
		this.timeout = pTimer;
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x0006C97D File Offset: 0x0006AB7D
	private void adStarted()
	{
		this.failed = 0;
		this.timeout = 10f;
		Analytics.LogEvent("interstitial_ad_started", true, true);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x0006C99D File Offset: 0x0006AB9D
	private void adFailed()
	{
		this.failed++;
		this.timeout = (float)(10 * this.failed);
		Analytics.LogEvent("interstitial_ad_failed", true, true);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x0006C9C9 File Offset: 0x0006ABC9
	private void adFinished()
	{
		this.failed = 0;
		this.timeout = 10f;
		Analytics.LogEvent("interstitial_ad_finished", true, true);
	}

	// Token: 0x04000D5E RID: 3422
	public static PlayInterstitialAd instance;

	// Token: 0x04000D5F RID: 3423
	public static IWorldBoxAd adProvider;

	// Token: 0x04000D60 RID: 3424
	public bool initiated;

	// Token: 0x04000D61 RID: 3425
	public float timeout;

	// Token: 0x04000D62 RID: 3426
	private int failed;
}
