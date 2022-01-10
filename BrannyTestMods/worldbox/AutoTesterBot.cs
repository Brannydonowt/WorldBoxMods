using System;

// Token: 0x020001F6 RID: 502
public class AutoTesterBot : BaseMapObject
{
	// Token: 0x06000B5C RID: 2908 RVA: 0x0006E4F0 File Offset: 0x0006C6F0
	internal override void create()
	{
		base.create();
		this.ai = new AiSystemTester(this);
		this.ai.nextJobDelegate = new GetNextJobID(AssetManager.tester_jobs.getNextJob);
		DebugConfig.createTool("Auto Tester", 150, 0);
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x0006E52F File Offset: 0x0006C72F
	public override void update(float pElapsed)
	{
		if (!this.active)
		{
			return;
		}
		base.update(pElapsed);
		if (this.wait > 0f)
		{
			this.wait -= pElapsed;
			return;
		}
		this.ai.update();
	}

	// Token: 0x04000D98 RID: 3480
	public string debugString = "";

	// Token: 0x04000D99 RID: 3481
	public bool active;

	// Token: 0x04000D9A RID: 3482
	internal AiSystemTester ai;

	// Token: 0x04000D9B RID: 3483
	public float wait;
}
