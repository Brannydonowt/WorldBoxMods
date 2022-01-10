using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class UFO : BaseActorComponent
{
	// Token: 0x06000792 RID: 1938 RVA: 0x000550C8 File Offset: 0x000532C8
	internal override void create()
	{
		base.create();
		this.beamRnd = base.transform.Find("Beam").GetComponent<SpriteRenderer>();
		this.beamAnim = base.transform.Find("Beam").GetComponent<SpriteAnimation>();
		this.hideBeam();
		this.setState(UfoState.Flying, null);
		this.actor.callbacks_get_hit.Add(new BaseActionActor(this.getHit));
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0005513B File Offset: 0x0005333B
	public void click()
	{
		if (this.beamAnim.isOn)
		{
			return;
		}
		this.startBeam();
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00055154 File Offset: 0x00053354
	private void setState(UfoState pState, WorldTile pTileTarget = null)
	{
		this.state = pState;
		WorldTile worldTile = null;
		switch (pState)
		{
		case UfoState.Flying:
			this.actor.curStats.speed = 20f;
			this.beamAnim.isOn = false;
			this.beamRnd.enabled = false;
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
					if (this.attacksForCity < 0)
					{
						this.cityToAttack = null;
					}
				}
			}
			if (worldTile == null)
			{
				worldTile = this.world.islandsCalculator.getRandomGround();
			}
			this.actor.moveTo(worldTile);
			return;
		case UfoState.Exploring:
			this.actor.curStats.speed = 10f;
			worldTile = this.actor.currentTile.zone.tiles.GetRandom<WorldTile>();
			this.actor.moveTo(worldTile);
			return;
		case UfoState.Attacking:
		case UfoState.AttackingUnitTarget:
			this.startBeam();
			return;
		case UfoState.Abduct:
			break;
		case UfoState.Dying:
			this.beamAnim.isOn = false;
			this.beamRnd.enabled = false;
			break;
		case UfoState.GoingToUnitTarget:
			this.actor.curStats.speed = 50f;
			this.actor.moveTo(pTileTarget);
			this.actor.is_moving = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x000552DC File Offset: 0x000534DC
	private void startBeam()
	{
		this.beamAnim.stopAt(0, true);
		this.beamAnim.isOn = true;
		this.beamRnd.enabled = true;
		Sfx.play("ufoAttack", true, base.transform.localPosition.x, base.transform.localPosition.y);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0005533C File Offset: 0x0005353C
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.beamAnim.update(pElapsed);
		if (this.actor.curStats.speed < 30f && this.state == UfoState.Flying)
		{
			this.actor.curStats.speed += pElapsed * 10f;
		}
		if (!this.actor.data.alive)
		{
			this.hideBeam();
			this.actor.updateFall(pElapsed);
			if (this.actor.zPosition.y == 0f)
			{
				Sfx.play("explosion medium", true, -1f, -1f);
				WorldTile tile = this.world.GetTile((int)this.actor.currentPosition.x, (int)this.actor.currentPosition.y);
				if (tile == null)
				{
					this.actor.killHimself(true, AttackType.Other, true, true);
					return;
				}
				this.actor.killHimself(true, AttackType.Other, true, true);
				MapAction.damageWorld(tile, 5, AssetManager.terraform.get("ufo_explosion"));
				this.world.stackEffects.get("explosionSmall").spawnAtRandomScale(tile, 0.45f, 0.6f);
			}
			return;
		}
		if (this.beamAnim.isOn)
		{
			if (this.beamAnim.currentFrameIndex == 4)
			{
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						WorldTile tile2 = this.world.GetTile(this.actor.currentTile.pos.x + j - 4, this.actor.currentTile.pos.y + i - 4);
						if (tile2 != null && Toolbox.Dist((float)this.actor.currentTile.pos.x, (float)this.actor.currentTile.pos.y, (float)tile2.pos.x, (float)tile2.pos.y) <= 4f)
						{
							this.attackTile(tile2);
						}
					}
				}
			}
			if (this.beamAnim.currentFrameIndex != this.beamAnim.frames.Length - 1)
			{
				return;
			}
			this.hideBeam();
		}
		if (this.actor.is_moving)
		{
			return;
		}
		if (this.actionTime > 0f)
		{
			this.actionTime -= pElapsed;
			return;
		}
		this.nextAction();
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x000555C6 File Offset: 0x000537C6
	private void hideBeam()
	{
		this.beamAnim.isOn = false;
		this.beamRnd.enabled = false;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x000555E0 File Offset: 0x000537E0
	private void attackTile(WorldTile pTile)
	{
		if (pTile == null)
		{
			return;
		}
		MapAction.damageWorld(pTile, 0, AssetManager.terraform.get("ufo_attack"));
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x000555FC File Offset: 0x000537FC
	private void nextAction()
	{
		if (!this.actor.data.alive)
		{
			return;
		}
		if (this.aggroTargets.Count != 0)
		{
			BaseSimObject x = null;
			for (int i = 0; i < this.aggroTargets.Count; i++)
			{
				this.aggroTargets.ShuffleOne(i);
				BaseSimObject baseSimObject = (BaseSimObject)this.aggroTargets[i];
				if (!(baseSimObject == null) && baseSimObject.base_data.alive)
				{
					this.setState(UfoState.GoingToUnitTarget, baseSimObject.currentTile);
					break;
				}
			}
			this.aggroTargets.Clear();
			if (x != null)
			{
				return;
			}
		}
		switch (this.state)
		{
		case UfoState.Flying:
			if (this.cityToAttack != null && this.actor.currentTile.building != null && !this.world.worldLaws.world_law_peaceful_monsters.boolVal)
			{
				this.setState(UfoState.Attacking, null);
			}
			this.exploringTicks = Toolbox.randomInt(3, 7);
			this.setState(UfoState.Exploring, null);
			return;
		case UfoState.Exploring:
			if (this.exploringTicks > 0)
			{
				this.exploringTicks--;
				if (this.actor.currentTile.zone.city != null)
				{
					this.cityToAttack = this.actor.currentTile.zone.city;
					this.attacksForCity = Toolbox.randomInt(3, 10);
				}
				if (this.cityToAttack != null)
				{
					this.setState(UfoState.Flying, null);
					return;
				}
				this.setState(UfoState.Exploring, null);
				return;
			}
			else
			{
				this.setState(UfoState.Flying, null);
			}
			break;
		case UfoState.Attacking:
		case UfoState.Abduct:
		case UfoState.Dying:
			break;
		case UfoState.GoingToUnitTarget:
			this.setState(UfoState.AttackingUnitTarget, null);
			return;
		case UfoState.AttackingUnitTarget:
			this.setState(UfoState.Flying, null);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x000557B8 File Offset: 0x000539B8
	internal void getHit(Actor pActor)
	{
		if (this.state == UfoState.Flying || this.state == UfoState.Exploring)
		{
			this.nextAction();
		}
		if (this.actor.attackedBy != null)
		{
			this.aggroTargets.Add(this.actor.attackedBy);
		}
	}

	// Token: 0x04000A1F RID: 2591
	private SpriteRenderer beamRnd;

	// Token: 0x04000A20 RID: 2592
	private SpriteAnimation beamAnim;

	// Token: 0x04000A21 RID: 2593
	private UfoState state;

	// Token: 0x04000A22 RID: 2594
	private int attacksForCity;

	// Token: 0x04000A23 RID: 2595
	private City cityToAttack;

	// Token: 0x04000A24 RID: 2596
	private int exploringTicks;

	// Token: 0x04000A25 RID: 2597
	private float actionTime;

	// Token: 0x04000A26 RID: 2598
	internal List<BaseMapObject> aggroTargets = new List<BaseMapObject>();
}
