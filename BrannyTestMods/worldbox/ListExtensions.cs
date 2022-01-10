using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x020002F8 RID: 760
public static class ListExtensions
{
	// Token: 0x0600112D RID: 4397 RVA: 0x000966E9 File Offset: 0x000948E9
	public static string ToJson(this IList<string> list)
	{
		if (list.Count == 0)
		{
			return "[]";
		}
		return "['" + string.Join("','", list) + "']";
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x00096714 File Offset: 0x00094914
	public static void ShuffleHalf<T>(this IList<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int count = list.Count;
		int num = count / 2 + 1;
		int num2 = 0;
		while (num2 < num && num2 < count)
		{
			list.Swap(num2, ListExtensions.rnd.Next(num2, count));
			num2 += 2;
		}
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x0009675C File Offset: 0x0009495C
	public static void ShuffleN<T>(this IList<T> list, int pItems)
	{
		if (list.Count < 2)
		{
			return;
		}
		int num = (list.Count < pItems) ? list.Count : pItems;
		for (int i = 0; i < num; i++)
		{
			list.Swap(i, ListExtensions.rnd.Next(i, num));
		}
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x000967A8 File Offset: 0x000949A8
	public static void Shuffle<T>(this IList<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			list.Swap(i, ListExtensions.rnd.Next(i, count));
		}
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x000967E5 File Offset: 0x000949E5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this IList<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		list.Swap(0, ListExtensions.rnd.Next(0, list.Count));
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x00096809 File Offset: 0x00094A09
	public static void ShuffleOne<T>(this IList<T> list, int nItem)
	{
		if (list.Count < 2)
		{
			return;
		}
		if (list.Count < nItem + 1)
		{
			return;
		}
		list.Swap(nItem, ListExtensions.rnd.Next(nItem, list.Count));
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x00096839 File Offset: 0x00094A39
	public static void ShuffleLast<T>(this IList<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		list.Swap(list.Count - 1, ListExtensions.rnd.Next(0, list.Count));
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x00096864 File Offset: 0x00094A64
	public static void ShuffleRandomOne<T>(this IList<T> list)
	{
		if (list.Count < 2)
		{
			return;
		}
		int num = Toolbox.randomInt(0, list.Count - 1);
		list.Swap(num, ListExtensions.rnd.Next(num, list.Count));
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x000968A4 File Offset: 0x00094AA4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Swap<T>(this IList<T> list, int i, int j)
	{
		T value = list[i];
		list[i] = list[j];
		list[j] = value;
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000968CF File Offset: 0x00094ACF
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetRandom<T>(this IList<T> list)
	{
		return list[ListExtensions.rnd.Next(0, list.Count)];
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x000968E8 File Offset: 0x00094AE8
	public static void RemoveAtSwapBack<T>(this List<T> list, T pObject)
	{
		int num = list.IndexOf(pObject);
		if (num == -1)
		{
			return;
		}
		int num2 = list.Count - 1;
		list[num] = list[num2];
		list[num2] = pObject;
		list.RemoveAt(num2);
	}

	// Token: 0x04001452 RID: 5202
	private static Random rnd = new Random();
}
