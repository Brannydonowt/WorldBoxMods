using System;
using System.Collections.Generic;

// Token: 0x020000E6 RID: 230
public static class WorldBehaviourActionBurnedTiles
{
	// Token: 0x060004DC RID: 1244 RVA: 0x00040014 File Offset: 0x0003E214
	public static void updateBurnedTiles()
	{
		WorldBehaviourActionBurnedTiles.tiles_to_update.Clear();
		WorldBehaviourActionBurnedTiles._tempIslands.Clear();
		for (int i = 0; i < MapBox.instance.islandsCalculator.islands.Count; i++)
		{
			TileIsland tileIsland = MapBox.instance.islandsCalculator.islands[i];
			if (tileIsland.type != TileLayerType.Ocean)
			{
				WorldBehaviourActionBurnedTiles._tempIslands.Add(tileIsland);
			}
		}
		for (int j = 0; j < WorldBehaviourActionBurnedTiles._tempIslands.Count; j++)
		{
			TileIsland tileIsland2 = WorldBehaviourActionBurnedTiles._tempIslands[j];
			for (int k = 0; k < 3; k++)
			{
				MapRegion random = tileIsland2.regions.GetRandom();
				if (MapBox.instance.gameStats.data.gameTime - random.lastBurnedUpdate >= 0.5)
				{
					random.lastBurnedUpdate = MapBox.instance.gameStats.data.gameTime;
					random.tiles.ShuffleOne<WorldTile>();
					for (int l = 0; l < random.tiles.Count; l++)
					{
						WorldTile worldTile = random.tiles[l];
						if (!worldTile.data.fire && !Toolbox.randomBool())
						{
							if (worldTile.burned_stages > 1)
							{
								worldTile.burned_stages--;
								MapBox.instance.burnedLayer.setTileDirty(worldTile);
								if (worldTile.burned_stages == 0)
								{
									WorldBehaviourActionBurnedTiles.tiles_to_update.Add(worldTile);
								}
							}
							else
							{
								worldTile.burned_stages = 0;
								WorldBehaviourActionBurnedTiles.tiles_to_update.Add(worldTile);
								MapBox.instance.burnedLayer.setTileDirty(worldTile);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x040006F3 RID: 1779
	public static List<WorldTile> tiles_to_update = new List<WorldTile>();

	// Token: 0x040006F4 RID: 1780
	private static List<TileIsland> _tempIslands = new List<TileIsland>();
}
