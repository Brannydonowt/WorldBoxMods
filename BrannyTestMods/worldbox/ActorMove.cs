using System;
using System.Collections.Generic;
using ai;
using EpPathFinding.cs;

// Token: 0x02000149 RID: 329
public class ActorMove
{
	// Token: 0x060007B6 RID: 1974 RVA: 0x00055E1C File Offset: 0x0005401C
	public static ExecuteEvent goTo(Actor actor, WorldTile target, bool pPathOnLiquid = false, bool pWalkOnBlocks = false)
	{
		MapBox instance = MapBox.instance;
		bool flag = false;
		actor.current_path.Clear();
		actor.current_path_global = null;
		if (!DebugConfig.isOn(DebugOption.SystemUnitPathfinding))
		{
			actor.current_path.Add(target);
			return ExecuteEvent.True;
		}
		if (actor.stats.isBoat && !target.isGoodForBoat())
		{
			return ExecuteEvent.False;
		}
		if (flag)
		{
			actor.current_path.Add(target);
			return ExecuteEvent.True;
		}
		if (actor.isInAir())
		{
			actor.current_path.Add(target);
			return ExecuteEvent.True;
		}
		bool flag2 = actor.currentTile.isSameIsland(target);
		if (flag2 && !actor.isInLiquid())
		{
			pPathOnLiquid = false;
		}
		instance.pathfindingParam.resetParam();
		instance.pathfindingParam.block = pWalkOnBlocks;
		instance.pathfindingParam.lava = !actor.stats.dieInLava;
		if (actor.currentTile.Type.lava)
		{
			instance.pathfindingParam.lava = true;
		}
		instance.pathfindingParam.ocean = actor.stats.oceanCreature;
		if (pPathOnLiquid && !actor.stats.damagedByOcean)
		{
			instance.pathfindingParam.ocean = true;
		}
		else if (actor.isInLiquid())
		{
			instance.pathfindingParam.ocean = true;
		}
		instance.pathfindingParam.ground = actor.stats.landCreature;
		instance.pathfindingParam.boat = (actor.stats.isBoat && actor.currentTile.isGoodForBoat());
		instance.pathfindingParam.limit = true;
		if (!PathfinderTools.tryToGetSimplePath(actor.currentTile, target, actor.current_path, actor.stats, instance.pathfindingParam))
		{
			actor.current_path.Clear();
		}
		instance.pathFindingVisualiser.showPath(null, actor.current_path);
		if (actor.current_path.Count > 0)
		{
			actor.setTileTarget(target);
			return ExecuteEvent.True;
		}
		if (!flag2)
		{
			if ((!target.Type.ground || !instance.pathfindingParam.ground) && (!target.Type.ocean || !instance.pathfindingParam.ocean) && (!target.Type.swamp || !instance.pathfindingParam.swamp) && (!target.Type.lava || !instance.pathfindingParam.lava) && (!target.Type.block || !instance.pathfindingParam.block))
			{
				return ExecuteEvent.False;
			}
			if (target.region.island.getTileCount() < actor.currentTile.region.island.getTileCount())
			{
				if (!target.region.island.connectedWith(actor.currentTile.region.island))
				{
					return ExecuteEvent.False;
				}
				instance.pathfindingParam.endToStartPath = true;
			}
			else if (!actor.currentTile.region.island.connectedWith(target.region.island))
			{
				return ExecuteEvent.False;
			}
		}
		bool flag3 = DebugConfig.isOn(DebugOption.UseGlobalPathLock);
		if (flag3)
		{
			if (actor.stats.isBoat)
			{
				flag3 = true;
			}
			else if (!flag2)
			{
				flag3 = false;
			}
		}
		if (flag3)
		{
			PathFinderResult globalPath = instance.regionPathFinder.getGlobalPath(actor.currentTile, target, actor.stats.isBoat);
			if (globalPath == PathFinderResult.SamePlace)
			{
				actor.current_path.Add(target);
				return ExecuteEvent.True;
			}
			if (globalPath == PathFinderResult.NotFound)
			{
				return ExecuteEvent.True;
			}
			if (globalPath == PathFinderResult.DifferentIslands)
			{
				return ExecuteEvent.True;
			}
			if (instance.regionPathFinder.last_globalPath != null)
			{
				actor.current_path_global = instance.regionPathFinder.last_globalPath;
				int num = 0;
				for (int i = 0; i < actor.current_path_global.Count; i++)
				{
					MapRegion mapRegion = actor.current_path_global[i];
					mapRegion.usedByPathLock = true;
					mapRegion.regionPathID = num++;
				}
				if (actor.stats.isBoat && DebugConfig.isOn(DebugOption.SystemCheckGoodForBoat))
				{
					ActorMove._temp_list.Clear();
					for (int j = 0; j < actor.current_path_global.Count; j++)
					{
						MapRegion mapRegion2 = actor.current_path_global[j];
						for (int k = 0; k < mapRegion2.neighbours.Count; k++)
						{
							MapRegion mapRegion3 = mapRegion2.neighbours[k];
							if (!actor.current_path_global.Contains(mapRegion3))
							{
								mapRegion3.usedByPathLock = true;
								ActorMove._temp_list.Add(mapRegion3);
							}
						}
					}
				}
			}
			else
			{
				actor.currentTile.region.usedByPathLock = true;
				actor.currentTile.region.regionPathID = 0;
			}
		}
		instance.pathfindingParam.useGlobalPathLock = flag3;
		WorldTile pTargetTile = target;
		if (AStarFinder.result_split_path && actor.current_path_global != null && actor.current_path_global.Count > 4)
		{
			pTargetTile = actor.current_path_global[4].tiles.GetRandom<WorldTile>();
		}
		instance.calcPath(actor.currentTile, pTargetTile, actor.current_path);
		if (AStarFinder.result_split_path)
		{
			actor.split_path = SplitPathStatus.Prepare;
		}
		if (flag3)
		{
			if (actor.current_path_global != null)
			{
				for (int l = 0; l < actor.current_path_global.Count; l++)
				{
					MapRegion mapRegion4 = actor.current_path_global[l];
					mapRegion4.usedByPathLock = false;
					mapRegion4.regionPathID = -1;
				}
			}
			actor.currentTile.region.usedByPathLock = false;
			actor.currentTile.region.regionPathID = -1;
		}
		actor.setTileTarget(target);
		return ExecuteEvent.True;
	}

	// Token: 0x04000A37 RID: 2615
	private static List<MapRegion> _temp_list = new List<MapRegion>();
}
