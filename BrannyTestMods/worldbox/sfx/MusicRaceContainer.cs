using System;

namespace sfx
{
	// Token: 0x02000305 RID: 773
	public class MusicRaceContainer
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x0009B4BE File Offset: 0x000996BE
		public void clear()
		{
			this.buildings = 0;
			this.advancements = 0;
			this.kingdom_exists = false;
		}

		// Token: 0x040014C9 RID: 5321
		public int buildings;

		// Token: 0x040014CA RID: 5322
		public int advancements;

		// Token: 0x040014CB RID: 5323
		public bool kingdom_exists;
	}
}
