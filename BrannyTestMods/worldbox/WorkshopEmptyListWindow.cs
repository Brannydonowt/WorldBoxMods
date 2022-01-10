using System;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class WorkshopEmptyListWindow : MonoBehaviour
{
	// Token: 0x06000F64 RID: 3940 RVA: 0x0008A953 File Offset: 0x00088B53
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		WindowHistory.list.RemoveAt(WindowHistory.list.Count - 1);
	}
}
