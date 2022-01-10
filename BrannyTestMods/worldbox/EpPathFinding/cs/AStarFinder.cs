using System;
using System.Collections.Generic;
using C5;

namespace EpPathFinding.cs
{
	// Token: 0x02000308 RID: 776
	public static class AStarFinder
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x0009C288 File Offset: 0x0009A488
		public static List<WorldTile> backTracePath(Node iNode, List<WorldTile> pSavePath, bool pEndToStartPath = false)
		{
			pSavePath.Clear();
			pSavePath.Add(iNode.tile);
			while (iNode.parent != null)
			{
				iNode = iNode.parent;
				pSavePath.Add(iNode.tile);
			}
			if (!pEndToStartPath)
			{
				pSavePath.Reverse();
			}
			return pSavePath;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0009C2D8 File Offset: 0x0009A4D8
		public static Node getNext(List<Node> pNodes)
		{
			int num = 0;
			Node node = null;
			for (int i = 0; i < pNodes.Count; i++)
			{
				if (node == null || node.CompareTo(pNodes[i]) > 0)
				{
					num = i;
					node = pNodes[i];
				}
			}
			pNodes.RemoveAt(num);
			return node;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0009C328 File Offset: 0x0009A528
		public static List<WorldTile> FindPath(AStarParam iParam, List<WorldTile> pSavePath)
		{
			if (iParam.world == null)
			{
				iParam.world = MapBox.instance;
			}
			IntervalHeap<Node> intervalHeap = new IntervalHeap<Node>();
			Node node;
			Node node2;
			if (iParam.endToStartPath)
			{
				node = iParam.EndNode;
				node2 = iParam.StartNode;
			}
			else
			{
				node = iParam.StartNode;
				node2 = iParam.EndNode;
			}
			HeuristicDelegate heuristicFunc = iParam.HeuristicFunc;
			BaseGrid searchGrid = iParam.SearchGrid;
			StaticGrid staticGrid = (StaticGrid)searchGrid;
			DiagonalMovement diagonalMovement = iParam.DiagonalMovement;
			float weight = iParam.Weight;
			bool boat = iParam.boat;
			node.startToCurNodeLen = 0f;
			node.heuristicStartToEndLen = 0f;
			intervalHeap.Add(node);
			node.isOpened = true;
			AStarFinder.result_split_path = false;
			AStarFinder.lastGlobalRegionID = 0;
			while (intervalHeap.Count != 0)
			{
				if (iParam.maxOpenList != -1 && intervalHeap.Count > iParam.maxOpenList)
				{
					return pSavePath;
				}
				Node node3 = intervalHeap.DeleteMin();
				node3.isClosed = true;
				searchGrid.addToClosed(node3);
				WorldTile tile = node3.tile;
				if (node3 == node2)
				{
					return AStarFinder.backTracePath(node2, pSavePath, iParam.endToStartPath);
				}
				if (iParam.useGlobalPathLock)
				{
					if (tile.region.regionPathID < AStarFinder.lastGlobalRegionID && tile.region.regionPathID != -1)
					{
						node3.isClosed = true;
						searchGrid.addToClosed(node3);
						continue;
					}
					if (tile.region.regionPathID > AStarFinder.lastGlobalRegionID)
					{
						AStarFinder.lastGlobalRegionID = tile.region.regionPathID;
					}
				}
				List<WorldTile> list;
				if (diagonalMovement == DiagonalMovement.Never)
				{
					list = tile.neighbours;
				}
				else
				{
					list = tile.neighboursAll;
				}
				for (int i = 0; i < list.Count; i++)
				{
					WorldTile worldTile = list[i];
					Node node4 = staticGrid.m_nodes[worldTile.x][worldTile.y];
					if (!node4.isClosed && (!worldTile.Type.block || iParam.block))
					{
						if (iParam.useGlobalPathLock)
						{
							if (worldTile.region.regionPathID < AStarFinder.lastGlobalRegionID && worldTile.region.regionPathID != -1)
							{
								node3.isClosed = true;
								searchGrid.addToClosed(node3);
								goto IL_4BD;
							}
							if (worldTile.region.regionPathID > AStarFinder.lastGlobalRegionID)
							{
								AStarFinder.lastGlobalRegionID = worldTile.region.regionPathID;
							}
							if (!worldTile.region.usedByPathLock)
							{
								MapRegion edge_region_right = worldTile.edge_region_right;
								if (edge_region_right == null || !edge_region_right.usedByPathLock)
								{
									MapRegion edge_region_up = worldTile.edge_region_up;
									if (edge_region_up == null || !edge_region_up.usedByPathLock)
									{
										node3.isClosed = true;
										searchGrid.addToClosed(node3);
										goto IL_4BD;
									}
								}
							}
						}
						if (boat)
						{
							if (!worldTile.isGoodForBoat())
							{
								goto IL_4BD;
							}
						}
						else
						{
							if (worldTile.Type.lava && !iParam.lava)
							{
								goto IL_4BD;
							}
							if ((!iParam.block || !worldTile.Type.block) && (!iParam.lava || !worldTile.Type.lava))
							{
								if (iParam.ground && iParam.ocean)
								{
									if (!worldTile.Type.ground && !worldTile.Type.ocean)
									{
										goto IL_4BD;
									}
								}
								else if ((!worldTile.Type.ground && iParam.ground) || (!worldTile.Type.ocean && iParam.ocean) || (!worldTile.Type.swamp && iParam.swamp))
								{
									goto IL_4BD;
								}
							}
						}
						float num = (float)node3.tile.Type.cost;
						num = 1f;
						if (iParam.roads && iParam.world.GetTileSimple(node4.x, node4.y).Type.road)
						{
							num = 0.01f;
						}
						if (AStarFinder.lastGlobalRegionID >= 4 && searchGrid.closed_list_count > 10)
						{
							AStarFinder.result_split_path = true;
							return AStarFinder.backTracePath(node3, pSavePath, iParam.endToStartPath);
						}
						float num2 = node3.startToCurNodeLen + num * ((node4.x - node3.x == 0 || node4.y - node3.y == 0) ? 1f : 1.414f);
						if (!node4.isOpened || num2 < node4.startToCurNodeLen)
						{
							node4.startToCurNodeLen = num2;
							if (node4.heuristicCurNodeToEndLen == null)
							{
								node4.heuristicCurNodeToEndLen = new float?(weight * heuristicFunc(Math.Abs(node4.x - node2.x), Math.Abs(node4.y - node2.y)));
							}
							node4.heuristicStartToEndLen = node4.startToCurNodeLen + node4.heuristicCurNodeToEndLen.Value;
							node4.parent = node3;
							if (!node4.isOpened)
							{
								intervalHeap.Add(node4);
								node4.isOpened = true;
								searchGrid.addToClosed(node4);
							}
						}
					}
					IL_4BD:;
				}
			}
			return pSavePath;
		}

		// Token: 0x040014E3 RID: 5347
		private static int lastGlobalRegionID;

		// Token: 0x040014E4 RID: 5348
		public static bool result_split_path;
	}
}
