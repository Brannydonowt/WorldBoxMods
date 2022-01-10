using System;

// Token: 0x020000E7 RID: 231
public class WorldBehaviourActionCreepBiomass
{
	// Token: 0x060004DE RID: 1246 RVA: 0x000401D8 File Offset: 0x0003E3D8
	public static void updateBiomassTiles()
	{
		int num = 0;
		while (num < 3 && WorldBehaviourActionCreepBiomass.tryActionOn(TopTileLibrary.biomass_low))
		{
			num++;
		}
		int num2 = 0;
		while (num2 < 3 && WorldBehaviourActionCreepBiomass.tryActionOn(TopTileLibrary.biomass_high))
		{
			num2++;
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00040218 File Offset: 0x0003E418
	private static bool tryActionOn(TopTileType pType)
	{
		if (pType.hashset.Count < 10)
		{
			return false;
		}
		WorldTile random = pType.getCurrentTiles().GetRandom<WorldTile>();
		MapBox.instance.redrawRenderedTile(random);
		return true;
	}
}
