using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000005 RID: 5
public static class TextExtension
{
	// Token: 0x06000007 RID: 7 RVA: 0x00003124 File Offset: 0x00001324
	public static void SetHindiText(this Text text, string value)
	{
		if (TextExtension.krutiDev == null)
		{
			TextExtension.krutiDev = (Resources.Load("CD_Kruti_Dev_010") as Font);
		}
		bool flag = value.IndexOf("</color>") > -1;
		if (flag)
		{
			TextExtension.colors.Clear();
			value = value.Replace("</color>", "END_COLOR");
			int num = 0;
			foreach (object obj in Regex.Matches(value, "<color.*?>"))
			{
				TextExtension.colors.Add(obj.ToString());
				value = value.Replace(obj.ToString(), "COLOR_" + num++.ToString());
			}
		}
		if (value.IndexOf("'") > -1)
		{
			value = value.Replace("'", "SINGLE_QUOTE");
		}
		value = HindiCorrector.GetCorrectedHindiText(value);
		if (value.IndexOf("SINGLE_QUOTE") > -1)
		{
			value = value.Replace("SINGLE_QUOTE", "'");
		}
		if (flag)
		{
			value = value.Replace("END_COLOR", "</color>");
			int num2 = 0;
			foreach (string newValue in TextExtension.colors)
			{
				value = value.Replace("COLOR_" + num2++.ToString(), newValue);
			}
		}
		text.font = TextExtension.krutiDev;
		text.text = value;
	}

	// Token: 0x04000009 RID: 9
	private static Font krutiDev;

	// Token: 0x0400000A RID: 10
	private static List<string> colors = new List<string>();
}
