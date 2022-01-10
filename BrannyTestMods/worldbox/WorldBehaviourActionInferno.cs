using System;

// Token: 0x020000ED RID: 237
public static class WorldBehaviourActionInferno
{
	// Token: 0x060004F4 RID: 1268 RVA: 0x00040D7C File Offset: 0x0003EF7C
	public static void updateInfernalLowAnimations()
	{
		if (TopTileLibrary.infernal_low.hashset.Count < 10)
		{
			return;
		}
		if (!Toolbox.randomChance(0.4f))
		{
			return;
		}
		WorldTile random = TopTileLibrary.infernal_low.getCurrentTiles().GetRandom<WorldTile>();
		MapBox.instance.redrawRenderedTile(random);
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00040DC5 File Offset: 0x0003EFC5
	public static void updateInfernoFireAction()
	{
		WorldBehaviourActionInferno.tryFireAction(TopTileLibrary.infernal_high);
		WorldBehaviourActionInferno.tryFireAction(TopTileLibrary.infernal_low);
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00040DDC File Offset: 0x0003EFDC
	private static void tryFireAction(TopTileType pType)
	{
		if (pType.hashset.Count < 10)
		{
			return;
		}
		if (!Toolbox.randomChance(0.1f))
		{
			return;
		}
		WorldTile random = pType.getCurrentTiles().GetRandom<WorldTile>();
		random.setFire(true);
		MapBox.instance.particlesFire.spawn(random.posV3);
	}
}
