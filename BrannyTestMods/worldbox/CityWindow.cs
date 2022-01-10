using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000254 RID: 596
public class CityWindow : MonoBehaviour
{
	// Token: 0x06000CF3 RID: 3315 RVA: 0x0007C33C File Offset: 0x0007A53C
	private void showStat(string pID, object pValue)
	{
		Text text = this.text_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null) + "\n";
		Text text2 = this.text_values;
		text2.text = text2.text + ((pValue != null) ? pValue.ToString() : null) + "\n";
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0007C394 File Offset: 0x0007A594
	private void showStat(string pID, object pValue, Color32? pColor)
	{
		Text text = this.text_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null) + "\n";
		Text text2 = this.text_values;
		text2.text = text2.text + Toolbox.coloredString(pValue.ToString(), pColor) + "\n";
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0007C3EA File Offset: 0x0007A5EA
	private void Awake()
	{
		if (!Application.isEditor && this.debugText != null)
		{
			Object.Destroy(this.debugText.gameObject);
		}
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0007C411 File Offset: 0x0007A611
	private void Update()
	{
		this.checkNameInput(false);
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0007C41C File Offset: 0x0007A61C
	private void checkNameInput(bool pDeactivate = false)
	{
		if (this.nameInput.inputField.isFocused)
		{
			if (Config.selectedCity == null)
			{
				return;
			}
			Config.selectedCity.data.cityName = this.nameInput.textField.text;
		}
		if (pDeactivate)
		{
			this.nameInput.inputField.DeactivateInputField();
		}
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0007C47B File Offset: 0x0007A67B
	private void OnEnable()
	{
		this.showInfo();
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0007C484 File Offset: 0x0007A684
	internal void showInfo()
	{
		if (Config.selectedCity == null)
		{
			return;
		}
		City selectedCity = Config.selectedCity;
		this.icon.sprite = (Sprite)Resources.Load("ui/Icons/" + selectedCity.race.icon, typeof(Sprite));
		this.nameInput.setText(selectedCity.data.cityName);
		this.population.text.text = selectedCity.getPopulationTotal().ToString() + "/" + selectedCity.status.homesTotal.ToString();
		this.territory.text.text = selectedCity.zones.Count.ToString() + "/" + selectedCity.status.maximumZones.ToString();
		this.army.text.text = selectedCity.getArmy().ToString() + "/" + selectedCity.getArmyMaxCity().ToString();
		this.buildings.text.text = ((selectedCity.buildings.Count <= 0) ? "0" : (selectedCity.buildings.Count.ToString() ?? ""));
		if (Application.isEditor)
		{
			string text = "";
			text = text + "\nbuildings: " + selectedCity.buildings.Count.ToString();
			text = text + "\nwood: " + selectedCity.data.storage.get("wood").ToString();
			if (this.debugText != null)
			{
				this.debugText.text = text;
			}
		}
		if (selectedCity.kingdom == null)
		{
			this.buttonKingdom.SetActive(false);
		}
		else
		{
			this.buttonKingdom.SetActive(true);
		}
		if (selectedCity.leader == null)
		{
			this.buttonLeader.SetActive(false);
		}
		else
		{
			this.buttonLeader.SetActive(true);
		}
		this.text_description.text = "";
		this.text_values.text = "";
		int num = 0;
		using (IEnumerator<Actor> enumerator = Config.selectedCity.units.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isInfectedWithAnything())
				{
					num++;
				}
			}
		}
		string text2 = "-";
		if (Config.selectedCity.leader != null)
		{
			text2 = Config.selectedCity.leader.data.firstName;
			text2 = text2 + "(" + Config.selectedCity.leader.data.age.ToString() + ")";
		}
		string pID = "village_statistics_leader";
		object pValue = text2;
		Kingdom kingdom = Config.selectedCity.kingdom;
		Color? color;
		if (kingdom == null)
		{
			color = null;
		}
		else
		{
			KingdomColor kingdomColor = kingdom.kingdomColor;
			color = ((kingdomColor != null) ? new Color?(kingdomColor.colorBorderOut) : null);
		}
		Color? color2 = color;
		this.showStat(pID, pValue, (color2 != null) ? new Color32?(color2.GetValueOrDefault()) : null);
		Text text3 = this.text_description;
		text3.text += "\n";
		Text text4 = this.text_values;
		text4.text += "\n";
		string text5 = "-";
		Culture culture = Config.selectedCity.getCulture();
		if (culture != null)
		{
			text5 = culture.name + "[" + culture.followers.ToString() + "]";
			text5 = Toolbox.coloredString(text5, new Color32?(culture.color32_text));
			this.buttonCulture.SetActive(true);
		}
		else
		{
			this.buttonCulture.SetActive(false);
		}
		this.showStat("culture", text5);
		Text text6 = this.text_description;
		text6.text += "\n";
		Text text7 = this.text_values;
		text7.text += "\n";
		string text8 = Config.selectedCity.kingdom.name + "[" + Config.selectedCity.kingdom.getPopulationTotal().ToString() + "]";
		string pID2 = "village_statistics_kingdom";
		object pValue2 = text8;
		Kingdom kingdom2 = Config.selectedCity.kingdom;
		Color? color3;
		if (kingdom2 == null)
		{
			color3 = null;
		}
		else
		{
			KingdomColor kingdomColor2 = kingdom2.kingdomColor;
			color3 = ((kingdomColor2 != null) ? new Color?(kingdomColor2.colorBorderOut) : null);
		}
		color2 = color3;
		this.showStat(pID2, pValue2, (color2 != null) ? new Color32?(color2.GetValueOrDefault()) : null);
		string text9 = "-";
		if (Config.selectedCity.kingdom.king != null)
		{
			text9 = Config.selectedCity.kingdom.king.data.firstName;
			text9 = text9 + "(" + Config.selectedCity.kingdom.king.data.age.ToString() + ")";
		}
		string pID3 = "village_statistics_king";
		object pValue3 = text9;
		Kingdom kingdom3 = Config.selectedCity.kingdom;
		Color? color4;
		if (kingdom3 == null)
		{
			color4 = null;
		}
		else
		{
			KingdomColor kingdomColor3 = kingdom3.kingdomColor;
			color4 = ((kingdomColor3 != null) ? new Color?(kingdomColor3.colorBorderOut) : null);
		}
		color2 = color4;
		this.showStat(pID3, pValue3, (color2 != null) ? new Color32?(color2.GetValueOrDefault()) : null);
		Text text10 = this.text_description;
		text10.text += "\n";
		Text text11 = this.text_values;
		text11.text += "\n";
		this.showStat("village_statistics_age", Config.selectedCity.data.age);
		Text text12 = this.text_description;
		text12.text += "\n";
		Text text13 = this.text_values;
		text13.text += "\n";
		this.showStat("village_statistics_born", Config.selectedCity.data.born);
		this.showStat("village_statistics_deaths", Config.selectedCity.data.deaths);
		this.showStat("village_statistics_infected", num);
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
		this.clearPrevButtons();
		this.loadResources();
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0007CB90 File Offset: 0x0007AD90
	private void loadResources()
	{
		this.temp_res.Clear();
		City selectedCity = Config.selectedCity;
		this.temp_res.Add(AssetManager.resources.get("gold"));
		foreach (ResourceAsset resourceAsset in AssetManager.resources.list)
		{
			if (!(resourceAsset.id == "gold"))
			{
				this.tryToAdd(resourceAsset);
			}
		}
		int num = 0;
		int count = this.temp_res.Count;
		foreach (ResourceAsset pResAsset in this.temp_res)
		{
			this.loadResource(pResAsset, num, count);
			num++;
		}
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0007CC7C File Offset: 0x0007AE7C
	private void tryToAdd(ResourceAsset pRes)
	{
		if (Config.selectedCity.data.storage.get(pRes.id) > 0)
		{
			this.temp_res.Add(pRes);
		}
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0007CCA8 File Offset: 0x0007AEA8
	private void loadResource(ResourceAsset pResAsset, int pIndex, int pTotal)
	{
		ButtonResource buttonResource = Object.Instantiate<ButtonResource>(this.prefabResource, this.parentResources);
		int pAmount = Config.selectedCity.data.storage.get(pResAsset.id);
		buttonResource.load(pResAsset, pAmount);
		RectTransform component = buttonResource.GetComponent<RectTransform>();
		float num = 15f;
		float num2 = 22.4f;
		float num3 = 192f - num * 1f;
		float num4 = num2 * 1f;
		if ((float)pTotal * num4 >= num3)
		{
			num4 = num3 / (float)pTotal;
		}
		float x = num + num4 * (float)pIndex;
		float y = 0f;
		component.anchoredPosition = new Vector2(x, y);
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0007CD38 File Offset: 0x0007AF38
	private void clearPrevButtons()
	{
		for (int i = 0; i < this.parentResources.childCount; i++)
		{
			Transform child = this.parentResources.GetChild(i);
			if (!(child.name == "Title"))
			{
				Object.Destroy(child.gameObject);
			}
		}
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0007CD85 File Offset: 0x0007AF85
	private void OnDisable()
	{
		if (Config.selectedCity == null)
		{
			return;
		}
		this.checkNameInput(false);
		this.nameInput.inputField.DeactivateInputField();
		Config.selectedCity = null;
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0007CDB2 File Offset: 0x0007AFB2
	public void clickKingdom()
	{
		Config.selectedKingdom = Config.selectedCity.kingdom;
		ScrollWindow.moveAllToLeftAndRemove(true);
		ScrollWindow.showWindow("kingdom");
		ScrollWindow.get("kingdom").GetComponent<KingdomWindow>().showInfo();
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0007CDE7 File Offset: 0x0007AFE7
	public void clickLeader()
	{
		Config.selectedUnit = Config.selectedCity.leader;
		ScrollWindow.moveAllToLeftAndRemove(true);
		ScrollWindow.showWindow("inspect_unit");
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0007CE08 File Offset: 0x0007B008
	public void clickCulture()
	{
		string culture = Config.selectedCity.data.culture;
		if (string.IsNullOrEmpty(culture))
		{
			return;
		}
		Config.selectedCulture = MapBox.instance.cultures.get(culture);
		ScrollWindow.showWindow("culture");
	}

	// Token: 0x04000FCB RID: 4043
	public ButtonResource prefabResource;

	// Token: 0x04000FCC RID: 4044
	public Transform parentResources;

	// Token: 0x04000FCD RID: 4045
	public CityIcon army;

	// Token: 0x04000FCE RID: 4046
	public CityIcon population;

	// Token: 0x04000FCF RID: 4047
	public CityIcon death;

	// Token: 0x04000FD0 RID: 4048
	public CityIcon territory;

	// Token: 0x04000FD1 RID: 4049
	public CityIcon buildings;

	// Token: 0x04000FD2 RID: 4050
	public NameInput nameInput;

	// Token: 0x04000FD3 RID: 4051
	public Image icon;

	// Token: 0x04000FD4 RID: 4052
	public Text debugText;

	// Token: 0x04000FD5 RID: 4053
	public GameObject buttonKingdom;

	// Token: 0x04000FD6 RID: 4054
	public GameObject buttonLeader;

	// Token: 0x04000FD7 RID: 4055
	public GameObject buttonCulture;

	// Token: 0x04000FD8 RID: 4056
	public Text text_description;

	// Token: 0x04000FD9 RID: 4057
	public Text text_values;

	// Token: 0x04000FDA RID: 4058
	private List<ResourceAsset> temp_res = new List<ResourceAsset>();
}
