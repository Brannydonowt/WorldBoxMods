using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class BaseAnimatedObject : BaseMapObject
{
	// Token: 0x0600029E RID: 670 RVA: 0x0002D7B0 File Offset: 0x0002B9B0
	internal void Awake()
	{
		this.spriteAnimation = base.gameObject.GetComponent<SpriteAnimation>();
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0002D7C3 File Offset: 0x0002B9C3
	internal override void create()
	{
		base.create();
		this.spriteAnimation = base.gameObject.GetComponent<SpriteAnimation>();
		if (this.spriteAnimation != null)
		{
			this.spriteAnimation.create();
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0002D7F5 File Offset: 0x0002B9F5
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateSpriteAnimation(pElapsed);
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0002D805 File Offset: 0x0002BA05
	internal virtual void updateSpriteAnimation(float pElapsed)
	{
		if (this.spriteAnimation != null)
		{
			this.spriteAnimation.update(pElapsed);
		}
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0002D821 File Offset: 0x0002BA21
	internal virtual void updateShadow()
	{
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0002D823 File Offset: 0x0002BA23
	public void resetShadow()
	{
		this.lastShadowPos = Vector3.zero;
		this.lastShadowScale = Vector3.zero;
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0002D83C File Offset: 0x0002BA3C
	protected void checkShadowPosition(Vector3 pPosition)
	{
		if (this.lastShadowPos.x != pPosition.x || this.lastShadowPos.y != pPosition.y)
		{
			this.lastShadowPos.Set(pPosition.x, pPosition.y, 0f);
			this.shadow.m_transform.localPosition = this.lastShadowPos;
		}
		Vector3 localScale = this.m_transform.localScale;
		if (this.lastShadowScale.x != localScale.x || this.lastShadowScale.y != localScale.y)
		{
			this.lastShadowScale.Set(localScale.x, localScale.y, localScale.z);
			this.shadow.m_transform.localScale = this.lastShadowScale;
		}
	}

	// Token: 0x0400037F RID: 895
	internal SpriteAnimation spriteAnimation;

	// Token: 0x04000380 RID: 896
	internal MapObjectShadow shadow;

	// Token: 0x04000381 RID: 897
	internal Vector3 lastShadowPos;

	// Token: 0x04000382 RID: 898
	internal Vector3 lastShadowScale;
}
