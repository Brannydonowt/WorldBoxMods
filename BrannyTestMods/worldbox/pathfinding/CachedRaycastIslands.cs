using System;
using System.Collections.Generic;

namespace pathfinding
{
	// Token: 0x02000319 RID: 793
	public class CachedRaycastIslands
	{
		// Token: 0x06001284 RID: 4740 RVA: 0x0009E510 File Offset: 0x0009C710
		public static string debug()
		{
			return CachedRaycastIslands.hash.Count.ToString() ?? "";
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0009E538 File Offset: 0x0009C738
		public static void add(WorldTile pFrom, WorldTile pTargetTile, ActorStats pStats, bool pResult)
		{
			string id = CachedRaycastIslands.getID(pFrom, pTargetTile, pStats);
			if (pResult)
			{
				CachedRaycastIslands.hash[id] = RaycastResult.Possible;
				return;
			}
			CachedRaycastIslands.hash[id] = RaycastResult.NotPossible;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0009E56C File Offset: 0x0009C76C
		public static RaycastResult getResult(WorldTile pFrom, WorldTile pTargetTile, ActorStats pStats)
		{
			string id = CachedRaycastIslands.getID(pFrom, pTargetTile, pStats);
			if (CachedRaycastIslands.hash.ContainsKey(id))
			{
				return CachedRaycastIslands.hash[id];
			}
			return RaycastResult.NotCalculated;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0009E59C File Offset: 0x0009C79C
		private static string getID(WorldTile pFrom, WorldTile pTargetTile, ActorStats pStats)
		{
			return string.Concat(new string[]
			{
				pFrom.chunk.x.ToString(),
				"_",
				pFrom.chunk.y.ToString(),
				"_",
				pFrom.region.island.id.ToString(),
				"_",
				pTargetTile.region.island.id.ToString(),
				"_",
				pStats.dieInLava.ToString(),
				"_",
				pStats.damagedByOcean.ToString(),
				"_",
				pStats.isBoat.ToString()
			});
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0009E669 File Offset: 0x0009C869
		public static void clear()
		{
			CachedRaycastIslands.hash.Clear();
		}

		// Token: 0x04001515 RID: 5397
		private static Dictionary<string, RaycastResult> hash = new Dictionary<string, RaycastResult>();
	}
}
