using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020003A6 RID: 934
	public class CityBehProduceUnit : BehaviourActionCity
	{
		// Token: 0x06001406 RID: 5126 RVA: 0x000A8B78 File Offset: 0x000A6D78
		public override BehResult execute(City pCity)
		{
			if (!DebugConfig.isOn(DebugOption.SystemProduceNewCitizens))
			{
				return BehResult.Stop;
			}
			if (pCity.getFoodItem(null) == null)
			{
				return BehResult.Stop;
			}
			if (pCity.status.homesFree == 0)
			{
				return BehResult.Stop;
			}
			this.unitProduced = false;
			int pMaxExclusive = pCity.status.population / 7 + 1;
			int num = Toolbox.randomInt(1, pMaxExclusive);
			CityBehProduceUnit.parent_index = 0;
			int num2 = 0;
			while (num2 < num && pCity.status.homesFree != 0)
			{
				this.tryToProduceUnit(pCity);
				num2++;
			}
			if (this.unitProduced)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000A8BFC File Offset: 0x000A6DFC
		private void tryToProduceUnit(City pCity)
		{
			if (pCity.getFoodItem(null) == null)
			{
				return;
			}
			if (pCity.status.homesFree == 0)
			{
				return;
			}
			Building buildingType = pCity.getBuildingType("house", true, false);
			if (buildingType == null)
			{
				return;
			}
			if (this.produceNewCitizen(buildingType, pCity))
			{
				this.unitProduced = true;
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000A8C4C File Offset: 0x000A6E4C
		private bool produceNewCitizen(Building pBuilding, City pCity)
		{
			List<Actor> simpleList = pCity.units.getSimpleList();
			Actor randomParent = CityBehProduceUnit.getRandomParent(simpleList, null);
			if (randomParent == null)
			{
				return false;
			}
			if (randomParent.haveTrait("infected") && Toolbox.randomBool())
			{
				return false;
			}
			Actor randomParent2 = CityBehProduceUnit.getRandomParent(simpleList, randomParent);
			randomParent.data.children++;
			ResourceAsset foodItem = pCity.getFoodItem(null);
			pCity.eatFoodItem(foodItem.id);
			pCity.status.homesFree--;
			pCity.data.born++;
			if (pCity.kingdom != null)
			{
				pCity.kingdom.born++;
			}
			ActorStats stats = randomParent.stats;
			ActorData actorData = new ActorData();
			actorData.cityID = pCity.data.cityID;
			actorData.status = new ActorStatus();
			actorData.status.statsID = stats.id;
			ActorBase.generateCivUnit(randomParent.stats, actorData.status, randomParent.race);
			actorData.status.generateTraits(stats, randomParent.race);
			actorData.status.inheritTraits(randomParent.data.traits);
			actorData.status.hunger = stats.maxHunger / 2;
			if (randomParent2 != null)
			{
				actorData.status.inheritTraits(randomParent2.data.traits);
				randomParent2.data.children++;
			}
			actorData.status.skin = ActorTool.getBabyColor(randomParent, randomParent2);
			actorData.status.skin_set = randomParent.data.skin_set;
			Culture babyCulture = CityBehProduceUnit.getBabyCulture(randomParent, randomParent2);
			if (babyCulture != null)
			{
				actorData.status.culture = babyCulture.id;
				actorData.status.level = babyCulture.getBornLevel();
			}
			pCity.data.popPoints.Add(actorData);
			return true;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000A8E3C File Offset: 0x000A703C
		private static Culture getBabyCulture(Actor pActor1, Actor pActor2)
		{
			string text = pActor1.data.culture;
			string text2 = text;
			if (pActor2 != null)
			{
				text2 = pActor2.data.culture;
			}
			if (string.IsNullOrEmpty(text))
			{
				City city = pActor1.city;
				text = ((city != null) ? city.data.culture : null);
			}
			if (string.IsNullOrEmpty(text2) && pActor2 != null)
			{
				City city2 = pActor2.city;
				text2 = ((city2 != null) ? city2.data.culture : null);
			}
			Culture culture = pActor1.currentTile.zone.culture;
			if (culture != null && culture.race == pActor1.race.id && Toolbox.randomChance(culture.stats.culture_spread_convert_chance.value))
			{
				text = culture.id;
			}
			if (Toolbox.randomBool())
			{
				return BehaviourActionBase<City>.world.cultures.get(text);
			}
			return BehaviourActionBase<City>.world.cultures.get(text2);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000A8F28 File Offset: 0x000A7128
		private static Actor getRandomParent(List<Actor> pList, Actor pActorIgnore = null)
		{
			if (CityBehProduceUnit.parent_index >= pList.Count)
			{
				CityBehProduceUnit.parent_index = 0;
			}
			for (int i = CityBehProduceUnit.parent_index; i < pList.Count; i++)
			{
				pList.ShuffleOne(i);
				Actor actor = pList[i];
				if (actor.data.alive && !(actor == pActorIgnore) && !actor.haveTrait("plague") && actor.data.age > 18)
				{
					CityBehProduceUnit.parent_index = i;
					return actor;
				}
			}
			return null;
		}

		// Token: 0x0400156A RID: 5482
		private static int parent_index;

		// Token: 0x0400156B RID: 5483
		private bool unitProduced;
	}
}
