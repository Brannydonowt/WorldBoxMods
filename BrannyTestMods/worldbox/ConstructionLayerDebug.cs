using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class ConstructionLayerDebug : MapLayer
{
	// Token: 0x06000811 RID: 2065 RVA: 0x00058E6B File Offset: 0x0005706B
	private void Update()
	{
		this.update(Time.deltaTime);
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00058E78 File Offset: 0x00057078
	internal override void create()
	{
		base.create();
		this.world.mapLayers.Add(this);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00058E91 File Offset: 0x00057091
	internal new void clear()
	{
		base.createTextureNew();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00058E9C File Offset: 0x0005709C
	protected override void UpdateDirty(float pElapsed)
	{
		if (!ConstructionLayerDebug.dirty)
		{
			return;
		}
		ConstructionLayerDebug.dirty = false;
		if (!this.created)
		{
			this.create();
		}
		if (this.pixels == null)
		{
			base.createTextureNew();
		}
		foreach (WorldTile worldTile in this.world.tilesList)
		{
			if (worldTile.building != null)
			{
				this.pixels[worldTile.data.tile_id] = Color.red;
			}
			else
			{
				this.pixels[worldTile.data.tile_id] = Toolbox.clear;
			}
		}
		base.updatePixels();
	}

	// Token: 0x04000A93 RID: 2707
	public static bool dirty;
}
