using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class FireLayer : MapLayer
{
	// Token: 0x060005E8 RID: 1512 RVA: 0x000474D8 File Offset: 0x000456D8
	internal override void create()
	{
		this.colorValues = new Color(1f, 0.46f, 0.19f, 1f);
		this.colorValues = this.fire_color;
		this.colors_amount = 30;
		base.create();
		this.colors.Reverse();
		base.enabled = true;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00047530 File Offset: 0x00045730
	public void setTileDirty(WorldTile pTile)
	{
		if (!this.pixels_to_update.contains(pTile))
		{
			this.pixels_to_update.add(pTile);
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0004754C File Offset: 0x0004574C
	protected override void checkAutoDisable()
	{
		if (WorldBehaviourActionFire.tiles.Count > 0)
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

	// Token: 0x060005EB RID: 1515 RVA: 0x0004759C File Offset: 0x0004579C
	protected override void UpdateDirty(float pElapsed)
	{
		if (this.pixels_to_update.Count() > 0)
		{
			foreach (WorldTile worldTile in this.pixels_to_update.dict.Keys)
			{
				int num = worldTile.data.fire_stage;
				if (!worldTile.data.fire)
				{
					num = this.colors.Count - 1;
				}
				this.pixels[worldTile.data.tile_id] = this.colors[num];
			}
			this.pixels_to_update.clear();
			base.updatePixels();
		}
	}

	// Token: 0x040007CE RID: 1998
	public Color fire_color;
}
