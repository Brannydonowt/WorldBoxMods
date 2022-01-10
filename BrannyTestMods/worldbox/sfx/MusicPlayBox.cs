using System;
using System.Collections.Generic;

namespace sfx
{
	// Token: 0x02000303 RID: 771
	public class MusicPlayBox
	{
		// Token: 0x060011D8 RID: 4568 RVA: 0x0009B447 File Offset: 0x00099647
		public void play(MusicLayer pLayer)
		{
			pLayer.play(this.currentTime);
			this.active.Add(pLayer);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0009B462 File Offset: 0x00099662
		public void stop(MusicLayer pLayer)
		{
			pLayer.stop();
			this.active.Remove(pLayer);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0009B477 File Offset: 0x00099677
		public void update()
		{
			if (this.active.Count > 0)
			{
				this.currentTime = this.active[0].s.time;
			}
		}

		// Token: 0x040014C4 RID: 5316
		public float currentTime;

		// Token: 0x040014C5 RID: 5317
		public List<MusicLayer> active = new List<MusicLayer>();
	}
}
