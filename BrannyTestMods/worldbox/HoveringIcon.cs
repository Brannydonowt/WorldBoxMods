using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025A RID: 602
public class HoveringIcon : MonoBehaviour
{
	// Token: 0x06000D12 RID: 3346 RVA: 0x0007D468 File Offset: 0x0007B668
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
		this.originalPos = base.transform.localPosition;
		this.randomTimer = Toolbox.randomFloat(1f * this.timer_mod, 1.5f * this.timer_mod);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0007D4B8 File Offset: 0x0007B6B8
	private void OnEnable()
	{
		base.transform.localPosition = new Vector3(this.originalPos.x, this.originalPos.y = this.originalPos.y + Toolbox.randomFloat(this.min, this.max));
		if (Toolbox.randomBool())
		{
			this.moveStageOne();
			return;
		}
		this.moveStageTwo();
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0007D517 File Offset: 0x0007B717
	private void OnDisable()
	{
		DOTween.Kill(base.transform, false);
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0007D526 File Offset: 0x0007B726
	private void moveStageTwo()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(base.transform, this.originalPos, this.randomTimer, false), 7).onComplete = new TweenCallback(this.moveStageOne);
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0007D558 File Offset: 0x0007B758
	private void moveStageOne()
	{
		Vector3 vector = new Vector3(this.originalPos.x, this.originalPos.y, 1f);
		vector.y += 3f;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(base.transform, vector, this.randomTimer, false), 7).onComplete = new TweenCallback(this.moveStageTwo);
	}

	// Token: 0x04000FED RID: 4077
	private Vector3 originalPos;

	// Token: 0x04000FEE RID: 4078
	private float randomTimer;

	// Token: 0x04000FEF RID: 4079
	public Image image;

	// Token: 0x04000FF0 RID: 4080
	public float min = -2f;

	// Token: 0x04000FF1 RID: 4081
	public float max = 2f;

	// Token: 0x04000FF2 RID: 4082
	public float timer_mod = 1f;
}
