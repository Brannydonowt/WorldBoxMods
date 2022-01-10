using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002BA RID: 698
public class TipButton : MonoBehaviour
{
	// Token: 0x06000F53 RID: 3923 RVA: 0x0008A551 File Offset: 0x00088751
	private void Awake()
	{
		this.action = new TooltipAction(this.showTooltipDefault);
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x0008A568 File Offset: 0x00088768
	private void Start()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltipOnClick));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0008A5B5 File Offset: 0x000887B5
	private void showTooltipOnClick()
	{
		this.action();
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x0008A5C2 File Offset: 0x000887C2
	private void showHoverTooltip()
	{
		if (!Config.tooltipsActive)
		{
			return;
		}
		this.action();
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x0008A5D8 File Offset: 0x000887D8
	public void showTooltipDefault()
	{
		if (this.textOnClick == "loyalty")
		{
			this.city.getRelationRating();
			Tooltip.info_city = this.city;
		}
		if (Config.isMobile)
		{
			if (!string.IsNullOrEmpty(this.text_override_non_steam))
			{
				this.textOnClick = this.text_override_non_steam;
			}
			if (!string.IsNullOrEmpty(this.description_override_non_steam))
			{
				this.textOnClickDescription = this.description_override_non_steam;
			}
		}
		Tooltip.info_tip = this.textOnClick;
		Tooltip.instance.show(base.gameObject, this.type, this.textOnClick, this.textOnClickDescription);
		base.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, 1f, 0.1f), 26);
	}

	// Token: 0x04001236 RID: 4662
	internal City city;

	// Token: 0x04001237 RID: 4663
	public string textOnClick;

	// Token: 0x04001238 RID: 4664
	public string textOnClickDescription;

	// Token: 0x04001239 RID: 4665
	public string text_override_non_steam = string.Empty;

	// Token: 0x0400123A RID: 4666
	public string description_override_non_steam = string.Empty;

	// Token: 0x0400123B RID: 4667
	public TooltipAction action;

	// Token: 0x0400123C RID: 4668
	public bool return_if_same_object;

	// Token: 0x0400123D RID: 4669
	public string type = "tip";
}
