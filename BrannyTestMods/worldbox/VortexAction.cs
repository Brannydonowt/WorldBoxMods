using System;
using System.Collections.Generic;

// Token: 0x0200018E RID: 398
public static class VortexAction
{
	// Token: 0x06000930 RID: 2352 RVA: 0x000615F4 File Offset: 0x0005F7F4
	internal static void moveTiles(WorldTile pCenter, BrushData pBrush)
	{
		MapBox instance = MapBox.instance;
		VortexAction.clear();
		foreach (BrushPixelData brushPixelData in pBrush.pos)
		{
			WorldTile tile = instance.GetTile(pCenter.x + brushPixelData.x, pCenter.y + brushPixelData.y);
			if (tile != null)
			{
				instance.flashEffects.flashPixel(tile, 10, ColorType.White);
				if (!Toolbox.randomChance(0.8f))
				{
					WorldTile random = Toolbox.getRandom<WorldTile>(tile.neighbours);
					if (random.top_type != null)
					{
						MapAction.removeGreens(random);
					}
					if (random.Type.liquid)
					{
						MapAction.removeLiquid(random);
					}
					if (random != null)
					{
						VortexAction.newTiles.Add(new VortexSwitchHelper
						{
							tile = random,
							newType = tile.main_type,
							newTopType = tile.top_type
						});
					}
				}
			}
		}
		foreach (VortexSwitchHelper vortexSwitchHelper in VortexAction.newTiles)
		{
			MapAction.terraformTile(vortexSwitchHelper.tile, vortexSwitchHelper.newType, vortexSwitchHelper.newTopType, TerraformLibrary.destroy_no_flash);
			if (vortexSwitchHelper.tile.Type.lava)
			{
				instance.lavaLayer.loadLavaTile(vortexSwitchHelper.tile);
			}
		}
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x00061780 File Offset: 0x0005F980
	private static void clear()
	{
		VortexAction.newTiles.Clear();
	}

	// Token: 0x04000BC8 RID: 3016
	private static List<VortexSwitchHelper> newTiles = new List<VortexSwitchHelper>();
}
