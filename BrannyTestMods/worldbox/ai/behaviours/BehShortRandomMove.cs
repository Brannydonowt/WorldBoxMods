using System;

namespace ai.behaviours
{
	// Token: 0x02000388 RID: 904
	public class BehShortRandomMove : BehaviourActionActor
	{
		// Token: 0x060013A1 RID: 5025 RVA: 0x000A3124 File Offset: 0x000A1324
		public override BehResult execute(Actor pActor)
		{
			WorldTile worldTile = null;
			for (int i = 0; i < pActor.currentTile.neighboursAll.Count; i++)
			{
				pActor.currentTile.neighboursAll.ShuffleOne(i);
				WorldTile worldTile2 = pActor.currentTile.neighboursAll[i];
				if (worldTile2.Type.layerType == pActor.currentTile.Type.layerType)
				{
					worldTile = worldTile2;
					break;
				}
			}
			if (worldTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = worldTile;
			return BehResult.Continue;
		}
	}
}
