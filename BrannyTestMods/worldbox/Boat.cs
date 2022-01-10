using System;
using System.Collections.Generic;
using life;
using life.taxi;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class Boat : BaseActorComponent
{
	// Token: 0x060003A9 RID: 937 RVA: 0x00038BC8 File Offset: 0x00036DC8
	internal override void create()
	{
		base.create();
		this.actor.callbacks_on_death.Add(new BaseActionActor(this.deathAction));
		this.actor.callbacks_on_death.Add(new BaseActionActor(this.updateBoatAnim));
		this.actor.callbacks_on_death.Add(new BaseActionActor(this.spawnBoatExplosion));
		this.actor.callbacks_landed.Add(new BaseActionActor(this.setWaterFrame));
		this.actor.callbacks_landed.Add(new BaseActionActor(this.cancelWork));
		this.actor.callbacks_added_madness.Add(new BaseActionActor(this.madnessAdded));
		this.actor.callbacks_cancel_path_movement.Add(new BaseActionActor(this.cancelPathfinderMovement));
		this.setState(BoatState.Idle);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00038CA8 File Offset: 0x00036EA8
	public bool isNearDock()
	{
		return !(this.actor.homeBuilding == null) && this.actor.homeBuilding.GetComponent<Docks>().tiles_ocean.Contains(this.actor.currentTile);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00038CF4 File Offset: 0x00036EF4
	private void cancelPathfinderMovement(Actor pActor)
	{
		this.cancelWork(pActor);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00038CFD File Offset: 0x00036EFD
	internal void cancelWork(Actor pActor)
	{
		this.setState(BoatState.Idle);
		this.actor.cancelAllBeh(null);
		if (this.taxiRequest != null)
		{
			TaxiManager.cancelRequest(this.taxiRequest);
			this.taxiRequest = null;
			this.taxiTarget = null;
		}
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00038D34 File Offset: 0x00036F34
	public void updateTexture()
	{
		this._textureId = "boat_fishing";
		if (this.actor.kingdom != null)
		{
			string str;
			if (this.actor.kingdom.race == null)
			{
				str = "human";
			}
			else
			{
				str = this.actor.kingdom.race.id;
				if (this.actor.kingdom.isCiv())
				{
					str = this.actor.kingdom.raceID;
				}
				else
				{
					str = "human";
				}
			}
			string id = this.actor.stats.id;
			if (id != null)
			{
				if (!(id == "boat_fishing"))
				{
					if (!(id == "boat_trading"))
					{
						if (id == "boat_transport")
						{
							this._textureId = "boat_transport_" + str;
						}
					}
					else
					{
						this._textureId = "boat_trading_" + str;
					}
				}
				else
				{
					this._textureId = "boat_fishing";
				}
			}
		}
		this.animationDataBoat = ActorAnimationLoader.loadAnimationBoat(this._textureId);
		ActorAnimation actorAnimation = this.animationDataBoat.dict[0];
		this.actor.spriteAnimation.setFrames(actorAnimation.frames);
		this.actor.spriteAnimation.timeBetweenFrames = actorAnimation.timeBetweenFrames;
		this.actor.spriteAnimation.resetAnim(0);
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00038E8B File Offset: 0x0003708B
	public void madnessAdded(Actor pActor)
	{
		pActor.getHit(100000f, true, AttackType.Other, null, true);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00038E9C File Offset: 0x0003709C
	public void spawnBoatExplosion(Actor pActor)
	{
		this.world.stackEffects.get("boatExplosion").spawnAt(this.actor.currentPosition, this.actor.stats.baseStats.scale);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00038EE9 File Offset: 0x000370E9
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (!Config.paused)
		{
			this.updateBoatAnim(null);
		}
		this.actor.setBodySprite(this.actor.spriteAnimation.currentSpriteGraphic);
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00038F1B File Offset: 0x0003711B
	private void deathAction(Actor pActor)
	{
		if (this.taxiRequest != null)
		{
			TaxiManager.finish(this.taxiRequest);
			this.taxiRequest = null;
		}
		this.unloadUnits(this.actor.currentTile, true);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00038F4C File Offset: 0x0003714C
	internal void unloadUnits(WorldTile pTile, bool pRandomForce = false)
	{
		foreach (Actor actor in this.unitsInside)
		{
			if (actor.data.alive)
			{
				actor.disembarkTo(this, pTile);
				if (pRandomForce)
				{
					actor.applyRandomForce();
				}
			}
		}
		this.unitsInside.Clear();
		this.taxiTarget = null;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00038FC8 File Offset: 0x000371C8
	internal void removeFromTaxi(Actor pActor)
	{
		this.unitsInside.Remove(pActor);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00038FD8 File Offset: 0x000371D8
	private void OnDrawGizmos()
	{
		if (!DebugConfig.isOn(DebugOption.ActorGizmosBoatTaxiRequest))
		{
			return;
		}
		if (this.taxiTarget != null)
		{
			Debug.DrawLine(base.transform.localPosition, this.taxiTarget.posV3, Color.cyan);
		}
		if (this.taxiRequest == null)
		{
			return;
		}
		if (!this.taxiRequest.isStillLegit())
		{
			return;
		}
		if (DebugConfig.isOn(DebugOption.ActorGizmosBoatTaxiRequestTarget))
		{
			Debug.DrawLine(base.transform.localPosition, this.taxiRequest.requestTile.posV3, Color.blue);
		}
		if (DebugConfig.isOn(DebugOption.ActorGizmosBoatTaxiTarget))
		{
			Debug.DrawLine(base.transform.localPosition, this.taxiRequest.target.posV3, Color.red);
		}
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0003908A File Offset: 0x0003728A
	internal void setState(BoatState pState)
	{
		if (pState == BoatState.StayInDock)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(true);
		}
		this.curState = pState;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x000390B1 File Offset: 0x000372B1
	internal bool isState(BoatState pState)
	{
		return this.curState == pState;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000390BC File Offset: 0x000372BC
	internal void addUnitInto(Actor pActor)
	{
		if (this.unitsInside.Add(pActor))
		{
			this.passengerWaitCounter = 0;
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x000390D4 File Offset: 0x000372D4
	internal void updateBoatAnim(Actor pActor = null)
	{
		if (this.animationDataBoat == null)
		{
			this.updateTexture();
		}
		if (!this.actor.data.alive)
		{
			this.setFrames(this.animationDataBoat.broken);
			return;
		}
		if (this.actor.zPosition.y != 0f)
		{
			this.setFrames(this.animationDataBoat.normal);
			return;
		}
		if (!this.actor.is_moving)
		{
			return;
		}
		if (this.lastStep == this.actor.nextStepPosition)
		{
			return;
		}
		this.lastStep = this.actor.nextStepPosition;
		Mathf.Abs(this.actor.currentPosition.x - this.actor.nextStepPosition.x);
		Mathf.Abs(this.actor.currentPosition.y - this.actor.nextStepPosition.y);
		int num = (int)(Toolbox.getAngle(this.actor.currentPosition.x, this.actor.currentPosition.y, this.actor.nextStepPosition.x, this.actor.nextStepPosition.y) * 57.29578f);
		if (this.animationDataBoat.dict.ContainsKey(num))
		{
			this.setFrames(this.animationDataBoat.dict[num]);
		}
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00039239 File Offset: 0x00037439
	public void setWaterFrame(Actor pActor)
	{
		if (!this.actor.data.alive)
		{
			return;
		}
		this.setFrames(this.animationDataBoat.dict[0]);
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00039268 File Offset: 0x00037468
	public void checkBoatLimit()
	{
		if (this.actor.homeBuilding != null && this.actor.homeBuilding.data.alive && this.actor.homeBuilding.GetComponent<Docks>().isMoreLimit(this.actor.stats))
		{
			this.actor.homeBuilding.GetComponent<Docks>().removeBoatFromDock(this.actor);
			this.actor.killHimself(true, AttackType.Other, false, false);
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x000392EB File Offset: 0x000374EB
	private void setFrames(ActorAnimation pAnimation)
	{
		this.actor.spriteAnimation.setFrames(pAnimation.frames);
	}

	// Token: 0x0400060F RID: 1551
	public BoatState curState;

	// Token: 0x04000610 RID: 1552
	private AnimationDataBoat animationDataBoat;

	// Token: 0x04000611 RID: 1553
	internal HashSet<Actor> unitsInside = new HashSet<Actor>();

	// Token: 0x04000612 RID: 1554
	internal bool isTransport;

	// Token: 0x04000613 RID: 1555
	internal TaxiRequest taxiRequest;

	// Token: 0x04000614 RID: 1556
	internal int passengerWaitCounter;

	// Token: 0x04000615 RID: 1557
	private string _textureId;

	// Token: 0x04000616 RID: 1558
	public WorldTile taxiTarget;

	// Token: 0x04000617 RID: 1559
	public bool pickup_near_dock;

	// Token: 0x04000618 RID: 1560
	private Vector3 lastStep = Vector3.zero;
}
