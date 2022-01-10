using System;

namespace ai.behaviours
{
	// Token: 0x02000332 RID: 818
	public class BehBoatFindTileInDock : BehBoat
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x0009F948 File Offset: 0x0009DB48
		public override void create()
		{
			base.create();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0009F960 File Offset: 0x0009DB60
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_building_target.data.state != BuildingState.CivKingdom)
			{
				return BehResult.Stop;
			}
			WorldTile oceanTileInSameOcean = pActor.beh_building_target.GetComponent<Docks>().getOceanTileInSameOcean(pActor.currentTile);
			if (oceanTileInSameOcean == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = oceanTileInSameOcean;
			return BehResult.Continue;
		}
	}
}
