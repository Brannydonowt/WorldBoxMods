using System;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class UnitSpriteAnimation : SpriteAnimation
{
	// Token: 0x060007AD RID: 1965 RVA: 0x00055CC0 File Offset: 0x00053EC0
	public override void create()
	{
		base.create();
		this.actor = base.GetComponent<Actor>();
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00055CD4 File Offset: 0x00053ED4
	protected override void updateFrame()
	{
		base.updateFrame();
		if (this.currentSpriteGraphic == null)
		{
			return;
		}
		AnimationFrameData animationFrameData = null;
		AnimationDataUnit actorAnimationData = this.actor.actorAnimationData;
		if (actorAnimationData != null)
		{
			actorAnimationData.frameData.TryGetValue(this.currentSpriteGraphic.name, ref animationFrameData);
		}
		if (animationFrameData != null)
		{
			this.actor.frameData = animationFrameData;
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00055D30 File Offset: 0x00053F30
	internal override void applyCurrentSpriteGraphics(Sprite pSprite)
	{
		this.actor.setBodySprite(pSprite);
	}

	// Token: 0x04000A34 RID: 2612
	private Actor actor;
}
