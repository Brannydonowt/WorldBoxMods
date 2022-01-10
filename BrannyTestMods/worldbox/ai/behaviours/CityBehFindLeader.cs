using System;

namespace ai.behaviours
{
	// Token: 0x020003A2 RID: 930
	public class CityBehFindLeader : BehaviourActionCity
	{
		// Token: 0x060013FD RID: 5117 RVA: 0x000A8808 File Offset: 0x000A6A08
		public override BehResult execute(City pCity)
		{
			if (pCity.units.Count < 10)
			{
				return BehResult.Continue;
			}
			if (pCity.leader != null)
			{
				return BehResult.Continue;
			}
			if (pCity.captureTicks > 0f)
			{
				return BehResult.Continue;
			}
			Actor actor = null;
			int num = 0;
			foreach (Actor actor2 in pCity.units)
			{
				int num2 = 1;
				if (actor2.data.profession == UnitProfession.Unit)
				{
					if (actor2.data.favorite)
					{
						num2 += 2;
					}
					int num3 = ActorTool.attributeDice(actor2, num2);
					if (actor == null || num3 > num)
					{
						actor = actor2;
						num = num3;
					}
				}
			}
			if (actor != null)
			{
				City.makeLeader(actor, pCity);
			}
			return BehResult.Continue;
		}
	}
}
