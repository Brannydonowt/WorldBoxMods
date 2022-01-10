using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class KingdomListWindow : MonoBehaviour
{
	// Token: 0x06000E74 RID: 3700 RVA: 0x000864BC File Offset: 0x000846BC
	private void OnEnable()
	{
		this.showList();
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x000864C4 File Offset: 0x000846C4
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
		this.world.kingdoms.list_civs.Sort(new Comparison<Kingdom>(this.kingdomSorter));
		foreach (Kingdom pKingdom in this.world.kingdoms.list_civs)
		{
			this.showElement(pKingdom);
		}
		this.transformContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)(this.elements.Count * 44 + 30));
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x000865CC File Offset: 0x000847CC
	private void showElement(Kingdom pKingdom)
	{
		KingdomElement kingdomElement = Object.Instantiate<KingdomElement>(this.kingdomElementPrefab, this.transformContent);
		kingdomElement.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (float)(-(float)(this.elements.Count * 44 + 30)));
		kingdomElement.kingdom = pKingdom;
		kingdomElement.kingdomName.text = pKingdom.name;
		Color colorBorderOut = pKingdom.kingdomColor.colorBorderOut;
		kingdomElement.kingdomName.color = colorBorderOut;
		this.elements.Add(kingdomElement);
		kingdomElement.iconRace.sprite = (Sprite)Resources.Load("ui/Icons/" + pKingdom.race.icon, typeof(Sprite));
		int num = 0;
		int num2 = 0;
		int count = pKingdom.cities.Count;
		foreach (City city in pKingdom.cities)
		{
			num += city.zones.Count;
			num2 += city.buildings.Count;
		}
		kingdomElement.textPopulation.text = (pKingdom.getPopulationTotal().ToString() ?? "");
		kingdomElement.textArmy.text = (pKingdom.countArmy().ToString() ?? "");
		kingdomElement.textZones.text = (num.ToString() ?? "");
		kingdomElement.textHouses.text = (num2.ToString() ?? "");
		kingdomElement.textCities.text = (count.ToString() ?? "");
		kingdomElement.buttonKing.gameObject.SetActive(pKingdom.king != null);
		kingdomElement.buttonCapital.gameObject.SetActive(pKingdom.capital != null);
		kingdomElement.banner.load(pKingdom);
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x000867D4 File Offset: 0x000849D4
	public int kingdomSorter(Kingdom k1, Kingdom k2)
	{
		return k2.getPopulationTotal().CompareTo(k1.getPopulationTotal());
	}

	// Token: 0x04001148 RID: 4424
	public KingdomElement kingdomElementPrefab;

	// Token: 0x04001149 RID: 4425
	private List<KingdomElement> elements = new List<KingdomElement>();

	// Token: 0x0400114A RID: 4426
	private MapBox world;

	// Token: 0x0400114B RID: 4427
	public Transform transformContent;
}
