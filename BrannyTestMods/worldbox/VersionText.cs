using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B6 RID: 694
public class VersionText : MonoBehaviour
{
	// Token: 0x06000F40 RID: 3904 RVA: 0x0008A333 File Offset: 0x00088533
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x0008A341 File Offset: 0x00088541
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.text.GetComponent<LocalizedText>().updateText(true);
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x0008A35C File Offset: 0x0008855C
	private void Update()
	{
		if (this.text == null)
		{
			return;
		}
		this.text.text = this.text.text.Replace("$old_version$", this.oldText(Config.gv));
		this.text.text = this.text.text.Replace("$new_version$", this.newText(VersionCheck.onlineVersion));
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x0008A3CE File Offset: 0x000885CE
	private string oldText(string pText)
	{
		return "<color=#FF0000>" + pText + "</color>";
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x0008A3E0 File Offset: 0x000885E0
	private string newText(string pText)
	{
		return "<color=#00FF00>" + pText + "</color>";
	}

	// Token: 0x0400122E RID: 4654
	internal Text text;
}
