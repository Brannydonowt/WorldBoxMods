using System;
using System.IO;
using UnityEngine;

// Token: 0x02000298 RID: 664
public static class PreviewHelper
{
	// Token: 0x06000EA7 RID: 3751 RVA: 0x00087E84 File Offset: 0x00086084
	public static Sprite loadWorkshopMapPreview()
	{
		byte[] array = File.ReadAllBytes(SaveManager.generatePngPreviewPath(SaveManager.currentWorkshopMapData.main_path));
		Texture2D texture2D = new Texture2D(64, 64);
		ImageConversion.LoadImage(texture2D, array);
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x00087EEC File Offset: 0x000860EC
	public static Sprite getCurrentWorldPreview()
	{
		Texture2D texture2D = Toolbox.ScaleTexture(MapBox.instance.worldLayer.texture, 512, 512);
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f));
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00087F4C File Offset: 0x0008614C
	public static Texture2D convertMapToTexture()
	{
		Texture2D texture = MapBox.instance.worldLayer.texture;
		Texture2D texture2D = new Texture2D(texture.width, texture.height);
		Color32[] pixels = texture.GetPixels32();
		texture2D.SetPixels32(pixels);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00087F90 File Offset: 0x00086190
	public static int getMaxAdSlots()
	{
		int num = 1;
		if (MapBox.instance.gameStats.data.gameLaunches > 10 && MapBox.instance.gameStats.data.gameTime > 36000.0)
		{
			num = 2;
		}
		if (MapBox.instance.gameStats.data.gameLaunches > 30 && MapBox.instance.gameStats.data.gameTime > 72000.0)
		{
			num = 3;
		}
		for (int i = num + 1; i <= 6; i++)
		{
			if (SaveManager.slotExists(i))
			{
				return 6;
			}
		}
		return num;
	}
}
