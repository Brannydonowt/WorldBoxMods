using System;

namespace ai.behaviours
{
	// Token: 0x0200031F RID: 799
	public class BehActiveCrabDangerCheck : BehActive
	{
		// Token: 0x0600129B RID: 4763 RVA: 0x0009F1F7 File Offset: 0x0009D3F7
		public override void create()
		{
			base.create();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0009F200 File Offset: 0x0009D400
		public override BehResult execute(Actor pActor)
		{
			if ((float)pActor.data.hunger < 50f)
			{
				return BehResult.Continue;
			}
			Toolbox.findNotSameRaceInChunkAround(pActor.currentTile.chunk, pActor.race.id);
			if (Toolbox.temp_list_units.Count > 0)
			{
				pActor.cancelAllBeh(null);
				pActor.stopMovement();
				pActor.ai.setJob("crab_burrow");
			}
			return BehResult.Continue;
		}
	}
}
