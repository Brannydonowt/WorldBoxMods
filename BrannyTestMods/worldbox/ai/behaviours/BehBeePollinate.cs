using System;

namespace ai.behaviours
{
	// Token: 0x02000327 RID: 807
	public class BehBeePollinate : BehaviourActionActor
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x0009F5BD File Offset: 0x0009D7BD
		public override void create()
		{
			base.create();
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0009F5C8 File Offset: 0x0009D7C8
		public override BehResult execute(Actor pActor)
		{
			Bee component = pActor.GetComponent<Bee>();
			if (pActor.currentTile.building == null)
			{
				return BehResult.Stop;
			}
			if (pActor.currentTile.building.stats.type == "flower")
			{
				component.pollen++;
				pActor.currentTile.pollinate();
			}
			pActor.timer_action = Toolbox.randomFloat(4f, 10f);
			return BehResult.Continue;
		}
	}
}
