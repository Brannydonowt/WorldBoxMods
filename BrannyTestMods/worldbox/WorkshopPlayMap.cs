using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002C6 RID: 710
public class WorkshopPlayMap : MonoBehaviour
{
	// Token: 0x06000F7E RID: 3966 RVA: 0x0008AFF9 File Offset: 0x000891F9
	private void Start()
	{
		if (base.GetComponent<Button>() != null)
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.playWorkShopMap));
		}
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0008B025 File Offset: 0x00089225
	public void playWorkShopMap()
	{
		ScrollWindow.showWindow("save_load_confirm");
	}
}
