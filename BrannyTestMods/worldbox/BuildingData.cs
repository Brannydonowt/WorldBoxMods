using System;
using System.ComponentModel;

// Token: 0x020000CA RID: 202
[Serializable]
public class BuildingData : BaseObjectData
{
	// Token: 0x0400066F RID: 1647
	[DefaultValue(BuildingState.Null)]
	public BuildingState state;

	// Token: 0x04000670 RID: 1648
	public int mainX;

	// Token: 0x04000671 RID: 1649
	public int mainY;

	// Token: 0x04000672 RID: 1650
	public string templateID;

	// Token: 0x04000673 RID: 1651
	[DefaultValue("")]
	public string cityID = "";

	// Token: 0x04000674 RID: 1652
	[DefaultValue(false)]
	public bool underConstruction;

	// Token: 0x04000675 RID: 1653
	[DefaultValue(false)]
	public bool spawnPixelActive;

	// Token: 0x04000676 RID: 1654
	public string objectID;

	// Token: 0x04000677 RID: 1655
	[DefaultValue(0)]
	public int progress;

	// Token: 0x04000678 RID: 1656
	[DefaultValue(-1)]
	public int frameID = -1;
}
