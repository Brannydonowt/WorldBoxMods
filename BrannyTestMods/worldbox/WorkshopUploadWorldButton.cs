using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002C7 RID: 711
public class WorkshopUploadWorldButton : MonoBehaviour
{
	// Token: 0x06000F81 RID: 3969 RVA: 0x0008B039 File Offset: 0x00089239
	private void Start()
	{
		if (base.GetComponent<Button>() != null)
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.uploadWorldToWorkshop));
		}
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x0008B065 File Offset: 0x00089265
	private void uploadWorldToWorkshop()
	{
		ScrollWindow.showWindow("steam_workshop_uploading");
	}
}
