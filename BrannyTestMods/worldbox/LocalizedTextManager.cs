using System;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.UI;

// Token: 0x020001BD RID: 445
public class LocalizedTextManager
{
	// Token: 0x060009FA RID: 2554 RVA: 0x00066A6F File Offset: 0x00064C6F
	public static void init()
	{
		if (LocalizedTextManager.instance != null)
		{
			return;
		}
		LocalizedTextManager.instance = new LocalizedTextManager();
		LocalizedTextManager.instance.create();
		LocalizedTextManager.instance.setLanguage(PlayerConfig.dict["language"].stringVal);
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060009FB RID: 2555 RVA: 0x00066AAB File Offset: 0x00064CAB
	public Font persianFont
	{
		get
		{
			if (this.m_persianFont == null)
			{
				this.m_persianFont = (Font)Resources.Load("Fonts/Tajawal-Bold", typeof(Font));
			}
			return this.m_persianFont;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060009FC RID: 2556 RVA: 0x00066AE0 File Offset: 0x00064CE0
	public Font thaiFont
	{
		get
		{
			if (this.m_thaiFont == null)
			{
				this.m_thaiFont = (Font)Resources.Load("Fonts/krubbold", typeof(Font));
			}
			return this.m_thaiFont;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060009FD RID: 2557 RVA: 0x00066B15 File Offset: 0x00064D15
	public Font japaneseFont
	{
		get
		{
			if (this.m_japaneseFont == null)
			{
				this.m_japaneseFont = (Font)Resources.Load("Fonts/MPLUSRounded1c-Medium", typeof(Font));
			}
			return this.m_japaneseFont;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060009FE RID: 2558 RVA: 0x00066B4A File Offset: 0x00064D4A
	public Font koreanFont
	{
		get
		{
			if (this.m_koreanFont == null)
			{
				this.m_koreanFont = (Font)Resources.Load("Fonts/NanumGothicCoding-Bold", typeof(Font));
			}
			return this.m_koreanFont;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060009FF RID: 2559 RVA: 0x00066B7F File Offset: 0x00064D7F
	public Font hindiFont
	{
		get
		{
			if (this.m_hindiFont == null)
			{
				this.m_hindiFont = (Font)Resources.Load("Fonts/Poppins-Regular", typeof(Font));
			}
			return this.m_hindiFont;
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00066BB4 File Offset: 0x00064DB4
	private void create()
	{
		this.defaultFont = (Font)Resources.Load("Fonts/Roboto-Bold", typeof(Font));
		LocalizedTextManager.instance = this;
		this.texts = new List<LocalizedText>();
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00066BE6 File Offset: 0x00064DE6
	public bool contains(string pString)
	{
		return LocalizedTextManager.instance.localizedText.ContainsKey(pString);
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00066BF8 File Offset: 0x00064DF8
	public static void addTextField(LocalizedText pText)
	{
		LocalizedTextManager.instance.texts.Add(pText);
		LocalizedTextManager.instance.langDirty = true;
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00066C15 File Offset: 0x00064E15
	public static void removeTextField(LocalizedText pText)
	{
		if (LocalizedTextManager.instance == null)
		{
			return;
		}
		LocalizedTextManager.instance.texts.Remove(pText);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00066C30 File Offset: 0x00064E30
	public static void updateTexts()
	{
		Debug.Log("LocalizedTextManager: total texts loaded: " + LocalizedTextManager.instance.texts.Count.ToString());
		foreach (LocalizedText localizedText in LocalizedTextManager.instance.texts)
		{
			localizedText.updateText(true);
		}
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00066CAC File Offset: 0x00064EAC
	public static bool stringExists(string pKey)
	{
		return LocalizedTextManager.instance.localizedText.ContainsKey(pKey);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00066CC4 File Offset: 0x00064EC4
	public static string getText(string pKey, Text text = null)
	{
		string text2 = "";
		if (LocalizedTextManager.instance.language == "boat")
		{
			return LocalizedTextManager.transformToBoat(text2);
		}
		if (LocalizedTextManager.instance.localizedText.ContainsKey(pKey))
		{
			text2 = LocalizedTextManager.instance.localizedText[pKey];
		}
		else
		{
			text2 = pKey;
			if (pKey.Contains("_placeholder"))
			{
				text2 = "";
			}
			else
			{
				Debug.LogError("LocalizedTextManager: missing text: " + pKey, text);
			}
		}
		if (pKey.StartsWith("world_law_", StringComparison.Ordinal))
		{
			text2 = text2.Replace("\n\n", "\n");
		}
		return text2;
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00066D64 File Offset: 0x00064F64
	public static string transformToBoat(string pText)
	{
		int num = pText.Split(new char[]
		{
			' '
		}).Length + 1;
		string text = "";
		for (int i = 0; i < num; i++)
		{
			if (text.Length > 0)
			{
				text += " ";
			}
			if (Toolbox.randomBool())
			{
				if (Toolbox.randomBool())
				{
					text += "Boat";
				}
				else if (Toolbox.randomBool())
				{
					text += "Aye";
				}
				else
				{
					text += "Argh";
				}
			}
			else if (Toolbox.randomBool())
			{
				text += "Ahoy";
			}
			else if (Toolbox.randomBool())
			{
				text += "boat";
			}
			else
			{
				text += "ye";
			}
		}
		return text;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00066E2C File Offset: 0x0006502C
	public void loadLocalizedText(string pLocaleID)
	{
		this.initiated = true;
		LocalizedTextManager.instance.localizedText = new Dictionary<string, string>();
		string path = "locales/" + pLocaleID;
		TextAsset textAsset;
		try
		{
			textAsset = (Resources.Load(path) as TextAsset);
		}
		catch (Exception)
		{
			textAsset = (Resources.Load("locales/en") as TextAsset);
		}
		Dictionary<string, object> dictionary = Json.Deserialize(textAsset.text) as Dictionary<string, object>;
		foreach (string text in dictionary.Keys)
		{
			this.localizedText.Add(text, dictionary[text] as string);
		}
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x00066EF4 File Offset: 0x000650F4
	public void setLanguage(string pLanguage)
	{
		if (this.language == pLanguage && !this.langDirty)
		{
			return;
		}
		this.langDirty = false;
		Debug.Log("LOAD LANGUAGE " + pLanguage);
		string path = "locales/" + pLanguage;
		if (Resources.Load(path) == null)
		{
			pLanguage = PlayerConfig.detectLanguage();
		}
		Resources.Load(path);
		bool flag = false;
		if (this.language != "not_set")
		{
			flag = true;
		}
		this.language = pLanguage;
		LocalizedTextManager.instance.loadLocalizedText(pLanguage);
		try
		{
			RestClient.DefaultRequestHeaders["wb-language"] = (this.language ?? "na");
		}
		catch (Exception)
		{
		}
		if (this.language == "fa" || this.language == "ar" || this.language == "ka")
		{
			LocalizedTextManager.currentFont = this.persianFont;
		}
		else if (this.language == "th")
		{
			LocalizedTextManager.currentFont = this.thaiFont;
		}
		else if (this.language == "ko")
		{
			LocalizedTextManager.currentFont = this.koreanFont;
		}
		else if (this.language == "ja")
		{
			LocalizedTextManager.currentFont = this.japaneseFont;
		}
		else if (this.language == "hi")
		{
			LocalizedTextManager.currentFont = this.hindiFont;
		}
		else
		{
			LocalizedTextManager.currentFont = this.defaultFont;
		}
		LocalizedTextManager.updateTexts();
		if (PlayerConfig.dict["language"].stringVal != pLanguage)
		{
			flag = true;
		}
		PlayerConfig.dict["language"].stringVal = pLanguage;
		if (flag)
		{
			PlayerConfig.saveData();
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x000670C0 File Offset: 0x000652C0
	public static bool isHindi()
	{
		return LocalizedTextManager.instance != null && LocalizedTextManager.instance.language == "hi";
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x000670E4 File Offset: 0x000652E4
	public static bool isHanzi()
	{
		return LocalizedTextManager.instance != null && (LocalizedTextManager.instance.language == "ch" || LocalizedTextManager.instance.language == "cz" || LocalizedTextManager.instance.language == "ja");
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00067144 File Offset: 0x00065344
	public static bool isRTLLang()
	{
		return LocalizedTextManager.instance != null && (LocalizedTextManager.instance.language == "fa" || LocalizedTextManager.instance.language == "he" || LocalizedTextManager.instance.language == "ar");
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x000671A0 File Offset: 0x000653A0
	internal static string langToCulture()
	{
		string text = LocalizedTextManager.instance.language;
		if (text == "lu")
		{
			return "";
		}
		if (text == "ka")
		{
			return "";
		}
		if (text == "gr")
		{
			return "";
		}
		if (text == "hr")
		{
			return "";
		}
		if (text == "boat")
		{
			return "";
		}
		if (text == "ch")
		{
			return "zh-Hant";
		}
		if (text == "cz")
		{
			return "zh-Hans";
		}
		if (text == "fn")
		{
			return "fi-FI";
		}
		if (text == "ph")
		{
			return "fil-PH";
		}
		if (text == "gr")
		{
			return "fi";
		}
		if (text == "br")
		{
			return "pt";
		}
		if (text == "ko")
		{
			return "ko-KR";
		}
		if (text == "th")
		{
			return "th-TH";
		}
		if (text == "ua")
		{
			return "uk";
		}
		if (text == "no")
		{
			return "nb-NO";
		}
		if (text == "vn")
		{
			return "vi";
		}
		return text;
	}

	// Token: 0x04000C88 RID: 3208
	private string missingTextString = "LOC_ER";

	// Token: 0x04000C89 RID: 3209
	public static LocalizedTextManager instance;

	// Token: 0x04000C8A RID: 3210
	internal List<LocalizedText> texts;

	// Token: 0x04000C8B RID: 3211
	internal string language = "not_set";

	// Token: 0x04000C8C RID: 3212
	private Dictionary<string, string> localizedText;

	// Token: 0x04000C8D RID: 3213
	private Font m_defaultFont;

	// Token: 0x04000C8E RID: 3214
	public Font defaultFont;

	// Token: 0x04000C8F RID: 3215
	private Font m_persianFont;

	// Token: 0x04000C90 RID: 3216
	private Font m_thaiFont;

	// Token: 0x04000C91 RID: 3217
	private Font m_japaneseFont;

	// Token: 0x04000C92 RID: 3218
	private Font m_koreanFont;

	// Token: 0x04000C93 RID: 3219
	private Font m_hindiFont;

	// Token: 0x04000C94 RID: 3220
	public static Font currentFont;

	// Token: 0x04000C95 RID: 3221
	internal bool initiated;

	// Token: 0x04000C96 RID: 3222
	private bool langDirty;
}
