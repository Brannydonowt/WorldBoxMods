using System;

namespace ai.behaviours
{
	// Token: 0x0200034D RID: 845
	public class BehCityFindStorage : BehCity
	{
		// Token: 0x06001308 RID: 4872 RVA: 0x000A0784 File Offset: 0x0009E984
		public override BehResult execute(Actor pActor)
		{
			Building storageNear = pActor.city.getStorageNear(pActor.currentTile);
			if (storageNear != null)
			{
				pActor.beh_building_target = storageNear;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
