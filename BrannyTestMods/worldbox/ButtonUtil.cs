using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200024F RID: 591
public class ButtonUtil : MonoBehaviour
{
	// Token: 0x06000CDD RID: 3293 RVA: 0x0007B978 File Offset: 0x00079B78
	public void ResetState()
	{
		if (this._button == null)
		{
			this._button = base.GetComponent<Button>();
			this._button.onClick.AddListener(new UnityAction(this.playSound));
		}
		this._button.enabled = false;
		this._button.enabled = true;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0007B9D3 File Offset: 0x00079BD3
	private void playSound()
	{
		Sfx.play("click", true, -1f, -1f);
	}

	// Token: 0x04000FB4 RID: 4020
	private Button _button;
}
