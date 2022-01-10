using System;
using RSG;

// Token: 0x0200023D RID: 573
public static class Auth
{
	// Token: 0x06000C9E RID: 3230 RVA: 0x0007B114 File Offset: 0x00079314
	public static void initializeAuth()
	{
		if (Auth.initialized)
		{
			return;
		}
		Auth.initialized = true;
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0007B124 File Offset: 0x00079324
	public static void AuthStateChanged(object sender, EventArgs eventArgs)
	{
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0007B126 File Offset: 0x00079326
	public static void signOut()
	{
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0007B128 File Offset: 0x00079328
	public static bool isValidUsername(string username)
	{
		return false;
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x0007B12B File Offset: 0x0007932B
	public static bool isValidEmail(string email)
	{
		return false;
	}

	// Token: 0x04000F5F RID: 3935
	public static UserLoginWindow userLoginWindow;

	// Token: 0x04000F60 RID: 3936
	public static bool isLoggedIn = false;

	// Token: 0x04000F61 RID: 3937
	public static string userId;

	// Token: 0x04000F62 RID: 3938
	public static string userName;

	// Token: 0x04000F63 RID: 3939
	public static string displayName;

	// Token: 0x04000F64 RID: 3940
	public static string emailAddress;

	// Token: 0x04000F65 RID: 3941
	private static bool initialized = false;

	// Token: 0x04000F66 RID: 3942
	public static bool authLoaded = false;

	// Token: 0x04000F67 RID: 3943
	public static Promise authLoadedPromise = new Promise();
}
