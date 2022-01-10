using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class QualityChanger : MonoBehaviour
{
	// Token: 0x06000346 RID: 838 RVA: 0x00035574 File Offset: 0x00033774
	internal void reset()
	{
		this.lowRes = true;
		this.changeMod = 1f;
		this.changeModTween = 1f;
		this.world.worldLayer.setRendererEnabled(true);
	}

	// Token: 0x06000347 RID: 839 RVA: 0x000355A4 File Offset: 0x000337A4
	internal void update()
	{
		this.renderBuildings = (!this.isFullLowRes() && this.tweenBuildings != 0f);
		if (this.colorAlpha.a != this.changeMod || (this.changeMod != 0f && this.changeMod != 1f))
		{
			this.colorAlpha.a = this.changeMod;
			this.mapRenderer.color = this.colorAlpha;
			this.world.unitLayer.sprRnd.color = this.colorAlpha;
			if (this.lowRes)
			{
				this.changeModTween = iTween.easeInOutCubic(0f, 1f, this.changeMod);
			}
			else
			{
				this.changeModTween = iTween.easeInBack(0f, 1f, this.changeMod);
			}
			this.tweenBuildings = Building.defaultScale.y * (1f - this.changeModTween);
		}
		if (this.lowRes && this.changeMod < 1f)
		{
			this.changeMod += Time.deltaTime * 3f;
			if (this.changeMod > 1f)
			{
				this.changeMod = 1f;
			}
		}
		else if (!this.lowRes && this.changeMod > 0f)
		{
			this.changeMod -= Time.deltaTime * 3f;
			if (this.changeMod < 0f)
			{
				this.changeMod = 0f;
			}
		}
		if (this.lowRes)
		{
			if (this.changeMod == 1f)
			{
				this.world.tilemap.enableTiles(false);
				if (this.world.transformShadows.gameObject.activeSelf)
				{
					this.world.transformShadows.gameObject.SetActive(false);
					return;
				}
			}
		}
		else if (this.changeMod == 0f)
		{
			this.world.worldLayer.setRendererEnabled(false);
		}
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0003579D File Offset: 0x0003399D
	public bool isFullLowRes()
	{
		return this.lowRes && this.changeMod == 1f;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x000357B6 File Offset: 0x000339B6
	public bool renderShadowsUnits()
	{
		return !this.lowRes && this.currentZoom < 50f;
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000357D4 File Offset: 0x000339D4
	internal void zoomUpdated(float pZoom, float pMaxZoom)
	{
		this.currentZoom = pZoom;
		bool flag = pZoom > 70f;
		if (flag == this.lowRes)
		{
			return;
		}
		this.lowRes = flag;
		if (!this.lowRes)
		{
			this.forceBuildings();
			this.forceUnits();
		}
		this.world.transformShadows.gameObject.SetActive(true);
		this.world.worldLayer.setRendererEnabled(true);
		this.world.tilemap.enableTiles(true);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00035850 File Offset: 0x00033A50
	private void forceBuildings()
	{
		this.world.buildings.checkAddRemove();
		List<Building> simpleList = this.world.buildings.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			simpleList[i].forceScale(0f);
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x000358A0 File Offset: 0x00033AA0
	private void forceUnits()
	{
		this.world.units.checkAddRemove();
		List<Actor> simpleList = this.world.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			simpleList[i].forceAnimation();
		}
	}

	// Token: 0x0400055A RID: 1370
	public SpriteRenderer mapRenderer;

	// Token: 0x0400055B RID: 1371
	public MapBox world;

	// Token: 0x0400055C RID: 1372
	internal bool lowRes = true;

	// Token: 0x0400055D RID: 1373
	internal float changeMod = 1f;

	// Token: 0x0400055E RID: 1374
	internal float changeModTween = 1f;

	// Token: 0x0400055F RID: 1375
	public float tweenBuildings;

	// Token: 0x04000560 RID: 1376
	private Color colorAlpha = new Color(1f, 1f, 1f);

	// Token: 0x04000561 RID: 1377
	internal bool renderBuildings;

	// Token: 0x04000562 RID: 1378
	private float currentZoom;
}
