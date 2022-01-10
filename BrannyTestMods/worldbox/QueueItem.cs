using System;
using UnityEngine;

// Token: 0x02000245 RID: 581
[Serializable]
public class QueueItem
{
	// Token: 0x04000F9A RID: 3994
	public object timestamp;

	// Token: 0x04000F9B RID: 3995
	public string salt = RequestHelper.salt;

	// Token: 0x04000F9C RID: 3996
	public string version = Application.version;

	// Token: 0x04000F9D RID: 3997
	public string identifier = Application.identifier;

	// Token: 0x04000F9E RID: 3998
	public string language = LocalizedTextManager.instance.language;

	// Token: 0x04000F9F RID: 3999
	public string platform = Application.platform.ToString();

	// Token: 0x04000FA0 RID: 4000
	public int progress;
}
