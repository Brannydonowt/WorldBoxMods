using System;

// Token: 0x02000064 RID: 100
public class ItemArmorMaterialLibrary : ItemAssetLibrary<ItemAsset>
{
	// Token: 0x0600021E RID: 542 RVA: 0x00027578 File Offset: 0x00025778
	public override void init()
	{
		base.init();
		ItemAsset itemAsset = new ItemAsset();
		itemAsset.id = "leather";
		itemAsset.equipment_value = 5;
		ItemAsset pAsset = itemAsset;
		this.t = itemAsset;
		this.add(pAsset);
		this.t.setCost(5, "leather", 1, "none", 0);
		this.t.baseStats.armor = 2;
		this.t.baseStats.dodge = 5f;
		this.t.baseStats.speed = 1f;
		ItemAsset itemAsset2 = new ItemAsset();
		itemAsset2.id = "copper";
		itemAsset2.equipment_value = 10;
		itemAsset2.tech_needed = "material_copper";
		pAsset = itemAsset2;
		this.t = itemAsset2;
		this.add(pAsset);
		this.t.setCost(5, "metals", 1, "none", 0);
		this.t.baseStats.armor = 3;
		ItemAsset itemAsset3 = new ItemAsset();
		itemAsset3.id = "bronze";
		itemAsset3.equipment_value = 15;
		itemAsset3.tech_needed = "material_bronze";
		pAsset = itemAsset3;
		this.t = itemAsset3;
		this.add(pAsset);
		this.t.setCost(5, "metals", 1, "none", 0);
		this.t.baseStats.armor = 4;
		ItemAsset itemAsset4 = new ItemAsset();
		itemAsset4.id = "silver";
		itemAsset4.equipment_value = 20;
		itemAsset4.tech_needed = "material_silver";
		pAsset = itemAsset4;
		this.t = itemAsset4;
		this.add(pAsset);
		this.t.setCost(10, "metals", 1, "none", 0);
		this.t.baseStats.armor = 5;
		ItemAsset itemAsset5 = new ItemAsset();
		itemAsset5.id = "iron";
		itemAsset5.equipment_value = 30;
		itemAsset5.tech_needed = "material_iron";
		pAsset = itemAsset5;
		this.t = itemAsset5;
		this.add(pAsset);
		this.t.setCost(15, "metals", 2, "none", 0);
		this.t.baseStats.armor = 6;
		ItemAsset itemAsset6 = new ItemAsset();
		itemAsset6.id = "steel";
		itemAsset6.equipment_value = 40;
		itemAsset6.tech_needed = "material_steel";
		pAsset = itemAsset6;
		this.t = itemAsset6;
		this.add(pAsset);
		this.t.setCost(20, "metals", 3, "none", 0);
		this.t.baseStats.armor = 7;
		ItemAsset itemAsset7 = new ItemAsset();
		itemAsset7.id = "mythril";
		itemAsset7.equipment_value = 50;
		itemAsset7.tech_needed = "material_mythril";
		pAsset = itemAsset7;
		this.t = itemAsset7;
		this.add(pAsset);
		this.t.setCost(50, "metals", 4, "none", 0);
		this.t.baseStats.armor = 8;
		ItemAsset itemAsset8 = new ItemAsset();
		itemAsset8.id = "adamantine";
		itemAsset8.equipment_value = 70;
		itemAsset8.tech_needed = "material_adamantine";
		pAsset = itemAsset8;
		this.t = itemAsset8;
		this.add(pAsset);
		this.t.setCost(100, "metals", 5, "none", 0);
		this.t.baseStats.armor = 10;
	}
}
