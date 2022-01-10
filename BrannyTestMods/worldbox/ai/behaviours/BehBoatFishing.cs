using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x02000334 RID: 820
	public class BehBoatFishing : BehaviourActionActor
	{
		// Token: 0x060012CD RID: 4813 RVA: 0x0009F9D4 File Offset: 0x0009DBD4
		public override BehResult execute(Actor pActor)
		{
			this.spawnFishnet(pActor);
			return BehResult.Continue;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0009F9E0 File Offset: 0x0009DBE0
		public void spawnFishnet(Actor pActor)
		{
			if (BehaviourActionBase<Actor>.world.qualityChanger.lowRes)
			{
				return;
			}
			Vector2 vector = Toolbox.randomPointOnCircle(3, 4);
			WorldTile tile = BehaviourActionBase<Actor>.world.GetTile(pActor.currentTile.pos.x + (int)vector.x, pActor.currentTile.pos.y + (int)vector.y);
			if (tile == null)
			{
				return;
			}
			if (!tile.Type.ocean)
			{
				return;
			}
			BehaviourActionBase<Actor>.world.stackEffects.get("fx_fishnet").spawnAt(tile, pActor.stats.baseStats.scale);
		}
	}
}
