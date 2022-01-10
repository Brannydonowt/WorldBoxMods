using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E9 RID: 745
public class ToggleIcon : MonoBehaviour
{
	// Token: 0x06001050 RID: 4176 RVA: 0x0008EC5E File Offset: 0x0008CE5E
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0008EC6C File Offset: 0x0008CE6C
	internal void updateIcon(bool pEnabled)
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
		if (pEnabled)
		{
			this.image.sprite = this.spriteON;
			return;
		}
		this.image.sprite = this.spriteOFF;
	}

	// Token: 0x0400135C RID: 4956
	public Sprite spriteON;

	// Token: 0x0400135D RID: 4957
	public Sprite spriteOFF;

	// Token: 0x0400135E RID: 4958
	private Image image;
}
