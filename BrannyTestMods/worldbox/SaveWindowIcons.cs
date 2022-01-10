using System;
using Steamworks.Ugc;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000220 RID: 544
public class SaveWindowIcons : MonoBehaviour
{
	// Token: 0x06000C49 RID: 3145 RVA: 0x00078DC4 File Offset: 0x00076FC4
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.nameInput.gameObject.SetActive(this.allow_edit);
		this.descriptionInput.gameObject.SetActive(this.allow_edit);
		if (this.use_current_world_info)
		{
			SavedMap savedMap = SaveManager.currentWorldToSavedMap();
			this.metaData = savedMap.getMeta();
		}
		else if (SaveManager.currentWorkshopMapData != null)
		{
			this.metaData = SaveManager.currentWorkshopMapData.meta_data_map;
		}
		else
		{
			this.metaData = SaveManager.getCurrentMeta();
			this.save_path = SaveManager.currentSavePath;
		}
		if (this.metaData != null)
		{
			this.checkRaceIcons(this.metaData);
			if (this.allow_edit)
			{
				this.nameInput.setText(this.metaData.mapStats.name);
				this.descriptionInput.setText(this.metaData.mapStats.description);
			}
			if (MapSizePresset.getPreset(this.metaData.width) != null)
			{
				this.textMapSize.text = LocalizedTextManager.getText("map_size_" + MapSizePresset.getPreset(this.metaData.width).ToString(), null);
			}
			else
			{
				this.textMapSize.text = this.metaData.width.ToString() + "x" + this.metaData.height.ToString();
			}
			this.textMapAge.text = this.metaData.mapStats.year.ToString();
			this.textPopulation.text = this.metaData.population.ToString();
			this.textMobs.text = this.metaData.mobs.ToString();
			this.textKingdoms.text = this.metaData.kingdoms.ToString();
			this.textCities.text = this.metaData.cities.ToString();
			this.textBuildings.text = this.metaData.buildings.ToString();
			this.textCultures.text = this.metaData.cultures.ToString();
			this.mapName.text = this.metaData.mapStats.name;
			this.textDescription.text = this.metaData.mapStats.description;
			if (SaveManager.currentWorkshopMapData != null)
			{
				Item workshop_item = SaveManager.currentWorkshopMapData.workshop_item;
				if (workshop_item.Owner.Id.ToString() == Config.steamId)
				{
					this.mapName.color = Toolbox.makeColor("#3DDEFF");
					return;
				}
				this.mapName.color = Toolbox.makeColor("#FF9B1C");
				return;
			}
		}
		else
		{
			this.mapName.GetComponent<LocalizedText>().updateText(true);
		}
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0007908B File Offset: 0x0007728B
	private void Update()
	{
		this.checkNameInput(false);
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00079094 File Offset: 0x00077294
	private void OnDisable()
	{
		this.checkNameInput(true);
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x000790A0 File Offset: 0x000772A0
	private void checkNameInput(bool pDeactivate = false)
	{
		if (this.save_names_to_current_world)
		{
			if (this.nameInput.inputField.isFocused)
			{
				MapBox.instance.mapStats.name = this.nameInput.textField.text;
			}
			if (this.descriptionInput.inputField.isFocused)
			{
				MapBox.instance.mapStats.description = this.descriptionInput.textField.text;
			}
		}
		if (pDeactivate)
		{
			this.nameInput.inputField.DeactivateInputField();
			this.descriptionInput.inputField.DeactivateInputField();
		}
		if (this.save_meta_data_on_close && this.metaData != null)
		{
			this.metaData.mapStats.name = this.nameInput.textField.text;
			this.metaData.mapStats.description = this.descriptionInput.textField.text;
			SaveManager.saveMetaData(this.metaData, this.save_path);
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0007919C File Offset: 0x0007739C
	private void checkRaceIcons(MapMetaData pData)
	{
		this.raceOrcs.gameObject.SetActive(false);
		this.raceHumans.gameObject.SetActive(false);
		this.raceElves.gameObject.SetActive(false);
		this.raceDwarves.gameObject.SetActive(false);
		if (((pData != null) ? pData.races : null) != null)
		{
			this.raceOrcs.gameObject.SetActive(pData.races.Contains("orc"));
			this.raceHumans.gameObject.SetActive(pData.races.Contains("human"));
			this.raceElves.gameObject.SetActive(pData.races.Contains("elf"));
			this.raceDwarves.gameObject.SetActive(pData.races.Contains("dwarf"));
		}
	}

	// Token: 0x04000E9E RID: 3742
	public bool use_current_world_info;

	// Token: 0x04000E9F RID: 3743
	public bool allow_edit;

	// Token: 0x04000EA0 RID: 3744
	public bool save_meta_data_on_close;

	// Token: 0x04000EA1 RID: 3745
	public bool save_names_to_current_world;

	// Token: 0x04000EA2 RID: 3746
	private string save_path;

	// Token: 0x04000EA3 RID: 3747
	public GameObject raceOrcs;

	// Token: 0x04000EA4 RID: 3748
	public GameObject raceHumans;

	// Token: 0x04000EA5 RID: 3749
	public GameObject raceElves;

	// Token: 0x04000EA6 RID: 3750
	public GameObject raceDwarves;

	// Token: 0x04000EA7 RID: 3751
	public Text textMapSize;

	// Token: 0x04000EA8 RID: 3752
	public Text textMapAge;

	// Token: 0x04000EA9 RID: 3753
	public Text textPopulation;

	// Token: 0x04000EAA RID: 3754
	public Text textMobs;

	// Token: 0x04000EAB RID: 3755
	public Text textCultures;

	// Token: 0x04000EAC RID: 3756
	public Text textKingdoms;

	// Token: 0x04000EAD RID: 3757
	public Text textCities;

	// Token: 0x04000EAE RID: 3758
	public Text textBuildings;

	// Token: 0x04000EAF RID: 3759
	public Text mapName;

	// Token: 0x04000EB0 RID: 3760
	public Text textDescription;

	// Token: 0x04000EB1 RID: 3761
	public NameInput nameInput;

	// Token: 0x04000EB2 RID: 3762
	public NameInput descriptionInput;

	// Token: 0x04000EB3 RID: 3763
	private MapMetaData metaData;
}
