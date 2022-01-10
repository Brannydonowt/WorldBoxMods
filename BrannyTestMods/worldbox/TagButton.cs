using System;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class TagButton : MonoBehaviour
{
	// Token: 0x06000F1D RID: 3869 RVA: 0x00089D63 File Offset: 0x00087F63
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x00089D70 File Offset: 0x00087F70
	public void showWorldNetTagListWindow()
	{
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x00089D72 File Offset: 0x00087F72
	public bool inListWindow()
	{
		return ScrollWindow.currentWindowsContains("worldnet_list_your_worlds") || ScrollWindow.currentWindowsContains("worldnet_list_more_worlds");
	}

	// Token: 0x0400120F RID: 4623
	public MapTagType tagType;
}
