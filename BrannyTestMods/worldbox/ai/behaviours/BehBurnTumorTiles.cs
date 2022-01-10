using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200033D RID: 829
	public class BehBurnTumorTiles : BehaviourActionActor
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x0009FE30 File Offset: 0x0009E030
		public override BehResult execute(Actor pActor)
		{
			if (!Toolbox.randomChance(0.7f))
			{
				return BehResult.Stop;
			}
			if (!pActor.currentTile.Type.ground)
			{
				return BehResult.Stop;
			}
			WorldTile worldTile = null;
			BehBurnTumorTiles.tiles.Clear();
			this.checkRegion(pActor.currentTile.region);
			if (BehBurnTumorTiles.tiles.Count != 0)
			{
				worldTile = BehBurnTumorTiles.tiles.GetRandom<WorldTile>();
			}
			else
			{
				for (int i = 0; i < pActor.currentTile.region.neighbours.Count; i++)
				{
					MapRegion pRegion = pActor.currentTile.region.neighbours[i];
					this.checkRegion(pRegion);
					if (BehBurnTumorTiles.tiles.Count != 0)
					{
						worldTile = BehBurnTumorTiles.tiles.GetRandom<WorldTile>();
						break;
					}
				}
			}
			if (worldTile == null)
			{
				return BehResult.Stop;
			}
			Spell spell = AssetManager.spells.get("fire");
			for (int j = 0; j < spell.action.Count; j++)
			{
				spell.action[j](null, worldTile);
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0009FF38 File Offset: 0x0009E138
		public void checkRegion(MapRegion pRegion)
		{
			for (int i = 0; i < pRegion.tiles.Count; i++)
			{
				WorldTile worldTile = pRegion.tiles[i];
				if (worldTile.Type.creep)
				{
					BehBurnTumorTiles.tiles.Add(worldTile);
				}
			}
		}

		// Token: 0x04001526 RID: 5414
		private static List<WorldTile> tiles = new List<WorldTile>();
	}
}
