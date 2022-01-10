using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class CornerAye : MonoBehaviour
{
	// Token: 0x06000E37 RID: 3639 RVA: 0x0008529B File Offset: 0x0008349B
	private void Awake()
	{
		this._rect = this.sprite.GetComponent<RectTransform>();
		this.reset();
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x000852B4 File Offset: 0x000834B4
	private void reset()
	{
		this._rect.anchoredPosition = new Vector2(100f, 0f);
		ShortcutExtensions.DOKill(this.sprite.transform, false);
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x000852E2 File Offset: 0x000834E2
	private void Start()
	{
		CornerAye.instance = this;
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x000852EC File Offset: 0x000834EC
	public void startAye()
	{
		this.reset();
		float num = 0.3f;
		Vector3 vector = default(Vector3);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.sprite.transform, vector, num, false), 27).onComplete = new TweenCallback(this.moveBack);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x00085338 File Offset: 0x00083538
	private void moveBack()
	{
		Vector3 vector = new Vector3(100f, 0f);
		float num = 0.3f;
		TweenSettingsExtensions.SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.sprite.transform, vector, num, false), 7), 0.1f);
	}

	// Token: 0x04001112 RID: 4370
	public static CornerAye instance;

	// Token: 0x04001113 RID: 4371
	public Transform sprite;

	// Token: 0x04001114 RID: 4372
	private RectTransform _rect;
}
