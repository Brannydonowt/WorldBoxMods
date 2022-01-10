using System;

namespace ai.behaviours
{
	// Token: 0x0200037D RID: 893
	public class BehPlantCrops : BehCity
	{
		// Token: 0x06001389 RID: 5001 RVA: 0x000A2D51 File Offset: 0x000A0F51
		public override void create()
		{
			base.create();
			this.null_check_tile_target = true;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000A2D60 File Offset: 0x000A0F60
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_tile_target.Type == TopTileLibrary.field && pActor.beh_tile_target.building == null)
			{
				BehaviourActionBase<Actor>.world.addBuilding("0wheat", pActor.beh_tile_target, null, false, false, BuildPlacingType.New);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
