using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class MapLayer : BaseMapObject
{
	// Token: 0x060008D2 RID: 2258 RVA: 0x0005EA78 File Offset: 0x0005CC78
	internal override void create()
	{
		base.create();
		this.pixels_to_update = new TileDictionary();
		this.sprRnd = base.gameObject.GetComponent<SpriteRenderer>();
		if (this.rewriteSortingLayer)
		{
			this.sprRnd.sortingLayerName = this.world.GetComponent<SpriteRenderer>().sortingLayerName;
		}
		this.colors = new List<Color32>();
		this.createColors();
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0005EADC File Offset: 0x0005CCDC
	protected virtual void checkAutoDisable()
	{
		if (!this.autoDisable)
		{
			return;
		}
		if (this.autoDisableCheckPixels)
		{
			if (this.pixels_to_update.Count() > 0)
			{
				if (!this.sprRnd.enabled)
				{
					this.sprRnd.enabled = true;
					return;
				}
			}
			else if (this.sprRnd.enabled)
			{
				this.sprRnd.enabled = false;
				return;
			}
		}
		else if (this.tileDictionary.Count() > 0)
		{
			if (!this.sprRnd.enabled)
			{
				this.sprRnd.enabled = true;
				return;
			}
		}
		else if (this.sprRnd.enabled)
		{
			this.sprRnd.enabled = false;
		}
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0005EB80 File Offset: 0x0005CD80
	internal void createTextureNew()
	{
		if (!(this.texture == null) && MapBox.width == this.textureWidth && MapBox.height == this.texture.height)
		{
			return;
		}
		if (this.sprRnd.sprite != null && this.textureWidth != 0)
		{
			Texture2DStorage.addToStorage(this.sprRnd.sprite, this.textureWidth, this.textureHeight);
		}
		this.textureWidth = MapBox.width;
		this.textureHeight = MapBox.height;
		this.sprRnd.sprite = Texture2DStorage.getSprite(this.textureWidth, this.textureHeight);
		this.texture = this.sprRnd.sprite.texture;
		this.textureID = this.texture.GetInstanceID();
		int num = this.texture.height * this.texture.width;
		Color32 color = Color.clear;
		this.pixels = new Color32[num];
		for (int i = 0; i < num; i++)
		{
			this.pixels[i] = color;
		}
		this.updatePixels();
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0005EC99 File Offset: 0x0005CE99
	public bool contains(WorldTile pTile)
	{
		return this.pixels_to_update.contains(pTile);
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0005ECA8 File Offset: 0x0005CEA8
	internal virtual void clear()
	{
		if (this.pixels == null)
		{
			return;
		}
		this.pixels_to_update.clear();
		Color32 color = Color.clear;
		for (int i = 0; i < this.pixels.Length; i++)
		{
			this.pixels[i] = color;
		}
		this.updatePixels();
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0005ECFA File Offset: 0x0005CEFA
	public void setRendererEnabled(bool pBool)
	{
		this.sprRnd.enabled = pBool;
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0005ED08 File Offset: 0x0005CF08
	protected void createColors()
	{
		for (int i = 0; i < this.colors_amount; i++)
		{
			float num;
			if (i > 0)
			{
				num = 1f / (float)this.colors_amount * (float)i;
			}
			else
			{
				num = 0f;
			}
			this.colors.Add(new Color(this.colorValues.r, this.colorValues.g, this.colorValues.b, num * this.colorValues.a));
		}
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0005ED86 File Offset: 0x0005CF86
	public override void update(float pElapsed)
	{
		this.checkAutoDisable();
		if (this.sprRnd.enabled)
		{
			this.UpdateDirty(pElapsed);
		}
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0005EDA2 File Offset: 0x0005CFA2
	internal void updatePixels()
	{
		this.texture.SetPixels32(this.pixels);
		this.texture.Apply();
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0005EDC0 File Offset: 0x0005CFC0
	protected virtual void UpdateDirty(float pElapsed)
	{
	}

	// Token: 0x04000B4B RID: 2891
	public bool autoDisable;

	// Token: 0x04000B4C RID: 2892
	public bool autoDisableCheckPixels;

	// Token: 0x04000B4D RID: 2893
	public int textureID;

	// Token: 0x04000B4E RID: 2894
	protected float timer;

	// Token: 0x04000B4F RID: 2895
	protected Color colorValues;

	// Token: 0x04000B50 RID: 2896
	protected int colors_amount = 1;

	// Token: 0x04000B51 RID: 2897
	internal SpriteRenderer sprRnd;

	// Token: 0x04000B52 RID: 2898
	internal Texture2D texture;

	// Token: 0x04000B53 RID: 2899
	internal Color32[] pixels;

	// Token: 0x04000B54 RID: 2900
	internal TileDictionary pixels_to_update;

	// Token: 0x04000B55 RID: 2901
	protected List<Color32> colors;

	// Token: 0x04000B56 RID: 2902
	internal TileDictionary tileDictionary;

	// Token: 0x04000B57 RID: 2903
	private int textureWidth;

	// Token: 0x04000B58 RID: 2904
	private int textureHeight;

	// Token: 0x04000B59 RID: 2905
	public bool rewriteSortingLayer = true;
}
