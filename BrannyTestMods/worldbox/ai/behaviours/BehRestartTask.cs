using System;

namespace ai.behaviours
{
	// Token: 0x02000385 RID: 901
	public class BehRestartTask : BehCity
	{
		// Token: 0x0600139D RID: 5021 RVA: 0x000A30EE File Offset: 0x000A12EE
		public override BehResult execute(Actor pActor)
		{
			return BehResult.RestartTask;
		}
	}
}
