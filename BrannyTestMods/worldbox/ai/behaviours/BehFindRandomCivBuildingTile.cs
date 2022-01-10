using System;

namespace ai.behaviours
{
	// Token: 0x02000363 RID: 867
	public class BehFindRandomCivBuildingTile : BehaviourActionActor
	{
		// Token: 0x06001344 RID: 4932 RVA: 0x000A17E0 File Offset: 0x0009F9E0
		public override BehResult execute(Actor pActor)
		{
			MapRegion region = pActor.currentTile.region;
			if (Toolbox.randomChance(0.65f) && region.tiles.Count > 0)
			{
				pActor.beh_tile_target = region.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			MapBox.instance.getObjectsInChunks(pActor.currentTile, 0, MapObjectType.Building);
			Building building = null;
			for (int i = 0; i < MapBox.instance.temp_map_objects.Count; i++)
			{
				MapBox.instance.temp_map_objects.ShuffleOne(i);
				Building building2 = (Building)MapBox.instance.temp_map_objects[i];
				if (building2.currentTile.isSameIsland(pActor.currentTile) && building2.stats.cityBuilding && building2.data.state == BuildingState.CivKingdom)
				{
					building = building2;
					break;
				}
			}
			if (!(building == null))
			{
				pActor.beh_tile_target = building.currentTile.region.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			if (region.tiles.Count > 0)
			{
				pActor.beh_tile_target = region.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
