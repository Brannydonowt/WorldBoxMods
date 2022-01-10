using System;

// Token: 0x02000063 RID: 99
public class ItemAccessoryMaterialLibrary : ItemAssetLibrary<ItemAsset>
{
	// Token: 0x0600021C RID: 540 RVA: 0x00027260 File Offset: 0x00025460
	public override void init()
	{
		base.init();
		ItemAsset itemAsset = new ItemAsset();
		itemAsset.id = "bone";
		itemAsset.equipment_value = 5;
		ItemAsset pAsset = itemAsset;
		this.t = itemAsset;
		this.add(pAsset);
		this.t.setCost(5, "bones", 1, "gems", 1);
		this.t.baseStats.crit = 2f;
		ItemAsset itemAsset2 = new ItemAsset();
		itemAsset2.id = "copper";
		itemAsset2.equipment_value = 10;
		itemAsset2.tech_needed = "material_copper";
		pAsset = itemAsset2;
		this.t = itemAsset2;
		this.add(pAsset);
		this.t.setCost(5, "metals", 1, "gems", 1);
		this.t.baseStats.crit = 3f;
		ItemAsset itemAsset3 = new ItemAsset();
		itemAsset3.id = "bronze";
		itemAsset3.equipment_value = 15;
		itemAsset3.tech_needed = "material_bronze";
		pAsset = itemAsset3;
		this.t = itemAsset3;
		this.add(pAsset);
		this.t.setCost(10, "metals", 1, "gems", 1);
		this.t.baseStats.crit = 4f;
		ItemAsset itemAsset4 = new ItemAsset();
		itemAsset4.id = "silver";
		itemAsset4.equipment_value = 20;
		itemAsset4.tech_needed = "material_silver";
		pAsset = itemAsset4;
		this.t = itemAsset4;
		this.add(pAsset);
		this.t.setCost(15, "metals", 2, "gems", 1);
		this.t.baseStats.crit = 5f;
		ItemAsset itemAsset5 = new ItemAsset();
		itemAsset5.id = "iron";
		itemAsset5.equipment_value = 30;
		itemAsset5.tech_needed = "material_iron";
		pAsset = itemAsset5;
		this.t = itemAsset5;
		this.add(pAsset);
		this.t.setCost(20, "metals", 3, "gems", 2);
		this.t.baseStats.crit = 6f;
		ItemAsset itemAsset6 = new ItemAsset();
		itemAsset6.id = "steel";
		itemAsset6.equipment_value = 40;
		itemAsset6.tech_needed = "material_steel";
		pAsset = itemAsset6;
		this.t = itemAsset6;
		this.add(pAsset);
		this.t.setCost(20, "metals", 3, "gems", 3);
		this.t.baseStats.crit = 7f;
		ItemAsset itemAsset7 = new ItemAsset();
		itemAsset7.id = "mythril";
		itemAsset7.equipment_value = 50;
		itemAsset7.tech_needed = "material_mythril";
		pAsset = itemAsset7;
		this.t = itemAsset7;
		this.add(pAsset);
		this.t.setCost(50, "metals", 4, "gems", 4);
		this.t.baseStats.crit = 8f;
		ItemAsset itemAsset8 = new ItemAsset();
		itemAsset8.id = "adamantine";
		itemAsset8.equipment_value = 70;
		itemAsset8.tech_needed = "material_adamantine";
		pAsset = itemAsset8;
		this.t = itemAsset8;
		this.add(pAsset);
		this.t.setCost(100, "metals", 5, "gems", 5);
		this.t.baseStats.crit = 10f;
	}
}
