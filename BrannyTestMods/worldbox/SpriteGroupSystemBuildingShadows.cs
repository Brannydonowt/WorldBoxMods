using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class SpriteGroupSystemBuildingShadows : SpriteGroupSystem<GroupSpriteObject>
{
	// Token: 0x0600047B RID: 1147 RVA: 0x0003DA30 File Offset: 0x0003BC30
	public override void create()
	{
		base.create();
		base.transform.name = "Building Shadows";
		GameObject gameObject = (GameObject)Resources.Load("shadows/PrefabShadowObject");
		this.prefab = gameObject.GetComponent<GroupSpriteObject>();
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0003DA70 File Offset: 0x0003BC70
	public override void update(float pElapsed)
	{
		if (!Config.shadowsActive)
		{
			base.clearFull();
			return;
		}
		this.usedIndex = 0;
		bool flag = !MapBox.instance.qualityChanger.lowRes;
		if (MapBox.instance.camera.orthographicSize > 60f)
		{
			flag = false;
		}
		if (!flag)
		{
			base.update(pElapsed);
			return;
		}
		for (int i = 0; i < MapBox.instance.zoneCalculator.zones.Count; i++)
		{
			TileZone tileZone = MapBox.instance.zoneCalculator.zones[i];
			if (tileZone.visible && tileZone.buildings_all.Count != 0)
			{
				foreach (Building building in tileZone.buildings_all)
				{
					if (building._is_visible && building.stats.shadow)
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
						Sprite sprite;
						if (building.data.underConstruction)
						{
							sprite = building.stats.sprites.construction_shadow;
						}
						else
						{
							sprite = building.animData.shadows[building.spriteAnimation.currentFrameIndex];
						}
						if (sprite == null)
						{
							Debug.Log("Sprite is missing: " + building.s_main_sprite.name);
						}
						else
						{
							groupSpriteObject.setSprite(sprite);
						}
						Vector3 currentScale = building.currentScale;
						if (building.curTween != null && building.curTween.active)
						{
							currentScale.y = building.transform.localScale.y;
						}
						Vector2 currentPosition = building.currentPosition;
						currentPosition.x += 2f * currentScale.y;
						currentPosition.y -= 1f * currentScale.x;
						groupSpriteObject.set(ref currentPosition, ref currentScale);
					}
				}
			}
		}
		base.update(pElapsed);
	}
}
