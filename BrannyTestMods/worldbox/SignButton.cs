using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AF RID: 687
public class SignButton : MonoBehaviour
{
	// Token: 0x06000F14 RID: 3860 RVA: 0x00089B34 File Offset: 0x00087D34
	public void tryLogin()
	{
		this.clearStatus();
		this.loginEmail = this.textName.text;
		this.loginPassword = this.textPassword.text;
		this.loginUsername = "";
		if (this.loginEmail == "" || this.loginPassword == "")
		{
			this.errorStatus("EmailPasswordEmpty");
			return;
		}
		if (Auth.isValidEmail(this.loginEmail))
		{
			this.userLoginWindow.setLoading();
			this.continueLogin();
			return;
		}
		this.loginUsername = this.textName.text;
		this.loginEmail = "";
		if (!Username.isValid(this.loginUsername))
		{
			this.errorStatus("InvalidUsername");
			return;
		}
		PlayerConfig.dict["username"].stringVal = this.loginUsername;
		PlayerConfig.saveData();
		Login.GetEmailForUsername(this.loginUsername, this.loginPassword, new Action<string, string>(this.emailLoginCallback));
		this.userLoginWindow.setLoading();
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x00089C3F File Offset: 0x00087E3F
	public void continueLogin()
	{
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x00089C41 File Offset: 0x00087E41
	public void emailLoginCallback(string returnedEmail, string errorReason)
	{
		if (errorReason != "")
		{
			this.userLoginWindow.setLogin();
			this.errorStatus(errorReason);
			return;
		}
		this.loginEmail = returnedEmail;
		this.continueLogin();
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x00089C70 File Offset: 0x00087E70
	private void errorStatus(string pMessage)
	{
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.textStatusMessage.GetComponent<LocalizedText>().key = pMessage;
			this.textStatusMessage.GetComponent<LocalizedText>().updateText(true);
		}
		else
		{
			this.textStatusMessage.text = pMessage;
		}
		this.textStatusMessage.color = Toolbox.makeColor("#FF8686");
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x00089CCC File Offset: 0x00087ECC
	private void goodStatus(string pMessage)
	{
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.textStatusMessage.GetComponent<LocalizedText>().key = pMessage;
			this.textStatusMessage.GetComponent<LocalizedText>().updateText(true);
		}
		else
		{
			this.textStatusMessage.text = pMessage;
		}
		this.textStatusMessage.color = Toolbox.makeColor("#95DD5D");
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x00089D26 File Offset: 0x00087F26
	private void clearStatus()
	{
		this.textStatusMessage.text = "";
	}

	// Token: 0x04001208 RID: 4616
	public UserLoginWindow userLoginWindow;

	// Token: 0x04001209 RID: 4617
	public InputField textName;

	// Token: 0x0400120A RID: 4618
	public InputField textPassword;

	// Token: 0x0400120B RID: 4619
	public Text textStatusMessage;

	// Token: 0x0400120C RID: 4620
	private string loginEmail;

	// Token: 0x0400120D RID: 4621
	private string loginPassword;

	// Token: 0x0400120E RID: 4622
	private string loginUsername;
}
