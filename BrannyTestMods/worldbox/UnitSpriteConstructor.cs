using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000DC RID: 220
public static class UnitSpriteConstructor
{
	// Token: 0x06000492 RID: 1170 RVA: 0x0003E467 File Offset: 0x0003C667
	public static Sprite getSprite(long pID)
	{
		return UnitSpriteConstructor._sprites[pID];
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0003E474 File Offset: 0x0003C674
	private static void checkInit()
	{
		UnitSpriteConstructor.initiated = true;
		UnitSpriteConstructor.units = UnitSpriteConstructor.newAtlas(UnitTextureAtlasID.UnitsSmall);
		UnitSpriteConstructor.boats = UnitSpriteConstructor.newAtlas(UnitTextureAtlasID.Boats);
		UnitSpriteConstructor.buildings = UnitSpriteConstructor.newAtlas(UnitTextureAtlasID.Buildings);
		for (int i = 0; i < UnitSpriteConstructor.atlases.Count; i++)
		{
			UnitSpriteConstructor.atlases[i].newTexture();
		}
		if (!CustomTextureAtlas.filesExists())
		{
			for (int j = 0; j < UnitSpriteConstructor.atlases.Count; j++)
			{
				UnitSpriteConstructor.atlases[j].textures = null;
			}
		}
		UnitSpriteConstructor.currentAtlas = UnitSpriteConstructor.units;
		UnitSpriteConstructor.color_clear = Color.clear;
		UnitSpriteConstructor.color_debug = Color.magenta;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0003E524 File Offset: 0x0003C724
	public static long getBuildingSpriteID(int pBaseSpriteID, KingdomColor pColor)
	{
		long num;
		if (pColor == null)
		{
			num = -1000000L;
		}
		else
		{
			num = (long)(pColor.id + 1);
		}
		return (num + 1L) * 10000000L + (long)pBaseSpriteID;
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0003E558 File Offset: 0x0003C758
	public static Sprite getSpriteBuilding(Building pBuilding, KingdomColor pColor)
	{
		if (!UnitSpriteConstructor.initiated)
		{
			UnitSpriteConstructor.checkInit();
		}
		long buildingSpriteID = UnitSpriteConstructor.getBuildingSpriteID(pBuilding.id_sprite_building, pColor);
		Sprite sprite = null;
		UnitSpriteConstructor._sprites.TryGetValue(buildingSpriteID, ref sprite);
		if (sprite == null)
		{
			sprite = UnitSpriteConstructor.createNewSpriteBuilding(pBuilding.s_main_sprite, pColor);
			UnitSpriteConstructor._sprites.Add(buildingSpriteID, sprite);
		}
		return sprite;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0003E5B4 File Offset: 0x0003C7B4
	public static Sprite getSpriteUnit(AnimationFrameData pFrameData, Actor pActor, Sprite pItem, KingdomColor pColor, Race pRace, int pSkinSet, int pSkinColor, UnitTextureAtlasID pTextureAtlasID)
	{
		pItem = null;
		if (!UnitSpriteConstructor.initiated)
		{
			UnitSpriteConstructor.checkInit();
		}
		if (pSkinSet == -1)
		{
			pSkinSet = 0;
		}
		long num = 0L;
		long num2 = 0L;
		long num3 = 0L;
		long num4 = 0L;
		long num5 = (long)pActor.id_sprite_body;
		if (pActor.s_head_sprite != null)
		{
			num4 = (long)pActor.id_sprite_head;
		}
		if (pSkinColor != -1)
		{
			num3 = (long)(pSkinSet + 1);
			num2 = (long)(pSkinColor + 1);
			if (pColor != null)
			{
				num = (long)(pColor.id + 1);
			}
		}
		else if (pColor != null)
		{
			num = (long)(pColor.id + 1);
		}
		long num6 = num * 100000000L + num4 * 100000L + num5 * 100L + num3 * 10L + num2;
		if (UnitSpriteConstructor._debugActor == pActor)
		{
			UnitSpriteConstructor._debug_id = num6;
			UnitSpriteConstructor._debug_kingdomID = num;
			UnitSpriteConstructor._debug_headId = num4;
			UnitSpriteConstructor._debug_bodyID = num5;
			UnitSpriteConstructor._debug_skinID = num2;
			UnitSpriteConstructor._debug_skinSetID = num3;
		}
		Sprite sprite = null;
		UnitSpriteConstructor._sprites.TryGetValue(num6, ref sprite);
		if (sprite == null)
		{
			sprite = UnitSpriteConstructor.createNewSpriteUnit(pFrameData, pActor.s_body_sprite, pActor.s_head_sprite, pItem, pColor, pActor.stats, pSkinSet, pSkinColor, pTextureAtlasID);
			UnitSpriteConstructor._sprites.Add(num6, sprite);
		}
		return sprite;
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0003E6D4 File Offset: 0x0003C8D4
	private static UnitSpriteConstructorAtlas newAtlas(UnitTextureAtlasID pID)
	{
		UnitSpriteConstructorAtlas unitSpriteConstructorAtlas = new UnitSpriteConstructorAtlas(pID);
		UnitSpriteConstructor.atlases.Add(unitSpriteConstructorAtlas);
		return unitSpriteConstructorAtlas;
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0003E6F4 File Offset: 0x0003C8F4
	public static void reset()
	{
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
	public static Sprite createNewSpriteBuilding(Sprite pMain, KingdomColor pKingdomColor)
	{
		UnitSpriteConstructor.currentAtlas = UnitSpriteConstructor.buildings;
		UnitSpriteConstructor._kingdomColor = pKingdomColor;
		Rect textureRect = pMain.textureRect;
		int num = (int)textureRect.width;
		int num2 = (int)textureRect.height;
		int num3 = 0;
		UnitSpriteConstructor.currentAtlas.checkBounds(num, num2);
		UnitSpriteConstructor._drawSkinColor = false;
		Color32[] pixels = pMain.texture.GetPixels32();
		int width = pMain.texture.width;
		int num4 = 0;
		while ((float)num4 < textureRect.width)
		{
			int num5 = 0;
			while ((float)num5 < textureRect.height)
			{
				int num6 = num4 + (int)textureRect.x;
				int num7 = num5 + (int)textureRect.y;
				int num8 = num6 + num7 * width;
				Color32 color = pixels[num8];
				if (color.a != 0)
				{
					color = UnitSpriteConstructor.checkSpecialColors(color);
					int num9 = num4 + num3 + UnitSpriteConstructor.currentAtlas.last_x;
					int num10 = num5 + UnitSpriteConstructor.currentAtlas.last_y;
					if (num9 < 0)
					{
						num9 = 0;
					}
					if (num10 < 0)
					{
						num10 = 0;
					}
					num8 = num9 + num10 * 1024;
					UnitSpriteConstructor.currentAtlas.pixels[num8] = color;
				}
				num5++;
			}
			num4++;
		}
		UnitSpriteConstructor.dirty = true;
		UnitSpriteConstructor.currentAtlas.dirty = true;
		Rect rect = new Rect((float)UnitSpriteConstructor.currentAtlas.last_x, (float)UnitSpriteConstructor.currentAtlas.last_y, (float)num, (float)num2);
		Vector2 pivot = new Vector2((pMain.pivot.x + (float)num3) / (float)num, pMain.pivot.y / (float)num2);
		Sprite result = Sprite.Create(UnitSpriteConstructor.currentAtlas.texture, rect, pivot, 1f);
		UnitSpriteConstructor.currentAtlas.last_x += num + 1;
		return result;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0003E8B0 File Offset: 0x0003CAB0
	public static Sprite createNewSpriteUnit(AnimationFrameData pFrameData, Sprite pMain, Sprite pHead, Sprite pItem, KingdomColor pKingdomColor, ActorStats pStats, int pSkinSetColor, int pSkinColor, UnitTextureAtlasID pAtlasID)
	{
		if (pAtlasID != UnitTextureAtlasID.UnitsSmall)
		{
			if (pAtlasID == UnitTextureAtlasID.Boats)
			{
				UnitSpriteConstructor.currentAtlas = UnitSpriteConstructor.boats;
			}
		}
		else
		{
			UnitSpriteConstructor.currentAtlas = UnitSpriteConstructor.units;
		}
		Rect textureRect = pMain.textureRect;
		Rect rect = textureRect;
		Rect rect2 = textureRect;
		int num = (int)textureRect.width;
		int num2 = (int)textureRect.height;
		int num3 = 0;
		int num4 = 0;
		UnitSpriteConstructor._kingdomColor = pKingdomColor;
		UnitSpriteConstructor._skin_color = UnitSpriteConstructor.placeholder_color_skin;
		UnitSpriteConstructor._drawSkinColor = false;
		if (pStats != null && pSkinColor != -1)
		{
			UnitSpriteConstructor._skin_color = pStats.color_sets[pSkinSetColor].colors[pSkinColor];
			UnitSpriteConstructor._drawSkinColor = true;
			UnitSpriteConstructor._skin_color_0 = Toolbox.makeDarkerColor(UnitSpriteConstructor._skin_color, 1f);
			UnitSpriteConstructor._skin_color_1 = Toolbox.makeDarkerColor(UnitSpriteConstructor._skin_color, 0.9f);
			UnitSpriteConstructor._skin_color_2 = Toolbox.makeDarkerColor(UnitSpriteConstructor._skin_color, 0.8f);
			UnitSpriteConstructor._skin_color_3 = Toolbox.makeDarkerColor(UnitSpriteConstructor._skin_color, 0.7f);
		}
		if (pHead != null)
		{
			rect = pHead.rect;
			int num5 = (int)pFrameData.posHead_new.y + (int)rect.height - num2;
			if (num5 > 0)
			{
				num4 = num5;
			}
			int num6 = (int)pFrameData.posHead_new.x + (int)rect.width - num;
			if (num6 > 0)
			{
				num3 = num6;
			}
			else if (pFrameData.posHead_new.x < 0f)
			{
				num3 = -(int)pFrameData.posHead_new.x;
			}
		}
		int num7 = num3;
		int num8 = num4;
		num += num7;
		num2 += num8;
		UnitSpriteConstructor.currentAtlas.checkBounds(num, num2);
		Color32[] pixels = pMain.texture.GetPixels32();
		int width = pMain.texture.width;
		int num9 = 0;
		while ((float)num9 < textureRect.width)
		{
			int num10 = 0;
			while ((float)num10 < textureRect.height)
			{
				int num11 = num9 + (int)textureRect.x;
				int num12 = num10 + (int)textureRect.y;
				int num13 = num11 + num12 * width;
				Color32 color = pixels[num13];
				if (color.a != 0)
				{
					color = UnitSpriteConstructor.checkSpecialColors(color);
					int num14 = num9 + num7 + UnitSpriteConstructor.currentAtlas.last_x;
					int num15 = num10 + UnitSpriteConstructor.currentAtlas.last_y;
					if (num14 < 0)
					{
						num14 = 0;
					}
					if (num15 < 0)
					{
						num15 = 0;
					}
					num13 = num14 + num15 * 1024;
					UnitSpriteConstructor.currentAtlas.pixels[num13] = color;
				}
				num10++;
			}
			num9++;
		}
		if (pHead != null)
		{
			pixels = pHead.texture.GetPixels32();
			int width2 = pHead.texture.width;
			int num16 = 0;
			while ((float)num16 < rect.width)
			{
				int num17 = 0;
				while ((float)num17 < rect.height)
				{
					int num18 = num16 + (int)rect.x;
					int num12 = num17 + (int)rect.y;
					int num13 = num18 + num12 * width2;
					Color32 color = pixels[num13];
					if (color.a != 0)
					{
						color = UnitSpriteConstructor.checkSpecialColors(color);
						int num14 = num7 + num16 + (int)pFrameData.posHead_new.x + UnitSpriteConstructor.currentAtlas.last_x - (int)pHead.pivot.x;
						int num15 = num17 + (int)pFrameData.posHead_new.y + UnitSpriteConstructor.currentAtlas.last_y - (int)pHead.pivot.y;
						if (num14 < 0)
						{
							num14 = 0;
						}
						if (num15 < 0)
						{
							num15 = 0;
						}
						num13 = num14 + num15 * 1024;
						UnitSpriteConstructor.currentAtlas.pixels[num13] = color;
					}
					num17++;
				}
				num16++;
			}
		}
		if (pItem != null)
		{
			pixels = pItem.texture.GetPixels32();
			int width3 = pItem.texture.width;
			int num19 = 0;
			while ((float)num19 < rect2.width)
			{
				int num20 = 0;
				while ((float)num20 < rect2.height)
				{
					int num21 = num19 + (int)rect2.x;
					int num12 = num20 + (int)rect2.y;
					int num13 = num21 + num12 * width3;
					Color32 color = pixels[num13];
					if (color.a != 0)
					{
						int num14 = UnitSpriteConstructor.currentAtlas.last_x + num7 + num19 + (int)pFrameData.posItem_new.x - (int)pItem.pivot.x;
						int num15 = UnitSpriteConstructor.currentAtlas.last_y + num20 + (int)pFrameData.posItem_new.y - (int)pItem.pivot.y;
						if (num14 < 0)
						{
							num14 = 0;
						}
						if (num15 < 0)
						{
							num15 = 0;
						}
						num13 = num14 + num15 * 1024;
						UnitSpriteConstructor.currentAtlas.pixels[num13] = color;
					}
					num20++;
				}
				num19++;
			}
		}
		UnitSpriteConstructor.dirty = true;
		UnitSpriteConstructor.currentAtlas.dirty = true;
		Rect rect3 = new Rect((float)UnitSpriteConstructor.currentAtlas.last_x, (float)UnitSpriteConstructor.currentAtlas.last_y, (float)num, (float)num2);
		Vector2 pivot = new Vector2((pMain.pivot.x + (float)num7) / (float)num, pMain.pivot.y / (float)num2);
		Sprite result = Sprite.Create(UnitSpriteConstructor.currentAtlas.texture, rect3, pivot, 1f);
		UnitSpriteConstructor.currentAtlas.last_x += num + 1;
		return result;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0003EE08 File Offset: 0x0003D008
	public static void checkDirty()
	{
		if (!UnitSpriteConstructor.dirty)
		{
			return;
		}
		UnitSpriteConstructor.dirty = false;
		for (int i = 0; i < UnitSpriteConstructor.atlases.Count; i++)
		{
			UnitSpriteConstructor.atlases[i].checkDirty();
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0003EE48 File Offset: 0x0003D048
	public static Color32 checkSpecialColors(Color32 pColor)
	{
		if (Config.EVERYTHING_MAGIC_COLOR)
		{
			return Toolbox.EVERYTHING_MAGIC_COLOR32;
		}
		if (UnitSpriteConstructor._kingdomColor != null)
		{
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_0))
			{
				pColor = UnitSpriteConstructor._kingdomColor.k_color_0;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_1))
			{
				pColor = UnitSpriteConstructor._kingdomColor.k_color_1;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_2))
			{
				pColor = UnitSpriteConstructor._kingdomColor.k_color_2;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_3))
			{
				pColor = UnitSpriteConstructor._kingdomColor.k_color_3;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_4))
			{
				pColor = UnitSpriteConstructor._kingdomColor.k_color_4;
			}
		}
		if (UnitSpriteConstructor._drawSkinColor)
		{
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_green_0))
			{
				pColor = UnitSpriteConstructor._skin_color_0;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_green_1))
			{
				pColor = UnitSpriteConstructor._skin_color_1;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_green_2))
			{
				pColor = UnitSpriteConstructor._skin_color_2;
			}
			else if (Toolbox.areColorsEqual(pColor, Toolbox.color_green_3))
			{
				pColor = UnitSpriteConstructor._skin_color_3;
			}
		}
		return pColor;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0003EF50 File Offset: 0x0003D150
	public static void debug(DebugTool pTool, Actor pActor)
	{
		if (!UnitSpriteConstructor.initiated)
		{
			return;
		}
		UnitSpriteConstructor._debugActor = pActor;
		pTool.setText("sprites:", UnitSpriteConstructor._sprites.Count);
		pTool.setSeparator();
		pTool.setText("units:", UnitSpriteConstructor.units.debug());
		pTool.setText("boats:", UnitSpriteConstructor.boats.debug());
		pTool.setSeparator();
		pTool.setText("_debug_id:", UnitSpriteConstructor._debug_id);
		pTool.setText("_debug_headId:", UnitSpriteConstructor._debug_headId);
		pTool.setText("_debug_bodyID:", UnitSpriteConstructor._debug_bodyID);
		pTool.setText("_debug_skinID:", UnitSpriteConstructor._debug_skinID);
		pTool.setText("_debug_kingdomID:", UnitSpriteConstructor._debug_kingdomID);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0003F024 File Offset: 0x0003D224
	public static void export()
	{
		for (int i = 0; i < UnitSpriteConstructor.atlases.Count; i++)
		{
			byte[] array = ImageConversion.EncodeToPNG(UnitSpriteConstructor.atlases[i].texture);
			File.WriteAllBytes(Application.dataPath + "/wbdiag/sprite_constructor" + i.ToString() + ".png", array);
		}
	}

	// Token: 0x040006AD RID: 1709
	private const bool DEBUG = false;

	// Token: 0x040006AE RID: 1710
	private static Actor _debugActor;

	// Token: 0x040006AF RID: 1711
	public static long _debug_id;

	// Token: 0x040006B0 RID: 1712
	private static long _debug_kingdomID;

	// Token: 0x040006B1 RID: 1713
	private static long _debug_headId;

	// Token: 0x040006B2 RID: 1714
	private static long _debug_bodyID;

	// Token: 0x040006B3 RID: 1715
	private static long _debug_skinID;

	// Token: 0x040006B4 RID: 1716
	private static long _debug_skinSetID;

	// Token: 0x040006B5 RID: 1717
	public const int EDGE_PIXEL = 1;

	// Token: 0x040006B6 RID: 1718
	public const int TEXTURE_SIZE = 1024;

	// Token: 0x040006B7 RID: 1719
	public static Dictionary<long, Sprite> _sprites = new Dictionary<long, Sprite>();

	// Token: 0x040006B8 RID: 1720
	private static Color32 placeholder_color_skin = Toolbox.makeColor("#00FF00");

	// Token: 0x040006B9 RID: 1721
	private static Color32 placeholder_color_skin_dark = Toolbox.makeColor("#00AF00");

	// Token: 0x040006BA RID: 1722
	public static Color32 color_clear;

	// Token: 0x040006BB RID: 1723
	private static Color32 color_debug;

	// Token: 0x040006BC RID: 1724
	private static bool dirty = false;

	// Token: 0x040006BD RID: 1725
	private static KingdomColor _kingdomColor;

	// Token: 0x040006BE RID: 1726
	private static Color32 _skin_color;

	// Token: 0x040006BF RID: 1727
	private static Color32 _skin_color_0;

	// Token: 0x040006C0 RID: 1728
	private static Color32 _skin_color_1;

	// Token: 0x040006C1 RID: 1729
	private static Color32 _skin_color_2;

	// Token: 0x040006C2 RID: 1730
	private static Color32 _skin_color_3;

	// Token: 0x040006C3 RID: 1731
	private static bool _drawSkinColor;

	// Token: 0x040006C4 RID: 1732
	public static UnitSpriteConstructorAtlas units;

	// Token: 0x040006C5 RID: 1733
	public static UnitSpriteConstructorAtlas boats;

	// Token: 0x040006C6 RID: 1734
	public static UnitSpriteConstructorAtlas buildings;

	// Token: 0x040006C7 RID: 1735
	private static List<UnitSpriteConstructorAtlas> atlases = new List<UnitSpriteConstructorAtlas>();

	// Token: 0x040006C8 RID: 1736
	private static UnitSpriteConstructorAtlas currentAtlas = null;

	// Token: 0x040006C9 RID: 1737
	public static bool initiated = false;
}
