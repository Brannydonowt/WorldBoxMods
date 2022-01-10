using System;
using Beebyte.Obfuscator;

// Token: 0x0200023A RID: 570
[ObfuscateLiterals]
internal static class TestingCB
{
	// Token: 0x06000C8E RID: 3214 RVA: 0x0007AD30 File Offset: 0x00078F30
	internal static void init()
	{
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.premiumChecker));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.premiumPossible));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.purpleTextures));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.fireworks));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.tutorial));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.aye));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.language));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.openWindow));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.deleteFile));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.nextCheck));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.valCheck));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.enableSigCheck));
		VersionCallbacks.versionCallbacks = (Action<string>)Delegate.Combine(VersionCallbacks.versionCallbacks, new Action<string>(TestingCB.adChecks));
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0007AEDD File Offset: 0x000790DD
	private static void premiumChecker(string pVersionCheck)
	{
		if (pVersionCheck.Contains("no_valid"))
		{
			Config.removePremium();
		}
		if (pVersionCheck.Contains("give_prem"))
		{
			Config.givePremium();
		}
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0007AF03 File Offset: 0x00079103
	private static void premiumPossible(string pVersionCheck)
	{
		if (pVersionCheck.Contains("dprchk"))
		{
			Config.pCheck(false);
		}
		if (pVersionCheck.Contains("eprchk"))
		{
			Config.pCheck(true);
		}
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0007AF2B File Offset: 0x0007912B
	private static void purpleTextures(string pVersionCheck)
	{
		if (pVersionCheck.Contains("everything_magic"))
		{
			Config.magicCheck(true);
		}
		if (pVersionCheck.Contains("nothing_magic"))
		{
			Config.magicCheck(false);
		}
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0007AF53 File Offset: 0x00079153
	private static void fireworks(string pVersionCheck)
	{
		if (pVersionCheck.Contains("fireworks"))
		{
			Config.fireworksCheck(true);
		}
		if (pVersionCheck.Contains("firenope"))
		{
			Config.fireworksCheck(false);
		}
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0007AF7B File Offset: 0x0007917B
	private static void tutorial(string pVersionCheck)
	{
		if (pVersionCheck.Contains("showtut"))
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
		if (pVersionCheck.Contains("bear"))
		{
			Tutorial.restartTutorial();
		}
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0007AFB7 File Offset: 0x000791B7
	private static void aye(string pVersionCheck)
	{
		if (pVersionCheck.Contains("aye"))
		{
			MapBox.aye();
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0007AFCC File Offset: 0x000791CC
	private static void language(string pVersionCheck)
	{
		if (pVersionCheck.Contains("lang_"))
		{
			string language = TestingCB.extractVal(pVersionCheck, "lang_", false);
			LocalizedTextManager.instance.setLanguage(language);
		}
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x0007AFFE File Offset: 0x000791FE
	private static void openWindow(string pVersionCheck)
	{
		if (pVersionCheck.Contains("window_"))
		{
			ScrollWindow.get(TestingCB.extractVal(pVersionCheck, "window_", true)).forceShow();
		}
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0007B023 File Offset: 0x00079223
	private static void deleteFile(string pVersionCheck)
	{
		if (pVersionCheck.Contains("del_"))
		{
			CustomTextureAtlas.delete(TestingCB.extractVal(pVersionCheck, "del_", false));
		}
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0007B044 File Offset: 0x00079244
	private static void nextCheck(string pVersionCheck)
	{
		if (pVersionCheck.Contains("nxtc_"))
		{
			int num = int.Parse(TestingCB.extractVal(pVersionCheck, "nxtc_", false));
			if (num > 0)
			{
				InitStuff.targetSeconds = (float)num;
				return;
			}
		}
		else
		{
			InitStuff.targetSeconds = 300f;
		}
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0007B086 File Offset: 0x00079286
	private static void valCheck(string pVersionCheck)
	{
		if (pVersionCheck.Contains("evalchk"))
		{
			Config.valCheck(true);
		}
		if (pVersionCheck.Contains("dvalchk"))
		{
			Config.valCheck(false);
		}
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x0007B0AE File Offset: 0x000792AE
	private static void enableSigCheck(string pVersionCheck)
	{
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x0007B0B0 File Offset: 0x000792B0
	private static void adChecks(string pVersionCheck)
	{
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x0007B0B4 File Offset: 0x000792B4
	public static string extractVal(string pVersionCheck, string pSplitValue, bool pLast = false)
	{
		string[] array = pVersionCheck.Split(new string[]
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
}
