using System;
using ai.behaviours;

// Token: 0x020001ED RID: 493
public class TesterBehEndJobTest : BehaviourActionTester
{
	// Token: 0x06000B46 RID: 2886 RVA: 0x0006DA48 File Offset: 0x0006BC48
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.ai.setJob(null);
		return BehResult.Continue;
	}
}
