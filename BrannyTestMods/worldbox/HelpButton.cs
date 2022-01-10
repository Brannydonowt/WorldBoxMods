using System;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class HelpButton : MonoBehaviour
{
	// Token: 0x06000D0D RID: 3341 RVA: 0x0007D1A0 File Offset: 0x0007B3A0
	public void clickHelp()
	{
		string stringVal = PlayerConfig.dict["language"].stringVal;
		Analytics.LogEvent("open_help", true, true);
		string url;
		if (Application.platform == RuntimePlatform.Android)
		{
			url = "https://support.google.com/googleplay/answer/1050566?hl=" + stringVal;
		}
		else
		{
			url = string.Concat(new string[]
			{
				"https://support.apple.com/",
				stringVal,
				"-",
				stringVal,
				"/HT203005"
			});
		}
		Application.OpenURL(url);
	}
}
