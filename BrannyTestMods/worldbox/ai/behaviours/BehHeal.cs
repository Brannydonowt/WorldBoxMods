using System;

namespace ai.behaviours
{
	// Token: 0x02000374 RID: 884
	public class BehHeal : BehaviourActionActor
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x000A271C File Offset: 0x000A091C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.data.health == pActor.curStats.health)
			{
				return BehResult.Stop;
			}
			foreach (WorldAction worldAction in AssetManager.spells.get("bloodRain").action)
			{
				worldAction(pActor, pActor.currentTile);
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}
	}
}
