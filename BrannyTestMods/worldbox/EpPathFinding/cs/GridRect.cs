using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000311 RID: 785
	public class GridRect
	{
		// Token: 0x0600125D RID: 4701 RVA: 0x0009DEA7 File Offset: 0x0009C0A7
		public GridRect()
		{
			this.minX = 0;
			this.minY = 0;
			this.maxX = 0;
			this.maxY = 0;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0009DECB File Offset: 0x0009C0CB
		public GridRect(int iMinX, int iMinY, int iMaxX, int iMaxY)
		{
			this.minX = iMinX;
			this.minY = iMinY;
			this.maxX = iMaxX;
			this.maxY = iMaxY;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0009DEF0 File Offset: 0x0009C0F0
		public GridRect(GridRect b)
		{
			this.minX = b.minX;
			this.minY = b.minY;
			this.maxX = b.maxX;
			this.maxY = b.maxY;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0009DF28 File Offset: 0x0009C128
		public override int GetHashCode()
		{
			return this.minX ^ this.minY ^ this.maxX ^ this.maxY;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0009DF48 File Offset: 0x0009C148
		public override bool Equals(object obj)
		{
			GridRect gridRect = (GridRect)obj;
			return gridRect != null && (this.minX == gridRect.minX && this.minY == gridRect.minY && this.maxX == gridRect.maxX) && this.maxY == gridRect.maxY;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0009DF9C File Offset: 0x0009C19C
		public bool Equals(GridRect p)
		{
			return p != null && (this.minX == p.minX && this.minY == p.minY && this.maxX == p.maxX) && this.maxY == p.maxY;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0009DFE8 File Offset: 0x0009C1E8
		public static bool operator ==(GridRect a, GridRect b)
		{
			return a == b || (a != null && b != null && (a.minX == b.minX && a.minY == b.minY && a.maxX == b.maxX) && a.maxY == b.maxY);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0009E03F File Offset: 0x0009C23F
		public static bool operator !=(GridRect a, GridRect b)
		{
			return !(a == b);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0009E04B File Offset: 0x0009C24B
		public GridRect Set(int iMinX, int iMinY, int iMaxX, int iMaxY)
		{
			this.minX = iMinX;
			this.minY = iMinY;
			this.maxX = iMaxX;
			this.maxY = iMaxY;
			return this;
		}

		// Token: 0x04001503 RID: 5379
		public int minX;

		// Token: 0x04001504 RID: 5380
		public int minY;

		// Token: 0x04001505 RID: 5381
		public int maxX;

		// Token: 0x04001506 RID: 5382
		public int maxY;
	}
}
