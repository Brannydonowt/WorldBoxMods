using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000292 RID: 658
public class WorldLogElement : MonoBehaviour
{
	// Token: 0x06000E89 RID: 3721 RVA: 0x0008757B File Offset: 0x0008577B
	public void clickLocate()
	{
		ref this.message.jumpToLocation();
	}

	// Token: 0x04001162 RID: 4450
	public Text date;

	// Token: 0x04001163 RID: 4451
	public Text description;

	// Token: 0x04001164 RID: 4452
	public Image icon;

	// Token: 0x04001165 RID: 4453
	public GameObject locate;

	// Token: 0x04001166 RID: 4454
	public GameObject follow;

	// Token: 0x04001167 RID: 4455
	public WorldLogMessage message;
}
