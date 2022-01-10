using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000006 RID: 6
public class MapNamesManager : MonoBehaviour
{
	// Token: 0x06000009 RID: 9 RVA: 0x000032E4 File Offset: 0x000014E4
	private void Awake()
	{
		MapNamesManager.instance = this;
		this.canvas = base.GetComponent<Canvas>();
		this.canvasRect = this.canvas.GetComponent<RectTransform>();
		this.canvasScaler = base.GetComponent<CanvasScaler>();
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00003315 File Offset: 0x00001515
	public void create()
	{
		this.world = MapBox.instance;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00003324 File Offset: 0x00001524
	internal void update()
	{
		this.clear();
		MapMode currentMode = this.getCurrentMode();
		this.setMode(currentMode);
		switch (currentMode)
		{
		case MapMode.None:
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			break;
		case MapMode.Kingdoms:
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.showNameKingdoms();
			break;
		case MapMode.Cultures:
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.showNameCultures();
			break;
		case MapMode.Villages:
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.showNameCities();
			break;
		}
		if (currentMode != MapMode.None)
		{
			this.updatePositions();
		}
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].update(this.world.deltaTime);
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00003414 File Offset: 0x00001614
	private MapMode getCurrentMode()
	{
		MapMode mapMode = MapMode.None;
		if (this.world.showMapNames())
		{
			mapMode = this.world.getForcedMapMode(MapMode.None);
			if (mapMode == MapMode.None)
			{
				if (this.world.showCultureZones())
				{
					mapMode = MapMode.Cultures;
				}
				else if (this.world.showKingdomZones())
				{
					mapMode = MapMode.Kingdoms;
				}
				else
				{
					mapMode = MapMode.Villages;
				}
			}
		}
		return mapMode;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00003468 File Offset: 0x00001668
	private void setMode(MapMode pMode)
	{
		if (this._lastMode == pMode)
		{
			return;
		}
		this._lastMode = pMode;
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].tweenScale = 0f;
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000034B4 File Offset: 0x000016B4
	private void showNameCultures()
	{
		for (int i = 0; i < this.world.cultures.list.Count; i++)
		{
			Culture culture = this.world.cultures.list[i];
			if (!(culture.titleCenter == Globals.POINT_IN_VOID))
			{
				this.getTextFor().showTextCulture(culture);
			}
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00003518 File Offset: 0x00001718
	private void showNameKingdoms()
	{
		for (int i = 0; i < this.world.kingdoms.list.Count; i++)
		{
			Kingdom kingdom = this.world.kingdoms.list[i];
			if (!(kingdom.capital == null))
			{
				MapText textFor = this.getTextFor();
				textFor.Kingdom = kingdom;
				textFor.showTextKingdom(kingdom);
			}
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00003580 File Offset: 0x00001780
	private void showNameCities()
	{
		for (int i = 0; i < this.world.citiesList.Count; i++)
		{
			City city = this.world.citiesList[i];
			MapText textFor = this.getTextFor();
			textFor.city = city;
			textFor.showTextCity();
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000035CC File Offset: 0x000017CC
	internal void updatePositions()
	{
		if (this.cur_active == 0)
		{
			return;
		}
		for (int i = 0; i < this.list.Count; i++)
		{
			MapText mapText = this.list[i];
			if (mapText.showing)
			{
				for (int j = 0; j < this.list.Count; j++)
				{
					MapText mapText2 = this.list[j];
					if (!(mapText == mapText2) && mapText.showing && mapText2.showing && mapText.Overlaps(mapText2))
					{
						if (mapText.priority_capital && !mapText2.priority_capital)
						{
							mapText2.setShowing(false);
						}
						else if (!mapText.priority_capital && mapText2.priority_capital)
						{
							mapText.setShowing(false);
						}
						else if (mapText.priority_population > mapText2.priority_population)
						{
							mapText2.setShowing(false);
						}
						else
						{
							mapText.setShowing(false);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000036B4 File Offset: 0x000018B4
	private MapText getTextFor()
	{
		MapText mapText;
		if (this.cur_active == this.list.Count)
		{
			mapText = Object.Instantiate<MapText>(this.prefab, base.transform);
			mapText.clear();
			mapText.manager = this;
			mapText.name = "map text " + this.list.Count.ToString();
			this.list.Add(mapText);
		}
		else
		{
			mapText = this.list[this.cur_active];
			this.cur_active++;
		}
		mapText.setShowing(true);
		return mapText;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00003750 File Offset: 0x00001950
	internal void clear()
	{
		this.cur_active = 0;
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].clear();
		}
	}

	// Token: 0x0400000B RID: 11
	public static MapNamesManager instance;

	// Token: 0x0400000C RID: 12
	private List<MapText> list = new List<MapText>();

	// Token: 0x0400000D RID: 13
	private int cur_active;

	// Token: 0x0400000E RID: 14
	public MapText prefab;

	// Token: 0x0400000F RID: 15
	private Canvas canvas;

	// Token: 0x04000010 RID: 16
	internal CanvasScaler canvasScaler;

	// Token: 0x04000011 RID: 17
	internal RectTransform canvasRect;

	// Token: 0x04000012 RID: 18
	internal MapBox world;

	// Token: 0x04000013 RID: 19
	private MapMode _lastMode;
}
