using System;

namespace ai.behaviours
{
	// Token: 0x0200037E RID: 894
	public class BehRandomSwim : BehaviourActionActor
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
		public override BehResult execute(Actor pActor)
		{
			BehaviourActionActor.possible_moves.Clear();
			foreach (WorldTile worldTile in pActor.currentTile.neighboursAll)
			{
				if (worldTile.Type.liquid)
				{
					BehaviourActionActor.possible_moves.Add(worldTile);
				}
			}
			if (BehaviourActionActor.possible_moves.Count > 0)
			{
				pActor.moveTo(Toolbox.getRandom<WorldTile>(BehaviourActionActor.possible_moves));
			}
			return BehResult.Continue;
		}
	}
}
