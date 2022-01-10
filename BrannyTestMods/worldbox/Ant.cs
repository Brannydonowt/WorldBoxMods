using System;

// Token: 0x0200012F RID: 303
public class Ant : BaseActorComponent
{
	// Token: 0x060006ED RID: 1773 RVA: 0x0004F324 File Offset: 0x0004D524
	internal override void create()
	{
		base.create();
		this.currentDirection = ActorDirection.Up;
		this.stepsMin = (int)Toolbox.randomFloat(1f, 6f);
		this.stepsMax = (int)Toolbox.randomFloat(10f, 60f);
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0004F360 File Offset: 0x0004D560
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.actor.stats.id == "sandSpider")
		{
			this.sandSpider();
			return;
		}
		if (this.actor.stats.id == "worm")
		{
			this.updateWorm();
			return;
		}
		if (this.actor.stats.id == "printer")
		{
			this.updatePrinter();
			return;
		}
		this.chooseAntAction();
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0004F3E4 File Offset: 0x0004D5E4
	private void updatePrinter()
	{
		if (this.currentPrint == null)
		{
			return;
		}
		for (int i = 0; i < this.currentPrint.stepsPerTick; i++)
		{
			this.printStep();
		}
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0004F416 File Offset: 0x0004D616
	public void setPrintTemplate(PrintTemplate pPrintTemplate)
	{
		this.currentPrint = pPrintTemplate;
		this.printTileOrigin = this.actor.currentTile;
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0004F430 File Offset: 0x0004D630
	private void printStep()
	{
		if (this.printTick >= this.currentPrint.steps.Count)
		{
			this.actor.killHimself(false, AttackType.None, false, true);
			return;
		}
		PrintStep printStep = this.currentPrint.steps[this.printTick];
		WorldTile tile = this.world.GetTile(this.printTileOrigin.pos.x + printStep.x, this.printTileOrigin.pos.y + printStep.y);
		if (tile != null)
		{
			this.actor.spawnOn(tile, 0f);
			this.printTile();
		}
		this.printTick++;
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0004F4E4 File Offset: 0x0004D6E4
	private void printTile()
	{
		Sfx.play("print1", true, base.transform.localPosition.x, base.transform.localPosition.y);
		if (this.actor.currentTile.top_type != null)
		{
			MapAction.decreaseTile(this.actor.currentTile, "flash");
		}
		if (this.actor.currentTile.Type.increaseTo != null)
		{
			MapAction.terraformMain(this.actor.currentTile, this.actor.currentTile.Type.increaseTo, AssetManager.terraform.get("destroy"));
			this.world.setTileDirty(this.actor.currentTile, true);
		}
		this.world.conwayLayer.remove(this.actor.currentTile);
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0004F5C0 File Offset: 0x0004D7C0
	internal void chooseAntAction()
	{
		string id = this.actor.stats.id;
		if (id != null)
		{
			if (id == "antBlack")
			{
				this.antBlack();
				return;
			}
			if (id == "antGreen")
			{
				this.antGreen();
				return;
			}
			if (id == "antBlue")
			{
				this.antBlue();
				return;
			}
			if (!(id == "antRed"))
			{
				return;
			}
			this.antRed();
		}
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0004F634 File Offset: 0x0004D834
	private void antAction(int pNewIndex)
	{
		ActorDirection actorDirection;
		if (this.actor.stats.id == "worm")
		{
			if (pNewIndex < 0)
			{
				pNewIndex = Toolbox.directions_all.Length - 1;
			}
			if (pNewIndex == Toolbox.directions_all.Length)
			{
				pNewIndex = 0;
			}
			actorDirection = Toolbox.directions_all[pNewIndex];
		}
		else
		{
			if (pNewIndex < 0)
			{
				pNewIndex = Toolbox.directions.Length - 1;
			}
			if (pNewIndex == Toolbox.directions.Length)
			{
				pNewIndex = 0;
			}
			actorDirection = Toolbox.directions[pNewIndex];
		}
		WorldTile worldTile = null;
		switch (actorDirection)
		{
		case ActorDirection.Up:
			worldTile = this.actor.currentTile.tile_up;
			if (worldTile == null)
			{
				worldTile = this.actor.currentTile.tile_down;
			}
			break;
		case ActorDirection.UpRight:
		{
			WorldTile currentTile = this.actor.currentTile;
			WorldTile worldTile2;
			if (currentTile == null)
			{
				worldTile2 = null;
			}
			else
			{
				WorldTile tile_up = currentTile.tile_up;
				worldTile2 = ((tile_up != null) ? tile_up.tile_right : null);
			}
			worldTile = worldTile2;
			if (worldTile == null)
			{
				WorldTile currentTile2 = this.actor.currentTile;
				WorldTile worldTile3;
				if (currentTile2 == null)
				{
					worldTile3 = null;
				}
				else
				{
					WorldTile tile_down = currentTile2.tile_down;
					worldTile3 = ((tile_down != null) ? tile_down.tile_left : null);
				}
				worldTile = worldTile3;
			}
			break;
		}
		case ActorDirection.Right:
			worldTile = this.actor.currentTile.tile_right;
			if (worldTile == null)
			{
				worldTile = this.actor.currentTile.tile_left;
			}
			break;
		case ActorDirection.UpLeft:
		{
			WorldTile currentTile3 = this.actor.currentTile;
			WorldTile worldTile4;
			if (currentTile3 == null)
			{
				worldTile4 = null;
			}
			else
			{
				WorldTile tile_up2 = currentTile3.tile_up;
				worldTile4 = ((tile_up2 != null) ? tile_up2.tile_left : null);
			}
			worldTile = worldTile4;
			if (worldTile == null)
			{
				WorldTile currentTile4 = this.actor.currentTile;
				WorldTile worldTile5;
				if (currentTile4 == null)
				{
					worldTile5 = null;
				}
				else
				{
					WorldTile tile_down2 = currentTile4.tile_down;
					worldTile5 = ((tile_down2 != null) ? tile_down2.tile_right : null);
				}
				worldTile = worldTile5;
			}
			break;
		}
		case ActorDirection.Down:
			worldTile = this.actor.currentTile.tile_down;
			if (worldTile == null)
			{
				worldTile = this.actor.currentTile.tile_up;
			}
			break;
		case ActorDirection.DownRight:
		{
			WorldTile currentTile5 = this.actor.currentTile;
			WorldTile worldTile6;
			if (currentTile5 == null)
			{
				worldTile6 = null;
			}
			else
			{
				WorldTile tile_down3 = currentTile5.tile_down;
				worldTile6 = ((tile_down3 != null) ? tile_down3.tile_right : null);
			}
			worldTile = worldTile6;
			if (worldTile == null)
			{
				WorldTile currentTile6 = this.actor.currentTile;
				WorldTile worldTile7;
				if (currentTile6 == null)
				{
					worldTile7 = null;
				}
				else
				{
					WorldTile tile_up3 = currentTile6.tile_up;
					worldTile7 = ((tile_up3 != null) ? tile_up3.tile_left : null);
				}
				worldTile = worldTile7;
			}
			break;
		}
		case ActorDirection.DownLeft:
		{
			WorldTile currentTile7 = this.actor.currentTile;
			WorldTile worldTile8;
			if (currentTile7 == null)
			{
				worldTile8 = null;
			}
			else
			{
				WorldTile tile_down4 = currentTile7.tile_down;
				worldTile8 = ((tile_down4 != null) ? tile_down4.tile_left : null);
			}
			worldTile = worldTile8;
			if (worldTile == null)
			{
				WorldTile currentTile8 = this.actor.currentTile;
				WorldTile worldTile9;
				if (currentTile8 == null)
				{
					worldTile9 = null;
				}
				else
				{
					WorldTile tile_up4 = currentTile8.tile_up;
					worldTile9 = ((tile_up4 != null) ? tile_up4.tile_right : null);
				}
				worldTile = worldTile9;
			}
			break;
		}
		case ActorDirection.Left:
			worldTile = this.actor.currentTile.tile_left;
			if (worldTile == null)
			{
				worldTile = this.actor.currentTile.tile_right;
			}
			break;
		}
		this.moveTo(worldTile);
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0004F8CC File Offset: 0x0004DACC
	protected ActorDirection getNewDirection(WorldTile pTile)
	{
		if (this.actor.currentTile.tile_up == pTile)
		{
			return ActorDirection.Up;
		}
		WorldTile currentTile = this.actor.currentTile;
		WorldTile worldTile;
		if (currentTile == null)
		{
			worldTile = null;
		}
		else
		{
			WorldTile tile_up = currentTile.tile_up;
			worldTile = ((tile_up != null) ? tile_up.tile_right : null);
		}
		if (worldTile == pTile)
		{
			return ActorDirection.UpRight;
		}
		WorldTile currentTile2 = this.actor.currentTile;
		WorldTile worldTile2;
		if (currentTile2 == null)
		{
			worldTile2 = null;
		}
		else
		{
			WorldTile tile_up2 = currentTile2.tile_up;
			worldTile2 = ((tile_up2 != null) ? tile_up2.tile_left : null);
		}
		if (worldTile2 == pTile)
		{
			return ActorDirection.UpLeft;
		}
		if (this.actor.currentTile.tile_down == pTile)
		{
			return ActorDirection.Down;
		}
		WorldTile currentTile3 = this.actor.currentTile;
		WorldTile worldTile3;
		if (currentTile3 == null)
		{
			worldTile3 = null;
		}
		else
		{
			WorldTile tile_down = currentTile3.tile_down;
			worldTile3 = ((tile_down != null) ? tile_down.tile_right : null);
		}
		if (worldTile3 == pTile)
		{
			return ActorDirection.DownRight;
		}
		WorldTile currentTile4 = this.actor.currentTile;
		WorldTile worldTile4;
		if (currentTile4 == null)
		{
			worldTile4 = null;
		}
		else
		{
			WorldTile tile_down2 = currentTile4.tile_down;
			worldTile4 = ((tile_down2 != null) ? tile_down2.tile_left : null);
		}
		if (worldTile4 == pTile)
		{
			return ActorDirection.DownLeft;
		}
		if (this.actor.currentTile.tile_left == pTile)
		{
			return ActorDirection.Left;
		}
		if (this.actor.currentTile.tile_right == pTile)
		{
			return ActorDirection.Right;
		}
		return ActorDirection.Up;
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0004F9D0 File Offset: 0x0004DBD0
	protected WorldTile getNextTileBasedOnDirection(WorldTile pTile, ActorDirection pDirection)
	{
		WorldTile worldTile = null;
		if (pDirection == ActorDirection.Up)
		{
			worldTile = ((pTile != null) ? pTile.tile_up : null);
		}
		if (ActorDirection.UpRight == pDirection)
		{
			WorldTile worldTile2;
			if (pTile == null)
			{
				worldTile2 = null;
			}
			else
			{
				WorldTile tile_up = pTile.tile_up;
				worldTile2 = ((tile_up != null) ? tile_up.tile_right : null);
			}
			worldTile = worldTile2;
		}
		if (ActorDirection.UpLeft == pDirection)
		{
			WorldTile worldTile3;
			if (pTile == null)
			{
				worldTile3 = null;
			}
			else
			{
				WorldTile tile_up2 = pTile.tile_up;
				worldTile3 = ((tile_up2 != null) ? tile_up2.tile_left : null);
			}
			worldTile = worldTile3;
		}
		if (ActorDirection.Down == pDirection)
		{
			worldTile = ((pTile != null) ? pTile.tile_down : null);
		}
		if (ActorDirection.DownRight == pDirection)
		{
			WorldTile worldTile4;
			if (pTile == null)
			{
				worldTile4 = null;
			}
			else
			{
				WorldTile tile_down = pTile.tile_down;
				worldTile4 = ((tile_down != null) ? tile_down.tile_right : null);
			}
			worldTile = worldTile4;
		}
		if (ActorDirection.DownLeft == pDirection)
		{
			WorldTile worldTile5;
			if (pTile == null)
			{
				worldTile5 = null;
			}
			else
			{
				WorldTile tile_down2 = pTile.tile_down;
				worldTile5 = ((tile_down2 != null) ? tile_down2.tile_left : null);
			}
			worldTile = worldTile5;
		}
		if (ActorDirection.Left == pDirection)
		{
			worldTile = ((pTile != null) ? pTile.tile_left : null);
		}
		if (ActorDirection.Right == pDirection)
		{
			worldTile = ((pTile != null) ? pTile.tile_right : null);
		}
		if (worldTile != null)
		{
			return worldTile;
		}
		if (pTile == null)
		{
			return null;
		}
		return pTile.tile_up;
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
	private void moveTo(WorldTile pTile)
	{
		this.currentDirection = this.getNewDirection(pTile);
		this.nextTile = this.getNextTileBasedOnDirection(pTile, this.currentDirection);
		if (this.nextTile != null)
		{
			this.nextTile = this.getNextTileBasedOnDirection(this.nextTile, this.currentDirection);
		}
		if (this.nextTile != null)
		{
			this.nextTile = this.getNextTileBasedOnDirection(this.nextTile, this.currentDirection);
		}
		if (this.nextTile != null && !this.nextTile.Type.block)
		{
			this.actor.moveTo(pTile);
			return;
		}
		this.currentDirection = this.getNewDirection(pTile);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0004FB49 File Offset: 0x0004DD49
	public static bool tileDrawWorm(WorldTile pTile, string pPowerID)
	{
		MapAction.wormTile(pTile, 4);
		return true;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0004FB54 File Offset: 0x0004DD54
	internal void updateWorm()
	{
		if (this.antSteps == 0 || this.antSteps > this.randomSteps)
		{
			this.currentDirection = Toolbox.getRandom<ActorDirection>(Toolbox.directions_all);
			this.randomSteps = (int)Toolbox.randomFloat((float)this.stepsMin, (float)this.stepsMax);
			this.antSteps = 1;
		}
		int pNewIndex = Toolbox.directions_all.IndexOf(this.currentDirection);
		if (this.actor.currentTile.Height < 220)
		{
			this.world.loopWithBrush(this.actor.currentTile, Brush.get(2, "circ_"), new PowerActionWithID(Ant.tileDrawWorm), "worm");
		}
		this.antAction(pNewIndex);
		this.antSteps++;
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0004FC18 File Offset: 0x0004DE18
	internal void sandSpider()
	{
		if (this.antSteps == 0)
		{
			this.currentDirection = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
		}
		else if (this.actor.currentTile.Type.IsType("sand") && !this.spiderChangedDirection)
		{
			this.currentDirection = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
			this.spiderChangedDirection = true;
		}
		int pNewIndex = Toolbox.directions.IndexOf(this.currentDirection);
		if (!this.actor.currentTile.Type.IsType("sand"))
		{
			this.spiderChangedDirection = false;
		}
		this.antUseOnTile(this.actor.currentTile, "sand");
		this.antAction(pNewIndex);
		this.antSteps++;
		if (this.antSteps > 20)
		{
			this.actor.killHimself(false, AttackType.None, false, true);
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
	private void antBlue()
	{
		int num = Toolbox.directions.IndexOf(this.currentDirection);
		string pType;
		if (this.actor.currentTile.Type.liquid)
		{
			pType = "sand";
			num++;
		}
		else
		{
			pType = "shallow_waters";
			num--;
		}
		this.antUseOnTile(this.actor.currentTile, pType);
		this.antAction(num);
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0004FD58 File Offset: 0x0004DF58
	private void antRed()
	{
		if (this.redAntTileType1 == null)
		{
			this.redAntTileType1 = TileLibrary.getRandomTileType(null);
			this.redAntTileType2 = TileLibrary.getRandomTileType(this.redAntTileType1);
		}
		int num = Toolbox.directions.IndexOf(this.currentDirection);
		if (this.antSteps == 0)
		{
			num = Toolbox.directions.IndexOf(this.currentDirection);
		}
		if (this.actor.currentTile.Type.IsType(this.redAntTileType2))
		{
			this.antUseOnTile(this.actor.currentTile, this.redAntTileType1);
			int num2 = this.antSteps;
			this.antSteps = num2 + 1;
			if (num2 > 3)
			{
				num++;
				this.antSteps = 0;
			}
		}
		else
		{
			this.antUseOnTile(this.actor.currentTile, this.redAntTileType2);
			int num2 = this.antSteps;
			this.antSteps = num2 + 1;
			if (num2 > 3)
			{
				num--;
				this.antSteps = 0;
			}
		}
		this.antAction(num);
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0004FE48 File Offset: 0x0004E048
	private void antGreen()
	{
		int num = Toolbox.directions.IndexOf(this.currentDirection);
		string pType;
		if (this.actor.currentTile.Type.liquid)
		{
			pType = "sand";
			num--;
		}
		else if (this.actor.currentTile.Type.IsType("sand"))
		{
			pType = "soil_low";
			num++;
		}
		else if (this.actor.currentTile.Type.IsType("soil_low"))
		{
			pType = "soil_high";
			num--;
		}
		else if (this.actor.currentTile.Type.IsType("forest"))
		{
			pType = "soil_low";
			num++;
		}
		else
		{
			pType = "sand";
			num--;
		}
		this.antUseOnTile(this.actor.currentTile, pType);
		this.antAction(num);
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0004FF28 File Offset: 0x0004E128
	private void antBlack()
	{
		int num = Toolbox.directions.IndexOf(this.currentDirection);
		if (this.antMode == 0)
		{
			if (this.actor.currentTile.Type.liquid)
			{
				this.antCounter = 20;
			}
			if (this.antCounter > 0)
			{
				string pType;
				if (!this.actor.currentTile.Type.IsType("mountains"))
				{
					pType = "mountains";
					num++;
				}
				else
				{
					pType = "hills";
					num--;
				}
				this.antUseOnTile(this.actor.currentTile, pType);
				this.antCounter--;
			}
			if (this.antCounter == 0)
			{
				this.antCounter = 40;
				this.antMode = 1;
				this.currentDirection = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
				num = Toolbox.directions.IndexOf(this.currentDirection);
			}
		}
		else if (this.antCounter > 0)
		{
			this.antCounter--;
			if (this.actor.currentTile.Type.IsType("mountains") || this.actor.currentTile.Type.IsType("hills"))
			{
				this.currentDirection = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
				num = Toolbox.directions.IndexOf(this.currentDirection);
			}
			else
			{
				this.antUseOnTile(this.actor.currentTile, "sand");
				this.currentDirection = Toolbox.getRandom<ActorDirection>(Toolbox.directions);
				num = Toolbox.directions.IndexOf(this.currentDirection);
			}
			if (this.antCounter == 0)
			{
				this.antMode = 0;
			}
		}
		this.antAction(num);
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x000500D0 File Offset: 0x0004E2D0
	private void antTile1()
	{
		int num = Toolbox.directions.IndexOf(this.currentDirection);
		this.changeDir--;
		int num2 = num;
		string pType;
		if (this.actor.currentTile.Type.IsType("forest"))
		{
			pType = "soil_low";
			num++;
		}
		else if (this.actor.currentTile.Type.liquid)
		{
			pType = "sand";
			num++;
		}
		else
		{
			pType = "forest";
			num--;
		}
		this.antUseOnTile(this.actor.currentTile, pType);
		if (this.changeDir > 0)
		{
			num = num2;
		}
		else
		{
			this.changeDir = 2;
		}
		this.antAction(num);
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00050180 File Offset: 0x0004E380
	private void antUseOnTile(WorldTile pTile, string pType)
	{
		MapAction.terraformMain(pTile, AssetManager.tiles.get(pType), TerraformLibrary.destroy);
	}

	// Token: 0x04000947 RID: 2375
	protected ActorDirection currentDirection;

	// Token: 0x04000948 RID: 2376
	private int stepsMin;

	// Token: 0x04000949 RID: 2377
	private int stepsMax;

	// Token: 0x0400094A RID: 2378
	private WorldTile nextTile;

	// Token: 0x0400094B RID: 2379
	private WorldTile printTileOrigin;

	// Token: 0x0400094C RID: 2380
	private int printTick;

	// Token: 0x0400094D RID: 2381
	internal PrintTemplate currentPrint;

	// Token: 0x0400094E RID: 2382
	private int randomSteps;

	// Token: 0x0400094F RID: 2383
	private bool spiderChangedDirection;

	// Token: 0x04000950 RID: 2384
	private string redAntTileType1;

	// Token: 0x04000951 RID: 2385
	private string redAntTileType2;

	// Token: 0x04000952 RID: 2386
	private int antMode;

	// Token: 0x04000953 RID: 2387
	private int antCounter;

	// Token: 0x04000954 RID: 2388
	private int antSteps;

	// Token: 0x04000955 RID: 2389
	private int changeDir = 3;
}
