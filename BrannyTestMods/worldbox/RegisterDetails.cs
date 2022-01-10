using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AD RID: 685
public class RegisterDetails : MonoBehaviour
{
	// Token: 0x06000EFC RID: 3836 RVA: 0x000898C4 File Offset: 0x00087AC4
	private void OnEnable()
	{
		RegisterDetails.checkButton();
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x000898CB File Offset: 0x00087ACB
	public void emailCheck(InputField pEmail)
	{
		RegisterDetails.runEmailCheck(pEmail);
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x000898D4 File Offset: 0x00087AD4
	public static async void runEmailCheck(InputField pEmail)
	{
		string text = pEmail.text;
		RegisterDetails.emailValid = false;
		Debug.Log("Name: " + text);
		if (!Auth.isValidEmail(text))
		{
			RegisterDetails.newStatus("InvalidEmail");
			RegisterDetails.checkButton();
			Debug.Log("Not valid");
		}
		else
		{
			RegisterDetails.clearStatus();
			RegisterDetails.emailValid = true;
			RegisterDetails.checkButton();
		}
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0008990D File Offset: 0x00087B0D
	public void passwordCheck(InputField pEmail)
	{
		RegisterDetails.runPasswordCheck(pEmail);
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00089918 File Offset: 0x00087B18
	public static void runPasswordCheck(InputField pPassword)
	{
		string text = pPassword.text;
		RegisterDetails.passwordValid = false;
		Debug.Log("Pass: " + text);
		if (text.Length < 6)
		{
			RegisterDetails.newStatus("ShortPassword");
			RegisterDetails.checkButton();
			Debug.Log("Not valid");
			return;
		}
		RegisterDetails.clearStatus();
		RegisterDetails.passwordValid = true;
		RegisterDetails.checkButton();
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x00089975 File Offset: 0x00087B75
	private static void checkButton()
	{
		if (RegisterDetails.emailValid && RegisterDetails.passwordValid)
		{
			RegisterDetails.unblockRegisterButton();
			return;
		}
		RegisterDetails.blockRegisterButton();
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00089990 File Offset: 0x00087B90
	private static void blockRegisterButton()
	{
		if (RegisterDetails.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().blockRegister2Button();
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x000899AD File Offset: 0x00087BAD
	private static void unblockRegisterButton()
	{
		if (RegisterDetails.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().unblockRegister2Button();
		}
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x000899CA File Offset: 0x00087BCA
	private static void newStatus(string pMessage)
	{
		if (RegisterDetails.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().newStatus(pMessage);
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x000899E8 File Offset: 0x00087BE8
	private static bool registerWindowExists()
	{
		return ScrollWindow.get("register") != null && ScrollWindow.get("register").GetComponent<UserRegisterWindow>() != null;
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00089A13 File Offset: 0x00087C13
	private static void clearStatus()
	{
		RegisterDetails.newStatus("");
	}

	// Token: 0x04001204 RID: 4612
	private static bool emailValid;

	// Token: 0x04001205 RID: 4613
	private static bool passwordValid;
}
