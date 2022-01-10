using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public class TextSortingLayer : MonoBehaviour
{
	// Token: 0x06000A9C RID: 2716 RVA: 0x0006ABB5 File Offset: 0x00068DB5
	private void Start()
	{
		this.meshRenderer = base.gameObject.GetComponent<MeshRenderer>();
		this.meshRenderer.sortingLayerID = SortingLayer.NameToID("MapOverlay");
		this.meshRenderer.sortingOrder = 200;
	}

	// Token: 0x04000D29 RID: 3369
	private MeshRenderer meshRenderer;
}
