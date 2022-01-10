using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class TileTypeBase : Asset
{
	// Token: 0x060001E1 RID: 481 RVA: 0x00024B9B File Offset: 0x00022D9B
	public bool IsType(string v)
	{
		return this.id == v;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00024BA9 File Offset: 0x00022DA9
	public bool IsType(TileTypeBase pType)
	{
		return pType.indexID == this.indexID;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00024BBC File Offset: 0x00022DBC
	public void setBiome(string pType)
	{
		if (pType == null)
		{
			this.biome = string.Empty;
			this.biome_low = string.Empty;
			this.biome_high = string.Empty;
			return;
		}
		this.biome = pType;
		this.biome_low = pType + "_low";
		this.biome_high = pType + "_high";
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00024C18 File Offset: 0x00022E18
	public void setDrawLayer(TileZIndexes pForceZ, string pForceOtherName = null)
	{
		if (pForceZ == TileZIndexes.nothing)
		{
			this.render_z = TileTypeBase.last_z;
			TileTypeBase.last_z++;
		}
		else
		{
			this.render_z = (int)pForceZ;
		}
		if (!string.IsNullOrEmpty(pForceOtherName))
		{
			this.drawLayerName = pForceOtherName;
			return;
		}
		this.drawLayerName = this.id;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00024C64 File Offset: 0x00022E64
	public void setAutoGrowPlants(GrowTypeSelector pSelector, params string[] pTypes)
	{
		this.grow_types_list_plants = Enumerable.ToList<string>(pTypes);
		this.grow_type_selector_plants = pSelector;
		this.grow_vegetation_auto = true;
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00024C80 File Offset: 0x00022E80
	public void setAutoGrowTrees(GrowTypeSelector pSelector, params string[] pTypes)
	{
		this.grow_types_list_trees = new List<string>();
		List<string> list = Enumerable.ToList<string>(pTypes);
		for (int i = 0; i < list.Count; i++)
		{
			string text = list[i];
			if (text.Contains("#"))
			{
				string[] array = text.Split(new char[]
				{
					'#'
				});
				string text2 = array[0];
				int num = int.Parse(array[1]);
				for (int j = 0; j < num; j++)
				{
					this.grow_types_list_trees.Add(text2);
				}
			}
			else
			{
				this.grow_types_list_trees.Add(text);
			}
		}
		this.grow_type_selector_trees = pSelector;
		this.grow_vegetation_auto = true;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00024D1C File Offset: 0x00022F1C
	public void copyAutoGrowTrees(TileTypeBase pType)
	{
		this.grow_types_list_trees = pType.grow_types_list_trees;
		this.grow_type_selector_trees = pType.grow_type_selector_trees;
		this.grow_vegetation_auto = pType.grow_vegetation_auto;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00024D44 File Offset: 0x00022F44
	public void addUnitsToSpawn(params string[] pUnitIDs)
	{
		this.spawn_units_auto = true;
		if (this.spawn_units_list == null)
		{
			this.spawn_units_list = new List<string>();
		}
		foreach (string text in pUnitIDs)
		{
			this.spawn_units_list.Add(text);
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00024D89 File Offset: 0x00022F89
	public void hashsetAdd(WorldTile pTile)
	{
		this._hashset_dirty = true;
		this.hashset.Add(pTile);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00024D9F File Offset: 0x00022F9F
	public void hashsetRemove(WorldTile pTile)
	{
		this._hashset_dirty = true;
		this.hashset.Remove(pTile);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00024DB5 File Offset: 0x00022FB5
	public List<WorldTile> getCurrentTiles()
	{
		if (this._hashset_dirty)
		{
			this._hashset_dirty = false;
			this._current_tiles = Enumerable.ToList<WorldTile>(this.hashset);
		}
		return this._current_tiles;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00024DDD File Offset: 0x00022FDD
	public void hashsetClear()
	{
		if (this._current_tiles != null)
		{
			this._current_tiles.Clear();
		}
		this.hashset.Clear();
		this._hashset_dirty = false;
	}

	// Token: 0x040001E9 RID: 489
	public const int FIRE_STAGES = 30;

	// Token: 0x040001EA RID: 490
	public const int BURNED_STAGES = 15;

	// Token: 0x040001EB RID: 491
	public const int EXPLOSION_STAGES = 60;

	// Token: 0x040001EC RID: 492
	public const int MAX_HEIGHT = 255;

	// Token: 0x040001ED RID: 493
	[NonSerialized]
	public HashSetWorldTile hashset = new HashSetWorldTile();

	// Token: 0x040001EE RID: 494
	[NonSerialized]
	private bool _hashset_dirty;

	// Token: 0x040001EF RID: 495
	[NonSerialized]
	private List<WorldTile> _current_tiles;

	// Token: 0x040001F0 RID: 496
	public static Color edge_color_ocean = Toolbox.makeColor("#2D61AF");

	// Token: 0x040001F1 RID: 497
	public static Color edge_color_no_ocean = Toolbox.makeColor("#494949");

	// Token: 0x040001F2 RID: 498
	public static Color edge_color_hills = Toolbox.makeColor("#313333");

	// Token: 0x040001F3 RID: 499
	public static Color edge_color_mountain = Toolbox.makeColor("#2C3032");

	// Token: 0x040001F4 RID: 500
	internal TileType increaseTo;

	// Token: 0x040001F5 RID: 501
	internal TileType decreaseTo;

	// Token: 0x040001F6 RID: 502
	public WorldAction unitDeathAction;

	// Token: 0x040001F7 RID: 503
	public TileStepAction stepAction;

	// Token: 0x040001F8 RID: 504
	public float stepActionChance = 0.05f;

	// Token: 0x040001F9 RID: 505
	public bool force_edge_variation;

	// Token: 0x040001FA RID: 506
	public int force_edge_variation_frame;

	// Token: 0x040001FB RID: 507
	public string increaseToID = string.Empty;

	// Token: 0x040001FC RID: 508
	public string decreaseToID = string.Empty;

	// Token: 0x040001FD RID: 509
	public string freezeToID = string.Empty;

	// Token: 0x040001FE RID: 510
	public string forceUnitSkinSet = "default";

	// Token: 0x040001FF RID: 511
	public bool creep;

	// Token: 0x04000200 RID: 512
	public bool wasteland;

	// Token: 0x04000201 RID: 513
	public int indexID;

	// Token: 0x04000202 RID: 514
	public static int last_indexID = 0;

	// Token: 0x04000203 RID: 515
	public string tileName;

	// Token: 0x04000204 RID: 516
	public float walkMod = 1f;

	// Token: 0x04000205 RID: 517
	public TileRank rankType;

	// Token: 0x04000206 RID: 518
	public TileRank creepRankType;

	// Token: 0x04000207 RID: 519
	public string biome = string.Empty;

	// Token: 0x04000208 RID: 520
	public string biome_low = string.Empty;

	// Token: 0x04000209 RID: 521
	public string biome_high = string.Empty;

	// Token: 0x0400020A RID: 522
	public bool canBeRemovedWithSickle;

	// Token: 0x0400020B RID: 523
	public bool canBeRemovedWithBucket;

	// Token: 0x0400020C RID: 524
	public bool canBeRemovedWithDemolish;

	// Token: 0x0400020D RID: 525
	public bool growToNearbyTiles;

	// Token: 0x0400020E RID: 526
	public int grassStrength;

	// Token: 0x0400020F RID: 527
	[NonSerialized]
	public List<string> spawn_units_list;

	// Token: 0x04000210 RID: 528
	[NonSerialized]
	public bool spawn_units_auto;

	// Token: 0x04000211 RID: 529
	public bool block;

	// Token: 0x04000212 RID: 530
	[NonSerialized]
	public TileSprites sprites;

	// Token: 0x04000213 RID: 531
	public TileLayerType layerType;

	// Token: 0x04000214 RID: 532
	public int render_z;

	// Token: 0x04000215 RID: 533
	public string drawLayerName;

	// Token: 0x04000216 RID: 534
	public static int last_z = 0;

	// Token: 0x04000217 RID: 535
	public bool canBeSetOnFire;

	// Token: 0x04000218 RID: 536
	public bool burnable;

	// Token: 0x04000219 RID: 537
	public bool canBeFilledWithOcean;

	// Token: 0x0400021A RID: 538
	public string fillToOcean;

	// Token: 0x0400021B RID: 539
	public bool liquid;

	// Token: 0x0400021C RID: 540
	public bool swamp;

	// Token: 0x0400021D RID: 541
	public bool ocean;

	// Token: 0x0400021E RID: 542
	public bool ground;

	// Token: 0x0400021F RID: 543
	public bool frozen;

	// Token: 0x04000220 RID: 544
	public bool road;

	// Token: 0x04000221 RID: 545
	public bool life;

	// Token: 0x04000222 RID: 546
	public bool damagedWhenWalked;

	// Token: 0x04000223 RID: 547
	public bool remove_on_heat;

	// Token: 0x04000224 RID: 548
	public bool remove_on_freeze;

	// Token: 0x04000225 RID: 549
	public bool greyGoo;

	// Token: 0x04000226 RID: 550
	public bool grass;

	// Token: 0x04000227 RID: 551
	public bool sand;

	// Token: 0x04000228 RID: 552
	public bool trees;

	// Token: 0x04000229 RID: 553
	public bool rocks;

	// Token: 0x0400022A RID: 554
	public bool soil;

	// Token: 0x0400022B RID: 555
	public bool terraformAfterFire;

	// Token: 0x0400022C RID: 556
	public bool explodable;

	// Token: 0x0400022D RID: 557
	public bool explodableDelayed;

	// Token: 0x0400022E RID: 558
	public bool explodableTimed;

	// Token: 0x0400022F RID: 559
	public bool explodableByOcean;

	// Token: 0x04000230 RID: 560
	public bool ignoreOceanEdgeRendering;

	// Token: 0x04000231 RID: 561
	public int explodeRange;

	// Token: 0x04000232 RID: 562
	public bool damageUnits;

	// Token: 0x04000233 RID: 563
	public int damage = 1;

	// Token: 0x04000234 RID: 564
	public bool lava;

	// Token: 0x04000235 RID: 565
	public int lavaLevel = -1;

	// Token: 0x04000236 RID: 566
	public bool edge_hills;

	// Token: 0x04000237 RID: 567
	public bool edge_mountains;

	// Token: 0x04000238 RID: 568
	[NonSerialized]
	public GrowTypeSelector grow_type_selector_trees;

	// Token: 0x04000239 RID: 569
	[NonSerialized]
	public GrowTypeSelector grow_type_selector_plants;

	// Token: 0x0400023A RID: 570
	[NonSerialized]
	public List<string> grow_types_list_trees;

	// Token: 0x0400023B RID: 571
	[NonSerialized]
	public List<string> grow_types_list_plants;

	// Token: 0x0400023C RID: 572
	[NonSerialized]
	public bool grow_vegetation_auto;

	// Token: 0x0400023D RID: 573
	public bool canGrowBiomeGrass;

	// Token: 0x0400023E RID: 574
	public int explodeTimer;

	// Token: 0x0400023F RID: 575
	public int cost = 1;

	// Token: 0x04000240 RID: 576
	public bool canBeFarmField;

	// Token: 0x04000241 RID: 577
	public bool canBuildOn;

	// Token: 0x04000242 RID: 578
	public float fireChance = 1f;

	// Token: 0x04000243 RID: 579
	public Color32 color;

	// Token: 0x04000244 RID: 580
	public Color32 edge_color;

	// Token: 0x04000245 RID: 581
	public bool drawPixel;

	// Token: 0x04000246 RID: 582
	public int strength = 3;

	// Token: 0x04000247 RID: 583
	public int heightMin = -1;

	// Token: 0x04000248 RID: 584
	public int[] additional_height;

	// Token: 0x04000249 RID: 585
	public bool used_in_generator;
}
