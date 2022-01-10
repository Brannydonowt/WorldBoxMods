using System;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class BurnedTilesLayer : MapLayer
{
	// Token: 0x060005B8 RID: 1464 RVA: 0x00045B8C File Offset: 0x00043D8C
	internal override void create()
	{
		this.colorValues = new Color(this.color.r, this.color.g, this.color.b, 0.5f);
		this.colors_amount = 15;
		this.autoDisable = false;
		base.create();
		base.enabled = true;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00045BE6 File Offset: 0x00043DE6
	public void setTileDirty(WorldTile pTile)
	{
		if (!this.pixels_to_update.contains(pTile))
		{
			this.pixels_to_update.add(pTile);
		}
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00045C04 File Offset: 0x00043E04
	protected override void UpdateDirty(float pElapsed)
	{
		if (this.pixels_to_update.Count() > 0)
		{
			foreach (WorldTile worldTile in this.pixels_to_update.dict.Keys)
			{
				if (worldTile.burned_stages > 0)
				{
					this.pixels[worldTile.data.tile_id] = this.colors[worldTile.burned_stages - 1];
				}
				else
				{
					this.pixels[worldTile.data.tile_id] = Toolbox.clear;
				}
			}
			this.pixels_to_update.clear();
			base.updatePixels();
		}
	}

	// Token: 0x0400079E RID: 1950
	public Color color;

	// Token: 0x0400079F RID: 1951
	private WorldBehaviour worldBehaviour;
}
