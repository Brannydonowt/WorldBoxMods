using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class SpriteGroupSystemUnitShadows : SpriteGroupSystem<GroupSpriteObject>
{
	// Token: 0x0600048A RID: 1162 RVA: 0x0003E1CC File Offset: 0x0003C3CC
	public override void create()
	{
		base.create();
		base.transform.name = "Unit Shadows";
		GameObject gameObject = (GameObject)Resources.Load("shadows/PrefabShadowObject");
		this.prefab = gameObject.GetComponent<GroupSpriteObject>();
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0003E20C File Offset: 0x0003C40C
	public override void update(float pElapsed)
	{
		if (!Config.shadowsActive)
		{
			base.clearFull();
			return;
		}
		if (!MapBox.instance.qualityChanger.renderShadowsUnits())
		{
			this.list_to_actors.Clear();
		}
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
			groupSpriteObject.setSprite(actor.stats.shadow_sprite);
			this.usedIndex++;
			groupSpriteObject.set(ref actor.curShadowPosition, ref actor.currentScale);
		}
		base.update(pElapsed);
	}
}
