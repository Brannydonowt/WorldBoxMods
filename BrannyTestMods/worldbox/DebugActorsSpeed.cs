using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class DebugActorsSpeed : MonoBehaviour
{
	// Token: 0x06000C8C RID: 3212 RVA: 0x0007AD12 File Offset: 0x00078F12
	private void Update()
	{
		Config.actorFastMode = (float)this.speedup;
	}

	// Token: 0x04000F59 RID: 3929
	public int speedup = 20;
}
