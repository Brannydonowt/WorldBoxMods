using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class UnitSpriteConstructorAtlas
{
	// Token: 0x0600048D RID: 1165 RVA: 0x0003E2DB File Offset: 0x0003C4DB
	public UnitSpriteConstructorAtlas(UnitTextureAtlasID pID)
	{
		this.id = pID;
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0003E2F8 File Offset: 0x0003C4F8
	public void newTexture()
	{
		this.texture = new Texture2D(1024, 1024);
		this.textures.Add(this.texture);
		this.texture.filterMode = FilterMode.Point;
		this.pixels = this.texture.GetPixels32();
		for (int i = 0; i < this.pixels.Length; i++)
		{
			this.pixels[i] = UnitSpriteConstructor.color_clear;
		}
		this.dirty = true;
		this.last_x = 0;
		this.last_y = 0;
		this.biggest_height = 0;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0003E388 File Offset: 0x0003C588
	public void checkDirty()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		this.texture.SetPixels32(this.pixels);
		this.texture.Apply();
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0003E3B8 File Offset: 0x0003C5B8
	public string debug()
	{
		return this.textures.Count.ToString() + " | " + this.last_y.ToString();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0003E3F0 File Offset: 0x0003C5F0
	public void checkBounds(int tWidth, int tHeight)
	{
		if (tHeight > this.biggest_height)
		{
			this.biggest_height = tHeight;
		}
		if (this.last_x + tWidth > 1024)
		{
			this.last_x = 0;
			this.last_y += this.biggest_height + 1;
			if (this.last_y + this.biggest_height > 1024 || this.last_y > 1024)
			{
				this.newTexture();
				return;
			}
			this.biggest_height = 0;
		}
	}

	// Token: 0x040006A5 RID: 1701
	public UnitTextureAtlasID id;

	// Token: 0x040006A6 RID: 1702
	public Texture2D texture;

	// Token: 0x040006A7 RID: 1703
	public Color32[] pixels;

	// Token: 0x040006A8 RID: 1704
	public List<Texture2D> textures = new List<Texture2D>();

	// Token: 0x040006A9 RID: 1705
	public int last_x;

	// Token: 0x040006AA RID: 1706
	public int last_y;

	// Token: 0x040006AB RID: 1707
	public int biggest_height;

	// Token: 0x040006AC RID: 1708
	public bool dirty;
}
