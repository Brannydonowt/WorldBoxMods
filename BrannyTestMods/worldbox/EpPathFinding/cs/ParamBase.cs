using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000316 RID: 790
	public abstract class ParamBase
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0009E1E0 File Offset: 0x0009C3E0
		public ParamBase(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, DiagonalMovement iDiagonalMovement, HeuristicMode iMode) : this(iGrid, iDiagonalMovement, iMode)
		{
			this.m_startNode = this.m_searchGrid.GetNodeAt(iStartPos.x, iStartPos.y);
			this.m_endNode = this.m_searchGrid.GetNodeAt(iEndPos.x, iEndPos.y);
			if (this.m_startNode == null)
			{
				this.m_startNode = new Node(iStartPos.x, iStartPos.y, new bool?(true));
			}
			if (this.m_endNode == null)
			{
				this.m_endNode = new Node(iEndPos.x, iEndPos.y, new bool?(true));
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0009E288 File Offset: 0x0009C488
		public void setGrid(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos)
		{
			this.m_searchGrid = iGrid;
			this.m_startNode = this.m_searchGrid.GetNodeAt(iStartPos.x, iStartPos.y);
			this.m_endNode = this.m_searchGrid.GetNodeAt(iEndPos.x, iEndPos.y);
			if (this.m_startNode == null)
			{
				this.m_startNode = new Node(iStartPos.x, iStartPos.y, new bool?(true));
			}
			if (this.m_endNode == null)
			{
				this.m_endNode = new Node(iEndPos.x, iEndPos.y, new bool?(true));
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0009E32C File Offset: 0x0009C52C
		public ParamBase(BaseGrid iGrid, DiagonalMovement iDiagonalMovement, HeuristicMode iMode)
		{
			this.SetHeuristic(iMode);
			this.m_searchGrid = iGrid;
			this.DiagonalMovement = iDiagonalMovement;
			this.m_startNode = null;
			this.m_endNode = null;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0009E357 File Offset: 0x0009C557
		public ParamBase()
		{
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0009E35F File Offset: 0x0009C55F
		public ParamBase(ParamBase param)
		{
			this.m_searchGrid = param.m_searchGrid;
			this.DiagonalMovement = param.DiagonalMovement;
			this.m_startNode = param.m_startNode;
			this.m_endNode = param.m_endNode;
		}

		// Token: 0x0600127B RID: 4731
		internal abstract void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null);

		// Token: 0x0600127C RID: 4732 RVA: 0x0009E398 File Offset: 0x0009C598
		public void Reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null)
		{
			this._reset(iStartPos, iEndPos, iSearchGrid);
			this.m_startNode = null;
			this.m_endNode = null;
			if (iSearchGrid != null)
			{
				this.m_searchGrid = iSearchGrid;
			}
			this.m_searchGrid.Reset();
			this.m_startNode = this.m_searchGrid.GetNodeAt(iStartPos.x, iStartPos.y);
			this.m_endNode = this.m_searchGrid.GetNodeAt(iEndPos.x, iEndPos.y);
			if (this.m_startNode == null)
			{
				this.m_startNode = new Node(iStartPos.x, iStartPos.y, new bool?(true));
			}
			if (this.m_endNode == null)
			{
				this.m_endNode = new Node(iEndPos.x, iEndPos.y, new bool?(true));
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0009E461 File Offset: 0x0009C661
		public HeuristicDelegate HeuristicFunc
		{
			get
			{
				return this.m_heuristic;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0009E469 File Offset: 0x0009C669
		public BaseGrid SearchGrid
		{
			get
			{
				return this.m_searchGrid;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0009E471 File Offset: 0x0009C671
		public Node StartNode
		{
			get
			{
				return this.m_startNode;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0009E479 File Offset: 0x0009C679
		public Node EndNode
		{
			get
			{
				return this.m_endNode;
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0009E484 File Offset: 0x0009C684
		public void SetHeuristic(HeuristicMode iMode)
		{
			this.m_heuristic = null;
			switch (iMode)
			{
			case HeuristicMode.MANHATTAN:
				this.m_heuristic = new HeuristicDelegate(Heuristic.Manhattan);
				return;
			case HeuristicMode.EUCLIDEAN:
				this.m_heuristic = new HeuristicDelegate(Heuristic.Euclidean);
				return;
			case HeuristicMode.CHEBYSHEV:
				this.m_heuristic = new HeuristicDelegate(Heuristic.Chebyshev);
				return;
			default:
				this.m_heuristic = new HeuristicDelegate(Heuristic.Euclidean);
				return;
			}
		}

		// Token: 0x0400150C RID: 5388
		internal BaseGrid m_searchGrid;

		// Token: 0x0400150D RID: 5389
		internal Node m_startNode;

		// Token: 0x0400150E RID: 5390
		internal Node m_endNode;

		// Token: 0x0400150F RID: 5391
		protected HeuristicDelegate m_heuristic;

		// Token: 0x04001510 RID: 5392
		public DiagonalMovement DiagonalMovement;
	}
}
