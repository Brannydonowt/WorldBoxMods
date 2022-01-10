using System;
using System.Collections.Generic;
using EpPathFinding.cs;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class PathFindingVisualiser : MapLayer
{
	// Token: 0x0600095B RID: 2395 RVA: 0x00063518 File Offset: 0x00061718
	internal override void create()
	{
		this.colorValues = new Color(1f, 0.46f, 0.19f, 1f);
		this.colorValues = this.default_color;
		base.create();
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0006354B File Offset: 0x0006174B
	protected override void UpdateDirty(float pElapsed)
	{
		if (DebugConfig.isOn(DebugOption.LastPath))
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
				return;
			}
		}
		else if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00063588 File Offset: 0x00061788
	internal override void clear()
	{
		foreach (WorldTile worldTile in this.tiles)
		{
			this.pixels[worldTile.data.tile_id] = Color.clear;
		}
		this.tiles.Clear();
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00063600 File Offset: 0x00061800
	internal void showPath(StaticGrid pGrid, List<WorldTile> pTilePath)
	{
		if (!DebugConfig.isOn(DebugOption.LastPath))
		{
			return;
		}
		this.clear();
		if (pGrid != null)
		{
			foreach (WorldTile worldTile in this.world.tilesList)
			{
				this.tiles.Add(worldTile);
				Node nodeAt = pGrid.GetNodeAt(worldTile.pos.x, worldTile.pos.y);
				if (nodeAt.isClosed)
				{
					this.pixels[worldTile.data.tile_id] = Color.red;
				}
				else if (nodeAt.isOpened)
				{
					this.pixels[worldTile.data.tile_id] = Color.green;
				}
				else
				{
					this.pixels[worldTile.data.tile_id] = Color.clear;
				}
			}
		}
		foreach (WorldTile worldTile2 in pTilePath)
		{
			this.pixels[worldTile2.data.tile_id] = Color.blue;
			this.tiles.Add(worldTile2);
		}
		base.updatePixels();
	}

	// Token: 0x04000BF9 RID: 3065
	public Color default_color;

	// Token: 0x04000BFA RID: 3066
	private List<WorldTile> tiles = new List<WorldTile>();
}
