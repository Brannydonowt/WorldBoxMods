using System;

// Token: 0x020000B0 RID: 176
[Serializable]
public class WorldTileData
{
	// Token: 0x06000394 RID: 916 RVA: 0x000383EE File Offset: 0x000365EE
	public WorldTileData()
	{
		this.clear();
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00038403 File Offset: 0x00036603
	internal void clear()
	{
	}

	// Token: 0x040005DE RID: 1502
	public string type;

	// Token: 0x040005DF RID: 1503
	public int height;

	// Token: 0x040005E0 RID: 1504
	public ConwayType conwayType = ConwayType.None;

	// Token: 0x040005E1 RID: 1505
	public bool fire;

	// Token: 0x040005E2 RID: 1506
	public int fire_stage;

	// Token: 0x040005E3 RID: 1507
	public int tile_id;

	// Token: 0x040005E4 RID: 1508
	public int x;

	// Token: 0x040005E5 RID: 1509
	public int y;
}
