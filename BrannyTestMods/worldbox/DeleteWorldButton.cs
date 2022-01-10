using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000218 RID: 536
public class DeleteWorldButton : MonoBehaviour
{
	// Token: 0x06000BF7 RID: 3063 RVA: 0x00076AA0 File Offset: 0x00074CA0
	private void Start()
	{
		if (base.GetComponent<Button>() != null)
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.deleteWorld));
		}
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00076ACC File Offset: 0x00074CCC
	private void deleteWorld()
	{
		ScrollWindow.showWindow("save_delete_confirm");
	}
}
