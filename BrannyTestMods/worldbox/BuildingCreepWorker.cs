using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class BuildingCreepWorker
{
	// Token: 0x06000435 RID: 1077 RVA: 0x0003C370 File Offset: 0x0003A570
	public BuildingCreepWorker(BuildingCreepHUB pParent)
	{
		this._parent = pParent;
		this._this_creep_biome = this._parent.building.stats.grow_creep_type;
		this.steps_max = this._parent.building.stats.grow_creep_steps_max;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0003C3C0 File Offset: 0x0003A5C0
	public void update()
	{
		if (this.cur_tile == null)
		{
			this._total_step_counter = 0;
			this.cur_tile = this._parent.building.currentTile;
			this.cur_direction = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
		}
		this.checkRandomDirectionChange();
		this.updateMovement(this.cur_tile);
		if (this._total_step_counter > this.steps_max)
		{
			this.cur_tile = null;
		}
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0003C42C File Offset: 0x0003A62C
	private void checkRandomDirectionChange()
	{
		if (this._parent.building.stats.grow_creep_random_new_direction)
		{
			if (this._direction_step_amount >= this._parent.building.stats.grow_creep_steps_before_new_direction)
			{
				this.cur_direction = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
				this._direction_step_amount = 0;
			}
			this._direction_step_amount++;
		}
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0003C494 File Offset: 0x0003A694
	private void creepFlash()
	{
		if (this._parent.building.stats.grow_creep_flash)
		{
			MapBox.instance.flashEffects.flashPixel(this.cur_tile, 15, ColorType.White);
		}
		if (this._parent.building.stats.grow_creep_redraw_tile)
		{
			MapBox.instance.redrawRenderedTile(this.cur_tile);
		}
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x0003C4F8 File Offset: 0x0003A6F8
	private void updateMovement(WorldTile pNextTile)
	{
		this.cur_tile = pNextTile;
		if (this.canPlaceWorkerOn(this.cur_tile))
		{
			this.makeCreep(this.cur_tile);
			this.creepFlash();
			this._total_step_counter++;
			return;
		}
		if (this.cur_tile.Type.biome == this._this_creep_biome)
		{
			this.creepFlash();
			pNextTile = this.getNextRandomTile(this.cur_tile);
			if (pNextTile == null)
			{
				this.cur_tile = null;
				return;
			}
			if (this.canPlaceWorkerOn(pNextTile))
			{
				this.cur_tile = pNextTile;
				return;
			}
			if (pNextTile.Type.biome == this._this_creep_biome)
			{
				this.cur_tile = pNextTile;
				return;
			}
			if (pNextTile.Type.creepRankType == TileRank.Nothing)
			{
				pNextTile = this.cur_tile;
				this.cur_direction = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
			}
		}
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0003C5CD File Offset: 0x0003A7CD
	private bool canPlaceWorkerOn(WorldTile pTile)
	{
		return pTile.Type.creepRankType != TileRank.Nothing || (pTile.Type.creep && pTile.Type.biome != this._this_creep_biome);
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0003C604 File Offset: 0x0003A804
	private void makeCreep(WorldTile pTile)
	{
		TopTileType topTileType = null;
		if (pTile.main_type.creepRankType == TileRank.High)
		{
			topTileType = AssetManager.topTiles.get(this._parent.building.stats.grow_creep_type_high);
		}
		else if (pTile.main_type.creepRankType == TileRank.Low)
		{
			topTileType = AssetManager.topTiles.get(this._parent.building.stats.grow_creep_type_low);
		}
		if (topTileType == null)
		{
			return;
		}
		MapAction.terraformTop(pTile, topTileType, TerraformLibrary.flash);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0003C684 File Offset: 0x0003A884
	private WorldTile getNextRandomTile(WorldTile pTile)
	{
		switch (this._parent.building.stats.grow_creep_movement_type)
		{
		case CreepWorkerMovementType.RandomNeighbourAll:
			return pTile.neighboursAll.GetRandom<WorldTile>();
		case CreepWorkerMovementType.RandomNeighbour:
			return pTile.neighbours.GetRandom<WorldTile>();
		case CreepWorkerMovementType.Direction:
			return this.getDirectionTile(pTile, this._parent.building.stats.grow_creep_direction_random_position);
		default:
			return pTile.neighboursAll.GetRandom<WorldTile>();
		}
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0003C6FC File Offset: 0x0003A8FC
	private WorldTile getDirectionTile(WorldTile pTile, bool pAddRandom = true)
	{
		int num = pTile.pos.x;
		int num2 = pTile.pos.y;
		ActorDirection actorDirection = this.cur_direction;
		switch (actorDirection)
		{
		case ActorDirection.Up:
			if (pAddRandom)
			{
				num += Random.Range(-1, 2);
			}
			num2++;
			break;
		case ActorDirection.UpRight:
		case ActorDirection.UpLeft:
			break;
		case ActorDirection.Right:
			num++;
			if (pAddRandom)
			{
				num2 += Random.Range(-1, 2);
			}
			break;
		case ActorDirection.Down:
			if (pAddRandom)
			{
				num += Random.Range(-1, 2);
			}
			num2--;
			break;
		default:
			if (actorDirection == ActorDirection.Left)
			{
				num--;
				if (pAddRandom)
				{
					num2 += Random.Range(-1, 2);
				}
			}
			break;
		}
		return MapBox.instance.GetTile(num, num2);
	}

	// Token: 0x0400065E RID: 1630
	private int steps_max;

	// Token: 0x0400065F RID: 1631
	private WorldTile cur_tile;

	// Token: 0x04000660 RID: 1632
	private ActorDirection cur_direction;

	// Token: 0x04000661 RID: 1633
	private BuildingCreepHUB _parent;

	// Token: 0x04000662 RID: 1634
	private int _total_step_counter;

	// Token: 0x04000663 RID: 1635
	private string _this_creep_biome;

	// Token: 0x04000664 RID: 1636
	private int _direction_step_amount;
}
