using System;

// Token: 0x020002F2 RID: 754
public class WaveController : BaseEffectController
{
	// Token: 0x06001114 RID: 4372 RVA: 0x000959EF File Offset: 0x00093BEF
	internal override void create()
	{
		base.create();
		this.timer_interval = 0.2f;
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x00095A04 File Offset: 0x00093C04
	public override void spawn()
	{
		TileZone random = Toolbox.getRandom<TileZone>(this.world.zoneCalculator.zones);
		if (random == null)
		{
			return;
		}
		if (random.tilesWithLiquid == 0)
		{
			return;
		}
		WorldTile random2 = Toolbox.getRandom<WorldTile>(random.tiles);
		if (random2.Type.layerType != TileLayerType.Ocean)
		{
			return;
		}
		base.GetObject().setTile(random2);
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x00095A5C File Offset: 0x00093C5C
	internal void checkTile(WorldTile tTile, int pRadius)
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			BaseEffect baseEffect = this.list[i];
			if (Toolbox.Dist(baseEffect.transform.position.x, baseEffect.transform.position.y, (float)tTile.pos.x, (float)tTile.pos.y) <= (float)pRadius)
			{
				base.killObject(baseEffect);
				return;
			}
		}
	}
}
