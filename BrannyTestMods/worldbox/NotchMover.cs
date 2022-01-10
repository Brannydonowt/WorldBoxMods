using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class NotchMover : MonoBehaviour
{
	// Token: 0x06000D30 RID: 3376 RVA: 0x0007DE90 File Offset: 0x0007C090
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originalTopPosition = this.rectTransform.anchoredPosition.y;
		this._canvas = base.gameObject.transform.GetComponentInParent<Canvas>();
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0007DECC File Offset: 0x0007C0CC
	private void Update()
	{
		if ((float)Screen.height == Screen.safeArea.height)
		{
			return;
		}
		if (this._canvas == null)
		{
			return;
		}
		float num = ((float)Screen.height - Screen.safeArea.height) / this._canvas.scaleFactor;
		this.rectTransform.anchoredPosition = new Vector3(this.rectTransform.anchoredPosition.x, this.originalTopPosition - num);
	}

	// Token: 0x0400100B RID: 4107
	private float originalTopPosition;

	// Token: 0x0400100C RID: 4108
	private RectTransform rectTransform;

	// Token: 0x0400100D RID: 4109
	private Canvas _canvas;
}
