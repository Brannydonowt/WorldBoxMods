using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
public class LogText
{
	// Token: 0x06000BD3 RID: 3027 RVA: 0x00075E04 File Offset: 0x00074004
	public static void log(string pEvent, string pInfo = "", string pState = "")
	{
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00075E11 File Offset: 0x00074011
	public static string getPath()
	{
		return Application.persistentDataPath + LogText.dataName;
	}

	// Token: 0x04000E4C RID: 3660
	private static string dataName = "/wb_runtime.log";

	// Token: 0x04000E4D RID: 3661
	private static bool created = false;

	// Token: 0x04000E4E RID: 3662
	internal static int offset = 0;
}
