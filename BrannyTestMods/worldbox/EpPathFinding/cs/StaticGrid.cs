using System;

namespace EpPathFinding.cs
{
	// Token: 0x0200030F RID: 783
	public class StaticGrid : BaseGrid
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0009D983 File Offset: 0x0009BB83
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x0009D98B File Offset: 0x0009BB8B
		public override int width { get; protected set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0009D994 File Offset: 0x0009BB94
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x0009D99C File Offset: 0x0009BB9C
		public override int height { get; protected set; }

		// Token: 0x06001244 RID: 4676 RVA: 0x0009D9A8 File Offset: 0x0009BBA8
		public StaticGrid(int iWidth, int iHeight, bool[][] iMatrix = null)
		{
			this.width = iWidth;
			this.height = iHeight;
			this.m_gridRect.minX = 0;
			this.m_gridRect.minY = 0;
			this.m_gridRect.maxX = iWidth - 1;
			this.m_gridRect.maxY = iHeight - 1;
			this.m_nodes = this.buildNodes(iWidth, iHeight, iMatrix);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0009DA0C File Offset: 0x0009BC0C
		public StaticGrid(StaticGrid b) : base(b)
		{
			bool[][] array = new bool[b.width][];
			for (int i = 0; i < b.width; i++)
			{
				array[i] = new bool[b.height];
				for (int j = 0; j < b.height; j++)
				{
					if (b.IsWalkableAt(i, j))
					{
						array[i][j] = true;
					}
					else
					{
						array[i][j] = false;
					}
				}
			}
			this.m_nodes = this.buildNodes(b.width, b.height, array);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0009DA90 File Offset: 0x0009BC90
		private Node[][] buildNodes(int iWidth, int iHeight, bool[][] iMatrix)
		{
			Node[][] array = new Node[iWidth][];
			for (int i = 0; i < iWidth; i++)
			{
				array[i] = new Node[iHeight];
				for (int j = 0; j < iHeight; j++)
				{
					array[i][j] = new Node(i, j, null);
				}
			}
			if (iMatrix == null)
			{
				return array;
			}
			if (iMatrix.Length != iWidth || iMatrix[0].Length != iHeight)
			{
				throw new Exception("Matrix size does not fit");
			}
			for (int k = 0; k < iWidth; k++)
			{
				for (int l = 0; l < iHeight; l++)
				{
					if (iMatrix[k][l])
					{
						array[k][l].walkable = true;
					}
					else
					{
						array[k][l].walkable = false;
					}
				}
			}
			return array;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0009DB3E File Offset: 0x0009BD3E
		public override Node GetNodeAt(int iX, int iY)
		{
			return this.m_nodes[iX][iY];
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0009DB4A File Offset: 0x0009BD4A
		public override bool IsWalkableAt(int iX, int iY)
		{
			return this.isInside(iX, iY) && this.m_nodes[iX][iY].walkable;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0009DB67 File Offset: 0x0009BD67
		protected bool isInside(int iX, int iY)
		{
			return iX >= 0 && iX < this.width && iY >= 0 && iY < this.height;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0009DB87 File Offset: 0x0009BD87
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			this.m_nodes[iX][iY].walkable = iWalkable;
			this.m_nodes[iX][iY].cost = pCost;
			return true;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0009DBAB File Offset: 0x0009BDAB
		public void SetTileNode(int iX, int iY, WorldTile pTile)
		{
			this.m_nodes[iX][iY].tile = pTile;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0009DBBD File Offset: 0x0009BDBD
		protected bool isInside(GridPos iPos)
		{
			return this.isInside(iPos.x, iPos.y);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0009DBD1 File Offset: 0x0009BDD1
		public override Node GetNodeAt(GridPos iPos)
		{
			return this.GetNodeAt(iPos.x, iPos.y);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0009DBE5 File Offset: 0x0009BDE5
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.IsWalkableAt(iPos.x, iPos.y);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0009DBF9 File Offset: 0x0009BDF9
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0009DC0F File Offset: 0x0009BE0F
		public override void Reset()
		{
			this.Reset(null);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0009DC18 File Offset: 0x0009BE18
		public void Reset(bool[][] iMatrix)
		{
			foreach (Node node in this.closedList)
			{
				node.Reset(null);
			}
			this.closedList.Clear();
			this.closed_list_count = 0;
			if (iMatrix == null)
			{
				return;
			}
			if (iMatrix.Length != this.width || iMatrix[0].Length != this.height)
			{
				throw new Exception("Matrix size does not fit");
			}
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					if (iMatrix[i][j])
					{
						this.m_nodes[i][j].walkable = true;
					}
					else
					{
						this.m_nodes[i][j].walkable = false;
					}
				}
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0009DCF8 File Offset: 0x0009BEF8
		public override BaseGrid Clone()
		{
			int width = this.width;
			int height = this.height;
			Node[][] nodes = this.m_nodes;
			StaticGrid staticGrid = new StaticGrid(width, height, null);
			Node[][] array = new Node[width][];
			for (int i = 0; i < width; i++)
			{
				array[i] = new Node[height];
				for (int j = 0; j < height; j++)
				{
					array[i][j] = new Node(i, j, new bool?(nodes[i][j].walkable));
				}
			}
			staticGrid.m_nodes = array;
			return staticGrid;
		}

		// Token: 0x04001500 RID: 5376
		public Node[][] m_nodes;
	}
}
