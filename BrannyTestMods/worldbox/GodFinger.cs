using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class GodFinger : BaseActorComponent
{
	// Token: 0x06000750 RID: 1872 RVA: 0x00053310 File Offset: 0x00051510
	internal override void create()
	{
		base.create();
		this.godPower = AssetManager.powers.get(Toolbox.getRandom<string>(this.powers));
		this.fingerTip = base.transform.Find("Tip").gameObject.GetComponent<SpriteAnimation>();
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00053360 File Offset: 0x00051560
	internal void newMove()
	{
		this.haveTarget = true;
		this.target = Toolbox.getRandom<WorldTile>(this.world.tilesList).posV3;
		this.rotateToward1 = Toolbox.getRandom<WorldTile>(this.world.tilesList).posV3;
		this.rotateToward2 = Toolbox.getRandom<WorldTile>(this.world.tilesList).posV3;
		this.rotateToward3 = Toolbox.getRandom<WorldTile>(this.world.tilesList).posV3;
		this.randomDirection.x = Toolbox.randomFloat(-20f, 20f);
		this.randomDirection.y = Toolbox.randomFloat(-20f, 20f);
		this.startPos = this.actor.currentPosition;
		this.distanceToTarget = 0f;
		this.speedMod = Toolbox.randomFloat(0.1f, 0.4f);
		this.draw = Toolbox.randomBool();
		if (!this.draw)
		{
			this.speedMod = Toolbox.randomFloat(0.2f, 0.6f);
		}
		this.angleDirection.Set(0f, 0f, 0f);
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0005348C File Offset: 0x0005168C
	internal void lightAction()
	{
		AchievementLibrary.achievementGodFingerLightning.check();
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x000534A0 File Offset: 0x000516A0
	public override void update(float pElapsed)
	{
		this.fingerTip.update(pElapsed);
		if (!this.actor.data.alive)
		{
			this.fingerTip.gameObject.SetActive(false);
			return;
		}
		if (this.world.isPaused())
		{
			return;
		}
		if (this.draw)
		{
			this.fingerTip.gameObject.SetActive(true);
			this.angleHand += pElapsed * 20f;
			if (this.angleHand > 30f)
			{
				this.angleHand = 30f;
			}
		}
		else
		{
			this.fingerTip.gameObject.SetActive(false);
			this.angleHand -= pElapsed * 20f;
			if (this.angleHand <= 0f)
			{
				this.angleHand = 0f;
			}
		}
		if (this.actor.zPosition.y > 0f)
		{
			this.haveTarget = false;
			this.distanceToTarget = 0f;
			this.draw = false;
			return;
		}
		base.transform.localEulerAngles = new Vector3(0f, 0f, this.angleHand);
		if (this.wait > 0f)
		{
			this.wait -= pElapsed;
			return;
		}
		if (!this.haveTarget)
		{
			this.newMove();
		}
		this.distanceToTarget += pElapsed * this.speedMod;
		float num = 30f;
		if (this.draw)
		{
			this.rotateToward1 = Vector3.MoveTowards(this.rotateToward1, this.rotateToward2, num * pElapsed);
			this.rotateToward2 = Vector3.MoveTowards(this.rotateToward2, this.rotateToward3, num * pElapsed);
			this.rotateToward3 = Vector3.MoveTowards(this.rotateToward3, this.rotateToward1, num * pElapsed);
			this.target = Vector3.MoveTowards(this.target, this.rotateToward1, num * pElapsed);
		}
		this.actor.currentPosition = Vector3.Lerp(this.startPos, this.target, this.distanceToTarget);
		this.actor._positionDirty = true;
		this.actor._currentTileDirty = true;
		if (this.draw && this.actor.currentTile != null && this.actor.currentPosition.x <= (float)MapBox.width && this.actor.currentPosition.y <= (float)MapBox.height && this.actor.currentPosition.x >= 0f && this.actor.currentPosition.y >= 0f)
		{
			this.world.conwayLayer.checkKillRange(this.actor.currentTile.pos, 2);
			this.world.loopWithBrush(this.actor.currentTile, Brush.get(2, "circ_"), this.godPower.click_action, this.godPower.id);
			this.world.loopWithBrush(this.actor.currentTile, Brush.get(2, "circ_"), new PowerActionWithID(GodFinger.fingerTile), "godFinger");
		}
		if (this.distanceToTarget >= 0.9f)
		{
			this.haveTarget = false;
			this.distanceToTarget = 0f;
			this.draw = false;
			if (Toolbox.randomChance(0.4f))
			{
				this.wait = Toolbox.randomFloat(0f, 4f);
			}
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0005380C File Offset: 0x00051A0C
	public static bool fingerTile(WorldTile pTile, string pPowerID)
	{
		for (int i = 0; i < pTile.units.Count; i++)
		{
			Actor actor = pTile.units[i];
			if (!actor.stats.flag_finger && actor.stats.canBeKilledByStuff && actor.data.alive)
			{
				actor.getHit(100000f, true, AttackType.Other, null, true);
			}
		}
		return true;
	}

	// Token: 0x040009AC RID: 2476
	public float wait;

	// Token: 0x040009AD RID: 2477
	private float speedMod = 1f;

	// Token: 0x040009AE RID: 2478
	private Vector3 target;

	// Token: 0x040009AF RID: 2479
	private Vector3 startPos;

	// Token: 0x040009B0 RID: 2480
	private Vector3 randomDirection;

	// Token: 0x040009B1 RID: 2481
	private Vector3 rotateToward1;

	// Token: 0x040009B2 RID: 2482
	private Vector3 rotateToward2;

	// Token: 0x040009B3 RID: 2483
	private Vector3 rotateToward3;

	// Token: 0x040009B4 RID: 2484
	private Vector3 angleDirection;

	// Token: 0x040009B5 RID: 2485
	private GodPower godPower;

	// Token: 0x040009B6 RID: 2486
	public float distanceToTarget;

	// Token: 0x040009B7 RID: 2487
	public bool haveTarget;

	// Token: 0x040009B8 RID: 2488
	private bool draw;

	// Token: 0x040009B9 RID: 2489
	private float angleHand;

	// Token: 0x040009BA RID: 2490
	private string[] powers = new string[]
	{
		"tileSand",
		"tileHighSoil",
		"tileSoil",
		"tileHills",
		"tileMountains"
	};

	// Token: 0x040009BB RID: 2491
	private SpriteAnimation fingerTip;
}
