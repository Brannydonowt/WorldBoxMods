using System;
using System.Collections.Generic;

// Token: 0x020002F7 RID: 759
public static class List
{
	// Token: 0x0600112C RID: 4396 RVA: 0x000966E1 File Offset: 0x000948E1
	public static List<T> Of<T>(params T[] args)
	{
		return new List<T>(args);
	}
}
