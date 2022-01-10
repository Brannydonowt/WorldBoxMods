using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AB RID: 683
public class ForgotPasswordButton : MonoBehaviour
{
	// Token: 0x06000EF0 RID: 3824 RVA: 0x00089628 File Offset: 0x00087828
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.newStatus("");
		this.emailInput.gameObject.SetActive(true);
		this.emailBG.gameObject.SetActive(true);
		this.continueButton.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		this.forgotPasswordButton = base.gameObject.GetComponent<Button>();
		this.checking = false;
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x0008969F File Offset: 0x0008789F
	public void resetPassword()
	{
		this.checking = true;
		this.clearStatus();
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x000896AE File Offset: 0x000878AE
	private void Update()
	{
		this.forgotPasswordButton.interactable = !this.checking;
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x000896C4 File Offset: 0x000878C4
	private void newStatus(string pMessage)
	{
		Debug.Log("new status " + pMessage);
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.statusMessage.GetComponent<LocalizedText>().key = pMessage;
			this.statusMessage.GetComponent<LocalizedText>().updateText(true);
			return;
		}
		this.statusMessage.text = pMessage;
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x00089718 File Offset: 0x00087918
	private void clearStatus()
	{
		this.newStatus("");
	}

	// Token: 0x040011FD RID: 4605
	public GameObject emailBG;

	// Token: 0x040011FE RID: 4606
	public InputField emailInput;

	// Token: 0x040011FF RID: 4607
	public Text statusMessage;

	// Token: 0x04001200 RID: 4608
	public Button continueButton;

	// Token: 0x04001201 RID: 4609
	private Button forgotPasswordButton;

	// Token: 0x04001202 RID: 4610
	private bool checking;
}
