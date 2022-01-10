using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class GoogleRewardAd : IWorldBoxAd
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0006C216 File Offset: 0x0006A416
	// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0006C21E File Offset: 0x0006A41E
	public Action adFailedCallback { get; set; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0006C227 File Offset: 0x0006A427
	// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0006C22F File Offset: 0x0006A42F
	public Action adFinishedCallback { get; set; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0006C238 File Offset: 0x0006A438
	// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x0006C240 File Offset: 0x0006A440
	public Action adStartedCallback { get; set; }

	// Token: 0x06000AEA RID: 2794 RVA: 0x0006C249 File Offset: 0x0006A449
	private string getRewardAdUnitID()
	{
		if (Config.isAndroid)
		{
			if (Config.testAds)
			{
				return "ca-app-pub-3940256099942544/5224354917";
			}
			return "ca-app-pub-8168183924385686/4994877784";
		}
		else
		{
			if (!Config.isIos)
			{
				return "unexpected_platform";
			}
			if (Config.testAds)
			{
				return "ca-app-pub-3940256099942544/1712485313";
			}
			return "ca-app-pub-8168183924385686/1330964944";
		}
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0006C284 File Offset: 0x0006A484
	public void RequestAd()
	{
		if (!Config.isMobile)
		{
			return;
		}
		if (Config.havePremium)
		{
			return;
		}
		if (this.rewardBasedVideo != null && !this.started)
		{
			return;
		}
		this.KillAd();
		this.started = false;
		this.rewardBasedVideo = new RewardedAd(this.getRewardAdUnitID());
		this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
		this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
		this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
		this.rewardBasedVideo.OnAdFailedToShow += this.HandleRewardedAdFailedToShow;
		this.rewardBasedVideo.OnUserEarnedReward += this.HandleRewardBasedVideoRewarded;
		this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
		this.rewardBasedVideo.OnPaidEvent += this.HandleOnPaidEvent;
		AdRequest adRequest;
		if (Config.testAds)
		{
			Debug.Log("REQUEST AD MOB VIDEO TEST");
			adRequest = new AdRequest.Builder().Build();
			List<string> list = new List<string>();
			list.Add("38469EF1320047F75C548E8477B3583B");
			List<string> testDeviceIds = list;
			MobileAds.SetRequestConfiguration(new RequestConfiguration.Builder().SetTestDeviceIds(testDeviceIds).build());
		}
		else
		{
			Debug.Log("REQUEST AD MOB VIDEO");
			adRequest = new AdRequest.Builder().Build();
		}
		this.rewardBasedVideo.LoadAd(adRequest);
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0006C3D6 File Offset: 0x0006A5D6
	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
		RewardedAds.debug += "h1_";
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0006C3F8 File Offset: 0x0006A5F8
	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.LoadAdError.GetMessage());
		this.started = true;
		RewardedAds.debug += "h2_";
		this.KillAd();
		if (this.adFailedCallback != null)
		{
			this.adFailedCallback();
		}
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0006C453 File Offset: 0x0006A653
	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
		this.started = true;
		RewardedAds.debug += "h3_";
		if (this.adStartedCallback != null)
		{
			this.adStartedCallback();
		}
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0006C490 File Offset: 0x0006A690
	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: " + args.AdError.GetMessage());
		this.started = true;
		RewardedAds.debug += "h4_";
		this.KillAd();
		if (this.adFailedCallback != null)
		{
			this.adFailedCallback();
		}
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0006C4EC File Offset: 0x0006A6EC
	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoRewarded");
		this.started = true;
		if (MapBox.instance != null)
		{
			MonoBehaviour.print("is worldbox on focus " + MapBox.instance.hasFocus.ToString());
		}
		RewardedAds.instance.handleRewards();
		RewardedAds.debug += "h5_";
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0006C553 File Offset: 0x0006A753
	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
		this.started = true;
		RewardedAds.debug += "h6_";
		this.KillAd();
		if (this.adFinishedCallback != null)
		{
			this.adFinishedCallback();
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0006C594 File Offset: 0x0006A794
	public void HandleOnPaidEvent(object sender, AdValueEventArgs args)
	{
		MonoBehaviour.print("Rewarded interstitial ad has received a paid event. " + args.AdValue.ToString());
		MonoBehaviour.print(string.Concat(new string[]
		{
			"Values: ",
			args.AdValue.Precision.ToString(),
			" ",
			args.AdValue.Value.ToString(),
			" ",
			args.AdValue.CurrencyCode
		}));
		this.started = true;
		RewardedAds.debug += "h7_";
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0006C640 File Offset: 0x0006A840
	public void KillAd()
	{
		if (this.rewardBasedVideo == null)
		{
			return;
		}
		if (!this.started)
		{
			return;
		}
		this.rewardBasedVideo.OnAdLoaded -= this.HandleRewardBasedVideoLoaded;
		this.rewardBasedVideo.OnAdFailedToLoad -= this.HandleRewardBasedVideoFailedToLoad;
		this.rewardBasedVideo.OnAdOpening -= this.HandleRewardBasedVideoOpened;
		this.rewardBasedVideo.OnAdFailedToShow -= this.HandleRewardedAdFailedToShow;
		this.rewardBasedVideo.OnUserEarnedReward -= this.HandleRewardBasedVideoRewarded;
		this.rewardBasedVideo.OnAdClosed -= this.HandleRewardBasedVideoClosed;
		this.rewardBasedVideo.OnPaidEvent -= this.HandleOnPaidEvent;
		this.rewardBasedVideo.Destroy();
		this.rewardBasedVideo = null;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0006C712 File Offset: 0x0006A912
	public bool IsReady()
	{
		return this.rewardBasedVideo != null && this.rewardBasedVideo.IsLoaded();
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0006C729 File Offset: 0x0006A929
	public void ShowAd()
	{
		if (this.IsReady())
		{
			this.started = true;
			this.rewardBasedVideo.Show();
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0006C745 File Offset: 0x0006A945
	public bool HasAd()
	{
		return this.rewardBasedVideo != null;
	}

	// Token: 0x04000D59 RID: 3417
	private RewardedAd rewardBasedVideo;

	// Token: 0x04000D5D RID: 3421
	private bool started;
}
