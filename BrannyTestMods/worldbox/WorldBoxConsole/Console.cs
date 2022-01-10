using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WorldBoxConsole
{
	// Token: 0x020002FF RID: 767
	public class Console : MonoBehaviour
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x00099E9C File Offset: 0x0009809C
		private void Awake()
		{
			this.text = base.transform.Find("Scroll View/Viewport/Content/CText").gameObject.GetComponent<Text>();
			Console.textGroup = base.transform.Find("Scroll View/Viewport/Content");
			this.textObj.Add(this.text);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00099EF0 File Offset: 0x000980F0
		private void addText()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(base.transform.Find("Scroll View/Viewport/Content/CText").gameObject);
			gameObject.name = "CText " + (this.textObj.Count + 1).ToString();
			Text component = gameObject.GetComponent<Text>();
			component.text = "";
			gameObject.transform.SetParent(Console.textGroup);
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.localScale = Vector3.one;
			component2.localPosition = Vector3.zero;
			component2.SetLeft(10f);
			component2.SetRight(10f);
			float num = 20f;
			for (int i = this.textObj.Count - 1; i > -1; i--)
			{
				RectTransform component3 = this.textObj[i].gameObject.GetComponent<RectTransform>();
				component3.SetBottom(num);
				num += component3.rect.height;
			}
			component2.SetBottom(0f);
			gameObject.GetComponent<Text>();
			this.textObj.Add(component);
			this.truncateGameObjects();
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0009A00D File Offset: 0x0009820D
		private void truncateGameObjects()
		{
			while (this.textObj.Count > 500)
			{
				Object.Destroy(this.textObj[0].gameObject);
				this.textObj.RemoveAt(0);
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0009A045 File Offset: 0x00098245
		private static void truncateTexts()
		{
			if (Console.texts.Count <= 500)
			{
				return;
			}
			while (Console.texts.Count > 500)
			{
				Console.texts.Dequeue();
			}
			Console.lineNum = 0;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0009A07B File Offset: 0x0009827B
		private void OnEnable()
		{
			if (!Config.gameLoaded)
			{
				return;
			}
			Console.lineNum = 0;
			this.text.text = "";
			Console.textGroup.gameObject.GetComponent<RectTransform>().SetBottom(0f);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0009A0B4 File Offset: 0x000982B4
		private void OnDisable()
		{
			Console.lineNum = 0;
			this.text.text = "";
			while (this.textObj.Count > 1)
			{
				Component component = this.textObj[this.textObj.Count - 1];
				this.textObj.Remove(this.textObj[this.textObj.Count - 1]);
				Object.Destroy(component.gameObject);
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0009A12D File Offset: 0x0009832D
		public void Toggle()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
				return;
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0009A155 File Offset: 0x00098355
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0009A163 File Offset: 0x00098363
		public void Show()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0009A171 File Offset: 0x00098371
		public bool isActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0009A180 File Offset: 0x00098380
		public static void HandleLog(string logString, string stackTrace, LogType type)
		{
			Console.log = ConsoleFormatter.logFormatter("[" + Console.TimeNow() + "] ");
			logString = ConsoleFormatter.logFormatter(logString);
			if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
			{
				if (Console.errorNum == 0)
				{
					Console.texts.Enqueue(ConsoleFormatter.addSystemInfo());
				}
				if (logString == Console.lastError)
				{
					Console.errorRepeated++;
					return;
				}
				Console.clearRepeat();
				Console.log += ConsoleFormatter.logError(Console.errorNum, logString, stackTrace);
				Console.lastError = logString;
				Console.errorNum++;
			}
			else
			{
				Console.clearRepeat();
				if (logString.StartsWith("notrace", StringComparison.Ordinal))
				{
					return;
				}
				Console.log = Console.log + "trace: " + logString;
			}
			Console.texts.Enqueue(Console.log);
			Console.truncateTexts();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0009A260 File Offset: 0x00098460
		private static void clearRepeat()
		{
			if (Console.errorRepeated > 0)
			{
				Console.log = "( last error repeated " + Console.errorRepeated.ToString() + " times )";
				Console.texts.Enqueue(Console.log);
				Console.log = "";
				Console.lastError = "";
				Console.errorRepeated = 0;
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0009A2BC File Offset: 0x000984BC
		private void Update()
		{
			if (Console.lineNum != Console.texts.Count)
			{
				string[] array = string.Join("\n", Console.texts).Split(new char[]
				{
					'\n'
				});
				int num = -1;
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = Mathf.CeilToInt((float)(i + 1) / 10f) - 1;
					while (this.textObj.Count < num2 + 1)
					{
						this.addText();
					}
					if (num2 != num)
					{
						this.textObj[num2].text = "";
						num = num2;
					}
					Text text = this.textObj[num2];
					text.text = text.text + "\n" + array[i];
					this.textObj[num2].text = this.textObj[num2].text.TrimStart(new char[]
					{
						'\n'
					});
				}
				Console.lineNum = Console.texts.Count;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0009A3C0 File Offset: 0x000985C0
		private void LateUpdate()
		{
			RectTransform component = this.textObj[this.textObj.Count - 1].gameObject.GetComponent<RectTransform>();
			component.SetBottom(0f);
			float num = 0f;
			float num2 = 0f;
			for (int i = this.textObj.Count - 2; i > -1; i--)
			{
				component = this.textObj[i].gameObject.GetComponent<RectTransform>();
				RectTransform component2 = this.textObj[i + 1].gameObject.GetComponent<RectTransform>();
				num += component2.rect.height;
				num2 += component.rect.height;
				component.SetBottom(num);
			}
			RectTransform component3 = Console.textGroup.GetComponent<RectTransform>();
			component3.sizeDelta = new Vector2(component3.sizeDelta.x, num2 + 300f);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0009A4A8 File Offset: 0x000986A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string TimeNow()
		{
			DateTime now = DateTime.Now;
			char[] array = new char[8];
			Console.Write2Chars(array, 0, now.Hour);
			array[2] = ':';
			Console.Write2Chars(array, 3, now.Minute);
			array[5] = ':';
			Console.Write2Chars(array, 6, now.Second);
			return new string(array);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0009A4FA File Offset: 0x000986FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Write2Chars(char[] chars, int offset, int value)
		{
			chars[offset] = Console.Digit(value / 10);
			chars[offset + 1] = Console.Digit(value % 10);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0009A516 File Offset: 0x00098716
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char Digit(int value)
		{
			return (char)(value + 48);
		}

		// Token: 0x040014B5 RID: 5301
		private const int MAX_ELEMENTS = 500;

		// Token: 0x040014B6 RID: 5302
		private static int lineNum = 0;

		// Token: 0x040014B7 RID: 5303
		private static int errorNum = 0;

		// Token: 0x040014B8 RID: 5304
		private Text text;

		// Token: 0x040014B9 RID: 5305
		private static Queue<string> texts = new Queue<string>(500);

		// Token: 0x040014BA RID: 5306
		private static string log = "";

		// Token: 0x040014BB RID: 5307
		private static int errorRepeated = 0;

		// Token: 0x040014BC RID: 5308
		private static string lastError = "";

		// Token: 0x040014BD RID: 5309
		private static Transform textGroup;

		// Token: 0x040014BE RID: 5310
		public List<Text> textObj = new List<Text>();
	}
}
