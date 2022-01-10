using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000391 RID: 913
	public class BehTaxiFindShipTile : BehCity
	{
		// Token: 0x060013BC RID: 5052 RVA: 0x000A39E0 File Offset: 0x000A1BE0
		public override BehResult execute(Actor pActor)
		{
			TaxiRequest requestForActor = TaxiManager.getRequestForActor(pActor);
			if (requestForActor == null || requestForActor.taxi == null || requestForActor.state != TaxiRequestState.Loading)
			{
				return BehResult.Stop;
			}
			Boat component = requestForActor.taxi.actor.GetComponent<Boat>();
			WorldTile worldTile = null;
			if (component.pickup_near_dock)
			{
				Building homeBuilding = component.actor.homeBuilding;
				if (homeBuilding != null)
				{
					WorldTile constructionTile = homeBuilding.getConstructionTile();
					if (constructionTile != null)
					{
						worldTile = constructionTile.region.tiles.GetRandom<WorldTile>();
					}
				}
			}
			if (worldTile == null)
			{
				worldTile = PathfinderTools.raycastTileForUnitToEmbark(pActor.currentTile, requestForActor.taxi.actor.currentTile);
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
