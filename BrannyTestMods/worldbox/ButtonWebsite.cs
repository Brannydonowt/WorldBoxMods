using System;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class ButtonWebsite : MonoBehaviour
{
	// Token: 0x06000FBE RID: 4030 RVA: 0x0008C0B9 File Offset: 0x0008A2B9
	public void openLink()
	{
		Analytics.LogEvent("open_link_website", true, true);
		Application.OpenURL("https://www.superworldbox.com/");
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x0008C0D1 File Offset: 0x0008A2D1
	public void openLinkLSFLW2()
	{
		Analytics.LogEvent("open_link_lsflw2", true, true);
		Application.OpenURL("https://apps.apple.com/app/apple-store/id1397453494?pt=117120454&ct=worldbox&mt=8");
	}
}
