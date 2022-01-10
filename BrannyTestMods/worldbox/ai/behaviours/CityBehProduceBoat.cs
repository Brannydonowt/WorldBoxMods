using System;

namespace ai.behaviours
{
	// Token: 0x020003A4 RID: 932
	public class CityBehProduceBoat : BehaviourActionCity
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x000A89BC File Offset: 0x000A6BBC
		public override BehResult execute(City pCity)
		{
			if (pCity.boatBuildTimer > 0f)
			{
				return BehResult.Stop;
			}
			if (!pCity.haveBuildingType("docks", true))
			{
				return BehResult.Stop;
			}
			Building buildingType = pCity.getBuildingType("docks", true, true);
			if (buildingType == null)
			{
				return BehResult.Stop;
			}
			Actor actor = buildingType.GetComponent<Docks>().buildBoatFromHere(pCity);
			if (actor == null)
			{
				return BehResult.Stop;
			}
			actor.setKingdom(pCity.kingdom);
			actor.GetComponent<Boat>().updateTexture();
			actor.setCity(pCity);
			pCity.boatBuildTimer = 10f;
			return BehResult.Continue;
		}
	}
}
