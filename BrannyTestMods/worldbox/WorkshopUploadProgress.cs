using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
internal class WorkshopUploadProgress : IProgress<float>
{
	// Token: 0x06000C8A RID: 3210 RVA: 0x0007ACE1 File Offset: 0x00078EE1
	public void Report(float value)
	{
		if (this.lastvalue >= value)
		{
			return;
		}
		this.lastvalue = value;
		WorkshopMaps.uploadProgress = this.lastvalue;
		Debug.Log(value);
	}

	// Token: 0x04000F58 RID: 3928
	internal float lastvalue;
}
