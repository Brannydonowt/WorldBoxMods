using System;

namespace ai.behaviours
{
	// Token: 0x0200037F RID: 895
	public class BehRandomTeleport : BehaviourActionActor
	{
		// Token: 0x0600138E RID: 5006 RVA: 0x000A2E54 File Offset: 0x000A1054
		public override BehResult execute(Actor pActor)
		{
			if (pActor.data.health < pActor.curStats.health)
			{
				return BehResult.Stop;
			}
			if (!Toolbox.randomChance(0.3f))
			{
				return BehResult.Stop;
			}
			Spell spell = AssetManager.spells.get("teleportRandom");
			bool flag = false;
			foreach (WorldAction worldAction in spell.action)
			{
				flag = worldAction(pActor, pActor.currentTile);
			}
			if (flag)
			{
				pActor.doCastAnimation();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
