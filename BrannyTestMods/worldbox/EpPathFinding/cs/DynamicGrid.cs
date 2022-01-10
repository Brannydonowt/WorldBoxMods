using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x0200030C RID: 780
	public class DynamicGrid : BaseGrid
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0009CCF3 File Offset: 0x0009AEF3
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x0009CD1C File Offset: 0x0009AF1C
		public override int width
		{
			get
			{
				if (this.m_notSet)
				{
					this.setBoundingBox();
				}
				return this.m_gridRect.maxX - this.m_gridRect.minX + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0009CD1E File Offset: 0x0009AF1E
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x0009CD47 File Offset: 0x0009AF47
		public override int height
		{
			get
			{
				if (this.m_notSet)
				{
					this.setBoundingBox();
				}
				return this.m_gridRect.maxY - this.m_gridRect.minY + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0009CD4C File Offset: 0x0009AF4C
		public DynamicGrid(List<GridPos> iWalkableGridList = null)
		{
			this.m_gridRect = new GridRect();
			this.m_gridRect.minX = 0;
			this.m_gridRect.minY = 0;
			this.m_gridRect.maxX = 0;
			this.m_gridRect.maxY = 0;
			this.m_notSet = true;
			this.buildNodes(iWalkableGridList);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0009CDA8 File Offset: 0x0009AFA8
		public DynamicGrid(DynamicGrid b) : base(b)
		{
			this.m_notSet = b.m_notSet;
			this.m_nodes = new Dictionary<GridPos, Node>(b.m_nodes);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0009CDD0 File Offset: 0x0009AFD0
		protected void buildNodes(List<GridPos> iWalkableGridList)
		{
			this.m_nodes = new Dictionary<GridPos, Node>();
			if (iWalkableGridList == null)
			{
				return;
			}
			foreach (GridPos gridPos in iWalkableGridList)
			{
				this.SetWalkableAt(gridPos.x, gridPos.y, true, 1);
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0009CE3C File Offset: 0x0009B03C
		public override Node GetNodeAt(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.GetNodeAt(iPos);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0009CE58 File Offset: 0x0009B058
		public override bool IsWalkableAt(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.IsWalkableAt(iPos);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0009CE74 File Offset: 0x0009B074
		private void setBoundingBox()
		{
			this.m_notSet = true;
			foreach (KeyValuePair<GridPos, Node> keyValuePair in this.m_nodes)
			{
				if (keyValuePair.Key.x < this.m_gridRect.minX || this.m_notSet)
				{
					this.m_gridRect.minX = keyValuePair.Key.x;
				}
				if (keyValuePair.Key.x > this.m_gridRect.maxX || this.m_notSet)
				{
					this.m_gridRect.maxX = keyValuePair.Key.x;
				}
				if (keyValuePair.Key.y < this.m_gridRect.minY || this.m_notSet)
				{
					this.m_gridRect.minY = keyValuePair.Key.y;
				}
				if (keyValuePair.Key.y > this.m_gridRect.maxY || this.m_notSet)
				{
					this.m_gridRect.maxY = keyValuePair.Key.y;
				}
				this.m_notSet = false;
			}
			this.m_notSet = false;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0009CFC8 File Offset: 0x0009B1C8
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			GridPos gridPos = new GridPos(iX, iY);
			if (iWalkable)
			{
				if (this.m_nodes.ContainsKey(gridPos))
				{
					return true;
				}
				if (iX < this.m_gridRect.minX || this.m_notSet)
				{
					this.m_gridRect.minX = iX;
				}
				if (iX > this.m_gridRect.maxX || this.m_notSet)
				{
					this.m_gridRect.maxX = iX;
				}
				if (iY < this.m_gridRect.minY || this.m_notSet)
				{
					this.m_gridRect.minY = iY;
				}
				if (iY > this.m_gridRect.maxY || this.m_notSet)
				{
					this.m_gridRect.maxY = iY;
				}
				this.m_nodes.Add(new GridPos(gridPos.x, gridPos.y), new Node(gridPos.x, gridPos.y, new bool?(iWalkable)));
			}
			else if (this.m_nodes.ContainsKey(gridPos))
			{
				this.m_nodes.Remove(gridPos);
				if (iX == this.m_gridRect.minX || iX == this.m_gridRect.maxX || iY == this.m_gridRect.minY || iY == this.m_gridRect.maxY)
				{
					this.m_notSet = true;
				}
			}
			return true;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0009D10B File Offset: 0x0009B30B
		public override Node GetNodeAt(GridPos iPos)
		{
			if (this.m_nodes.ContainsKey(iPos))
			{
				return this.m_nodes[iPos];
			}
			return null;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0009D129 File Offset: 0x0009B329
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.m_nodes.ContainsKey(iPos);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0009D137 File Offset: 0x0009B337
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0009D14D File Offset: 0x0009B34D
		public override void Reset()
		{
			this.Reset(null);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0009D158 File Offset: 0x0009B358
		public void Reset(List<GridPos> iWalkableGridList)
		{
			foreach (KeyValuePair<GridPos, Node> keyValuePair in this.m_nodes)
			{
				keyValuePair.Value.Reset(null);
			}
			if (iWalkableGridList == null)
			{
				return;
			}
			foreach (KeyValuePair<GridPos, Node> keyValuePair2 in this.m_nodes)
			{
				if (iWalkableGridList.Contains(keyValuePair2.Key))
				{
					this.SetWalkableAt(keyValuePair2.Key, true);
				}
				else
				{
					this.SetWalkableAt(keyValuePair2.Key, false);
				}
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0009D228 File Offset: 0x0009B428
		public override BaseGrid Clone()
		{
			DynamicGrid dynamicGrid = new DynamicGrid(null);
			foreach (KeyValuePair<GridPos, Node> keyValuePair in this.m_nodes)
			{
				dynamicGrid.SetWalkableAt(keyValuePair.Key.x, keyValuePair.Key.y, true, 1);
			}
			return dynamicGrid;
		}

		// Token: 0x040014F9 RID: 5369
		protected Dictionary<GridPos, Node> m_nodes;

		// Token: 0x040014FA RID: 5370
		private bool m_notSet;
	}
}
