using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000215 RID: 533
public class UiWindowStretch : EventTrigger
{
	// Token: 0x06000BDE RID: 3038 RVA: 0x00075FBA File Offset: 0x000741BA
	private void Start()
	{
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00075FBC File Offset: 0x000741BC
	public void Update()
	{
		if (this.dragging)
		{
			Vector3 b = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			this.newSize = this.posClicked - b;
			this.stretchTarget.sizeDelta = new Vector2(this.originSizeDelta.x - this.newSize.x, this.originSizeDelta.y + this.newSize.y);
		}
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00076040 File Offset: 0x00074240
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!this.dragging)
		{
			this.posClicked = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			this.originSizeDelta = this.stretchTarget.sizeDelta;
		}
		this.dragging = true;
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00076091 File Offset: 0x00074291
	public override void OnPointerUp(PointerEventData eventData)
	{
		this.dragging = false;
	}

	// Token: 0x04000E59 RID: 3673
	public RectTransform stretchTarget;

	// Token: 0x04000E5A RID: 3674
	private bool dragging;

	// Token: 0x04000E5B RID: 3675
	private Transform mainTransform;

	// Token: 0x04000E5C RID: 3676
	private Transform canvasContainer;

	// Token: 0x04000E5D RID: 3677
	public Vector3 posClicked;

	// Token: 0x04000E5E RID: 3678
	public Vector3 newSize;

	// Token: 0x04000E5F RID: 3679
	public Vector2 originSizeDelta;
}
