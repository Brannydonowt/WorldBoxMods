using System;
using ai.behaviours;

// Token: 0x020001EB RID: 491
public class TesterBehCloseWindows : BehaviourActionTester
{
	// Token: 0x06000B42 RID: 2882 RVA: 0x0006D965 File Offset: 0x0006BB65
	public override BehResult execute(AutoTesterBot pObject)
	{
		ScrollWindow.hideAllEvent(true);
		return base.execute(pObject);
	}
}
