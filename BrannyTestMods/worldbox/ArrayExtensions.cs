using System;

// Token: 0x020001A2 RID: 418
public static class ArrayExtensions
{
	// Token: 0x06000997 RID: 2455 RVA: 0x00064C42 File Offset: 0x00062E42
	public static int IndexOf<T>(this T[] array, T value)
	{
		return Array.IndexOf<T>(array, value);
	}
}
