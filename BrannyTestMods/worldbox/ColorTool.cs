using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class ColorTool : MonoBehaviour
{
	// Token: 0x06000B83 RID: 2947 RVA: 0x0006F2AC File Offset: 0x0006D4AC
	public void init()
	{
		this.cleanup();
		ref KingdomColorsData ptr = KingdomColors.init(null);
		this.curX = this.startX;
		this.curY = this.startY;
		BannerGenerator.loadBannersFromResources();
		foreach (KingdomColorContainer kingdomColorContainer in ptr.colors)
		{
			foreach (KingdomColor pColor in kingdomColorContainer.list)
			{
				this.createColorToolElement(kingdomColorContainer, pColor);
			}
			this.curY += this.raceStepY;
			this.curX = this.startX;
		}
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0006F384 File Offset: 0x0006D584
	public void cleanup()
	{
		while (this.container.childCount > 0)
		{
			Object.DestroyImmediate(this.container.GetChild(0).gameObject);
		}
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0006F3AC File Offset: 0x0006D5AC
	private void createColorToolElement(KingdomColorContainer pRace, KingdomColor pColor)
	{
		Vector3 position = new Vector3((float)this.curX, (float)this.curY);
		this.curX += this.colorStepX;
		ColorToolElement component = Object.Instantiate<GameObject>(this.prefab, this.container).GetComponent<ColorToolElement>();
		component.create(pRace, pColor);
		component.transform.position = position;
		component.transform.name = pRace.race + "_" + pColor.name;
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0006F42C File Offset: 0x0006D62C
	public void save()
	{
		for (int i = 0; i < this.container.childCount; i++)
		{
			this.container.GetChild(i).GetComponent<ColorToolElement>();
		}
		KingdomColors.exportToFile();
	}

	// Token: 0x04000DA5 RID: 3493
	public string colorString;

	// Token: 0x04000DA6 RID: 3494
	public string raceID = "human";

	// Token: 0x04000DA7 RID: 3495
	public GameObject prefab;

	// Token: 0x04000DA8 RID: 3496
	public Transform container;

	// Token: 0x04000DA9 RID: 3497
	public int startX;

	// Token: 0x04000DAA RID: 3498
	public int startY = 20;

	// Token: 0x04000DAB RID: 3499
	public int raceStepY = 10;

	// Token: 0x04000DAC RID: 3500
	public int colorStepX = 10;

	// Token: 0x04000DAD RID: 3501
	private int curX;

	// Token: 0x04000DAE RID: 3502
	private int curY;
}
