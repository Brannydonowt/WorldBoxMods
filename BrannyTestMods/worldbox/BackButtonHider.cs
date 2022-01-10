using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
internal class BackButtonHider : MonoBehaviour
{
	// Token: 0x06000FAA RID: 4010 RVA: 0x0008BDC2 File Offset: 0x00089FC2
	private void OnEnable()
	{
		if (WindowHistory.list.Count >= 1)
		{
			base.gameObject.SetActive(true);
			return;
		}
		base.gameObject.SetActive(false);
	}
}
