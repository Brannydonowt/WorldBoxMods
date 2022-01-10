using System;

// Token: 0x02000240 RID: 576
public class Login
{
	// Token: 0x06000CA7 RID: 3239 RVA: 0x0007B168 File Offset: 0x00079368
	public static string createLoginQueueItemAsJSON(string username, string password)
	{
		string str = "";
		string str2 = "";
		return str + str2;
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0007B188 File Offset: 0x00079388
	public static async void GetEmailForUsername(string username, string password, Action<string, string> resultCallback)
	{
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0007B1B9 File Offset: 0x000793B9
	private static void UnsubscribeLoginQueueListener()
	{
	}

	// Token: 0x04000F6C RID: 3948
	private static string loginKey = "";

	// Token: 0x04000F6D RID: 3949
	private static Action<string, string> callback;
}
