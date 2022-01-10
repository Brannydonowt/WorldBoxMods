using System;
using System.Collections.Generic;

// Token: 0x02000045 RID: 69
public class ResourceLibrary : AssetLibrary<ResourceAsset>
{
	// Token: 0x060001B8 RID: 440 RVA: 0x0002297C File Offset: 0x00020B7C
	public override void init()
	{
		base.init();
		this.add(new ResourceAsset
		{
			id = "gold",
			icon = "iconResGold",
			maximum = 9999,
			supplyBoundGive = 600,
			supplyBoundTake = 10,
			supplyGive = 100,
			type = ResType.Currency
		});
		this.add(new ResourceAsset
		{
			id = "berries",
			type = ResType.Food,
			icon = "iconResBerries",
			restoreHunger = 30,
			restoreHealth = 0.05f,
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "bread",
			type = ResType.Food,
			icon = "iconResBread",
			restoreHunger = 60,
			ingredientsAmount = 1,
			restoreHealth = 0.1f,
			ingredients = new string[]
			{
				"wheat"
			},
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "fish",
			type = ResType.Food,
			restoreHunger = 50,
			icon = "iconResFish",
			restoreHealth = 0.15f,
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "meat",
			type = ResType.Food,
			restoreHunger = 60,
			icon = "iconResMeat",
			restoreHealth = 0.15f,
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "sushi",
			type = ResType.Food,
			icon = "iconResSushi",
			restoreHunger = 70,
			restoreHealth = 0.2f,
			ingredientsAmount = 1,
			ingredients = new string[]
			{
				"fish"
			},
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "jam",
			type = ResType.Food,
			icon = "iconResJam",
			restoreHunger = 70,
			ingredientsAmount = 2,
			restoreHealth = 0.3f,
			ingredients = new string[]
			{
				"berries"
			},
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "cider",
			type = ResType.Food,
			icon = "iconResCider",
			restoreHunger = 100,
			ingredientsAmount = 3,
			restoreHealth = 0.4f,
			ingredients = new string[]
			{
				"berries"
			},
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "ale",
			type = ResType.Food,
			icon = "iconResAle",
			restoreHunger = 100,
			ingredientsAmount = 3,
			restoreHealth = 0.3f,
			ingredients = new string[]
			{
				"wheat"
			},
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "burger",
			type = ResType.Food,
			icon = "iconResBurger",
			restoreHunger = 100,
			ingredientsAmount = 1,
			restoreHealth = 0.5f,
			ingredients = new string[]
			{
				"meat",
				"wheat"
			},
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "pie",
			type = ResType.Food,
			icon = "iconResPie",
			restoreHunger = 100,
			ingredientsAmount = 1,
			restoreHealth = 0.5f,
			ingredients = new string[]
			{
				"meat",
				"wheat"
			},
			tradeBound = 50,
			tradeGive = 5
		});
		this.add(new ResourceAsset
		{
			id = "tea",
			type = ResType.Food,
			restoreHunger = 100,
			restoreHealth = 0.25f,
			icon = "iconResTea",
			tradeBound = 50,
			tradeGive = 5,
			ingredients = new string[]
			{
				"wheat"
			}
		});
		this.add(new ResourceAsset
		{
			id = "honey",
			type = ResType.Food,
			restoreHealth = 0.8f,
			restoreHunger = 100,
			icon = "iconResHoney"
		});
		this.add(new ResourceAsset
		{
			id = "wheat",
			type = ResType.Ingredient,
			icon = "iconResWheat",
			supplyBoundGive = 100,
			supplyBoundTake = 20
		});
		this.add(new ResourceAsset
		{
			id = "wood",
			icon = "iconResWood",
			type = ResType.Strategic
		});
		this.add(new ResourceAsset
		{
			id = "stone",
			icon = "iconResStone",
			mineRate = 25,
			type = ResType.Strategic
		});
		this.add(new ResourceAsset
		{
			id = "ore",
			icon = "iconResOre",
			mineRate = 5,
			type = ResType.Ingredient
		});
		this.add(new ResourceAsset
		{
			id = "metals",
			icon = "iconResMetals",
			ingredients = new string[]
			{
				"ore"
			},
			type = ResType.Strategic
		});
		this.add(new ResourceAsset
		{
			id = "bones",
			icon = "iconResBones",
			type = ResType.Strategic
		});
		this.add(new ResourceAsset
		{
			id = "leather",
			icon = "iconResLeather",
			type = ResType.Strategic
		});
		this.add(new ResourceAsset
		{
			id = "gems",
			icon = "iconResGems",
			mineRate = 1,
			type = ResType.Strategic
		});
		foreach (ResourceAsset resourceAsset in this.list)
		{
			if (resourceAsset.mineRate != 0)
			{
				for (int i = 0; i < resourceAsset.mineRate; i++)
				{
					this.poolMine.Add(resourceAsset);
				}
			}
		}
	}

	// Token: 0x040001A4 RID: 420
	public List<ResourceAsset> poolMine = new List<ResourceAsset>();
}
