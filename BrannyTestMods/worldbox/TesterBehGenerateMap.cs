using System;
using ai.behaviours;

// Token: 0x020001EF RID: 495
public class TesterBehGenerateMap : BehaviourActionTester
{
	// Token: 0x06000B4B RID: 2891 RVA: 0x0006DB88 File Offset: 0x0006BD88
	public override BehResult execute(AutoTesterBot pObject)
	{
		Config.customZoneX = 7;
		Config.customZoneY = 7;
		BehaviourActionBase<AutoTesterBot>.world.generateNewMap("custom");
		return base.execute(pObject);
	}
}
