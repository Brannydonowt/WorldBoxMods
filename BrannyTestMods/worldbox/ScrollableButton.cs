using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200026A RID: 618
public class ScrollableButton : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IScrollHandler
{
	// Token: 0x06000DAA RID: 3498 RVA: 0x00080288 File Offset: 0x0007E488
	protected void Awake()
	{
		this.scrollRect = base.gameObject.GetComponentInParent<ScrollRect>();
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0008029B File Offset: 0x0007E49B
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.scrollRect.SendMessage("OnBeginDrag", eventData);
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x000802AE File Offset: 0x0007E4AE
	public void OnDrag(PointerEventData eventData)
	{
		this.scrollRect.SendMessage("OnDrag", eventData);
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000802C1 File Offset: 0x0007E4C1
	public void OnEndDrag(PointerEventData eventData)
	{
		this.scrollRect.SendMessage("OnEndDrag", eventData);
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x000802D4 File Offset: 0x0007E4D4
	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		this.scrollRect.SendMessage("OnInitializePotentialDrag", eventData);
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x000802E7 File Offset: 0x0007E4E7
	public void OnScroll(PointerEventData eventData)
	{
		this.scrollRect.SendMessage("OnScroll", eventData);
	}

	// Token: 0x04001065 RID: 4197
	private ScrollRect scrollRect;
}
