using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A9 RID: 681
public class AuthButton : MonoBehaviour
{
	// Token: 0x06000EE1 RID: 3809 RVA: 0x000895B5 File Offset: 0x000877B5
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x000895C2 File Offset: 0x000877C2
	public void showWorldNetOwnWorldsWindow()
	{
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x000895C4 File Offset: 0x000877C4
	public void showWorldNetWorldsListWindow()
	{
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x000895C6 File Offset: 0x000877C6
	public void showWorldNetMainWindow()
	{
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x000895C8 File Offset: 0x000877C8
	public void showWorldNetUploadWindow()
	{
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x000895CA File Offset: 0x000877CA
	public void showBrowseByTagWindow()
	{
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x000895CC File Offset: 0x000877CC
	public void wbbConfirm()
	{
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x000895CE File Offset: 0x000877CE
	public void uploadWorldButton()
	{
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x000895D0 File Offset: 0x000877D0
	public void checkAuthAndOpenWindow()
	{
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x000895DA File Offset: 0x000877DA
	// Note: this type is marked as 'beforefieldinit'.
	static AuthButton()
	{
		List<string> list = new List<string>();
		list.Add("worldnet_main");
		AuthButton.worldnetNoSub = list;
	}

	// Token: 0x040011FA RID: 4602
	private static string windowId;

	// Token: 0x040011FB RID: 4603
	private static List<string> worldnetNoSub;
}
