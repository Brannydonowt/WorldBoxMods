using System;

// Token: 0x020000E9 RID: 233
public static class WorldBehaviourActionEnchanted
{
	// Token: 0x060004E9 RID: 1257 RVA: 0x00040599 File Offset: 0x0003E799
	public static void updateSparksAction()
	{
		WorldBehaviourActionEnchanted.tryActionOn(TopTileLibrary.enchanted_high);
		WorldBehaviourActionEnchanted.tryActionOn(TopTileLibrary.enchanted_low);
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x000405B0 File Offset: 0x0003E7B0
	private static void tryActionOn(TopTileType pType)
	{
		if (pType.hashset.Count < 10)
		{
			return;
		}
		WorldTile random = pType.getCurrentTiles().GetRandom<WorldTile>();
		MapBox.instance.stackEffects.get("fx_enchanted_sparkle").spawnAt(random.posV3, 0.25f);
	}
}
