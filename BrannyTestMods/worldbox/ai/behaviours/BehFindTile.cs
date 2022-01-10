using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000368 RID: 872
	public class BehFindTile : BehaviourActionActor
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x000A1BC8 File Offset: 0x0009FDC8
		public BehFindTile(string pType)
		{
			this.type = pType;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000A1BD8 File Offset: 0x0009FDD8
		public override BehResult execute(Actor pActor)
		{
			BehaviourActionActor.possible_moves.Clear();
			if (this.type == "new_road")
			{
				WorldTile roadTileToBuild = pActor.city.getRoadTileToBuild(pActor);
				if (roadTileToBuild != null)
				{
					BehaviourActionActor.possible_moves.Add(roadTileToBuild);
				}
			}
			else
			{
				this.findInChunks(pActor);
			}
			if (BehaviourActionActor.possible_moves.Count == 0)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = Toolbox.getRandom<WorldTile>(BehaviourActionActor.possible_moves);
			return BehResult.Continue;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000A1C44 File Offset: 0x0009FE44
		private void findInChunks(Actor pActor)
		{
			List<MapChunk> allChunksFromTile = Toolbox.getAllChunksFromTile(pActor.currentTile);
			for (int i = 0; i < allChunksFromTile.Count; i++)
			{
				MapChunk mapChunk = allChunksFromTile[i];
				for (int j = 0; j < mapChunk.regions.Count; j++)
				{
					MapRegion mapRegion = mapChunk.regions[j];
					int k = 0;
					while (k < mapRegion.tiles.Count)
					{
						WorldTile worldTile = mapRegion.tiles[k];
						string text = this.type;
						if (text == null)
						{
							goto IL_EF;
						}
						if (!(text == "sand"))
						{
							if (!(text == "water"))
							{
								if (!(text == "grass"))
								{
									goto IL_EF;
								}
								if (worldTile.isSameIsland(pActor.currentTile) && !(worldTile.targetedBy != null) && worldTile.Type.grass)
								{
									if (!(worldTile.building != null))
									{
										goto IL_FE;
									}
								}
							}
							else if (!(worldTile.targetedBy != null))
							{
								if (worldTile.Type.ocean)
								{
									goto IL_FE;
								}
							}
						}
						else if (worldTile.Type.sand)
						{
							goto IL_FE;
						}
						IL_10A:
						k++;
						continue;
						IL_EF:
						if (!worldTile.isSameIsland(pActor.currentTile))
						{
							goto IL_10A;
						}
						IL_FE:
						BehaviourActionActor.possible_moves.Add(worldTile);
						goto IL_10A;
					}
				}
			}
		}

		// Token: 0x0400152F RID: 5423
		internal string type;
	}
}
