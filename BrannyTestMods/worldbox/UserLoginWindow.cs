using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B3 RID: 691
public class UserLoginWindow : MonoBehaviour
{
	// Token: 0x06000F24 RID: 3876 RVA: 0x00089DD8 File Offset: 0x00087FD8
	public void Start()
	{
		this.checkState();
		if (PlayerConfig.dict["username"].stringVal != "")
		{
			this.inputTextUser.text = PlayerConfig.dict["username"].stringVal;
		}
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x00089E2C File Offset: 0x0008802C
	public void checkState()
	{
		Debug.Log("Check Login Window State");
		if (Auth.isLoggedIn)
		{
			if (Auth.displayName != "" && Auth.displayName != null)
			{
				Debug.Log("displayName found");
				this.usernameText.text = Auth.displayName;
			}
			else if (Auth.userName != "" && Auth.userName != null)
			{
				Debug.Log("userName found");
				this.usernameText.text = Auth.userName;
			}
			else
			{
				Debug.Log("emailAddress found");
				this.usernameText.text = Auth.emailAddress;
			}
			this.setLogout();
		}
		else
		{
			this.setLogin();
		}
		this.isLoggedIn = Auth.isLoggedIn;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00089EE8 File Offset: 0x000880E8
	public void Update()
	{
		if (this.isLoggedIn != Auth.isLoggedIn)
		{
			this.checkState();
		}
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x00089F00 File Offset: 0x00088100
	public void setLoading()
	{
		this.windowTitle.GetComponent<LocalizedText>().key = "logging_in";
		this.windowTitle.GetComponent<LocalizedText>().updateText(true);
		this.groupLogin.SetActive(false);
		this.groupLogged.SetActive(false);
		this.groupLoading.SetActive(true);
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x00089F58 File Offset: 0x00088158
	public void setLogin()
	{
		this.windowTitle.GetComponent<LocalizedText>().key = "Login";
		this.windowTitle.GetComponent<LocalizedText>().updateText(true);
		this.groupLogged.SetActive(false);
		this.groupLoading.SetActive(false);
		this.groupLogin.SetActive(true);
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x00089FB0 File Offset: 0x000881B0
	public void setLogout()
	{
		this.windowTitle.GetComponent<LocalizedText>().key = "welcome_worldnet";
		this.windowTitle.GetComponent<LocalizedText>().updateText(true);
		this.groupLogin.SetActive(false);
		this.groupLoading.SetActive(false);
		this.groupLogged.SetActive(true);
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x0008A007 File Offset: 0x00088207
	public void clearWindow(string pMessage = "...")
	{
		this.textMessage.text = pMessage;
		this.inputTextPassword.text = "";
		this.inputTextUser.text = "";
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x0008A038 File Offset: 0x00088238
	public void clearCredentials()
	{
		this.inputTextPassword.text = "";
		this.inputTextUser.text = "";
		if (PlayerConfig.dict["username"].stringVal != "")
		{
			this.inputTextUser.text = PlayerConfig.dict["username"].stringVal;
		}
	}

	// Token: 0x04001211 RID: 4625
	public GameObject groupLogged;

	// Token: 0x04001212 RID: 4626
	public GameObject groupLogin;

	// Token: 0x04001213 RID: 4627
	public GameObject groupLoading;

	// Token: 0x04001214 RID: 4628
	public Text usernameText;

	// Token: 0x04001215 RID: 4629
	public Text windowTitle;

	// Token: 0x04001216 RID: 4630
	public InputField inputTextUser;

	// Token: 0x04001217 RID: 4631
	public InputField inputTextPassword;

	// Token: 0x04001218 RID: 4632
	public Text textMessage;

	// Token: 0x04001219 RID: 4633
	private bool isLoggedIn;
}
