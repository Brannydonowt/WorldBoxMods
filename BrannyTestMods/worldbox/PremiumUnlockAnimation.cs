using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class PremiumUnlockAnimation : MonoBehaviour
{
	// Token: 0x06000D3D RID: 3389 RVA: 0x0007E114 File Offset: 0x0007C314
	private void Awake()
	{
		this.aye.transform.localScale = new Vector3(1f, 0f, 1f);
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0007E13C File Offset: 0x0007C33C
	private void Start()
	{
		this.canvasGroup = this.shineFX.GetComponent<CanvasGroup>();
		this.circleFX.SetActive(true);
		TweenSettingsExtensions.SetLoops<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.circleFX.transform, Vector3.one, 1f), -1, 1);
		TweenSettingsExtensions.SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.aye.transform, Vector3.one, 1f), 24), 0.5f);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0007E1B4 File Offset: 0x0007C3B4
	private void Update()
	{
		this.canvasGroup.alpha += Time.deltaTime / this.fadeDelay;
		this.shineFX.transform.Rotate(new Vector3(0f, 0f, 1f));
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0007E203 File Offset: 0x0007C403
	public void clickClose()
	{
		this.circleFX.gameObject.SetActive(false);
		this.shineFX.gameObject.SetActive(false);
	}

	// Token: 0x0400101E RID: 4126
	public float time;

	// Token: 0x0400101F RID: 4127
	public GameObject circleFX;

	// Token: 0x04001020 RID: 4128
	public GameObject shineFX;

	// Token: 0x04001021 RID: 4129
	public GameObject aye;

	// Token: 0x04001022 RID: 4130
	private CanvasGroup canvasGroup;

	// Token: 0x04001023 RID: 4131
	public float fadeDelay;

	// Token: 0x04001024 RID: 4132
	private int index;

	// Token: 0x04001025 RID: 4133
	public Vector3 scaleAdd;
}
