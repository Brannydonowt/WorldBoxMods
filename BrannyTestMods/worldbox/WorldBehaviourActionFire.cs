using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
public static class WorldBehaviourActionFire
{
	// Token: 0x060004ED RID: 1261 RVA: 0x00040750 File Offset: 0x0003E950
	public static void clear()
	{
		WorldBehaviourActionFire.tiles.Clear();
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0004075C File Offset: 0x0003E95C
	public static void updateFire()
	{
		if (WorldBehaviourActionFire.tiles.Count == 0)
		{
			return;
		}
		foreach (WorldTile worldTile in WorldBehaviourActionFire.tiles)
		{
			if (worldTile.data.fire_stage > 9)
			{
				for (int i = 0; i < worldTile.neighbours.Count; i++)
				{
					WorldTile worldTile2 = worldTile.neighbours[i];
					if (Toolbox.randomChance(worldTile2.Type.fireChance) && worldTile2.setFire(false))
					{
						MapBox.instance.flashEffects.flashPixel(worldTile2, 10, ColorType.White);
					}
				}
			}
			if (worldTile.building == null && worldTile.data.fire_stage % 15 == 0 && Random.value > 0.9f)
			{
				MapBox.instance.particlesFire.spawn(worldTile.posV3);
			}
			worldTile.data.fire_stage++;
			MapBox.instance.setTileDirty(worldTile, false);
			MapBox.instance.fireLayer.setTileDirty(worldTile);
			if (worldTile.data.fire_stage >= 20)
			{
				worldTile.stopFire(true);
			}
		}
	}

	// Token: 0x040006FD RID: 1789
	public static HashSetWorldTile tiles = new HashSetWorldTile();
}
