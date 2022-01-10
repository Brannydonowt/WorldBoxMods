using System;
using System.Collections.Generic;

// Token: 0x02000225 RID: 549
[Serializable]
public class ConstructionData
{
	// Token: 0x04000EE8 RID: 3816
	public int cornerX;

	// Token: 0x04000EE9 RID: 3817
	public int cornerY;

	// Token: 0x04000EEA RID: 3818
	public int mainX;

	// Token: 0x04000EEB RID: 3819
	public int mainY;

	// Token: 0x04000EEC RID: 3820
	public string templateID;

	// Token: 0x04000EED RID: 3821
	public string cityID = "";

	// Token: 0x04000EEE RID: 3822
	public bool underConstruction;

	// Token: 0x04000EEF RID: 3823
	public List<ConstructionTileData> tileData;

	// Token: 0x04000EF0 RID: 3824
	public string objectID;

	// Token: 0x04000EF1 RID: 3825
	public int health = 1;
}
