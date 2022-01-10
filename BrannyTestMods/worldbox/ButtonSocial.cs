using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class ButtonSocial : MonoBehaviour
{
	// Token: 0x06000FB2 RID: 4018 RVA: 0x0008BF70 File Offset: 0x0008A170
	public void openLink()
	{
		Analytics.LogEvent("open_link_facebook", true, true);
		Application.OpenURL("https://www.facebook.com/superworldbox");
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x0008BF88 File Offset: 0x0008A188
	public void openDiscord()
	{
		Analytics.LogEvent("open_link_discord", true, true);
		Application.OpenURL("https://discordapp.com/invite/worldbox");
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0008BFA0 File Offset: 0x0008A1A0
	public void openLinkReddit()
	{
		Analytics.LogEvent("open_link_reddit", true, true);
		Application.OpenURL("https://www.reddit.com/r/worldbox");
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x0008BFB8 File Offset: 0x0008A1B8
	public void openLinkMoonBox()
	{
		Analytics.LogEvent("open_link_moonbox", true, true);
		if (Config.isIos)
		{
			Application.OpenURL("https://bit.ly/moonbox_ios");
			return;
		}
		Application.OpenURL("https://bit.ly/moonbox2_wb");
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x0008BFE4 File Offset: 0x0008A1E4
	public void openLinkSteam()
	{
		Analytics.LogEvent("open_link_steam", true, true);
		Application.OpenURL("https://store.steampowered.com/app/1206560/" + "?utm_source=game_bar" + "&utm_campaign=get_wishlists" + "&utm_medium=" + Application.platform.ToString());
	}
}
