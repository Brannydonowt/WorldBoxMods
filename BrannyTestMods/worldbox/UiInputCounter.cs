using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000271 RID: 625
public class UiInputCounter : MonoBehaviour
{
	// Token: 0x06000DD3 RID: 3539 RVA: 0x00082C44 File Offset: 0x00080E44
	private void Start()
	{
		this.nameText.onValueChanged.AddListener(delegate(string <p0>)
		{
			this.textChanged();
		});
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00082C62 File Offset: 0x00080E62
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.textChanged();
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00082C74 File Offset: 0x00080E74
	public void textChanged()
	{
		base.GetComponent<Text>().text = this.nameText.text.Length.ToString() + " / " + this.nameText.characterLimit.ToString();
	}

	// Token: 0x0400109C RID: 4252
	public InputField nameText;
}
