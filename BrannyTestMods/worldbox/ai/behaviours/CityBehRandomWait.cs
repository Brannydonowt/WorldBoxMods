using System;

namespace ai.behaviours
{
	// Token: 0x020003A7 RID: 935
	public class CityBehRandomWait : BehaviourActionCity
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x000A8FB1 File Offset: 0x000A71B1
		public CityBehRandomWait(float pMin = 0f, float pMax = 1f)
		{
			this.min = pMin;
			this.max = pMax;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x000A8FC7 File Offset: 0x000A71C7
		public override BehResult execute(City pCity)
		{
			pCity.timer_action = Toolbox.randomFloat(this.min, this.max);
			return BehResult.Continue;
		}

		// Token: 0x0400156C RID: 5484
		private float min;

		// Token: 0x0400156D RID: 5485
		private float max;
	}
}
