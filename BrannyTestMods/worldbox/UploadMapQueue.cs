using System;
using System.Collections.Generic;

// Token: 0x0200022B RID: 555
[Serializable]
public class UploadMapQueue : QueueItem
{
	// Token: 0x04000F19 RID: 3865
	public string username;

	// Token: 0x04000F1A RID: 3866
	public string userId;

	// Token: 0x04000F1B RID: 3867
	public string reason;

	// Token: 0x04000F1C RID: 3868
	public string error;

	// Token: 0x04000F1D RID: 3869
	public string status;

	// Token: 0x04000F1E RID: 3870
	public string mapName;

	// Token: 0x04000F1F RID: 3871
	public string mapDescription;

	// Token: 0x04000F20 RID: 3872
	public List<string> mapTags;

	// Token: 0x04000F21 RID: 3873
	public string mapFileName;

	// Token: 0x04000F22 RID: 3874
	public string mapPreviewName;

	// Token: 0x04000F23 RID: 3875
	public string mapId;

	// Token: 0x04000F24 RID: 3876
	public MapMetaData mapMeta;
}
