using System;
using System.Diagnostics;
using System.IO;
using Beebyte.Obfuscator;
using Proyecto26;
using RSG;
using SimpleJSON;
using UnityEngine;

// Token: 0x020001DA RID: 474
[ObfuscateLiterals]
internal static class VersionCheck
{
	// Token: 0x06000AAC RID: 2732 RVA: 0x0006B022 File Offset: 0x00069222
	internal static void checkVersion()
	{
		VersionCheck.checkPlatform();
		VersionCheck.checkDLLs();
		VersionCheck.getOnlineVersion();
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0006B034 File Offset: 0x00069234
	internal static bool isOutdated()
	{
		if (!(VersionCheck.onlineVersion != "") || !(Config.gv != VersionCheck.onlineVersion))
		{
			return false;
		}
		if (VersionCheck.onlineVersion.Split(new char[]
		{
			'.'
		}).Length != 3)
		{
			return false;
		}
		if (Config.gv.Split(new char[]
		{
			'.'
		}).Length != 3)
		{
			return false;
		}
		SemanticVersion semanticVersion = new SemanticVersion(VersionCheck.onlineVersion);
		SemanticVersion other = new SemanticVersion(Config.gv);
		int num = semanticVersion.CompareTo(other);
		return num > 0;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0006B0C4 File Offset: 0x000692C4
	internal static void checkDLLs()
	{
		try
		{
			foreach (object obj in Process.GetCurrentProcess().Modules)
			{
				ProcessModule processModule = (ProcessModule)obj;
				if (processModule.FileName.ToLower().Contains("steam") && processModule.ModuleMemorySize > 0)
				{
					RestClient.DefaultRequestHeaders["wb-stms"] = processModule.ModuleMemorySize.ToString();
				}
			}
		}
		catch (Exception)
		{
		}
		int num = 0;
		try
		{
			foreach (string text in Directory.EnumerateFiles(Application.dataPath, "*team*.*", 1))
			{
				num++;
				try
				{
					string text2 = Path.GetFileName(text);
					text2 = text2 + "," + new FileInfo(text).Length.ToString();
					RestClient.DefaultRequestHeaders["wb-stf" + num.ToString()] = text2;
				}
				catch (Exception)
				{
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0006B240 File Offset: 0x00069440
	// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0006B224 File Offset: 0x00069424
	private static string versionCheck
	{
		get
		{
			return VersionCheck._vsCheck;
		}
		set
		{
			VersionCheck._vsCheck = value;
			VersionCallbacks.timer = Toolbox.randomFloat(300f, 600f);
		}
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0006B248 File Offset: 0x00069448
	private static void getOnlineVersion()
	{
		if (VersionCheck.platform.Length < 2)
		{
			return;
		}
		string url = "https://versions.superworldbox.com/versions/" + VersionCheck.platform + ".json?" + Toolbox.cacheBuster();
		try
		{
			RestClient.DefaultRequestHeaders["wb-type"] = "vercheck";
			RestClient.DefaultRequestHeaders["wb-prem"] = (Config.havePremium ? "y" : "n");
		}
		catch (Exception)
		{
		}
		RestClient.Get(url).Then(delegate(ResponseHelper response)
		{
			VersionCheck.versionCheck = JSON.Parse(response.Text);
			if (VersionCheck.versionCheck == "")
			{
				return;
			}
			if (VersionCheck.versionCheck.Split(new char[]
			{
				'.'
			}).Length != 3)
			{
				try
				{
					if (VersionCheck.versionCheck.Contains("no_valid"))
					{
						Config.removePremium();
					}
					if (VersionCheck.versionCheck.Contains("give_prem"))
					{
						Config.givePremium();
					}
					if (VersionCheck.versionCheck.Contains("dprchk"))
					{
						Config.pCheck(false);
					}
					if (VersionCheck.versionCheck.Contains("eprchk"))
					{
						Config.pCheck(true);
					}
					if (VersionCheck.versionCheck.Contains("everything_magic"))
					{
						Config.magicCheck(true);
					}
					if (VersionCheck.versionCheck.Contains("nothing_magic"))
					{
						Config.magicCheck(false);
					}
					if (VersionCheck.versionCheck.Contains("fireworks"))
					{
						Config.fireworksCheck(true);
					}
					if (VersionCheck.versionCheck.Contains("firenope"))
					{
						Config.fireworksCheck(false);
					}
					if (VersionCheck.versionCheck.Contains("showtut"))
					{
						MapBox instance = MapBox.instance;
						if (instance != null)
						{
							Tutorial tutorial = instance.tutorial;
							if (tutorial != null)
							{
								tutorial.startTutorial();
							}
						}
					}
					if (VersionCheck.versionCheck.Contains("aye"))
					{
						MapBox.aye();
					}
					if (VersionCheck.versionCheck.Contains("bear"))
					{
						Tutorial.restartTutorial();
					}
					if (VersionCheck.versionCheck.Contains("lang_"))
					{
						string language = VersionCheck.extractVal(VersionCheck.versionCheck, "lang_", false);
						LocalizedTextManager.instance.setLanguage(language);
					}
					if (VersionCheck.versionCheck.Contains("window_"))
					{
						ScrollWindow.get(VersionCheck.extractVal(VersionCheck.versionCheck, "window_", true)).forceShow();
					}
					if (VersionCheck.versionCheck.Contains("del_"))
					{
						CustomTextureAtlas.delete(VersionCheck.extractVal(VersionCheck.versionCheck, "del_", false));
					}
					if (VersionCheck.versionCheck.Contains("nxtc_"))
					{
						int num = int.Parse(VersionCheck.extractVal(VersionCheck.versionCheck, "nxtc_", false));
						if (num > 0)
						{
							InitStuff.targetSeconds = (float)num;
						}
					}
					else
					{
						InitStuff.targetSeconds = 300f;
					}
				}
				catch (Exception)
				{
				}
				return;
			}
			VersionCheck.onlineVersion = VersionCheck.versionCheck;
			if (!VersionCheck.shownVersion)
			{
				VersionCheck.shownVersion = true;
				Debug.Log("Ver " + VersionCheck.onlineVersion + " " + Application.version);
				if (VersionCheck.isOutdated())
				{
					Debug.Log("Current version is outdated");
				}
			}
		}).Catch(delegate(Exception err)
		{
			Debug.Log("Some error happened during version check");
			Debug.Log(err.Message);
		});
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0006B318 File Offset: 0x00069518
	public static bool isWNOutdated()
	{
		if (string.IsNullOrEmpty(VersionCheck.wnVersion.version))
		{
			return true;
		}
		if (string.IsNullOrEmpty(VersionCheck.wnVersion.build))
		{
			return true;
		}
		if (Config.gv != VersionCheck.wnVersion.version)
		{
			if (VersionCheck.wnVersion.version.Split(new char[]
			{
				'.'
			}).Length != 3)
			{
				return false;
			}
			if (Config.gv.Split(new char[]
			{
				'.'
			}).Length != 3)
			{
				return false;
			}
			SemanticVersion semanticVersion = new SemanticVersion(VersionCheck.wnVersion.version);
			SemanticVersion other = new SemanticVersion(Config.gv);
			int num = semanticVersion.CompareTo(other);
			return num > 0;
		}
		else
		{
			if (Config.versionCodeText != VersionCheck.wnVersion.build)
			{
				int num2 = int.Parse(VersionCheck.wnVersion.build);
				int value = int.Parse(Config.versionCodeText);
				int num3 = num2.CompareTo(value);
				return num3 > 0;
			}
			return false;
		}
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0006B418 File Offset: 0x00069618
	private static string extractVal(string versionCheck, string pSplitValue, bool pLast = false)
	{
		string[] array = versionCheck.Split(new string[]
		{
			pSplitValue
		}, StringSplitOptions.RemoveEmptyEntries);
		string text;
		if (array.Length > 1)
		{
			text = array[1];
		}
		else
		{
			text = array[0];
		}
		if (!pLast && text.Contains("_"))
		{
			text = text.Split(new string[]
			{
				"_"
			}, StringSplitOptions.RemoveEmptyEntries)[0];
		}
		return text;
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0006B470 File Offset: 0x00069670
	private static void checkPlatform()
	{
		RuntimePlatform runtimePlatform = Application.platform;
		if (runtimePlatform <= RuntimePlatform.Android)
		{
			switch (runtimePlatform)
			{
			case RuntimePlatform.OSXEditor:
				VersionCheck.platform = "mac";
				return;
			case RuntimePlatform.OSXPlayer:
				VersionCheck.platform = "mac";
				return;
			case RuntimePlatform.WindowsPlayer:
				VersionCheck.platform = "pc";
				return;
			case RuntimePlatform.OSXWebPlayer:
			case RuntimePlatform.OSXDashboardPlayer:
			case RuntimePlatform.WindowsWebPlayer:
			case (RuntimePlatform)6:
				break;
			case RuntimePlatform.WindowsEditor:
				VersionCheck.platform = "pc";
				return;
			case RuntimePlatform.IPhonePlayer:
				VersionCheck.platform = "ios";
				return;
			default:
				if (runtimePlatform == RuntimePlatform.Android)
				{
					VersionCheck.platform = "android";
					return;
				}
				break;
			}
		}
		else
		{
			if (runtimePlatform == RuntimePlatform.LinuxPlayer)
			{
				VersionCheck.platform = "linux";
				return;
			}
			if (runtimePlatform == RuntimePlatform.LinuxEditor)
			{
				VersionCheck.platform = "linux";
				return;
			}
		}
		VersionCheck.platform = "unknown";
	}

	// Token: 0x04000D3B RID: 3387
	private static string platform = "";

	// Token: 0x04000D3C RID: 3388
	public static string onlineVersion = "";

	// Token: 0x04000D3D RID: 3389
	public static WorldNetVersion wnVersion;

	// Token: 0x04000D3E RID: 3390
	public static Promise wnPromise;

	// Token: 0x04000D3F RID: 3391
	private static bool shownVersion = false;

	// Token: 0x04000D40 RID: 3392
	internal static string _vsCheck;
}
