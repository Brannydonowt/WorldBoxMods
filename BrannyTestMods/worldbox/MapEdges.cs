using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class MapEdges
{
	// Token: 0x06001126 RID: 4390 RVA: 0x00096294 File Offset: 0x00094494
	internal static void AddEdge(WorldTile[,] tilesMap, string pWhat)
	{
		MapEdges.edgeSize = 64;
		if (MapEdges.textureLeft == null)
		{
			MapEdges.textureLeft = (Texture2D)Resources.Load("edges/edge100xLeft");
			MapEdges.textureRight = (Texture2D)Resources.Load("edges/edge100xRight");
			MapEdges.textureUp = (Texture2D)Resources.Load("edges/edge100xUp");
			MapEdges.textureDown = (Texture2D)Resources.Load("edges/edge100xDown");
			MapEdges.textureTempUp = (Texture2D)Resources.Load("edges/edgeTempUp");
			MapEdges.textureTempDown = (Texture2D)Resources.Load("edges/edgeTempDown");
		}
		int num = (int)((float)MapBox.width / (float)MapEdges.edgeSize) + 1;
		int num2 = (int)((float)MapBox.height / (float)MapEdges.edgeSize) + 1;
		if (pWhat == "temperature")
		{
			for (int i = 0; i < num; i++)
			{
				MapEdges.fill(i, 0, MapEdges.textureTempDown, tilesMap, pWhat);
			}
			for (int j = 0; j < num; j++)
			{
				MapEdges.fill(j, num2 - 2, MapEdges.textureTempUp, tilesMap, pWhat);
			}
			return;
		}
		for (int k = 0; k < num2; k++)
		{
			MapEdges.fill(0, k, MapEdges.textureLeft, tilesMap, pWhat);
		}
		for (int l = 0; l < num2; l++)
		{
			MapEdges.fill(num - 2, l, MapEdges.textureRight, tilesMap, pWhat);
		}
		for (int m = 0; m < num; m++)
		{
			MapEdges.fill(m, 0, MapEdges.textureDown, tilesMap, pWhat);
		}
		for (int n = 0; n < num; n++)
		{
			MapEdges.fill(n, num2 - 2, MapEdges.textureUp, tilesMap, pWhat);
		}
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x00096414 File Offset: 0x00094614
	internal static void fill(int pX, int pY, Texture2D pTexture, WorldTile[,] tilesMap, string pWhat)
	{
		for (int i = 0; i < pTexture.height; i++)
		{
			for (int j = 0; j < pTexture.width; j++)
			{
				int num = (int)(pTexture.GetPixel(j, i).a * 255f);
				int num2 = j + pX * MapEdges.edgeSize;
				int num3 = i + pY * MapEdges.edgeSize;
				if (num2 < MapBox.width && num3 < MapBox.height)
				{
					WorldTile worldTile = tilesMap[num2, num3];
					if (worldTile != null && pWhat != null && pWhat == "height")
					{
						worldTile.Height -= num;
					}
				}
			}
		}
	}

	// Token: 0x0400144A RID: 5194
	private static Texture2D textureLeft;

	// Token: 0x0400144B RID: 5195
	private static Texture2D textureRight;

	// Token: 0x0400144C RID: 5196
	private static Texture2D textureUp;

	// Token: 0x0400144D RID: 5197
	private static Texture2D textureDown;

	// Token: 0x0400144E RID: 5198
	private static Texture2D textureTempUp;

	// Token: 0x0400144F RID: 5199
	private static Texture2D textureTempDown;

	// Token: 0x04001450 RID: 5200
	private static int edgeSize;
}
