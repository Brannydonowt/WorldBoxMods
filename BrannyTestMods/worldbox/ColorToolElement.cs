using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000205 RID: 517
public class ColorToolElement : MonoBehaviour
{
	// Token: 0x06000B88 RID: 2952 RVA: 0x0006F494 File Offset: 0x0006D694
	public void create(KingdomColorContainer pRace, KingdomColor pColor)
	{
		this.colorContainer = pColor;
		BannerContainer bannerContainer = BannerGenerator.dict[pRace.race];
		this.background.sprite = bannerContainer.backrounds.GetRandom<Sprite>();
		this.icon.sprite = bannerContainer.icons.GetRandom<Sprite>();
		this.setColors(this.colorContainer);
		this.colorBorderInside = this.colorContainer.colorBorderInside;
		this.colorBorderOut = this.colorContainer.colorBorderOut;
		this.colorBorderBannerIcon = this.colorContainer.colorBorderBannerIcon;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0006F524 File Offset: 0x0006D724
	private void setColors(KingdomColor pColorContainer)
	{
		if (pColorContainer == null)
		{
			return;
		}
		if (this.borderInside == null)
		{
			return;
		}
		this.borderInside.color = pColorContainer.colorBorderInside;
		this.borderOutside.color = pColorContainer.colorBorderOut;
		this.background.color = pColorContainer.colorBorderOut;
		this.icon.color = pColorContainer.colorBorderBannerIcon;
		this.roof.color = pColorContainer.colorBorderInside;
		this.cityNameText.color = pColorContainer.colorBorderOut;
		this.name = pColorContainer.name;
		this.cityNameText.text = this.name;
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0006F5C8 File Offset: 0x0006D7C8
	private void OnValidate()
	{
		if (this.colorContainer == null)
		{
			return;
		}
		this.colorContainer.colorBorderInside = this.colorBorderInside;
		this.colorContainer.colorBorderOut = this.colorBorderOut;
		this.colorContainer.colorBorderBannerIcon = this.colorBorderBannerIcon;
		this.colorContainer.name = this.name;
		this.setColors(this.colorContainer);
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0006F62E File Offset: 0x0006D82E
	public void update(Color pColor)
	{
	}

	// Token: 0x04000DAF RID: 3503
	[Header("Edit Colors")]
	public Color colorBorderInside;

	// Token: 0x04000DB0 RID: 3504
	public Color colorBorderOut;

	// Token: 0x04000DB1 RID: 3505
	public Color colorBorderBannerIcon;

	// Token: 0x04000DB2 RID: 3506
	[Header("Edit Name")]
	public new string name;

	// Token: 0x04000DB3 RID: 3507
	[Header("Other Stuff")]
	[Space(30f)]
	public Image background;

	// Token: 0x04000DB4 RID: 3508
	public Image icon;

	// Token: 0x04000DB5 RID: 3509
	public Text cityNameText;

	// Token: 0x04000DB6 RID: 3510
	public Image roof;

	// Token: 0x04000DB7 RID: 3511
	public Image borderInside;

	// Token: 0x04000DB8 RID: 3512
	public Image borderOutside;

	// Token: 0x04000DB9 RID: 3513
	[HideInInspector]
	public KingdomColor colorContainer;

	// Token: 0x04000DBA RID: 3514
	[HideInInspector]
	public KingdomColorContainer raceContainer;
}
