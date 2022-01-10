using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200020E RID: 526
public class DebugUiDrag : EventTrigger
{
	// Token: 0x06000BC1 RID: 3009 RVA: 0x000757DB File Offset: 0x000739DB
	private void Start()
	{
		this.mainTransform = base.transform.parent.parent;
		this.canvasContainer = this.mainTransform.parent;
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00075804 File Offset: 0x00073A04
	public void Update()
	{
		if (this.dragging)
		{
			Vector3 position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			this.mainTransform.SetParent(null, true);
			this.mainTransform.SetParent(this.canvasContainer, true);
			this.mainTransform.position = position;
		}
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00075863 File Offset: 0x00073A63
	public override void OnPointerDown(PointerEventData eventData)
	{
		this.dragging = true;
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0007586C File Offset: 0x00073A6C
	public override void OnPointerUp(PointerEventData eventData)
	{
		this.dragging = false;
	}

	// Token: 0x04000E3F RID: 3647
	private bool dragging;

	// Token: 0x04000E40 RID: 3648
	private Transform mainTransform;

	// Token: 0x04000E41 RID: 3649
	private Transform canvasContainer;
}
