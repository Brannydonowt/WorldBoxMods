using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000307 RID: 775
	public class AStarParam : ParamBase
	{
		// Token: 0x060011EE RID: 4590 RVA: 0x0009C238 File Offset: 0x0009A438
		public void resetParam()
		{
			this.swamp = false;
			this.roads = false;
			this.ocean = false;
			this.lava = false;
			this.ground = false;
			this.useGlobalPathLock = false;
			this.boat = false;
			this.limit = false;
			this.endToStartPath = false;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0009C284 File Offset: 0x0009A484
		internal override void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null)
		{
		}

		// Token: 0x040014D6 RID: 5334
		internal MapBox world;

		// Token: 0x040014D7 RID: 5335
		public float Weight;

		// Token: 0x040014D8 RID: 5336
		internal int maxOpenList = -1;

		// Token: 0x040014D9 RID: 5337
		internal bool roads;

		// Token: 0x040014DA RID: 5338
		internal bool useGlobalPathLock;

		// Token: 0x040014DB RID: 5339
		internal bool boat;

		// Token: 0x040014DC RID: 5340
		internal bool limit;

		// Token: 0x040014DD RID: 5341
		internal bool swamp;

		// Token: 0x040014DE RID: 5342
		internal bool ocean;

		// Token: 0x040014DF RID: 5343
		internal bool lava;

		// Token: 0x040014E0 RID: 5344
		internal bool block;

		// Token: 0x040014E1 RID: 5345
		internal bool ground;

		// Token: 0x040014E2 RID: 5346
		internal bool endToStartPath;
	}
}
