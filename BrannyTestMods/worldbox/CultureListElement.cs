using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027F RID: 639
public class CultureListElement : MonoBehaviour
{
	// Token: 0x06000E12 RID: 3602 RVA: 0x0008435C File Offset: 0x0008255C
	public void clickCulture()
	{
		Config.selectedCulture = this.culture;
		ScrollWindow.showWindow("culture");
	}

	// Token: 0x040010E5 RID: 4325
	public new Text name;

	// Token: 0x040010E6 RID: 4326
	public Culture culture;

	// Token: 0x040010E7 RID: 4327
	public Text textFollowers;

	// Token: 0x040010E8 RID: 4328
	public Text textZones;

	// Token: 0x040010E9 RID: 4329
	public Text textCities;

	// Token: 0x040010EA RID: 4330
	public Text textLevel;

	// Token: 0x040010EB RID: 4331
	public Image cultureElement;

	// Token: 0x040010EC RID: 4332
	public Image cultureDecor;

	// Token: 0x040010ED RID: 4333
	public Image iconRace;
}
