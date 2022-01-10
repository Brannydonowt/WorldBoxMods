using System;
using System.Collections.Generic;
using System.IO;
using Firebase.Analytics;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class PlayerConfig
{
	// Token: 0x06000A26 RID: 2598 RVA: 0x00067841 File Offset: 0x00065A41
	public static void init()
	{
		if (PlayerConfig.instance != null)
		{
			return;
		}
		Debug.Log("INIT PlayerConfig");
		PlayerConfig.instance = new PlayerConfig();
		PlayerConfig.instance.create();
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0006786C File Offset: 0x00065A6C
	public void create()
	{
		PlayerConfig.instance = this;
		this.rewardCheckTimer = this.rewardCheckTimerInterval;
		this.setNewDataPath();
		Debug.Log("Init PlayerConfig ");
		if (File.Exists(this.dataPath))
		{
			try
			{
				this.loadData();
				return;
			}
			catch (Exception)
			{
				this.initNewSave();
				return;
			}
		}
		this.initNewSave();
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x000678D0 File Offset: 0x00065AD0
	internal void start()
	{
		AdButtonTimer.setAdTimer();
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x000678D7 File Offset: 0x00065AD7
	internal void update()
	{
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x000678D9 File Offset: 0x00065AD9
	private void setNewDataPath()
	{
		this.dataPath = Application.persistentDataPath + "/worldboxData";
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x000678F0 File Offset: 0x00065AF0
	private void initNewSave()
	{
		this.data = new PlayerConfigData();
		this.data.initData();
		PlayerConfig.dict["language"].stringVal = PlayerConfig.detectLanguage();
		Config.steamLanguageAllowDetect = true;
		PlayerConfig.saveData();
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0006792C File Offset: 0x00065B2C
	public static bool unlockAchievement(string pName)
	{
		if (PlayerConfig.instance == null)
		{
			return false;
		}
		if (PlayerConfig.instance.data.achievements.Contains(pName))
		{
			return false;
		}
		PlayerConfig.instance.data.achievements.Add(pName);
		PlayerConfig.instance.data.achievements_hashset.Add(pName);
		PlayerConfig.saveData();
		return true;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0006798C File Offset: 0x00065B8C
	public static bool isAchievementUnlocked(string pName)
	{
		return PlayerConfig.instance != null && PlayerConfig.instance.data.achievements_hashset.Contains(pName);
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x000679AC File Offset: 0x00065BAC
	public static void setFirebaseProp(string pName, string pProp)
	{
		if (!Config.isMobile)
		{
			return;
		}
		if (!Config.firebaseAvailable)
		{
			return;
		}
		FirebaseAnalytics.SetUserProperty(pName, pProp);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x000679C5 File Offset: 0x00065BC5
	public static void toggleFullScreen()
	{
		PlayerConfig.setFullScreen(!PlayerConfig.dict["fullscreen"].boolVal, true);
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x000679E4 File Offset: 0x00065BE4
	public static void setFullScreen(bool fullScreen, bool switchScreen = true)
	{
		PlayerConfig.dict["fullscreen"].boolVal = fullScreen;
		PlayerConfig.saveData();
		if (switchScreen)
		{
			PlayerConfig.checkSettings();
		}
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x00067A08 File Offset: 0x00065C08
	public static string detectLanguage()
	{
		switch (Application.systemLanguage)
		{
		case SystemLanguage.Arabic:
			return "ar";
		case SystemLanguage.Czech:
			return "cs";
		case SystemLanguage.Danish:
			return "da";
		case SystemLanguage.Dutch:
			return "nl";
		case SystemLanguage.English:
			return "en";
		case SystemLanguage.Finnish:
			return "fn";
		case SystemLanguage.French:
			return "fr";
		case SystemLanguage.German:
			return "de";
		case SystemLanguage.Greek:
			return "gr";
		case SystemLanguage.Hebrew:
			return "he";
		case SystemLanguage.Hungarian:
			return "hu";
		case SystemLanguage.Indonesian:
			return "id";
		case SystemLanguage.Italian:
			return "it";
		case SystemLanguage.Japanese:
			return "ja";
		case SystemLanguage.Korean:
			return "ko";
		case SystemLanguage.Norwegian:
			return "no";
		case SystemLanguage.Polish:
			return "pl";
		case SystemLanguage.Portuguese:
			return "pt";
		case SystemLanguage.Romanian:
			return "ro";
		case SystemLanguage.Russian:
			return "ru";
		case SystemLanguage.SerboCroatian:
			return "hr";
		case SystemLanguage.Slovak:
			return "sk";
		case SystemLanguage.Spanish:
			return "es";
		case SystemLanguage.Swedish:
			return "sv";
		case SystemLanguage.Thai:
			return "th";
		case SystemLanguage.Turkish:
			return "tr";
		case SystemLanguage.Ukrainian:
			return "ua";
		case SystemLanguage.Vietnamese:
			return "vn";
		case SystemLanguage.ChineseSimplified:
			return "cz";
		case SystemLanguage.ChineseTraditional:
			return "ch";
		}
		return "en";
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00067BF4 File Offset: 0x00065DF4
	public static void saveData()
	{
		string text = Toolbox.encode(JsonUtility.ToJson(PlayerConfig.instance.data));
		File.WriteAllText(PlayerConfig.instance.dataPath, text);
		foreach (PlayerOptionData playerOptionData in PlayerConfig.dict.Values)
		{
			if (playerOptionData.boolVal)
			{
				PlayerConfig.setFirebaseProp("option_" + playerOptionData.name, playerOptionData.boolVal ? "on" : "off");
			}
		}
		PlayerConfig.setFirebaseProp("option_language", PlayerConfig.dict["language"].stringVal);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x00067CB8 File Offset: 0x00065EB8
	private void loadData()
	{
		if (!File.Exists(this.dataPath))
		{
			return;
		}
		string text = File.ReadAllText(this.dataPath);
		if (text.Contains("list"))
		{
			Debug.Log("Load old... \n" + text);
		}
		else
		{
			try
			{
				text = Toolbox.decode(text);
			}
			catch (Exception)
			{
				text = "";
			}
		}
		if (text.Contains("list"))
		{
			this.data = JsonUtility.FromJson<PlayerConfigData>(text);
			this.data.initData();
			if (this.data.get("language").stringVal == "boat")
			{
				this.data.get("language").stringVal = PlayerConfig.detectLanguage();
			}
		}
		else
		{
			this.initNewSave();
		}
		if (this.data.fireworksCheck0133)
		{
			Config.EVERYTHING_FIREWORKS = true;
		}
		if (this.data.magicCheck0133)
		{
			Config.EVERYTHING_MAGIC_COLOR = true;
		}
		if (this.data.premium)
		{
			Config.havePremium = true;
		}
		foreach (string text2 in this.data.achievements)
		{
			this.data.achievements_hashset.Add(text2);
		}
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00067E14 File Offset: 0x00066014
	internal static bool optionEnabled(string gameOptionName, OptionType pType)
	{
		foreach (PlayerOptionData playerOptionData in PlayerConfig.instance.data.list)
		{
			if (!(playerOptionData.name != gameOptionName) && pType == OptionType.Bool)
			{
				return playerOptionData.boolVal;
			}
		}
		return false;
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00067E88 File Offset: 0x00066088
	internal static bool optionBoolEnabled(string pName)
	{
		return PlayerConfig.dict[pName].boolVal;
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00067E9C File Offset: 0x0006609C
	internal static void switchOption(string gameOptionName, OptionType pType)
	{
		foreach (PlayerOptionData playerOptionData in PlayerConfig.instance.data.list)
		{
			if (!(playerOptionData.name != gameOptionName) && pType == OptionType.Bool)
			{
				playerOptionData.boolVal = !playerOptionData.boolVal;
			}
		}
		PlayerConfig.checkSettings();
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00067F18 File Offset: 0x00066118
	public static void switchVsync()
	{
		PlayerConfig.dict["vsync"].boolVal = !PlayerConfig.dict["vsync"].boolVal;
		PlayerConfig.setVsync(PlayerConfig.dict["vsync"].boolVal);
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00067F69 File Offset: 0x00066169
	public static void checkVsync()
	{
		PlayerConfig.setVsync(PlayerConfig.dict["vsync"].boolVal);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00067F84 File Offset: 0x00066184
	public static void setVsync(bool vsyncEnabled)
	{
		if (!vsyncEnabled)
		{
			QualitySettings.vSyncCount = 0;
			if (Application.targetFrameRate != 60)
			{
				Application.targetFrameRate = 60;
			}
			return;
		}
		if (Screen.currentResolution.refreshRate < 61)
		{
			QualitySettings.vSyncCount = 1;
			return;
		}
		if (Screen.currentResolution.refreshRate < 121)
		{
			QualitySettings.vSyncCount = 2;
			return;
		}
		if (Screen.currentResolution.refreshRate < 181)
		{
			QualitySettings.vSyncCount = 3;
			return;
		}
		QualitySettings.vSyncCount = 4;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00067FFC File Offset: 0x000661FC
	internal static void checkSettings()
	{
		Config.shadowsActive = PlayerConfig.dict["shadows"].boolVal;
		Config.tooltipsActive = PlayerConfig.dict["tooltips"].boolVal;
		Config.experimentalMode = PlayerConfig.dict["experimental"].boolVal;
		Config.wbb_confirmed = PlayerConfig.dict["wbb_confirmed"].boolVal;
		if (PlayerConfig.dict["portrait"].boolVal)
		{
			Config.setPortrait(true);
		}
		else
		{
			Config.setPortrait(false);
		}
		if (Config.isComputer)
		{
			Config.fullScreen = PlayerConfig.dict["fullscreen"].boolVal;
			if (Config.fullScreen)
			{
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true, Screen.currentResolution.refreshRate);
			}
			else
			{
				Screen.fullScreen = false;
			}
			PlayerConfig.setVsync(PlayerConfig.dict["vsync"].boolVal);
		}
		MapBox.instance.sleekRenderSettings.vignetteEnabled = PlayerConfig.dict["vignette"].boolVal;
		MapBox.instance.sleekRenderSettings.bloomEnabled = PlayerConfig.dict["bloom"].boolVal;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0006814C File Offset: 0x0006634C
	public static int countRewards()
	{
		PlayerConfig playerConfig = PlayerConfig.instance;
		bool flag;
		if (playerConfig == null)
		{
			flag = (null != null);
		}
		else
		{
			PlayerConfigData playerConfigData = playerConfig.data;
			flag = (((playerConfigData != null) ? playerConfigData.rewardedPowers : null) != null);
		}
		if (flag)
		{
			return PlayerConfig.instance.data.rewardedPowers.Count;
		}
		return 0;
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00068183 File Offset: 0x00066383
	public static void clearRewards()
	{
		PlayerConfig playerConfig = PlayerConfig.instance;
		if (playerConfig == null)
		{
			return;
		}
		PlayerConfigData playerConfigData = playerConfig.data;
		if (playerConfigData == null)
		{
			return;
		}
		List<RewardedPower> rewardedPowers = playerConfigData.rewardedPowers;
		if (rewardedPowers == null)
		{
			return;
		}
		rewardedPowers.Clear();
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x000681A8 File Offset: 0x000663A8
	public void clearAchievements()
	{
		this.data.achievements.Clear();
		PlayerConfig.saveData();
	}

	// Token: 0x04000CB4 RID: 3252
	public static PlayerConfig instance;

	// Token: 0x04000CB5 RID: 3253
	public static Dictionary<string, PlayerOptionData> dict = new Dictionary<string, PlayerOptionData>();

	// Token: 0x04000CB6 RID: 3254
	private string dataPath;

	// Token: 0x04000CB7 RID: 3255
	internal PlayerConfigData data;

	// Token: 0x04000CB8 RID: 3256
	private float rewardCheckTimer = 10f;

	// Token: 0x04000CB9 RID: 3257
	private float rewardCheckTimerInterval = 60f;
}
