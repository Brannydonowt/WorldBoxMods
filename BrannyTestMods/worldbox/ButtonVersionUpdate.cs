using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class ButtonVersionUpdate : MonoBehaviour
{
	// Token: 0x06000FBA RID: 4026 RVA: 0x0008C060 File Offset: 0x0008A260
	public void openLink()
	{
		Analytics.LogEvent("open_link_version", true, true);
		Application.OpenURL("https://www.superworldbox.com/");
	}
}
