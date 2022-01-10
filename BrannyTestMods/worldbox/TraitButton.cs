using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200028C RID: 652
public class TraitButton : MonoBehaviour
{
	// Token: 0x06000E65 RID: 3685 RVA: 0x00085F8C File Offset: 0x0008418C
	private void Start()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00085FDC File Offset: 0x000841DC
	internal void load(string pTrait)
	{
		this.trait = AssetManager.traits.get(pTrait);
		if (this.trait == null)
		{
			return;
		}
		Sprite sprite = (Sprite)Resources.Load("ui/Icons/" + this.trait.icon, typeof(Sprite));
		base.GetComponent<Image>().sprite = sprite;
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x00086039 File Offset: 0x00084239
	private void showHoverTooltip()
	{
		if (!Config.tooltipsActive)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0008604C File Offset: 0x0008424C
	private void showTooltip()
	{
		string pTitle = "trait_" + this.trait.id;
		string text = "trait_" + this.trait.id + "_info";
		if (!LocalizedTextManager.stringExists(text))
		{
			text = null;
		}
		Tooltip.instance.show(base.gameObject, "normal", pTitle, text);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, 0.8f, 0.1f), 26);
	}

	// Token: 0x04001138 RID: 4408
	private ActorTrait trait;
}
