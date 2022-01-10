using System;

// Token: 0x0200007E RID: 126
[Serializable]
public class BuildingFundament
{
	// Token: 0x060002CA RID: 714 RVA: 0x0002E3FC File Offset: 0x0002C5FC
	public BuildingFundament(int pLeft, int pRight, int pTop, int pBottom)
	{
		this.left = pLeft;
		this.right = pRight;
		this.top = pTop;
		this.bottom = pBottom;
	}

	// Token: 0x040003B6 RID: 950
	public int left;

	// Token: 0x040003B7 RID: 951
	public int right;

	// Token: 0x040003B8 RID: 952
	public int top;

	// Token: 0x040003B9 RID: 953
	public int bottom;
}
