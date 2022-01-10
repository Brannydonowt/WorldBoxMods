using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x0200030B RID: 779
	public abstract class BaseGrid
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x0009CAAB File Offset: 0x0009ACAB
		public BaseGrid()
		{
			this.m_gridRect = new GridRect();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0009CAC9 File Offset: 0x0009ACC9
		public BaseGrid(BaseGrid b)
		{
			this.m_gridRect = new GridRect(b.m_gridRect);
			this.width = b.width;
			this.height = b.height;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0009CB05 File Offset: 0x0009AD05
		public GridRect gridRect
		{
			get
			{
				return this.m_gridRect;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06001201 RID: 4609
		// (set) Token: 0x06001202 RID: 4610
		public abstract int width { get; protected set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06001203 RID: 4611
		// (set) Token: 0x06001204 RID: 4612
		public abstract int height { get; protected set; }

		// Token: 0x06001205 RID: 4613
		public abstract Node GetNodeAt(int iX, int iY);

		// Token: 0x06001206 RID: 4614
		public abstract bool IsWalkableAt(int iX, int iY);

		// Token: 0x06001207 RID: 4615
		public abstract bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1);

		// Token: 0x06001208 RID: 4616
		public abstract Node GetNodeAt(GridPos iPos);

		// Token: 0x06001209 RID: 4617
		public abstract bool IsWalkableAt(GridPos iPos);

		// Token: 0x0600120A RID: 4618
		public abstract bool SetWalkableAt(GridPos iPos, bool iWalkable);

		// Token: 0x0600120B RID: 4619 RVA: 0x0009CB10 File Offset: 0x0009AD10
		public List<Node> GetNeighbors(Node iNode, DiagonalMovement diagonalMovement)
		{
			int x = iNode.x;
			int y = iNode.y;
			List<Node> list = new List<Node>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			GridPos gridPos = new GridPos();
			if (this.IsWalkableAt(gridPos.Set(x, y - 1)))
			{
				list.Add(this.GetNodeAt(gridPos));
				flag = true;
			}
			if (this.IsWalkableAt(gridPos.Set(x + 1, y)))
			{
				list.Add(this.GetNodeAt(gridPos));
				flag3 = true;
			}
			if (this.IsWalkableAt(gridPos.Set(x, y + 1)))
			{
				list.Add(this.GetNodeAt(gridPos));
				flag5 = true;
			}
			if (this.IsWalkableAt(gridPos.Set(x - 1, y)))
			{
				list.Add(this.GetNodeAt(gridPos));
				flag7 = true;
			}
			switch (diagonalMovement)
			{
			case DiagonalMovement.Always:
				flag2 = true;
				flag4 = true;
				flag6 = true;
				flag8 = true;
				break;
			case DiagonalMovement.IfAtLeastOneWalkable:
				flag2 = (flag7 || flag);
				flag4 = (flag || flag3);
				flag6 = (flag3 || flag5);
				flag8 = (flag5 || flag7);
				break;
			case DiagonalMovement.OnlyWhenNoObstacles:
				flag2 = (flag7 && flag);
				flag4 = (flag && flag3);
				flag6 = (flag3 && flag5);
				flag8 = (flag5 && flag7);
				break;
			}
			if (flag2 && this.IsWalkableAt(gridPos.Set(x - 1, y - 1)))
			{
				list.Add(this.GetNodeAt(gridPos));
			}
			if (flag4 && this.IsWalkableAt(gridPos.Set(x + 1, y - 1)))
			{
				list.Add(this.GetNodeAt(gridPos));
			}
			if (flag6 && this.IsWalkableAt(gridPos.Set(x + 1, y + 1)))
			{
				list.Add(this.GetNodeAt(gridPos));
			}
			if (flag8 && this.IsWalkableAt(gridPos.Set(x - 1, y + 1)))
			{
				list.Add(this.GetNodeAt(gridPos));
			}
			return list;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0009CCD7 File Offset: 0x0009AED7
		public void addToClosed(Node pNode)
		{
			this.closedList.Add(pNode);
			this.closed_list_count++;
		}

		// Token: 0x0600120D RID: 4621
		public abstract void Reset();

		// Token: 0x0600120E RID: 4622
		public abstract BaseGrid Clone();

		// Token: 0x040014F5 RID: 5365
		public List<Node> closedList = new List<Node>();

		// Token: 0x040014F6 RID: 5366
		public int closed_list_count;

		// Token: 0x040014F7 RID: 5367
		public const int CLOSED_LIST_MINIMUM_ELEMENTS = 10;

		// Token: 0x040014F8 RID: 5368
		protected GridRect m_gridRect;
	}
}
