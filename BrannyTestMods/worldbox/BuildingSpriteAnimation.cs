using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class BuildingSpriteAnimation : SpriteAnimation
{
	// Token: 0x0600070D RID: 1805 RVA: 0x00050AA6 File Offset: 0x0004ECA6
	public override void create()
	{
		base.create();
		this.building = base.GetComponent<Building>();
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00050ABA File Offset: 0x0004ECBA
	internal override void applyCurrentSpriteGraphics(Sprite pSprite)
	{
		this.building.setMainSprite(pSprite);
	}

	// Token: 0x0400095D RID: 2397
	private Building building;
}
