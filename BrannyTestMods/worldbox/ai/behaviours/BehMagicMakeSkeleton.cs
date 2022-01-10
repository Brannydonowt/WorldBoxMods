using System;

namespace ai.behaviours
{
	// Token: 0x02000378 RID: 888
	public class BehMagicMakeSkeleton : BehaviourActionActor
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x000A2918 File Offset: 0x000A0B18
		public override BehResult execute(Actor pActor)
		{
			BehMagicMakeSkeleton.<>c__DisplayClass0_0 CS$<>8__locals1 = new BehMagicMakeSkeleton.<>c__DisplayClass0_0();
			Toolbox.findSameUnitInChunkAround(pActor.currentTile.chunk, "skeleton");
			if (Toolbox.temp_list_units.Count > 6)
			{
				return BehResult.Stop;
			}
			BehMagicMakeSkeleton.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
			WorldTile currentTile = pActor.currentTile;
			WorldTile tTile;
			if (currentTile == null)
			{
				tTile = null;
			}
			else
			{
				MapRegion region = currentTile.region;
				tTile = ((region != null) ? region.tiles.GetRandom<WorldTile>() : null);
			}
			CS$<>8__locals2.tTile = tTile;
			if (CS$<>8__locals1.tTile == null)
			{
				return BehResult.Stop;
			}
			if (CS$<>8__locals1.tTile.units.Count > 0)
			{
				return BehResult.Stop;
			}
			BaseEffect baseEffect = BehaviourActionBase<Actor>.world.stackEffects.get("fx_create_skeleton").spawnAt(CS$<>8__locals1.tTile.posV3, pActor.stats.baseStats.scale);
			pActor.doCastAnimation();
			baseEffect.setCallback(19, new BaseCallback(CS$<>8__locals1.<execute>g__spawnSkeleton|0));
			return BehResult.Continue;
		}
	}
}
