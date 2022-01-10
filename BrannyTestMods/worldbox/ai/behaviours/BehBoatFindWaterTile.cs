using System;

namespace ai.behaviours
{
	// Token: 0x02000333 RID: 819
	public class BehBoatFindWaterTile : BehaviourActionActor
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x0009F9B0 File Offset: 0x0009DBB0
		public override BehResult execute(Actor pActor)
		{
			WorldTile randomTileForBoat = ActorTool.getRandomTileForBoat(pActor);
			pActor.beh_tile_target = randomTileForBoat;
			return BehResult.Continue;
		}
	}
}
