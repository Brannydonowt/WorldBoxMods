using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000221 RID: 545
public class SaveWorldButton : MonoBehaviour
{
	// Token: 0x06000C4F RID: 3151 RVA: 0x00079286 File Offset: 0x00077486
	private void Start()
	{
		if (base.GetComponent<Button>() != null)
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.saveWorld));
		}
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x000792B2 File Offset: 0x000774B2
	private void saveWorld()
	{
		ScrollWindow.showWindow("save_world_confirm");
	}
}
