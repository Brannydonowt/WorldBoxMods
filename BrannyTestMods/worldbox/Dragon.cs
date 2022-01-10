using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class Dragon : BaseActorComponent
{
	// Token: 0x0600073B RID: 1851 RVA: 0x0005202C File Offset: 0x0005022C
	internal override void create()
	{
		base.create();
		this.spriteAnimation = base.GetComponent<SpriteAnimation>();
		if (this.actor.haveTrait("zombie"))
		{
			this.ignoredKingdoms = new string[]
			{
				"undead"
			};
			this.dragonAsset = PrefabLibrary.instance.zombieDragonAsset;
		}
		else
		{
			this.ignoredKingdoms = new string[]
			{
				"dragons"
			};
			this.dragonAsset = PrefabLibrary.instance.dragonAsset;
		}
		this.actor.callbacks_get_hit.Add(new BaseActionActor(this.getHit));
		this.setState(DragonState.Fly);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x000520CA File Offset: 0x000502CA
	private void setFlying(bool pVal)
	{
		this.actor.flying = pVal;
		if (pVal)
		{
			this.actor.hitboxZ = 8f;
			return;
		}
		this.actor.hitboxZ = 2f;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x000520FC File Offset: 0x000502FC
	private void setState(DragonState pState)
	{
		this.state = pState;
		DragonAssetContainer asset = this.dragonAsset.getAsset(pState);
		this.animationDoneOnce = false;
		this.setFlying(false);
		WorldTile worldTile = null;
		if (!this.actor.haveTrait("zombie"))
		{
			this.sleepy++;
		}
		switch (this.state)
		{
		case DragonState.Fly:
			this.setFlying(true);
			if (this.actorToAttack != null && !this.actorToAttack.data.alive)
			{
				this.actorToAttack = null;
			}
			if (this.actor.haveTrait("zombie") && this.actorToAttack == null && this.cityToAttack == null && this.world.kingdoms.getKingdomByID("goldenBrain").buildings.Count > 0)
			{
				float num = 0f;
				foreach (Building building in this.world.kingdoms.getKingdomByID("goldenBrain").buildings)
				{
					float num2 = Toolbox.DistTile(building.currentTile, this.actor.currentTile);
					if (worldTile == null || num2 < num)
					{
						worldTile = building.currentTile;
						num = num2;
					}
				}
				if (worldTile != null && worldTile == this.actor.currentTile)
				{
					this.setState(DragonState.Landing);
					return;
				}
			}
			if (this.actorToAttack != null && this.actorToAttack.data != null && this.actorToAttack.data.alive && this.actorToAttack.currentTile != null && !this.shouldFly() && this.actor.currentTile == this.actorToAttack.currentTile)
			{
				this.setState(DragonState.Landing);
				return;
			}
			if (this.attacksForCity > 0 && this.cityToAttack != null)
			{
				if (this.cityToAttack.buildings.Count == 0)
				{
					this.cityToAttack = null;
				}
				else
				{
					worldTile = this.cityToAttack.buildings.GetRandom().currentTile.zone.tiles.GetRandom<WorldTile>();
					this.attacksForCity--;
				}
			}
			if (worldTile == null)
			{
				int num3 = 50;
				worldTile = this.world.islandsCalculator.getRandomGround();
				while (Toolbox.Dist((float)this.actor.currentTile.pos.x, (float)this.actor.currentTile.pos.y, (float)worldTile.pos.x, (float)worldTile.pos.y) > 100f)
				{
					worldTile = this.world.islandsCalculator.getRandomGround();
					num3--;
					if (num3 <= 0)
					{
						break;
					}
				}
			}
			this.actor.moveTo(worldTile);
			break;
		case DragonState.LandAttack:
			Sfx.play("dragonAttack", true, base.transform.localPosition.x, base.transform.localPosition.y);
			break;
		case DragonState.Death:
			this.spriteAnimation.looped = false;
			break;
		case DragonState.SleepStart:
			this.sleepy = 0;
			this.actor.setFlip(false);
			break;
		case DragonState.SleepLoop:
			this.sleepy = 0;
			this.actor.setFlip(false);
			this.actionTime = Toolbox.randomFloat(10f, 80f);
			break;
		case DragonState.SleepUp:
			this.sleepy = 0;
			this.actor.setFlip(false);
			break;
		case DragonState.Landing:
			if (this.shouldFly())
			{
				this.setState(DragonState.Up);
				return;
			}
			break;
		case DragonState.Slide:
			Sfx.play("dragonSwoop", true, base.transform.localPosition.x, base.transform.localPosition.y);
			this.setFlying(true);
			break;
		case DragonState.Up:
			this.landAttacks = 0;
			this.actionTime = 0f;
			this.setFlying(true);
			break;
		case DragonState.Idle:
			this.actionTime = Toolbox.randomFloat(1f, 3f);
			break;
		}
		this.spriteAnimation.setFrames(asset.frames);
		this.spriteAnimation.timeBetweenFrames = asset.speed;
		this.spriteAnimation.resetAnim(0);
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00052564 File Offset: 0x00050764
	private bool shouldFly()
	{
		return !this.actor.currentTile.Type.ground && this.actor.data.alive;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00052590 File Offset: 0x00050790
	private void attackTile(WorldTile pTile)
	{
		if (pTile == null)
		{
			return;
		}
		if (this.actor.haveTrait("zombie"))
		{
			DropsLibrary.action_acid(pTile, null);
			if (Toolbox.randomBool())
			{
				this.world.dropManager.spawnBurstPixel(pTile, "acid", Toolbox.randomFloat(0.05f, 0.5f), Toolbox.randomFloat(0.05f, 0.75f), 0f, -1f);
				return;
			}
		}
		else
		{
			pTile.setFire(true);
			if (pTile.building != null)
			{
				pTile.building.getHit(10f, true, AttackType.Other, null, true);
			}
			if (Toolbox.randomBool())
			{
				this.world.dropManager.spawnBurstPixel(pTile, "fire", Toolbox.randomFloat(0.05f, 0.5f), Toolbox.randomFloat(0.05f, 0.75f), 0f, -1f);
			}
		}
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00052674 File Offset: 0x00050874
	private bool hasTargetsForSlide()
	{
		if (this.world.worldLaws.world_law_peaceful_monsters.boolVal)
		{
			return false;
		}
		foreach (WorldTile tTile in this.attackRange(this.actor.flip))
		{
			if (this.hasTarget(tTile))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x000526F4 File Offset: 0x000508F4
	private List<WorldTile> attackRange(bool flip = false)
	{
		List<WorldTile> list = new List<WorldTile>();
		int num;
		if (flip)
		{
			num = -25;
		}
		else
		{
			num = 20;
		}
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 35; j++)
			{
				WorldTile tile = this.world.GetTile(this.actor.currentTile.x + j - 15 + num, this.actor.currentTile.y - i + 2);
				if (tile != null)
				{
					list.Add(tile);
				}
			}
		}
		return list;
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00052774 File Offset: 0x00050974
	private bool hasTarget(WorldTile tTile)
	{
		if (this.world.worldLaws.world_law_peaceful_monsters.boolVal)
		{
			return false;
		}
		if (tTile.building != null && tTile.building.data.alive && !tTile.building.isRuin())
		{
			return true;
		}
		if (tTile.units.Count > 0)
		{
			foreach (Actor actor in tTile.units)
			{
				if (actor.data.alive && actor.transform.localPosition.z <= 0f)
				{
					if (this.ignoredKingdoms != null)
					{
						bool flag = false;
						foreach (string b in this.ignoredKingdoms)
						{
							Kingdom kingdom = actor.kingdom;
							if (((kingdom != null) ? kingdom.id : null) == b)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							continue;
						}
					}
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00052894 File Offset: 0x00050A94
	public override void update(float pElapsed)
	{
		if (this.spriteAnimation.currentFrameIndex > 0)
		{
			this.animationDoneOnce = true;
		}
		if (!this.actor.data.alive)
		{
			this.actionTime = -1f;
		}
		if (this.state == DragonState.Slide && this.spriteAnimation.currentFrameIndex == 7)
		{
			using (List<WorldTile>.Enumerator enumerator = this.attackRange(this.actor.flip).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					WorldTile pTile = enumerator.Current;
					if (!Toolbox.randomBool())
					{
						this.world.applyForce(pTile, 2, this.actor.curStats.knockback, true, false, this.actor.curStats.damage, this.ignoredKingdoms, this.actor, null);
						this.attackTile(pTile);
					}
				}
				goto IL_289;
			}
		}
		if (this.state == DragonState.LandAttack && this.spriteAnimation.currentFrameIndex == 5)
		{
			this.landAttacks++;
			for (int i = 0; i < 12; i++)
			{
				for (int j = 0; j < 20; j++)
				{
					if (!Toolbox.randomBool())
					{
						WorldTile tile = this.world.GetTile(this.actor.currentTile.pos.x + j - 10, this.actor.currentTile.pos.y - i + 1);
						if (tile != null && Toolbox.Dist((float)this.actor.currentTile.pos.x, (float)this.actor.currentTile.pos.y, (float)tile.pos.x, (float)tile.pos.y) <= 9f)
						{
							this.attackTile(tile);
						}
					}
				}
			}
			this.world.applyForce(this.actor.currentTile, 10, this.actor.curStats.knockback, true, false, this.actor.curStats.damage, this.ignoredKingdoms, this.actor, null);
		}
		else if (this.state == DragonState.Death && this.spriteAnimation.currentFrameIndex == this.spriteAnimation.frames.Length - 1)
		{
			this.actor.updateDeadBlackAnimation();
			if (this._died)
			{
				return;
			}
			this._died = true;
			if (this.actor.haveTrait("fire_blood"))
			{
				this.fireBloodExplosion();
			}
			return;
		}
		IL_289:
		if (this.actor.is_moving)
		{
			return;
		}
		if (this.shouldFly())
		{
			this.actionTime = 0f;
		}
		if (this.actionTime > 0f)
		{
			this.actionTime -= pElapsed;
			return;
		}
		this.nextAction();
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00052B80 File Offset: 0x00050D80
	private void fireBloodExplosion()
	{
		for (int i = 0; i < 25; i++)
		{
			if (Toolbox.randomBool())
			{
				this.world.dropManager.spawnBurstPixel(this.actor.currentTile, "fire", Toolbox.randomFloat(0.05f, 0.5f), Toolbox.randomFloat(0.05f, 1f), 0f, -1f);
			}
			foreach (WorldTile pTile in this.actor.currentTile.neighboursAll)
			{
				if (Toolbox.randomBool())
				{
					this.world.dropManager.spawnBurstPixel(pTile, "fire", Toolbox.randomFloat(0.05f, 0.5f), Toolbox.randomFloat(0.05f, 1f), 0f, -1f);
				}
			}
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00052C80 File Offset: 0x00050E80
	private void nextAction()
	{
		bool boolVal = this.world.worldLaws.world_law_peaceful_monsters.boolVal;
		if (this.spriteAnimation.currentFrameIndex == 0 && this.animationDoneOnce)
		{
			if (!this.actor.data.alive)
			{
				if (this.actor.flying)
				{
					this.setState(DragonState.Landing);
					return;
				}
				if (this.state != DragonState.Death)
				{
					this.setState(DragonState.Death);
					return;
				}
			}
			else
			{
				if (this._justGotHit && !boolVal)
				{
					if (this.state == DragonState.Idle || this.state == DragonState.SleepUp || this.state == DragonState.LandAttack)
					{
						if (this.landAttacks > 2 || this.shouldFly())
						{
							this.setState(DragonState.Up);
							return;
						}
						this._justGotHit = false;
						this.setState(DragonState.LandAttack);
						return;
					}
					else if (this.state == DragonState.Fly || this.state == DragonState.Up)
					{
						this.setState(DragonState.Slide);
						this._justGotHit = false;
						return;
					}
				}
				if (this.state == DragonState.Landing)
				{
					if (!boolVal && this.actorToAttack != null && this.actorToAttack.data.alive && this.actor.currentTile != null && this.actor.currentTile == this.actorToAttack.currentTile && !this.shouldFly())
					{
						this.setState(DragonState.LandAttack);
						return;
					}
					if (!boolVal && this.attacksForCity > 0 && this.cityToAttack != null && Toolbox.randomChance(0.2f))
					{
						if (this.shouldFly() && this.hasTargetsForSlide() && Toolbox.randomBool())
						{
							this.setState(DragonState.Slide);
							this.attacksForCity--;
							return;
						}
						if (!this.shouldFly())
						{
							this.setState(DragonState.LandAttack);
							this.attacksForCity--;
							return;
						}
					}
					if (this.shouldFly())
					{
						this.setState(DragonState.Fly);
						return;
					}
				}
				this._canSleep = true;
				if (this.actorToAttack != null || this.cityToAttack != null)
				{
					this._canSleep = false;
				}
				else if (this.actor.currentTile.units.Count > 1)
				{
					this._canSleep = false;
				}
				Dragon.possibleActions.Clear();
				if (this.state == DragonState.SleepStart)
				{
					Dragon.possibleActions.Add(DragonState.SleepLoop);
				}
				if (this.state == DragonState.SleepLoop)
				{
					Dragon.possibleActions.Add(DragonState.SleepLoop);
					Dragon.possibleActions.Add(DragonState.SleepUp);
				}
				if (this.state == DragonState.SleepUp)
				{
					if (this._canSleep)
					{
						Dragon.possibleActions.Add(DragonState.SleepStart);
					}
					Dragon.possibleActions.Add(DragonState.Up);
					Dragon.possibleActions.Add(DragonState.Idle);
				}
				if (this.state == DragonState.Up || this.state == DragonState.Fly || this.state == DragonState.Slide)
				{
					if (!this.shouldFly())
					{
						Dragon.possibleActions.Add(DragonState.Fly);
						if (this.state != DragonState.Up)
						{
							Dragon.possibleActions.Add(DragonState.Landing);
						}
					}
					Dragon.possibleActions.Add(DragonState.Fly);
					Dragon.possibleActions.Add(DragonState.Fly);
					Dragon.possibleActions.Add(DragonState.Fly);
					Dragon.possibleActions.Add(DragonState.Fly);
					Dragon.possibleActions.Add(DragonState.Fly);
					if (this.state != DragonState.Slide && !boolVal)
					{
						Dragon.possibleActions.Add(DragonState.Slide);
					}
				}
				if (this.state == DragonState.Landing || this.state == DragonState.Idle || this.state == DragonState.LandAttack)
				{
					Dragon.possibleActions.Add(DragonState.Up);
					if (!boolVal)
					{
						Dragon.possibleActions.Add(DragonState.LandAttack);
					}
					if (this._canSleep && this.sleepy > 10)
					{
						Dragon.possibleActions.Add(DragonState.SleepStart);
						Dragon.possibleActions.Add(DragonState.SleepStart);
						Dragon.possibleActions.Add(DragonState.SleepStart);
					}
					Dragon.possibleActions.Add(DragonState.Idle);
					Dragon.possibleActions.Add(DragonState.Idle);
					Dragon.possibleActions.Add(DragonState.Idle);
				}
				if (Dragon.possibleActions.Count > 0)
				{
					DragonState random = Dragon.possibleActions.GetRandom<DragonState>();
					this.setState(random);
				}
				else
				{
					Debug.Log("no possible actions for current state : " + this.state.ToString());
				}
				Dragon.possibleActions.Clear();
			}
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0005306E File Offset: 0x0005126E
	internal void clickToWakeup()
	{
		if (this.state == DragonState.SleepLoop || this.state == DragonState.SleepStart)
		{
			this.actionTime = 0f;
		}
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00053090 File Offset: 0x00051290
	internal void getHit(Actor pActor)
	{
		if (!this.world.worldLaws.world_law_peaceful_monsters.boolVal && this.actor.attackedBy != null)
		{
			if (this.actor.attackedBy.isActor() && (this.actorToAttack == null || !this.actorToAttack.data.alive))
			{
				this.actorToAttack = this.actor.attackedBy.a;
			}
			this.cityToAttack = this.actor.attackedBy.city;
			this.attacksForCity = Toolbox.randomInt(4, 12);
		}
		this._justGotHit = true;
		this.actionTime = 0f;
	}

	// Token: 0x04000995 RID: 2453
	private string[] ignoredKingdoms;

	// Token: 0x04000996 RID: 2454
	private DragonAsset dragonAsset;

	// Token: 0x04000997 RID: 2455
	private DragonState state;

	// Token: 0x04000998 RID: 2456
	private bool animationDoneOnce;

	// Token: 0x04000999 RID: 2457
	private float actionTime;

	// Token: 0x0400099A RID: 2458
	private int landAttacks;

	// Token: 0x0400099B RID: 2459
	private int attacksForCity;

	// Token: 0x0400099C RID: 2460
	private City cityToAttack;

	// Token: 0x0400099D RID: 2461
	private Actor actorToAttack;

	// Token: 0x0400099E RID: 2462
	private bool _justGotHit;

	// Token: 0x0400099F RID: 2463
	private bool _canSleep;

	// Token: 0x040009A0 RID: 2464
	private int sleepy;

	// Token: 0x040009A1 RID: 2465
	private bool _died;

	// Token: 0x040009A2 RID: 2466
	private static List<DragonState> possibleActions = new List<DragonState>();

	// Token: 0x040009A3 RID: 2467
	private SpriteAnimation spriteAnimation;
}
