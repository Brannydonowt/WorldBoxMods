using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class HeatRayEffect : BaseAnimatedObject
{
	// Token: 0x060005FC RID: 1532 RVA: 0x000479AC File Offset: 0x00045BAC
	private new void Awake()
	{
		this.ray.transform.localScale = new Vector3(1f, 0f, 1f);
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x000479D4 File Offset: 0x00045BD4
	private void Update()
	{
		this.update(this.world.getCurElapsed());
		this.ray.update(this.world.getCurElapsed());
		this.heat.update(this.world.getCurElapsed());
		if (this.ticksActive > 0)
		{
			this.ticksActive--;
			return;
		}
		this.active = false;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00047A3D File Offset: 0x00045C3D
	internal bool isReady()
	{
		return this.touchedGround;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00047A48 File Offset: 0x00045C48
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		Vector3 position = this.ray.transform.position;
		position.z = base.transform.position.y;
		this.ray.transform.position = position;
		position = this.heat.transform.position;
		position.z = base.transform.position.y;
		this.heat.transform.position = position;
		if (this.active)
		{
			if (this.rayScaleY < 2000f)
			{
				this.rayScaleY += pElapsed * 7000f;
				if (this.rayScaleY >= 2000f)
				{
					this.rayScaleY = 2000f;
					this.touchedGround = true;
				}
				this.ray.transform.localScale = new Vector3(this.rayWidth, this.rayScaleY, 1f);
			}
		}
		else
		{
			this.touchedGround = false;
			if (this.rayScaleY > 0f)
			{
				this.rayScaleY -= pElapsed * 4000f;
				if (this.rayScaleY < 0f)
				{
					this.rayScaleY = 0f;
					base.gameObject.SetActive(false);
				}
				this.ray.transform.localScale = new Vector3(this.rayWidth, this.rayScaleY, 1f);
			}
		}
		this.heat.gameObject.SetActive(this.touchedGround);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x00047BCC File Offset: 0x00045DCC
	internal void play(Vector2 pPos, int pSize)
	{
		if (pSize >= 10)
		{
			this.rayWidth = 1f;
		}
		else
		{
			this.rayWidth = 0.4f;
		}
		base.transform.localPosition = new Vector3(pPos.x, pPos.y);
		this.active = true;
		this.ticksActive = 4;
		base.gameObject.SetActive(true);
	}

	// Token: 0x040007D9 RID: 2009
	public SpriteAnimation ray;

	// Token: 0x040007DA RID: 2010
	public SpriteAnimation heat;

	// Token: 0x040007DB RID: 2011
	private bool active;

	// Token: 0x040007DC RID: 2012
	private int ticksActive;

	// Token: 0x040007DD RID: 2013
	private bool touchedGround;

	// Token: 0x040007DE RID: 2014
	private float rayScaleY;

	// Token: 0x040007DF RID: 2015
	private float rayWidth = 1f;
}
