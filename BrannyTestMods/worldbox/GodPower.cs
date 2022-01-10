using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
[Serializable]
public class GodPower : Asset
{
	// Token: 0x06000145 RID: 325 RVA: 0x00016F7C File Offset: 0x0001517C
	internal static void addPower(GodPower pPower, PowerButton pButton)
	{
		GodPower.godPowersOnCanvas.Add(pPower.id, pPower);
		if (pPower.requiresPremium)
		{
			GodPower.premiumPowers.Add(pPower);
			GodPower.premiumButtons.Add(pButton);
		}
		if (!pPower.requiresPremium)
		{
			GodPower.powersRank0.Add(pButton);
			return;
		}
		switch (pPower.rank)
		{
		case PowerRank.Rank0_free:
			break;
		case PowerRank.Rank1_common:
			GodPower.powersRank1.Add(pButton);
			return;
		case PowerRank.Rank2_normal:
			GodPower.powersRank2.Add(pButton);
			return;
		case PowerRank.Rank3_good:
			GodPower.powersRank3.Add(pButton);
			return;
		case PowerRank.Rank4_awesome:
			GodPower.powersRank4.Add(pButton);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0001701D File Offset: 0x0001521D
	public Sprite getIconSprite()
	{
		if (this.cached_sprite == null)
		{
			this.cached_sprite = (Sprite)Resources.Load("ui/Icons/" + this.icon, typeof(Sprite));
		}
		return this.cached_sprite;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00017060 File Offset: 0x00015260
	public static void diagnostic()
	{
		LogText.log("Ranked Powers", "Print", "");
		GodPower.printRankedPowerButtons("rank0", GodPower.powersRank0);
		GodPower.printRankedPowerButtons("rank1", GodPower.powersRank1);
		GodPower.printRankedPowerButtons("rank2", GodPower.powersRank2);
		GodPower.printRankedPowerButtons("rank3", GodPower.powersRank3);
		GodPower.printRankedPowerButtons("rank4", GodPower.powersRank4);
		GodPower.printRankedPowers("premium powers", GodPower.premiumPowers);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000170DC File Offset: 0x000152DC
	private static void printRankedPowerButtons(string pID, List<PowerButton> pList)
	{
		string text = "";
		foreach (PowerButton powerButton in pList)
		{
			text = text + powerButton.godPower.id + ", ";
		}
		if (text.Length > 2)
		{
			text = text.Substring(0, text.Length - 2);
		}
		LogText.log(pID, text, "");
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00017168 File Offset: 0x00015368
	private static void printRankedPowers(string pID, List<GodPower> pList)
	{
		string text = "";
		foreach (GodPower godPower in pList)
		{
			text = text + godPower.id + ", ";
		}
		text = text.Substring(0, text.Length - 2);
		LogText.log(pID, text, "");
	}

	// Token: 0x0400010B RID: 267
	internal static List<PowerButton> powersRank0 = new List<PowerButton>();

	// Token: 0x0400010C RID: 268
	internal static List<PowerButton> powersRank1 = new List<PowerButton>();

	// Token: 0x0400010D RID: 269
	internal static List<PowerButton> powersRank2 = new List<PowerButton>();

	// Token: 0x0400010E RID: 270
	internal static List<PowerButton> powersRank3 = new List<PowerButton>();

	// Token: 0x0400010F RID: 271
	internal static List<PowerButton> powersRank4 = new List<PowerButton>();

	// Token: 0x04000110 RID: 272
	internal static List<PowerButton> premiumButtons = new List<PowerButton>();

	// Token: 0x04000111 RID: 273
	internal static List<GodPower> premiumPowers = new List<GodPower>();

	// Token: 0x04000112 RID: 274
	internal static Dictionary<string, GodPower> godPowersOnCanvas = new Dictionary<string, GodPower>();

	// Token: 0x04000113 RID: 275
	public string name = "DEFAULT NAME";

	// Token: 0x04000114 RID: 276
	public bool requiresPremium;

	// Token: 0x04000115 RID: 277
	public PowerRank rank;

	// Token: 0x04000116 RID: 278
	public string spawnSound = "";

	// Token: 0x04000117 RID: 279
	public string icon = string.Empty;

	// Token: 0x04000118 RID: 280
	[NonSerialized]
	public Sprite cached_sprite;

	// Token: 0x04000119 RID: 281
	public bool showToolSizes;

	// Token: 0x0400011A RID: 282
	public bool unselectWhenWindow;

	// Token: 0x0400011B RID: 283
	public MapMode force_map_text;

	// Token: 0x0400011C RID: 284
	public bool holdAction;

	// Token: 0x0400011D RID: 285
	public float clickInterval;

	// Token: 0x0400011E RID: 286
	public float particleInterval;

	// Token: 0x0400011F RID: 287
	public float fallingChance = 0.95f;

	// Token: 0x04000120 RID: 288
	public string tileType = string.Empty;

	// Token: 0x04000121 RID: 289
	public TileType cached_tile_type_asset;

	// Token: 0x04000122 RID: 290
	public string topTileType = string.Empty;

	// Token: 0x04000123 RID: 291
	public TopTileType cached_top_tile_type_asset;

	// Token: 0x04000124 RID: 292
	public string dropID = string.Empty;

	// Token: 0x04000125 RID: 293
	public DropAsset cached_drop_asset;

	// Token: 0x04000126 RID: 294
	public string forceBrush;

	// Token: 0x04000127 RID: 295
	public bool drawLines;

	// Token: 0x04000128 RID: 296
	public PowerActionType type;

	// Token: 0x04000129 RID: 297
	public bool highlight;

	// Token: 0x0400012A RID: 298
	public PowerActionWithID click_brush_action;

	// Token: 0x0400012B RID: 299
	public PowerActionWithID click_action;

	// Token: 0x0400012C RID: 300
	public PowerAction click_power_brush_action;

	// Token: 0x0400012D RID: 301
	public PowerAction click_power_action;

	// Token: 0x0400012E RID: 302
	public string toggle_name = string.Empty;

	// Token: 0x0400012F RID: 303
	public PowerToggleAction toggle_action;

	// Token: 0x04000130 RID: 304
	public string actorStatsId = "?";

	// Token: 0x04000131 RID: 305
	public float actorSpawnHeight = 6f;

	// Token: 0x04000132 RID: 306
	public string showSpawnEffect = string.Empty;

	// Token: 0x04000133 RID: 307
	public string printersPrint = "";

	// Token: 0x04000134 RID: 308
	public bool ignoreFastSpawn;

	// Token: 0x04000135 RID: 309
	public bool tester_enabled = true;

	// Token: 0x04000136 RID: 310
	public bool map_modes_switch;

	// Token: 0x04000137 RID: 311
	public bool allow_unit_selection;
}
