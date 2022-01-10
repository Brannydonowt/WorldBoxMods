using System;

namespace ai.behaviours
{
	// Token: 0x0200036B RID: 875
	public class BehFindTileForFarm : BehCity
	{
		// Token: 0x06001359 RID: 4953 RVA: 0x000A1FF0 File Offset: 0x000A01F0
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city.tasks.wheats > 0)
			{
				return BehResult.Stop;
			}
			pActor.city.zones.ShuffleOne<TileZone>();
			BehaviourActionActor.possible_moves.Clear();
			foreach (TileZone tileZone in pActor.city.zones)
			{
				foreach (TileTypeBase pType in tileZone.canBeFarms)
				{
					HashSetWorldTile tilesOfType = tileZone.getTilesOfType(pType);
					if (tilesOfType != null && tilesOfType.Count != 0)
					{
						foreach (WorldTile worldTile in tilesOfType)
						{
							if (!(worldTile.targetedBy != null) && pActor.currentTile.isSameIsland(worldTile) && worldTile.zone == tileZone && !(worldTile.building != null) && worldTile.IsTypeAround(TopTileLibrary.field))
							{
								BehaviourActionActor.possible_moves.Add(worldTile);
							}
						}
					}
				}
			}
			if (BehaviourActionActor.possible_moves.Count == 0)
			{
				foreach (TileZone tileZone2 in pActor.city.zones)
				{
					foreach (TileTypeBase pType2 in tileZone2.canBeFarms)
					{
						HashSetWorldTile tilesOfType = tileZone2.getTilesOfType(pType2);
						if (tilesOfType != null)
						{
							foreach (WorldTile worldTile2 in tilesOfType)
							{
								if (!(worldTile2.building != null) && !(worldTile2.targetedBy != null) && pActor.currentTile.isSameIsland(worldTile2))
								{
									BehaviourActionActor.possible_moves.Add(worldTile2);
								}
							}
						}
					}
				}
			}
			if (BehaviourActionActor.possible_moves.Count == 0)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = BehaviourActionActor.possible_moves.GetRandom<WorldTile>();
			return BehResult.Continue;
		}
	}
}
