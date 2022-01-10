using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class TileLibrary : AssetLibrary<TileType>
{
	// Token: 0x060001D0 RID: 464 RVA: 0x00023BF0 File Offset: 0x00021DF0
	public override void init()
	{
		base.init();
		TileLibrary.list_generator = new List<TileType>();
		this.dict = new Dictionary<string, TileType>();
		this.SetListTo("generator");
		Toolbox.clear = Color.clear;
		TileLibrary.deep_ocean = this.add(new TileType
		{
			id = "deep_ocean",
			color = Toolbox.makeColor("#3370CC"),
			liquid = true,
			ocean = true,
			heightMin = 0,
			decreaseToID = "pit_deep_ocean",
			increaseToID = "pit_close_ocean",
			walkMod = 0.1f,
			strength = 0,
			layerType = TileLayerType.Ocean
		});
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.deep_ocean, null);
		this.t.render_z = 0;
		TileLibrary.close_ocean = this.clone("close_ocean", "deep_ocean");
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.close_ocean, null);
		this.t.drawPixel = true;
		this.t.color = Toolbox.makeColor("#4084E2");
		this.t.heightMin = 30;
		this.t.decreaseToID = "pit_close_ocean";
		this.t.increaseToID = "pit_shallow_waters";
		this.t.strength = 0;
		this.t.layerType = TileLayerType.Ocean;
		TileLibrary.shallow_waters = this.add(new TileType
		{
			id = "shallow_waters",
			drawPixel = true,
			color = Toolbox.makeColor("#55AEF0"),
			edge_color = Toolbox.makeColor("#3F90EA"),
			liquid = true,
			ocean = true,
			heightMin = 70,
			freezeToID = "ice",
			decreaseToID = "pit_shallow_waters",
			increaseToID = "sand",
			walkMod = 0.1f,
			strength = 0,
			layerType = TileLayerType.Ocean
		});
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.shallow_waters, null);
		TileLibrary.pit_deep_ocean = this.clone("pit_deep_ocean", "deep_ocean");
		this.t.setDrawLayer(TileZIndexes.pit_deep_ocean, null);
		this.t.drawPixel = true;
		this.t.color = Toolbox.makeColor("#898989");
		this.t.liquid = false;
		this.t.ocean = false;
		this.t.walkMod = 1f;
		this.t.canBeFilledWithOcean = true;
		this.t.fillToOcean = "deep_ocean";
		this.t.decreaseToID = "";
		this.t.increaseToID = "pit_close_ocean";
		this.t.canBeSetOnFire = true;
		this.t.layerType = TileLayerType.Ground;
		this.t.strength = 2;
		TileLibrary.pit_close_ocean = this.clone("pit_close_ocean", "close_ocean");
		this.t.setDrawLayer(TileZIndexes.pit_close_ocean, null);
		this.t.drawPixel = true;
		this.t.color = Toolbox.makeColor("#A0A0A0");
		this.t.liquid = false;
		this.t.ocean = false;
		this.t.walkMod = 1f;
		this.t.canBeFilledWithOcean = true;
		this.t.fillToOcean = "close_ocean";
		this.t.decreaseToID = "pit_deep_ocean";
		this.t.increaseToID = "pit_shallow_waters";
		this.t.canBeSetOnFire = true;
		this.t.layerType = TileLayerType.Ground;
		this.t.strength = 2;
		TileLibrary.pit_shallow_waters = this.clone("pit_shallow_waters", "shallow_waters");
		this.t.setDrawLayer(TileZIndexes.pit_shallow_waters, null);
		this.t.drawPixel = true;
		this.t.color = Toolbox.makeColor("#C1C1C1");
		this.t.liquid = false;
		this.t.ocean = false;
		this.t.walkMod = 1f;
		this.t.canBeFilledWithOcean = true;
		this.t.fillToOcean = "shallow_waters";
		this.t.decreaseToID = "pit_close_ocean";
		this.t.increaseToID = "sand";
		this.t.freezeToID = string.Empty;
		this.t.canBeSetOnFire = true;
		this.t.layerType = TileLayerType.Ground;
		this.t.strength = 2;
		this.add(new TileType
		{
			id = "border_pit",
			layerType = TileLayerType.Ground
		});
		this.t.setDrawLayer(TileZIndexes.border_pit, null);
		this.add(new TileType
		{
			id = "border_water",
			layerType = TileLayerType.Ground
		});
		this.t.setDrawLayer(TileZIndexes.border_water, null);
		TileLibrary.sand = this.add(new TileType
		{
			cost = 116,
			id = "sand",
			sand = true,
			drawPixel = true,
			color = Toolbox.makeColor("#F7E898"),
			edge_color = Toolbox.makeColor("#D8C08C"),
			heightMin = 98,
			additional_height = new int[]
			{
				6,
				5
			},
			decreaseToID = "pit_shallow_waters",
			increaseToID = "soil_low",
			ground = true,
			walkMod = 0.7f,
			freezeToID = "snow_sand",
			creepRankType = TileRank.Low,
			canBeSetOnFire = true,
			canBuildOn = true
		});
		this.t.addUnitsToSpawn(new string[]
		{
			"crab"
		});
		this.t.setAutoGrowTrees(new GrowTypeSelector(TileActionLibrary.getGrowTypeSand), Array.Empty<string>());
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.sand, null);
		TileLibrary.soil_low = this.add(new TileType
		{
			cost = 115,
			drawPixel = true,
			id = "soil_low",
			color = Toolbox.makeColor("#E2934B"),
			heightMin = 108,
			decreaseToID = "sand",
			increaseToID = "soil_high",
			ground = true,
			walkMod = 1f,
			canGrowBiomeGrass = true,
			soil = true,
			freezeToID = "snow_low",
			rankType = TileRank.Low,
			creepRankType = TileRank.Low,
			canBeFarmField = true,
			canBuildOn = true,
			canBeSetOnFire = true,
			used_in_generator = true
		});
		this.t.setDrawLayer(TileZIndexes.soil_low, null);
		TileLibrary.soil_high = this.add(new TileType
		{
			cost = 120,
			drawPixel = true,
			id = "soil_high",
			color = Toolbox.makeColor("#B66F3A"),
			heightMin = 128,
			additional_height = new int[]
			{
				15,
				16,
				17,
				14,
				13,
				12,
				11,
				10
			},
			decreaseToID = "soil_low",
			increaseToID = "hills",
			ground = true,
			walkMod = 1f,
			rankType = TileRank.High,
			creepRankType = TileRank.High,
			canGrowBiomeGrass = true,
			soil = true,
			freezeToID = "snow_high",
			canBeFarmField = true,
			canBuildOn = true,
			canBeSetOnFire = true,
			used_in_generator = true
		});
		this.t.setDrawLayer(TileZIndexes.soil_high, null);
		TileLibrary.lava0 = this.add(new TileType
		{
			cost = 100,
			drawPixel = true,
			id = "lava0",
			color = Toolbox.makeColor("#F62D14"),
			decreaseToID = "sand",
			increaseToID = "hills",
			liquid = true,
			walkMod = 0.2f,
			damageUnits = true,
			damage = 150,
			lava = true,
			lavaLevel = 0,
			strength = 0,
			layerType = TileLayerType.Lava
		});
		this.t.stepAction = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.stepActionChance = 0.9f;
		this.t.setDrawLayer(TileZIndexes.lava0, null);
		TileLibrary.lava1 = this.clone("lava1", "lava0");
		this.t.setDrawLayer(TileZIndexes.lava1, null);
		this.t.color = Toolbox.makeColor("#FF6700");
		this.t.stepAction = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.stepActionChance = 0.9f;
		this.t.lavaLevel = 1;
		TileLibrary.lava2 = this.clone("lava2", "lava0");
		this.t.setDrawLayer(TileZIndexes.lava2, null);
		this.t.color = Toolbox.makeColor("#FFAC00");
		this.t.stepAction = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.stepActionChance = 0.9f;
		this.t.lavaLevel = 2;
		TileLibrary.lava3 = this.clone("lava3", "lava0");
		this.t.setDrawLayer(TileZIndexes.lava3, null);
		this.t.color = Toolbox.makeColor("#FFDE00");
		this.t.stepAction = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.stepActionChance = 0.9f;
		this.t.lavaLevel = 3;
		TileLibrary.hills = this.add(new TileType
		{
			cost = 140,
			drawPixel = true,
			id = "hills",
			color = Toolbox.makeColor("#5B5E5C"),
			heightMin = 199,
			rocks = true,
			ground = true,
			edge_hills = true,
			additional_height = new int[]
			{
				2,
				-6
			},
			decreaseToID = "soil_high",
			increaseToID = "mountains",
			walkMod = 0.4f,
			freezeToID = "snow_hills",
			canBeSetOnFire = true
		});
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.hills, null);
		TileLibrary.mountains = this.add(new TileType
		{
			cost = 160,
			drawPixel = true,
			id = "mountains",
			color = Toolbox.makeColor("#414545"),
			heightMin = 210,
			rocks = true,
			edge_mountains = true,
			additional_height = new int[]
			{
				2,
				4
			},
			decreaseToID = "hills",
			walkMod = 0.1f,
			freezeToID = "snow_block",
			canBeSetOnFire = true,
			layerType = TileLayerType.Block,
			block = true,
			force_edge_variation = true,
			force_edge_variation_frame = 2
		});
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.mountains, null);
		TileLibrary.grey_goo = this.add(new TileType
		{
			cost = 10,
			drawPixel = true,
			greyGoo = true,
			id = "grey_goo",
			color = Toolbox.makeColor("#5D6191"),
			decreaseToID = "pit_deep_ocean",
			burnable = true,
			ground = false,
			walkMod = 0.1f,
			damageUnits = true,
			damage = 50,
			strength = 0,
			life = true,
			layerType = TileLayerType.Goo
		});
		this.t.setDrawLayer(TileZIndexes.grey_goo, null);
		this._depth_list_generator = new TileType[256];
		this._depth_list_gameplay = new TileType[256];
		foreach (TileType tileType in this.list)
		{
			if (tileType.used_in_generator)
			{
				TileLibrary.list_generator.Add(tileType);
			}
		}
		this.SetListTo("generator");
		for (int i = 0; i < this._depth_list_generator.Length; i++)
		{
			this._depth_list_generator.SetValue(this.GetTypeByDepth(i, TileLibrary.list_generator), i);
		}
		this.SetListTo("gameplay");
		for (int j = 0; j < this._depth_list_gameplay.Length; j++)
		{
			this._depth_list_gameplay.SetValue(this.GetTypeByDepth(j, this.list), j);
		}
		this.SetListTo("generator");
		foreach (TileType tileType2 in this.list)
		{
			tileType2.decreaseTo = this.getGen(tileType2.decreaseToID);
			tileType2.increaseTo = this.getGen(tileType2.increaseToID);
		}
		this.loadTileSprites();
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00024940 File Offset: 0x00022B40
	private void loadTileSprites()
	{
		foreach (TileType pType in this.list)
		{
			this.loadSpritesForTile(pType);
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00024994 File Offset: 0x00022B94
	private void loadSpritesForTile(TileType pType)
	{
		Sprite[] array = Resources.LoadAll<Sprite>("tiles/" + pType.id);
		if (array == null || array.Length == 0)
		{
			return;
		}
		pType.sprites = new TileSprites();
		foreach (Sprite pSprite in array)
		{
			pType.sprites.addVariation(pSprite);
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x000249EA File Offset: 0x00022BEA
	public TileType getGen(string pID)
	{
		if (!this.dict.ContainsKey(pID))
		{
			return null;
		}
		return this.dict[pID];
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00024A08 File Offset: 0x00022C08
	public override TileType add(TileType pAsset)
	{
		pAsset.indexID = TileTypeBase.last_indexID++;
		return base.add(pAsset);
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00024A24 File Offset: 0x00022C24
	public void SetListTo(string pVal)
	{
		if (pVal == "generator")
		{
			this._depth_list = this._depth_list_generator;
			return;
		}
		if (pVal == "gameplay")
		{
			this._depth_list = this._depth_list_gameplay;
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00024A5C File Offset: 0x00022C5C
	public TileType GetTypeByDepth(int pHeight, List<TileType> pList)
	{
		TileType tileType = null;
		for (int i = 0; i < pList.Count; i++)
		{
			TileType tileType2 = pList[i];
			if (tileType2.heightMin != -1)
			{
				if (tileType == null)
				{
					tileType = tileType2;
				}
				else if (pHeight >= tileType2.heightMin)
				{
					tileType = tileType2;
				}
			}
		}
		return tileType;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00024AA0 File Offset: 0x00022CA0
	public override TileType clone(string pNew, string pFrom)
	{
		TileType tileType = base.clone(pNew, pFrom);
		tileType.canBeFarmField = false;
		tileType.used_in_generator = false;
		return tileType;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00024AB8 File Offset: 0x00022CB8
	public TileType GetTypeByDepth(WorldTile pWorldTile)
	{
		return this._depth_list[pWorldTile.Height];
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00024AC8 File Offset: 0x00022CC8
	public static string getRandomTileType(string pExclude = null)
	{
		TileLibrary._temp_list.Clear();
		TileLibrary._temp_list.Add("deep_ocean");
		TileLibrary._temp_list.Add("close_ocean");
		TileLibrary._temp_list.Add("shallow_waters");
		TileLibrary._temp_list.Add("sand");
		TileLibrary._temp_list.Add("soil_low");
		TileLibrary._temp_list.Add("soil_high");
		TileLibrary._temp_list.Add("soil_low");
		TileLibrary._temp_list.Add("hills");
		TileLibrary._temp_list.Add("mountains");
		if (pExclude != null)
		{
			TileLibrary._temp_list.Remove(pExclude);
		}
		return Toolbox.getRandom<string>(TileLibrary._temp_list);
	}

	// Token: 0x040001C7 RID: 455
	public static List<TileType> list_generator;

	// Token: 0x040001C8 RID: 456
	public TileType[] _depth_list;

	// Token: 0x040001C9 RID: 457
	public TileType[] _depth_list_generator;

	// Token: 0x040001CA RID: 458
	public TileType[] _depth_list_gameplay;

	// Token: 0x040001CB RID: 459
	public static TileType mountains;

	// Token: 0x040001CC RID: 460
	public static TileType hills;

	// Token: 0x040001CD RID: 461
	public static TileType grey_goo;

	// Token: 0x040001CE RID: 462
	public static TileType deep_ocean;

	// Token: 0x040001CF RID: 463
	public static TileType close_ocean;

	// Token: 0x040001D0 RID: 464
	public static TileType shallow_waters;

	// Token: 0x040001D1 RID: 465
	public static TileType sand;

	// Token: 0x040001D2 RID: 466
	public static TileType soil_low;

	// Token: 0x040001D3 RID: 467
	public static TileType soil_high;

	// Token: 0x040001D4 RID: 468
	public static TileType lava0;

	// Token: 0x040001D5 RID: 469
	public static TileType lava1;

	// Token: 0x040001D6 RID: 470
	public static TileType lava2;

	// Token: 0x040001D7 RID: 471
	public static TileType lava3;

	// Token: 0x040001D8 RID: 472
	public static TileType pit_deep_ocean;

	// Token: 0x040001D9 RID: 473
	public static TileType pit_close_ocean;

	// Token: 0x040001DA RID: 474
	public static TileType pit_shallow_waters;

	// Token: 0x040001DB RID: 475
	public static TileType border_pit;

	// Token: 0x040001DC RID: 476
	public static TileType border_water;

	// Token: 0x040001DD RID: 477
	private static List<string> _temp_list = new List<string>();
}
