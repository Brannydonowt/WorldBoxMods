using System;
using System.Collections.Generic;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x0200039E RID: 926
	public class CityBehCheckCitizenTasks : BehaviourActionCity
	{
		// Token: 0x060013EF RID: 5103 RVA: 0x000A79AC File Offset: 0x000A5BAC
		public override BehResult execute(City pCity)
		{
			CityTasksData tasks = pCity.tasks;
			CityJobs jobs = pCity.jobs;
			CityStatus status = pCity.status;
			Culture culture = pCity.getCulture();
			jobs.clearTasks();
			tasks.trees = 0;
			tasks.stone = 0;
			tasks.ore = 0;
			tasks.mine = 0;
			tasks.gold = 0;
			tasks.bushes = 0;
			tasks.ruins = 0;
			tasks.farmFields = 0;
			tasks.canBeFarms = 0;
			tasks.roads = 0;
			tasks.wheats = 0;
			tasks.fire = 0;
			if (!DebugConfig.isOn(DebugOption.SystemCityTasks))
			{
				return BehResult.Continue;
			}
			tasks.metal = pCity.data.storage.get("ore");
			for (int i = 0; i < pCity.neighbourZones.Count; i++)
			{
				TileZone tileZone = pCity.neighbourZones[i];
				tasks.fire += tileZone.tilesOnFire;
			}
			for (int j = 0; j < pCity.zones.Count; j++)
			{
				TileZone tileZone2 = pCity.zones[j];
				tasks.fire += tileZone2.tilesOnFire;
				tasks.trees += tileZone2.trees.Count;
				tasks.stone += tileZone2.stone.Count;
				tasks.ore += tileZone2.ore.Count;
				tasks.gold += tileZone2.gold.Count;
				tasks.ruins += tileZone2.ruins.Count;
				tasks.canBeFarms += tileZone2.countCanBeFarms();
				using (HashSet<Building>.Enumerator enumerator = tileZone2.food.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.haveResources)
						{
							tasks.bushes++;
						}
					}
				}
				HashSetWorldTile tilesOfType = tileZone2.getTilesOfType(TopTileLibrary.field);
				if (tilesOfType != null)
				{
					foreach (WorldTile worldTile in tilesOfType)
					{
						if (worldTile.building == null)
						{
							tasks.farmFields++;
						}
						else if (worldTile.building.stats.buildingType == BuildingType.Wheat && !worldTile.building.canBeUpgraded())
						{
							tasks.wheats++;
						}
					}
				}
			}
			if (pCity.haveBuildingType("mine", true))
			{
				tasks.mine = status.population / 20;
				jobs.jobs["miner"] = tasks.mine;
			}
			if (tasks.fire > 0)
			{
				jobs.addJob("fireman", tasks.fire, 0);
			}
			int num = 0;
			if (tasks.stone > 0 && pCity.data.storage.get("stone") < 999)
			{
				num += tasks.stone;
			}
			if (tasks.gold > 0 && pCity.data.storage.get("gold") < 999)
			{
				num += tasks.gold;
			}
			if (tasks.ore > 0 && pCity.data.storage.get("ore") < 999)
			{
				num += tasks.ore;
			}
			if (num > status.population / 10)
			{
				num = status.population / 10;
			}
			if (num > 0)
			{
				jobs.addJob("miner_deposit", num, 0);
			}
			if (tasks.metal > 0)
			{
				jobs.addJob("metalworker", status.population / 20, pCity.data.storage.get("ore"));
			}
			if (status.population > 60)
			{
				jobs.addJob("blacksmith", 3, 0);
			}
			else if (status.population > 30)
			{
				jobs.addJob("blacksmith", 2, 0);
			}
			else if (status.population > 15)
			{
				jobs.addJob("blacksmith", 1, 0);
			}
			if (pCity.settleTarget != null)
			{
				if ((BehaviourActionBase<City>.world.worldLaws.world_law_kingdom_expansion.boolVal && status.population > 30 && BehaviourActionBase<City>.world.cityPlaceFinder.zones.Count > 0) || (status.population > 30 && status.homesFree < 5))
				{
					int num2 = status.population / 20 + 1;
					if (num2 > 7)
					{
						num2 = 7;
					}
					jobs.jobs["settler"] = num2;
				}
				else
				{
					jobs.jobs["settler"] = 0;
				}
			}
			if (status.population > 20)
			{
				int num3 = status.population / 20 + 1;
				if (num3 > 3)
				{
					num3 = 3;
				}
				jobs.jobs["hunter"] = num3;
			}
			if (tasks.bushes > 0)
			{
				jobs.jobs["gatherer"] = Mathf.Clamp(status.population / 10 + 1, 1, 3);
			}
			if (pCity.roadTilesToBuild.Count > 0)
			{
				tasks.roads = 1;
				jobs.jobs["road_builder"] = 1;
			}
			if (tasks.ruins > 0)
			{
				jobs.jobs["cleaner"] = 1;
			}
			if (tasks.trees > 0 && pCity.data.storage.get("wood") < 100)
			{
				int max = Mathf.Clamp(tasks.trees, 1, 3);
				jobs.jobs["woodcutter"] = Mathf.Clamp(status.population / 10 + 1, 1, max);
			}
			if (pCity.getBuildingToBuild() != null)
			{
				jobs.jobs["builder"] = Mathf.Clamp(status.population / 10 + 1, 1, 3);
			}
			int num4 = status.population / 5 + 1;
			if (tasks.farmFields < num4 && num4 < 128)
			{
				jobs.jobs["farmer_plower"] = 1;
			}
			int num5 = num4 / 2 + 1;
			if (tasks.wheats > 0 || tasks.farmFields > 0)
			{
				jobs.jobs["farmer"] = num5;
			}
			jobs.occupied.Clear();
			List<Actor> simpleList = pCity.units.getSimpleList();
			for (int k = 0; k < simpleList.Count; k++)
			{
				Actor actor = simpleList[k];
				if (actor.ai.job != null && actor.ai.job.cityJob)
				{
					if (!jobs.occupied.ContainsKey(actor.ai.job.id))
					{
						jobs.occupied[actor.ai.job.id] = 0;
					}
					Dictionary<string, int> occupied = jobs.occupied;
					string id = actor.ai.job.id;
					int num6 = occupied[id];
					occupied[id] = num6 + 1;
				}
			}
			if (status.populationAdults > 15)
			{
				if (status.populationAdults < 50)
				{
					status.warriorSlots = (int)((float)status.populationAdults * 0.4f + 1f);
				}
				else
				{
					status.warriorSlots = (int)((float)status.populationAdults * 0.85f + 1f);
				}
				if (status.warriorSlots > pCity.getArmyMaxLeader())
				{
					status.warriorSlots = pCity.getArmyMaxLeader();
				}
				if (culture != null)
				{
					status.warriorSlots += (int)culture.stats.bonus_max_army.value;
				}
				jobs.jobs["attacker"] = status.warriorSlots;
			}
			status.warriorCurrent = pCity.countProfession(UnitProfession.Warrior);
			return BehResult.Continue;
		}
	}
}
