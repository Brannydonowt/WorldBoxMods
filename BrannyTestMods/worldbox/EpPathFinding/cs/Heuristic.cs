using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000313 RID: 787
	public class Heuristic
	{
		// Token: 0x06001266 RID: 4710 RVA: 0x0009E06B File Offset: 0x0009C26B
		public static float Manhattan(int iDx, int iDy)
		{
			return (float)iDx + (float)iDy;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0009E074 File Offset: 0x0009C274
		public static float Euclidean(int iDx, int iDy)
		{
			float num = (float)iDx;
			float num2 = (float)iDy;
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0009E093 File Offset: 0x0009C293
		public static float Chebyshev(int iDx, int iDy)
		{
			return (float)Math.Max(iDx, iDy);
		}
	}
}
