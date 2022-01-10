using System;

namespace ai.behaviours
{
	// Token: 0x02000387 RID: 903
	public class BehSetNextTask : BehaviourActionActor
	{
		// Token: 0x0600139F RID: 5023 RVA: 0x000A30F9 File Offset: 0x000A12F9
		public BehSetNextTask(string taskID, bool pClean = true)
		{
			this.task_id = taskID;
			this.clean = pClean;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000A310F File Offset: 0x000A130F
		public override BehResult execute(Actor pActor)
		{
			return base.forceTask(pActor, this.task_id, this.clean);
		}

		// Token: 0x04001541 RID: 5441
		private bool clean;

		// Token: 0x04001542 RID: 5442
		private string task_id;
	}
}
