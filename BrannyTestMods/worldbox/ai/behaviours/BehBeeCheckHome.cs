using System;

namespace ai.behaviours
{
	// Token: 0x02000325 RID: 805
	public class BehBeeCheckHome : BehaviourActionActor
	{
		// Token: 0x060012AB RID: 4779 RVA: 0x0009F51C File Offset: 0x0009D71C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.homeBuilding != null && !pActor.homeBuilding.data.alive)
			{
				pActor.setHomeBuilding(null);
			}
			if (pActor.homeBuilding == null)
			{
				if (pActor.currentTile.Type.grow_type_selector_plants != null)
				{
					MapBox.instance.tryGrowVegetationRandom(pActor.currentTile, VegetationType.Plants, false, true);
				}
				pActor.killHimself(true, AttackType.None, false, true);
			}
			return BehResult.Continue;
		}
	}
}
