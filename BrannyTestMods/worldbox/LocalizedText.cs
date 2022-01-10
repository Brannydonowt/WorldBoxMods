using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Utils;

// Token: 0x020001BA RID: 442
public class LocalizedText : MonoBehaviour
{
	// Token: 0x060009F1 RID: 2545 RVA: 0x00066518 File Offset: 0x00064718
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00066526 File Offset: 0x00064726
	private void Start()
	{
		if (this.autoField)
		{
			LocalizedTextManager.addTextField(this);
			this.updateText(true);
		}
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x00066540 File Offset: 0x00064740
	internal virtual void updateText(bool pCheckText = true)
	{
		if (this.text == null)
		{
			return;
		}
		if (LocalizedTextManager.instance == null || !LocalizedTextManager.instance.initiated)
		{
			return;
		}
		if (LocalizedTextManager.currentFont != null)
		{
			this.text.font = LocalizedTextManager.currentFont;
		}
		string text = LocalizedTextManager.getText(this.key, this.text);
		if (this.convertToUppercase)
		{
			text = text.ToUpper();
		}
		if (this.specialTags)
		{
			if (text.Contains("$total_prem_powers$"))
			{
				text = text.Replace("$total_prem_powers$", GodPower.premiumPowers.Count.ToString() ?? "");
			}
			if (text.Contains("$minutes$"))
			{
				text = text.Replace("$minutes$", 30.ToString() ?? "");
			}
			if (text.Contains("$power$"))
			{
				text = text.Replace("$power$", LocalizedTextManager.getText(Config.powerToUnlock.name, null) ?? "");
			}
			if (text.Contains("$hours$"))
			{
				text = text.Replace("$hours$", 3.ToString() ?? "");
			}
			if (text.Contains("$number$"))
			{
				text = text.Replace("$number$", 3.ToString() ?? "");
			}
			if (text.Contains("$discord_count$"))
			{
				text = text.Replace("$discord_count$", "300.000");
			}
			if (text.Contains("$wbcode$"))
			{
				text = text.Replace("$wbcode$", "<color=cyan>WB-5555-1166-5555</color>");
			}
		}
		this.text.text = text;
		this.checkTextFont();
		if (pCheckText)
		{
			this.checkSpecialLanguages();
		}
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x000666F8 File Offset: 0x000648F8
	internal void checkTextFont()
	{
		if (this.text == null)
		{
			return;
		}
		if (LocalizedTextManager.currentFont == null)
		{
			return;
		}
		this.text.font = LocalizedTextManager.currentFont;
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00066728 File Offset: 0x00064928
	internal void checkSpecialLanguages()
	{
		if (this.text == null)
		{
			return;
		}
		this.checkTextFont();
		if (LocalizedTextManager.isRTLLang())
		{
			this.text.text = this.getRTLText(this.text.text);
		}
		if (LocalizedTextManager.isHindi())
		{
			if (!Regex.IsMatch(this.text.text, "[a-zA-Z]"))
			{
				this.text.SetHindiText(this.text.text);
			}
			this.text.fontStyle = 1;
		}
		if (LocalizedTextManager.isHanzi())
		{
			if (!this.hanziApplied && this.text.fontSize < 9)
			{
				this.fontStyleBefore = new FontStyle?(this.text.fontStyle);
				this.text.fontStyle = 0;
				if (this.text.gameObject.GetComponent<Shadow>() == null)
				{
					this.text.gameObject.AddComponent<Shadow>().effectColor = new Color(0f, 0f, 0f, 160f);
					this.shadowBefore = new bool?(false);
				}
				else
				{
					this.shadowBefore = new bool?(true);
				}
				this.hanziApplied = true;
				return;
			}
		}
		else if (this.hanziApplied)
		{
			if (this.fontStyleBefore != null)
			{
				this.text.fontStyle = this.fontStyleBefore.Value;
			}
			if (this.shadowBefore != null)
			{
				bool? flag = this.shadowBefore;
				bool flag2 = false;
				if ((flag.GetValueOrDefault() == flag2 & flag != null) && this.text.gameObject.GetComponent<Shadow>() != null)
				{
					Object.Destroy(this.text.gameObject.GetComponent<Shadow>());
				}
			}
			this.hanziApplied = false;
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x000668EC File Offset: 0x00064AEC
	private string getRTLText(string pString)
	{
		this.text.cachedTextGenerator.Populate(pString, this.text.GetGenerationSettings(this.text.rectTransform.rect.size));
		List<UILineInfo> list = this.text.cachedTextGenerator.lines as List<UILineInfo>;
		if (list == null)
		{
			return null;
		}
		string text = "";
		for (int i = 0; i < list.Count; i++)
		{
			if (i < list.Count - 1)
			{
				int startCharIdx = list[i].startCharIdx;
				int length = list[i + 1].startCharIdx - list[i].startCharIdx;
				text += pString.Substring(startCharIdx, length);
				if (text.Length > 0 && text[text.Length - 1] != '\n' && text[text.Length - 1] != '\r')
				{
					text += this.LineEnding.ToString();
				}
			}
			else
			{
				text += pString.Substring(list[i].startCharIdx);
			}
		}
		return UPersianUtils.RtlFix(text);
	}

	// Token: 0x04000C7C RID: 3196
	public bool convertToUppercase;

	// Token: 0x04000C7D RID: 3197
	public bool autoField = true;

	// Token: 0x04000C7E RID: 3198
	public bool specialTags;

	// Token: 0x04000C7F RID: 3199
	public string key = "??????";

	// Token: 0x04000C80 RID: 3200
	internal Text text;

	// Token: 0x04000C81 RID: 3201
	private bool hanziApplied;

	// Token: 0x04000C82 RID: 3202
	private bool? shadowBefore = new bool?(false);

	// Token: 0x04000C83 RID: 3203
	private FontStyle? fontStyleBefore;

	// Token: 0x04000C84 RID: 3204
	protected char LineEnding = '\n';
}
