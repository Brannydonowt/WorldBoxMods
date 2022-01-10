using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028F RID: 655
public class KingdomElement : MonoBehaviour
{
	// Token: 0x06000E70 RID: 3696 RVA: 0x00086433 File Offset: 0x00084633
	public void clickKingdomElement()
	{
		Config.selectedKingdom = this.kingdom;
		ScrollWindow.showWindow("kingdom");
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0008644A File Offset: 0x0008464A
	public void clickKing()
	{
		Config.selectedKingdom = this.kingdom;
		Config.selectedUnit = this.kingdom.king;
		if (Config.selectedUnit == null)
		{
			return;
		}
		ScrollWindow.showWindow("inspect_unit");
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x0008647F File Offset: 0x0008467F
	public void clickCapital()
	{
		Config.selectedKingdom = this.kingdom;
		Config.selectedCity = this.kingdom.capital;
		if (Config.selectedCity == null)
		{
			return;
		}
		ScrollWindow.showWindow("village");
	}

	// Token: 0x0400113D RID: 4413
	internal Kingdom kingdom;

	// Token: 0x0400113E RID: 4414
	public Image iconRace;

	// Token: 0x0400113F RID: 4415
	public Text textPopulation;

	// Token: 0x04001140 RID: 4416
	public Text textArmy;

	// Token: 0x04001141 RID: 4417
	public Text textCities;

	// Token: 0x04001142 RID: 4418
	public Text textHouses;

	// Token: 0x04001143 RID: 4419
	public Text textZones;

	// Token: 0x04001144 RID: 4420
	public Text kingdomName;

	// Token: 0x04001145 RID: 4421
	public GameObject buttonCapital;

	// Token: 0x04001146 RID: 4422
	public GameObject buttonKing;

	// Token: 0x04001147 RID: 4423
	public BannerLoader banner;
}
