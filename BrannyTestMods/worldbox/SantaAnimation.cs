using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class SantaAnimation : BaseMapObject
{
	// Token: 0x0600078F RID: 1935 RVA: 0x00054FA8 File Offset: 0x000531A8
	internal override void create()
	{
		base.create();
		this.tStr = new Vector3(this.shakeX, this.shakeY);
		this.shakeTween = ShortcutExtensions.DOShakePosition(base.transform, 0.5f, this.tStr, 10, 90f, false, false);
		this.actor = base.transform.parent.GetComponent<Santa>();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0005501C File Offset: 0x0005321C
	private void Update()
	{
		if (this.actor.colorEffect > 0f)
		{
			this.spriteRenderer.sharedMaterial = this.actor.colorMaterial;
		}
		else
		{
			this.spriteRenderer.sharedMaterial = LibraryMaterials.instance.matWorldObjects;
		}
		if (this.world.isPaused())
		{
			return;
		}
		if (!this.shakeTween.active)
		{
			this.shakeTween = ShortcutExtensions.DOShakePosition(base.transform, 0.5f, this.tStr, 10, 90f, false, false);
		}
	}

	// Token: 0x04000A11 RID: 2577
	public float shakeX = 2f;

	// Token: 0x04000A12 RID: 2578
	public float shakeY = 0.3f;

	// Token: 0x04000A13 RID: 2579
	private Tween shakeTween;

	// Token: 0x04000A14 RID: 2580
	private Vector3 tStr;

	// Token: 0x04000A15 RID: 2581
	private Actor actor;

	// Token: 0x04000A16 RID: 2582
	private SpriteRenderer spriteRenderer;
}
