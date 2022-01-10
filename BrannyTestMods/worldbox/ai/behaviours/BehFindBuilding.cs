using System;

namespace ai.behaviours
{
	// Token: 0x0200035D RID: 861
	public class BehFindBuilding : BehaviourActionActor
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x000A11A5 File Offset: 0x0009F3A5
		public BehFindBuilding(string pType)
		{
			this.type = pType;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000A11B4 File Offset: 0x0009F3B4
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_building_target = BehFindBuilding.findBuildingType(pActor, this.type);
			if (pActor.beh_building_target != null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000A11DC File Offset: 0x0009F3DC
		public static Building findBuildingType(Actor pActor, string pType)
		{
			Building building = null;
			Toolbox.temp_list_buildings_2.Clear();
			Toolbox.addToTempChunksBuildingsType(pActor.currentTile.chunk, pType, false);
			for (int i = 0; i < pActor.currentTile.chunk.neighboursAll.Count; i++)
			{
				Toolbox.addToTempChunksBuildingsType(pActor.currentTile.chunk.neighboursAll[i], pType, false);
			}
			if (Toolbox.temp_list_buildings_2.Count == 0)
			{
				return null;
			}
			Toolbox.temp_list_buildings_2.Shuffle<Building>();
			for (int j = 0; j < Toolbox.temp_list_buildings_2.Count; j++)
			{
				Building building2 = Toolbox.temp_list_buildings_2[j];
				if (building2.currentTile.isSameIsland(pActor.currentTile))
				{
					building = building2;
				}
			}
			if (building == null)
			{
				building = Toolbox.temp_list_buildings_2.GetRandom<Building>();
			}
			return building;
		}

		// Token: 0x0400152E RID: 5422
		private string type;
	}
}
