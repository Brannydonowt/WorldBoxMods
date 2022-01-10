using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class Smoke : BaseEffect
{
	// Token: 0x0600062F RID: 1583 RVA: 0x00049634 File Offset: 0x00047834
	private void Update()
	{
		if (this.timer_scale > 0f)
		{
			this.timer_scale -= this.world.elapsed;
			return;
		}
		this.timer_scale = 0.01f;
		base.setAlpha(this.alpha - 0.01f);
		if (this.alpha <= 0f)
		{
			this.controller.killObject(this);
			return;
		}
		if (base.transform.localScale.x < 4f)
		{
			base.transform.localScale = new Vector3(base.transform.localScale.x + 0.03f, base.transform.localScale.y + 0.03f);
		}
		base.transform.localPosition = new Vector3(base.transform.localPosition.x + this.world.wind_direction.x * 0.5f, base.transform.localPosition.y + this.world.wind_direction.y * 0.5f);
	}

	// Token: 0x04000814 RID: 2068
	private float timer_scale;
}
