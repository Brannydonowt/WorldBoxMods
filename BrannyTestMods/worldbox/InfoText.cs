using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class InfoText : MonoBehaviour
{
	// Token: 0x060009DF RID: 2527 RVA: 0x00065E9F File Offset: 0x0006409F
	private void Start()
	{
		this.text.gameObject.GetComponent<Renderer>().sortingOrder = 1000;
		this.shadow.gameObject.GetComponent<Renderer>().sortingOrder = 999;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x00065ED5 File Offset: 0x000640D5
	public void setText(string pText)
	{
		this.text.text = pText;
		this.shadow.text = pText;
	}

	// Token: 0x04000C6B RID: 3179
	public TextMesh text;

	// Token: 0x04000C6C RID: 3180
	public TextMesh shadow;
}
