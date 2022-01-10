using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x0200030E RID: 782
	public class PartialGridWPool : BaseGrid
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0009D6AD File Offset: 0x0009B8AD
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x0009D6C8 File Offset: 0x0009B8C8
		public override int width
		{
			get
			{
				return this.m_gridRect.maxX - this.m_gridRect.minX + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0009D6CA File Offset: 0x0009B8CA
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x0009D6E5 File Offset: 0x0009B8E5
		public override int height
		{
			get
			{
				return this.m_gridRect.maxY - this.m_gridRect.minY + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0009D6E7 File Offset: 0x0009B8E7
		public PartialGridWPool(NodePool iNodePool, GridRect iGridRect = null)
		{
			if (iGridRect == null)
			{
				this.m_gridRect = new GridRect();
			}
			else
			{
				this.m_gridRect = iGridRect;
			}
			this.m_nodePool = iNodePool;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0009D713 File Offset: 0x0009B913
		public PartialGridWPool(PartialGridWPool b) : base(b)
		{
			this.m_nodePool = b.m_nodePool;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0009D728 File Offset: 0x0009B928
		public void SetGridRect(GridRect iGridRect)
		{
			this.m_gridRect = iGridRect;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0009D731 File Offset: 0x0009B931
		public bool IsInside(int iX, int iY)
		{
			return iX >= this.m_gridRect.minX && iX <= this.m_gridRect.maxX && iY >= this.m_gridRect.minY && iY <= this.m_gridRect.maxY;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0009D770 File Offset: 0x0009B970
		public override Node GetNodeAt(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.GetNodeAt(iPos);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0009D78C File Offset: 0x0009B98C
		public override bool IsWalkableAt(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.IsWalkableAt(iPos);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0009D7A8 File Offset: 0x0009B9A8
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			if (!this.IsInside(iX, iY))
			{
				return false;
			}
			GridPos iPos = new GridPos(iX, iY);
			this.m_nodePool.SetNode(iPos, new bool?(iWalkable));
			return true;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0009D7DD File Offset: 0x0009B9DD
		public bool IsInside(GridPos iPos)
		{
			return this.IsInside(iPos.x, iPos.y);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0009D7F1 File Offset: 0x0009B9F1
		public override Node GetNodeAt(GridPos iPos)
		{
			if (!this.IsInside(iPos))
			{
				return null;
			}
			return this.m_nodePool.GetNode(iPos);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0009D80A File Offset: 0x0009BA0A
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.IsInside(iPos) && this.m_nodePool.Nodes.ContainsKey(iPos);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0009D828 File Offset: 0x0009BA28
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0009D840 File Offset: 0x0009BA40
		public override void Reset()
		{
			int num = (this.m_gridRect.maxX - this.m_gridRect.minX) * (this.m_gridRect.maxY - this.m_gridRect.minY);
			if (this.m_nodePool.Nodes.Count > num)
			{
				GridPos gridPos = new GridPos(0, 0);
				for (int i = this.m_gridRect.minX; i <= this.m_gridRect.maxX; i++)
				{
					gridPos.x = i;
					for (int j = this.m_gridRect.minY; j <= this.m_gridRect.maxY; j++)
					{
						gridPos.y = j;
						Node node = this.m_nodePool.GetNode(gridPos);
						if (node != null)
						{
							node.Reset(null);
						}
					}
				}
				return;
			}
			foreach (KeyValuePair<GridPos, Node> keyValuePair in this.m_nodePool.Nodes)
			{
				keyValuePair.Value.Reset(null);
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0009D970 File Offset: 0x0009BB70
		public override BaseGrid Clone()
		{
			return new PartialGridWPool(this.m_nodePool, this.m_gridRect);
		}

		// Token: 0x040014FD RID: 5373
		private NodePool m_nodePool;
	}
}
