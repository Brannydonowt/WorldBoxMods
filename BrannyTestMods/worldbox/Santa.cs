using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class Santa : Actor
{
	// Token: 0x0600078B RID: 1931 RVA: 0x00054C3C File Offset: 0x00052E3C
	internal override void create()
	{
		base.create();
		this.timer = 2f + Toolbox.randomFloat(0f, 2f);
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00054C60 File Offset: 0x00052E60
	public override void update(float pElapsed)
	{
		this.colorEffect -= pElapsed;
		if (this.colorEffect < 0f)
		{
			this.colorEffect = 0f;
		}
		if (this.data.health > 0)
		{
			base.update(pElapsed);
		}
		else
		{
			this.updateFall(pElapsed);
			base.updatePos();
		}
		if (base.transform.position.x > (float)(MapBox.width * 2))
		{
			base.killHimself(true, AttackType.None, false, true);
			return;
		}
		if (this.data.health > 0)
		{
			this.nextStepPosition.x = (float)(MapBox.width * 3);
			this.nextStepPosition.y = this.currentPosition.y;
			this.is_moving = true;
			if (!this.world.isPaused())
			{
				this.currentPosition += new Vector2(this.curStats.speed * 0.1f, Toolbox.randomFloat(-1f, 1f)) * this.world.elapsed;
				this._positionDirty = true;
			}
		}
		else
		{
			if (this.timer_smoke > 0f)
			{
				this.timer_smoke -= this.world.elapsed;
			}
			else
			{
				this.timer_smoke = 0.1f;
				this.world.stackEffects.get("fireSmoke").spawnAt(base.transform.position, 1f);
			}
			this.currentPosition += new Vector2(4f, Toolbox.randomFloat(-1f, 1f)) * this.world.elapsed;
			this._positionDirty = true;
		}
		if (this.zPosition.y == 0f)
		{
			Sfx.play("explosion medium", true, -1f, -1f);
			WorldTile tile = this.world.GetTile((int)this.currentPosition.x, (int)this.currentPosition.y);
			if (tile == null)
			{
				base.killHimself(true, AttackType.Other, true, true);
				return;
			}
			base.killHimself(true, AttackType.Other, true, true);
			MapAction.damageWorld(tile, 5, AssetManager.terraform.get("grenade"));
			this.world.stackEffects.get("explosionSmall").spawnAtRandomScale(tile, 0.45f, 0.6f);
			return;
		}
		else
		{
			if (!this.data.alive)
			{
				return;
			}
			if (this.world.isPaused())
			{
				return;
			}
			this.timer -= this.world.elapsed;
			if (this.timer > 0f)
			{
				return;
			}
			this.timer = 2f + Toolbox.randomFloat(0f, 2f);
			if (this.data.health > 0)
			{
				WorldTile tile2 = this.world.GetTile((int)this.currentPosition.x, (int)this.currentPosition.y);
				if (tile2 == null)
				{
					return;
				}
				this.world.dropManager.spawn(tile2, "santa_bomb", this.zPosition.y, -1f).soundOn = true;
			}
			return;
		}
	}

	// Token: 0x04000A0E RID: 2574
	private float timer = 1f;

	// Token: 0x04000A0F RID: 2575
	private float timer_smoke;

	// Token: 0x04000A10 RID: 2576
	private static string[] ignoredKingdom = new string[]
	{
		"santa"
	};
}
