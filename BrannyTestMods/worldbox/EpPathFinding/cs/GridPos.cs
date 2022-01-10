using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000310 RID: 784
	public class GridPos : IEquatable<GridPos>
	{
		// Token: 0x06001253 RID: 4691 RVA: 0x0009DD81 File Offset: 0x0009BF81
		public GridPos()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0009DD97 File Offset: 0x0009BF97
		public GridPos(int iX, int iY)
		{
			this.x = iX;
			this.y = iY;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0009DDAD File Offset: 0x0009BFAD
		public GridPos(GridPos b)
		{
			this.x = b.x;
			this.y = b.y;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0009DDCD File Offset: 0x0009BFCD
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0009DDDC File Offset: 0x0009BFDC
		public override bool Equals(object obj)
		{
			GridPos gridPos = (GridPos)obj;
			return gridPos != null && this.x == gridPos.x && this.y == gridPos.y;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0009DE13 File Offset: 0x0009C013
		public bool Equals(GridPos p)
		{
			return p != null && this.x == p.x && this.y == p.y;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0009DE38 File Offset: 0x0009C038
		public static bool operator ==(GridPos a, GridPos b)
		{
			return a == b || (a != null && b != null && a.x == b.x && a.y == b.y);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0009DE68 File Offset: 0x0009C068
		public static bool operator !=(GridPos a, GridPos b)
		{
			return !(a == b);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0009DE74 File Offset: 0x0009C074
		public GridPos Set(int iX, int iY)
		{
			this.x = iX;
			this.y = iY;
			return this;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0009DE85 File Offset: 0x0009C085
		public override string ToString()
		{
			return string.Format("({0},{1})", this.x, this.y);
		}

		// Token: 0x04001501 RID: 5377
		public int x;

		// Token: 0x04001502 RID: 5378
		public int y;
	}
}
