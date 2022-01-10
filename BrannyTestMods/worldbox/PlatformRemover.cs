using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
public class PlatformRemover : MonoBehaviour
{
	// Token: 0x06000A24 RID: 2596 RVA: 0x000677AC File Offset: 0x000659AC
	private void Awake()
	{
		if (this.removeOnGlobalVersion)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnEditor && Config.isEditor)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnPC && Config.isComputer)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnAndroid && Config.isAndroid)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnIOS && Config.isIos)
		{
			Object.Destroy(base.gameObject);
			return;
		}
	}

	// Token: 0x04000CAD RID: 3245
	public bool removeOnIOS;

	// Token: 0x04000CAE RID: 3246
	public bool removeOnAndroid;

	// Token: 0x04000CAF RID: 3247
	public bool removeOnPC;

	// Token: 0x04000CB0 RID: 3248
	public bool removeOnEditor;

	// Token: 0x04000CB1 RID: 3249
	public bool removeOnGlobalVersion;

	// Token: 0x04000CB2 RID: 3250
	public bool removeOnChineseVersion;

	// Token: 0x04000CB3 RID: 3251
	public bool removeOnNonPremiumVersion;
}
