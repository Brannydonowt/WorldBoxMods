using System;
using System.Collections.Generic;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x020003B2 RID: 946
	public class KingdomCheckRevolts : BehaviourActionKingdom
	{
		// Token: 0x06001432 RID: 5170 RVA: 0x000AA3C0 File Offset: 0x000A85C0
		public override BehResult execute(Kingdom pKingdom)
		{
			if (!MapBox.instance.worldLaws.world_law_diplomacy.boolVal)
			{
				return BehResult.Stop;
			}
			if (!MapBox.instance.worldLaws.world_law_rebellions.boolVal)
			{
				return BehResult.Stop;
			}
			if (pKingdom.timer_loyalty > 0f)
			{
				return BehResult.Stop;
			}
			if (pKingdom.king == null)
			{
				return BehResult.Stop;
			}
			KingdomCheckRevolts._temp_cities.Clear();
			pKingdom.timer_loyalty = (float)ActorTool.attributeDice(pKingdom.king, 10);
			int num = 0;
			int num2 = 0;
			City city = null;
			foreach (City city2 in pKingdom.cities)
			{
				if (!(city2 == pKingdom.capital))
				{
					Object x = city2;
					Actor king = pKingdom.king;
					if (!(x == ((king != null) ? king.city : null)))
					{
						city2.getRelationRating();
						if (!(city2.leader == null))
						{
							if (city2._opinion_total >= 0)
							{
								num2++;
							}
							else
							{
								num++;
								if (city2.captureTicks <= 0f && city2.data.timer_revolt <= 0f && city2.getArmy() >= city2.getArmyMaxCity() / 2 && city2.getArmy() >= 10)
								{
									KingdomCheckRevolts._temp_cities.Add(city2);
									if (city == null || city2._opinion_total < city._opinion_total)
									{
										city = city2;
									}
								}
							}
						}
					}
				}
			}
			if (city == null)
			{
				return BehResult.Stop;
			}
			KingdomCheckRevolts._temp_cities.Remove(city);
			int pAmount = 4 + this.getBonusDice(city._opinion_total);
			int num3 = ActorTool.attributeDice(pKingdom.king, 4);
			if (ActorTool.attributeDice(city.leader, pAmount) + num * 2 > num3)
			{
				WorldLog.logCityRevolt(city);
				Kingdom kingdom = city.kingdom;
				Kingdom pKingdom2 = city.makeOwnKingdom();
				MapBox.instance.kingdoms.diplomacyManager.startWar(pKingdom2, kingdom, true);
				int cities = city.leader.curStats.cities;
				int num4 = 0;
				foreach (City city3 in KingdomCheckRevolts._temp_cities)
				{
					pAmount = 4 + this.getBonusDice(city._opinion_total);
					num3 = ActorTool.attributeDice(pKingdom.king, 4);
					if (ActorTool.attributeDice(city3.leader, pAmount) + num * 3 + num4 * 7 > num3)
					{
						city3.joinAnotherKingdom(pKingdom2);
						num4++;
						if (num4 >= cities)
						{
							break;
						}
					}
				}
				return BehResult.Continue;
			}
			return BehResult.Continue;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x000AA668 File Offset: 0x000A8868
		public int getBonusDice(int pOpinion)
		{
			if (pOpinion > 0)
			{
				return 0;
			}
			return Mathf.Abs(pOpinion) / 20;
		}

		// Token: 0x04001574 RID: 5492
		private static List<City> _temp_cities = new List<City>();
	}
}
