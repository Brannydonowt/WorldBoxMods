using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020003A9 RID: 937
	public class CityBehUpgradeRandomBuilding : BehaviourActionCity
	{
		// Token: 0x06001413 RID: 5139 RVA: 0x000A9293 File Offset: 0x000A7493
		public override BehResult execute(City pCity)
		{
			if (!DebugConfig.isOn(DebugOption.SystemBuildTick))
			{
				return BehResult.Continue;
			}
			if (Toolbox.randomBool())
			{
				this.upgradeRandomBuilding(pCity);
			}
			return BehResult.Continue;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000A92B0 File Offset: 0x000A74B0
		private void upgradeRandomBuilding(City pCity)
		{
			if (pCity.buildings.Count == 0)
			{
				return;
			}
			Building building = null;
			if (building == null)
			{
				List<Building> simpleList = pCity.buildings.getSimpleList();
				for (int i = 0; i < simpleList.Count; i++)
				{
					simpleList.ShuffleOne(i);
					Building building2 = simpleList[i];
					if (building2.canBeUpgraded())
					{
						building = building2;
						break;
					}
				}
			}
			if (building == null)
			{
				return;
			}
			string upgradeTo = building.stats.upgradeTo;
			BuildingAsset buildingAsset = AssetManager.buildings.get(upgradeTo);
			if (!pCity.haveEnoughResourcesFor(buildingAsset.cost))
			{
				return;
			}
			pCity.spendResourcesFor(buildingAsset.cost);
			building.upgradeBulding();
		}
	}
}
