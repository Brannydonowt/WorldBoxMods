using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B4 RID: 692
public class UserRegisterWindow : MonoBehaviour
{
	// Token: 0x06000F2D RID: 3885 RVA: 0x0008A0AC File Offset: 0x000882AC
	public void Start()
	{
		this.checkState();
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x0008A0B4 File Offset: 0x000882B4
	private void OnEnable()
	{
		this.checkState();
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x0008A0BC File Offset: 0x000882BC
	public void RegisterNewAccount(string username, string password, string email)
	{
		UserRegisterWindow._username = username;
		UserRegisterWindow._password = password;
		UserRegisterWindow._email = email;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x0008A0D0 File Offset: 0x000882D0
	public void registerAccountCallback(string errorReason)
	{
		Config.lockGameControls = false;
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x0008A0D8 File Offset: 0x000882D8
	public void checkState()
	{
		Debug.Log("Check Register Window State");
		if (Auth.isLoggedIn)
		{
			this.setSuccess();
			return;
		}
		this.setPage1();
		this.blockRegister1Button();
		this.blockRegister2Button();
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x0008A104 File Offset: 0x00088304
	public void setSuccess()
	{
		Config.lockGameControls = false;
		this.page2.SetActive(false);
		this.page1.SetActive(false);
		this.creationPage.SetActive(false);
		this.successPage.SetActive(true);
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x0008A13C File Offset: 0x0008833C
	public void setPage2()
	{
		Config.lockGameControls = false;
		this.page1.SetActive(false);
		this.successPage.SetActive(false);
		this.creationPage.SetActive(false);
		this.page2.SetActive(true);
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x0008A174 File Offset: 0x00088374
	public void setPage1()
	{
		Config.lockGameControls = false;
		this.page2.SetActive(false);
		this.successPage.SetActive(false);
		this.creationPage.SetActive(false);
		this.page1.SetActive(true);
		InputField inputField = this.inputTextUsername;
		if (!string.IsNullOrEmpty((inputField != null) ? inputField.text : null))
		{
			RegisterUsername.runUsernameCheck(this.inputTextUsername);
		}
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x0008A1DB File Offset: 0x000883DB
	public void setCreation()
	{
		Config.lockGameControls = true;
		this.page1.SetActive(false);
		this.page2.SetActive(false);
		this.successPage.SetActive(false);
		this.creationPage.SetActive(true);
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x0008A213 File Offset: 0x00088413
	public void blockRegister1Button()
	{
		this.usernameCheckButton.GetComponent<CanvasGroup>().alpha = 0.2f;
		this.usernameCheckButton.interactable = false;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x0008A236 File Offset: 0x00088436
	public void unblockRegister1Button()
	{
		this.usernameCheckButton.GetComponent<CanvasGroup>().alpha = 1f;
		this.usernameCheckButton.interactable = true;
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x0008A259 File Offset: 0x00088459
	public void blockRegister2Button()
	{
		this.emailCheckButton.GetComponent<CanvasGroup>().alpha = 0.2f;
		this.emailCheckButton.interactable = false;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x0008A27C File Offset: 0x0008847C
	public void unblockRegister2Button()
	{
		this.emailCheckButton.GetComponent<CanvasGroup>().alpha = 1f;
		this.emailCheckButton.interactable = true;
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x0008A2A0 File Offset: 0x000884A0
	public void newStatus(string pMessage)
	{
		Debug.Log("new status " + pMessage);
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.textMessage.GetComponent<LocalizedText>().key = pMessage;
			this.textMessage.GetComponent<LocalizedText>().updateText(true);
			return;
		}
		this.textMessage.text = pMessage;
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x0008A2F4 File Offset: 0x000884F4
	public void clearStatus()
	{
		this.newStatus("");
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x0008A301 File Offset: 0x00088501
	public void blockRegisterButton()
	{
	}

	// Token: 0x0400121A RID: 4634
	public GameObject page1;

	// Token: 0x0400121B RID: 4635
	public GameObject page2;

	// Token: 0x0400121C RID: 4636
	public GameObject successPage;

	// Token: 0x0400121D RID: 4637
	public GameObject creationPage;

	// Token: 0x0400121E RID: 4638
	public Button usernameCheckButton;

	// Token: 0x0400121F RID: 4639
	public Button emailCheckButton;

	// Token: 0x04001220 RID: 4640
	public InputField inputTextUsername;

	// Token: 0x04001221 RID: 4641
	public InputField inputTextEmail;

	// Token: 0x04001222 RID: 4642
	public InputField inputTextPassword;

	// Token: 0x04001223 RID: 4643
	public Text textMessage;

	// Token: 0x04001224 RID: 4644
	private static string _email = "";

	// Token: 0x04001225 RID: 4645
	private static string _password = "";

	// Token: 0x04001226 RID: 4646
	private static string _username = "";
}
