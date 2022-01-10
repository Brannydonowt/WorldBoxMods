using System;
using UnityEngine;

// Token: 0x02000203 RID: 515
public class CenterTipCaller : MonoBehaviour
{
	// Token: 0x06000B81 RID: 2945 RVA: 0x0006F27E File Offset: 0x0006D47E
	public void Show()
	{
		Tooltip.instance.show(base.gameObject, "normal", this.tip_title, this.tip_id);
	}

	// Token: 0x04000DA3 RID: 3491
	public string tip_title;

	// Token: 0x04000DA4 RID: 3492
	public string tip_id;
}
