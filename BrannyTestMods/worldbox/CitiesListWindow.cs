using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class CitiesListWindow : MonoBehaviour
{
	// Token: 0x06000E0D RID: 3597 RVA: 0x000840BB File Offset: 0x000822BB
	private void OnEnable()
	{
		this.showList();
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x000840C4 File Offset: 0x000822C4
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
		this.world.citiesList.Sort(new Comparison<City>(this.sorter));
		foreach (City pObject in this.world.citiesList)
		{
			this.showElement(pObject);
		}
		this.transformContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)(this.elements.Count * 44 + 30));
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x000841C4 File Offset: 0x000823C4
	private void showElement(City pObject)
	{
		CitiesListElement citiesListElement = Object.Instantiate<CitiesListElement>(this.elementPrefab, this.transformContent);
		citiesListElement.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (float)(-(float)(this.elements.Count * 44 + 30)));
		this.elements.Add(citiesListElement);
		citiesListElement.city = pObject;
		citiesListElement.banner.load(pObject.kingdom);
		citiesListElement.text_name.text = pObject.data.cityName;
		citiesListElement.text_name.color = pObject.kingdom.kingdomColor.colorBorderOut;
		citiesListElement.population.text = (pObject.getPopulationTotal().ToString() ?? "");
		citiesListElement.army.text = (pObject.getArmy().ToString() ?? "");
		citiesListElement.zones.text = (pObject.zones.Count.ToString() ?? "");
		citiesListElement.age.text = (pObject.data.age.ToString() ?? "");
		citiesListElement.raceIcon.sprite = (Sprite)Resources.Load("ui/Icons/" + pObject.race.icon, typeof(Sprite));
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00084328 File Offset: 0x00082528
	public int sorter(City k1, City k2)
	{
		return k2.getPopulationTotal().CompareTo(k1.getPopulationTotal());
	}

	// Token: 0x040010E1 RID: 4321
	public CitiesListElement elementPrefab;

	// Token: 0x040010E2 RID: 4322
	private List<CitiesListElement> elements = new List<CitiesListElement>();

	// Token: 0x040010E3 RID: 4323
	private MapBox world;

	// Token: 0x040010E4 RID: 4324
	public Transform transformContent;
}
