using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.CrashReportHandler;

// Token: 0x02000085 RID: 133
[ObfuscateLiterals]
public class Config
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x060002D8 RID: 728 RVA: 0x000317A9 File Offset: 0x0002F9A9
	// (set) Token: 0x060002D7 RID: 727 RVA: 0x000317A1 File Offset: 0x0002F9A1
	public static bool havePremium
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return Config._hpr;
		}
		set
		{
			Config._hpr = value;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x060002D9 RID: 729 RVA: 0x000317B0 File Offset: 0x0002F9B0
	public static bool spectatorMode
	{
		get
		{
			return MoveCamera.focusUnit != null;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x060002DA RID: 730 RVA: 0x000317BD File Offset: 0x0002F9BD
	public static bool controllingUnit
	{
		get
		{
			return Config.controllableUnit != null;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x060002DB RID: 731 RVA: 0x000317CA File Offset: 0x0002F9CA
	public static bool joyControls
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x000317CD File Offset: 0x0002F9CD
	internal static string getAdmobAppID()
	{
		if (Config.isAndroid)
		{
			return "ca-app-pub-8168183924385686~1706369463";
		}
		if (Config.isIos)
		{
			return "ca-app-pub-8168183924385686~8080206125";
		}
		return "unexpected_platform";
	}

	// Token: 0x060002DD RID: 733 RVA: 0x000317EE File Offset: 0x0002F9EE
	public static void setPortrait(bool pValue)
	{
		if (pValue)
		{
			if (Screen.orientation != ScreenOrientation.Portrait)
			{
				Screen.orientation = ScreenOrientation.Portrait;
				return;
			}
		}
		else if (Screen.orientation != ScreenOrientation.LandscapeLeft)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00031810 File Offset: 0x0002FA10
	public static void enableAutoRotation(bool pValue)
	{
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00031814 File Offset: 0x0002FA14
	public static void setControllableCreature(Actor pActor)
	{
		Config.controllableUnit = pActor;
		if (Config.controllingUnit)
		{
			Config.timeScale = 1f;
			Config.paused = false;
		}
		if (!Config.joyControls)
		{
			if (MapBox.instance.joys != null)
			{
				Object.Destroy(MapBox.instance.joys, 0.5f);
				MapBox.instance.joys = null;
			}
			return;
		}
		if (Config.controllingUnit)
		{
			MapBox.instance.joys.SetActive(true);
			return;
		}
		MapBox.instance.joys.SetActive(false);
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x000318A0 File Offset: 0x0002FAA0
	[Skip]
	[SkipRename]
	[DoNotFake]
	public static void updateCrashMetadata()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (Config.worldLoading)
		{
			return;
		}
		if (Config.skip)
		{
			return;
		}
		if (Config.timer > 0f)
		{
			Config.timer -= Time.fixedDeltaTime;
			return;
		}
		Config.timer = 5f;
		CrashReportHandler.enableCaptureExceptions = false;
		try
		{
			if (!string.IsNullOrEmpty(Config.versionCodeText))
			{
				CrashReportHandler.SetUserMetadata("u_versionCodeText", Config.versionCodeText);
			}
			CrashReportHandler.SetUserMetadata("c_MODDED", Config.MODDED.ToString());
			CrashReportHandler.SetUserMetadata("c_HAVE_PREMIUM", Config.havePremium.ToString());
			CrashReportHandler.SetUserMetadata("c_game_speed", Config.timeScale.ToString());
			CrashReportHandler.SetUserMetadata("c_sonic_speed", DebugConfig.isOn(DebugOption.SonicSpeed).ToString());
			CrashReportHandler.SetUserMetadata("c_show_map_names", MapBox.instance.showMapNames().ToString());
			if (DebugConfig.instance.debugButton.gameObject.activeSelf)
			{
				CrashReportHandler.SetUserMetadata("c_debug_button", "visible");
			}
			if (UnitSpriteConstructor.initiated)
			{
				CrashReportHandler.SetUserMetadata("o_sprite_constructor_sprites", UnitSpriteConstructor._sprites.Count.ToString());
				CrashReportHandler.SetUserMetadata("o_textures_boats", UnitSpriteConstructor.boats.textures.Count.ToString());
				CrashReportHandler.SetUserMetadata("o_textures_units", UnitSpriteConstructor.units.textures.Count.ToString());
			}
			CrashReportHandler.SetUserMetadata("o_camera_lowRes", MapBox.instance.qualityChanger.lowRes.ToString());
			CrashReportHandler.SetUserMetadata("o_selected_power", MapBox.instance.getSelectedPower());
			CrashReportHandler.SetUserMetadata("c_map_mode", MapBox.instance.zoneCalculator.getCurrentMode().ToString());
			if (ScrollWindow.isWindowActive())
			{
				CrashReportHandler.SetUserMetadata("o_window_open", ScrollWindow.currentWindows[0].screen_id);
			}
			else
			{
				CrashReportHandler.SetUserMetadata("o_window_open", "false");
			}
			string text = "";
			for (int i = 0; i < Config._loggedSelectedPowers.Count; i++)
			{
				text = text + Config._loggedSelectedPowers[i] + ",";
			}
			CrashReportHandler.SetUserMetadata("o_power_history", text);
			CrashReportHandler.SetUserMetadata("map_units", MapBox.instance.units.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_pop_points", MapBox.instance.countPopPoints().ToString());
			CrashReportHandler.SetUserMetadata("map_buildings", MapBox.instance.buildings.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_civ_kingdoms", MapBox.instance.kingdoms.list_civs.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_cultures", MapBox.instance.cultures.list.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_zones", MapBox.instance.zoneCalculator.zones.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_chunks", MapBox.instance.mapChunkManager.list.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_drops_active", MapBox.instance.dropManager.activeIndex.ToString());
			CrashReportHandler.SetUserMetadata("map_stackEffects_active", MapBox.instance.stackEffects.countActive().ToString());
		}
		catch (Exception message)
		{
			Config.skip = true;
			Debug.LogError(message);
			throw;
		}
		CrashReportHandler.enableCaptureExceptions = true;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00031C40 File Offset: 0x0002FE40
	public static void logSelectedPower(GodPower pPower)
	{
		if (Config._loggedSelectedPowers.Count > 10)
		{
			Config._loggedSelectedPowers.RemoveAt(0);
		}
		Config._loggedSelectedPowers.Add(pPower.id);
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00031C6B File Offset: 0x0002FE6B
	public static void magicCheck(bool pEnabled)
	{
		Config.EVERYTHING_MAGIC_COLOR = pEnabled;
		PlayerConfig.instance.data.magicCheck0133 = pEnabled;
		PlayerConfig.saveData();
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00031C88 File Offset: 0x0002FE88
	public static void fireworksCheck(bool pEnabled)
	{
		Config.EVERYTHING_FIREWORKS = pEnabled;
		PlayerConfig.instance.data.fireworksCheck0133 = pEnabled;
		PlayerConfig.saveData();
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00031CA5 File Offset: 0x0002FEA5
	public static void valCheck(bool pEnabled)
	{
		PlayerConfig.instance.data.valCheck = pEnabled;
		PlayerConfig.saveData();
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00031CBC File Offset: 0x0002FEBC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void pCheck(bool value)
	{
		PlayerConfig.instance.data.pPossible0133 = value;
		PlayerConfig.saveData();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00031CD3 File Offset: 0x0002FED3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void removePremium()
	{
		Config.havePremium = false;
		PlayerConfig.instance.data.premium = false;
		PlayerConfig.saveData();
		PremiumElementsChecker.checkElements();
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00031CF5 File Offset: 0x0002FEF5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void givePremium()
	{
		InAppManager.activatePrem(false);
	}

	// Token: 0x0400042C RID: 1068
	public static bool jobs_updater_parallel = true;

	// Token: 0x0400042D RID: 1069
	public static string gv;

	// Token: 0x0400042E RID: 1070
	public static string gs = "";

	// Token: 0x0400042F RID: 1071
	public static string versionCodeText = string.Empty;

	// Token: 0x04000430 RID: 1072
	public static string versionCodeDate = string.Empty;

	// Token: 0x04000431 RID: 1073
	public static string iname = string.Empty;

	// Token: 0x04000432 RID: 1074
	public static string testStreamingAssets = "test";

	// Token: 0x04000433 RID: 1075
	public static Actor controllableUnit = null;

	// Token: 0x04000434 RID: 1076
	public static bool worldLoading = false;

	// Token: 0x04000435 RID: 1077
	public static int WORLD_SAVE_VERSION = 9;

	// Token: 0x04000436 RID: 1078
	public static string customMapSize = "standard";

	// Token: 0x04000437 RID: 1079
	public static int customZoneX = 0;

	// Token: 0x04000438 RID: 1080
	public static int customZoneY = 0;

	// Token: 0x04000439 RID: 1081
	public static int customPerlinScale = 10;

	// Token: 0x0400043A RID: 1082
	public static int customRandomShapes = 10;

	// Token: 0x0400043B RID: 1083
	public static int customWaterLevel = 10;

	// Token: 0x0400043C RID: 1084
	public const int AGE_CAN_BE_PARENT = 18;

	// Token: 0x0400043D RID: 1085
	public const int AGE_BABY = 8;

	// Token: 0x0400043E RID: 1086
	public static int ZONE_AMOUNT_X = 4;

	// Token: 0x0400043F RID: 1087
	public static int ZONE_AMOUNT_Y = 4;

	// Token: 0x04000440 RID: 1088
	public static string customMapSizeDefault = "standard";

	// Token: 0x04000441 RID: 1089
	public static string maxMapSize = "iceberg";

	// Token: 0x04000442 RID: 1090
	public static int ZONE_AMOUNT_X_DEFAULT = 3;

	// Token: 0x04000443 RID: 1091
	public static int ZONE_AMOUNT_Y_DEFAULT = 4;

	// Token: 0x04000444 RID: 1092
	public const int MAP_BLOCK_SIZE = 64;

	// Token: 0x04000445 RID: 1093
	public const int CHUNK_SIZE = 8;

	// Token: 0x04000446 RID: 1094
	public const int TILES_IN_CHUNK = 64;

	// Token: 0x04000447 RID: 1095
	public const int CITY_ZONE_SIZE = 8;

	// Token: 0x04000448 RID: 1096
	public const int CITY_ZONE_TILES = 64;

	// Token: 0x04000449 RID: 1097
	public const int TILES_IN_REGION = 64;

	// Token: 0x0400044A RID: 1098
	public const int PREVIEW_MAP_SIZE = 512;

	// Token: 0x0400044B RID: 1099
	public static float timeScale = 1f;

	// Token: 0x0400044C RID: 1100
	public static bool test = false;

	// Token: 0x0400044D RID: 1101
	public static bool MODDED = false;

	// Token: 0x0400044E RID: 1102
	public static bool EVERYTHING_MAGIC_COLOR = false;

	// Token: 0x0400044F RID: 1103
	public static bool EVERYTHING_FIREWORKS = false;

	// Token: 0x04000450 RID: 1104
	public static bool paused = false;

	// Token: 0x04000451 RID: 1105
	public static bool lockGameControls = false;

	// Token: 0x04000452 RID: 1106
	internal static string steamName;

	// Token: 0x04000453 RID: 1107
	internal static string steamId;

	// Token: 0x04000454 RID: 1108
	internal static bool steamLanguageAllowDetect = true;

	// Token: 0x04000455 RID: 1109
	internal static string discordId;

	// Token: 0x04000456 RID: 1110
	internal static string discordName;

	// Token: 0x04000457 RID: 1111
	internal static string discordDiscriminator;

	// Token: 0x04000458 RID: 1112
	public static bool testAds = false;

	// Token: 0x04000459 RID: 1113
	public static bool fastCities = false;

	// Token: 0x0400045A RID: 1114
	public static float actorFastMode = 1f;

	// Token: 0x0400045B RID: 1115
	public static bool firebaseEnabled = true;

	// Token: 0x0400045C RID: 1116
	public static bool authEnabled = false;

	// Token: 0x0400045D RID: 1117
	public const string firebaseDatabaseURL = "https://worldbox-g.firebaseio.com/";

	// Token: 0x0400045E RID: 1118
	public const string baseURL = "https://versions.superworldbox.com";

	// Token: 0x0400045F RID: 1119
	public const string currencyURL = "https://currency.superworldbox.com";

	// Token: 0x04000460 RID: 1120
	public static bool adsInitialized = false;

	// Token: 0x04000461 RID: 1121
	public static bool disableDiscord = false;

	// Token: 0x04000462 RID: 1122
	public static bool disableSteam = false;

	// Token: 0x04000463 RID: 1123
	public static bool ignoreStartupWindow = false;

	// Token: 0x04000464 RID: 1124
	public static bool loadSaveOnStart = false;

	// Token: 0x04000465 RID: 1125
	public static bool loadTestMap = false;

	// Token: 0x04000466 RID: 1126
	public static int loadSaveOnStartSlot = 1;

	// Token: 0x04000467 RID: 1127
	public static float LOAD_TIME_INIT = 0f;

	// Token: 0x04000468 RID: 1128
	public static float LOAD_TIME_CREATE = 0f;

	// Token: 0x04000469 RID: 1129
	public static float LOAD_TIME_GENERATE = 0f;

	// Token: 0x0400046A RID: 1130
	private static bool _hpr = false;

	// Token: 0x0400046B RID: 1131
	public static bool debug_ruins = false;

	// Token: 0x0400046C RID: 1132
	public static bool spriteAnimationsOn = true;

	// Token: 0x0400046D RID: 1133
	public static bool showZonesInfo = false;

	// Token: 0x0400046E RID: 1134
	public static bool showFPS = true;

	// Token: 0x0400046F RID: 1135
	public static bool shadowsActive = false;

	// Token: 0x04000470 RID: 1136
	public static bool tooltipsActive = true;

	// Token: 0x04000471 RID: 1137
	public static bool experimentalMode = false;

	// Token: 0x04000472 RID: 1138
	public static bool wbb_confirmed = false;

	// Token: 0x04000473 RID: 1139
	public static bool fullScreen = true;

	// Token: 0x04000474 RID: 1140
	public static bool firebaseAvailable = false;

	// Token: 0x04000475 RID: 1141
	public static bool uploadAvailable = false;

	// Token: 0x04000476 RID: 1142
	public static bool gameLoaded = false;

	// Token: 0x04000477 RID: 1143
	public static bool showStartupWindow = false;

	// Token: 0x04000478 RID: 1144
	public static bool skipTutorial = false;

	// Token: 0x04000479 RID: 1145
	public static bool showConsoleOnStart = false;

	// Token: 0x0400047A RID: 1146
	public static bool isEditor = false;

	// Token: 0x0400047B RID: 1147
	public static bool isMobile = false;

	// Token: 0x0400047C RID: 1148
	public static bool isIos = false;

	// Token: 0x0400047D RID: 1149
	public static bool isAndroid = false;

	// Token: 0x0400047E RID: 1150
	public static bool isComputer = true;

	// Token: 0x0400047F RID: 1151
	public static bool greyGooDamaged = false;

	// Token: 0x04000480 RID: 1152
	public static GodPower powerToUnlock;

	// Token: 0x04000481 RID: 1153
	public static string currentBrush = "circ_5";

	// Token: 0x04000482 RID: 1154
	public static BrushData currentBrushData;

	// Token: 0x04000483 RID: 1155
	public static City selectedCity;

	// Token: 0x04000484 RID: 1156
	public static Kingdom selectedKingdom;

	// Token: 0x04000485 RID: 1157
	public static Culture selectedCulture;

	// Token: 0x04000486 RID: 1158
	public static Actor selectedUnit;

	// Token: 0x04000487 RID: 1159
	private static float timer = 5f;

	// Token: 0x04000488 RID: 1160
	private static bool skip = false;

	// Token: 0x04000489 RID: 1161
	private static List<string> _loggedSelectedPowers = new List<string>();
}
