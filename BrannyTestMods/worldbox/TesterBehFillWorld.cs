using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020001EE RID: 494
public class TesterBehFillWorld : BehaviourActionTester
{
	// Token: 0x06000B48 RID: 2888 RVA: 0x0006DA5F File Offset: 0x0006BC5F
	public TesterBehFillWorld(string pType)
	{
		this.type = pType;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0006DA70 File Offset: 0x0006BC70
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (TesterBehFillWorld.tiles.Count == 0)
		{
			foreach (TileType tileType in AssetManager.tiles.list)
			{
				TesterBehFillWorld.tiles.Add(tileType.id);
			}
		}
		string random = this.type;
		if (this.type == "random")
		{
			random = TesterBehFillWorld.tiles.GetRandom<string>();
		}
		TileType pType = AssetManager.tiles.get(random);
		for (int i = 0; i < 3; i++)
		{
			foreach (WorldTile pTile in BehaviourActionBase<AutoTesterBot>.world.mapChunkManager.list.GetRandom<MapChunk>().tiles)
			{
				MapAction.terraformMain(pTile, pType, TerraformLibrary.destroy_no_flash);
			}
		}
		return base.execute(pObject);
	}

	// Token: 0x04000D8D RID: 3469
	private static List<string> tiles = new List<string>();

	// Token: 0x04000D8E RID: 3470
	private string type;
}
