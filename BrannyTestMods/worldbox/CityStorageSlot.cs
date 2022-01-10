using System;

// Token: 0x020000F9 RID: 249
[Serializable]
public class CityStorageSlot
{
	// Token: 0x0600058A RID: 1418 RVA: 0x000451C6 File Offset: 0x000433C6
	public void create()
	{
		this.asset = AssetManager.resources.get(this.id);
	}

	// Token: 0x04000772 RID: 1906
	public string id;

	// Token: 0x04000773 RID: 1907
	public int amount;

	// Token: 0x04000774 RID: 1908
	[NonSerialized]
	public ResourceAsset asset;
}
