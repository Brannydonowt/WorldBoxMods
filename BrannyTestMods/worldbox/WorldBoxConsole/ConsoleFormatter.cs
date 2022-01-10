using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WorldBoxConsole
{
	// Token: 0x02000300 RID: 768
	public class ConsoleFormatter
	{
		// Token: 0x060011C4 RID: 4548 RVA: 0x0009A568 File Offset: 0x00098768
		public static string logError(int errorNum, string logString, string stackTrace)
		{
			ConsoleFormatter.log = "";
			ConsoleFormatter.log = ConsoleFormatter.log + "<color=red>--- error[" + errorNum.ToString() + "]: ---</color>\n";
			foreach (string str in logString.Split(new char[]
			{
				'\n'
			}))
			{
				ConsoleFormatter.log = ConsoleFormatter.log + "<b><color=cyan>" + str + "</color></b>\n";
			}
			try
			{
				stackTrace = ConsoleFormatter.formatStacktrace(stackTrace);
			}
			catch (Exception)
			{
			}
			if (stackTrace.Trim() != "")
			{
				ConsoleFormatter.log += "<color=red>--- stack: ---</color>\n";
				ConsoleFormatter.log = ConsoleFormatter.log + stackTrace + "\n";
			}
			return ConsoleFormatter.log;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0009A63C File Offset: 0x0009883C
		public static string addSystemInfo()
		{
			ConsoleFormatter.log = "";
			ConsoleFormatter.log += "-----------";
			ConsoleFormatter.log = ConsoleFormatter.log + "\nGame Version: <color=white>" + Application.version + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\noperatingSystemFamily: <color=white>" + SystemInfo.operatingSystemFamily.ToString() + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\ndeviceModel: <color=white>" + SystemInfo.deviceModel + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\ndeviceName: <color=white>" + SystemInfo.deviceName + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\ndeviceType: <color=white>" + SystemInfo.deviceType.ToString() + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\nsystemMemorySize: <color=white>" + SystemInfo.systemMemorySize.ToString() + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\ngraphicsDeviceID: <color=white>" + SystemInfo.graphicsDeviceID.ToString() + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\nGC.GetTotalMemory: <color=white>" + (GC.GetTotalMemory(false) / 1000000L).ToString() + " mb</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\ngraphicsMemorySize: <color=white>" + SystemInfo.graphicsMemorySize.ToString() + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\nmaxTextureSize: <color=white>" + SystemInfo.maxTextureSize.ToString() + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\noperatingSystem: <color=white>" + SystemInfo.operatingSystem + "</color>";
			ConsoleFormatter.log = ConsoleFormatter.log + "\nprocessorType: <color=white>" + SystemInfo.processorType + "</color>";
			ConsoleFormatter.log += "\n-----------";
			return ConsoleFormatter.log;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0009A834 File Offset: 0x00098A34
		public static string logFormatter(string logString)
		{
			if (logString.Trim() != "" && logString.Any(new Func<char, bool>(char.IsDigit)) && !logString.Contains("<color"))
			{
				foreach (string text in (from c in Regex.Split(logString, "[^0-9\\.]+")
				where c != "." && c.Trim() != ""
				select c into x
				orderby x.Length descending
				select x).Distinct<string>())
				{
					if (!text.Contains("<color"))
					{
						logString = logString.Replace(text, "<color=white>" + text + "</color>");
					}
				}
			}
			return logString;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0009A930 File Offset: 0x00098B30
		public static string formatStacktrace(string stackTrace)
		{
			string[] array = stackTrace.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains("(at "))
				{
					string[] array2 = array[i].Split(new string[]
					{
						" (at "
					}, StringSplitOptions.None);
					ConsoleFormatter.start = array2[0];
					ConsoleFormatter.end = array2[1].Substring(0, array2[1].Length - 1);
				}
				else
				{
					ConsoleFormatter.start = array[i];
					ConsoleFormatter.end = "";
				}
				if (ConsoleFormatter.start.Contains("("))
				{
					string[] array3 = ConsoleFormatter.start.Split(new char[]
					{
						'('
					});
					string text = array3[0];
					string text2 = array3[1].Substring(0, array3[1].Length - 1);
					if (text.Contains(":"))
					{
						string[] array4 = text.Split(new char[]
						{
							':'
						});
						array4[array4.Length - 1] = "<b><color=cyan>" + array4[array4.Length - 1] + "</color></b>";
						text = string.Join(":", array4);
					}
					else if (text.Contains("."))
					{
						string[] array5 = text.Split(new char[]
						{
							'.'
						});
						array5[array5.Length - 1] = "<b><color=cyan>" + array5[array5.Length - 1] + "</color></b>";
						text = string.Join(".", array5);
					}
					if (text2.Trim() != string.Empty)
					{
						string[] array6;
						if (text2.Contains(","))
						{
							array6 = text2.Split(new char[]
							{
								','
							});
						}
						else
						{
							array6 = new string[]
							{
								text2
							};
						}
						for (int j = 0; j < array6.Length; j++)
						{
							if (array6[j].Trim().Contains(' '))
							{
								string[] array7 = array6[j].Trim().Split(new char[]
								{
									' '
								});
								string text3 = array7[0];
								if (text3.Contains("."))
								{
									text3 = text3.Split(new char[]
									{
										'.'
									})[text3.Split(new char[]
									{
										'.'
									}).Length - 1];
								}
								string text4 = array7[1];
								array6[j] = string.Concat(new string[]
								{
									"<color=#FFCC1C>",
									text3,
									"</color> <b><color=cyan>",
									text4,
									"</color></b>"
								});
							}
							else
							{
								array6[j] = "<color=#FFCC1C>" + array6[j].Trim() + "</color>";
							}
							text2 = string.Join(", ", array6);
						}
					}
					ConsoleFormatter.start = text + "(" + text2 + ")";
					while (ConsoleFormatter.start.Contains("System."))
					{
						ConsoleFormatter.start = ConsoleFormatter.start.Replace("System.", string.Empty);
					}
				}
				if (ConsoleFormatter.end != "")
				{
					if (ConsoleFormatter.end.Contains("BuiltInPackages/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"BuiltInPackages/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains("unity/build/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"unity/build/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains("Unity.app/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"Unity.app/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains("Export/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"Export/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains(":"))
					{
						string[] array8 = ConsoleFormatter.end.Split(new char[]
						{
							':'
						});
						string[] array9 = array8[array8.Length - 2].Split(new char[]
						{
							'/'
						});
						array9[array9.Length - 1] = "<size=7><b><color=cyan>" + array9[array9.Length - 1] + "</color></b></size>";
						array8[array8.Length - 2] = string.Join("/", array9);
						array8[array8.Length - 1] = "<size=7><b><color=cyan>" + array8[array8.Length - 1] + "</color></b></size>";
						ConsoleFormatter.end = string.Join(":", array8);
					}
					ConsoleFormatter.end = "<size=5> (at " + ConsoleFormatter.end + ")</size>";
				}
				array[i] = "<size=7>" + ConsoleFormatter.start + "</size>" + ConsoleFormatter.end;
			}
			stackTrace = string.Join("\n", array);
			return stackTrace;
		}

		// Token: 0x040014BF RID: 5311
		private static string log;

		// Token: 0x040014C0 RID: 5312
		private static string start;

		// Token: 0x040014C1 RID: 5313
		private static string end;
	}
}
