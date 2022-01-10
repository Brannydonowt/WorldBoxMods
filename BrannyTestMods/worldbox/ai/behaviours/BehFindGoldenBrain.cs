using System;

namespace ai.behaviours
{
	// Token: 0x02000361 RID: 865
	public class BehFindGoldenBrain : BehaviourActionActor
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x000A1504 File Offset: 0x0009F704
		public override BehResult execute(Actor pActor)
		{
			if (BehaviourActionBase<Actor>.world.worldLaws.world_law_peaceful_monsters.boolVal)
			{
				return BehResult.Stop;
			}
			Building building = null;
			float num = 0f;
			foreach (Building building2 in BehaviourActionBase<Actor>.world.kingdoms.getKingdomByID("goldenBrain").buildings)
			{
				if (building2.currentTile.isSameIsland(pActor.currentTile))
				{
					float num2 = Toolbox.DistTile(building2.currentTile, pActor.currentTile);
					if (building == null || num2 < num)
					{
						building = building2;
						num = num2;
					}
				}
			}
			if (building == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target = building;
			if (building != null)
			{
				pActor.goTo(pActor.beh_building_target.currentTile, false, false);
			}
			return BehResult.Continue;
		}
	}
}
