using System;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class CustomWorldWarning : MonoBehaviour
{
	// Token: 0x06000D0A RID: 3338 RVA: 0x0007D115 File Offset: 0x0007B315
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0007D134 File Offset: 0x0007B334
	private void Update()
	{
		if (Config.customMapSize == "small" || Config.customMapSize == "tiny" || Config.customMapSize == "standard")
		{
			this.canvasGroup.alpha = 0f;
			return;
		}
		this.canvasGroup.alpha = 1f;
	}

	// Token: 0x04000FE8 RID: 4072
	private CanvasGroup canvasGroup;
}
