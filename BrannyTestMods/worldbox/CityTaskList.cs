using System;
using System.Collections.Generic;

// Token: 0x02000009 RID: 9
public class CityTaskList
{
	// Token: 0x06000024 RID: 36 RVA: 0x00004226 File Offset: 0x00002426
	public static void init()
	{
		CityTaskList.jobsUnit = new List<string>();
		CityTaskList.jobsWarrior = new List<string>();
		CityTaskList.initTaskList();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00004241 File Offset: 0x00002441
	internal static void prepare(City pCity, Actor pUnit)
	{
		CityTaskList.city = pCity;
		CityTaskList.taskUnit = pUnit;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00004250 File Offset: 0x00002450
	private static void initTaskList()
	{
		CityTaskList.jobsUnit.Add("builder");
		CityTaskList.jobsUnit.Add("gatherer");
		CityTaskList.jobsUnit.Add("farmer_plower");
		CityTaskList.jobsUnit.Add("farmer");
		CityTaskList.jobsUnit.Add("hunter");
		CityTaskList.jobsUnit.Add("woodcutter");
		CityTaskList.jobsUnit.Add("miner");
		CityTaskList.jobsUnit.Add("miner_deposit");
		CityTaskList.jobsUnit.Add("blacksmith");
		CityTaskList.jobsUnit.Add("road_builder");
		CityTaskList.jobsUnit.Add("cleaner");
		CityTaskList.jobsUnit.Add("settler");
		CityTaskList.jobsWarrior.Add("attacker");
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00004320 File Offset: 0x00002520
	internal static bool checkJob(string pJob)
	{
		if (CityTaskList.city.jobs.haveJob(pJob))
		{
			CityTaskList.city.jobs.takeJob(pJob);
			CityTaskList.taskUnit.ai.setJob(pJob);
			return true;
		}
		return false;
	}

	// Token: 0x0400002D RID: 45
	internal static List<string> jobsUnit;

	// Token: 0x0400002E RID: 46
	internal static List<string> jobsWarrior;

	// Token: 0x0400002F RID: 47
	private static Actor taskUnit;

	// Token: 0x04000030 RID: 48
	private static City city;
}
