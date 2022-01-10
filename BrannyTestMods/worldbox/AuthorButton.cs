using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class AuthorButton : MonoBehaviour
{
	// Token: 0x06000EEC RID: 3820 RVA: 0x000895F1 File Offset: 0x000877F1
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x000895FE File Offset: 0x000877FE
	public void showWorldNetAuthorListWindow()
	{
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00089600 File Offset: 0x00087800
	public bool inListWindow()
	{
		return ScrollWindow.currentWindowsContains("worldnet_list_your_worlds") || ScrollWindow.currentWindowsContains("worldnet_list_more_worlds");
	}

	// Token: 0x040011FC RID: 4604
	public string authorId;
}
