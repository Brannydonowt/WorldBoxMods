using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public class Brush
{
	// Token: 0x0600099A RID: 2458 RVA: 0x00064C86 File Offset: 0x00062E86
	public static string getRandom()
	{
		return Enumerable.ToList<string>(Brush.dictionary.Keys).GetRandom<string>();
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00064C9C File Offset: 0x00062E9C
	public static void init()
	{
		Brush.dictionary = new Dictionary<string, BrushData>();
		Brush.addCircle(1, true);
		Brush.addCircle(2, true);
		Brush.addCircle(4, true);
		Brush.addCircle(5, true);
		Brush.addCircle(10, false);
		Brush.addCircle(11, false);
		Brush.addCircle(15, false);
		Brush.addDot(true);
		Brush.addSquare(1, true);
		Brush.addSquare(5, true);
		Brush.addSquare(10, false);
		Brush.addSquare(15, false);
		Brush.addSpecial("diamond_1", -1, true);
		Brush.addSpecial("diamond_2", -1, true);
		Brush.addSpecial("diamond_4", -1, true);
		Brush.addSpecial("diamond_5", -1, false);
		Brush.addSpecial("diamond_7", -1, false);
		Brush.addSpecial("special_1", -1, false);
		Brush.addSpecial("special_2", -1, false);
		Brush.addSpecial("special_3", -1, false);
		Brush.addSpecial("special_4", -1, true);
		Brush.addSpecial("special_5", -1, true);
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00064D8C File Offset: 0x00062F8C
	public static BrushData get(int pSize, string pID = "circ_")
	{
		string text = pID + pSize.ToString();
		BrushData brushData;
		Brush.dictionary.TryGetValue(text, ref brushData);
		if (brushData == null)
		{
			bool pContinuous = false;
			if (pSize <= 5)
			{
				pContinuous = true;
			}
			if (pID == "circ_")
			{
				return Brush.addCircle(pSize, pContinuous);
			}
		}
		return brushData;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x00064DD6 File Offset: 0x00062FD6
	public static BrushData get(string pID)
	{
		return Brush.dictionary[pID];
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x00064DE4 File Offset: 0x00062FE4
	private static void addDot(bool pContinuous = false)
	{
		BrushData brushData = new BrushData();
		brushData.continuous = pContinuous;
		brushData.size = 0;
		brushData.pos.Add(new BrushPixelData(0, 0, 0f, 0f));
		Brush.add("sqr_" + 0.ToString(), brushData);
		Brush.add("circ_" + 0.ToString(), brushData);
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00064E54 File Offset: 0x00063054
	private static void addSquare(int pSize, bool pContinuous = false)
	{
		BrushData brushData = new BrushData();
		brushData.continuous = pContinuous;
		brushData.size = pSize;
		Vector2Int vector2Int = new Vector2Int(pSize / 2, pSize / 2);
		for (int i = -pSize; i <= pSize; i++)
		{
			for (int j = -pSize; j <= pSize; j++)
			{
				int x = vector2Int.x;
				int y = vector2Int.y;
				brushData.pos.Add(new BrushPixelData(i, j, (float)pSize, (float)pSize));
			}
		}
		Brush.add("sqr_" + pSize.ToString(), brushData);
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00064EDC File Offset: 0x000630DC
	private static void add(string pID, BrushData pData)
	{
		pData.pos.Shuffle<BrushPixelData>();
		int num = 0;
		for (int i = 0; i < pData.pos.Count; i++)
		{
			BrushPixelData brushPixelData = pData.pos[i];
			if (brushPixelData.x == 0 && brushPixelData.y == 0)
			{
				num = pData.pos.IndexOf(brushPixelData);
				break;
			}
		}
		BrushPixelData brushPixelData2 = pData.pos[0];
		BrushPixelData brushPixelData3 = pData.pos[num];
		pData.pos[0] = brushPixelData3;
		pData.pos[num] = brushPixelData2;
		Brush.dictionary.Add(pID, pData);
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00064F7C File Offset: 0x0006317C
	private static BrushData addCircle(int pSize, bool pContinuous = false)
	{
		BrushData brushData = new BrushData();
		brushData.continuous = pContinuous;
		brushData.size = pSize;
		Vector2Int vector2Int = new Vector2Int(pSize / 2, pSize / 2);
		for (int i = -pSize; i <= pSize; i++)
		{
			for (int j = -pSize; j <= pSize; j++)
			{
				int num = vector2Int.x + i;
				int num2 = vector2Int.y + j;
				float num3 = Toolbox.Dist((float)vector2Int.x, (float)vector2Int.y, (float)num, (float)num2);
				if (num3 <= (float)pSize)
				{
					brushData.pos.Add(new BrushPixelData(i, j, (float)pSize, num3));
				}
			}
		}
		Brush.add("circ_" + pSize.ToString(), brushData);
		return brushData;
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x00065038 File Offset: 0x00063238
	private static void addSpecial(string pID, int pSize = -1, bool pContinuous = false)
	{
		string str = "brush_" + pID;
		BrushData brushData = new BrushData();
		brushData.continuous = pContinuous;
		Texture2D texture2D = Resources.Load<Texture2D>("brushes/" + str);
		int width = texture2D.width;
		int height = texture2D.height;
		if (pSize == -1)
		{
			pSize = width / 2;
		}
		brushData.size = pSize;
		Vector2Int vector2Int = new Vector2Int(width / 2, height / 2);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (!(texture2D.GetPixel(i, j) != Color.white))
				{
					int pX = vector2Int.x - i;
					int pY = vector2Int.y - j;
					brushData.pos.Add(new BrushPixelData(pX, pY, (float)pSize, (float)pSize));
				}
			}
		}
		Brush.add(pID, brushData);
	}

	// Token: 0x04000C49 RID: 3145
	private static Dictionary<string, BrushData> dictionary;
}
