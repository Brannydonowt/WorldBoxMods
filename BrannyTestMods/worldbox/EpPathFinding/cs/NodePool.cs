using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x02000314 RID: 788
	public class NodePool
	{
		// Token: 0x0600126A RID: 4714 RVA: 0x0009E0A5 File Offset: 0x0009C2A5
		public NodePool()
		{
			this.m_nodes = new Dictionary<GridPos, Node>();
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0009E0B8 File Offset: 0x0009C2B8
		public Dictionary<GridPos, Node> Nodes
		{
			get
			{
				return this.m_nodes;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0009E0C0 File Offset: 0x0009C2C0
		public Node GetNode(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.GetNode(iPos);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0009E0DC File Offset: 0x0009C2DC
		public Node GetNode(GridPos iPos)
		{
			Node result = null;
			this.m_nodes.TryGetValue(iPos, ref result);
			return result;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0009E0FC File Offset: 0x0009C2FC
		public Node SetNode(int iX, int iY, bool? iWalkable = null)
		{
			GridPos iPos = new GridPos(iX, iY);
			return this.SetNode(iPos, iWalkable);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0009E11C File Offset: 0x0009C31C
		public Node SetNode(GridPos iPos, bool? iWalkable = null)
		{
			if (iWalkable == null)
			{
				Node node = new Node(iPos.x, iPos.y, new bool?(true));
				this.m_nodes.Add(iPos, node);
				return node;
			}
			if (!iWalkable.Value)
			{
				this.removeNode(iPos);
				return null;
			}
			Node result = null;
			if (this.m_nodes.TryGetValue(iPos, ref result))
			{
				return result;
			}
			Node node2 = new Node(iPos.x, iPos.y, iWalkable);
			this.m_nodes.Add(iPos, node2);
			return node2;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0009E1A4 File Offset: 0x0009C3A4
		protected void removeNode(int iX, int iY)
		{
			GridPos iPos = new GridPos(iX, iY);
			this.removeNode(iPos);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0009E1C0 File Offset: 0x0009C3C0
		protected void removeNode(GridPos iPos)
		{
			if (this.m_nodes.ContainsKey(iPos))
			{
				this.m_nodes.Remove(iPos);
			}
		}

		// Token: 0x0400150B RID: 5387
		protected Dictionary<GridPos, Node> m_nodes;
	}
}
