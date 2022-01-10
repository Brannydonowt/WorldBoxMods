using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class ItemGenerator
{
	// Token: 0x06000221 RID: 545 RVA: 0x000278A0 File Offset: 0x00025AA0
	public static List<string> getPool(string pID)
	{
		if (ItemGenerator.pools.ContainsKey(pID))
		{
			return ItemGenerator.pools[pID];
		}
		List<string> list = new List<string>();
		ItemGenerator.pools.Add(pID, list);
		return list;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x000278DC File Offset: 0x00025ADC
	private static void createPools(string pID, List<ItemAsset> pList)
	{
		foreach (ItemAsset itemAsset in pList)
		{
			string[] array = itemAsset.pool.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				List<string> pool = ItemGenerator.getPool(pID + "_" + array[i]);
				for (int j = 0; j < itemAsset.rarity; j++)
				{
					pool.Add(itemAsset.id);
				}
			}
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x00027984 File Offset: 0x00025B84
	public static void init()
	{
		ItemGenerator.pools = new Dictionary<string, List<string>>();
		ItemGenerator.createPools("prefix", AssetManager.items_prefix.list);
		ItemGenerator.createPools("suffix", AssetManager.items_suffix.list);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x000279B8 File Offset: 0x00025BB8
	public static void generateItemsIDS()
	{
		ItemGenerator.list_id_string = "";
		foreach (string text in AssetManager.items.weapons_id_melee)
		{
		}
		foreach (string pID in AssetManager.items.weapons_id_range)
		{
			ItemGenerator.generateIdsFor(pID, "weapon", "range");
		}
		foreach (ItemAsset itemAsset in AssetManager.items_suffix.list)
		{
			string id = itemAsset.id;
			if (!(id == "0"))
			{
				ItemGenerator.list_id_string = ItemGenerator.list_id_string + "\nitem_prefix_of_" + id;
			}
		}
		Debug.Log(ItemGenerator.list_id_string);
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00027AD0 File Offset: 0x00025CD0
	public static void generateIdsFor(string pID, string pType, string pPool)
	{
		foreach (ItemAsset itemAsset in AssetManager.items_prefix.list)
		{
			if (itemAsset.pool.Contains(pPool))
			{
				string id = itemAsset.id;
				if (id == "0")
				{
					ItemGenerator.list_id_string = ItemGenerator.list_id_string + "\nitem_" + pID;
				}
				else
				{
					ItemGenerator.list_id_string = string.Concat(new string[]
					{
						ItemGenerator.list_id_string,
						"\nitem_",
						pID,
						"_",
						id
					});
				}
			}
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00027B8C File Offset: 0x00025D8C
	public static void generateItem(ItemAsset pItem, string pMaterial = "base", ActorEquipmentSlot pSlot = null, int pYear = 0, string pWhere = null, string pWho = null, int pTries = 1)
	{
		if (pWhere == null)
		{
			pWhere = null;
		}
		if (pWho == null)
		{
			pWho = null;
		}
		ItemData itemData = new ItemData();
		string pool = pItem.pool;
		string text = "0";
		string text2;
		if (pItem.suffixes.Count == 0 && pItem.prefixes.Count == 0)
		{
			text2 = ItemGenerator.getPool("prefix_" + pool).GetRandom<string>();
			ItemAsset itemAsset = AssetManager.items_prefix.get(text2);
			if (pTries > 1)
			{
				while (pTries > 0)
				{
					itemAsset = AssetManager.items_prefix.get(text2);
					pTries--;
					if (itemAsset.quality == ItemQuality.Legendary)
					{
						break;
					}
				}
			}
			if (itemAsset.quality > ItemQuality.Normal)
			{
				text = ItemGenerator.getPool("suffix_" + pool).GetRandom<string>();
			}
		}
		else
		{
			text = ((pItem.suffixes.Count > 0) ? pItem.suffixes.GetRandom<string>() : "0");
			text2 = ((pItem.prefixes.Count > 0) ? pItem.prefixes.GetRandom<string>() : "0");
		}
		string text3 = "";
		if (text2 != "0")
		{
			text3 = text3 + text2 + " ";
		}
		text3 = text3 + pMaterial + " ";
		text3 += pItem.id;
		if (text != "0")
		{
			text3 = text3 + " of " + text;
		}
		itemData.id = pItem.id;
		itemData.material = pMaterial;
		itemData.prefix = text2;
		itemData.suffix = text;
		itemData.year = pYear;
		itemData.by = pWho;
		itemData.from = pWhere;
		itemData.type = pSlot.type;
		pSlot.setItem(itemData);
		ItemGenerator.tString = ItemGenerator.tString + "\n" + text3;
	}

	// Token: 0x040002DB RID: 731
	private static Dictionary<string, List<string>> pools;

	// Token: 0x040002DC RID: 732
	private static string tString = "";

	// Token: 0x040002DD RID: 733
	private static string list_id_string;
}
