using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B2 RID: 690
public class UserForgotPasswordWindow : MonoBehaviour
{
	// Token: 0x06000F21 RID: 3873 RVA: 0x00089D99 File Offset: 0x00087F99
	public void Start()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.forgotPasswordButton.gameObject.SetActive(true);
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x00089DB4 File Offset: 0x00087FB4
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.forgotPasswordButton.gameObject.SetActive(true);
	}

	// Token: 0x04001210 RID: 4624
	public Button forgotPasswordButton;
}
