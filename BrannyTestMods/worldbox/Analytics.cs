using System;
using Firebase.Analytics;

// Token: 0x020001A1 RID: 417
public class Analytics
{
	// Token: 0x0600098D RID: 2445 RVA: 0x00064A80 File Offset: 0x00062C80
	public static void trackWindow(string name)
	{
		if (Config.isComputer)
		{
			return;
		}
		string text = Analytics.Slugify(name);
		if (Config.firebaseAvailable)
		{
			FirebaseAnalytics.LogEvent("open_window", "window_id", text);
			Analytics.logScreen("ScrollWindow", text);
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00064ABE File Offset: 0x00062CBE
	public static void hideWindow()
	{
		Analytics.logScreen("GamePlay", "gameplay");
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00064ACF File Offset: 0x00062CCF
	public static void worldLoaded()
	{
		Analytics.logScreen("GamePlay", "gameplay");
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00064AE0 File Offset: 0x00062CE0
	public static void worldLoading()
	{
		Analytics.logScreen("LoadingScreen", "loading");
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00064AF4 File Offset: 0x00062CF4
	private static void logScreen(string pClass, string pName)
	{
		if (Config.firebaseAvailable)
		{
			Parameter[] array = new Parameter[]
			{
				new Parameter(FirebaseAnalytics.ParameterScreenClass, pClass),
				new Parameter(FirebaseAnalytics.ParameterScreenName, pName)
			};
			FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventScreenView, array);
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00064B38 File Offset: 0x00062D38
	public static void LogEvent(string name, bool pFirebase = true, bool pFacebook = true)
	{
		if (Config.isComputer)
		{
			return;
		}
		MapBox instance = MapBox.instance;
		bool flag;
		if (instance == null)
		{
			flag = false;
		}
		else
		{
			AutoTesterBot auto_tester = instance.auto_tester;
			bool? flag2 = (auto_tester != null) ? new bool?(auto_tester.active) : null;
			bool flag3 = true;
			flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
		}
		if (flag)
		{
			return;
		}
		string text = Analytics.Slugify(name);
		if (Config.firebaseAvailable && pFirebase)
		{
			FirebaseAnalytics.LogEvent(text);
		}
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00064BA8 File Offset: 0x00062DA8
	public static void LogEvent(string name, string parameterName, string parameterValue)
	{
		if (Config.isComputer)
		{
			return;
		}
		MapBox instance = MapBox.instance;
		bool flag;
		if (instance == null)
		{
			flag = false;
		}
		else
		{
			AutoTesterBot auto_tester = instance.auto_tester;
			bool? flag2 = (auto_tester != null) ? new bool?(auto_tester.active) : null;
			bool flag3 = true;
			flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
		}
		if (flag)
		{
			return;
		}
		string text = Analytics.Slugify(name);
		if (Config.firebaseAvailable)
		{
			FirebaseAnalytics.LogEvent(text, parameterName, parameterValue);
		}
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00064C15 File Offset: 0x00062E15
	public static string Slugify(string phrase)
	{
		return phrase.Trim().Replace(" ", "_").ToLower();
	}

	// Token: 0x04000C41 RID: 3137
	private static int countdown = 10;
}
