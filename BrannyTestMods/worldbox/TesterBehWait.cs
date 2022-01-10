using System;
using ai.behaviours;

// Token: 0x020001F5 RID: 501
public class TesterBehWait : BehaviourActionTester
{
	// Token: 0x06000B5A RID: 2906 RVA: 0x0006E4CC File Offset: 0x0006C6CC
	public TesterBehWait(float pWait)
	{
		this.wait = pWait;
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x0006E4DB File Offset: 0x0006C6DB
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.wait = this.wait;
		return base.execute(pObject);
	}

	// Token: 0x04000D97 RID: 3479
	private float wait;
}
