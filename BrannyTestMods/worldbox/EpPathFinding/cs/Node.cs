using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x0200030A RID: 778
	public class Node : IComparable<Node>
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x0009C814 File Offset: 0x0009AA14
		public Node(int iX, int iY, bool? iWalkable = null)
		{
			this.x = iX;
			this.y = iY;
			this.walkable = (iWalkable != null && iWalkable.Value);
			this.heuristicStartToEndLen = 0f;
			this.startToCurNodeLen = 0f;
			this.heuristicCurNodeToEndLen = null;
			this.isOpened = false;
			this.isClosed = false;
			this.parent = null;
			this.cost = 1;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0009C88C File Offset: 0x0009AA8C
		public Node(Node b)
		{
			this.x = b.x;
			this.y = b.y;
			this.walkable = b.walkable;
			this.heuristicStartToEndLen = b.heuristicStartToEndLen;
			this.startToCurNodeLen = b.startToCurNodeLen;
			this.heuristicCurNodeToEndLen = b.heuristicCurNodeToEndLen;
			this.isOpened = b.isOpened;
			this.isClosed = b.isClosed;
			this.parent = b.parent;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0009C90C File Offset: 0x0009AB0C
		public void Reset(bool? iWalkable = null)
		{
			if (iWalkable != null)
			{
				this.walkable = iWalkable.Value;
			}
			this.heuristicStartToEndLen = 0f;
			this.startToCurNodeLen = 0f;
			this.heuristicCurNodeToEndLen = null;
			this.isOpened = false;
			this.isClosed = false;
			this.parent = null;
			this.cost = 1;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0009C970 File Offset: 0x0009AB70
		public int CompareTo(Node iObj)
		{
			float num = this.heuristicStartToEndLen - iObj.heuristicStartToEndLen;
			if (num > 0f)
			{
				return 1;
			}
			if (num == 0f)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0009C9A0 File Offset: 0x0009ABA0
		public static List<GridPos> Backtrace(Node iNode)
		{
			List<GridPos> list = new List<GridPos>();
			list.Add(new GridPos(iNode.x, iNode.y));
			while (iNode.parent != null)
			{
				iNode = iNode.parent;
				list.Add(new GridPos(iNode.x, iNode.y));
			}
			list.Reverse();
			return list;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0009CA00 File Offset: 0x0009AC00
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0009CA10 File Offset: 0x0009AC10
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Node node = obj as Node;
			return node != null && this.x == node.x && this.y == node.y;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0009CA4C File Offset: 0x0009AC4C
		public bool Equals(Node p)
		{
			return p != null && this.x == p.x && this.y == p.y;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0009CA71 File Offset: 0x0009AC71
		public static bool operator ==(Node a, Node b)
		{
			return a == b || (a != null && b != null && a.x == b.x && a.y == b.y);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0009CA9F File Offset: 0x0009AC9F
		public static bool operator !=(Node a, Node b)
		{
			return !(a == b);
		}

		// Token: 0x040014EA RID: 5354
		public WorldTile tile;

		// Token: 0x040014EB RID: 5355
		public int x;

		// Token: 0x040014EC RID: 5356
		public int y;

		// Token: 0x040014ED RID: 5357
		public bool walkable;

		// Token: 0x040014EE RID: 5358
		public int cost;

		// Token: 0x040014EF RID: 5359
		public float heuristicStartToEndLen;

		// Token: 0x040014F0 RID: 5360
		public float startToCurNodeLen;

		// Token: 0x040014F1 RID: 5361
		public float? heuristicCurNodeToEndLen;

		// Token: 0x040014F2 RID: 5362
		public bool isOpened;

		// Token: 0x040014F3 RID: 5363
		public bool isClosed;

		// Token: 0x040014F4 RID: 5364
		public Node parent;
	}
}
