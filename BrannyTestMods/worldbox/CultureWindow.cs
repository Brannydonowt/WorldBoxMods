using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000280 RID: 640
public class CultureWindow : MonoBehaviour
{
	// Token: 0x06000E14 RID: 3604 RVA: 0x0008437C File Offset: 0x0008257C
	public void showInfo()
	{
		if (Config.selectedCulture == null)
		{
			return;
		}
		MapBox.instance.cultures.recalcCultureValues();
		this.cultureElement.sprite = (Sprite)Resources.Load(Config.selectedCulture.icon_element, typeof(Sprite));
		this.cultureDecor.sprite = (Sprite)Resources.Load(Config.selectedCulture.icon_decor, typeof(Sprite));
		this.cultureElement.color = Toolbox.makeColor(Config.selectedCulture.color);
		this.cultureDecor.color = Toolbox.makeColor(Config.selectedCulture.color);
		if (this.tech_prefab == null)
		{
			this.tech_prefab = Resources.Load<TechElement>("ui/PrefabTechElement");
		}
		this.nameInput.setText(Config.selectedCulture.name);
		this.text_description.text = string.Empty;
		this.text_values.text = string.Empty;
		this.population.text.text = (Config.selectedCulture.followers.ToString() ?? "");
		this.cities.text.text = (Config.selectedCulture.cities.ToString() ?? "");
		this.kingdoms.text.text = (Config.selectedCulture.kingdoms.ToString() ?? "");
		this.knowledge_gain.text.text = (Config.selectedCulture.knowledge_gain.ToString("0.0") ?? "");
		this.level.text.text = (Config.selectedCulture.getCurrentLevel().ToString() ?? "");
		this.zones.text.text = (Config.selectedCulture.zones.Count.ToString() ?? "");
		this.spreadSpeed.text.text = (Config.selectedCulture.stats.culture_spread_speed.value.ToString("0.0") ?? "");
		int num = (int)(Config.selectedCulture.stats.culture_spread_convert_chance.value * 100f);
		this.convertChance.text.text = num.ToString() + "%";
		int num2 = MapBox.instance.mapStats.year - Config.selectedCulture.year + 1;
		if (!string.IsNullOrEmpty(Config.selectedCulture.village_origin))
		{
			this.showStat("culture_founded_in", Config.selectedCulture.village_origin);
		}
		this.showStat("age", num2);
		float pVal = Config.selectedCulture.research_progress;
		float pMax;
		if (!string.IsNullOrEmpty(Config.selectedCulture.researching_tech))
		{
			pMax = Config.selectedCulture.getKnowledgeCostForResearch();
			CultureTechAsset cultureTechAsset = AssetManager.culture_tech.get(Config.selectedCulture.researching_tech);
			Sprite sprite = (Sprite)Resources.Load("ui/Icons/" + cultureTechAsset.icon, typeof(Sprite));
			this.iconCurrentTech.GetComponent<Image>().sprite = sprite;
			this.iconCurrentTech.gameObject.SetActive(true);
		}
		else
		{
			pVal = 0f;
			pMax = 0f;
			this.iconCurrentTech.gameObject.SetActive(false);
		}
		this.researchBar.setBar(pVal, pMax, pVal.ToString("0.0") + "/" + pMax.ToString("0"));
		this.clearPrevTechElements();
		this.loadTechButtons();
		Race race = AssetManager.raceLibrary.get(Config.selectedCulture.race);
		this.text_rare_knowledges.text = Config.selectedCulture.countCurrentRareTech().ToString() + "/" + race.culture_rate_tech_limit.ToString();
		this.text_description.GetComponent<LocalizedText>().checkTextFont();
		this.text_values.GetComponent<LocalizedText>().checkTextFont();
		this.text_description.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.text_values.GetComponent<LocalizedText>().checkSpecialLanguages();
		if (LocalizedTextManager.isRTLLang())
		{
			this.text_description.alignment = 2;
			this.text_values.alignment = 0;
			return;
		}
		this.text_description.alignment = 0;
		this.text_values.alignment = 2;
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x000847EC File Offset: 0x000829EC
	private void testIcons(Culture pCulture)
	{
		pCulture.list_tech_ids.Clear();
		int num = Toolbox.randomInt(5, 200);
		for (int i = 0; i < num; i++)
		{
			pCulture.list_tech_ids.Add(AssetManager.culture_tech.list.GetRandom<CultureTechAsset>().id);
		}
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0008483C File Offset: 0x00082A3C
	private void loadTechButtons()
	{
		Culture selectedCulture = Config.selectedCulture;
		List<CultureTechAsset> list = new List<CultureTechAsset>();
		List<CultureTechAsset> list2 = new List<CultureTechAsset>();
		selectedCulture.list_tech_ids.Sort((string x, string y) => string.Compare(x, y));
		for (int i = 0; i < selectedCulture.list_tech_ids.Count; i++)
		{
			CultureTechAsset cultureTechAsset = AssetManager.culture_tech.get(selectedCulture.list_tech_ids[i]);
			if (cultureTechAsset != null)
			{
				TechType type = cultureTechAsset.type;
				if (type != TechType.Common)
				{
					if (type == TechType.Rare)
					{
						list2.Add(cultureTechAsset);
					}
				}
				else
				{
					list.Add(cultureTechAsset);
				}
			}
		}
		this.loadButtons(list, this.tech_common);
		this.loadButtons(list2, this.tech_rare);
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x000848F8 File Offset: 0x00082AF8
	private void loadButtons(List<CultureTechAsset> pList, Transform pTransform)
	{
		GridLayoutGroup component = pTransform.GetComponent<GridLayoutGroup>();
		int num = 26;
		float num2 = -10f;
		float width = pTransform.GetComponent<RectTransform>().rect.width;
		float num3 = (pTransform.GetComponent<RectTransform>().rect.height - (float)num * 0.7f) * (width - (float)num * 0.7f);
		float num4 = num2;
		do
		{
			num4 -= 1f;
		}
		while ((float)pList.Count * ((float)num + num4) * ((float)num + num4) >= num3);
		int count = pList.Count;
		component.spacing = new Vector2(num4, num4);
		for (int i = 0; i < pList.Count; i++)
		{
			this.loadTechButton(pList[i], (float)i, pList.Count, pTransform);
		}
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x000849B7 File Offset: 0x00082BB7
	private void loadTechButton(CultureTechAsset pTech, float pIndex, int pTotal, Transform pTransform)
	{
		Object.Instantiate<TechElement>(this.tech_prefab, pTransform).load(pTech);
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x000849CC File Offset: 0x00082BCC
	private void clearPrevTechElements()
	{
		this.clearOnTransform(this.tech_common);
		this.clearOnTransform(this.tech_rare);
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x000849E8 File Offset: 0x00082BE8
	private void clearOnTransform(Transform pTransform)
	{
		for (int i = 0; i < pTransform.childCount; i++)
		{
			Transform child = pTransform.GetChild(i);
			if (!(child.name == "Title"))
			{
				Object.Destroy(child.gameObject);
			}
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x00084A2C File Offset: 0x00082C2C
	private void checkNameInput(bool pDeactivate = false)
	{
		if (this.nameInput.inputField.isFocused)
		{
			if (Config.selectedCulture == null)
			{
				return;
			}
			Config.selectedCulture.name = this.nameInput.textField.text;
		}
		if (pDeactivate)
		{
			this.nameInput.inputField.DeactivateInputField();
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x00084A80 File Offset: 0x00082C80
	private void Update()
	{
		this.checkNameInput(false);
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x00084A89 File Offset: 0x00082C89
	private void OnEnable()
	{
		this.showInfo();
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00084A94 File Offset: 0x00082C94
	private void showStat(string pID, object pValue)
	{
		Text text = this.text_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null) + "\n";
		Text text2 = this.text_values;
		text2.text = text2.text + ((pValue != null) ? pValue.ToString() : null) + "\n";
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00084AEA File Offset: 0x00082CEA
	private void OnDisable()
	{
		if (Config.selectedCulture == null)
		{
			return;
		}
		this.checkNameInput(true);
		Config.selectedCulture = null;
	}

	// Token: 0x040010EE RID: 4334
	public NameInput nameInput;

	// Token: 0x040010EF RID: 4335
	public Text text_description;

	// Token: 0x040010F0 RID: 4336
	public Text text_values;

	// Token: 0x040010F1 RID: 4337
	public Text text_rare_knowledges;

	// Token: 0x040010F2 RID: 4338
	public CityIcon population;

	// Token: 0x040010F3 RID: 4339
	public CityIcon cities;

	// Token: 0x040010F4 RID: 4340
	public CityIcon kingdoms;

	// Token: 0x040010F5 RID: 4341
	public CityIcon knowledge_gain;

	// Token: 0x040010F6 RID: 4342
	public CityIcon level;

	// Token: 0x040010F7 RID: 4343
	public CityIcon zones;

	// Token: 0x040010F8 RID: 4344
	public CityIcon spreadSpeed;

	// Token: 0x040010F9 RID: 4345
	public CityIcon convertChance;

	// Token: 0x040010FA RID: 4346
	public Transform tech_common;

	// Token: 0x040010FB RID: 4347
	public Transform tech_rare;

	// Token: 0x040010FC RID: 4348
	private TechElement tech_prefab;

	// Token: 0x040010FD RID: 4349
	public StatBar researchBar;

	// Token: 0x040010FE RID: 4350
	public Image iconCurrentTech;

	// Token: 0x040010FF RID: 4351
	public Image cultureElement;

	// Token: 0x04001100 RID: 4352
	public Image cultureDecor;
}
