using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009D RID: 157
[Serializable]
public class PrintTemplate
{
	// Token: 0x0400054F RID: 1359
	public string name;

	// Token: 0x04000550 RID: 1360
	public Texture2D graphics;

	// Token: 0x04000551 RID: 1361
	internal List<PrintStep> steps;

	// Token: 0x04000552 RID: 1362
	internal int stepsPerTick = 1;
}
