using System;
using UnityEngine;

// Token: 0x020001AD RID: 429
public static class Epoch
{
	// Token: 0x060009B7 RID: 2487 RVA: 0x00065624 File Offset: 0x00063824
	public static double Current()
	{
		return (DateTime.UtcNow - Epoch.epochStart).TotalSeconds;
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00065648 File Offset: 0x00063848
	public static double SecondsElapsed(double t1)
	{
		return (double)Mathf.Abs((float)(Epoch.Current() - t1));
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00065658 File Offset: 0x00063858
	public static int SecondsElapsed(int t1, int t2)
	{
		return Mathf.Abs(t1 - t2);
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00065664 File Offset: 0x00063864
	internal static DateTime toDateTime(double epoch)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch);
	}

	// Token: 0x04000C61 RID: 3169
	private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
