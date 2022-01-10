using System;
using ai.behaviours;

// Token: 0x020001F4 RID: 500
public class TesterBehSuperDamageToUnits : BehaviourActionTester
{
	// Token: 0x06000B58 RID: 2904 RVA: 0x0006E454 File Offset: 0x0006C654
	public override BehResult execute(AutoTesterBot pObject)
	{
		foreach (Actor actor in BehaviourActionBase<AutoTesterBot>.world.units)
		{
			if (actor.stats.canBeKilledByStuff)
			{
				actor.getHit(1E+17f, true, AttackType.Other, null, true);
			}
		}
		return base.execute(pObject);
	}
}
