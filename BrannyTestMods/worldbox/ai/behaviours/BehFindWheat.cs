using System;

namespace ai.behaviours
{
	// Token: 0x0200036E RID: 878
	public class BehFindWheat : BehCity
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x000A23DC File Offset: 0x000A05DC
		public override BehResult execute(Actor pActor)
		{
			WorldTile worldTile = null;
			int num = 0;
			for (int i = 0; i < pActor.city.zones.Count; i++)
			{
				pActor.city.zones.ShuffleOne(i);
				HashSetWorldTile tilesOfType = pActor.city.zones[i].getTilesOfType(TopTileLibrary.field);
				if (tilesOfType != null && tilesOfType.Count != 0)
				{
					float num2 = 0f;
					worldTile = null;
					foreach (WorldTile worldTile2 in tilesOfType)
					{
						if (!(worldTile2.building == null) && worldTile2.building.stats.canBeHarvested && worldTile2.isSameIsland(pActor.currentTile) && !(worldTile2.targetedBy != null))
						{
							float num3 = Toolbox.DistTile(pActor.currentTile, worldTile2);
							if (worldTile == null || num3 < num2)
							{
								num2 = num3;
								worldTile = worldTile2;
							}
						}
					}
					num++;
					if (num > 3 && worldTile != null)
					{
						break;
					}
				}
			}
			if (worldTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = worldTile;
			pActor.beh_building_target = pActor.beh_tile_target.building;
			return BehResult.Continue;
		}
	}
}
