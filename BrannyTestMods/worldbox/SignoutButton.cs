using System;
using UnityEngine;

// Token: 0x020002B0 RID: 688
public class SignoutButton : MonoBehaviour
{
	// Token: 0x06000F1B RID: 3867 RVA: 0x00089D40 File Offset: 0x00087F40
	public void tryLogOut()
	{
		Auth.signOut();
		ScrollWindow.get("worldnet_logout").clickHide("right");
	}
}
