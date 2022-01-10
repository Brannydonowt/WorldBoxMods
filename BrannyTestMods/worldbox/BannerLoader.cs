using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028E RID: 654
public class BannerLoader : MonoBehaviour
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x00086328 File Offset: 0x00084528
	public void load(Kingdom pKingdom)
	{
		this.kingdom = pKingdom;
		BannerGenerator.setSeed(pKingdom.bannerSeed);
		BannerContainer bannerContainer = BannerGenerator.dict[pKingdom.race.bannerId];
		this.partBackround.sprite = Toolbox.getRandom<Sprite>(bannerContainer.backrounds);
		this.partIcon.sprite = Toolbox.getRandom<Sprite>(bannerContainer.icons);
		this.partBackround.color = this.kingdom.kingdomColor.colorBorderOut;
		this.partIcon.color = this.kingdom.kingdomColor.colorBorderBannerIcon;
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x000863C0 File Offset: 0x000845C0
	public void showBannerTooltip()
	{
		if (Tooltip.lastObject == base.gameObject)
		{
			Config.selectedKingdom = this.kingdom;
			ScrollWindow.get("kingdom").showSameWindow();
			Tooltip.hideTooltip();
			return;
		}
		Tooltip.info_kingdom = this.kingdom;
		Tooltip.info_tip = "kingdom";
		Tooltip.instance.show(base.gameObject, "kingdom", null, null);
	}

	// Token: 0x0400113A RID: 4410
	public Image partBackround;

	// Token: 0x0400113B RID: 4411
	public Image partIcon;

	// Token: 0x0400113C RID: 4412
	private Kingdom kingdom;
}
