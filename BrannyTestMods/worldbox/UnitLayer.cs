using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class UnitLayer : MapLayer
{
	// Token: 0x0600092C RID: 2348 RVA: 0x000612FC File Offset: 0x0005F4FC
	internal override void create()
	{
		this.dead = Toolbox.makeColor("#393939");
		this.prevTiles = new List<WorldTile>();
		base.create();
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00061324 File Offset: 0x0005F524
	internal override void clear()
	{
		this.prevTiles.Clear();
		base.clear();
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00061338 File Offset: 0x0005F538
	protected override void UpdateDirty(float pElapsed)
	{
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = this.interval;
		for (int i = 0; i < this.prevTiles.Count; i++)
		{
			WorldTile worldTile = this.prevTiles[i];
			this.pixels[worldTile.data.tile_id] = this._color_clear;
		}
		this.prevTiles.Clear();
		bool flag = PlayerConfig.optionBoolEnabled("marks_boats");
		bool flag2 = this.world.showCultureZones();
		if (this.world.isSelectedPowerAny() && !this.world.isPowerForceMapMode(MapMode.None))
		{
			flag2 = false;
		}
		List<Actor> simpleList = this.world.units.getSimpleList();
		for (int j = 0; j < simpleList.Count; j++)
		{
			Actor actor = simpleList[j];
			if (actor.stats.hideOnMinimap && actor.stats.color.a != 0 && !(actor.insideBuilding != null))
			{
				this.prevTiles.Add(actor.currentTile);
				if (actor.currentTile != null)
				{
					if (!actor.data.alive)
					{
						this.pixels[actor.currentTile.data.tile_id] = this.dead;
					}
					else if (flag2)
					{
						Culture culture = this.world.cultures.get(actor.data.culture);
						if (culture != null)
						{
							this.pixels[actor.currentTile.data.tile_id] = culture.color32_text;
						}
					}
					else if ((actor.stats.isBoat || actor.stats.unit) && actor.kingdom != null && actor.kingdom.isCiv())
					{
						if (!flag || !actor.stats.drawBoatMark)
						{
							this.pixels[actor.currentTile.data.tile_id] = actor.kingdom.kingdomColor.color32_unit;
						}
					}
					else
					{
						this.pixels[actor.currentTile.data.tile_id] = actor.stats.color;
					}
				}
			}
		}
		base.updatePixels();
	}

	// Token: 0x04000BC1 RID: 3009
	private List<WorldTile> prevTiles;

	// Token: 0x04000BC2 RID: 3010
	private float interval = 0.1f;

	// Token: 0x04000BC3 RID: 3011
	private Color32 dead = new Color(0f, 0f, 0f, 0.5f);

	// Token: 0x04000BC4 RID: 3012
	private Color32 _color_clear = Color.clear;
}
