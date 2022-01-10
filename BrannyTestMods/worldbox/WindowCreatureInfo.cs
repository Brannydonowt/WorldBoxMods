using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EA RID: 746
public class WindowCreatureInfo : MonoBehaviour
{
	// Token: 0x06001053 RID: 4179 RVA: 0x0008ECC4 File Offset: 0x0008CEC4
	private void Update()
	{
		this.checkNameInput(false);
		if (this.favoriteFoodBg.gameObject.activeSelf)
		{
			this.favoriteFoodBg.transform.Rotate(Vector3.forward * 70f * Time.deltaTime, Space.Self);
		}
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0008ED14 File Offset: 0x0008CF14
	private void checkNameInput(bool pDeactivate = false)
	{
		if (this.nameInput.inputField.isFocused)
		{
			if (Config.selectedUnit == null)
			{
				return;
			}
			Config.selectedUnit.data.firstName = this.nameInput.textField.text;
		}
		if (pDeactivate)
		{
			this.nameInput.inputField.DeactivateInputField();
		}
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0008ED74 File Offset: 0x0008CF74
	private void OnEnable()
	{
		if (Config.selectedUnit == null)
		{
			return;
		}
		Actor selectedUnit = Config.selectedUnit;
		this.nameInput.setText(selectedUnit.data.firstName);
		this.health.setBar((float)selectedUnit.data.health, (float)selectedUnit.curStats.health, selectedUnit.data.health.ToString() + "/" + selectedUnit.curStats.health.ToString());
		if (selectedUnit.stats.needFood || selectedUnit.stats.unit)
		{
			this.hunger.gameObject.SetActive(true);
			int num = (int)((float)selectedUnit.data.hunger / (float)selectedUnit.stats.maxHunger * 100f);
			this.hunger.setBar((float)num, 100f, num.ToString() + "%");
		}
		else
		{
			this.hunger.gameObject.SetActive(false);
		}
		this.damage.gameObject.SetActive(true);
		this.armor.gameObject.SetActive(true);
		this.speed.gameObject.SetActive(true);
		this.attackSpeed.gameObject.SetActive(true);
		this.crit.gameObject.SetActive(true);
		this.diplomacy.gameObject.SetActive(true);
		this.warfare.gameObject.SetActive(true);
		this.stewardship.gameObject.SetActive(true);
		this.intelligence.gameObject.SetActive(true);
		if (!selectedUnit.stats.unit)
		{
			this.diplomacy.gameObject.SetActive(false);
			this.warfare.gameObject.SetActive(false);
			this.stewardship.gameObject.SetActive(false);
			this.intelligence.gameObject.SetActive(false);
		}
		if (!selectedUnit.stats.inspect_stats)
		{
			this.damage.gameObject.SetActive(false);
			this.armor.gameObject.SetActive(false);
			this.speed.gameObject.SetActive(false);
			this.diplomacy.gameObject.SetActive(false);
			this.attackSpeed.gameObject.SetActive(false);
			this.crit.gameObject.SetActive(false);
		}
		this.damage.text.text = (selectedUnit.curStats.damage.ToString() ?? "");
		this.armor.text.text = selectedUnit.curStats.armor.ToString() + "%";
		this.speed.text.text = (selectedUnit.curStats.speed.ToString() ?? "");
		this.crit.text.text = selectedUnit.curStats.crit.ToString() + "%";
		this.attackSpeed.text.text = (selectedUnit.curStats.attackSpeed.ToString() ?? "");
		this.showAttribute(this.diplomacy.text, selectedUnit.curStats.diplomacy);
		this.showAttribute(this.stewardship.text, selectedUnit.curStats.stewardship);
		this.showAttribute(this.intelligence.text, selectedUnit.curStats.intelligence);
		this.showAttribute(this.warfare.text, selectedUnit.curStats.warfare);
		Sprite sprite = (Sprite)Resources.Load("ui/Icons/" + selectedUnit.stats.icon, typeof(Sprite));
		this.icon.sprite = sprite;
		this.avatarLoader.load(selectedUnit);
		if (selectedUnit.stats.hideFavoriteIcon)
		{
			this.iconFavorite.transform.parent.gameObject.SetActive(false);
		}
		else
		{
			this.iconFavorite.transform.parent.gameObject.SetActive(true);
		}
		this.text_description.text = "";
		this.text_values.text = "";
		this.showStat("creature_statistics_age", Config.selectedUnit.data.age);
		if (selectedUnit.stats.inspect_kills)
		{
			this.showStat("creature_statistics_kills", Config.selectedUnit.data.kills);
		}
		if (selectedUnit.stats.inspect_experience)
		{
			this.showStat("creature_statistics_character_experience", Config.selectedUnit.data.experience.ToString() + "/" + Config.selectedUnit.getExpToLevelup().ToString());
		}
		if (selectedUnit.stats.inspect_experience)
		{
			this.showStat("creature_statistics_character_level", Config.selectedUnit.data.level);
		}
		if (selectedUnit.stats.inspect_children)
		{
			this.showStat("creature_statistics_children", Config.selectedUnit.data.children);
		}
		this.moodBG.gameObject.SetActive(false);
		this.favoriteFoodBg.gameObject.SetActive(false);
		this.favoriteFoodSprite.gameObject.SetActive(false);
		if (selectedUnit.stats.unit && !selectedUnit.stats.baby)
		{
			string pValue = "??";
			if (!string.IsNullOrEmpty(Config.selectedUnit.data.favoriteFood))
			{
				pValue = LocalizedTextManager.getText(Config.selectedUnit.data.favoriteFood, null);
				this.favoriteFoodBg.gameObject.SetActive(true);
				this.favoriteFoodSprite.gameObject.SetActive(true);
				this.favoriteFoodSprite.sprite = AssetManager.resources.get(Config.selectedUnit.data.favoriteFood).getSprite();
			}
			this.showStat("creature_statistics_favorite_food", pValue);
		}
		if (selectedUnit.stats.unit)
		{
			this.moodBG.gameObject.SetActive(true);
			this.showStat("creature_statistics_mood", LocalizedTextManager.getText("mood_" + selectedUnit.data.mood, null));
			MoodAsset moodAsset = AssetManager.moods.get(selectedUnit.data.mood);
			this.moodSprite.sprite = moodAsset.getSprite();
			if (selectedUnit.s_personality != null)
			{
				this.showStat("creature_statistics_personality", LocalizedTextManager.getText("personality_" + selectedUnit.s_personality.id, null));
			}
		}
		Text text = this.text_description;
		text.text += "\n";
		Text text2 = this.text_values;
		text2.text += "\n";
		if (selectedUnit.stats.inspect_home)
		{
			string pID = "creature_statistics_homeVillage";
			object pValue2 = (Config.selectedUnit.city != null) ? Config.selectedUnit.city.data.cityName : "??";
			Kingdom kingdom = Config.selectedUnit.kingdom;
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
			this.showStat(pID, pValue2, (color2 != null) ? new Color32?(color2.GetValueOrDefault()) : null);
		}
		if (Config.selectedUnit.kingdom != null && Config.selectedUnit.kingdom.isCiv())
		{
			string pID2 = "kingdom";
			object name = Config.selectedUnit.kingdom.name;
			Kingdom kingdom2 = Config.selectedUnit.kingdom;
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
			Color? color2 = color3;
			this.showStat(pID2, name, (color2 != null) ? new Color32?(color2.GetValueOrDefault()) : null);
		}
		Culture culture = MapBox.instance.cultures.get(selectedUnit.data.culture);
		if (culture != null)
		{
			string text3 = "";
			text3 += culture.name;
			text3 = text3 + "[" + culture.followers.ToString() + "]";
			text3 = Toolbox.coloredString(text3, new Color32?(culture.color32_text));
			this.showStat("culture", text3);
			this.buttonCultures.SetActive(true);
		}
		else
		{
			this.buttonCultures.SetActive(false);
		}
		if (Config.selectedUnit.stats.isBoat)
		{
			Boat component = Config.selectedUnit.GetComponent<Boat>();
			this.showStat("passengers", component.unitsInside.Count);
			if (component.isState(BoatState.TransportDoLoading))
			{
				this.showStat("status", LocalizedTextManager.getText("status_waiting_for_passengers", null));
			}
		}
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
		if (selectedUnit.city == null)
		{
			this.buttonCity.SetActive(false);
		}
		else
		{
			this.buttonCity.SetActive(true);
		}
		if (selectedUnit.kingdom == null || !selectedUnit.kingdom.isCiv())
		{
			this.buttonKingdom.SetActive(false);
		}
		else
		{
			this.buttonKingdom.SetActive(true);
		}
		this.backgroundCiv.SetActive(this.buttonCity.activeSelf || this.buttonKingdom.activeSelf);
		this.updateFavoriteIconFor(selectedUnit);
		this.clearPrevButtons();
		this.loadTraits();
		this.loadEquipment();
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0008F798 File Offset: 0x0008D998
	private void showAttribute(Text pText, int pValue)
	{
		string text = pValue.ToString() ?? "";
		if (pValue < 4)
		{
			text = Toolbox.coloredText(text, Toolbox.color_negative, false);
		}
		else if (pValue >= 20)
		{
			text = Toolbox.coloredText(text, Toolbox.color_positive, false);
		}
		pText.text = text;
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0008F7E4 File Offset: 0x0008D9E4
	private void showStat(string pID, object pValue)
	{
		Text text = this.text_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null) + "\n";
		Text text2 = this.text_values;
		text2.text = text2.text + ((pValue != null) ? pValue.ToString() : null) + "\n";
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x0008F83C File Offset: 0x0008DA3C
	private void showStat(string pID, object pValue, Color32? pColor)
	{
		Text text = this.text_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null) + "\n";
		Text text2 = this.text_values;
		text2.text = text2.text + Toolbox.coloredString(pValue.ToString(), pColor) + "\n";
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0008F894 File Offset: 0x0008DA94
	private void clearPrevButtons()
	{
		for (int i = 0; i < this.traitsParent.childCount; i++)
		{
			Transform child = this.traitsParent.GetChild(i);
			if (!(child.name == "Title"))
			{
				Object.Destroy(child.gameObject);
			}
		}
		for (int j = 0; j < this.equipmentParent.childCount; j++)
		{
			Transform child = this.equipmentParent.GetChild(j);
			if (!(child.name == "Title"))
			{
				Object.Destroy(child.gameObject);
			}
		}
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0008F924 File Offset: 0x0008DB24
	private void loadTraits()
	{
		Actor selectedUnit = Config.selectedUnit;
		int num = 0;
		int count = selectedUnit.data.traits.Count;
		if (selectedUnit.data.traits != null)
		{
			for (int i = 0; i < selectedUnit.data.traits.Count; i++)
			{
				this.loadTraitButton(selectedUnit.data.traits[i], num, count);
				num++;
			}
		}
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x0008F990 File Offset: 0x0008DB90
	private void loadEquipment()
	{
		Actor selectedUnit = Config.selectedUnit;
		this.temp_equipment.Clear();
		this.equipmentParent.gameObject.SetActive(false);
		if (selectedUnit.equipment == null)
		{
			return;
		}
		this.equipmentParent.gameObject.SetActive(true);
		if (selectedUnit.equipment.weapon.data != null)
		{
			this.temp_equipment.Add(selectedUnit.equipment.weapon);
		}
		if (selectedUnit.equipment.helmet.data != null)
		{
			this.temp_equipment.Add(selectedUnit.equipment.helmet);
		}
		if (selectedUnit.equipment.armor.data != null)
		{
			this.temp_equipment.Add(selectedUnit.equipment.armor);
		}
		if (selectedUnit.equipment.boots.data != null)
		{
			this.temp_equipment.Add(selectedUnit.equipment.boots);
		}
		if (selectedUnit.equipment.ring.data != null)
		{
			this.temp_equipment.Add(selectedUnit.equipment.ring);
		}
		if (selectedUnit.equipment.amulet.data != null)
		{
			this.temp_equipment.Add(selectedUnit.equipment.amulet);
		}
		int num = 0;
		int count = this.temp_equipment.Count;
		if (this.temp_equipment.Count > 0)
		{
			for (int i = 0; i < this.temp_equipment.Count; i++)
			{
				this.loadEquipmentButton(this.temp_equipment[i], num, count);
				num++;
			}
		}
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x0008FB14 File Offset: 0x0008DD14
	private void loadEquipmentButton(ActorEquipmentSlot pSlot, int pIndex, int pTotal)
	{
		EquipmentButton equipmentButton = Object.Instantiate<EquipmentButton>(this.prefabEquipment, this.equipmentParent);
		equipmentButton.load(pSlot);
		RectTransform component = equipmentButton.GetComponent<RectTransform>();
		float num = 10f;
		float num2 = 22.4f;
		float num3 = 136f - num * 1.5f;
		float num4 = num2 * 0.8f;
		if ((float)pTotal * num4 >= num3)
		{
			num4 = num3 / (float)pTotal;
		}
		float x = num + num4 * (float)pIndex;
		float y = -11f;
		component.anchoredPosition = new Vector2(x, y);
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x0008FB88 File Offset: 0x0008DD88
	private void loadTraitButton(string pID, int pIndex, int pTotal)
	{
		TraitButton traitButton = Object.Instantiate<TraitButton>(this.prefabTrait, this.traitsParent);
		traitButton.load(pID);
		RectTransform component = traitButton.GetComponent<RectTransform>();
		float num = 10f;
		float num2 = 22.4f;
		float num3 = 136f - num * 1.5f;
		float num4 = num2 * 0.7f;
		if ((float)pTotal * num4 >= num3)
		{
			num4 = num3 / (float)pTotal;
		}
		float x = num + num4 * (float)pIndex;
		float y = -11f;
		component.anchoredPosition = new Vector2(x, y);
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x0008FBFC File Offset: 0x0008DDFC
	private void updateFavoriteIconFor(Actor pUnit)
	{
		if (pUnit.data.favorite)
		{
			this.iconFavorite.color = Color.white;
			return;
		}
		this.iconFavorite.color = new Color(1f, 1f, 1f, 0.5f);
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0008FC4C File Offset: 0x0008DE4C
	public void pressFavorite()
	{
		if (Config.selectedUnit == null)
		{
			return;
		}
		Actor selectedUnit = Config.selectedUnit;
		selectedUnit.data.favorite = !selectedUnit.data.favorite;
		this.updateFavoriteIconFor(selectedUnit);
		if (selectedUnit.data.favorite)
		{
			WorldTip.showNowTop("tip_favorite_icon");
		}
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0008FCA4 File Offset: 0x0008DEA4
	private void OnDisable()
	{
		if (Config.selectedUnit == null)
		{
			return;
		}
		this.checkNameInput(true);
		Config.selectedUnit = null;
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0008FCC1 File Offset: 0x0008DEC1
	public void clickKingdom()
	{
		Config.selectedKingdom = Config.selectedUnit.kingdom;
		ScrollWindow.moveAllToLeftAndRemove(true);
		ScrollWindow.showWindow("kingdom");
		ScrollWindow.get("kingdom").GetComponent<KingdomWindow>().showInfo();
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0008FCF6 File Offset: 0x0008DEF6
	public void clickVillage()
	{
		Config.selectedCity = Config.selectedUnit.city;
		ScrollWindow.moveAllToLeftAndRemove(true);
		ScrollWindow.get("village").clickShow();
		ScrollWindow.get("village").GetComponent<CityWindow>().showInfo();
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x0008FD30 File Offset: 0x0008DF30
	public void clickCulture()
	{
		if (string.IsNullOrEmpty(Config.selectedUnit.data.culture))
		{
			return;
		}
		Config.selectedCulture = MapBox.instance.cultures.get(Config.selectedUnit.data.culture);
		ScrollWindow.showWindow("culture");
	}

	// Token: 0x0400135F RID: 4959
	public Image favoriteFoodSprite;

	// Token: 0x04001360 RID: 4960
	public Image favoriteFoodBg;

	// Token: 0x04001361 RID: 4961
	public Image moodSprite;

	// Token: 0x04001362 RID: 4962
	public Image moodBG;

	// Token: 0x04001363 RID: 4963
	public TraitButton prefabTrait;

	// Token: 0x04001364 RID: 4964
	public EquipmentButton prefabEquipment;

	// Token: 0x04001365 RID: 4965
	public Transform traitsParent;

	// Token: 0x04001366 RID: 4966
	public Transform equipmentParent;

	// Token: 0x04001367 RID: 4967
	public StatBar health;

	// Token: 0x04001368 RID: 4968
	public StatBar hunger;

	// Token: 0x04001369 RID: 4969
	public CityIcon damage;

	// Token: 0x0400136A RID: 4970
	public CityIcon speed;

	// Token: 0x0400136B RID: 4971
	public CityIcon armor;

	// Token: 0x0400136C RID: 4972
	public CityIcon attackSpeed;

	// Token: 0x0400136D RID: 4973
	public CityIcon crit;

	// Token: 0x0400136E RID: 4974
	public CityIcon diplomacy;

	// Token: 0x0400136F RID: 4975
	public CityIcon warfare;

	// Token: 0x04001370 RID: 4976
	public CityIcon stewardship;

	// Token: 0x04001371 RID: 4977
	public CityIcon intelligence;

	// Token: 0x04001372 RID: 4978
	public NameInput nameInput;

	// Token: 0x04001373 RID: 4979
	public Image icon;

	// Token: 0x04001374 RID: 4980
	public UnitAvatarLoader avatarLoader;

	// Token: 0x04001375 RID: 4981
	public Image iconFavorite;

	// Token: 0x04001376 RID: 4982
	public GameObject buttonKingdom;

	// Token: 0x04001377 RID: 4983
	public GameObject buttonCity;

	// Token: 0x04001378 RID: 4984
	public GameObject backgroundCiv;

	// Token: 0x04001379 RID: 4985
	public Text text_description;

	// Token: 0x0400137A RID: 4986
	public Text text_values;

	// Token: 0x0400137B RID: 4987
	public GameObject buttonCultures;

	// Token: 0x0400137C RID: 4988
	private List<ActorEquipmentSlot> temp_equipment = new List<ActorEquipmentSlot>();
}
