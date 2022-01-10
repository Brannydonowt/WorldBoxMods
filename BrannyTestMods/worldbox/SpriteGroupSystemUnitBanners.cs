using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class SpriteGroupSystemUnitBanners : SpriteGroupSystem<GroupSpriteObject>
{
	// Token: 0x06000482 RID: 1154 RVA: 0x0003DE20 File Offset: 0x0003C020
	public override void create()
	{
		base.create();
		base.transform.name = "Unit Banners";
		GameObject gameObject = (GameObject)Resources.Load("Prefabs/PrefabUnitBanner");
		this.prefab = gameObject.GetComponent<GroupSpriteObject>();
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0003DE5F File Offset: 0x0003C05F
	protected override GroupSpriteObject createNew()
	{
		GroupSpriteObject groupSpriteObject = base.createNew();
		groupSpriteObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		return groupSpriteObject;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0003DE88 File Offset: 0x0003C088
	public override void update(float pElapsed)
	{
		this.usedIndex = 0;
		for (int i = 0; i < this.list_to_actors.Count; i++)
		{
			Actor actor = this.list_to_actors[i];
			AnimationFrameData animationFrameData = actor.getAnimationFrameData();
			if (actor.kingdom.isCiv() && actor.kingdom.kingdomColor != null && animationFrameData != null)
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
				groupSpriteObject.setColor(ref actor.kingdom.kingdomColor.colorBorderOut);
				this.t_pos.x = actor.curTransformPosition.x + animationFrameData.posHead.x * actor.currentScale.x;
				this.t_pos.y = actor.curTransformPosition.y + animationFrameData.posHead.y * actor.currentScale.y;
				this.t_pos.z = 0f;
				base.checkRotation(groupSpriteObject, actor);
				this.t_pos.z = actor.curTransformPosition.z + this.z_pos;
				groupSpriteObject.setPosOnly(ref this.t_pos);
			}
		}
		base.update(pElapsed);
	}
}
