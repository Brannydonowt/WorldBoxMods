using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026D RID: 621
public class Tooltip : MonoBehaviour
{
	// Token: 0x06000DB2 RID: 3506 RVA: 0x00080311 File Offset: 0x0007E511
	private void Awake()
	{
		Tooltip.instance = this;
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00080328 File Offset: 0x0007E528
	public void checkClear()
	{
		if (!base.gameObject.activeSelf && Tooltip.lastObject != null)
		{
			if (this.clearTimer < 0.2f)
			{
				this.clearTimer += Time.deltaTime;
				return;
			}
			Tooltip.lastObject = null;
		}
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x00080375 File Offset: 0x0007E575
	public bool isShowingFor(GameObject pObject)
	{
		return Tooltip.lastObject == pObject;
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00080388 File Offset: 0x0007E588
	public void show(GameObject pObject, string pType, string pTitle = null, string pDescription = null)
	{
		if (CanvasMain.tooltip_show_timeout > 0f)
		{
			return;
		}
		if (Input.mousePresent)
		{
			if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(1)) && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
			{
				return;
			}
			if (Input.GetAxis("Mouse ScrollWheel") != 0f)
			{
				return;
			}
		}
		Tooltip.lastObject = pObject;
		base.gameObject.SetActive(true);
		base.transform.localScale = new Vector3(1f, 0.1f, 1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScaleY(base.transform, 1f, 0.1f), 34);
		this.timeout = 0.1f;
		this.description.gameObject.SetActive(false);
		this.stats_description.gameObject.SetActive(false);
		this.stats_values.gameObject.SetActive(false);
		this.iconBack.gameObject.SetActive(false);
		this.description.rectTransform.sizeDelta = new Vector2(100f, 23f);
		this.description.rectTransform.anchoredPosition = new Vector2(0f, -23f);
		this.description.alignment = 4;
		this.list.Clear();
		this.temp_description = "";
		this.temp_description_stat = "";
		this.temp_value = "";
		if (pType == "kingdom")
		{
			this.name.text = Toolbox.coloredText(Tooltip.info_kingdom.name ?? "", Toolbox.colorToHex(Tooltip.info_kingdom.kingdomColor.colorBorderOut, false), false);
			this.stats_description.gameObject.SetActive(true);
			this.stats_values.gameObject.SetActive(true);
			this.setKingdomOpinion();
		}
		else if (pType == "normal")
		{
			if (pTitle != null)
			{
				this.name.text = LocalizedTextManager.getText(pTitle, null);
			}
			if (pDescription != null)
			{
				this.temp_description = LocalizedTextManager.getText(pDescription, null);
			}
		}
		else if (pType == "map_meta")
		{
			this.showMapMeta();
		}
		else if (pType == "equipment")
		{
			this.showEquipment();
		}
		else
		{
			if (pType == "resource")
			{
				try
				{
					this.showResource();
					goto IL_432;
				}
				catch (Exception)
				{
					Debug.Log("0?? " + ScrollWindow.currentWindows[0].screen_id);
					string str = "1?? ";
					ResourceAsset resourceAsset = Tooltip.info_resource;
					Debug.Log(str + ((resourceAsset != null) ? resourceAsset.ToString() : null) == null);
					string str2 = "2?? ";
					ResourceAsset resourceAsset2 = Tooltip.info_resource;
					Debug.Log(str2 + ((resourceAsset2 != null) ? resourceAsset2.id : null));
					throw;
				}
			}
			if (pType == "world_law")
			{
				this.name.text = LocalizedTextManager.getText(Tooltip.info_tip, null);
				this.temp_description = LocalizedTextManager.getText(pDescription, null);
				if (Config.isMobile)
				{
					this.temp_description += "\n\n";
					this.temp_description += Toolbox.coloredText(LocalizedTextManager.getText("world_laws_tip_mobile_tap", null), "#999999", false);
				}
			}
			else if (pType == "tip")
			{
				if (LocalizedTextManager.stringExists(Tooltip.info_tip))
				{
					this.name.text = LocalizedTextManager.getText(Tooltip.info_tip, null);
				}
				else
				{
					this.name.text = Tooltip.info_tip;
				}
				if (!string.IsNullOrEmpty(pDescription))
				{
					this.temp_description = pDescription;
					this.temp_description = LocalizedTextManager.getText(pDescription, null);
					if (this.temp_description.Contains("$favorite_food$"))
					{
						string newValue = "??";
						if (!string.IsNullOrEmpty(Config.selectedUnit.data.favoriteFood))
						{
							newValue = LocalizedTextManager.getText(Config.selectedUnit.data.favoriteFood, null);
						}
						this.temp_description = this.temp_description.Replace("$favorite_food$", newValue);
					}
				}
				if (Tooltip.info_tip == "loyalty")
				{
					this.setLoyalty();
				}
			}
		}
		IL_432:
		if (this.list.Count > 0)
		{
			this.list.Sort(new Comparison<TooltipOpinionInfo>(this.sorter));
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.list.Count; i++)
			{
				TooltipOpinionInfo tooltipOpinionInfo = this.list[i];
				if (tooltipOpinionInfo.value > 0)
				{
					flag = true;
				}
				if (tooltipOpinionInfo.value < 0 && !flag2 && i > 0 && flag)
				{
					flag2 = true;
					this.temp_description_stat += "\n---";
					this.temp_value += "\n---";
				}
				if (i > 0)
				{
					this.temp_description_stat += "\n";
					this.temp_value += "\n";
				}
				if (tooltipOpinionInfo.value > 0)
				{
					this.temp_value += Toolbox.coloredText("+" + tooltipOpinionInfo.value.ToString(), Toolbox.color_positive, false);
					this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText(tooltipOpinionInfo.description, null), "#43FF43", false);
				}
				else
				{
					this.temp_value += Toolbox.coloredText(tooltipOpinionInfo.value.ToString() ?? "", Toolbox.color_negative, false);
					this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText(tooltipOpinionInfo.description, null), "#FB2C21", false);
				}
			}
			this.stats_description.text = this.temp_description_stat;
			this.stats_values.text = this.temp_value;
		}
		float num = 23.6f;
		float num2 = -num;
		Vector2 sizeDelta = new Vector2(this.rect.sizeDelta.x, num);
		bool flag3 = this.temp_description != "" && this.iconBack.gameObject.activeSelf;
		if (this.temp_description != "")
		{
			this.description.text = this.temp_description;
			this.description.gameObject.SetActive(true);
			if (!flag3)
			{
				num2 = this.description.rectTransform.localPosition.y - this.description.preferredHeight - 5f;
				sizeDelta.y += this.description.preferredHeight + 5f;
			}
		}
		if (this.iconBack.gameObject.activeSelf)
		{
			this.iconBack.rectTransform.anchoredPosition = new Vector2(15.5f, this.description.rectTransform.localPosition.y);
			if (!flag3)
			{
				num2 -= 17f;
				sizeDelta.y += 17f;
			}
		}
		if (flag3)
		{
			float num3;
			if (this.description.preferredHeight > this.iconBack.rectTransform.sizeDelta.y)
			{
				num3 = this.description.preferredHeight + 5f;
			}
			else
			{
				num3 = this.iconBack.rectTransform.sizeDelta.y + 5f;
			}
			num2 -= num3;
			sizeDelta.y += num3;
		}
		if (this.stats_description.gameObject.activeSelf || this.stats_description.gameObject.activeSelf)
		{
			this.stats_description.rectTransform.localPosition = new Vector3(0f, num2);
			this.stats_values.rectTransform.localPosition = new Vector3(0f, num2);
			sizeDelta.y += this.stats_description.preferredHeight;
			sizeDelta.y += 6f;
		}
		this.rect.sizeDelta = sizeDelta;
		float num4 = pObject.transform.position.x;
		float num5 = pObject.transform.position.y;
		float scaleFactor = base.transform.parent.GetComponent<Canvas>().scaleFactor;
		float num6 = pObject.GetComponent<RectTransform>().sizeDelta.y * 0.5f * pObject.transform.localScale.y * scaleFactor;
		num5 -= num6;
		float num7 = num5 - this.rect.sizeDelta.y * scaleFactor;
		this.debugPosY = num7;
		this.debugPosSizeY = (float)Screen.height;
		float num8 = num4 - this.rect.sizeDelta.x * scaleFactor;
		float num9 = num4 + this.rect.sizeDelta.x * scaleFactor;
		if (num7 < 0f)
		{
			num5 += this.rect.sizeDelta.y * scaleFactor + num6 * 2f;
		}
		if (num8 < 0f)
		{
			num4 = pObject.transform.position.x;
			num4 += pObject.GetComponent<RectTransform>().sizeDelta.x * 0.5f * pObject.transform.localScale.x * scaleFactor;
			num4 += this.rect.sizeDelta.x * scaleFactor * 0.5f;
			num5 = pObject.transform.position.y;
			num5 += this.rect.sizeDelta.y * scaleFactor * 0.5f;
		}
		else if (num9 > (float)Screen.width)
		{
			num4 = pObject.transform.position.x;
			num4 -= pObject.GetComponent<RectTransform>().sizeDelta.x * 0.5f * pObject.transform.localScale.x * scaleFactor;
			num4 -= this.rect.sizeDelta.x * scaleFactor * 0.5f;
			num5 = pObject.transform.position.y;
			num5 += this.rect.sizeDelta.y * scaleFactor * 0.5f;
		}
		num7 = num5 - this.rect.sizeDelta.y * scaleFactor;
		if (num7 < 0f)
		{
			num5 = this.rect.sizeDelta.y * scaleFactor;
		}
		this.rect.position = new Vector2(num4, num5);
		if (!this.stats_values.gameObject.activeSelf && !this.description.gameObject.activeSelf)
		{
			this.topGraphics.sprite = this.tooltipTopGraphicsNormal;
			this.topGraphics.rectTransform.sizeDelta = this.rect.sizeDelta;
		}
		else
		{
			this.topGraphics.sprite = this.tooltipTopGraphicsFlat;
			Vector2 sizeDelta2 = this.rect.sizeDelta;
			sizeDelta2.y = 21.6f;
			this.topGraphics.rectTransform.sizeDelta = sizeDelta2;
		}
		this.name.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.stats_values.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.description.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.stats_description.GetComponent<LocalizedText>().checkSpecialLanguages();
		if (LocalizedTextManager.isRTLLang())
		{
			this.stats_description.alignment = 2;
			this.stats_values.alignment = 0;
			return;
		}
		this.stats_description.alignment = 0;
		this.stats_values.alignment = 2;
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00080F34 File Offset: 0x0007F134
	private void showResource()
	{
		this.name.text = LocalizedTextManager.getText(Tooltip.info_resource.id, null);
		this.temp_description_stat += LocalizedTextManager.getText("amount", null);
		this.temp_value += Config.selectedCity.data.storage.get(Tooltip.info_resource.id).ToString();
		if (Tooltip.info_resource.id == "gold")
		{
			this.addItemText("yearly_gain", Config.selectedCity.gold_change, false, true, false);
			this.temp_description_stat += "\n";
			this.temp_value += "\n";
			this.addItemText("tax", Config.selectedCity.gold_in_tax, false, true, false);
			this.temp_description_stat += "\n---";
			this.temp_value += "\n---";
			if (Config.selectedCity.gold_out_army != 0)
			{
				this.addItemText("upkeep_army", -Config.selectedCity.gold_out_army, false, true, false);
			}
			if (Config.selectedCity.gold_out_buildigs != 0)
			{
				this.addItemText("upkeep_buildings", -Config.selectedCity.gold_out_buildigs, false, true, false);
			}
			if (Config.selectedCity.gold_out_homeless != 0)
			{
				this.addItemText("upkeep_homeless", -Config.selectedCity.gold_out_homeless, false, true, false);
			}
		}
		this.stats_description.gameObject.SetActive(true);
		this.stats_values.gameObject.SetActive(true);
		this.stats_description.text = this.temp_description_stat;
		this.stats_values.text = this.temp_value;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00081104 File Offset: 0x0007F304
	private void showMapMeta()
	{
		if (Tooltip.info_map_meta == null)
		{
			return;
		}
		this.name.text = Tooltip.info_map_meta.mapStats.name;
		string pColor = "#95DD5D";
		this.addLineIntText("world_age", Tooltip.info_map_meta.mapStats.year, pColor);
		this.addLineIntText("kingdoms", Tooltip.info_map_meta.kingdoms, pColor);
		this.addLineIntText("villages", Tooltip.info_map_meta.cities, pColor);
		this.addLineIntText("population", Tooltip.info_map_meta.population, pColor);
		this.addLineIntText("world_laws_tab_mobs", Tooltip.info_map_meta.mobs, pColor);
		this.addLineIntText("world_laws_tab_mobs", Tooltip.info_map_meta.mobs, pColor);
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		this.addLineText("created", Tooltip.info_map_meta.temp_date_string.ToString(), "#F3961F");
		this.stats_description.gameObject.SetActive(true);
		this.stats_values.gameObject.SetActive(true);
		this.stats_description.text = this.temp_description_stat;
		this.stats_values.text = this.temp_value;
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00081280 File Offset: 0x0007F480
	private void showEquipment()
	{
		if (Tooltip.info_equipment_slot == null)
		{
			return;
		}
		ItemData data = Tooltip.info_equipment_slot.data;
		if (data == null)
		{
			return;
		}
		string text = "item_" + data.id;
		if (data.prefix != "0")
		{
			text = text + "_" + data.prefix;
		}
		string text2 = LocalizedTextManager.getText("item_name_template", null);
		text2 = text2.Replace("$item_id$", LocalizedTextManager.getText(text, null));
		if (data.suffix == "0")
		{
			text2 = text2.Replace("$item_suffix$", "");
		}
		else
		{
			text2 = text2.Replace("$item_suffix$", LocalizedTextManager.getText("item_suffix_of_" + data.suffix, null));
		}
		this.iconBack.gameObject.SetActive(true);
		string text3 = "ui/Icons/items/icon_" + data.id;
		if (data.material != "base")
		{
			text3 = text3 + "_" + data.material;
		}
		Sprite sprite = (Sprite)Resources.Load(text3, typeof(Sprite));
		this.icon.sprite = sprite;
		ItemTools.calcItemValues(data);
		string itemClass = ItemTools.getItemClass(data.type);
		this.addItemText("damage", ItemTools.s_stats.damage, false, true, false);
		this.addItemText("attackSpeed", (int)ItemTools.s_stats.attackSpeed, false, true, false);
		this.addItemText("crit", (int)ItemTools.s_stats.crit, true, true, false);
		this.addItemText("armor", ItemTools.s_stats.armor, true, true, false);
		this.addItemText("speed", (int)ItemTools.s_stats.speed, false, true, false);
		this.addItemText("diplomacy", ItemTools.s_stats.diplomacy, false, true, false);
		this.addItemText("health", ItemTools.s_stats.health, false, true, false);
		this.addItemText("damage", (int)ItemTools.s_stats.mod_damage, true, true, true);
		this.addItemText("attackSpeed", (int)ItemTools.s_stats.mod_attackSpeed, true, true, true);
		this.addItemText("crit", (int)ItemTools.s_stats.mod_crit, true, true, true);
		this.addItemText("armor", (int)ItemTools.s_stats.mod_armor, true, true, true);
		this.addItemText("speed", (int)ItemTools.s_stats.mod_speed, true, true, true);
		this.addItemText("diplomacy", (int)ItemTools.s_stats.mod_diplomacy, true, true, true);
		this.addItemText("health", (int)ItemTools.s_stats.mod_health, true, true, true);
		if (this.temp_value.Length > 0)
		{
			this.stats_description.gameObject.SetActive(true);
			this.stats_values.gameObject.SetActive(true);
			this.stats_description.text = this.temp_description_stat;
			this.stats_values.text = this.temp_value;
		}
		string str = "";
		string pColor = "#ffffff";
		switch (ItemTools.s_quality)
		{
		case ItemQuality.Junk:
			pColor = "#9E9E9E";
			str = "";
			break;
		case ItemQuality.Normal:
			pColor = "#FFFFFF";
			str = "";
			break;
		case ItemQuality.Rare:
			pColor = "#66AFFF";
			str = "_rare";
			break;
		case ItemQuality.Epic:
			pColor = "#FFF15E";
			str = "_epic";
			break;
		case ItemQuality.Legendary:
			pColor = "#FF7028";
			str = "_legendary";
			break;
		}
		if (data.material != "base")
		{
			text2 = "(" + LocalizedTextManager.getText("item_mat_" + data.material, null) + ") " + text2;
		}
		this.name.text = Toolbox.coloredText(text2, pColor, false);
		this.description.alignment = 3;
		this.description.rectTransform.sizeDelta = new Vector2(76f, 35f);
		this.description.rectTransform.anchoredPosition = new Vector2(15f, -23f);
		string text4 = LocalizedTextManager.getText(itemClass + str, null);
		string text5 = LocalizedTextManager.getText("item_template_description_full", null);
		string text6 = LocalizedTextManager.getText("item_template_description_age_only", null);
		if (string.IsNullOrEmpty(data.by))
		{
			text5 = text6;
		}
		int num = MapBox.instance.mapStats.year - Tooltip.info_equipment_slot.data.year + 1;
		text5 = text5.Replace("$item_creator_name$", Tooltip.info_equipment_slot.data.by);
		text5 = text5.Replace("$item_creator_kingdom$", Tooltip.info_equipment_slot.data.from);
		text5 = text5.Replace("$item_creator_years$", num.ToString() ?? "");
		if (num < 1)
		{
			text5 = text5.Replace("$year_ending$", LocalizedTextManager.getText("item_template_description_year", null) ?? "");
		}
		else
		{
			text5 = text5.Replace("$year_ending$", LocalizedTextManager.getText("item_template_description_years", null) ?? "");
		}
		this.temp_description += Toolbox.coloredText(text4, pColor, false);
		this.temp_description += "\n";
		this.temp_description = this.temp_description + "\n" + Toolbox.coloredText(text5, pColor, false);
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x000817D4 File Offset: 0x0007F9D4
	private void addItemText(string pID, int pValue, bool pPercent = false, bool pColor = true, bool pMod = false)
	{
		if (pValue == 0)
		{
			return;
		}
		if (this.temp_description_stat.Length > 0)
		{
			this.temp_description_stat += "\n";
			this.temp_value += "\n";
		}
		int num = pValue;
		string text = num.ToString() ?? "";
		if (!pColor)
		{
			this.temp_value += Toolbox.coloredText(text + (pPercent ? "%" : ""), "#ffffff", false);
			this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText(pID, null), "#ffffff", false);
			return;
		}
		if (pValue > 0)
		{
			this.temp_value += Toolbox.coloredText("+" + text + (pPercent ? "%" : ""), "#43FF43", false);
			this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText(pID, null), "#43FF43", false);
			return;
		}
		this.temp_value += Toolbox.coloredText(text + (pPercent ? "%" : ""), "#FB2C21", false);
		this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText(pID, null), "#FB2C21", false);
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x0008193D File Offset: 0x0007FB3D
	private void addLineIntText(string pID, int pValue, string pColor)
	{
		if (pValue == 0)
		{
			return;
		}
		this.addLineText(pID, pValue.ToString(), pColor);
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00081954 File Offset: 0x0007FB54
	private void addLineText(string pID, string pValue, string pColor)
	{
		if (this.temp_description_stat.Length > 0)
		{
			this.temp_description_stat += "\n";
			this.temp_value += "\n";
		}
		this.temp_value += Toolbox.coloredText(pValue, pColor, false);
		this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText(pID, null), pColor, false);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x000819D4 File Offset: 0x0007FBD4
	private void setKingdomOpinion()
	{
		DiplomacyRelation relation = MapBox.instance.kingdoms.diplomacyManager.getRelation(Tooltip.info_kingdom, Config.selectedKingdom);
		relation.recalculate();
		KingdomOpinion opinion = relation.getOpinion(Config.selectedKingdom, Tooltip.info_kingdom);
		this.temp_description_stat += LocalizedTextManager.getText("race", null);
		this.temp_value += LocalizedTextManager.getText(Tooltip.info_kingdom.race.nameLocale, null);
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		this.temp_description_stat += LocalizedTextManager.getText("kingdom_statistics_age", null);
		this.temp_value += Tooltip.info_kingdom.age.ToString();
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		this.temp_description_stat += LocalizedTextManager.getText("population", null);
		this.temp_value += Tooltip.info_kingdom.getPopulationTotal().ToString();
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		this.temp_description_stat += LocalizedTextManager.getText("army", null);
		this.temp_value = this.temp_value + Tooltip.info_kingdom.countArmy().ToString() + "/" + Tooltip.info_kingdom.countArmyMax().ToString();
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		if (opinion._opinion_total >= 0)
		{
			this.temp_value += Toolbox.coloredText(opinion._opinion_total.ToString() ?? "", "#43FF43", false);
			this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText("opinion_total", null), "#43FF43", false);
		}
		else
		{
			this.temp_value += Toolbox.coloredText(opinion._opinion_total.ToString() ?? "", "#FB2C21", false);
			this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText("opinion_total", null), "#FB2C21", false);
		}
		this.temp_description_stat += "\n---";
		this.temp_value += "\n---";
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		if (opinion._opinion_king != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_king",
				value = opinion._opinion_king
			});
		}
		if (opinion._opinion_kings_mood != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_kings_mood",
				value = opinion._opinion_kings_mood
			});
		}
		if (opinion._opinion_is_supreme != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_is_supreme",
				value = opinion._opinion_is_supreme
			});
		}
		if (opinion._opinion_close_borders != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_close_borders",
				value = opinion._opinion_close_borders
			});
		}
		if (opinion._opinion_far_lands != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_far_lands",
				value = opinion._opinion_far_lands
			});
		}
		if (opinion._in_war != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_in_war",
				value = opinion._in_war
			});
		}
		if (opinion._opinion_same_wars != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_same_wars",
				value = opinion._opinion_same_wars
			});
		}
		if (opinion._opinion_race != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "race",
				value = opinion._opinion_race
			});
		}
		if (opinion._opinion_zones != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_zones",
				value = opinion._opinion_zones
			});
		}
		if (opinion._opinion_peace_time != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_peace_time",
				value = opinion._opinion_peace_time
			});
		}
		if (opinion._opinion_power != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_power",
				value = opinion._opinion_power
			});
		}
		if (opinion._opinion_traits != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_loyalty_traits",
				value = opinion._opinion_traits
			});
		}
		if (opinion._opinion_culture != 0)
		{
			string text;
			if (opinion._opinion_culture > 0)
			{
				text = "opinion_culture_same";
			}
			else
			{
				text = "opinion_culture_different";
			}
			this.list.Add(new TooltipOpinionInfo
			{
				description = text,
				value = opinion._opinion_culture
			});
		}
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00081FDC File Offset: 0x000801DC
	private void setLoyalty()
	{
		this.name.text = LocalizedTextManager.getText("loyalty", null);
		this.stats_description.gameObject.SetActive(true);
		this.stats_values.gameObject.SetActive(true);
		if (Tooltip.info_city._opinion_total > 0)
		{
			this.temp_value += Toolbox.coloredText(Tooltip.info_city._opinion_total.ToString() ?? "", "#43FF43", false);
			this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText("opinion_total", null), "#43FF43", false);
		}
		else
		{
			this.temp_value += Toolbox.coloredText(Tooltip.info_city._opinion_total.ToString() ?? "", "#FB2C21", false);
			this.temp_description_stat += Toolbox.coloredText(LocalizedTextManager.getText("opinion_total", null), "#FB2C21", false);
		}
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		this.temp_description_stat += "\n";
		this.temp_value += "\n";
		if (Tooltip.info_city._opinion_world_law != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_world_law",
				value = Tooltip.info_city._opinion_world_law
			});
		}
		if (Tooltip.info_city._opinion_superior_enemies != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_superior_enemies",
				value = Tooltip.info_city._opinion_superior_enemies
			});
		}
		if (Tooltip.info_city._opinion_close_to_capital != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_close_to_capital",
				value = Tooltip.info_city._opinion_close_to_capital
			});
		}
		if (Tooltip.info_city._opinion_cities != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_number_of_cities",
				value = Tooltip.info_city._opinion_cities
			});
		}
		if (Tooltip.info_city._opinion_king != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_king",
				value = Tooltip.info_city._opinion_king
			});
		}
		if (Tooltip.info_city._opinion_new_city != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_new_city",
				value = Tooltip.info_city._opinion_new_city
			});
		}
		if (Tooltip.info_city._opinion_culture != 0)
		{
			string text;
			if (Tooltip.info_city._opinion_culture > 0)
			{
				text = "opinion_culture_same";
			}
			else
			{
				text = "opinion_culture_different";
			}
			this.list.Add(new TooltipOpinionInfo
			{
				description = text,
				value = Tooltip.info_city._opinion_culture
			});
		}
		if (Tooltip.info_city._opinion_new_kingdom != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_new_kingdom",
				value = Tooltip.info_city._opinion_new_kingdom
			});
		}
		if (Tooltip.info_city._opinion_leader != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_leader",
				value = Tooltip.info_city._opinion_leader
			});
		}
		if (Tooltip.info_city._opinion_loyalty_traits != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_loyalty_traits",
				value = Tooltip.info_city._opinion_loyalty_traits
			});
		}
		if (Tooltip.info_city._opinion_loyalty_mood != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_leaders_loyalty_mood",
				value = Tooltip.info_city._opinion_loyalty_mood
			});
		}
		if (Tooltip.info_city._opinion_population != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_population",
				value = Tooltip.info_city._opinion_population
			});
		}
		if (Tooltip.info_city._opinion_zones != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_zones",
				value = Tooltip.info_city._opinion_zones
			});
		}
		if (Tooltip.info_city._opinion_distance != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_distance",
				value = Tooltip.info_city._opinion_distance
			});
		}
		if (Tooltip.info_city._opinion_capital != 0)
		{
			this.list.Add(new TooltipOpinionInfo
			{
				description = "opinion_capital",
				value = Tooltip.info_city._opinion_capital
			});
		}
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x000824E8 File Offset: 0x000806E8
	public int sorter(TooltipOpinionInfo p1, TooltipOpinionInfo p2)
	{
		return p2.value.CompareTo(p1.value);
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x000824FC File Offset: 0x000806FC
	private void Update()
	{
		if (this.timeout > 0f)
		{
			this.timeout -= Time.deltaTime;
			return;
		}
		if (Input.GetMouseButtonDown(0) || Input.GetAxis("Mouse ScrollWheel") != 0f || ScrollRectExtended.instance.isDragged())
		{
			this.hide();
		}
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00082554 File Offset: 0x00080754
	public void hide()
	{
		ShortcutExtensions.DOKill(base.transform, false);
		base.gameObject.SetActive(false);
		this.clearTimer = 0f;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0008257A File Offset: 0x0008077A
	public static void hideTooltip()
	{
		if (Tooltip.instance != null)
		{
			Tooltip.instance.hide();
			Tooltip.lastObject = null;
		}
	}

	// Token: 0x0400106A RID: 4202
	public static Kingdom info_kingdom;

	// Token: 0x0400106B RID: 4203
	public static string info_tip;

	// Token: 0x0400106C RID: 4204
	public static string info_tip_description;

	// Token: 0x0400106D RID: 4205
	public static ActorEquipmentSlot info_equipment_slot;

	// Token: 0x0400106E RID: 4206
	public static ResourceAsset info_resource;

	// Token: 0x0400106F RID: 4207
	public static City info_city;

	// Token: 0x04001070 RID: 4208
	public static MapMetaData info_map_meta;

	// Token: 0x04001071 RID: 4209
	public static Tooltip instance;

	// Token: 0x04001072 RID: 4210
	public static GameObject lastObject;

	// Token: 0x04001073 RID: 4211
	public Sprite tooltipTopGraphicsFlat;

	// Token: 0x04001074 RID: 4212
	public Sprite tooltipTopGraphicsNormal;

	// Token: 0x04001075 RID: 4213
	public Image icon;

	// Token: 0x04001076 RID: 4214
	public Image iconBack;

	// Token: 0x04001077 RID: 4215
	public Image topGraphics;

	// Token: 0x04001078 RID: 4216
	public Image background;

	// Token: 0x04001079 RID: 4217
	public new Text name;

	// Token: 0x0400107A RID: 4218
	public Text description;

	// Token: 0x0400107B RID: 4219
	public Text stats_description;

	// Token: 0x0400107C RID: 4220
	public Text stats_values;

	// Token: 0x0400107D RID: 4221
	private RectTransform rect;

	// Token: 0x0400107E RID: 4222
	public float debugPosY;

	// Token: 0x0400107F RID: 4223
	public float debugPosSizeY;

	// Token: 0x04001080 RID: 4224
	private float timeout;

	// Token: 0x04001081 RID: 4225
	private float clearTimer;

	// Token: 0x04001082 RID: 4226
	private List<TooltipOpinionInfo> list = new List<TooltipOpinionInfo>();

	// Token: 0x04001083 RID: 4227
	private string temp_description = "";

	// Token: 0x04001084 RID: 4228
	private string temp_description_stat = "";

	// Token: 0x04001085 RID: 4229
	private string temp_value = "";
}
