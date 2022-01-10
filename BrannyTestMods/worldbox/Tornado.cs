using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class Tornado : BaseActorComponent
{
	// Token: 0x06000656 RID: 1622 RVA: 0x0004A2B4 File Offset: 0x000484B4
	internal override void create()
	{
		base.create();
		this.moveTimer = new WorldTimer(0f, new Action(this.moveTick));
		this.brushTimer = new WorldTimer(0f, new Action(this.brushTick));
		this.prepare(Tornado.TORNADO_SCALE_DEFAULT);
		this.actor.callbacks_landed.Add(new BaseActionActor(this.tornado_landed));
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0004A326 File Offset: 0x00048526
	private void tornado_landed(Actor pActor)
	{
		if (!this.actor.inMapBorder())
		{
			this.killTornado();
			return;
		}
		this.newMove();
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0004A344 File Offset: 0x00048544
	internal void prepare(float pForcedScale = 1f)
	{
		if (this.started)
		{
			return;
		}
		this.started = true;
		this.actor = base.GetComponent<Actor>();
		this.actor.data.alive = true;
		base.GetComponent<SpriteRenderer>().flipX = Toolbox.randomBool();
		base.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		if (this.tweener != null && this.tweener.active)
		{
			TweenExtensions.Kill(this.tweener, false);
		}
		this.tweener = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, new Vector3(pForcedScale, pForcedScale, pForcedScale), 0.5f), 6);
		this.shrinkTimer = Toolbox.randomFloat(35f, 70f);
		this.resizeTornado(pForcedScale);
		this.newMove();
		this.moveTimer.setTime(-1f);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0004A424 File Offset: 0x00048624
	internal void forceScaleTo(float pForcedScale)
	{
		this.scale = pForcedScale;
		if (this.tweener != null && this.tweener.active)
		{
			TweenExtensions.Kill(this.tweener, false);
		}
		base.transform.localScale = new Vector3(pForcedScale, pForcedScale, pForcedScale);
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0004A464 File Offset: 0x00048664
	internal void newMove()
	{
		this.actor.findCurrentTile(true);
		this.startPos = this.actor.currentTile.posV3;
		this.targetTile = Toolbox.getRandomTileAround(this.actor.currentTile);
		WorldTile randomTileAround = Toolbox.getRandomTileAround(this.targetTile);
		WorldTile randomTileAround2 = Toolbox.getRandomTileAround(randomTileAround);
		this.firstCorner.x = randomTileAround.posV3.x;
		this.firstCorner.y = randomTileAround.posV3.y;
		this.secondCorner.x = randomTileAround2.posV3.x;
		this.secondCorner.y = randomTileAround2.posV3.y;
		this.targetPos = this.targetTile.posV3;
		this.distanceToTarget = 0f;
		this.speedMod = Toolbox.randomFloat(0.1f, 0.2f);
		this.angleDirection.Set(0f, 0f, 0f);
		this.timeout = (float)Toolbox.randomInt(0, 5);
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0004A570 File Offset: 0x00048770
	public override void update(float pElapsed)
	{
		if (this.world.isPaused())
		{
			return;
		}
		if (!this.actor.data.alive)
		{
			return;
		}
		this.shrinkTimer -= pElapsed;
		if (this.burstTimer > 0f)
		{
			this.burstTimer -= pElapsed;
		}
		if (this.resizeCooldown > 0f)
		{
			this.resizeCooldown -= pElapsed;
		}
		this.updateColorEffect(pElapsed);
		if (this.actor.is_moving)
		{
			return;
		}
		if (this.actor.zPosition.y > 0f)
		{
			this.targetTile = null;
			this.distanceToTarget = 0f;
			return;
		}
		if (this.targetTile == null)
		{
			this.newMove();
		}
		this.distanceToTarget += pElapsed * this.speedMod;
		this.moveTimer.update(pElapsed);
		this.brushTimer.update(pElapsed);
		this.world.applyForce(this.actor.currentTile, 10, 0.5f, false, false, 0, null, null, null);
		if (this.distanceToTarget >= 0.95f)
		{
			this.targetTile = null;
			this.distanceToTarget = 0f;
		}
		if (this.shrinkTimer <= 0f && this.actor.data.alive)
		{
			if (AchievementLibrary.isUnlocked("achievementBabyTornado"))
			{
				this.split(false);
				return;
			}
			this.shrink();
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0004A6D8 File Offset: 0x000488D8
	private void moveTick()
	{
		this.actor.currentPosition = Toolbox.cubeBezier3(ref this.startPos, ref this.firstCorner, ref this.secondCorner, ref this.targetPos, Toolbox.easeInOutQuart(this.distanceToTarget));
		this.actor._positionDirty = true;
		this.actor._currentTileDirty = true;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0004A738 File Offset: 0x00048938
	private void tornadoAction()
	{
		this.brush = Brush.get((int)(this.scale * 6f), "circ_");
		for (int i = 0; i < this.brush.pos.Count; i++)
		{
			BrushPixelData brushPixelData = this.brush.pos[i];
			int num = this.actor.currentTile.pos.x + brushPixelData.x;
			int num2 = this.actor.currentTile.pos.y + brushPixelData.y;
			if (num >= 0 && num < MapBox.width && num2 >= 0 && num2 < MapBox.height)
			{
				WorldTile tileSimple = this.world.GetTileSimple(num, num2);
				if (tileSimple.Type.ocean)
				{
					MapAction.removeLiquid(tileSimple);
					if (Toolbox.randomBool())
					{
						this.spawnBurst(tileSimple, "rain", false);
					}
				}
				if (tileSimple.top_type != null || tileSimple.Type.life)
				{
					MapAction.decreaseTile(tileSimple, "flash");
				}
				if (tileSimple.Type.lava)
				{
					this.world.lavaLayer.removeLava(tileSimple);
					if (Toolbox.randomBool())
					{
						this.spawnBurst(tileSimple, "lava", true);
					}
				}
				if (tileSimple.building != null && tileSimple.building.stats.canBeDamagedByTornado)
				{
					tileSimple.building.getHit(1f, true, AttackType.Other, null, true);
				}
				if (tileSimple.data.fire)
				{
					tileSimple.stopFire(false);
				}
				if (this.burstTimer < 0f && Toolbox.randomChance(0.5f) && !tileSimple.Type.IsType("sand"))
				{
					bool canBeFarmField = tileSimple.Type.canBeFarmField;
				}
			}
		}
		if (this.burstTimer < 0f)
		{
			this.burstTimer = Toolbox.randomFloat(0.5f, 2f);
		}
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0004A928 File Offset: 0x00048B28
	private void spawnBurst(WorldTile pTile, string pType, bool pCreateGround = true)
	{
		if (this.world.dropManager.activeIndex > 3000)
		{
			return;
		}
		this.world.dropManager.spawnBurstPixel(pTile, pType, Toolbox.randomFloat(0.2f, 0.5f), Toolbox.randomFloat(1.3f, 1.8f), 0f, -1f);
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0004A987 File Offset: 0x00048B87
	private void brushTick()
	{
		this.tornadoAction();
		this.world.conwayLayer.checkKillRange(this.actor.currentTile.pos, 7);
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0004A9B0 File Offset: 0x00048BB0
	internal void shrink()
	{
		if (!this.actor.data.alive)
		{
			return;
		}
		if (this.scale < Tornado.TORNADO_SCALE_MIN)
		{
			this.killTornado();
			return;
		}
		if (this.resizeCooldown > 0f)
		{
			return;
		}
		this.scale *= Tornado.TORNADO_SCALE_DECREASE_MOD;
		Sfx.play("tornado", true, -1f, -1f);
		this.startColorEffect("white");
		this.resizeTornado(this.scale);
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0004AA30 File Offset: 0x00048C30
	internal void grow()
	{
		if (!this.actor.data.alive)
		{
			return;
		}
		if (this.scale >= Tornado.TORNADO_SCALE_MAX)
		{
			return;
		}
		if (this.resizeCooldown > 0f)
		{
			return;
		}
		this.scale *= Tornado.TORNADO_SCALE_INCREASE_MOD;
		if (this.scale > Tornado.TORNADO_SCALE_MAX)
		{
			this.scale = Tornado.TORNADO_SCALE_MAX;
		}
		Sfx.play("tornado", true, -1f, -1f);
		this.startColorEffect("red");
		this.resizeTornado(this.scale);
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0004AAC4 File Offset: 0x00048CC4
	internal bool split(bool pHit = false)
	{
		if (!this.actor.data.alive)
		{
			return false;
		}
		Sfx.play("tornado", true, -1f, -1f);
		float pForcedScale = this.scale;
		this.scale *= 0.5f;
		this.resizeTornado(this.scale);
		this.startColorEffect("white");
		if (this.scale < Tornado.TORNADO_SCALE_MIN)
		{
			this.killTornado();
			if (pHit)
			{
				AchievementLibrary.achievementBabyTornado.check();
			}
			return true;
		}
		int num = 1;
		if (!pHit)
		{
			num = Toolbox.randomInt(0, 1);
		}
		for (int i = 0; i < num; i++)
		{
			Tornado component = this.world.createNewUnit("tornado", this.actor.currentTile, null, 0f, null).GetComponent<Tornado>();
			component.forceScaleTo(pForcedScale);
			component.resizeTornado(this.scale);
		}
		return true;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0004ABA4 File Offset: 0x00048DA4
	internal void resizeTornado(float pScale)
	{
		this.scale = pScale;
		this.shrinkTimer = Toolbox.randomFloat(35f, 70f);
		if (this.tweener != null && this.tweener.active)
		{
			TweenExtensions.Kill(this.tweener, false);
		}
		this.tweener = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, new Vector3(this.scale, this.scale, this.scale), 0.4f), 26);
		this.actor.curStats.speed = 6f * this.scale;
		this.actor.curStats.damage = (int)(this.scale * 10f);
		this.resizeCooldown = 0.1f;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0004AC68 File Offset: 0x00048E68
	public void startColorEffect(string pType = "red")
	{
		this.colorEffect = 0.3f;
		if (pType == "red")
		{
			this.colorMaterial = LibraryMaterials.instance.matDamaged;
		}
		else if (pType == "white")
		{
			this.colorMaterial = LibraryMaterials.instance.matHighLighted;
		}
		this.updateColorEffect(0f);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0004ACC8 File Offset: 0x00048EC8
	private void updateColorEffect(float pElapsed)
	{
		if (this.colorEffect == 0f)
		{
			return;
		}
		this.colorEffect -= pElapsed;
		if (this.colorEffect < 0f)
		{
			this.colorEffect = 0f;
		}
		if (this.colorEffect > 0f)
		{
			base.GetComponent<SpriteRenderer>().sharedMaterial = this.colorMaterial;
			return;
		}
		base.GetComponent<SpriteRenderer>().sharedMaterial = LibraryMaterials.instance.matWorldObjects;
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0004AD3D File Offset: 0x00048F3D
	internal void kill()
	{
		this.started = false;
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0004AD48 File Offset: 0x00048F48
	internal void killTornado()
	{
		if (!this.actor.data.alive)
		{
			return;
		}
		this.actor.data.alive = false;
		if (this.tweener != null && this.tweener.active)
		{
			TweenExtensions.Kill(this.tweener, false);
		}
		this.tweener = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, new Vector3(0f, 0f, 0f), 0.4f), 26), new TweenCallback(this.finishAndRemove));
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0004ADDC File Offset: 0x00048FDC
	private void finishAndRemove()
	{
		this.actor.killHimself(true, AttackType.None, false, true);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0004ADF0 File Offset: 0x00048FF0
	internal static void growTornados(WorldTile pTile)
	{
		Tornado.resizeOnTile(pTile, "grow");
		for (int i = 0; i < pTile.neighboursAll.Count; i++)
		{
			Tornado.resizeOnTile(pTile.neighboursAll[i], "grow");
		}
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0004AE34 File Offset: 0x00049034
	internal static void shrinkTornados(WorldTile pTile)
	{
		Tornado.resizeOnTile(pTile, "shrink");
		for (int i = 0; i < pTile.neighboursAll.Count; i++)
		{
			Tornado.resizeOnTile(pTile.neighboursAll[i], "shrink");
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0004AE78 File Offset: 0x00049078
	internal static void resizeOnTile(WorldTile pTile, string direction)
	{
		for (int i = 0; i < pTile.units.Count; i++)
		{
			Actor actor = pTile.units[i];
			if (actor.base_data.alive && actor.a.stats.id == "tornado")
			{
				if (direction == "grow")
				{
					actor.GetComponent<Tornado>().grow();
				}
				else
				{
					actor.GetComponent<Tornado>().shrink();
				}
			}
		}
	}

	// Token: 0x0400083E RID: 2110
	public static float TORNADO_SCALE_DECREASE_MOD = 0.9f;

	// Token: 0x0400083F RID: 2111
	public static float TORNADO_SCALE_INCREASE_MOD = 1.1f;

	// Token: 0x04000840 RID: 2112
	public static float TORNADO_SCALE_MIN = 0.1f;

	// Token: 0x04000841 RID: 2113
	public static float TORNADO_SCALE_MAX = 20f;

	// Token: 0x04000842 RID: 2114
	public static float TORNADO_SCALE_DEFAULT = 0.5f;

	// Token: 0x04000843 RID: 2115
	private float shrinkTimer = 1f;

	// Token: 0x04000844 RID: 2116
	private Tweener tweener;

	// Token: 0x04000845 RID: 2117
	private bool started;

	// Token: 0x04000846 RID: 2118
	private float scale = 1f;

	// Token: 0x04000847 RID: 2119
	private float timeout;

	// Token: 0x04000848 RID: 2120
	private WorldTile targetTile;

	// Token: 0x04000849 RID: 2121
	private Vector3 startPos;

	// Token: 0x0400084A RID: 2122
	private Vector3 firstCorner;

	// Token: 0x0400084B RID: 2123
	private Vector3 secondCorner;

	// Token: 0x0400084C RID: 2124
	public float distanceToTarget;

	// Token: 0x0400084D RID: 2125
	private Vector3 angleDirection;

	// Token: 0x0400084E RID: 2126
	private Vector3 targetPos;

	// Token: 0x0400084F RID: 2127
	private float speedMod = 0.02f;

	// Token: 0x04000850 RID: 2128
	private BrushData brush;

	// Token: 0x04000851 RID: 2129
	private float resizeCooldown = 1.5f;

	// Token: 0x04000852 RID: 2130
	private WorldTimer moveTimer;

	// Token: 0x04000853 RID: 2131
	private WorldTimer brushTimer;

	// Token: 0x04000854 RID: 2132
	private float burstTimer = 1f;

	// Token: 0x04000855 RID: 2133
	protected ActorDirection currentDirection;

	// Token: 0x04000856 RID: 2134
	internal float colorEffect;

	// Token: 0x04000857 RID: 2135
	internal Material colorMaterial;
}
