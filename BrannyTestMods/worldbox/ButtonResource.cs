using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200024E RID: 590
public class ButtonResource : MonoBehaviour
{
	// Token: 0x06000CD8 RID: 3288 RVA: 0x0007B85C File Offset: 0x00079A5C
	private void Start()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0007B8A9 File Offset: 0x00079AA9
	internal void load(ResourceAsset pAsset, int pAmount)
	{
		this.asset = pAsset;
		if (this.asset == null)
		{
			return;
		}
		base.GetComponent<Image>().sprite = pAsset.getSprite();
		this.textAmount.text = (pAmount.ToString() ?? "");
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0007B8E7 File Offset: 0x00079AE7
	private void showHoverTooltip()
	{
		if (!Config.tooltipsActive)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0007B8F8 File Offset: 0x00079AF8
	private void showTooltip()
	{
		Tooltip.info_resource = this.asset;
		Tooltip.instance.show(base.gameObject, "resource", null, null);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, 0.8f, 0.1f), 26);
	}

	// Token: 0x04000FB2 RID: 4018
	public Text textAmount;

	// Token: 0x04000FB3 RID: 4019
	private ResourceAsset asset;
}
