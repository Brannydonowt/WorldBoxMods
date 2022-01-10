using System;

// Token: 0x020001F7 RID: 503
public class AiSystemTester : AiSystem<AutoTesterBot, JobTesterAsset, BehaviourTaskTester, BehaviourActionTester, BehaviourTesterCondition>
{
	// Token: 0x06000B5F RID: 2911 RVA: 0x0006E57B File Offset: 0x0006C77B
	public AiSystemTester(AutoTesterBot pObject) : base(pObject)
	{
		this.jobs_library = AssetManager.tester_jobs;
		this.task_library = AssetManager.tester_tasks;
	}
}
