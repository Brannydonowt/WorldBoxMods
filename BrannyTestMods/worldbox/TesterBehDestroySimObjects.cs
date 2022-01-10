using System;
using ai.behaviours;

// Token: 0x020001EC RID: 492
public class TesterBehDestroySimObjects : BehaviourActionTester
{
	// Token: 0x06000B44 RID: 2884 RVA: 0x0006D97C File Offset: 0x0006BB7C
	public override BehResult execute(AutoTesterBot pObject)
	{
		BehaviourActionBase<AutoTesterBot>.world.units.checkAddRemove();
		foreach (Actor actor in BehaviourActionBase<AutoTesterBot>.world.units)
		{
			actor.getHit(1E+10f, true, AttackType.Other, null, true);
		}
		BehaviourActionBase<AutoTesterBot>.world.buildings.checkAddRemove();
		foreach (Building building in BehaviourActionBase<AutoTesterBot>.world.buildings)
		{
			building.getHit(1E+10f, true, AttackType.Other, null, true);
		}
		return base.execute(pObject);
	}
}
