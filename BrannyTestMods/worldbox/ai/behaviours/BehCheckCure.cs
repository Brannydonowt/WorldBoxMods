using System;

namespace ai.behaviours
{
	// Token: 0x02000341 RID: 833
	public class BehCheckCure : BehaviourActionActor
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x000A0134 File Offset: 0x0009E334
		public override BehResult execute(Actor pActor)
		{
			if (!Toolbox.randomChance(0.9f))
			{
				return BehResult.Stop;
			}
			if (!pActor.currentTile.Type.ground)
			{
				return BehResult.Stop;
			}
			MapBox.instance.getObjectsInChunks(pActor.currentTile, 0, MapObjectType.Actor);
			Actor actor = null;
			foreach (BaseSimObject baseSimObject in MapBox.instance.temp_map_objects)
			{
				Actor actor2 = (Actor)baseSimObject;
				if (ActorTool.canBeCuredFromTraits(actor2))
				{
					actor = actor2;
					break;
				}
			}
			if (actor == null)
			{
				return BehResult.Stop;
			}
			foreach (WorldAction worldAction in AssetManager.spells.get("cure").action)
			{
				worldAction(actor, actor.currentTile);
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}
	}
}
