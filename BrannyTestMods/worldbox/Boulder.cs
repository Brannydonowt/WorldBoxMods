using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class Boulder : Actor
{
	// Token: 0x06000705 RID: 1797 RVA: 0x00050568 File Offset: 0x0004E768
	internal override void create()
	{
		base.create();
		this.angleRotation = Toolbox.randomFloat(-200f, 200f);
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00050588 File Offset: 0x0004E788
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.impactEffect > 0f)
		{
			this.impactEffect -= pElapsed;
		}
		if (this.zPosition.y != 0f)
		{
			this.angle += this.angleRotation * pElapsed;
			base.transform.localEulerAngles = new Vector3(0f, 0f, this.angle);
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x000505FE File Offset: 0x0004E7FE
	protected override void updateForce(float pElapsed)
	{
		base.updateForce(pElapsed);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00050608 File Offset: 0x0004E808
	private void spawnEffect(string pEffect)
	{
		if (this.impactEffect > 0f)
		{
			return;
		}
		this.impactEffect = 0.8f;
		Vector3 pVector = this.currentPosition;
		pVector.y -= 2f;
		this.world.stackEffects.get(pEffect).spawnAt(pVector, base.transform.localScale.x);
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00050674 File Offset: 0x0004E874
	protected override void actionLanded()
	{
		this._currentTileDirty = true;
		base.findCurrentTile(true);
		this._positionDirty = true;
		base.updatePos();
		base.actionLanded();
		if (this.currentBounce > 0.3f)
		{
			if (this.currentBounce > 1.5f)
			{
				Vector3 pVector = this.currentPosition;
				pVector.y -= 2f;
				ExplosionFlash explosionFlash = (ExplosionFlash)this.world.stackEffects.get("explosionWave").spawnNew();
				if (explosionFlash != null)
				{
					explosionFlash.startFlash(pVector, (int)this.currentBounce, 6f);
				}
			}
			if (this.currentBounce == 3f)
			{
				this.world.startShake(0.3f, 0.01f, 2f, true, true);
			}
			this.currentBounce *= 0.7f;
			this.dirX /= 2f;
			this.dirY /= 2f;
			base.addForce(this.dirX, this.dirY, this.currentBounce);
			if (!base.inMapBorder())
			{
				this.spawnEffect("boulderImpactWater");
				return;
			}
			if (this.currentTile != null)
			{
				if (this.currentTile.Type.ocean || this.currentTile.Type.swamp)
				{
					this.spawnEffect("boulderImpactWater");
				}
				else
				{
					this.spawnEffect("boulderImpact");
				}
				this.world.loopWithBrush(this.currentTile, Brush.get(5, "circ_"), new PowerActionWithID(Boulder.tileDrawBoulder), "boulder");
				this.world.applyForce(this.currentTile, 5, 0.5f, false, false, 0, null, null, null);
				this.world.conwayLayer.checkKillRange(this.currentTile.pos, 5);
				return;
			}
		}
		else
		{
			if (this.currentTile.Type.ocean || this.currentTile.Type.swamp)
			{
				this.spawnEffect("boulderImpactWater");
			}
			else
			{
				this.spawnEffect("boulderImpact");
			}
			this.impactEffect = 0f;
			if (base.inMapBorder())
			{
				MapAction.damageWorld(this.currentTile, 10, AssetManager.terraform.get("bomb"));
			}
			this.spawnEffect("explosionSmall");
			Sfx.play("explosion meteorite", true, -1f, -1f);
			base.killHimself(true, AttackType.None, false, true);
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x000508E0 File Offset: 0x0004EAE0
	public static bool tileDrawBoulder(WorldTile pTile, string pPowerID)
	{
		foreach (Actor actor in pTile.units)
		{
			actor.getHit(1000f, true, AttackType.Other, null, true);
		}
		Sfx.play("bowlingBall", true, (float)pTile.x, (float)pTile.y);
		if ((pTile.Type.ocean || pTile.Type.swamp) && Toolbox.randomChance(0.3f))
		{
			MapBox.instance.dropManager.spawnBurstPixel(pTile, "rain", Toolbox.randomFloat(0.3f, 0.8f), Toolbox.randomFloat(0.6f, 1.2f), 0f, -1f);
		}
		if (pTile.Type.lava && Toolbox.randomChance(0.3f))
		{
			MapBox.instance.dropManager.spawnBurstPixel(pTile, "lava", Toolbox.randomFloat(0.3f, 0.8f), Toolbox.randomFloat(0.6f, 1.2f), 0f, -1f);
		}
		MapAction.decreaseTile(pTile, "destroy");
		return true;
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00050A18 File Offset: 0x0004EC18
	internal override void spawnOn(WorldTile pTile, float pZHeight = 0f)
	{
		base.spawnOn(pTile, 0f);
		this.dirX = Toolbox.randomFloat(-3f, 3f);
		this.dirY = Toolbox.randomFloat(-3f, 3f);
		base.addForce(this.dirX, this.dirY, this.currentBounce);
		this.zPosition.y = 50f;
		this.forceVector.z = -6f;
	}

	// Token: 0x04000957 RID: 2391
	private float currentBounce = 3f;

	// Token: 0x04000958 RID: 2392
	private float dirX;

	// Token: 0x04000959 RID: 2393
	private float dirY;

	// Token: 0x0400095A RID: 2394
	private float angle;

	// Token: 0x0400095B RID: 2395
	private float angleRotation;

	// Token: 0x0400095C RID: 2396
	private float impactEffect;
}
