using System;

// Token: 0x020000EE RID: 238
public static class WorldBehaviourActionSwampAnimation
{
	// Token: 0x060004F7 RID: 1271 RVA: 0x00040E30 File Offset: 0x0003F030
	public static void updateSwampTiles()
	{
		if (TopTileLibrary.swamp_low.hashset.Count < 10)
		{
			return;
		}
		WorldTile random = TopTileLibrary.swamp_low.getCurrentTiles().GetRandom<WorldTile>();
		MapBox.instance.redrawRenderedTile(random);
	}
}
