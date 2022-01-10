using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class SmokeController : BaseEffectController
{
	// Token: 0x06000631 RID: 1585 RVA: 0x00049758 File Offset: 0x00047958
	public override void spawn()
	{
		for (int i = 0; i < this.world.citiesList.Count; i++)
		{
			City city = this.world.citiesList[i];
			if (!(city == null))
			{
				this.spawnSmoke(city.getTile());
			}
		}
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x000497A7 File Offset: 0x000479A7
	public void spawnSmoke(Vector3 pVector, float pScale)
	{
		if (this.activeIndex > 300)
		{
			return;
		}
		base.GetObject().prepare(pVector, pScale);
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x000497C4 File Offset: 0x000479C4
	public void spawnSmoke(WorldTile pTile)
	{
		if (this.activeIndex > 300)
		{
			return;
		}
		base.GetObject().prepare(pTile, 0.5f);
	}
}
