using System;

// Token: 0x020001E3 RID: 483
public interface IWorldBoxAd
{
	// Token: 0x06000AF9 RID: 2809
	void RequestAd();

	// Token: 0x06000AFA RID: 2810
	void KillAd();

	// Token: 0x06000AFB RID: 2811
	bool IsReady();

	// Token: 0x06000AFC RID: 2812
	void ShowAd();

	// Token: 0x06000AFD RID: 2813
	bool HasAd();

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000AFE RID: 2814
	// (set) Token: 0x06000AFF RID: 2815
	Action adFailedCallback { get; set; }

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000B00 RID: 2816
	// (set) Token: 0x06000B01 RID: 2817
	Action adFinishedCallback { get; set; }

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000B02 RID: 2818
	// (set) Token: 0x06000B03 RID: 2819
	Action adStartedCallback { get; set; }
}
