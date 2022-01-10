using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000299 RID: 665
public class SaveSlotWindow : MonoBehaviour
{
	// Token: 0x06000EAB RID: 3755 RVA: 0x0008802C File Offset: 0x0008622C
	private void checkChildren()
	{
		if (this.previews.Count > 0)
		{
			return;
		}
		int childCount = this.buttonsContainer.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			BoxPreview component = this.buttonsContainer.transform.GetChild(i).GetComponent<BoxPreview>();
			this.previews.Add(component);
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x00088088 File Offset: 0x00086288
	private void OnEnable()
	{
		Vector3 localPosition = base.GetComponent<ScrollWindow>().transform_content.localPosition;
		localPosition.y = 0f;
		base.GetComponent<ScrollWindow>().transform_content.localPosition = localPosition;
		this.checkChildren();
		Vector3 localPosition2 = this.buttonsContainer.transform.localPosition;
		this.buttonsContainer.transform.localPosition = new Vector3(localPosition2.x, 0f, localPosition2.z);
		this.prepareLoadPreviews();
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x00088108 File Offset: 0x00086308
	private void prepareLoadPreviews()
	{
		SaveManager.clearCurrentSelectedWorld();
		for (int i = 0; i < this.previews.Count; i++)
		{
			this.previews[i].setSlot(i + 1);
		}
	}

	// Token: 0x04001189 RID: 4489
	public GameObject buttonsContainer;

	// Token: 0x0400118A RID: 4490
	private List<BoxPreview> previews = new List<BoxPreview>();

	// Token: 0x0400118B RID: 4491
	public GameObject slotButtonPrefabNew;

	// Token: 0x0400118C RID: 4492
	public ScrollRect scroll_rect;
}
