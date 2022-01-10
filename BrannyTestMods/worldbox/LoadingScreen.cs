using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200025C RID: 604
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x06000D19 RID: 3353 RVA: 0x0007D5F2 File Offset: 0x0007B7F2
	private void Start()
	{
		this.lastBgHeight = -1f;
		this.setupBg();
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0007D608 File Offset: 0x0007B808
	private void setupBg()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		if (this.lastBgHeight == num2 && this.lastBgWidth == num && this.canvas.scaleFactor == this.lastCScale)
		{
			return;
		}
		this.lastBgWidth = num;
		this.lastBgHeight = num2;
		this.lastCScale = this.canvas.scaleFactor;
		float num3 = (float)this.background.mainTexture.width * this.canvas.scaleFactor;
		float num4 = (float)this.background.mainTexture.height * this.canvas.scaleFactor;
		float num5 = (float)Screen.width / num3;
		float num6 = (float)Screen.height / num4;
		if (num5 > num6)
		{
			this.background.transform.localScale = new Vector3(num5, num5, 1f);
			return;
		}
		this.background.transform.localScale = new Vector3(num6, num6, 1f);
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0007D6FC File Offset: 0x0007B8FC
	private void Awake()
	{
		PlayerConfig.init();
		LocalizedTextManager.init();
		Config.enableAutoRotation(false);
		base.transform.localPosition = default(Vector3);
		if (this.inGameScreen)
		{
			this.outTimer = 0.3f;
			this.canvasGroup.alpha = 1f;
			this.appearDone = true;
			this.bar.transform.localScale = new Vector3(1f, 1f, 1f);
			return;
		}
		this.canvasGroup.alpha = 0f;
		this.bar.transform.localScale = new Vector3(0f, 1f, 1f);
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0007D7B0 File Offset: 0x0007B9B0
	private void startAction()
	{
		ScrollWindow.hideAllEvent(false);
		this.modeIn = false;
		if (Config.isMobile && !Config.havePremium)
		{
			Debug.Log("PremiumElementsChecker.goodForInterstitialAd(): " + PremiumElementsChecker.goodForInterstitialAd().ToString());
			if (PremiumElementsChecker.goodForInterstitialAd())
			{
				if (PlayInterstitialAd.instance.isReady())
				{
					PlayInterstitialAd.instance.showAd();
					PremiumElementsChecker.setInterstitialAdTimer(100);
				}
				else
				{
					PlayInterstitialAd.instance.initAds();
				}
			}
		}
		this.action();
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x0007D838 File Offset: 0x0007BA38
	internal void startTransition(LoadingScreen.TransitionAction pAction)
	{
		Config.enableAutoRotation(false);
		this.lastBgHeight = -1f;
		this.setupBg();
		this.action = pAction;
		this.bar.gameObject.SetActive(false);
		this.percents.gameObject.SetActive(false);
		this.topText.gameObject.SetActive(false);
		this.tipText.gameObject.SetActive(false);
		this.mask.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		this.canvasGroup.alpha = 0f;
		this.modeIn = true;
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0007D8DC File Offset: 0x0007BADC
	private void OnEnable()
	{
		this.lastBgHeight = -1f;
		this.setupBg();
		string key = "Loading Screen " + Random.Range(1, 22).ToString();
		this.topText.key = key;
		this.tipText.key = this.getTipID();
		this.topText.updateText(true);
		this.tipText.updateText(true);
		this.topText.gameObject.SetActive(true);
		this.tipText.gameObject.SetActive(true);
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0007D96C File Offset: 0x0007BB6C
	private string getTipID()
	{
		int num = Toolbox.randomInt(0, 17);
		string str;
		if (num <= 9)
		{
			str = "00" + num.ToString();
		}
		else
		{
			str = "0" + num.ToString();
		}
		return "tip" + str;
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0007D9C0 File Offset: 0x0007BBC0
	private void Update()
	{
		this.setupBg();
		if (!this.inGameScreen)
		{
			if (!this.appearDone)
			{
				this.canvasGroup.alpha += Time.deltaTime;
				if (this.canvasGroup.alpha < 1f)
				{
					return;
				}
				this.appearDone = true;
				base.StartCoroutine(this.LoadGame());
			}
			float num = this.bar.transform.localScale.x;
			if (this.bar.transform.localScale.x < this.asyncLoad.progress)
			{
				num = this.bar.transform.localScale.x + Time.deltaTime * 2f;
				if (num > this.asyncLoad.progress)
				{
					num = this.asyncLoad.progress;
				}
				this.bar.transform.localScale = new Vector3(num, 1f, 1f);
			}
			this.percents.text = Mathf.CeilToInt(this.asyncLoad.progress * 100f).ToString() + " %";
			Config.showStartupWindow = true;
			if (num >= 0.9f)
			{
				if (!this.asyncLoad.allowSceneActivation)
				{
					Analytics.LogEvent("preloading_done", true, true);
				}
				this.asyncLoad.allowSceneActivation = true;
			}
			return;
		}
		if (this.modeIn)
		{
			if (this.canvasGroup.alpha >= 1f)
			{
				this.startAction();
			}
			this.canvasGroup.alpha += Time.deltaTime * 2f;
			return;
		}
		if (this.outTimer > 0f)
		{
			this.outTimer -= Time.deltaTime;
			return;
		}
		if (this.canvasGroup.alpha <= 0f)
		{
			Config.enableAutoRotation(true);
			base.gameObject.SetActive(false);
		}
		if (!Config.worldLoading)
		{
			this.canvasGroup.alpha -= Time.fixedDeltaTime * 2f;
		}
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0007DBC7 File Offset: 0x0007BDC7
	private IEnumerator LoadGame()
	{
		this.asyncLoad = SceneManager.LoadSceneAsync("World");
		this.asyncLoad.allowSceneActivation = false;
		while (!this.asyncLoad.isDone)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x04000FF3 RID: 4083
	public Image background;

	// Token: 0x04000FF4 RID: 4084
	public CanvasGroup canvasGroup;

	// Token: 0x04000FF5 RID: 4085
	public Text percents;

	// Token: 0x04000FF6 RID: 4086
	public LocalizedText topText;

	// Token: 0x04000FF7 RID: 4087
	public LocalizedText tipText;

	// Token: 0x04000FF8 RID: 4088
	public Image bar;

	// Token: 0x04000FF9 RID: 4089
	public Image mask;

	// Token: 0x04000FFA RID: 4090
	private AsyncOperation asyncLoad;

	// Token: 0x04000FFB RID: 4091
	private bool appearDone;

	// Token: 0x04000FFC RID: 4092
	public bool inGameScreen;

	// Token: 0x04000FFD RID: 4093
	internal bool modeIn;

	// Token: 0x04000FFE RID: 4094
	public LoadingScreen.TransitionAction action;

	// Token: 0x04000FFF RID: 4095
	private float outTimer;

	// Token: 0x04001000 RID: 4096
	public Canvas canvas;

	// Token: 0x04001001 RID: 4097
	private float originalTextTopPosition;

	// Token: 0x04001002 RID: 4098
	private float lastBgWidth;

	// Token: 0x04001003 RID: 4099
	private float lastBgHeight;

	// Token: 0x04001004 RID: 4100
	private float lastCScale;

	// Token: 0x04001005 RID: 4101
	public bool debugg;

	// Token: 0x020003FF RID: 1023
	// (Invoke) Token: 0x06001641 RID: 5697
	public delegate void TransitionAction();
}
