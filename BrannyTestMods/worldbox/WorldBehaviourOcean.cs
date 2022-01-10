using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EF RID: 239
public static class WorldBehaviourOcean
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x00040E6C File Offset: 0x0003F06C
	public static void clear()
	{
		WorldBehaviourOcean.tiles_to_update.Clear();
		WorldBehaviourOcean.tiles.Clear();
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00040E84 File Offset: 0x0003F084
	public static void updateOcean()
	{
		if (WorldBehaviourOcean.tiles.Count == 0)
		{
			return;
		}
		WorldBehaviourOcean.tiles_to_update.Clear();
		foreach (WorldTile worldTile in WorldBehaviourOcean.tiles)
		{
			if (worldTile.world_edge)
			{
				if ((float)Random.Range(0, 100) >= 30f)
				{
					WorldBehaviourOcean.tiles_to_update.Add(worldTile);
				}
			}
			else if (worldTile.IsOceanAround() && (float)Random.Range(0, 100) >= 30f)
			{
				WorldBehaviourOcean.tiles_to_update.Add(worldTile);
			}
		}
		for (int i = 0; i < WorldBehaviourOcean.tiles_to_update.Count; i++)
		{
			WorldTile worldTile2 = WorldBehaviourOcean.tiles_to_update[i];
			if (worldTile2.Type.canBeFilledWithOcean)
			{
				if (worldTile2.Type.explodableByOcean)
				{
					MapBox.instance.explosionLayer.explodeBomb(worldTile2, false);
				}
				else
				{
					MapAction.setOcean(worldTile2);
				}
			}
		}
	}

	// Token: 0x04000705 RID: 1797
	private static List<WorldTile> tiles_to_update = new List<WorldTile>();

	// Token: 0x04000706 RID: 1798
	public static HashSetWorldTile tiles = new HashSetWorldTile();
}
