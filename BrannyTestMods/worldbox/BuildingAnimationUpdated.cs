using System;

// Token: 0x020000C4 RID: 196
public class BuildingAnimationUpdated : BaseBuildingComponent
{
	// Token: 0x0600042F RID: 1071 RVA: 0x0003C257 File Offset: 0x0003A457
	internal override void create()
	{
		base.create();
		this.spriteAnimation = base.gameObject.GetComponent<SpriteAnimation>();
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x0003C270 File Offset: 0x0003A470
	public override void update(float pElapsed)
	{
		this.spriteAnimation.update(pElapsed);
	}

	// Token: 0x04000656 RID: 1622
	private SpriteAnimation spriteAnimation;
}
