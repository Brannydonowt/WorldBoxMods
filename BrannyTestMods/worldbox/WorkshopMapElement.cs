using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C2 RID: 706
public class WorkshopMapElement : MonoBehaviour
{
	// Token: 0x06000F6E RID: 3950 RVA: 0x0008AAD0 File Offset: 0x00088CD0
	public void load(WorkshopMapData pData)
	{
		this.data = pData;
		this.textName.text = pData.meta_data_map.mapStats.name;
		this.textKingdoms.text = pData.meta_data_map.kingdoms.ToString();
		this.textCities.text = pData.meta_data_map.cities.ToString();
		this.textPopulation.text = pData.meta_data_map.population.ToString();
		this.textMobs.text = pData.meta_data_map.mobs.ToString();
		this.textUpvotes.text = pData.workshop_item.VotesUp.ToString();
		this.textComments.text = pData.workshop_item.NumComments.ToString();
		this.image.sprite = pData.sprite_small_preview;
		if (pData.workshop_item.Owner.Id.ToString() == Config.steamId)
		{
			this.textName.color = Toolbox.makeColor("#3DDEFF");
			this.ayeIcon.gameObject.SetActive(true);
			return;
		}
		this.textName.color = Toolbox.makeColor("#FF9B1C");
		this.ayeIcon.gameObject.SetActive(false);
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x0008AC2F File Offset: 0x00088E2F
	public void clickWorkshopMap()
	{
		SaveManager.currentWorkshopMapData = this.data;
		ScrollWindow.showWindow("steam_workshop_play_world");
	}

	// Token: 0x04001253 RID: 4691
	private WorkshopMapData data;

	// Token: 0x04001254 RID: 4692
	public Image image;

	// Token: 0x04001255 RID: 4693
	public Text textName;

	// Token: 0x04001256 RID: 4694
	public Text textKingdoms;

	// Token: 0x04001257 RID: 4695
	public Text textCities;

	// Token: 0x04001258 RID: 4696
	public Text textPopulation;

	// Token: 0x04001259 RID: 4697
	public Text textMobs;

	// Token: 0x0400125A RID: 4698
	public Text textUpvotes;

	// Token: 0x0400125B RID: 4699
	public Text textComments;

	// Token: 0x0400125C RID: 4700
	public Image mainBackground;

	// Token: 0x0400125D RID: 4701
	public Image ayeIcon;
}
