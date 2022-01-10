using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x0200030D RID: 781
	public class DynamicGridWPool : BaseGrid
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0009D2A0 File Offset: 0x0009B4A0
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x0009D2C9 File Offset: 0x0009B4C9
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0009D2CB File Offset: 0x0009B4CB
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x0009D2F4 File Offset: 0x0009B4F4
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

		// Token: 0x06001224 RID: 4644 RVA: 0x0009D2F8 File Offset: 0x0009B4F8
		public DynamicGridWPool(NodePool iNodePool)
		{
			this.m_gridRect = new GridRect();
			this.m_gridRect.minX = 0;
			this.m_gridRect.minY = 0;
			this.m_gridRect.maxX = 0;
			this.m_gridRect.maxY = 0;
			this.m_notSet = true;
			this.m_nodePool = iNodePool;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0009D354 File Offset: 0x0009B554
		public DynamicGridWPool(DynamicGridWPool b) : base(b)
		{
			this.m_notSet = b.m_notSet;
			this.m_nodePool = b.m_nodePool;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0009D378 File Offset: 0x0009B578
		public override Node GetNodeAt(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.GetNodeAt(iPos);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0009D394 File Offset: 0x0009B594
		public override bool IsWalkableAt(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.IsWalkableAt(iPos);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0009D3B0 File Offset: 0x0009B5B0
		private void setBoundingBox()
		{
			this.m_notSet = true;
			foreach (KeyValuePair<GridPos, Node> keyValuePair in this.m_nodePool.Nodes)
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

		// Token: 0x06001229 RID: 4649 RVA: 0x0009D508 File Offset: 0x0009B708
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			GridPos iPos = new GridPos(iX, iY);
			this.m_nodePool.SetNode(iPos, new bool?(iWalkable));
			if (iWalkable)
			{
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
			}
			else if (iX == this.m_gridRect.minX || iX == this.m_gridRect.maxX || iY == this.m_gridRect.minY || iY == this.m_gridRect.maxY)
			{
				this.m_notSet = true;
			}
			return true;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0009D600 File Offset: 0x0009B800
		public override Node GetNodeAt(GridPos iPos)
		{
			return this.m_nodePool.GetNode(iPos);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0009D60E File Offset: 0x0009B80E
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.m_nodePool.Nodes.ContainsKey(iPos);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0009D621 File Offset: 0x0009B821
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0009D638 File Offset: 0x0009B838
		public override void Reset()
		{
			foreach (KeyValuePair<GridPos, Node> keyValuePair in this.m_nodePool.Nodes)
			{
				keyValuePair.Value.Reset(null);
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0009D6A0 File Offset: 0x0009B8A0
		public override BaseGrid Clone()
		{
			return new DynamicGridWPool(this.m_nodePool);
		}

		// Token: 0x040014FB RID: 5371
		private bool m_notSet;

		// Token: 0x040014FC RID: 5372
		private NodePool m_nodePool;
	}
}
