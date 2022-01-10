using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class RewardedAds : MonoBehaviour
{
	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0006D3BE File Offset: 0x0006B5BE
	// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0006D3C5 File Offset: 0x0006B5C5
	public static string debug
	{
		get
		{
			return RewardedAds._debug;
		}
		set
		{
			RewardedAds._debug = ((value != null && value.Length > 50) ? value.Substring(value.Length - 50, 50) : value);
		}
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0006D3ED File Offset: 0x0006B5ED
	private void Awake()
	{
		RewardedAds.instance = this;
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x0006D3F5 File Offset: 0x0006B5F5
	private void Start()
	{
		RewardedAds.debug += "s_";
		this.initAds();
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x0006D414 File Offset: 0x0006B614
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
		if (RewardedAds.rewardAdsQueue > 0f)
		{
			RewardedAds.rewardAdsQueue -= Time.deltaTime;
			if (RewardedAds.rewardAdsQueue <= 0f || MapBox.instance.hasFocus)
			{
				this.executeHandleRewards();
				RewardedAds.rewardAdsQueue = 0f;
			}
		}
		if (this.timeout >= 0f)
		{
			this.timeout -= Time.deltaTime;
			return;
		}
		if (!RewardedAds.hasAd())
		{
			this.RequestRewardBasedVideo();
			return;
		}
		this.timeout += 10f;
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x0006D4BC File Offset: 0x0006B6BC
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
		Debug.Log("Init ads");
		RewardedAds.debug += "i_";
		this.realInit();
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x0006D510 File Offset: 0x0006B710
	public void realInit()
	{
		this.initiated = true;
		RewardedAds.adProvider = new GoogleRewardAd();
		RewardedAds.adProvider.adFinishedCallback = new Action(this.adFinished);
		RewardedAds.adProvider.adFailedCallback = new Action(this.adFailed);
		RewardedAds.adProvider.adStartedCallback = new Action(this.adStarted);
		RewardedAds.debug += "i2_";
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x0006D584 File Offset: 0x0006B784
	public void loadNewAd()
	{
		RewardedAds.debug += "n_";
		RewardedAds.debug += "n2_";
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x0006D5AE File Offset: 0x0006B7AE
	public void unloadAd()
	{
		RewardedAds.debug += "u_";
		RewardedAds.adProvider.KillAd();
		RewardedAds.debug += "u2_";
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x0006D5E4 File Offset: 0x0006B7E4
	private void RequestRewardBasedVideo()
	{
		RewardedAds.debug += "h8_";
		this.timeout = 10f;
		this.unloadAd();
		RewardedAds.adProvider.RequestAd();
		RewardedAds.debug += "h9_";
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x0006D634 File Offset: 0x0006B834
	private static void logEvent(string pEvent)
	{
		Analytics.LogEvent(pEvent, true, true);
		if (RewardedAds.adType != "")
		{
			Analytics.LogEvent(pEvent + "_" + RewardedAds.adType, true, true);
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x0006D666 File Offset: 0x0006B866
	private void adStarted()
	{
		this.failed = 0;
		this.timeout = 2f;
		RewardedAds.logEvent("ad_reward_started");
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x0006D684 File Offset: 0x0006B884
	private void adFailed()
	{
		this.failed++;
		this.timeout = (float)(10 * this.failed);
		RewardedAds.logEvent("ad_reward_failed");
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x0006D6AE File Offset: 0x0006B8AE
	private void adFinished()
	{
		this.failed = 0;
		this.timeout = 2f;
		RewardedAds.logEvent("ad_reward_finished");
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x0006D6CC File Offset: 0x0006B8CC
	private PowerButton generateRandomReward()
	{
		return null;
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x0006D6CF File Offset: 0x0006B8CF
	private bool haveRewardAvailable()
	{
		return false;
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0006D6D2 File Offset: 0x0006B8D2
	private void generateReward()
	{
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0006D6D4 File Offset: 0x0006B8D4
	internal static bool isReady()
	{
		return RewardedAds.instance.initiated && RewardedAds.adProvider != null && RewardedAds.adProvider.IsReady();
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0006D6F7 File Offset: 0x0006B8F7
	public static bool hasAd()
	{
		return RewardedAds.adProvider.HasAd();
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x0006D703 File Offset: 0x0006B903
	internal static bool isShowing()
	{
		return false;
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x0006D708 File Offset: 0x0006B908
	public void ShowRewardedAd(string pAdType = "")
	{
		RewardedAds.adType = pAdType;
		RewardedAds.debug += "h10_";
		if (RewardedAds.isReady())
		{
			Debug.Log("Show rewarded video");
			RewardedAds.logEvent("ad_reward_start");
			RewardedAds.adProvider.ShowAd();
			return;
		}
		ScrollWindow.get("ad_loading_error").clickShow();
		RewardedAds.logEvent("ad_reward_loading_error");
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x0006D770 File Offset: 0x0006B970
	public static void forceShowAd()
	{
		try
		{
			RewardedAds.adType = "force";
			if (!RewardedAds.instance.initiated)
			{
				RewardedAds.instance.realInit();
				RewardedAds.logEvent("ad_reward_force_request");
				RewardedAds.adProvider.RequestAd();
			}
			if (RewardedAds.adProvider.IsReady())
			{
				RewardedAds.logEvent("ad_reward_force_start");
				RewardedAds.adProvider.ShowAd();
			}
			else
			{
				RewardedAds.logEvent("ad_reward_force_request");
				RewardedAds.adProvider.RequestAd();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x0006D7FC File Offset: 0x0006B9FC
	public void handleRewards()
	{
		if (!MapBox.instance.hasFocus)
		{
			Debug.Log("Delay reward");
			RewardedAds.rewardAdsQueue = 0.3f;
			return;
		}
		this.executeHandleRewards();
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x0006D828 File Offset: 0x0006BA28
	public void executeHandleRewards()
	{
		RewardedAds.rewardAdsQueue = 0f;
		Debug.Log("Execute reward");
		ScrollWindow.hideAllEvent(false);
		ScrollWindow.get("reward_ads_received").show(null, "right", "right", true);
		if (PlayInterstitialAd.instance != null)
		{
			PremiumElementsChecker.setInterstitialAdTimer(100);
		}
		this.generateReward();
		if (MapBox.instance.gameStats.data.gameLaunches > 10 && MapBox.instance.gameStats.data.gameTime > 36000.0)
		{
			PlayerConfig.instance.data.nextAdTimestamp = Epoch.Current() + (double)(60f * Toolbox.randomFloat(2f, 5f));
		}
		else
		{
			PlayerConfig.instance.data.nextAdTimestamp = Epoch.Current() + 300.0;
		}
		PremiumElementsChecker.checkElements();
		AdButtonTimer.setAdTimer();
		RewardedAds.logEvent("ad_reward_watched");
		PlayerConfig.saveData();
		RewardedAds.debug += "hr_";
	}

	// Token: 0x04000D83 RID: 3459
	internal static RewardedAds instance;

	// Token: 0x04000D84 RID: 3460
	internal static IWorldBoxAd adProvider;

	// Token: 0x04000D85 RID: 3461
	public bool initiated;

	// Token: 0x04000D86 RID: 3462
	private float timeout;

	// Token: 0x04000D87 RID: 3463
	private int failed;

	// Token: 0x04000D88 RID: 3464
	private static string adType;

	// Token: 0x04000D89 RID: 3465
	public static string _debug = "";

	// Token: 0x04000D8A RID: 3466
	private static List<PowerButton> rewardPowers = new List<PowerButton>();

	// Token: 0x04000D8B RID: 3467
	private static List<PowerButton> unlockButtons = new List<PowerButton>();

	// Token: 0x04000D8C RID: 3468
	private static float rewardAdsQueue = 0f;
}
