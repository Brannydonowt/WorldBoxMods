using System;
using System.Collections.Generic;

// Token: 0x02000021 RID: 33
public class CityBuildOrder
{
	// Token: 0x060000E5 RID: 229 RVA: 0x000129D4 File Offset: 0x00010BD4
	public static void init()
	{
		CityBuildOrder.list = new List<CityBuildOrderElement>();
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "bonfire",
			priority = 100,
			limitType = 1
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "windmill",
			priority = 100,
			limitType = 1,
			requiredBuildings = 5,
			requiredPop = 20,
			addRace = true
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "docks",
			priority = 100,
			addRace = true,
			limitType = 5,
			requiredBuildings = 2,
			waitForResources = false
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "well",
			requiredBuildings = 10,
			requiredPop = 20,
			waitForResources = false,
			limitType = 1
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "tent",
			addRace = true,
			limitID = 5,
			checkFullVillage = true
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "hall",
			priority = 90,
			limitType = 1,
			addRace = true,
			requiredBuildings = 10,
			requiredPop = 20,
			waitForResources = true
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "mine",
			limitType = 1,
			requiredBuildings = 10,
			requiredPop = 20,
			waitForResources = false
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "barracks",
			addRace = true,
			limitType = 1,
			requiredBuildings = 10,
			requiredPop = 30,
			waitForResources = false
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "watch_tower",
			addRace = true,
			limitType = 1,
			requiredBuildings = 10,
			requiredPop = 30,
			waitForResources = false
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "temple",
			addRace = true,
			limitType = 1,
			requiredBuildings = 40,
			requiredPop = 150,
			waitForResources = true
		});
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "statue",
			limitType = 1,
			requiredBuildings = 20,
			requiredPop = 170,
			waitForResources = false,
			usedByRacesCheck = true,
			usedByRaces = "human,dwarf"
		});
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00012C4D File Offset: 0x00010E4D
	private static void addTent(int pLimit)
	{
		CityBuildOrder.add(new CityBuildOrderElement
		{
			buildingID = "tent",
			addRace = true,
			limitType = pLimit
		});
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00012C72 File Offset: 0x00010E72
	public static void add(CityBuildOrderElement pElement)
	{
		CityBuildOrder.list.Add(pElement);
	}

	// Token: 0x040000D7 RID: 215
	public static List<CityBuildOrderElement> list;
}
