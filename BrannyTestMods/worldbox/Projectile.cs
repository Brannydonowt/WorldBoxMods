using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class Projectile : BaseEffect
{
	// Token: 0x06000623 RID: 1571 RVA: 0x00048B28 File Offset: 0x00046D28
	public void start(Vector3 pStart, Vector3 pTarget, string pAssetID, float pTargetZ = 0f)
	{
		if (!this.created)
		{
			this.create();
		}
		this.byWho = null;
		this.asset = AssetManager.projectiles.get(pAssetID);
		this.spriteAnimation = base.GetComponent<SpriteAnimation>();
		if (this.asset._frames == null || this.asset._frames.Length == 0)
		{
			this.asset._frames = Resources.LoadAll<Sprite>("effects/projectiles/" + this.asset.texture);
		}
		this.spriteAnimation.setFrames(this.asset._frames);
		this.spriteAnimation.setFrameIndex(0);
		this.spriteAnimation.enabled = this.asset.animated;
		this.spriteAnimation.looped = this.asset.looped;
		this.spriteAnimation.timeBetweenFrames = this.asset.animation_speed;
		Vector3 vector = new Vector3(pStart.x, pStart.y, 0f);
		this.curScale = this.asset.startScale;
		this.targetScale = this.asset.targetScale;
		this.m_transform.localScale = new Vector3(this.curScale, this.curScale);
		this.currentPosition = vector;
		this.m_transform.position = vector;
		this.speed = this.asset.speed + Toolbox.randomFloat(0f, this.asset.speed_random);
		this.vecStart = new Vector3(pStart.x, pStart.y);
		this.vecTarget = new Vector3(pTarget.x, pTarget.y);
		this.targetZ = pTargetZ;
		this.target_reached = false;
		this.halfHitChecked = false;
		this.t_rotation = Vector3.zero;
		if (pTargetZ != 0f)
		{
			if (pTargetZ < 5f)
			{
				pTargetZ = 20f;
			}
			this.vecTargetZ = new Vector3(this.vecTarget.x, this.vecTarget.y + pTargetZ);
			float num = Toolbox.Dist(this.vecStart.x, this.vecStart.y, this.vecTarget.x, this.vecTarget.y);
			this.vecTarget = Toolbox.getNewPoint(this.vecStart.x, this.vecStart.y, this.vecTarget.x, this.vecTarget.y, num * 2f, false);
		}
		this.timeInAir = 0f;
		this.timeToTarget = (Toolbox.DistVec3(this.vecStart, this.vecTarget) + pTargetZ) / this.speed;
		if (pTargetZ > 0f && this.timeToTarget < 0.7f)
		{
			this.timeToTarget = 0.7f;
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00048DED File Offset: 0x00046FED
	public void setStats(BaseStats pStats)
	{
		this.curStats.clear();
		this.curStats.addStats(pStats);
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00048E08 File Offset: 0x00047008
	internal override void updateShadow()
	{
		if (this.asset.texture_shadow == string.Empty)
		{
			return;
		}
		if (!Config.shadowsActive)
		{
			if (this.shadow != null)
			{
				Object.Destroy(this.shadow.gameObject);
				this.shadow = null;
				this.lastShadowPos.x = -100000f;
				this.lastShadowPos.y = -100000f;
			}
			return;
		}
		if (this.shadow == null)
		{
			this.shadow = this.world.createShadow(this, this.asset.texture_shadow);
			base.resetShadow();
		}
		Vector3 vector = Vector3.Lerp(this.vecStart, this.vecTarget, this.timeInAir / this.timeToTarget);
		base.checkShadowPosition(vector);
		float z = Toolbox.getAngle(vector.x, vector.y, this.vecTarget.x, this.vecTarget.y) * 57.29578f;
		this.shadow.m_transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, z));
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00048F24 File Offset: 0x00047124
	public float getAngle()
	{
		float height = this.timeToTarget * 4f;
		if (this.targetZ != 0f)
		{
			height = this.targetZ;
		}
		Vector3 vector = Projectile.Parabola(this.vecStart, this.vecTarget, height, 0f);
		Vector3 vector2 = Projectile.Parabola(this.vecStart, this.vecTarget, height, 0.1f);
		return Toolbox.getAngle(vector.x, vector.y, vector2.x, vector2.y) * 57.29578f;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00048FA8 File Offset: 0x000471A8
	public override void update(float pElapsed)
	{
		if (this.state == 3)
		{
			return;
		}
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		base.update(pElapsed);
		if (this.target_reached)
		{
			return;
		}
		this.timeInAir += pElapsed;
		if (this.curScale < this.targetScale)
		{
			this.curScale += pElapsed * 0.2f;
			if (this.curScale > this.targetScale)
			{
				this.curScale = this.targetScale;
			}
			this.m_transform.localScale = new Vector3(this.curScale, this.curScale);
		}
		if (this.asset.rotate)
		{
			this.t_rotation.y = this.t_rotation.y + Toolbox.randomFloat(10f, 50f);
			this.m_transform.localEulerAngles = this.t_rotation;
		}
		if (this.asset.look_at_target)
		{
			float z = Toolbox.getAngle(base.transform.position.x, base.transform.position.y, this.vecTarget.x, this.vecTarget.y) * 57.29578f;
			this.m_transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, z));
		}
		if (this.asset.trailEffect_enabled)
		{
			if (this.timer_smoke > 0f)
			{
				this.timer_smoke -= pElapsed;
			}
			else
			{
				BaseEffect baseEffect = this.world.stackEffects.get(this.asset.trailEffect_texture).spawnAt(this.m_transform.position, this.asset.trailEffect_scale);
				if (this.asset.look_at_target && baseEffect != null)
				{
					baseEffect.transform.rotation = this.m_transform.rotation;
				}
				this.timer_smoke = this.asset.trailEffect_timer;
			}
		}
		float num = this.timeInAir / this.timeToTarget;
		this.currentPosition = Vector3.Lerp(this.vecStart, this.vecTarget, num);
		if (this.asset.parabolic)
		{
			float height = this.timeToTarget * 4f;
			if (this.targetZ != 0f)
			{
				height = this.targetZ;
			}
			this.currentPosition = Projectile.Parabola(this.vecStart, this.vecTarget, height, num);
			Vector2 a = Projectile.Parabola(this.vecStart, this.vecTarget, height, (this.timeInAir + pElapsed) / this.timeToTarget);
			this.m_transform.rotation = Toolbox.LookAt2D(a - this.currentPosition);
		}
		this.m_transform.position = this.currentPosition;
		this.updateShadow();
		if (!this.halfHitChecked && num >= 0.5f)
		{
			this.halfHitChecked = true;
			if (this.checkHit(this.vecTargetZ))
			{
				this.targetReached();
				return;
			}
		}
		if (this.timeInAir > this.timeToTarget)
		{
			if (!this.checkHit(this.vecTarget))
			{
				this.world.stackEffects.get("miss").spawnAt(this.m_transform.position, 0.1f);
			}
			this.targetReached();
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x000492E4 File Offset: 0x000474E4
	public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
	{
		Func<float, float> func = (float x) => -4f * height * x * x + 4f * height * x;
		Vector3 vector = Vector3.Lerp(start, end, t);
		return new Vector3(vector.x, func(t) + Mathf.Lerp(start.y, end.y, t), vector.z);
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00049340 File Offset: 0x00047540
	public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
	{
		Func<float, float> func = (float x) => -4f * height * x * x + 4f * height * x;
		return new Vector2(Vector2.Lerp(start, end, t).x, func(t) + Mathf.Lerp(start.y, end.y, t));
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00049394 File Offset: 0x00047594
	private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float num = 1f - t;
		float num2 = t * t;
		float num3 = num * num;
		float d = num3 * num;
		float d2 = num2 * t;
		return d * p0 + 3f * num3 * t * p1 + 3f * num * num2 * p2 + d2 * p3;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x000493F4 File Offset: 0x000475F4
	private bool checkHit(Vector3 pHitVector)
	{
		bool result = false;
		if (this.byWho != null && this.byWho.base_data.alive)
		{
			result = this.world.newAttack(this.byWho, pHitVector, this.world.GetTile((int)this.currentPosition.x, (int)this.currentPosition.y), this.targetObject);
		}
		return result;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00049460 File Offset: 0x00047660
	private void targetReached()
	{
		this.target_reached = true;
		if (!string.IsNullOrEmpty(this.asset.endEffect))
		{
			this.world.stackEffects.get(this.asset.endEffect).spawnAt(this.vecTarget, 0.25f);
		}
		WorldTile tile = this.world.GetTile((int)this.vecTarget.x, (int)this.vecTarget.y);
		if (this.asset.playImpactSound)
		{
			Sfx.play(this.asset.impactSoundID, true, -1f, -1f);
		}
		if (tile != null)
		{
			if (this.asset.world_actions != null)
			{
				this.asset.world_actions(null, tile);
			}
			if (this.asset.hitFreeze)
			{
				MapAction.freezeTile(tile);
				for (int i = 0; i < tile.neighbours.Count; i++)
				{
					WorldTile pTile = tile.neighbours[i];
					if (Toolbox.randomBool())
					{
						MapAction.freezeTile(pTile);
					}
				}
			}
			if (this.asset.hitShake)
			{
				this.world.startShake(0.01f, 0.01f, 0.25f, true, true);
			}
			if (this.asset.terraformOption != string.Empty)
			{
				MapAction.damageWorld(tile, this.asset.terraformRange, AssetManager.terraform.get(this.asset.terraformOption));
			}
		}
		base.kill();
		if (this.shadow != null)
		{
			Object.Destroy(this.shadow.gameObject);
			this.shadow = null;
		}
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x000495F8 File Offset: 0x000477F8
	private void OnDrawGizmos()
	{
		Debug.DrawLine(this.m_transform.position, this.vecStart, Color.black);
	}

	// Token: 0x04000803 RID: 2051
	internal BaseStats curStats = new BaseStats();

	// Token: 0x04000804 RID: 2052
	internal BaseSimObject byWho;

	// Token: 0x04000805 RID: 2053
	public BaseSimObject targetObject;

	// Token: 0x04000806 RID: 2054
	private float speed;

	// Token: 0x04000807 RID: 2055
	private Vector3 vecStart;

	// Token: 0x04000808 RID: 2056
	private Vector3 vecTarget;

	// Token: 0x04000809 RID: 2057
	private Vector3 vecTargetZ;

	// Token: 0x0400080A RID: 2058
	private float curScale;

	// Token: 0x0400080B RID: 2059
	private float targetScale;

	// Token: 0x0400080C RID: 2060
	private ProjectileAsset asset;

	// Token: 0x0400080D RID: 2061
	private float timeToTarget;

	// Token: 0x0400080E RID: 2062
	private float timeInAir;

	// Token: 0x0400080F RID: 2063
	private bool target_reached;

	// Token: 0x04000810 RID: 2064
	public float targetZ;

	// Token: 0x04000811 RID: 2065
	private bool halfHitChecked;

	// Token: 0x04000812 RID: 2066
	private Vector3 t_rotation = Vector3.zero;

	// Token: 0x04000813 RID: 2067
	private float timer_smoke;
}
