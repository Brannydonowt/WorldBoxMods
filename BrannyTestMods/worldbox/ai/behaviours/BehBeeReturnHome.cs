using System;

namespace ai.behaviours
{
	// Token: 0x02000328 RID: 808
	public class BehBeeReturnHome : BehaviourActionActor
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x0009F64C File Offset: 0x0009D84C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.homeBuilding == null && !pActor.homeBuilding.data.alive)
			{
				pActor.setHomeBuilding(null);
				return BehResult.Stop;
			}
			Bee component = pActor.GetComponent<Bee>();
			if (Toolbox.DistTile(pActor.currentTile, pActor.homeBuilding.currentTile) > 3f)
			{
				return BehResult.Stop;
			}
			if (component.pollen == 3 && pActor.currentTile.building == pActor.homeBuilding)
			{
				component.pollen = 0;
				pActor.homeBuilding.GetComponent<Beehive>().addHoney();
				pActor.timer_action = 3f;
			}
			return BehResult.Continue;
		}
	}
}
