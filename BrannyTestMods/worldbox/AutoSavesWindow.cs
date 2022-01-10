using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class AutoSavesWindow : MonoBehaviour
{
	// Token: 0x06000EB4 RID: 3764 RVA: 0x00088381 File Offset: 0x00086581
	private void OnEnable()
	{
		this.prepareList();
		this.prepareSaves();
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x00088390 File Offset: 0x00086590
	private void prepareSaves()
	{
		this._showQueue.Clear();
		List<AutoSaveData> autoSaves = AutoSaveManager.getAutoSaves();
		for (int i = 0; i < autoSaves.Count; i++)
		{
			AutoSaveData autoSaveData = autoSaves[i];
			this._showQueue.Enqueue(autoSaveData);
		}
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x000883D3 File Offset: 0x000865D3
	private void Update()
	{
		if (this._timer > 0f)
		{
			this._timer -= Time.deltaTime;
			return;
		}
		this._timer = 0.02f;
		this.showNextItemFromQueue();
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x00088408 File Offset: 0x00086608
	private void showNextItemFromQueue()
	{
		if (this._showQueue.Count == 0)
		{
			return;
		}
		AutoSaveData pData = this._showQueue.Dequeue();
		this.renderMapElement(pData);
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x00088438 File Offset: 0x00086638
	private void prepareList()
	{
		this.world = MapBox.instance;
		foreach (AutoSaveElement autoSaveElement in this.elements)
		{
			Object.Destroy(autoSaveElement.gameObject);
		}
		this.elements.Clear();
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x000884A4 File Offset: 0x000866A4
	private void renderMapElement(AutoSaveData pData)
	{
		AutoSaveElement autoSaveElement = Object.Instantiate<AutoSaveElement>(this.elementPrefab, this.transformContent);
		this.elements.Add(autoSaveElement);
		autoSaveElement.load(pData);
	}

	// Token: 0x04001196 RID: 4502
	public AutoSaveElement elementPrefab;

	// Token: 0x04001197 RID: 4503
	private List<AutoSaveElement> elements = new List<AutoSaveElement>();

	// Token: 0x04001198 RID: 4504
	private Queue<AutoSaveData> _showQueue = new Queue<AutoSaveData>();

	// Token: 0x04001199 RID: 4505
	private MapBox world;

	// Token: 0x0400119A RID: 4506
	public Transform transformContent;

	// Token: 0x0400119B RID: 4507
	private float _timer;
}
