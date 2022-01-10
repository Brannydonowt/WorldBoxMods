using System;

// Token: 0x020000FC RID: 252
public class AntimatterBombEffect : BaseEffect
{
	// Token: 0x06000592 RID: 1426 RVA: 0x0004539A File Offset: 0x0004359A
	internal override void create()
	{
		base.create();
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x000453A2 File Offset: 0x000435A2
	private new void Awake()
	{
		this.spriteAnimation = base.GetComponent<SpriteAnimation>();
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000453B0 File Offset: 0x000435B0
	private void Update()
	{
		this.world.startShake(0.3f, 0.01f, 2f, true, true);
		if (this.spriteAnimation.currentFrameIndex >= 15 && !this.used)
		{
			this.used = true;
			this.world.applyForce(this.tile, 10, 0f, true, false, 1000, null, null, null);
			this.world.loopWithBrush(this.tile, Brush.get(11, "circ_"), new PowerActionWithID(this.tileAntimatter), "antimatter");
		}
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00045447 File Offset: 0x00043647
	public bool tileAntimatter(WorldTile pTile, string pPowerID)
	{
		MapAction.terraformMain(pTile, TileLibrary.pit_deep_ocean, TerraformLibrary.destroy_no_flash);
		return true;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0004545A File Offset: 0x0004365A
	internal void spawn(WorldTile pTile)
	{
		this.tile = pTile;
		this.used = false;
		this.prepare(pTile, 1f);
		this.spriteAnimation.resetAnim(0);
		Sfx.play("antimatterBomb", true, -1f, -1f);
	}

	// Token: 0x04000787 RID: 1927
	private bool used;
}
