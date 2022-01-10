using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class CulturesListWindow : MonoBehaviour
{
	// Token: 0x06000E21 RID: 3617 RVA: 0x00084B09 File Offset: 0x00082D09
	private void OnEnable()
	{
		this.showList();
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x00084B14 File Offset: 0x00082D14
	internal void showList()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.world = MapBox.instance;
		while (this.elements.Count > 0)
		{
			Component component = this.elements[this.elements.Count - 1];
			this.elements.RemoveAt(this.elements.Count - 1);
			Object.Destroy(component.gameObject);
		}
		this.world.cultures.list.Sort(new Comparison<Culture>(this.sorter));
		foreach (Culture pObject in this.world.cultures.list)
		{
			this.showElement(pObject);
		}
		this.transformContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)(this.elements.Count * 44 + 30));
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x00084C1C File Offset: 0x00082E1C
	private void showElement(Culture pObject)
	{
		CultureListElement cultureListElement = Object.Instantiate<CultureListElement>(this.elementPrefab, this.transformContent);
		cultureListElement.GetComponent<RectTransform>().anchoredPosition = new Vector3(-6f, (float)(-(float)(this.elements.Count * 44 + 30)));
		this.elements.Add(cultureListElement);
		cultureListElement.culture = pObject;
		Race race = AssetManager.raceLibrary.get(pObject.race);
		cultureListElement.iconRace.sprite = (Sprite)Resources.Load("ui/Icons/" + race.icon, typeof(Sprite));
		cultureListElement.name.text = pObject.name;
		cultureListElement.name.color = pObject.color32_text;
		cultureListElement.cultureElement.sprite = (Sprite)Resources.Load(cultureListElement.culture.icon_element, typeof(Sprite));
		cultureListElement.cultureDecor.sprite = (Sprite)Resources.Load(cultureListElement.culture.icon_decor, typeof(Sprite));
		cultureListElement.cultureElement.color = Toolbox.makeColor(cultureListElement.culture.color);
		cultureListElement.cultureDecor.color = Toolbox.makeColor(cultureListElement.culture.color);
		cultureListElement.textFollowers.text = (pObject.followers.ToString() ?? "");
		cultureListElement.textLevel.text = (pObject.getCurrentLevel().ToString() ?? "");
		cultureListElement.textCities.text = (pObject.cities.ToString() ?? "");
		cultureListElement.textZones.text = (pObject.zones.Count.ToString() ?? "");
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x00084DF3 File Offset: 0x00082FF3
	public int sorter(Culture k1, Culture k2)
	{
		return k2.followers.CompareTo(k1.followers);
	}

	// Token: 0x04001101 RID: 4353
	public CultureListElement elementPrefab;

	// Token: 0x04001102 RID: 4354
	private List<CultureListElement> elements = new List<CultureListElement>();

	// Token: 0x04001103 RID: 4355
	private MapBox world;

	// Token: 0x04001104 RID: 4356
	public Transform transformContent;
}
