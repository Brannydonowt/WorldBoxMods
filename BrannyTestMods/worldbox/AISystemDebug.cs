using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000148 RID: 328
public static class AISystemDebug
{
	// Token: 0x060007B1 RID: 1969 RVA: 0x00055D46 File Offset: 0x00053F46
	public static void clear()
	{
		AISystemDebug.debug_list_actions.Clear();
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00055D52 File Offset: 0x00053F52
	public static void debugLog(string pString)
	{
		AISystemDebug.debug_list_actions.Add(pString);
		if (AISystemDebug.debug_list_actions.Count > 1000)
		{
			AISystemDebug.debug_list_actions.RemoveAt(0);
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00055D7C File Offset: 0x00053F7C
	public static void log()
	{
		File.WriteAllText(AISystemDebug.getPath(), "");
		string text = "";
		foreach (string str in AISystemDebug.debug_list_actions)
		{
			text = text + str + "\n";
		}
		File.WriteAllText(AISystemDebug.getPath(), text);
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00055DF4 File Offset: 0x00053FF4
	public static string getPath()
	{
		return Application.persistentDataPath + AISystemDebug.dataName;
	}

	// Token: 0x04000A35 RID: 2613
	private static string dataName = "/ai_system.log";

	// Token: 0x04000A36 RID: 2614
	private static List<string> debug_list_actions = new List<string>();
}
