using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class PixelFlashEffects : MapLayer
{
	// Token: 0x0600061E RID: 1566 RVA: 0x000488B4 File Offset: 0x00046AB4
	internal override void create()
	{
		this.colors_amount = 30;
		this.colorValues = new Color(1f, 1f, 1f);
		this.colorWhite = new ColorArray(1f, 1f, 1f, 1f, 30f, 0.5f);
		this.colorPurple = new ColorArray(ConwayLife.colorEater, 30);
		this.colorBlue = new ColorArray(Toolbox.makeColor("#3BCC55"), 30);
		base.create();
		base.enabled = true;
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00048948 File Offset: 0x00046B48
	public void flashPixel(WorldTile pTile, int pVal = -1, ColorType pColorType = ColorType.White)
	{
		if (Config.worldLoading)
		{
			return;
		}
		if (pVal == -1)
		{
			pVal = this.colors_amount - 1;
		}
		if (!base.enabled)
		{
			return;
		}
		switch (pColorType)
		{
		case ColorType.White:
			pTile.colorArray = this.colorWhite;
			break;
		case ColorType.Purple:
			pTile.colorArray = this.colorPurple;
			break;
		case ColorType.Blue:
			pTile.colorArray = this.colorBlue;
			break;
		}
		if (pTile.flash_state <= 0)
		{
			this.pixels_to_update.add(pTile);
		}
		if (pTile.flash_state < pVal)
		{
			pTile.flash_state = pVal;
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x000489D5 File Offset: 0x00046BD5
	internal override void clear()
	{
		base.clear();
		this.pixels_to_update.clear();
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x000489E8 File Offset: 0x00046BE8
	protected override void UpdateDirty(float pElapsed)
	{
		if (this._timer > 0f)
		{
			this._timer -= this.world.deltaTime;
			return;
		}
		this._timer = 0.01f;
		if (this.pixels_to_update.Count() > 0)
		{
			this.toRemove.Clear();
			foreach (WorldTile worldTile in this.pixels_to_update.dict.Keys)
			{
				if (worldTile.flash_state < 0)
				{
					this.toRemove.Add(worldTile);
				}
				else
				{
					this.pixels[worldTile.data.tile_id] = worldTile.colorArray.colors[worldTile.flash_state];
					worldTile.flash_state--;
				}
			}
			for (int i = 0; i < this.toRemove.Count; i++)
			{
				WorldTile pTile = this.toRemove[i];
				this.pixels_to_update.remove(pTile, true);
			}
			base.updatePixels();
		}
	}

	// Token: 0x040007FE RID: 2046
	private List<WorldTile> toRemove = new List<WorldTile>();

	// Token: 0x040007FF RID: 2047
	private ColorArray colorWhite;

	// Token: 0x04000800 RID: 2048
	private ColorArray colorPurple;

	// Token: 0x04000801 RID: 2049
	private ColorArray colorBlue;

	// Token: 0x04000802 RID: 2050
	private float _timer;
}
