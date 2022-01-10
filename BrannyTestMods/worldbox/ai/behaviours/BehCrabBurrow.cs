using System;

namespace ai.behaviours
{
	// Token: 0x02000358 RID: 856
	public class BehCrabBurrow : BehaviourActionActor
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x000A0E74 File Offset: 0x0009F074
		public override void create()
		{
			base.create();
			this.forceAnimation = "burrow";
			this.special_inside_object = true;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000A0E90 File Offset: 0x0009F090
		public override BehResult execute(Actor pActor)
		{
			if ((float)pActor.data.hunger < 50f)
			{
				pActor.ai.setJob(null);
				return BehResult.Stop;
			}
			Toolbox.findNotSameRaceInChunkAround(pActor.currentTile.chunk, pActor.race.id);
			if (Toolbox.temp_list_units.Count == 0)
			{
				pActor.ai.setJob(null);
				return BehResult.Stop;
			}
			pActor.timer_action = Toolbox.randomFloat(10f, 20f);
			return BehResult.RepeatStep;
		}
	}
}
