using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B8 RID: 696
public class StatBar : MonoBehaviour
{
	// Token: 0x06000F4D RID: 3917 RVA: 0x0008A4C4 File Offset: 0x000886C4
	public void setBar(float pVal, float pMax, string pString)
	{
		this.bar_width = this.bar.rect.width;
		float x;
		if (pMax != 0f)
		{
			x = Mathf.Floor(pVal / pMax * this.bar_width);
		}
		else
		{
			x = this.bar_width;
		}
		this.mask.sizeDelta = new Vector2(x, this.mask.sizeDelta.y);
		this.textField.text = pString;
	}

	// Token: 0x04001232 RID: 4658
	public Text textField;

	// Token: 0x04001233 RID: 4659
	public RectTransform mask;

	// Token: 0x04001234 RID: 4660
	public RectTransform bar;

	// Token: 0x04001235 RID: 4661
	private float bar_width = 70f;
}
