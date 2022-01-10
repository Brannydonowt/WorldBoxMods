using System;
using System.Collections.Generic;
using EpPathFinding.cs;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class PathfinderTools
{
	// Token: 0x06000960 RID: 2400 RVA: 0x00063790 File Offset: 0x00061990
	public static List<WorldTile> raycast(WorldTile pFrom, WorldTile pTarget)
	{
		PathfinderTools.raycastResult.Clear();
		float num = Toolbox.DistTile(pFrom, pTarget) * 0.99f;
		PathfinderTools.a.Set(pFrom.posV3.x, pFrom.posV3.y, 0f);
		PathfinderTools.b.Set(pTarget.posV3.x, pTarget.posV3.y, 0f);
		WorldTile worldTile = null;
		int num2 = 0;
		while ((float)num2 <= num)
		{
			Vector3 vector = Vector3.Lerp(PathfinderTools.a, PathfinderTools.b, (float)num2 / num);
			int x = Mathf.FloorToInt(vector.x);
			int y = Mathf.FloorToInt(vector.y);
			WorldTile tile = MapBox.instance.GetTile(x, y);
			if (tile != null && (worldTile == null || tile != worldTile))
			{
				PathfinderTools.raycastResult.Add(tile);
				worldTile = tile;
			}
			num2++;
		}
		if (worldTile != pTarget)
		{
			PathfinderTools.raycastResult.Add(pTarget);
		}
		return PathfinderTools.raycastResult;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00063878 File Offset: 0x00061A78
	public static bool tryToGetSimplePath(WorldTile pFrom, WorldTile pTargetTile, List<WorldTile> pPathToFill, ActorStats pStats, AStarParam pParam)
	{
		PathfinderTools.raycastResult.Clear();
		WorldTile worldTile = pFrom;
		float num = 0f;
		WorldTile worldTile2 = null;
		for (;;)
		{
			for (int i = 0; i < worldTile.neighboursAll.Count; i++)
			{
				WorldTile worldTile3 = worldTile.neighboursAll[i];
				float num2 = Toolbox.DistTile(worldTile3, pTargetTile);
				if (worldTile2 == null || num2 < num)
				{
					worldTile2 = worldTile3;
					num = num2;
				}
			}
			if (worldTile2 == null || worldTile2 == worldTile)
			{
				goto IL_108;
			}
			worldTile = worldTile2;
			if (!pParam.block && worldTile.Type.block)
			{
				break;
			}
			if (!pParam.lava && worldTile.Type.lava)
			{
				return false;
			}
			if (!pParam.ocean && worldTile.Type.ocean)
			{
				return false;
			}
			if (!pParam.swamp && worldTile.Type.swamp)
			{
				return false;
			}
			if (!pParam.ground && worldTile.Type.ground)
			{
				return false;
			}
			if (pParam.boat && !worldTile.isGoodForBoat())
			{
				return false;
			}
			PathfinderTools.raycastResult.Add(worldTile2);
			if (worldTile2 == pTargetTile)
			{
				goto IL_108;
			}
			worldTile2 = null;
		}
		return false;
		IL_108:
		pPathToFill.AddRange(PathfinderTools.raycastResult);
		return true;
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0006399C File Offset: 0x00061B9C
	public static WorldTile raycastTileForUnitToEmbark(WorldTile pFromGround, WorldTile pTargetOcean)
	{
		List<WorldTile> list = PathfinderTools.raycast(pTargetOcean, pFromGround);
		WorldTile result = null;
		TileIsland island = pFromGround.region.island;
		for (int i = 0; i < list.Count; i++)
		{
			WorldTile worldTile = list[i];
			if (worldTile.region.island == island)
			{
				result = worldTile;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x000639F0 File Offset: 0x00061BF0
	public static WorldTile raycastTileForUnitLandingFromOcean(WorldTile pFromOcean, WorldTile pTargetGround)
	{
		List<WorldTile> list = PathfinderTools.raycast(pFromOcean, pTargetGround);
		WorldTile result = null;
		TileIsland island = pTargetGround.region.island;
		for (int i = 0; i < list.Count; i++)
		{
			WorldTile worldTile = list[i];
			if (worldTile.region.island == island)
			{
				result = worldTile;
				break;
			}
		}
		return result;
	}

	// Token: 0x04000BFB RID: 3067
	private static List<WorldTile> raycastResult = new List<WorldTile>();

	// Token: 0x04000BFC RID: 3068
	private static Vector3 a = default(Vector3);

	// Token: 0x04000BFD RID: 3069
	private static Vector3 b = default(Vector3);
}
