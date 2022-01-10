using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000219 RID: 537
public class LoadWorldButton : MonoBehaviour
{
	// Token: 0x06000BFA RID: 3066 RVA: 0x00076AE0 File Offset: 0x00074CE0
	private void Start()
	{
		if (base.GetComponent<Button>() != null)
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.loadWorld));
		}
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00076B0C File Offset: 0x00074D0C
	private void loadWorld()
	{
		ScrollWindow.showWindow("save_load_confirm");
	}
}
