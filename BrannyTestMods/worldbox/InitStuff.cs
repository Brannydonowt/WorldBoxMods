using System;
using System.Threading.Tasks;
using Facebook.Unity;
using Firebase;
using Firebase.Analytics;
using GoogleMobileAds.Api;
using Proyecto26;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class InitStuff : MonoBehaviour
{
	// Token: 0x060009E2 RID: 2530 RVA: 0x00065EF7 File Offset: 0x000640F7
	private void Start()
	{
		InitStuff.initSteam();
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00065F00 File Offset: 0x00064100
	public static void initOnlineServices()
	{
		if (InitStuff.initiated)
		{
			return;
		}
		InitStuff.initiated = true;
		try
		{
			RestClient.DefaultRequestHeaders["salt"] = (global::RequestHelper.salt ?? "na");
			RestClient.DefaultRequestHeaders["wb-version"] = (Application.version ?? "na");
			RestClient.DefaultRequestHeaders["wb-identifier"] = (Application.identifier ?? "na");
			RestClient.DefaultRequestHeaders["wb-platform"] = (Application.platform.ToString() ?? "na");
			RestClient.DefaultRequestHeaders["wb-language"] = (LocalizedTextManager.instance.language ?? "na");
			RestClient.DefaultRequestHeaders["wb-prem"] = (Config.havePremium ? "y" : "n");
			RestClient.DefaultRequestHeaders["wb-build"] = (Config.versionCodeText ?? "na");
		}
		catch (Exception ex)
		{
			Debug.Log("RestClient initialization Error");
			Debug.LogError(ex.Message);
		}
		try
		{
			if (Config.firebaseEnabled)
			{
				InitStuff.initFirebase();
			}
		}
		catch (Exception ex2)
		{
			Debug.Log("Firebase Init Error");
			Debug.LogError(ex2.Message);
		}
		try
		{
			VersionCheck.checkVersion();
		}
		catch (Exception ex3)
		{
			Debug.Log("Version Error");
			Debug.LogError(ex3.Message);
		}
		InitStuff.initDiscord();
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00066088 File Offset: 0x00064288
	private void Update()
	{
		if (this.servicesInitTimeOut > 0f)
		{
			this.servicesInitTimeOut -= Time.fixedDeltaTime;
			if (this.servicesInitTimeOut < 0f)
			{
				InitStuff.initOnlineServices();
			}
		}
		if (this.adsInitTimeOut > 0f)
		{
			this.adsInitTimeOut -= Time.fixedDeltaTime;
			if (this.adsInitTimeOut < 0f)
			{
				InitStuff.initGoogleMobileAds();
			}
		}
		if (this.elapsedSeconds <= InitStuff.targetSeconds)
		{
			this.elapsedSeconds += Time.deltaTime;
			return;
		}
		this.elapsedSeconds = 0f;
		try
		{
			if (Config.havePremium || InitStuff.targetSeconds != 900f)
			{
				VersionCheck.checkVersion();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00066150 File Offset: 0x00064350
	internal static void initGoogleMobileAds()
	{
		if (Config.isMobile && !Config.havePremium)
		{
			try
			{
				Debug.Log("[ADS] Initializing");
				MobileAds.Initialize(delegate(InitializationStatus initStatus)
				{
					Debug.Log("[ADS] Initialized");
					Config.adsInitialized = true;
				});
			}
			catch (Exception message)
			{
				Debug.Log("[ADS] Could not initialize ads");
				Debug.LogError(message);
			}
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x000661BC File Offset: 0x000643BC
	private static void initFirebase()
	{
		Debug.Log("Firebase init");
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(delegate(Task<DependencyStatus> task)
		{
			Debug.Log("Firebase task status");
			DependencyStatus result = task.Result;
			if (result == null)
			{
				try
				{
					Config.firebaseAvailable = true;
					FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
					Debug.Log("Firebase loaded");
					FirebaseAnalytics.LogEvent("data", "installerName", Config.iname ?? "");
					FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri("https://worldbox-g.firebaseio.com/");
					if (Config.authEnabled)
					{
						Auth.initializeAuth();
					}
					return;
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message);
					Config.authEnabled = false;
					Config.firebaseAvailable = false;
					return;
				}
			}
			Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", result));
		});
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x000661F4 File Offset: 0x000643F4
	private static void initDiscord()
	{
		if (Config.disableDiscord)
		{
			return;
		}
		GameObject gameObject = new GameObject("Discord");
		gameObject.hideFlags = HideFlags.DontSave;
		Object.DontDestroyOnLoad(gameObject);
		gameObject.AddComponent<DiscordTracker>();
		gameObject.transform.SetParent(GameObject.Find("Services").transform);
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x00066244 File Offset: 0x00064444
	internal static void initSteam()
	{
		if (Config.disableSteam)
		{
			return;
		}
		GameObject gameObject = new GameObject("Steam");
		gameObject.hideFlags = HideFlags.DontSave;
		Object.DontDestroyOnLoad(gameObject);
		gameObject.AddComponent<SteamSDK>();
		gameObject.transform.SetParent(GameObject.Find("Services").transform);
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x00066294 File Offset: 0x00064494
	private static void initFB()
	{
		try
		{
			if (FB.IsInitialized)
			{
				FB.ActivateApp();
			}
			else
			{
				FB.Init(delegate()
				{
					FB.ActivateApp();
				}, null, null);
			}
		}
		catch (Exception message)
		{
			Debug.Log("Facebook initFB Error");
			Debug.LogError(message);
		}
	}

	// Token: 0x04000C6D RID: 3181
	private static bool initiated = false;

	// Token: 0x04000C6E RID: 3182
	private float elapsedSeconds;

	// Token: 0x04000C6F RID: 3183
	public static float targetSeconds = 900f;

	// Token: 0x04000C70 RID: 3184
	private float servicesInitTimeOut = 3f;

	// Token: 0x04000C71 RID: 3185
	private float adsInitTimeOut = 45f;
}
