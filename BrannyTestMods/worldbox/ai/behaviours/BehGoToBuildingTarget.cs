using System;

namespace ai.behaviours
{
	// Token: 0x02000372 RID: 882
	public class BehGoToBuildingTarget : BehaviourActionActor
	{
		// Token: 0x0600136A RID: 4970 RVA: 0x000A268C File Offset: 0x000A088C
		public BehGoToBuildingTarget(bool pPathOnWater = false)
		{
			this.pathOnWater = pPathOnWater;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000A269B File Offset: 0x000A089B
		public override void create()
		{
			base.create();
			this.null_check_building_target = true;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000A26AA File Offset: 0x000A08AA
		public override BehResult execute(Actor pActor)
		{
			this.goToBuilding(pActor);
			return BehResult.Continue;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000A26B4 File Offset: 0x000A08B4
		internal void goToBuilding(Actor pActor)
		{
			WorldTile currentTile = pActor.beh_building_target.currentTile;
			pActor.goTo(currentTile, this.pathOnWater, false);
		}

		// Token: 0x04001532 RID: 5426
		private bool pathOnWater;
	}
}
