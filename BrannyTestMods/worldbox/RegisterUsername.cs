using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AE RID: 686
public class RegisterUsername : MonoBehaviour
{
	// Token: 0x06000F09 RID: 3849 RVA: 0x00089A29 File Offset: 0x00087C29
	private void OnEnable()
	{
		RegisterUsername.usernameOK = false;
		RegisterUsername.termsOK = false;
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x00089A37 File Offset: 0x00087C37
	public void usernameCheck(InputField pUsername)
	{
		RegisterUsername.runUsernameCheck(pUsername);
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x00089A40 File Offset: 0x00087C40
	public static async void runUsernameCheck(InputField pUsername)
	{
		RegisterUsername.clearStatus();
		RegisterUsername.blockRegisterButton();
		RegisterUsername.usernameOK = false;
		string text = pUsername.text;
		Debug.Log("Name: " + text);
		if (!Username.isValid(text))
		{
			RegisterUsername.newStatus("InvalidUsernameLong");
			RegisterUsername.blockRegisterButton();
			Debug.Log("Not valid");
		}
		else
		{
			Debug.Log("Check if taken : " + text);
			try
			{
				TaskAwaiter<bool> taskAwaiter = Username.isTaken(text).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					RegisterUsername.newStatus("UsernameTaken");
					RegisterUsername.blockRegisterButton();
					return;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				RegisterUsername.newStatus(ex.Message.ToString());
				RegisterUsername.blockRegisterButton();
				return;
			}
			Debug.Log("not taken?");
			RegisterUsername.usernameOK = true;
			RegisterUsername.unblockRegisterButton();
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x00089A79 File Offset: 0x00087C79
	public void termsCheck(bool pTermsEnabled)
	{
		RegisterUsername.termsOK = pTermsEnabled;
		RegisterUsername.unblockRegisterButton();
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x00089A86 File Offset: 0x00087C86
	private static void blockRegisterButton()
	{
		if (RegisterUsername.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().blockRegister1Button();
		}
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x00089AA3 File Offset: 0x00087CA3
	private static void unblockRegisterButton()
	{
		if (!RegisterUsername.usernameOK || !RegisterUsername.termsOK)
		{
			RegisterUsername.blockRegisterButton();
			return;
		}
		if (RegisterUsername.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().unblockRegister1Button();
		}
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x00089AD4 File Offset: 0x00087CD4
	private static void newStatus(string pMessage)
	{
		if (RegisterUsername.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().newStatus(pMessage);
		}
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x00089AF2 File Offset: 0x00087CF2
	private static bool registerWindowExists()
	{
		return ScrollWindow.get("register") != null && ScrollWindow.get("register").GetComponent<UserRegisterWindow>() != null;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x00089B1D File Offset: 0x00087D1D
	private static void clearStatus()
	{
		RegisterUsername.newStatus("");
	}

	// Token: 0x04001206 RID: 4614
	private static bool usernameOK;

	// Token: 0x04001207 RID: 4615
	private static bool termsOK;
}
