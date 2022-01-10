using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class GeneratorTool : ScriptableObject
{
	// Token: 0x06001118 RID: 4376 RVA: 0x00095AE3 File Offset: 0x00093CE3
	internal static void Setup(WorldTile[,] pTilesMap)
	{
		GeneratorTool.tilesMap = pTilesMap;
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x00095AEB File Offset: 0x00093CEB
	public static void Init()
	{
		GeneratorTool.LoadGenShapeTextures();
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x00095AF4 File Offset: 0x00093CF4
	internal static void applyTemplate(string pTexture, float pMod = 1f)
	{
		Texture2D texture2D = (Texture2D)Resources.Load("mapTemplates/earth");
		float num = 255f * pMod;
		for (int i = 0; i < texture2D.width; i++)
		{
			for (int j = 0; j < texture2D.height; j++)
			{
				WorldTile tile = MapBox.instance.GetTile(i, j);
				if (tile != null)
				{
					int num2 = (int)((1f - texture2D.GetPixel(i, j).g) * num);
					tile.Height += num2;
				}
			}
		}
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x00095B78 File Offset: 0x00093D78
	internal static void ApplyRandomShape(string pWhat = "height", float tDistMax = 2f, float pMod = 0.7f, bool pSubtract = false)
	{
		Texture2D texture2D;
		int num;
		int num2;
		if (GeneratorTool.randomTextures.Count < 40)
		{
			texture2D = Object.Instantiate<Texture2D>(Toolbox.getRandom<Texture2D>(GeneratorTool.textures));
			GeneratorTool.randomTextures.Add(texture2D);
			num = (int)((float)texture2D.width * Random.Range(0.3f, 2f));
			num2 = (int)((float)texture2D.height * Random.Range(0.3f, 2f));
			texture2D = TextureRotator.Rotate(texture2D, Random.Range(0, 360));
			TextureScale.Bilinear(texture2D, num, num2);
		}
		else
		{
			texture2D = Toolbox.getRandom<Texture2D>(GeneratorTool.randomTextures);
		}
		num = texture2D.width;
		num2 = texture2D.height;
		int num3 = MapBox.width / 2 - num / 2 - (int)Random.Range((float)(-(float)num) * tDistMax, (float)num * tDistMax);
		int num4 = MapBox.height / 2 - num2 / 2 - (int)Random.Range((float)(-(float)num2) * tDistMax, (float)num2 * tDistMax);
		if (num3 < 0)
		{
			num3 = 0;
		}
		if (num4 < 0)
		{
			num4 = 0;
		}
		if (num3 + num > MapBox.width)
		{
			num3 = MapBox.width - num;
		}
		if (num4 + num2 > MapBox.height)
		{
			num4 = MapBox.height - num2;
		}
		float num5 = 255f * pMod;
		for (int i = 0; i < texture2D.width; i++)
		{
			for (int j = 0; j < texture2D.height; j++)
			{
				WorldTile tile = MapBox.instance.GetTile(num3 + i, num4 + j);
				if (tile != null)
				{
					int num6 = (int)(texture2D.GetPixel(i, j).a * num5);
					if (pSubtract)
					{
						num6 = -num6;
					}
					if (pWhat != null && pWhat == "height")
					{
						tile.Height += num6;
					}
				}
			}
		}
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x00095D14 File Offset: 0x00093F14
	private static void LoadGenShapeTextures()
	{
		Object[] array = Resources.LoadAll("gen_shapes", typeof(Texture2D));
		GeneratorTool.textures = new List<Texture2D>();
		foreach (Object @object in array)
		{
			GeneratorTool.textures.Add(@object as Texture2D);
		}
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x00095D64 File Offset: 0x00093F64
	public static void ApplyWaterLevel(WorldTile[,] tilesMap, int width, int height, int pVal)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				tilesMap[i, j].Height -= pVal;
			}
		}
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00095DA0 File Offset: 0x00093FA0
	public static void ApplyPerlinNoice(WorldTile[,] tilesMap, int width, int height, float pPosX, float pPosY, float pAlphaMod, float pScaleMod, bool pSubtract = false, GeneratorTarget pTarget = GeneratorTarget.Height)
	{
		float num = 255f * pAlphaMod;
		float num2 = 1f;
		float num3 = 1f;
		if (width > height)
		{
			num2 = (float)(width / height);
		}
		else
		{
			num3 = (float)(height / width);
		}
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				WorldTile worldTile = tilesMap[i, j];
				float x = pPosX + (float)i / (float)width * pScaleMod * num2;
				float y = pPosY + (float)j / (float)height * pScaleMod * num3;
				int num4 = (int)(Mathf.PerlinNoise(x, y) * num);
				if (pSubtract)
				{
					num4 = -num4;
				}
				if (pTarget == GeneratorTarget.Height)
				{
					worldTile.Height += num4;
				}
			}
		}
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x00095E40 File Offset: 0x00094040
	public static void UpdateTileTypes(bool pGeneratorStage = false)
	{
		foreach (WorldTile worldTile in MapBox.instance.tilesList)
		{
			TileType typeByDepth = AssetManager.tiles.GetTypeByDepth(worldTile);
			worldTile.setTileType(typeByDepth);
		}
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x00095EA4 File Offset: 0x000940A4
	public static void GenerateTileNeighbours(WorldTile[,] tilesList)
	{
		int upperBound = tilesList.GetUpperBound(0);
		int upperBound2 = tilesList.GetUpperBound(1);
		for (int i = tilesList.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = tilesList.GetLowerBound(1); j <= upperBound2; j++)
			{
				WorldTile worldTile = tilesList[i, j];
				WorldTile tile = GeneratorTool.GetTile(worldTile.x - 1, worldTile.y);
				worldTile.AddNeighbour(tile, TileDirection.Left, false);
				tile = GeneratorTool.GetTile(worldTile.x + 1, worldTile.y);
				worldTile.AddNeighbour(tile, TileDirection.Right, false);
				tile = GeneratorTool.GetTile(worldTile.x, worldTile.y - 1);
				worldTile.AddNeighbour(tile, TileDirection.Down, false);
				tile = GeneratorTool.GetTile(worldTile.x, worldTile.y + 1);
				worldTile.AddNeighbour(tile, TileDirection.Up, false);
				tile = GeneratorTool.GetTile(worldTile.x - 1, worldTile.y - 1);
				worldTile.AddNeighbour(tile, TileDirection.Null, true);
				tile = GeneratorTool.GetTile(worldTile.x - 1, worldTile.y + 1);
				worldTile.AddNeighbour(tile, TileDirection.Null, true);
				tile = GeneratorTool.GetTile(worldTile.x + 1, worldTile.y - 1);
				worldTile.AddNeighbour(tile, TileDirection.Null, true);
				tile = GeneratorTool.GetTile(worldTile.x + 1, worldTile.y + 1);
				worldTile.AddNeighbour(tile, TileDirection.Null, true);
			}
		}
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x00096010 File Offset: 0x00094210
	public static void ApplyRingEffect()
	{
		for (int i = 0; i < MapBox.width; i++)
		{
			for (int j = 0; j < MapBox.height; j++)
			{
				foreach (TileType tileType in AssetManager.tiles.list)
				{
					if (tileType.additional_height != null)
					{
						bool flag = false;
						for (int k = 0; k < tileType.additional_height.Length; k++)
						{
							WorldTile worldTile = GeneratorTool.tilesMap[i, j];
							if (worldTile.Height == tileType.heightMin - tileType.additional_height[k])
							{
								worldTile.Height = tileType.heightMin;
								flag = true;
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x000960F0 File Offset: 0x000942F0
	private static WorldTile GetTile(int x, int y)
	{
		if (x < 0 || x >= MapBox.width)
		{
			return null;
		}
		if (y < 0 || y >= MapBox.height)
		{
			return null;
		}
		return GeneratorTool.tilesMap[x, y];
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0009611C File Offset: 0x0009431C
	internal static void modifyIsland(TileIsland pIsland)
	{
		int num = Toolbox.randomInt(0, 3);
		if (num == 0 && pIsland.regions.Count < 20)
		{
			using (IEnumerator<MapRegion> enumerator = pIsland.regions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MapRegion mapRegion = enumerator.Current;
					foreach (WorldTile worldTile in mapRegion.tiles)
					{
						if (worldTile.Type.layerType != TileLayerType.Block)
						{
							worldTile.setTopTileType(null);
							worldTile.setTileType("sand");
						}
					}
				}
				return;
			}
		}
		if (num == 1)
		{
			foreach (MapRegion mapRegion2 in pIsland.regions)
			{
				foreach (WorldTile worldTile2 in mapRegion2.tiles)
				{
					if (worldTile2.Type.layerType == TileLayerType.Block && worldTile2.main_type.canGrowBiomeGrass)
					{
						DropsLibrary.useSeedOn(worldTile2, TopTileLibrary.grass_low, TopTileLibrary.grass_high);
					}
				}
			}
		}
	}

	// Token: 0x04001447 RID: 5191
	private static WorldTile[,] tilesMap;

	// Token: 0x04001448 RID: 5192
	private static List<Texture2D> textures;

	// Token: 0x04001449 RID: 5193
	private static List<Texture2D> randomTextures = new List<Texture2D>();
}
