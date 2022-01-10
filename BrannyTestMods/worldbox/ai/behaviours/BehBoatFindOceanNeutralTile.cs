using System;

namespace ai.behaviours
{
	// Token: 0x0200032F RID: 815
	public class BehBoatFindOceanNeutralTile : BehBoat
	{
		// Token: 0x060012C2 RID: 4802 RVA: 0x0009F824 File Offset: 0x0009DA24
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			if (pActor.homeBuilding != null)
			{
				if (pActor.GetComponent<Boat>().isNearDock())
				{
					return BehResult.Stop;
				}
				WorldTile oceanTileInSameOcean = pActor.homeBuilding.GetComponent<Docks>().getOceanTileInSameOcean(pActor.currentTile);
				if (oceanTileInSameOcean != null)
				{
					pActor.beh_tile_target = oceanTileInSameOcean;
					return BehResult.Continue;
				}
			}
			WorldTile randomTileForBoat = ActorTool.getRandomTileForBoat(pActor);
			if (randomTileForBoat == null)
			{
				return BehResult.Stop;
			}
			if (randomTileForBoat.zone.city != null && randomTileForBoat.zone.city.kingdom.isEnemy(pActor.kingdom))
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = randomTileForBoat;
			return BehResult.Continue;
		}
	}
}
