using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000282 RID: 642
public class TechElement : MonoBehaviour
{
	// Token: 0x06000E26 RID: 3622 RVA: 0x00084E1C File Offset: 0x0008301C
	private void Start()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00084E6C File Offset: 0x0008306C
	internal void load(CultureTechAsset pAsset)
	{
		this.asset = pAsset;
		if (this.asset == null)
		{
			return;
		}
		Sprite sprite = (Sprite)Resources.Load("ui/Icons/" + this.asset.icon, typeof(Sprite));
		base.GetComponent<Image>().sprite = sprite;
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x00084EBF File Offset: 0x000830BF
	internal void load(string pID)
	{
		this.load(AssetManager.culture_tech.get(pID));
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x00084ED2 File Offset: 0x000830D2
	private void showHoverTooltip()
	{
		if (!Config.tooltipsActive)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x00084EE4 File Offset: 0x000830E4
	private void showTooltip()
	{
		string pTitle = "tech_" + this.asset.id;
		string text = "tech_info_" + this.asset.id;
		if (!LocalizedTextManager.stringExists(text))
		{
			text = null;
		}
		Tooltip.instance.show(base.gameObject, "normal", pTitle, text);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, 0.8f, 0.1f), 26);
	}

	// Token: 0x04001105 RID: 4357
	private CultureTechAsset asset;
}
