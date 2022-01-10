using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200038B RID: 907
	public class BehSpawnGrassSeeds : BehaviourActionActor
	{
		// Token: 0x060013A7 RID: 5031 RVA: 0x000A3270 File Offset: 0x000A1470
		public override BehResult execute(Actor pActor)
		{
			if (!Toolbox.randomChance(0.3f))
			{
				return BehResult.Stop;
			}
			if (!pActor.currentTile.Type.ground)
			{
				return BehResult.Stop;
			}
			Spell spell = AssetManager.spells.get("spawnGrassSeeds");
			BehSpawnGrassSeeds._tiles.Clear();
			foreach (WorldTile worldTile in pActor.currentTile.region.tiles)
			{
				if (!(worldTile.Type.biome == "grass") && worldTile.Type.canGrowBiomeGrass)
				{
					BehSpawnGrassSeeds._tiles.Add(worldTile);
				}
			}
			if (BehSpawnGrassSeeds._tiles.Count == 0)
			{
				return BehResult.Stop;
			}
			foreach (WorldAction worldAction in spell.action)
			{
				worldAction(pActor, BehSpawnGrassSeeds._tiles.GetRandom<WorldTile>());
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}

		// Token: 0x04001543 RID: 5443
		private static List<WorldTile> _tiles = new List<WorldTile>();
	}
}
