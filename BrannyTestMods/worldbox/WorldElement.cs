using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CB RID: 715
public class WorldElement : MonoBehaviour
{
	// Token: 0x0400128E RID: 4750
	internal Map map;

	// Token: 0x0400128F RID: 4751
	internal ScrollWindow windowWorldList;

	// Token: 0x04001290 RID: 4752
	internal WorldListWindow worldListWindow;

	// Token: 0x04001291 RID: 4753
	public Text mapName;

	// Token: 0x04001292 RID: 4754
	public Text mapId;

	// Token: 0x04001293 RID: 4755
	public Text mapDescription;

	// Token: 0x04001294 RID: 4756
	public GameObject iconsGroup;

	// Token: 0x04001295 RID: 4757
	public Image raceHumans;

	// Token: 0x04001296 RID: 4758
	public Image raceOrcs;

	// Token: 0x04001297 RID: 4759
	public Image raceElves;

	// Token: 0x04001298 RID: 4760
	public Image raceDwarves;

	// Token: 0x04001299 RID: 4761
	public Text population;

	// Token: 0x0400129A RID: 4762
	public Text cities;

	// Token: 0x0400129B RID: 4763
	public Text houses;

	// Token: 0x0400129C RID: 4764
	public Text zones;

	// Token: 0x0400129D RID: 4765
	public GameObject tag1;

	// Token: 0x0400129E RID: 4766
	public Image tag1icon;

	// Token: 0x0400129F RID: 4767
	public GameObject tag2;

	// Token: 0x040012A0 RID: 4768
	public Image tag2icon;

	// Token: 0x040012A1 RID: 4769
	public GameObject year;

	// Token: 0x040012A2 RID: 4770
	public GameObject uploader;

	// Token: 0x040012A3 RID: 4771
	public Text tag1Text;

	// Token: 0x040012A4 RID: 4772
	public Text tag2Text;

	// Token: 0x040012A5 RID: 4773
	public Text yearText;

	// Token: 0x040012A6 RID: 4774
	public Text uploaderText;

	// Token: 0x040012A7 RID: 4775
	public Text downloads;

	// Token: 0x040012A8 RID: 4776
	public Text plays;

	// Token: 0x040012A9 RID: 4777
	public Text favs;

	// Token: 0x040012AA RID: 4778
	public MapPreviewImage mapPreviewImage;

	// Token: 0x040012AB RID: 4779
	public bool clickable = true;

	// Token: 0x040012AC RID: 4780
	public bool listPreview;
}
