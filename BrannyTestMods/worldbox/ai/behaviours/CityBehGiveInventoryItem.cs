using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020003A3 RID: 931
	public class CityBehGiveInventoryItem : BehaviourActionCity
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x000A88E4 File Offset: 0x000A6AE4
		public override BehResult execute(City pCity)
		{
			List<ActorEquipmentSlot> list = ActorEquipment.getList(pCity.data.storage.itemStorage, false);
			if (list.Count == 0)
			{
				return BehResult.Stop;
			}
			foreach (ActorEquipmentSlot pSlot in list)
			{
				if ((!(pCity.kingdom.king != null) || !City.giveItem(pCity.kingdom.king, pSlot, pCity)) && (!(pCity.leader != null) || !City.giveItem(pCity.leader, pSlot, pCity)) && pCity.countProfession(UnitProfession.Warrior) > 0)
				{
					City.giveItem(pCity.professionsDict[UnitProfession.Warrior].GetRandom<Actor>(), pSlot, pCity);
				}
			}
			return BehResult.Continue;
		}
	}
}
