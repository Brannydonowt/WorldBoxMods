using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E0 RID: 736
public class IconRotationAnimation : MonoBehaviour
{
	// Token: 0x06000FCC RID: 4044 RVA: 0x0008C2AC File Offset: 0x0008A4AC
	private void Awake()
	{
		this.initScale = base.transform.localScale;
		this.scaleTo = this.initScale * 1.1f;
		if (this.randomDelay)
		{
			this.delay = Toolbox.randomFloat(1f, 10f);
		}
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x0008C2FD File Offset: 0x0008A4FD
	private void checkDestroyTween()
	{
		if (this.curTween != null && this.curTween.active)
		{
			TweenExtensions.Kill(this.curTween, false);
		}
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x0008C320 File Offset: 0x0008A520
	private void OnDestroy()
	{
		this.checkDestroyTween();
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x0008C328 File Offset: 0x0008A528
	private void rotate1()
	{
		this.curTween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, this.scaleTo, 0.3f), this.delay), 28), new TweenCallback(this.rotate2));
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x0008C374 File Offset: 0x0008A574
	private void rotate2()
	{
		if (this.randomDelay)
		{
			this.delay = Toolbox.randomFloat(1f, 10f);
		}
		this.curTween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, this.initScale, 0.3f), 0f), 28), new TweenCallback(this.rotate1));
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x0008C3DC File Offset: 0x0008A5DC
	private void OnEnable()
	{
		this.checkDestroyTween();
		this.rotate1();
	}

	// Token: 0x040012E1 RID: 4833
	public Image image;

	// Token: 0x040012E2 RID: 4834
	public float delay = 5f;

	// Token: 0x040012E3 RID: 4835
	public bool randomDelay;

	// Token: 0x040012E4 RID: 4836
	private Vector3 initScale;

	// Token: 0x040012E5 RID: 4837
	private Vector3 scaleTo;

	// Token: 0x040012E6 RID: 4838
	internal Tweener curTween;
}
