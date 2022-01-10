using System;
using System.Collections.Generic;

// Token: 0x02000068 RID: 104
public class ItemAsset : Asset
{
	// Token: 0x06000229 RID: 553 RVA: 0x00027D5C File Offset: 0x00025F5C
	public void setCost(int pGoldCost, string pResourceID_1 = "none", int pCostResource_1 = 0, string pResourceID_2 = "none", int pCostResource_2 = 0)
	{
		this.cost_gold = pGoldCost;
		this.cost_resource_id_1 = pResourceID_1;
		this.cost_resource_1 = pCostResource_1;
		this.cost_resource_id_2 = pResourceID_2;
		this.cost_resource_2 = pCostResource_2;
	}

	// Token: 0x040002E4 RID: 740
	public List<string> prefixes = new List<string>();

	// Token: 0x040002E5 RID: 741
	public List<string> suffixes = new List<string>();

	// Token: 0x040002E6 RID: 742
	public List<string> materials = new List<string>();

	// Token: 0x040002E7 RID: 743
	public int cost_gold;

	// Token: 0x040002E8 RID: 744
	public int cost_resource_1;

	// Token: 0x040002E9 RID: 745
	public string cost_resource_id_1 = "none";

	// Token: 0x040002EA RID: 746
	public int cost_resource_2;

	// Token: 0x040002EB RID: 747
	public string cost_resource_id_2 = "none";

	// Token: 0x040002EC RID: 748
	public BaseStats baseStats = new BaseStats();

	// Token: 0x040002ED RID: 749
	public int rarity = 1;

	// Token: 0x040002EE RID: 750
	public int equipment_value;

	// Token: 0x040002EF RID: 751
	public string pool;

	// Token: 0x040002F0 RID: 752
	public string slash = string.Empty;

	// Token: 0x040002F1 RID: 753
	public string projectile;

	// Token: 0x040002F2 RID: 754
	public WorldAction attackAction;

	// Token: 0x040002F3 RID: 755
	public ItemQuality quality = ItemQuality.Normal;

	// Token: 0x040002F4 RID: 756
	public WeaponType attackType;

	// Token: 0x040002F5 RID: 757
	public string tech_needed = string.Empty;
}
