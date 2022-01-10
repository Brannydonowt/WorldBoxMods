using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027D RID: 637
public class CitiesListElement : MonoBehaviour
{
	// Token: 0x06000E0B RID: 3595 RVA: 0x0008408E File Offset: 0x0008228E
	public void click()
	{
		Config.selectedCity = this.city;
		if (Config.selectedCity == null)
		{
			return;
		}
		ScrollWindow.showWindow("village");
	}

	// Token: 0x040010D9 RID: 4313
	internal City city;

	// Token: 0x040010DA RID: 4314
	public Text text_name;

	// Token: 0x040010DB RID: 4315
	public Text population;

	// Token: 0x040010DC RID: 4316
	public Text army;

	// Token: 0x040010DD RID: 4317
	public Text zones;

	// Token: 0x040010DE RID: 4318
	public Text age;

	// Token: 0x040010DF RID: 4319
	public Image raceIcon;

	// Token: 0x040010E0 RID: 4320
	public BannerLoader banner;
}
