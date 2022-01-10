using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class SpriteGroupSystemUnitItems : SpriteGroupSystem<GroupSpriteObject>
{
	// Token: 0x06000486 RID: 1158 RVA: 0x0003DFFC File Offset: 0x0003C1FC
	public override void create()
	{
		base.create();
		base.transform.name = "Unit Items";
		GameObject gameObject = (GameObject)Resources.Load("Prefabs/PrefabUnitRenderer");
		this.prefab = gameObject.GetComponent<GroupSpriteObject>();
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0003E03B File Offset: 0x0003C23B
	protected override GroupSpriteObject createNew()
	{
		GroupSpriteObject groupSpriteObject = base.createNew();
		groupSpriteObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		return groupSpriteObject;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0003E064 File Offset: 0x0003C264
	public override void update(float pElapsed)
	{
		this.usedIndex = 0;
		for (int i = 0; i < this.list_to_actors.Count; i++)
		{
			Actor actor = this.list_to_actors[i];
			AnimationFrameData animationFrameData = actor.getAnimationFrameData();
			if (animationFrameData != null)
			{
				GroupSpriteObject groupSpriteObject;
				if (this.active.Count > this.usedIndex)
				{
					groupSpriteObject = this.active[this.usedIndex];
				}
				else
				{
					groupSpriteObject = base.get();
				}
				this.usedIndex++;
				groupSpriteObject.setScale(actor.currentScale);
				groupSpriteObject.setColor(ref actor.unit_sprite_color);
				groupSpriteObject.setSharedMaterial(actor.spriteRenderer.sharedMaterial);
				groupSpriteObject.setSprite(actor.s_item_sprite);
				this.t_pos.x = actor.curTransformPosition.x + animationFrameData.posItem.x * actor.currentScale.x;
				this.t_pos.y = actor.curTransformPosition.y + animationFrameData.posItem.y * actor.currentScale.y;
				this.t_pos.z = 0f;
				base.checkRotation(groupSpriteObject, actor);
				this.t_pos.z = actor.curTransformPosition.z + this.z_pos;
				groupSpriteObject.setPosOnly(ref this.t_pos);
			}
		}
		base.update(pElapsed);
	}
}
