using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CF RID: 463
internal static class StatsHelper
{
	// Token: 0x06000A96 RID: 2710 RVA: 0x00069A60 File Offset: 0x00067C60
	public static string getStatistic(string statName)
	{
		int num = 0;
		string text = "";
		StatsHelper.world = MapBox.instance;
		StatsHelper.stats = MapBox.instance.gameStats;
		if (statName != null)
		{
			uint num2 = <PrivateImplementationDetails>.ComputeStringHash(statName);
			if (num2 <= 2435719345U)
			{
				if (num2 <= 1016429901U)
				{
					if (num2 <= 483080967U)
					{
						if (num2 != 33793887U)
						{
							if (num2 != 118714783U)
							{
								if (num2 == 483080967U)
								{
									if (statName == "world_statistics_population")
									{
										text = (StatsHelper.world.getCivWorldPopulation().ToString() ?? "");
									}
								}
							}
							else if (statName == "world_statistics_deaths_hunger")
							{
								text = (StatsHelper.world.mapStats.deaths_hunger.ToString() ?? "");
							}
						}
						else if (statName == "world_name")
						{
							text = (StatsHelper.world.mapStats.name ?? "");
						}
					}
					else if (num2 <= 758765828U)
					{
						if (num2 != 589666732U)
						{
							if (num2 == 758765828U)
							{
								if (statName == "statistics_pixels_exploded")
								{
									text = (StatsHelper.stats.data.pixelsExploded.ToString() ?? "");
								}
							}
						}
						else if (statName == "world_statistics_beasts")
						{
							using (IEnumerator<Actor> enumerator = StatsHelper.world.units.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									if (!enumerator.Current.stats.unit)
									{
										num++;
									}
								}
							}
							text = (num.ToString() ?? "");
						}
					}
					else if (num2 != 777967779U)
					{
						if (num2 == 1016429901U)
						{
							if (statName == "world_statistics_houses")
							{
								int num3 = 0;
								using (IEnumerator<Building> enumerator2 = StatsHelper.world.buildings.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										if (enumerator2.Current.stats.cityBuilding)
										{
											num3++;
										}
									}
								}
								text = (num3.ToString() ?? "");
							}
						}
					}
					else if (statName == "kingdoms")
					{
						return StatsHelper.world.kingdoms.list_civs.Count.ToString() ?? "";
					}
				}
				else if (num2 <= 1559858549U)
				{
					if (num2 <= 1369794228U)
					{
						if (num2 != 1126423310U)
						{
							if (num2 == 1369794228U)
							{
								if (statName == "statistics_trees_grown")
								{
									text = (StatsHelper.stats.data.treesGrown.ToString() ?? "");
								}
							}
						}
						else if (statName == "world_statistics_deaths_total")
						{
							text = (StatsHelper.world.mapStats.deaths.ToString() ?? "");
						}
					}
					else if (num2 != 1497970011U)
					{
						if (num2 == 1559858549U)
						{
							if (statName == "kingdoms_villages")
							{
								if (StatsHelper.getStatistic("kingdoms") == "0" && StatsHelper.getStatistic("villages") == "0")
								{
									return "";
								}
								return string.Concat(new string[]
								{
									LocalizedTextManager.getText("kingdoms", null),
									": ",
									StatsHelper.getStatistic("kingdoms"),
									"/",
									LocalizedTextManager.getText("villages", null),
									": ",
									StatsHelper.getStatistic("villages")
								});
							}
						}
					}
					else if (statName == "statistics_total_playtime")
					{
						text = Toolbox.formatTime((float)StatsHelper.stats.data.gameTime);
					}
				}
				else if (num2 <= 1802730125U)
				{
					if (num2 != 1708005325U)
					{
						if (num2 == 1802730125U)
						{
							if (statName == "world_statistics_deaths_natural")
							{
								text = (StatsHelper.world.mapStats.deaths_age.ToString() ?? "");
							}
						}
					}
					else if (statName == "world_statistics_deaths_eaten")
					{
						text = (StatsHelper.world.mapStats.deaths_eaten.ToString() ?? "");
					}
				}
				else if (num2 != 2093771881U)
				{
					if (num2 == 2435719345U)
					{
						if (statName == "world_statistics_most_populated_village")
						{
							City city = null;
							foreach (City city2 in StatsHelper.world.citiesList)
							{
								if (city == null || city2.getPopulationTotal() > city.getPopulationTotal())
								{
									city = city2;
								}
							}
							if (city == null)
							{
								text = "?";
							}
							else
							{
								text = city.data.cityName + " [" + city.getPopulationTotal().ToString() + "]";
							}
						}
					}
				}
				else if (statName == "world_statistics_trees")
				{
					int num4 = 0;
					using (IEnumerator<Building> enumerator2 = StatsHelper.world.buildings.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.stats.buildingType == BuildingType.Tree)
							{
								num4++;
							}
						}
					}
					text = (num4.ToString() ?? "");
				}
			}
			else if (num2 <= 3264656001U)
			{
				if (num2 <= 2626934769U)
				{
					if (num2 != 2481706057U)
					{
						if (num2 != 2542657016U)
						{
							if (num2 == 2626934769U)
							{
								if (statName == "races")
								{
									int num5 = 0;
									int num6 = 0;
									int num7 = 0;
									int num8 = 0;
									foreach (Actor actor in StatsHelper.world.units)
									{
										if (actor.race.id == "human")
										{
											num5++;
										}
										if (actor.race.id == "elf")
										{
											num6++;
										}
										if (actor.race.id == "orc")
										{
											num7++;
										}
										if (actor.race.id == "dwarf")
										{
											num8++;
										}
									}
									return string.Concat(new string[]
									{
										"H:",
										num5.ToString(),
										" E:",
										num6.ToString(),
										" O:",
										num7.ToString(),
										" D:",
										num8.ToString()
									});
								}
							}
						}
						else if (statName == "houses")
						{
							if (StatsHelper.getStatistic("world_statistics_houses") == "0")
							{
								return "";
							}
							text = LocalizedTextManager.getText("world_statistics_houses_all", null);
							text = text.Replace("$houses$", StatsHelper.getStatistic("world_statistics_houses"));
							return text.Replace("$destroyed$", StatsHelper.getStatistic("world_statistics_houses_destroyed"));
						}
					}
					else if (statName == "world_statistics_houses_destroyed")
					{
						text = (StatsHelper.world.mapStats.housesDestroyed.ToString() ?? "");
					}
				}
				else if (num2 <= 3030326920U)
				{
					if (num2 != 2984398973U)
					{
						if (num2 == 3030326920U)
						{
							if (statName == "statistics_creatures_created")
							{
								text = (StatsHelper.stats.data.creaturesCreated.ToString() ?? "");
							}
						}
					}
					else if (statName == "statistics_meteorites_launched")
					{
						text = (StatsHelper.stats.data.meteoritesLaunched.ToString() ?? "");
					}
				}
				else if (num2 != 3260641586U)
				{
					if (num2 == 3264656001U)
					{
						if (statName == "statistics_bombs_dropped")
						{
							text = (StatsHelper.stats.data.bombsDropped.ToString() ?? "");
						}
					}
				}
				else if (statName == "world_statistics_islands")
				{
					text = (StatsHelper.world.islandsCalculator.countLandIslands().ToString() ?? "");
				}
			}
			else if (num2 <= 3895373649U)
			{
				if (num2 <= 3587022190U)
				{
					if (num2 != 3293556380U)
					{
						if (num2 == 3587022190U)
						{
							if (statName == "world_statistics_deaths_other")
							{
								text = (StatsHelper.world.mapStats.deaths_other.ToString() ?? "");
							}
						}
					}
					else if (statName == "world_statistics_infected")
					{
						using (IEnumerator<Actor> enumerator = StatsHelper.world.units.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.isInfectedWithAnything())
								{
									num++;
								}
							}
						}
						text = (num.ToString() ?? "");
					}
				}
				else if (num2 != 3599352998U)
				{
					if (num2 == 3895373649U)
					{
						if (statName == "world_statistics_time")
						{
							text = "y:" + (StatsHelper.world.mapStats.year + 1).ToString() + ", m:" + (StatsHelper.world.mapStats.month + 1).ToString();
						}
					}
				}
				else if (statName == "statistics_creatures_died")
				{
					text = (StatsHelper.stats.data.creaturesDied.ToString() ?? "");
				}
			}
			else if (num2 <= 4088841282U)
			{
				if (num2 != 4005057186U)
				{
					if (num2 == 4088841282U)
					{
						if (statName == "world_statistics_biggest_village")
						{
							City city3 = null;
							foreach (City city4 in StatsHelper.world.citiesList)
							{
								if (city3 == null || city4.zones.Count > city3.zones.Count)
								{
									city3 = city4;
								}
							}
							if (city3 == null)
							{
								text = "?";
							}
							else
							{
								text = city3.data.cityName + " [" + city3.zones.Count.ToString() + "]";
							}
						}
					}
				}
				else if (statName == "world_statistics_deaths_plague")
				{
					text = (StatsHelper.world.mapStats.deaths_plague.ToString() ?? "");
				}
			}
			else if (num2 != 4198346388U)
			{
				if (num2 == 4262922032U)
				{
					if (statName == "villages")
					{
						return StatsHelper.world.citiesList.Count.ToString() ?? "";
					}
				}
			}
			else if (statName == "most_popular_race")
			{
				int num9 = 0;
				Race race = null;
				foreach (Race race2 in AssetManager.raceLibrary.list)
				{
					if (race2.units.Count > num9)
					{
						race = race2;
						num9 = race2.units.Count;
					}
				}
				if (num9 == 0 || race == null)
				{
					return "";
				}
				text = LocalizedTextManager.getText("world_statistics_most_popular_race", null);
				string nameLocale = race.nameLocale;
				if (string.IsNullOrEmpty(nameLocale))
				{
					nameLocale = race.units.GetRandom().stats.nameLocale;
				}
				if (string.IsNullOrEmpty(nameLocale))
				{
					string str = "no name found for ";
					Race race3 = race;
					Debug.Log(str + ((race3 != null) ? race3.ToString() : null));
					return "";
				}
				text = text.Replace("$race$", LocalizedTextManager.getText(nameLocale, null));
				return text.Replace("$units$", num9.ToString() ?? "");
			}
		}
		return text;
	}

	// Token: 0x04000D18 RID: 3352
	private static MapBox world;

	// Token: 0x04000D19 RID: 3353
	private static GameStats stats;
}
