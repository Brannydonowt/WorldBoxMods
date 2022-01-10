using System;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class LoadingScreenOut : MonoBehaviour
{
	// Token: 0x06000D23 RID: 3363 RVA: 0x0007DBDE File Offset: 0x0007BDDE
	private void Update()
	{
		this.canvasGroup.alpha -= Time.deltaTime * 2f;
		if (this.canvasGroup.alpha <= 0f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001006 RID: 4102
	public CanvasGroup canvasGroup;
}
