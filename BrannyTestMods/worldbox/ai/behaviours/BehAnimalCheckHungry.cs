using System;

namespace ai.behaviours
{
	// Token: 0x02000321 RID: 801
	public class BehAnimalCheckHungry : BehaviourActionActor
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x0009F36C File Offset: 0x0009D56C
		public override BehResult execute(Actor pActor)
		{
			if ((float)pActor.data.hunger > 50f)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
