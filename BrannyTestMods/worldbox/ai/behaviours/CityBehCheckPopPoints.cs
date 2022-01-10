using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020003A0 RID: 928
	public class CityBehCheckPopPoints : BehaviourActionCity
	{
		// Token: 0x060013F8 RID: 5112 RVA: 0x000A8420 File Offset: 0x000A6620
		public override BehResult execute(City pCity)
		{
			if (pCity.data.popPoints.Count == 0)
			{
				return BehResult.Continue;
			}
			CityBehCheckPopPoints._temp_actors.Clear();
			CityBehCheckPopPoints._temp_buildings.Clear();
			int populationUnits = pCity.getPopulationUnits();
			int num = 0;
			int num2 = 0;
			foreach (string text in pCity.jobs.jobs.Keys)
			{
				int num3 = pCity.jobs.jobs[text];
				int num4 = 0;
				if (pCity.jobs.occupied.ContainsKey(text))
				{
					num4 = pCity.jobs.occupied[text];
				}
				num += num3;
				num2 += num4;
			}
			if (num <= populationUnits)
			{
				return BehResult.Continue;
			}
			int num5 = num - populationUnits;
			if (num5 < 0)
			{
				return BehResult.Continue;
			}
			CityBehCheckPopPoints._temp_actors.Clear();
			List<Building> simpleList = pCity.buildings.getSimpleList();
			for (int i = 0; i < simpleList.Count; i++)
			{
				simpleList.ShuffleOne(i);
				Building building = simpleList[i];
				if (!building.data.underConstruction && building.data.alive && !building.stats.isRuin && building.stats.housing != 0)
				{
					CityBehCheckPopPoints._temp_buildings.Add(building);
					if (CityBehCheckPopPoints._temp_buildings.Count > num5)
					{
						break;
					}
				}
			}
			if (CityBehCheckPopPoints._temp_buildings.Count == 0)
			{
				return BehResult.Continue;
			}
			if (num5 > 10)
			{
				num5 = 10;
			}
			for (int j = 0; j < num5; j++)
			{
				ActorData actorDataPopPoint = City.getActorDataPopPoint(pCity);
				if (actorDataPopPoint == null)
				{
					break;
				}
				Building random = CityBehCheckPopPoints._temp_buildings.GetRandom<Building>();
				pCity.spawnPopPoint(actorDataPopPoint, random.currentTile);
			}
			return BehResult.Continue;
		}

		// Token: 0x04001567 RID: 5479
		private static List<Actor> _temp_actors = new List<Actor>();

		// Token: 0x04001568 RID: 5480
		private static List<Building> _temp_buildings = new List<Building>();
	}
}
