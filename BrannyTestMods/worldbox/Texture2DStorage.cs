using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class Texture2DStorage
{
	// Token: 0x06000A9F RID: 2719 RVA: 0x0006AC08 File Offset: 0x00068E08
	internal static Sprite getSprite(int pW, int pH)
	{
		string text = pW.ToString() + "_" + pH.ToString();
		if (Texture2DStorage.pools.ContainsKey(text))
		{
			SpritePool spritePool = Texture2DStorage.pools[text];
			if (spritePool.list.Count > 0)
			{
				Sprite result = spritePool.list[spritePool.list.Count - 1];
				spritePool.list.RemoveAt(spritePool.list.Count - 1);
				return result;
			}
		}
		if (!Texture2DStorage.prefabs.ContainsKey(text))
		{
			Texture2D texture2D = new Texture2D(pW, pH, TextureFormat.RGBA32, false)
			{
				filterMode = FilterMode.Point
			};
			Texture2DStorage.prefabs.Add(text, texture2D);
		}
		return Sprite.Create(Object.Instantiate<Texture2D>(Texture2DStorage.prefabs[text]), new Rect(0f, 0f, (float)pW, (float)pH), new Vector2(0f, 0f), 1f);
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0006ACEC File Offset: 0x00068EEC
	internal static void addToStorage(Sprite pSprite, int pW, int pH)
	{
		string text = pW.ToString() + "_" + pH.ToString();
		SpritePool spritePool;
		if (Texture2DStorage.pools.ContainsKey(text))
		{
			spritePool = Texture2DStorage.pools[text];
		}
		else
		{
			spritePool = new SpritePool();
			Texture2DStorage.pools.Add(text, spritePool);
		}
		spritePool.list.Add(pSprite);
	}

	// Token: 0x04000D2B RID: 3371
	private static Dictionary<string, SpritePool> pools = new Dictionary<string, SpritePool>();

	// Token: 0x04000D2C RID: 3372
	private static Dictionary<string, Texture2D> prefabs = new Dictionary<string, Texture2D>();
}
