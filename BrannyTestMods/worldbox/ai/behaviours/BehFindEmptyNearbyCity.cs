using System;

namespace ai.behaviours
{
	// Token: 0x0200035F RID: 863
	public class BehFindEmptyNearbyCity : BehaviourActionActor
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x000A12D4 File Offset: 0x0009F4D4
		public override BehResult execute(Actor pActor)
		{
			City emptyCity = BehFindEmptyNearbyCity.getEmptyCity(pActor.currentTile, pActor.race);
			if (emptyCity != null)
			{
				pActor.becomeCitizen(emptyCity);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000A1308 File Offset: 0x0009F508
		public static City getEmptyCity(WorldTile pFromTile, Race pRace)
		{
			BehaviourActionActor.temp_cities.Clear();
			foreach (City city in MapBox.instance.citiesList)
			{
				if (city.race == pRace && city.status.population <= 40 && (city.status.population <= 5 || city.status.homesFree != 0) && city.getTile() != null && city.getTile().isSameIsland(pFromTile))
				{
					BehaviourActionActor.temp_cities.Add(city);
				}
			}
			return Toolbox.getRandom<City>(BehaviourActionActor.temp_cities);
		}
	}
}
