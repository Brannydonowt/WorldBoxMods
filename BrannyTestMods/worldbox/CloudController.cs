using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class CloudController : BaseEffectController
{
	// Token: 0x060005C4 RID: 1476 RVA: 0x00046268 File Offset: 0x00044468
	internal override void create()
	{
		base.create();
		this.timer_interval = 15f;
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0004627C File Offset: 0x0004447C
	public Cloud getNext()
	{
		Cloud cloud = (Cloud)base.GetObject();
		if (!cloud.created)
		{
			cloud.create();
		}
		return cloud;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x000462A4 File Offset: 0x000444A4
	internal void spawnCloud(Vector3 pVec, string pType)
	{
		this.getNext().prepare(pVec, pType);
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x000462B3 File Offset: 0x000444B3
	public override void spawn()
	{
		Cloud next = this.getNext();
		next.setType("normal");
		next.prepare();
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x000462CC File Offset: 0x000444CC
	internal void checkTile(WorldTile tTile, int pRadius)
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			BaseEffect baseEffect = this.list[i];
			if (Toolbox.Dist(baseEffect.transform.position.x, baseEffect.transform.position.y, (float)tTile.x, (float)tTile.y) <= (float)pRadius)
			{
				baseEffect.startToDie();
				return;
			}
		}
	}
}
