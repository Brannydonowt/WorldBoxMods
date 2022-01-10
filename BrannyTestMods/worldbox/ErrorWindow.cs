using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AE RID: 430
public class ErrorWindow : MonoBehaviour
{
	// Token: 0x060009BC RID: 2492 RVA: 0x000656A1 File Offset: 0x000638A1
	private void OnEnable()
	{
		this.errorText.text = ErrorWindow.errorMessage;
	}

	// Token: 0x04000C62 RID: 3170
	public Text errorText;

	// Token: 0x04000C63 RID: 3171
	public static string errorMessage;
}
