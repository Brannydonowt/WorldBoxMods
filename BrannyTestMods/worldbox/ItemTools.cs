using System;

// Token: 0x0200006C RID: 108
public class ItemTools
{
	// Token: 0x06000232 RID: 562 RVA: 0x00029B98 File Offset: 0x00027D98
	public static void calcItemValues(ItemData pData)
	{
		ItemTools.s_stats.clear();
		ItemTools.s_value = 0;
		ItemTools.s_quality = ItemQuality.Normal;
		ItemTools.checkStat(ItemTools.getItemMaterialLibrary(pData.type, pData.material));
		ItemTools.checkStat(AssetManager.items.get(pData.id));
		ItemTools.checkStat(AssetManager.items_prefix.get(pData.prefix));
		ItemTools.checkStat(AssetManager.items_suffix.get(pData.suffix));
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00029C10 File Offset: 0x00027E10
	private static void checkStat(ItemAsset pAsset)
	{
		if (pAsset == null)
		{
			return;
		}
		if (ItemTools.s_quality != ItemQuality.Junk)
		{
			if (pAsset.quality == ItemQuality.Junk)
			{
				ItemTools.s_quality = ItemQuality.Junk;
			}
			else if (pAsset.quality > ItemTools.s_quality)
			{
				ItemTools.s_quality = pAsset.quality;
			}
		}
		ItemTools.s_stats.addStats(pAsset.baseStats);
		ItemTools.s_value += pAsset.equipment_value;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00029C74 File Offset: 0x00027E74
	public static ItemAsset getItemMaterialLibrary(EquipmentType pType, string pMaterial)
	{
		ItemAsset result = null;
		switch (pType)
		{
		case EquipmentType.Weapon:
			result = AssetManager.items_material_weapon.get(pMaterial);
			break;
		case EquipmentType.Helmet:
			result = AssetManager.items_material_armor.get(pMaterial);
			break;
		case EquipmentType.Armor:
			result = AssetManager.items_material_armor.get(pMaterial);
			break;
		case EquipmentType.Boots:
			result = AssetManager.items_material_armor.get(pMaterial);
			break;
		case EquipmentType.Ring:
			result = AssetManager.items_material_accessory.get(pMaterial);
			break;
		case EquipmentType.Amulet:
			result = AssetManager.items_material_accessory.get(pMaterial);
			break;
		}
		return result;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00029CF8 File Offset: 0x00027EF8
	public static string getItemClass(EquipmentType pType)
	{
		string result = "";
		switch (pType)
		{
		case EquipmentType.Weapon:
			result = "item_class_weapon";
			break;
		case EquipmentType.Helmet:
			result = "item_class_armor";
			break;
		case EquipmentType.Armor:
			result = "item_class_armor";
			break;
		case EquipmentType.Boots:
			result = "item_class_armor";
			break;
		case EquipmentType.Ring:
			result = "item_class_accessory";
			break;
		case EquipmentType.Amulet:
			result = "item_class_accessory";
			break;
		}
		return result;
	}

	// Token: 0x040002F8 RID: 760
	public static int s_value = 0;

	// Token: 0x040002F9 RID: 761
	public static ItemQuality s_quality;

	// Token: 0x040002FA RID: 762
	public static BaseStats s_stats = new BaseStats();
}
