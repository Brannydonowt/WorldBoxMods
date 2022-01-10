using System;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class StatusParticle : BaseEffect
{
	// Token: 0x06000653 RID: 1619 RVA: 0x0004A265 File Offset: 0x00048465
	public void spawnParticle(Vector3 pVector, Color pColor, float pScale = 0.25f)
	{
		base.prepare(pVector, pScale);
		base.GetComponent<SpriteRenderer>().color = pColor;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0004A27B File Offset: 0x0004847B
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		base.setScale(this.scale - pElapsed * 0.2f);
		if (this.scale <= 0f)
		{
			base.kill();
		}
	}
}
