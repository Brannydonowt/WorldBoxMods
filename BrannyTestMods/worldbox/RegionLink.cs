using System;

// Token: 0x02000186 RID: 390
public class RegionLink
{
	// Token: 0x060008F9 RID: 2297 RVA: 0x0006025F File Offset: 0x0005E45F
	public override int GetHashCode()
	{
		return this.id;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00060267 File Offset: 0x0005E467
	public override bool Equals(object obj)
	{
		return this.Equals(obj as RegionLink);
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00060275 File Offset: 0x0005E475
	public bool Equals(RegionLink pObject)
	{
		return this.id == pObject.id;
	}

	// Token: 0x04000B7E RID: 2942
	public int id;

	// Token: 0x04000B7F RID: 2943
	public HashSetMapRegion regions = new HashSetMapRegion();
}
