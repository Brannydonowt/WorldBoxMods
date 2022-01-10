using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000291 RID: 657
public class KingdomWindow : MonoBehaviour
{
	// Token: 0x06000E79 RID: 3705 RVA: 0x00086808 File Offset: 0x00084A08
	private void Update()
	{
		this.checkNameInput(false);
		float y = this.transformContent.GetComponent<RectTransform>().anchoredPosition.y;
		foreach (CityInfoElement cityInfoElement in this.elements)
		{
			float num = -cityInfoElement.rect.anchoredPosition.y;
			if (y - num >= -60f && y - num < 300f)
			{
				cityInfoElement.gameObject.SetActive(true);
			}
			else
			{
				cityInfoElement.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x000868B8 File Offset: 0x00084AB8
	private void checkNameInput(bool pDeactivate = false)
	{
		if (this.nameInput.inputField.isFocused)
		{
			if (Config.selectedKingdom == null)
			{
				return;
			}
			Config.selectedKingdom.name = this.nameInput.textField.text;
		}
		if (this.mottoInput.inputField.isFocused)
		{
			if (Config.selectedKingdom == null)
			{
				return;
			}
			Config.selectedKingdom.motto = this.mottoInput.textField.text;
		}
		if (pDeactivate)
		{
			this.nameInput.inputField.DeactivateInputField();
			this.mottoInput.inputField.DeactivateInputField();
		}
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x00086950 File Offset: 0x00084B50
	public void showInfo()
	{
		if (Config.selectedKingdom == null)
		{
			return;
		}
		this.kingdomBanner.load(Config.selectedKingdom);
		this.transformContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		this.curKingdom = Config.selectedKingdom;
		if (string.IsNullOrEmpty(this.curKingdom.motto))
		{
			this.curKingdom.generateMotto();
		}
		this.nameInput.setText(this.curKingdom.name);
		this.mottoInput.setText(this.curKingdom.motto);
		Graphic textField = this.mottoInput.textField;
		Kingdom selectedKingdom = Config.selectedKingdom;
		Color? color;
		if (selectedKingdom == null)
		{
			color = null;
		}
		else
		{
			KingdomColor kingdomColor = selectedKingdom.kingdomColor;
			color = ((kingdomColor != null) ? new Color?(kingdomColor.colorBorderOut) : null);
		}
		Color? color2 = color;
		textField.color = color2.Value;
		int num = 0;
		int num2 = 0;
		foreach (City city in this.curKingdom.cities)
		{
			num2 += city.buildings.Count;
			num += city.zones.Count;
		}
		this.population.text.text = (this.curKingdom.getPopulationTotal().ToString() ?? "");
		this.buildings.text.text = (num2.ToString() ?? "");
		this.territory.text.text = (num.ToString() ?? "");
		this.army.text.text = this.curKingdom.countArmy().ToString() + "/" + this.curKingdom.countArmyMax().ToString();
		int maxCities = Config.selectedKingdom.getMaxCities();
		string text = this.curKingdom.cities.Count.ToString() + "/" + maxCities.ToString();
		if (this.curKingdom.cities.Count > maxCities)
		{
			text = Toolbox.coloredText(text, Toolbox.color_negative, false);
		}
		this.cities.text.text = text;
		if (this.curKingdom.king == null)
		{
			this.buttonKing.SetActive(false);
		}
		else
		{
			this.buttonKing.SetActive(true);
		}
		if (this.curKingdom.capital == null)
		{
			this.buttonCapital.SetActive(false);
		}
		else
		{
			this.buttonCapital.SetActive(true);
		}
		while (this.elements.Count > 0)
		{
			Component component = this.elements[this.elements.Count - 1];
			this.elements.RemoveAt(this.elements.Count - 1);
			Object.Destroy(component.gameObject);
		}
		this.curKingdom.cities.Sort(new Comparison<City>(this.citiesSorter));
		foreach (City pCity in this.curKingdom.cities)
		{
			this.showElement(pCity);
		}
		this.transformContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)(230 + this.elements.Count * 25));
		int num3 = 0;
		int num4 = 0;
		foreach (Actor actor in Config.selectedKingdom.units)
		{
			if (actor.isInfectedWithAnything())
			{
				num3++;
			}
			if (actor.data.hunger <= 10)
			{
				num4++;
			}
		}
		this.text_description.text = "";
		this.text_values.text = "";
		this.showStat("kingdom_statistics_age", Config.selectedKingdom.age);
		this.showStat("kingdom_statistics_born", Config.selectedKingdom.born);
		this.showStat("kingdom_statistics_deaths", Config.selectedKingdom.deaths);
		if (num3 > 0)
		{
			this.showStat("kingdom_statistics_infected", num3);
		}
		if (num4 > 0)
		{
			this.showStat("statistics_starving_people", num4);
		}
		Text text2 = this.text_description;
		text2.text += "\n";
		Text text3 = this.text_values;
		text3.text += "\n";
		string text4 = "-";
		if (Config.selectedKingdom.king != null)
		{
			text4 = Config.selectedKingdom.king.data.firstName;
			text4 = text4 + "(" + Config.selectedKingdom.king.data.age.ToString() + ")";
		}
		string pID = "kingdom_statistics_king";
		object pValue = text4;
		Kingdom selectedKingdom2 = Config.selectedKingdom;
		Color? color3;
		if (selectedKingdom2 == null)
		{
			color3 = null;
		}
		else
		{
			KingdomColor kingdomColor2 = selectedKingdom2.kingdomColor;
			color3 = ((kingdomColor2 != null) ? new Color?(kingdomColor2.colorBorderOut) : null);
		}
		color2 = color3;
		this.showStat(pID, pValue, (color2 != null) ? new Color32?(color2.GetValueOrDefault()) : null);
		if (Config.selectedKingdom.king != null)
		{
			this.showStat("creature_statistics_personality", LocalizedTextManager.getText("personality_" + Config.selectedKingdom.king.s_personality.id, null));
			this.showStat("kingdom_statistics_king_ruled", Config.selectedKingdom.lastKingRuledFor);
		}
		string text5 = "-";
		if (Config.selectedKingdom.capital != null)
		{
			text5 = Config.selectedKingdom.capital.data.cityName;
			text5 = text5 + "[" + Config.selectedKingdom.capital.getPopulationTotal().ToString() + "]";
		}
		string pID2 = "kingdom_statistics_capital";
		object pValue2 = text5;
		Kingdom selectedKingdom3 = Config.selectedKingdom;
		Color? color4;
		if (selectedKingdom3 == null)
		{
			color4 = null;
		}
		else
		{
			KingdomColor kingdomColor3 = selectedKingdom3.kingdomColor;
			color4 = ((kingdomColor3 != null) ? new Color?(kingdomColor3.colorBorderOut) : null);
		}
		color2 = color4;
		this.showStat(pID2, pValue2, (color2 != null) ? new Color32?(color2.GetValueOrDefault()) : null);
		string text6 = "-";
		Culture culture = Config.selectedKingdom.getCulture();
		if (culture != null)
		{
			text6 = culture.name + "[" + culture.followers.ToString() + "]";
			text6 = Toolbox.coloredString(text6, new Color32?(culture.color32_text));
			this.buttonCulture.SetActive(true);
		}
		else
		{
			this.buttonCulture.SetActive(false);
		}
		this.showStat("culture", text6);
		this.text_description.GetComponent<LocalizedText>().checkTextFont();
		this.text_values.GetComponent<LocalizedText>().checkTextFont();
		this.text_description.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.text_values.GetComponent<LocalizedText>().checkSpecialLanguages();
		if (LocalizedTextManager.isRTLLang())
		{
			this.text_description.alignment = 2;
			this.text_values.alignment = 0;
		}
		else
		{
			this.text_description.alignment = 0;
			this.text_values.alignment = 2;
		}
		this.clearOldBanners();
		this.loadWarBanners();
		this.loadAlliedBanners();
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x00087108 File Offset: 0x00085308
	private void showStat(string pID, object pValue)
	{
		Text text = this.text_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null) + "\n";
		Text text2 = this.text_values;
		text2.text = text2.text + ((pValue != null) ? pValue.ToString() : null) + "\n";
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x00087160 File Offset: 0x00085360
	private void showStat(string pID, object pValue, Color32? pColor)
	{
		Text text = this.text_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null) + "\n";
		Text text2 = this.text_values;
		text2.text = text2.text + Toolbox.coloredString(pValue.ToString(), pColor) + "\n";
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x000871B8 File Offset: 0x000853B8
	private void loadWarBanners()
	{
		int count = this.curKingdom.civs_enemies.Count;
		if (count == 0)
		{
			return;
		}
		int num = 0;
		foreach (Kingdom pKingdom in this.curKingdom.civs_enemies.Keys)
		{
			this.loadKingdomBannerFor(pKingdom, num, (float)count, this.containerBannersWar);
			num++;
		}
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0008723C File Offset: 0x0008543C
	private void loadAlliedBanners()
	{
		int count = this.curKingdom.civs_allies.Count;
		if (count == 0)
		{
			return;
		}
		int num = 0;
		foreach (Kingdom kingdom in this.curKingdom.civs_allies.Keys)
		{
			if (kingdom != this.curKingdom)
			{
				this.loadKingdomBannerFor(kingdom, num, (float)count, this.containerBannersAllies);
				num++;
			}
		}
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x000872C8 File Offset: 0x000854C8
	private void loadKingdomBannerFor(Kingdom pKingdom, int pIndex, float pTotal, Transform pContainer)
	{
		BannerLoader bannerLoader = Object.Instantiate<BannerLoader>(this.prefabBanner, pContainer);
		bannerLoader.load(pKingdom);
		RectTransform component = bannerLoader.GetComponent<RectTransform>();
		float num = 10f;
		float num2 = 27f;
		float num3 = 136f - num * 1.8f;
		float num4 = num2 * 0.7f;
		if (pTotal * num4 >= num3)
		{
			num4 = num3 / pTotal;
		}
		float x = num + num4 * (float)pIndex;
		float y = -11f;
		component.anchoredPosition = new Vector2(x, y);
		bannerLoader.gameObject.AddComponent<TipButton>();
		bannerLoader.gameObject.GetComponent<TipButton>().action = new TooltipAction(bannerLoader.showBannerTooltip);
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x00087360 File Offset: 0x00085560
	private void clearOldBanners()
	{
		for (int i = 0; i < this.containerBannersWar.childCount; i++)
		{
			Object.Destroy(this.containerBannersWar.GetChild(i).gameObject);
		}
		for (int j = 0; j < this.containerBannersAllies.childCount; j++)
		{
			Object.Destroy(this.containerBannersAllies.GetChild(j).gameObject);
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x000873C8 File Offset: 0x000855C8
	private void showElement(City pCity)
	{
		CityInfoElement cityInfoElement = Object.Instantiate<CityInfoElement>(this.prefabCityInfo, this.cityInfoPlacement);
		cityInfoElement.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (float)(-(float)(this.elements.Count * 25)));
		cityInfoElement.show(pCity);
		this.elements.Add(cityInfoElement);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x00087424 File Offset: 0x00085624
	public int citiesSorter(City o1, City o2)
	{
		if (o2.isCapitalCity())
		{
			return 1;
		}
		if (o1.isCapitalCity())
		{
			return -1;
		}
		return o2.getPopulationTotal().CompareTo(o1.getPopulationTotal());
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x00087459 File Offset: 0x00085659
	private void OnDisable()
	{
		if (this.curKingdom == null)
		{
			return;
		}
		this.checkNameInput(true);
		if (ScrollWindow.windowLoaded("list_kingdoms"))
		{
			ScrollWindow.get("list_kingdoms").GetComponent<KingdomListWindow>().showList();
		}
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0008748B File Offset: 0x0008568B
	public void clickKing()
	{
		Config.selectedKingdom.name = this.nameInput.textField.text;
		Config.selectedUnit = Config.selectedKingdom.king;
		ScrollWindow.moveAllToLeftAndRemove(true);
		ScrollWindow.showWindow("inspect_unit");
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x000874C8 File Offset: 0x000856C8
	public void clickCapital()
	{
		Config.selectedKingdom.name = this.nameInput.textField.text;
		Config.selectedCity = Config.selectedKingdom.capital;
		ScrollWindow.moveAllToLeftAndRemove(true);
		ScrollWindow.get("village").clickShow();
		ScrollWindow.get("village").GetComponent<CityWindow>().showInfo();
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00087528 File Offset: 0x00085728
	public void clickCulture()
	{
		string cultureID = Config.selectedKingdom.cultureID;
		if (string.IsNullOrEmpty(cultureID))
		{
			return;
		}
		Config.selectedCulture = MapBox.instance.cultures.get(cultureID);
		ScrollWindow.showWindow("culture");
	}

	// Token: 0x0400114C RID: 4428
	public CityIcon population;

	// Token: 0x0400114D RID: 4429
	public CityIcon buildings;

	// Token: 0x0400114E RID: 4430
	public CityIcon cities;

	// Token: 0x0400114F RID: 4431
	public CityIcon territory;

	// Token: 0x04001150 RID: 4432
	public CityIcon army;

	// Token: 0x04001151 RID: 4433
	public NameInput nameInput;

	// Token: 0x04001152 RID: 4434
	public NameInput mottoInput;

	// Token: 0x04001153 RID: 4435
	private Kingdom curKingdom;

	// Token: 0x04001154 RID: 4436
	public GameObject buttonCapital;

	// Token: 0x04001155 RID: 4437
	public GameObject buttonKing;

	// Token: 0x04001156 RID: 4438
	public GameObject buttonCulture;

	// Token: 0x04001157 RID: 4439
	public BannerLoader prefabBanner;

	// Token: 0x04001158 RID: 4440
	public CityInfoElement prefabCityInfo;

	// Token: 0x04001159 RID: 4441
	public Transform cityInfoPlacement;

	// Token: 0x0400115A RID: 4442
	private List<CityInfoElement> elements = new List<CityInfoElement>();

	// Token: 0x0400115B RID: 4443
	public GameObject transformContent;

	// Token: 0x0400115C RID: 4444
	public Transform containerBannersWar;

	// Token: 0x0400115D RID: 4445
	public Transform containerBannersAllies;

	// Token: 0x0400115E RID: 4446
	public BannerLoader kingdomBanner;

	// Token: 0x0400115F RID: 4447
	public Transform transformCityInfo;

	// Token: 0x04001160 RID: 4448
	public Text text_description;

	// Token: 0x04001161 RID: 4449
	public Text text_values;
}
