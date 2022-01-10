using System;
using Discord;
using Proyecto26;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class DiscordTracker : MonoBehaviour
{
	// Token: 0x06000BE5 RID: 3045 RVA: 0x000760E4 File Offset: 0x000742E4
	private void Start()
	{
		if (this.initiated)
		{
			return;
		}
		this.initiated = true;
		bool flag = false;
		try
		{
			DiscordTracker.instance = this;
			DiscordTracker.discord = new Discord(816251591299432468L, 1UL);
			DiscordTracker.activityManager = DiscordTracker.discord.GetActivityManager();
			Activity activity = default(Activity);
			activity.State = LocalizedTextManager.getText("discord_browsing", null);
			activity.Assets.LargeImage = "worldboxlogo";
			activity.Assets.LargeText = "WorldBox";
			activity.Timestamps.Start = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			activity.Instance = true;
			DiscordTracker.activity = activity;
			ActivityManager activityManager = DiscordTracker.activityManager;
			if (activityManager != null)
			{
				activityManager.UpdateActivity(DiscordTracker.activity, delegate(Result res)
				{
					if (res == null)
					{
						DiscordTracker.nextStat();
						return;
					}
					Debug.LogError(res);
					Object.Destroy(DiscordTracker.instance);
				});
			}
		}
		catch (ResultException message)
		{
			Debug.Log("Disabling Discord Integration (Discord not running, or game not run as Administrator)");
			Debug.Log(message);
			flag = true;
		}
		catch (Exception message2)
		{
			Debug.Log("Disabling Discord Integration (Discord not running, or game not run as Administrator)");
			Debug.Log(message2);
			flag = true;
		}
		if (flag)
		{
			Object.Destroy(DiscordTracker.instance);
		}
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x0007622C File Offset: 0x0007442C
	private static void tryGetUser()
	{
		try
		{
			DiscordTracker.userTries--;
			User currentUser = DiscordTracker.discord.GetUserManager().GetCurrentUser();
			string text = currentUser.Id.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				Config.discordId = text;
				RestClient.DefaultRequestHeaders["wb-dsc"] = text;
				DiscordTracker.haveUser = true;
				Debug.Log("D:" + Config.discordId);
			}
			else
			{
				Debug.Log("D:nf");
			}
			string text2 = currentUser.Username.ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				Config.discordName = text2;
			}
			string text3 = currentUser.Discriminator.ToString();
			if (!string.IsNullOrEmpty(text3))
			{
				Config.discordDiscriminator = text3;
			}
			VersionCheck.checkVersion();
		}
		catch (Exception)
		{
			Debug.Log("D:F");
		}
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00076300 File Offset: 0x00074500
	private void Update()
	{
		try
		{
			DiscordTracker.discord.RunCallbacks();
			if (DiscordTracker.secTimer > 1f)
			{
				DiscordTracker.secTimer -= Time.deltaTime;
			}
			else
			{
				DiscordTracker.secTimer = 1f;
				DiscordTracker.updateStat();
			}
			if (DiscordTracker.timer > 0f)
			{
				DiscordTracker.timer -= Time.deltaTime;
			}
			else
			{
				DiscordTracker.resetTimer();
				if (!DiscordTracker.haveUser && DiscordTracker.userTries > 0)
				{
					DiscordTracker.tryGetUser();
				}
				DiscordTracker.nextStat();
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			Object.Destroy(DiscordTracker.instance);
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x000763A4 File Offset: 0x000745A4
	private void OnDisable()
	{
		Discord discord = DiscordTracker.discord;
		if (discord == null)
		{
			return;
		}
		discord.Dispose();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x000763B5 File Offset: 0x000745B5
	private void OnDestroy()
	{
		DiscordTracker.instance = null;
		DiscordTracker.activityManager = null;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x000763C4 File Offset: 0x000745C4
	public static void setPower(GodPower pPower)
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		DiscordTracker.state = "";
		if (MoveCamera.focusUnit != null)
		{
			return;
		}
		if (pPower == null)
		{
			DiscordTracker.state = LocalizedTextManager.getText("discord_watching", null);
		}
		else if (LocalizedTextManager.stringExists(pPower.name))
		{
			if (pPower.name == "Select City")
			{
				return;
			}
			DiscordTracker.state = LocalizedTextManager.getText("discord_using", null).Replace("$power$", LocalizedTextManager.getText(pPower.name, null));
		}
		DiscordTracker.amount = 0;
		DiscordTracker.activity.State = DiscordTracker.state;
		ActivityManager activityManager = DiscordTracker.activityManager;
		if (activityManager == null)
		{
			return;
		}
		activityManager.UpdateActivity(DiscordTracker.activity, delegate(Result res)
		{
		});
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x0007649C File Offset: 0x0007469C
	public static void showText(string pString)
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		DiscordTracker.state = "";
		if (pString == "")
		{
			return;
		}
		pString = DiscordTracker.RemoveRichTextDynamicTag(pString, "color");
		DiscordTracker.activity.State = pString;
		ActivityManager activityManager = DiscordTracker.activityManager;
		if (activityManager == null)
		{
			return;
		}
		activityManager.UpdateActivity(DiscordTracker.activity, delegate(Result res)
		{
		});
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x0007651C File Offset: 0x0007471C
	public static void trackPower(string pString = "")
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		DiscordTracker.state = "";
		if (MoveCamera.focusUnit != null)
		{
			return;
		}
		if (pString == "Button Close")
		{
			return;
		}
		if (pString == "Select City")
		{
			return;
		}
		DiscordTracker.amount = 0;
		if (pString != "")
		{
			DiscordTracker.activity.State = LocalizedTextManager.getText("discord_viewing", null).Replace("$window$", LocalizedTextManager.getText(pString, null));
		}
		else
		{
			DiscordTracker.activity.State = LocalizedTextManager.getText("discord_browsing", null);
		}
		ActivityManager activityManager = DiscordTracker.activityManager;
		if (activityManager == null)
		{
			return;
		}
		activityManager.UpdateActivity(DiscordTracker.activity, delegate(Result res)
		{
		});
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x000765F0 File Offset: 0x000747F0
	public static void trackWindow(string screen_id)
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		DiscordTracker.state = "";
		if (screen_id == "")
		{
			return;
		}
		if (screen_id == null)
		{
			return;
		}
		if (!(screen_id == "kingdom"))
		{
			if (!(screen_id == "village"))
			{
				if (!(screen_id == "inspect_unit"))
				{
					return;
				}
				DiscordTracker.activity.State = LocalizedTextManager.getText("Inspect", null) + ": " + Config.selectedUnit.data.firstName;
			}
			else
			{
				DiscordTracker.activity.State = LocalizedTextManager.getText("village", null) + ": " + Config.selectedCity.name;
			}
		}
		else
		{
			DiscordTracker.activity.State = LocalizedTextManager.getText("village_statistics_kingdom", null) + ": " + Config.selectedKingdom.name;
		}
		ActivityManager activityManager = DiscordTracker.activityManager;
		if (activityManager == null)
		{
			return;
		}
		activityManager.UpdateActivity(DiscordTracker.activity, delegate(Result res)
		{
		});
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0007670C File Offset: 0x0007490C
	public static void PlusOne()
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		if (MoveCamera.focusUnit != null)
		{
			return;
		}
		if (DiscordTracker.state == "")
		{
			return;
		}
		DiscordTracker.amount++;
		DiscordTracker.activity.State = DiscordTracker.state + " (" + DiscordTracker.amount.ToString() + ")";
		ActivityManager activityManager = DiscordTracker.activityManager;
		if (activityManager == null)
		{
			return;
		}
		activityManager.UpdateActivity(DiscordTracker.activity, delegate(Result res)
		{
		});
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x000767B0 File Offset: 0x000749B0
	public static void updateDetails(string pStat, string pString)
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		if (LocalizedTextManager.stringExists(pStat))
		{
			DiscordTracker.activity.Details = LocalizedTextManager.getText(pStat, null) + ": " + pString;
		}
		else
		{
			DiscordTracker.activity.Details = pString;
		}
		ActivityManager activityManager = DiscordTracker.activityManager;
		if (activityManager == null)
		{
			return;
		}
		activityManager.UpdateActivity(DiscordTracker.activity, delegate(Result res)
		{
		});
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0007682F File Offset: 0x00074A2F
	private static void resetTimer()
	{
		DiscordTracker.timer = 9f;
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x0007683C File Offset: 0x00074A3C
	private static void updateStat()
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		string text = StatsHelper.getStatistic(DiscordTracker.rotateStats[DiscordTracker.currentStat]);
		if (text != "0" && text != "")
		{
			try
			{
				int num;
				if (int.TryParse(text, out num))
				{
					text = string.Format("{0:n0}", num);
				}
			}
			catch (Exception)
			{
			}
			DiscordTracker.updateDetails(DiscordTracker.rotateStats[DiscordTracker.currentStat], text);
			return;
		}
		DiscordTracker.nextStat();
		DiscordTracker.updateStat();
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x000768D0 File Offset: 0x00074AD0
	private static void nextStat()
	{
		if (DiscordTracker.instance == null)
		{
			return;
		}
		DiscordTracker.currentStat = Toolbox.randomInt(0, DiscordTracker.rotateStats.Length - 1);
		if (DiscordTracker.currentStat >= DiscordTracker.rotateStats.Length)
		{
			DiscordTracker.currentStat = 0;
		}
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x00076908 File Offset: 0x00074B08
	private static string RemoveRichTextDynamicTag(string input, string tag)
	{
		for (;;)
		{
			int num = input.IndexOf("<" + tag + "=");
			if (num == -1)
			{
				break;
			}
			int num2 = input.Substring(num, input.Length - num).IndexOf('>');
			if (num2 > 0)
			{
				input = input.Remove(num, num2 + 1);
			}
		}
		input = DiscordTracker.RemoveRichTextTag(input, tag, false);
		return input;
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00076968 File Offset: 0x00074B68
	private static string RemoveRichTextTag(string input, string tag, bool isStart = true)
	{
		for (;;)
		{
			int num = input.IndexOf(isStart ? ("<" + tag + ">") : ("</" + tag + ">"));
			if (num == -1)
			{
				break;
			}
			input = input.Remove(num, 2 + tag.Length + (!isStart).GetHashCode());
		}
		if (isStart)
		{
			input = DiscordTracker.RemoveRichTextTag(input, tag, false);
		}
		return input;
	}

	// Token: 0x04000E60 RID: 3680
	public static Discord discord;

	// Token: 0x04000E61 RID: 3681
	public static ActivityManager activityManager;

	// Token: 0x04000E62 RID: 3682
	private bool initiated;

	// Token: 0x04000E63 RID: 3683
	private static DiscordTracker instance;

	// Token: 0x04000E64 RID: 3684
	private static Activity activity;

	// Token: 0x04000E65 RID: 3685
	private static float timer = 10f;

	// Token: 0x04000E66 RID: 3686
	private static float secTimer = 1f;

	// Token: 0x04000E67 RID: 3687
	private static bool haveUser = false;

	// Token: 0x04000E68 RID: 3688
	private static int userTries = 10;

	// Token: 0x04000E69 RID: 3689
	private static int amount = 0;

	// Token: 0x04000E6A RID: 3690
	private static string state;

	// Token: 0x04000E6B RID: 3691
	private static int currentStat = 0;

	// Token: 0x04000E6C RID: 3692
	private static string[] rotateStats = new string[]
	{
		"world_name",
		"world_statistics_population",
		"world_statistics_population",
		"world_statistics_deaths_total",
		"world_statistics_deaths_total",
		"world_statistics_deaths_total",
		"world_statistics_infected",
		"world_statistics_time",
		"world_statistics_time",
		"houses",
		"kingdoms_villages",
		"most_popular_race",
		"most_popular_race",
		"most_popular_race",
		"races"
	};
}
