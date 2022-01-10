using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class ButtonVote : MonoBehaviour
{
	// Token: 0x06000FBC RID: 4028 RVA: 0x0008C080 File Offset: 0x0008A280
	public void openLink()
	{
		Analytics.LogEvent("click_vote", true, true);
		if (Config.isAndroid)
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.mkarpenko.worldbox");
			return;
		}
		if (Config.isIos)
		{
			Application.OpenURL("https://itunes.apple.com/app/id1450941371");
		}
	}
}
