using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class GoogleInterstitialAd : IWorldBoxAd
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0006BEAA File Offset: 0x0006A0AA
	// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0006BEB2 File Offset: 0x0006A0B2
	public Action adFailedCallback { get; set; }

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0006BEBB File Offset: 0x0006A0BB
	// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0006BEC3 File Offset: 0x0006A0C3
	public Action adFinishedCallback { get; set; }

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0006BECC File Offset: 0x0006A0CC
	// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0006BED4 File Offset: 0x0006A0D4
	public Action adStartedCallback { get; set; }

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0006BEDD File Offset: 0x0006A0DD
	private string getInterstitialAdUnitID()
	{
		if (Config.isAndroid)
		{
			if (Config.testAds)
			{
				return "ca-app-pub-3940256099942544/1033173712";
			}
			return "ca-app-pub-8168183924385686/1164653801";
		}
		else
		{
			if (!Config.isIos)
			{
				return "unexpected_platform";
			}
			if (Config.testAds)
			{
				return "ca-app-pub-3940256099942544/4411468910";
			}
			return "ca-app-pub-8168183924385686/5197120350";
		}
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0006BF18 File Offset: 0x0006A118
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
		string interstitialAdUnitID = this.getInterstitialAdUnitID();
		this.KillAd();
		this.interstitial = new InterstitialAd(interstitialAdUnitID);
		this.interstitial.OnAdLoaded += this.HandleOnAdLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleOnAdOpened;
		this.interstitial.OnAdClosed += this.HandleOnAdClosed;
		this.interstitial.OnPaidEvent += this.HandleOnPaidEvent;
		AdRequest adRequest;
		if (Config.testAds)
		{
			Debug.Log("REQUEST AD MOB interstitial TEST");
			adRequest = new AdRequest.Builder().Build();
			List<string> list = new List<string>();
			list.Add("38469EF1320047F75C548E8477B3583B");
			List<string> testDeviceIds = list;
			MobileAds.SetRequestConfiguration(new RequestConfiguration.Builder().SetTestDeviceIds(testDeviceIds).build());
		}
		else
		{
			Debug.Log("REQUEST AD MOB interstitial");
			adRequest = new AdRequest.Builder().Build();
		}
		this.interstitial.LoadAd(adRequest);
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0006C026 File Offset: 0x0006A226
	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0006C032 File Offset: 0x0006A232
	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.LoadAdError.GetMessage());
		this.KillAd();
		if (this.adFailedCallback != null)
		{
			this.adFailedCallback();
		}
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0006C067 File Offset: 0x0006A267
	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
		if (this.adStartedCallback != null)
		{
			this.adStartedCallback();
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0006C086 File Offset: 0x0006A286
	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
		this.KillAd();
		if (this.adFinishedCallback != null)
		{
			this.adFinishedCallback();
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0006C0AC File Offset: 0x0006A2AC
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
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0006C13C File Offset: 0x0006A33C
	public void KillAd()
	{
		if (this.interstitial == null)
		{
			return;
		}
		this.interstitial.OnAdLoaded -= this.HandleOnAdLoaded;
		this.interstitial.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;
		this.interstitial.OnAdOpening -= this.HandleOnAdOpened;
		this.interstitial.OnAdClosed -= this.HandleOnAdClosed;
		this.interstitial.OnPaidEvent -= this.HandleOnPaidEvent;
		this.interstitial.Destroy();
		this.interstitial = null;
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0006C1D7 File Offset: 0x0006A3D7
	public bool IsReady()
	{
		return this.interstitial != null && this.interstitial.IsLoaded();
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0006C1EE File Offset: 0x0006A3EE
	public void ShowAd()
	{
		if (this.IsReady())
		{
			this.interstitial.Show();
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0006C203 File Offset: 0x0006A403
	public bool HasAd()
	{
		return this.interstitial != null;
	}

	// Token: 0x04000D55 RID: 3413
	private InterstitialAd interstitial;
}
