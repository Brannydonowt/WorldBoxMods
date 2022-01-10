using System;

namespace ai.behaviours
{
	// Token: 0x020003AB RID: 939
	public class KingdomBehCheckCapital : BehaviourActionKingdom
	{
		// Token: 0x06001418 RID: 5144 RVA: 0x000A97BC File Offset: 0x000A79BC
		public override BehResult execute(Kingdom pKingdom)
		{
			if (pKingdom.cities.Count == 0)
			{
				return BehResult.Continue;
			}
			if (pKingdom.capital != null && pKingdom.capital.alive)
			{
				return BehResult.Continue;
			}
			pKingdom.cities.Shuffle<City>();
			City city = null;
			foreach (City city2 in pKingdom.cities)
			{
				if (city == null || city2.buildings.Count > city.buildings.Count)
				{
					city = city2;
				}
			}
			pKingdom.capital = city;
			pKingdom.capitalID = pKingdom.capital.data.cityID;
			pKingdom.location = pKingdom.capital.cityCenter;
			return BehResult.Continue;
		}
	}
}
