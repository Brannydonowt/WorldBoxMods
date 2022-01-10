using System;
using System.Collections.Generic;

// Token: 0x020000F8 RID: 248
[Serializable]
public class CityStorage
{
	// Token: 0x0600057F RID: 1407 RVA: 0x00044D90 File Offset: 0x00042F90
	public void loadFromSave()
	{
		if (this.savedItems != null)
		{
			this.itemStorage.load(this.savedItems);
		}
		if (this.savedResources != null)
		{
			foreach (CityStorageSlot cityStorageSlot in this.savedResources)
			{
				if (cityStorageSlot.id == "food")
				{
					cityStorageSlot.id = "wheat";
				}
				if (AssetManager.resources.get(cityStorageSlot.id) != null && cityStorageSlot.amount >= 0)
				{
					cityStorageSlot.create();
					this.putToDict(cityStorageSlot);
				}
			}
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00044E44 File Offset: 0x00043044
	public ResourceAsset getRandomFood()
	{
		if (this.listFood.Count == 0)
		{
			return null;
		}
		this.listFood.Sort(new Comparison<CityStorageSlot>(this.foodSorter));
		for (int i = 0; i < this.listFood.Count; i++)
		{
			CityStorageSlot cityStorageSlot = this.listFood[i];
			if (cityStorageSlot.amount > 0)
			{
				return cityStorageSlot.asset;
			}
		}
		return null;
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00044EAB File Offset: 0x000430AB
	public int foodSorter(CityStorageSlot o1, CityStorageSlot o2)
	{
		return o2.amount.CompareTo(o1.amount);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00044EC0 File Offset: 0x000430C0
	public void save()
	{
		this.savedResources = new List<CityStorageSlot>();
		foreach (CityStorageSlot cityStorageSlot in this.resources.Values)
		{
			if (cityStorageSlot.amount != 0)
			{
				this.savedResources.Add(cityStorageSlot);
			}
		}
		this.savedItems = this.itemStorage.getDataForSave();
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00044F44 File Offset: 0x00043144
	public ItemAsset getMaterialForItem(ItemAsset pItemAsset, ItemAssetLibrary<ItemAsset> pLib, City pCity, bool pCheckCost = true)
	{
		Culture culture = pCity.getCulture();
		if (culture == null)
		{
			return null;
		}
		for (int i = pLib.list.Count - 1; i >= 0; i--)
		{
			ItemAsset itemAsset = pLib.list[i];
			if (pItemAsset.materials.Contains(itemAsset.id) && (!pCheckCost || ((string.IsNullOrEmpty(itemAsset.tech_needed) || culture.haveTech(itemAsset.tech_needed)) && itemAsset.cost_gold <= this.get("gold") && (!(itemAsset.cost_resource_id_1 != "none") || itemAsset.cost_resource_1 <= this.get(itemAsset.cost_resource_id_1)) && this.get(itemAsset.cost_resource_id_1) >= 15 && (!(itemAsset.cost_resource_id_2 != "none") || itemAsset.cost_resource_2 <= this.get(itemAsset.cost_resource_id_2)))))
			{
				return itemAsset;
			}
		}
		return null;
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00045032 File Offset: 0x00043232
	public int get(string pRes)
	{
		if (!this.resources.ContainsKey(pRes))
		{
			return 0;
		}
		return this.resources[pRes].amount;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00045058 File Offset: 0x00043258
	private void addNew(string pResID, int pAmount)
	{
		CityStorageSlot cityStorageSlot = new CityStorageSlot();
		cityStorageSlot.id = pResID;
		cityStorageSlot.amount = pAmount;
		cityStorageSlot.create();
		this.putToDict(cityStorageSlot);
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00045088 File Offset: 0x00043288
	private void putToDict(CityStorageSlot pRes)
	{
		if (this.resources.ContainsKey(pRes.id))
		{
			return;
		}
		this.resources.Add(pRes.id, pRes);
		if (pRes.asset.type == ResType.Food)
		{
			this.resourcesFood.Add(pRes.id, pRes);
			this.listFood.Add(pRes);
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x000450E7 File Offset: 0x000432E7
	public void set(string pRes, int pAmount)
	{
		if (!this.resources.ContainsKey(pRes))
		{
			this.addNew(pRes, pAmount);
			return;
		}
		this.resources[pRes].amount = pAmount;
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00045114 File Offset: 0x00043314
	public void change(string pRes, int pAmount = 1)
	{
		if (DebugConfig.isOn(DebugOption.CityInfiniteResources))
		{
			pAmount = 999;
		}
		if (!this.resources.ContainsKey(pRes))
		{
			this.addNew(pRes, pAmount);
			return;
		}
		CityStorageSlot cityStorageSlot = this.resources[pRes];
		cityStorageSlot.amount += pAmount;
		if (cityStorageSlot.amount > cityStorageSlot.asset.maximum)
		{
			this.resources[pRes].amount = cityStorageSlot.asset.maximum;
		}
	}

	// Token: 0x0400076C RID: 1900
	[NonSerialized]
	public ActorEquipment itemStorage = new ActorEquipment();

	// Token: 0x0400076D RID: 1901
	[NonSerialized]
	public Dictionary<string, CityStorageSlot> resources = new Dictionary<string, CityStorageSlot>();

	// Token: 0x0400076E RID: 1902
	[NonSerialized]
	public Dictionary<string, CityStorageSlot> resourcesFood = new Dictionary<string, CityStorageSlot>();

	// Token: 0x0400076F RID: 1903
	[NonSerialized]
	private List<CityStorageSlot> listFood = new List<CityStorageSlot>();

	// Token: 0x04000770 RID: 1904
	public List<CityStorageSlot> savedResources;

	// Token: 0x04000771 RID: 1905
	public List<ItemData> savedItems;
}
