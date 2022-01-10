using System;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class WindowBackground : MonoBehaviour
{
	// Token: 0x06000F59 RID: 3929 RVA: 0x0008A6E6 File Offset: 0x000888E6
	private void Start()
	{
		this.group = base.GetComponent<CanvasGroup>();
		this.world = MapBox.instance;
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x0008A700 File Offset: 0x00088900
	private void Update()
	{
		if (ScrollWindow.isWindowActive() && this.group.alpha < 1f)
		{
			this.group.alpha += Time.deltaTime * 5f;
			return;
		}
		if (!ScrollWindow.isWindowActive() && this.group.alpha > 0f)
		{
			this.group.alpha -= Time.deltaTime * 5f;
		}
	}

	// Token: 0x0400123E RID: 4670
	private CanvasGroup group;

	// Token: 0x0400123F RID: 4671
	private MapBox world;
}
