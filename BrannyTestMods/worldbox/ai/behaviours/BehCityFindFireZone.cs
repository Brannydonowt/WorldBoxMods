using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200034B RID: 843
	public class BehCityFindFireZone : BehCity
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x000A0540 File Offset: 0x0009E740
		public override BehResult execute(Actor pActor)
		{
			BehCityFindFireZone.chosenTile = null;
			BehCityFindFireZone.tNewTile = null;
			BehCityFindFireZone.tAllZones.Clear();
			BehCityFindFireZone.tAllZones.AddRange(pActor.city.zones);
			BehCityFindFireZone.tAllZones.AddRange(pActor.city.neighbourZones);
			for (int i = 0; i < BehCityFindFireZone.tAllZones.Count; i++)
			{
				TileZone tileZone = BehCityFindFireZone.tAllZones[i];
				if (tileZone.tilesOnFire != 0)
				{
					BehCityFindFireZone.tNewTile = tileZone.centerTile;
					if (BehCityFindFireZone.chosenTile != null)
					{
						BehCityFindFireZone.newDist = Toolbox.Dist((float)pActor.currentTile.pos.x, (float)pActor.currentTile.pos.y, (float)BehCityFindFireZone.tNewTile.pos.x, (float)BehCityFindFireZone.tNewTile.pos.y);
						if (BehCityFindFireZone.chosenDist > BehCityFindFireZone.newDist)
						{
							BehCityFindFireZone.chosenTile = BehCityFindFireZone.tNewTile;
							BehCityFindFireZone.chosenDist = BehCityFindFireZone.newDist;
						}
					}
					else
					{
						BehCityFindFireZone.chosenTile = BehCityFindFireZone.tNewTile;
						BehCityFindFireZone.chosenDist = Toolbox.Dist((float)pActor.currentTile.pos.x, (float)pActor.currentTile.pos.y, (float)BehCityFindFireZone.chosenTile.pos.x, (float)BehCityFindFireZone.chosenTile.pos.y);
					}
				}
			}
			if (BehCityFindFireZone.chosenTile == null)
			{
				return BehResult.Stop;
			}
			BehCityFindFireZone.chosenTile = Toolbox.getClosestTile(BehCityFindFireZone.chosenTile.zone.tiles, pActor.currentTile);
			if (BehCityFindFireZone.chosenTile.data.fire || BehCityFindFireZone.chosenTile.building != null)
			{
				for (int j = 0; j < 10; j++)
				{
					BehCityFindFireZone.chosenTile = Toolbox.getRandom<WorldTile>(BehCityFindFireZone.chosenTile.zone.tiles);
					if (!BehCityFindFireZone.chosenTile.data.fire && BehCityFindFireZone.chosenTile.building == null)
					{
						break;
					}
				}
			}
			pActor.beh_tile_target = BehCityFindFireZone.chosenTile;
			return BehResult.Continue;
		}

		// Token: 0x04001529 RID: 5417
		private static List<TileZone> tAllZones = new List<TileZone>();

		// Token: 0x0400152A RID: 5418
		private static WorldTile chosenTile;

		// Token: 0x0400152B RID: 5419
		private static WorldTile tNewTile;

		// Token: 0x0400152C RID: 5420
		private static float chosenDist;

		// Token: 0x0400152D RID: 5421
		private static float newDist;
	}
}
