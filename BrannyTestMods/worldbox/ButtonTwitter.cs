using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class ButtonTwitter : MonoBehaviour
{
	// Token: 0x06000FB8 RID: 4024 RVA: 0x0008C040 File Offset: 0x0008A240
	public void openLink()
	{
		Analytics.LogEvent("open_link_twitter", true, true);
		Application.OpenURL("http://twitter.com/mixamko");
	}
}
