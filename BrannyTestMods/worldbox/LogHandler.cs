using System;
using System.IO;
using UnityEngine;
using WorldBoxConsole;

// Token: 0x02000210 RID: 528
public class LogHandler
{
	// Token: 0x06000BCA RID: 3018 RVA: 0x000758F4 File Offset: 0x00073AF4
	[RuntimeInitializeOnLoadMethod]
	public static void init()
	{
		if (LogHandler.initialized)
		{
			return;
		}
		LogHandler.initialized = true;
		if (!Application.isEditor)
		{
			Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
			Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.ScriptOnly);
			Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.ScriptOnly);
		}
		Application.logMessageReceived += LogHandler.HandleLog;
		Application.logMessageReceived += Console.HandleLog;
		if (!Directory.Exists(LogHandler.getDirPath()))
		{
			Directory.CreateDirectory(LogHandler.getDirPath());
		}
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00075964 File Offset: 0x00073B64
	private static void HandleLog(string logString, string stackTrace, LogType type)
	{
		if (LogHandler.errorNum > 100)
		{
			return;
		}
		if (type != LogType.Error && type != LogType.Exception && type != LogType.Assert)
		{
			LogHandler.clearRepeat();
			LogHandler.log = LogHandler.log + "- trace: " + logString + "\n";
			if (stackTrace.Trim() != "")
			{
				LogHandler.log = LogHandler.log + "- trace-stack:\n" + stackTrace + "\n";
			}
			return;
		}
		if (LogHandler.errorNum == 0)
		{
			LogHandler.log = LogHandler.log + "Game Version: " + Application.version;
			LogHandler.log = LogHandler.log + "\nMODDED: " + Config.MODDED.ToString();
			LogHandler.log = LogHandler.log + "\noperatingSystemFamily: " + SystemInfo.operatingSystemFamily.ToString();
			LogHandler.log = LogHandler.log + "\ndeviceModel: " + SystemInfo.deviceModel;
			LogHandler.log = LogHandler.log + "\ndeviceName: " + SystemInfo.deviceName;
			LogHandler.log = LogHandler.log + "\ndeviceType: " + SystemInfo.deviceType.ToString();
			LogHandler.log = LogHandler.log + "\nsystemMemorySize: " + SystemInfo.systemMemorySize.ToString();
			LogHandler.log = LogHandler.log + "\ngraphicsDeviceID: " + SystemInfo.graphicsDeviceID.ToString();
			LogHandler.log = LogHandler.log + "\nGC.GetTotalMemory: " + (GC.GetTotalMemory(false) / 1000000L).ToString() + " mb";
			LogHandler.log = LogHandler.log + "\ngraphicsMemorySize: " + SystemInfo.graphicsMemorySize.ToString();
			LogHandler.log = LogHandler.log + "\nmaxTextureSize: " + SystemInfo.maxTextureSize.ToString();
			LogHandler.log = LogHandler.log + "\noperatingSystem: " + SystemInfo.operatingSystem;
			LogHandler.log = LogHandler.log + "\nprocessorType: " + SystemInfo.processorType;
			LogHandler.log += "\n-----------\n\n";
		}
		if (stackTrace.Trim() != "" && stackTrace == LogHandler.lastError)
		{
			LogHandler.errorRepeated++;
			return;
		}
		if (stackTrace.Trim() == "" && logString == LogHandler.lastError)
		{
			LogHandler.errorRepeated++;
			return;
		}
		LogHandler.clearRepeat();
		LogHandler.log = string.Concat(new string[]
		{
			LogHandler.log,
			"- error[",
			LogHandler.errorNum.ToString(),
			"]: ",
			logString,
			"\n"
		});
		LogHandler.log = LogHandler.log + "- stack:\n" + stackTrace + "\n";
		LogHandler.lastError = stackTrace;
		File.WriteAllText(LogHandler.getPath(), LogHandler.log);
		LogHandler.errorNum++;
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00075C67 File Offset: 0x00073E67
	private static void clearRepeat()
	{
		if (LogHandler.errorRepeated > 0)
		{
			LogHandler.log = LogHandler.log + "- last error repeated " + LogHandler.errorRepeated.ToString() + " times\n";
			LogHandler.lastError = "";
			LogHandler.errorRepeated = 0;
		}
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00075CA4 File Offset: 0x00073EA4
	public static string getDirPath()
	{
		return Application.persistentDataPath + LogHandler.folder_base;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00075CB8 File Offset: 0x00073EB8
	public static string getPath()
	{
		if (LogHandler.timeString == "")
		{
			LogHandler.timeString = DateTime.Now.ToShortTimeString();
		}
		string text = DateTime.Today.ToShortDateString() + "_" + LogHandler.timeString;
		text = text.Replace("/", "_");
		text = text.Replace("\\", "_");
		text = text.Replace(" ", "_");
		text = text.Replace(":", "_");
		return string.Concat(new string[]
		{
			LogHandler.getDirPath(),
			LogHandler.dataName,
			"_",
			text,
			".log"
		});
	}

	// Token: 0x04000E44 RID: 3652
	private static string folder_base = "/logs";

	// Token: 0x04000E45 RID: 3653
	private static string dataName = "/error";

	// Token: 0x04000E46 RID: 3654
	public static string log = "";

	// Token: 0x04000E47 RID: 3655
	private static int errorNum = 0;

	// Token: 0x04000E48 RID: 3656
	private static string lastError = "";

	// Token: 0x04000E49 RID: 3657
	private static int errorRepeated = 0;

	// Token: 0x04000E4A RID: 3658
	private static bool initialized = false;

	// Token: 0x04000E4B RID: 3659
	private static string timeString = "";
}
