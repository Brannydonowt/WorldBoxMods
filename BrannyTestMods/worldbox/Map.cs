using System;
using System.Collections.Generic;

// Token: 0x02000242 RID: 578
[Serializable]
public class Map
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0007B1D8 File Offset: 0x000793D8
	public string formattedMapId
	{
		get
		{
			if (!string.IsNullOrEmpty(this.mapId) && this.mapId.Length == 12)
			{
				return string.Concat(new string[]
				{
					"WB-",
					this.mapId.Substring(0, 4),
					"-",
					this.mapId.Substring(4, 4),
					"-",
					this.mapId.Substring(8, 4)
				});
			}
			return this.mapId;
		}
	}

	// Token: 0x04000F72 RID: 3954
	public string mapId;

	// Token: 0x04000F73 RID: 3955
	public string language;

	// Token: 0x04000F74 RID: 3956
	public string timestamp;

	// Token: 0x04000F75 RID: 3957
	public string userId;

	// Token: 0x04000F76 RID: 3958
	public string username;

	// Token: 0x04000F77 RID: 3959
	public string version;

	// Token: 0x04000F78 RID: 3960
	public string mapName;

	// Token: 0x04000F79 RID: 3961
	public string mapDescription;

	// Token: 0x04000F7A RID: 3962
	public int size;

	// Token: 0x04000F7B RID: 3963
	public int sortIndex;

	// Token: 0x04000F7C RID: 3964
	public List<MapTagType> mapTags;

	// Token: 0x04000F7D RID: 3965
	public MapMetaData mapMeta;

	// Token: 0x04000F7E RID: 3966
	public OnlineStats onlineStats = new OnlineStats
	{
		downloads = 0,
		plays = 0,
		favs = 0,
		reports = 0
	};
}
