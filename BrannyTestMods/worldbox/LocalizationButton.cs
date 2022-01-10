using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200025F RID: 607
public class LocalizationButton : MonoBehaviour
{
	// Token: 0x06000D28 RID: 3368 RVA: 0x0007DC64 File Offset: 0x0007BE64
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.changeSound));
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0007DC82 File Offset: 0x0007BE82
	private void changeSound()
	{
		LocalizedTextManager.instance.setLanguage(base.transform.gameObject.name);
	}
}
