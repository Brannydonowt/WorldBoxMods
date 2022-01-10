using System;

namespace ai.behaviours
{
	// Token: 0x02000360 RID: 864
	public class BehFindFarmField : BehCity
	{
		// Token: 0x0600133C RID: 4924 RVA: 0x000A13C8 File Offset: 0x0009F5C8
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.zones.Count == 0)
			{
				return BehResult.Stop;
			}
			float num = 0f;
			WorldTile worldTile = null;
			int num2 = 0;
			for (int i = 0; i < pActor.city.zones.Count; i++)
			{
				pActor.city.zones.ShuffleOne(i);
				HashSetWorldTile tilesOfType = pActor.city.zones[i].getTilesOfType(TopTileLibrary.field);
				if (tilesOfType != null && tilesOfType.Count != 0)
				{
					foreach (WorldTile worldTile2 in tilesOfType)
					{
						if (!(worldTile2.targetedBy != null) && pActor.currentTile.isSameIsland(worldTile2) && !(worldTile2.building != null))
						{
							float num3 = Toolbox.DistTile(pActor.currentTile, worldTile2);
							if (worldTile == null || num3 < num)
							{
								num = num3;
								worldTile = worldTile2;
							}
						}
					}
					num2++;
					if (num2 > 3 && worldTile != null)
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
			return BehResult.Continue;
		}
	}
}
