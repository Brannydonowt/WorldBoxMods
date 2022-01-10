using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class BannerGenerator
{
	// Token: 0x06000675 RID: 1653 RVA: 0x0004B0C3 File Offset: 0x000492C3
	public static void init()
	{
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0004B0C8 File Offset: 0x000492C8
	private static void loadTexturesFromResources(string pID)
	{
		BannerContainer bannerContainer = new BannerContainer();
		bannerContainer.id = pID;
		Sprite[] array = Resources.LoadAll<Sprite>("banners/" + pID + "/background");
		Sprite[] array2 = Resources.LoadAll<Sprite>("banners/" + pID + "/icon");
		foreach (Sprite sprite in array)
		{
			bannerContainer.backrounds.Add(sprite);
		}
		foreach (Sprite sprite2 in array2)
		{
			bannerContainer.icons.Add(sprite2);
		}
		BannerGenerator.dict.Add(bannerContainer.id, bannerContainer);
		BannerGenerator.list.Add(bannerContainer);
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0004B16C File Offset: 0x0004936C
	public static void setSeed(int pSeed)
	{
		BannerGenerator.seed = pSeed;
		Random.InitState(BannerGenerator.seed);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0004B17E File Offset: 0x0004937E
	public static void loadBannersFromResources()
	{
		BannerGenerator.dict.Clear();
		BannerGenerator.list.Clear();
		BannerGenerator.loadTexturesFromResources("dwarf");
		BannerGenerator.loadTexturesFromResources("elf");
		BannerGenerator.loadTexturesFromResources("human");
		BannerGenerator.loadTexturesFromResources("orc");
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0004B1BC File Offset: 0x000493BC
	public static void loadBanners(string pPath)
	{
		string[] directories = Directory.GetDirectories(pPath);
		for (int i = 0; i < directories.Length; i++)
		{
			BannerGenerator.loadBannerDir(directories[i]);
		}
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0004B1E8 File Offset: 0x000493E8
	public static void loadBannerDir(string pPath)
	{
		Debug.Log("LOAD BANNER " + pPath);
		string[] directories = Directory.GetDirectories(pPath);
		string[] array = pPath.Replace('\\', ',').Replace('/', ',').Split(new char[]
		{
			','
		});
		string text = array[array.Length - 1];
		BannerContainer bannerContainer;
		if (BannerGenerator.dict.ContainsKey(text))
		{
			bannerContainer = BannerGenerator.dict[text];
		}
		else
		{
			bannerContainer = new BannerContainer();
			bannerContainer.id = text;
			BannerGenerator.dict.Add(bannerContainer.id, bannerContainer);
		}
		foreach (string text2 in directories)
		{
			if (text2.Contains("background"))
			{
				BannerGenerator.loadBackgrounds(text2, bannerContainer);
			}
			else if (text2.Contains("icon"))
			{
				BannerGenerator.loadIcons(text2, bannerContainer);
			}
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0004B2B8 File Offset: 0x000494B8
	private static void loadBackgrounds(string pPath, BannerContainer pContainer)
	{
		Debug.Log("LOAD BANNER loadBackgrounds " + pPath);
		foreach (string text in Directory.GetFiles(pPath))
		{
			if (!text.Contains(".meta"))
			{
				Sprite sprite = BannerGenerator.createSprite(text);
				pContainer.backrounds.Add(sprite);
			}
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0004B310 File Offset: 0x00049510
	private static Sprite createSprite(string pPath)
	{
		byte[] array = File.ReadAllBytes(pPath);
		Texture2D texture2D = new Texture2D(54, 54);
		texture2D.filterMode = FilterMode.Point;
		ImageConversion.LoadImage(texture2D, array);
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 1f);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0004B374 File Offset: 0x00049574
	private static void loadIcons(string pPath, BannerContainer pContainer)
	{
		foreach (string text in Directory.GetFiles(pPath))
		{
			if (!text.Contains(".meta"))
			{
				Sprite sprite = BannerGenerator.createSprite(text);
				pContainer.icons.Add(sprite);
			}
		}
	}

	// Token: 0x0400085C RID: 2140
	private static int seed;

	// Token: 0x0400085D RID: 2141
	public static Dictionary<string, BannerContainer> dict = new Dictionary<string, BannerContainer>();

	// Token: 0x0400085E RID: 2142
	public static List<BannerContainer> list = new List<BannerContainer>();
}
