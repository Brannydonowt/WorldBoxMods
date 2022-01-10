using System;

namespace ai.behaviours
{
	// Token: 0x0200034C RID: 844
	public class BehCityFindNewJob : BehCity
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x000A0761 File Offset: 0x0009E961
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isInCityIsland())
			{
				pActor.city.findNewJob(pActor);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
