using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class SaveSlotManager : MonoBehaviour
{
	// Token: 0x06000D48 RID: 3400 RVA: 0x0007E61C File Offset: 0x0007C81C
	private void Init()
	{
		SaveManager.clearCurrentSelectedWorld();
		int num = 65;
		int num2 = 65;
		int num3 = 0;
		int num4 = 1;
		int num5 = 10;
		for (int i = 0; i < num5; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.slotButtonPrefab, this.buttonsContainer.transform);
			gameObject.transform.localPosition = new Vector3((float)(-(float)num), (float)(-(float)num3 * num2));
			this.setID(gameObject, num4++);
			gameObject = Object.Instantiate<GameObject>(this.slotButtonPrefab, this.buttonsContainer.transform);
			gameObject.transform.localPosition = new Vector3(0f, (float)(-(float)num3 * num2));
			this.setID(gameObject, num4++);
			gameObject = Object.Instantiate<GameObject>(this.slotButtonPrefab, this.buttonsContainer.transform);
			gameObject.transform.localPosition = new Vector3((float)num, (float)(-(float)num3 * num2));
			this.setID(gameObject, num4++);
			num3++;
		}
		this.content.sizeDelta = new Vector2(0f, (float)(num5 * num2));
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0007E72A File Offset: 0x0007C92A
	private void OnEnable()
	{
		this.loaded = false;
		this.Init();
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0007E73C File Offset: 0x0007C93C
	private void Update()
	{
		foreach (LevelPreviewButton levelPreviewButton in this.previews)
		{
			if (!levelPreviewButton.loaded && !levelPreviewButton.loading)
			{
				levelPreviewButton.reloadImage();
				break;
			}
		}
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0007E7A0 File Offset: 0x0007C9A0
	private void OnDisable()
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.previews[i].checkTextureDestroy();
			Object.Destroy(this.containers[i]);
			this.containers[i] = null;
		}
		this.previews.Clear();
		this.containers.Clear();
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0007E808 File Offset: 0x0007CA08
	private void setID(GameObject pContainer, int pID)
	{
		Transform transform = pContainer.transform.Find("AnimationContainer/Mask/SizeContainer/Button");
		transform.GetComponent<SlotButtonCallback>().slotID = pID;
		transform.GetComponent<LevelPreviewButton>().loaded = false;
		transform.GetComponent<LevelPreviewButton>().worldNetUpload = this.worldNetUpload;
		if (pID > 1)
		{
			transform.GetComponent<LevelPreviewButton>().premiumOnly = true;
		}
		this.previews.Add(transform.GetComponent<LevelPreviewButton>());
		this.containers.Add(pContainer);
	}

	// Token: 0x04001033 RID: 4147
	public GameObject buttonsContainer;

	// Token: 0x04001034 RID: 4148
	private List<LevelPreviewButton> previews = new List<LevelPreviewButton>();

	// Token: 0x04001035 RID: 4149
	private List<GameObject> containers = new List<GameObject>();

	// Token: 0x04001036 RID: 4150
	public GameObject slotButtonPrefab;

	// Token: 0x04001037 RID: 4151
	public RectTransform content;

	// Token: 0x04001038 RID: 4152
	private Vector3 originalPos;

	// Token: 0x04001039 RID: 4153
	public bool loaded;

	// Token: 0x0400103A RID: 4154
	public bool worldNetUpload;
}
