using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EB RID: 747
public class WorldLawElement : MonoBehaviour
{
	// Token: 0x06001065 RID: 4197 RVA: 0x0008FD94 File Offset: 0x0008DF94
	private void Awake()
	{
		this.lawID = base.transform.name;
		this.colorNormal = Toolbox.makeColor("#FFFFFF");
		this.colorDisabled = Toolbox.makeColor("#666666");
		this.colorDisabled.a = 0.6f;
		this.button.GetComponent<TipButton>().type = "world_law";
		this.button.GetComponent<TipButton>().textOnClick = this.lawID + "_title";
		this.button.GetComponent<TipButton>().textOnClickDescription = this.lawID + "_description";
		this.button.GetComponent<TipButton>().return_if_same_object = true;
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x0008FE48 File Offset: 0x0008E048
	private void OnEnable()
	{
		if (!Config.gameLoaded || Config.worldLoading)
		{
			return;
		}
		this.updateStatus();
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0008FE60 File Offset: 0x0008E060
	public void click()
	{
		if (Config.isMobile)
		{
			if (!Tooltip.instance.isShowingFor(this.button.gameObject))
			{
				return;
			}
			Tooltip.hideTooltip();
		}
		MapBox.instance.worldLaws.dict[this.lawID].boolVal = !MapBox.instance.worldLaws.dict[this.lawID].boolVal;
		this.updateStatus();
		if (this.lawID == "world_law_peaceful_monsters" && MapBox.instance.worldLaws.dict[this.lawID].boolVal)
		{
			MapBox.instance.stopAttacksFor(true);
		}
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0008FF18 File Offset: 0x0008E118
	private void updateStatus()
	{
		bool boolVal = MapBox.instance.worldLaws.dict[this.lawID].boolVal;
		this.toggleIcon.updateIcon(boolVal);
		if (boolVal)
		{
			this.icon.color = this.colorNormal;
			return;
		}
		this.icon.color = this.colorDisabled;
	}

	// Token: 0x0400137D RID: 4989
	public Button button;

	// Token: 0x0400137E RID: 4990
	public Image icon;

	// Token: 0x0400137F RID: 4991
	private string lawID;

	// Token: 0x04001380 RID: 4992
	private Color colorNormal;

	// Token: 0x04001381 RID: 4993
	private Color colorDisabled;

	// Token: 0x04001382 RID: 4994
	public ToggleIcon toggleIcon;
}
