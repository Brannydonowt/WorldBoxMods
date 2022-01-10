using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class SpriteGroupSystemFavorites : SpriteGroupSystem<GroupSpriteObject>
{
	// Token: 0x0600047E RID: 1150 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
	public override void create()
	{
		base.create();
		base.transform.name = "Unit Favorites";
		GameObject gameObject = (GameObject)Resources.Load("Prefabs/PrefabFavoriteUnit");
		this.prefab = gameObject.GetComponent<GroupSpriteObject>();
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0003DD07 File Offset: 0x0003BF07
	protected override GroupSpriteObject createNew()
	{
		GroupSpriteObject groupSpriteObject = base.createNew();
		groupSpriteObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		return groupSpriteObject;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0003DD30 File Offset: 0x0003BF30
	public override void update(float pElapsed)
	{
		this.usedIndex = 0;
		for (int i = 0; i < this.list_to_actors.Count; i++)
		{
			Actor actor = this.list_to_actors[i];
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
			this.t_pos.x = actor.curTransformPosition.x;
			this.t_pos.y = actor.curTransformPosition.y + actor.currentScale.y * actor.spriteRenderer.sprite.bounds.size.y + 1f;
			groupSpriteObject.setPosOnly(ref this.t_pos);
		}
		base.update(pElapsed);
	}
}
