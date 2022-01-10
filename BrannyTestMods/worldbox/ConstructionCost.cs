using System;
using Beebyte.Obfuscator;

// Token: 0x0200007F RID: 127
[ObfuscateLiterals]
[Serializable]
public class ConstructionCost
{
	// Token: 0x060002CB RID: 715 RVA: 0x0002E421 File Offset: 0x0002C621
	public ConstructionCost(int pWood = 0, int pStone = 0, int pMetals = 0, int pGold = 0)
	{
		this.wood = pWood;
		this.stone = pStone;
		this.metals = pMetals;
		this.gold = pGold;
	}

	// Token: 0x040003BA RID: 954
	public int wood;

	// Token: 0x040003BB RID: 955
	public int stone;

	// Token: 0x040003BC RID: 956
	public int metals;

	// Token: 0x040003BD RID: 957
	public int gold;
}
