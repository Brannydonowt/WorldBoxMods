using System;
using System.Collections.Generic;

// Token: 0x020000EA RID: 234
public static class WorldBehaviourActionErosion
{
	// Token: 0x060004EB RID: 1259 RVA: 0x00040600 File Offset: 0x0003E800
	public static void updateErosion()
	{
		if (!MapBox.instance.worldLaws.world_law_erosion.boolVal)
		{
			return;
		}
		WorldBehaviourActionErosion.list.Clear();
		MapBox.instance.islandsCalculator.islands.ShuffleOne<TileIsland>();
		for (int i = 0; i < MapBox.instance.islandsCalculator.islands.Count; i++)
		{
			TileIsland tileIsland = MapBox.instance.islandsCalculator.islands[i];
			if (tileIsland.type == TileLayerType.Ground)
			{
				for (int j = 0; j < 5; j++)
				{
					WorldTile randomTile = tileIsland.getRandomTile();
					if (randomTile != null && (randomTile.Type.canGrowBiomeGrass || randomTile.Type.grass) && randomTile.IsOceanAround() && !WorldBehaviourActionErosion.list.Contains(randomTile))
					{
						WorldBehaviourActionErosion.list.Add(randomTile);
						if (WorldBehaviourActionErosion.list.Count >= 5)
						{
							break;
						}
					}
				}
				if (WorldBehaviourActionErosion.list.Count >= 5)
				{
					break;
				}
			}
		}
		if (WorldBehaviourActionErosion.list.Count == 0)
		{
			return;
		}
		WorldBehaviourActionErosion.list.ShuffleOne<WorldTile>();
		for (int k = 0; k < WorldBehaviourActionErosion.list.Count; k++)
		{
			MapAction.terraformMain(WorldBehaviourActionErosion.list[k], TileLibrary.sand, AssetManager.terraform.get("flash"));
		}
	}

	// Token: 0x040006FB RID: 1787
	private const int MAX_TILES_IN_LIST = 5;

	// Token: 0x040006FC RID: 1788
	private static List<WorldTile> list = new List<WorldTile>();
}
