using System;
using UnityEngine;

// Token: 0x020002AC RID: 684
public class RegisterButton : MonoBehaviour
{
	// Token: 0x06000EF6 RID: 3830 RVA: 0x0008972D File Offset: 0x0008792D
	public void usernameCheck()
	{
		Debug.Log("Name:  " + this.userRegisterWindow.inputTextUsername.text);
		this.userRegisterWindow.setPage2();
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x0008975C File Offset: 0x0008795C
	public void tryRegister()
	{
		this.clearStatus();
		Debug.Log("Name:  " + this.userRegisterWindow.inputTextUsername.text);
		Debug.Log("Email: " + this.userRegisterWindow.inputTextEmail.text);
		string text = this.userRegisterWindow.inputTextUsername.text;
		string text2 = this.userRegisterWindow.inputTextEmail.text;
		string text3 = this.userRegisterWindow.inputTextPassword.text;
		if (text2 == "" || text3 == "")
		{
			this.newStatus("EmailPasswordEmpty");
			return;
		}
		if (!Auth.isValidEmail(text2))
		{
			this.newStatus("InvalidEmail");
			return;
		}
		if (text3.Length < 6)
		{
			this.newStatus("ShortPassword");
			return;
		}
		this.userRegisterWindow.RegisterNewAccount(text, text3, text2);
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0008983D File Offset: 0x00087A3D
	private void sendVerification()
	{
		Debug.Log("send verification");
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0008984C File Offset: 0x00087A4C
	private void newStatus(string pMessage)
	{
		Debug.Log("new status " + pMessage);
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.userRegisterWindow.textMessage.GetComponent<LocalizedText>().key = pMessage;
			this.userRegisterWindow.textMessage.GetComponent<LocalizedText>().updateText(true);
			return;
		}
		this.userRegisterWindow.textMessage.text = pMessage;
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x000898AF File Offset: 0x00087AAF
	private void clearStatus()
	{
		this.newStatus("");
	}

	// Token: 0x04001203 RID: 4611
	public UserRegisterWindow userRegisterWindow;
}
